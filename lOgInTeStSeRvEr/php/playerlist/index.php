<?php
$allow_include = true;

require_once "../misc/playergenerator.php";
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>PLAYERLIST</title>
</head>
<body>
    <h2>Current Player Maker Configuration</h2>
    <?php
        foreach ($config_ini as $key => $value) {
            echo "$key : $value</br>";
        }
    ?>

    <h2>Email : Password List</h2>
    <?php
        foreach ($user_pass as $key => $value) {
            echo "$key : $value</br>";
        }
    ?>

    <h2>Email : Team List</h2>
    <?php
        foreach ($user_team as $key => $value) {
            echo "$key : $value</br>";
        }
    ?>
</body>
</html>
