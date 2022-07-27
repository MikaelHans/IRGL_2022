<?php 
if (!isset($allow_include)){
    header("Location: ./");
}

$config_ini = parse_ini_file(".CONSTANTS.ini");

$user_pass = [];
$user_team = [];

for ($i = 0; $i < $config_ini["PLAYER_COUNT"]; $i++){
    $gen_username = $config_ini["PLAYER_NAME"] . $i . $config_ini["PLAYER_EMAIL_URI"];
    $gen_password = $config_ini["PLAYER_NAME"] . $i;
    $gen_team = (int)($i / 2);
    $user_pass[$gen_username] = $gen_password;
    $user_team[$gen_username] = $gen_team;
}

