<?php

ini_set('display_errors', 1);

$allow_include = true;

// define some constants and content-type headers
require_once "../imports.php";

function getTeamScore(int $team_id){
    $score = DBGetData(TEAM_SCORE_TABLE_NAME, ["score"], "team_id = ?", [$team_id])[0]["score"];
    return (int)$score;
}

function checkAccountExists(string $email){
    return DBCheckExists(PARTICIPANT_TABLE_NAME, ["email"], "email = ?", [$email]);
}

function getTeamID(string $email){
    $team_id = DBGetData(PARTICIPANT_TABLE_NAME, ["team_id"], "email = ?", [$email])[0]["team_id"];
    return (int)$team_id;
}

function checkTeamSemifinalExists(int $team_id){
    return DBCheckExists(TEAM_SCORE_TABLE_NAME, ["team_id"], "team_id = ?", [$team_id]);
}

function updateTeamScore(int $team_id, int $score){
    DBUpdateData(TEAM_SCORE_TABLE_NAME, ["score"], [$score], "team_id = ?", [$team_id]);
}

// I KNOW THIS UPDATE IS UNSAFE BCS NO HANDSHAKE CHECKING BUT FUCK IT AM I RIGHT?

if (processJSONtoPOST()){

    if(isset($_POST["email"]) && isset($_POST["delta"]))
    {
        $email = $_POST["email"];
        $delta = $_POST["delta"];
        if ($email != "" && $delta != "")
        {
            if (requestFromUnity()){
                if(filter_var($email, FILTER_VALIDATE_EMAIL))
                {
                    if(checkAccountExists($email))
                    {
                        $team_id = getTeamID($email);
                        if(checkTeamSemifinalExists($team_id))
                        {
                            $score = getTeamScore($team_id);
                            $newscore = $score + $delta;
                            updateTeamScore($team_id, $newscore);
                            logAPIRequest($email, "update score", "success: updated score for team_id -> $team_id by $delta from $score to $newscore ", "");
                        }
                        else
                        {
                            try{
                                logAPIRequest($email, "update score", "failed: not semifinal account", "");
                            }catch (Exception $e){
                                // do nothing
                            }
                        }
                    }
                    else
                    {
                        try{
                            logAPIRequest($email, "update score", "failed: account not exist", "");
                        }catch (Exception $e){
                            // do nothing
                        }
                    }
                }
                else
                {
                    try{
                        logAPIRequest($email, "update score", "bad request: invalid email", "");
                    }catch (Exception $e){
                        // do nothing
                    }
                }
            }
            else
            {
                try{
                    logAPIRequest($email, "update score", "failed: request not from unity client", "");
                }catch (Exception $e){
                    // do nothing
                }
            }
        }
        else
        {
            try{
                logAPIRequest("unknown", "update score", "bad request: email or delta is empty", "");
            }catch (Exception $e){
                // do nothing
            }
        }
    }
    else
    {
        try{
            logAPIRequest("unknown", "update score", "bad request: missing email or password", "");
        }catch (Exception $e){
            // do nothing
        }
    }
    echo "This function does not return anything, if you see this, prepare to get disqualified :)";
    exit();
}

try{
    logAPIRequest("unknown", "update score", "invalid request", "return to home");
}catch (Exception $e){
    // do nothing
}

header("Location: ../");
exit();