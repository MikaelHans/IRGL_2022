<?php
ini_set('display_errors', 1);

$allow_include = true;

require_once "./imports.php";

// for ($i=0; $i < 50; $i++) { 
//     $gen_team_id = $i+1;
//     $gen_password = "team" . $gen_team_id;
//     $gen_pw_hash = password_hash($gen_password, PASSWORD_DEFAULT);
//     DBInsertData("2022_main_teams_beta", ["id", "password"], [$gen_team_id, $gen_pw_hash]);
// }

// for ($i=0; $i < 100; $i++) { 
//     $gen_team_id = ((int)($i/2))+1;
//     $gen_player = "player$i@test.com";
//     DBInsertData("2022_main_participants_beta", ["email", "team_id"], [$gen_player, $gen_team_id]);
// }

// for ($i=0; $i < 49; $i++) { 
//     $gen_team_id = $i+1;
//     DBInsertData(TEAM_SCORE_TABLE_NAME, ["team_id"], [$gen_team_id]);
// }