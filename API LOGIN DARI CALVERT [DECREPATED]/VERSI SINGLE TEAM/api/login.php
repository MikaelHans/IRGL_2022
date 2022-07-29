<?php
    include '../config/database.php';

    header('Content-Type: application/json');

    if (!isset($_POST['name']) || !isset($_POST['password'])) {
        http_response_code(400);
        echo json_encode(array(
            "success" => false,
            "message" => "Missing credentials"
        ));
        exit();
    }

    $sql = "SELECT id, name, password
            FROM 2022_main_teams
            WHERE name = ?";
    $stmt = $pdo->prepare($sql);
    $stmt->execute([$_POST['name']]);
    $team = $stmt->fetch();

    if ($team) {
        if (password_verify($_POST['password'], $team['password'])) {
            $sql = "SELECT logged_in
                    FROM 2022_semifinal_teams
                    WHERE id_team = ?";
            $stmt = $pdo->prepare($sql);
            $stmt->execute([$team['id']]);
            $team_logged_in = $stmt->fetch();

            if ($team_logged_in['logged_in'] == 1) {
                http_response_code(400);
                echo json_encode(array(
                    "success" => false,
                    "message" => "Already logged in"
                ));
                exit();
            }

            $sql = "UPDATE 2022_semifinal_teams
                    SET logged_in = 1
                    WHERE id_team = ?";
            $stmt = $pdo->prepare($sql);
            $stmt->execute([$team['id']]);

            http_response_code(200);
            echo json_encode(array(
                "success" => true,
                "message" => "Successfully logged in"
            ));
            exit();
        }
    }

    http_response_code(401);
    echo json_encode(array(
        "success" => false,
        "message" => "Wrong credentials"
    ));
    exit();
?>