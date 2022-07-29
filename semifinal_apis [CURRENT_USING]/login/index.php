<?php
ini_set('display_errors', 1);

$allow_include = true;

// define some constants and content-type headers
require_once "../imports.php";


function generateReturnJSON(bool $success, string $message, int $team_id = -999){
    $return_json = [
        "success" => $success,
        "message" => $message
    ];
    if ($team_id !== -999){
        $return_json["id_team"] = $team_id;
    }
    return json_encode($return_json);
}

define("ADMIN_DOMAIN_CHECK", "@admin.com");
define("ADMIN_PASSWORD", "alexander_liliana_joni");

function checkAdmin(string $email, string $password){
    return strpos($email, ADMIN_DOMAIN_CHECK) !== false && $password === ADMIN_PASSWORD;
}

function checkAccountExists(string $email){
    return DBCheckExists(PARTICIPANT_TABLE_NAME, ["email"], "email = ?", [$email]);
}

function checkTeamPassword(int $team_id, string $password){
    $password_hash = DBGetData(TEAM_DETAIL_TABLE_NAME, ["password"], "id = ?", [$team_id])[0]["password"];
    return password_verify($password, $password_hash);
}

function getTeamID(string $email){
    $team_id = DBGetData(PARTICIPANT_TABLE_NAME, ["team_id"], "email = ?", [$email])[0]["team_id"];
    return (int)$team_id;
}

function checkTeamSemifinalExists(int $team_id){
    return DBCheckExists(TEAM_SCORE_TABLE_NAME, ["team_id"], "team_id = ?", [$team_id]);
}

if(processJSONtoPOST()){
    if(isset($_POST["email"]) && isset($_POST["password"]))
    {
        $email = $_POST["email"];
        $password = $_POST["password"];
        if ($email != "" && $password != "")
        {
            if(requestFromUnity())
            {
                if(filter_var($email, FILTER_VALIDATE_EMAIL))
                {
                    if(!checkAdmin($email, $password))
                    {
                            if(checkAccountExists($email))
                            {
                                $team_id = getTeamID($email);
                                if(checkTeamSemifinalExists($team_id))
                                {
                                    if(checkTeamPassword($team_id, $password))
                                    {
                                        $returns = generateReturnJSON(true, "Login successful", $team_id);
                                        try{
                                            logAPIRequest($email, "login", "success: success login", $returns);
                                        }catch (Exception $e){
                                            // do nothing
                                        }
                                        echo $returns;
                                    }
                                    else
                                    {
                                        $returns = generateReturnJSON(false, "Incorrect password");
                                        try{
                                            logAPIRequest($email, "login", "failed: incorrect password", $returns);
                                        }catch (Exception $e){
                                            // do nothing
                                        }
                                        echo $returns;
                                    }
                                }
                                else
                                {
                                    $returns = generateReturnJSON(false, "You are not registered for the semifinals");
                                    try{
                                        logAPIRequest($email, "login", "failed: not semifinal account", $returns);
                                    }catch (Exception $e){
                                        // do nothing
                                    }
                                    echo $returns;
                                }
                            }
                            else
                            {
                                $returns = generateReturnJSON(false, "Account does not exist");
                                try{
                                    logAPIRequest($email, "login", "failed: account not exist", $returns);
                                }catch (Exception $e){
                                    // do nothing
                                }
                                echo $returns;
                            }
                        }
                        else
                        {
                            $returns = generateReturnJSON(true, "Login successful as admin", -1);
                            try{
                                logAPIRequest($email, "login", "success: logged in as admin", $returns);
                            }catch (Exception $e){
                                // do nothing
                            }
                            echo $returns;
                        }
                    }
                    else{
                        $returns = generateReturnJSON(false, "Invalid email");
                        try{
                            logAPIRequest("unknown", "login", "bad request: invalid email", $returns);
                        }catch (Exception $e){
                            // do nothing
                        }
                        echo $returns;
                    }
                }else{
                    $returns = generateReturnJSON(false, "Request not from Unity");
                    try{
                        logAPIRequest($email, "login", "failed: request not from unity client", $returns);
                    }catch (Exception $e){
                        // do nothing
                    }
                    echo $returns;
                }
            }
            else
            {
                $returns = generateReturnJSON(false, "Email or password cannot be empty");
                try{
                    logAPIRequest("unknown", "login", "bad request: email or password empty", $returns);
                }catch (Exception $e){
                    // do nothing
                }
                echo $returns;
            }
        }
        else
        {
            $returns = generateReturnJSON(false, "Email or password cannot be empty");
            try{
                logAPIRequest("unknown", "login", "bad request: missing email or password", $returns);
            }catch (Exception $e){
                // do nothing
            }
            echo $returns;
        }
    
    exit();
}

try{
    logAPIRequest("unknown", "login", "invalid request", "return to home");
}catch (Exception $e){
    // do nothing
}

header("Location: ../");
exit();