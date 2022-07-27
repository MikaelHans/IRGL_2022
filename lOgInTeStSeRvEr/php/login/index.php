<?php
ini_set('display_errors', 1);

$allow_include = true;

require_once "../misc/playergenerator.php";

header("Content-Type: application/json");

function generate_return_json($success, $message, $id_team = ""){
    $return_json = [
        "success" => $success,
        "message" => $message
    ];
    if ($id_team !== ""){
        $return_json["id_team"] = $id_team;
    }
    return json_encode($return_json);
}


if ($_SERVER["REQUEST_METHOD"] === "POST") {
    $_POST = json_decode(strtolower(file_get_contents('php://input')), true);
    if(isset($_POST["email"]) && isset($_POST["password"])){
        if (!array_key_exists($_POST["email"], $user_pass)){
            http_response_code(401);
            echo generate_return_json(false, "User not found");
            exit();

        }

        if ($user_pass[$_POST["email"]] != $_POST["password"]){
            http_response_code(401);
            echo generate_return_json(false, "Wrong password");
            exit();

        }

        echo generate_return_json(true, "Login successful", $user_team[$_POST["email"]]);
        exit();

    }
    exit();
}


header("Location: ../");
exit();