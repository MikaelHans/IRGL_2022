<?php
    include '../config/database.php';

    header('Content-Type: application/json');

    if (!isset($_POST['email']) || !isset($_POST['password'])) {
        http_response_code(400);
        echo json_encode(array(
            "success" => false,
            "message" => "Missing credentials"
        ));
        exit();
    }

    $sql = "SELECT 
                mp.id AS id_team_member,
                mp.email AS member_email,
                mt.password AS team_password
            FROM
                2022_main_participants mp
            JOIN
                2022_main_teams mt ON mp.team_id = mt.id
            WHERE
                mp.email = ?";
    $stmt = $pdo->prepare($sql);
    $stmt->execute([$_POST['email']]);
    $member = $stmt->fetch();

    if ($member) {
        if (password_verify($_POST['password'], $member['team_password'])) {
            $sql = "UPDATE
                        2022_semifinal_teams
                    SET
                        last_connected = NOW(),
                    WHERE
                        id_team_member = ?";
            $stmt = $pdo->prepare($sql);
            $stmt->execute([$member['id_team_member']]);

            http_response_code(200);
            echo json_encode(array(
                "success" => true,
                "message" => "Successfully logged in",
                "id_team" => $member['id_team_member']
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
