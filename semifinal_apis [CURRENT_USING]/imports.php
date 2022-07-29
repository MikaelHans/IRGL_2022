<?php
    if(!isset($allow_include)){
        header("Location: ../");
        exit();
    }


    $host = '127.0.0.1';
    $db = 'irgl';
    $user = 'irgl';
    $pass = 'dbiRglUKP';
    $charset = 'utf8mb4';
    $dsn = "mysql:host=$host;dbname=$db;charset=$charset;";
    $options = [
        PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC, 
        PDO::ATTR_EMULATE_PREPARES   => false,
    ];

    try {
        $pdo = new PDO($dsn, $user, $pass, $options);
    } catch (PDOException $e) {
        die ("PDOException :: " . $e->getMessage(). " " . $e->getCode());
    }
    define("PARTICIPANT_TABLE_NAME", "2022_main_participants_beta");
    define("TEAM_DETAIL_TABLE_NAME", "2022_main_teams_beta");

    define("TEAM_SCORE_TABLE_NAME", "2022_semifinal_score");
    // define("PARTICIPANT_TABLE_NAME", "2022_main_participants");
    // define("TEAM_DETAIL_TABLE_NAME", "2022_main_teams");
    define("API_LOGGER_TABLE_NAME", "2022_semifinal_api_logger");
    
    function DBInsertData(string $table, array $columns, array $values) {
        global $pdo;
        $columns_string = implode(',', $columns);
        $values_string = implode(',', array_map(function () {
            return "?";
        }, $values));
        $stmt = $pdo->prepare("INSERT INTO $table($columns_string) VALUES($values_string)");
        $stmt->execute($values);
        return $pdo->lastInsertId();
    }

    function DBGetData(string $table, array $columns, string $conditions = "", array $conditions_values = []) {
        global $pdo;
        $columns_string = implode(',', $columns);
        $conditions_string = $conditions != "" ? "WHERE $conditions" : "";
        $stmt = $pdo->prepare("SELECT $columns_string FROM $table $conditions_string");
        $stmt->execute($conditions_values);
        return $stmt->fetchAll();
    }

    function DBCheckExists(string $table, array $columns, string $conditions = "", array $conditions_values = []) {
        global $pdo;
        $data = DBGetData($table, $columns, $conditions, $conditions_values);
        return count($data) > 0;
    }

    function DBUpdateData(string $table, array $columns, array $values, string $conditions = "", array $conditions_values = []) {
        global $pdo;
        $columns_string = implode(',', array_map(function ($column) {
            return "$column = ?";
        }, $columns));
        $conditions_string = $conditions != "" ? "WHERE $conditions" : "";
        $stmt = $pdo->prepare("UPDATE $table SET $columns_string $conditions_string");
        $stmt->execute(array_merge($values, $conditions_values));
        return $stmt->rowCount();
    }

    function processJSONtoPOST() {
        global $_POST;
        $_POST = json_decode(strtolower(file_get_contents('php://input')), true);
        return $_SERVER["REQUEST_METHOD"] === "POST";
    }

    function logAPIRequest(
        string $requester, 
        string $action,
        string $action_detail,
        string $server_returns
        ){
        $received = (string) file_get_contents('php://input');
        
        $localipcheckregex = "/^192\.168.*/i";
        $pushua = "unknown";
        $puship = "unknown";
        $pushcity = "unknown";
        $pushreg = "unknown";
        $pushcoun = "unknown";
        $pushisp = "unknown";

        
        if(isset($_SERVER['HTTP_USER_AGENT']))
            $pushua = $_SERVER['HTTP_USER_AGENT'];

        if (isset($_SERVER['HTTP_CLIENT_IP']))
            $puship = $_SERVER['HTTP_CLIENT_IP'];
        else if(isset($_SERVER['HTTP_X_FORWARDED_FOR']))
            $puship = $_SERVER['HTTP_X_FORWARDED_FOR'];
        else if(isset($_SERVER['HTTP_X_FORWARDED']))
            $puship = $_SERVER['HTTP_X_FORWARDED'];
        else if(isset($_SERVER['HTTP_FORWARDED_FOR']))
            $puship = $_SERVER['HTTP_FORWARDED_FOR'];
        else if(isset($_SERVER['HTTP_FORWARDED']))
            $puship = $_SERVER['HTTP_FORWARDED'];
        else if(isset($_SERVER['REMOTE_ADDR']))
            $puship = $_SERVER['REMOTE_ADDR'];


        if($puship!="unknown"){
            @$getlocAPI = file_get_contents("http://ipinfo.io/$puship?token=4098c8c7ca55e9");
            $getlocAPI = json_decode($getlocAPI);
            
            if(isset($getlocAPI->city)){
                $pushcity = $getlocAPI->city;
            }
            if(isset($getlocAPI->region)){
                $pushreg = $getlocAPI->region;
            }
            if(isset($getlocAPI->country)){
                $pushcoun = $getlocAPI->country;
            }
            if(isset($getlocAPI->org)){
                $pushisp = $getlocAPI->org;
            }else{
                $pushisp = gethostbyaddr($puship);
            }

            if(preg_match($localipcheckregex, $puship) == 1){
                $pushcity = "LOCAL";
                $pushreg = "LOCAL";
                $pushcoun = "LOCAL";
                $pushisp = "LOCAL";
            }
        }

        DBInsertData(
            API_LOGGER_TABLE_NAME,
            [   
                "request_method",
                "requester_data",
                "received_data",
                "action",
                "action_detail",
                "server_return_data",
                "current_uri",
                "user_agent",
                "ip_address",
                "region",
                "city",
                "country",
                "isp",
            ],
            [
                $_SERVER['REQUEST_METHOD'],
                $requester,
                $received,
                $action,
                $action_detail,
                $server_returns,
                $_SERVER['REQUEST_URI'],
                $pushua,
                $puship,
                $pushreg,
                $pushcity,
                $pushcoun,
                $pushisp,
            ]
        );
    }

    function requestFromUnity(){
        if (isset($_SERVER['HTTP_USER_AGENT']))
            return (strpos($_SERVER['HTTP_USER_AGENT'], 'UnityWebRequest') !== false);
        return false;
    }

    header("Content-Type: application/json");
