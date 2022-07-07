-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 28, 2022 at 07:10 PM
-- Server version: 10.4.19-MariaDB
-- PHP Version: 7.3.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `irgl`
--

-- --------------------------------------------------------

--
-- Table structure for table `2022_semifinal_teams`
--

CREATE TABLE `2022_semifinal_teams` (
  `id` smallint(6) NOT NULL,
  `id_team` smallint(6) NOT NULL,
  `id_team_member` int(11) NOT NULL,
  `point` int(11) NOT NULL DEFAULT 0,
  `data` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT '{}',
  `last_connected` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `2022_semifinal_teams`
--

INSERT INTO `2022_semifinal_teams` (`id`, `id_team`, `id_team_member`, `point`, `data`, `last_connected`) VALUES
(1, 50, 117, 0, '{}', NULL),
(2, 50, 118, 0, '{}', NULL),
(3, 50, 119, 0, '{}', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `2022_semifinal_teams`
--
ALTER TABLE `2022_semifinal_teams`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `2022_semifinal_teams`
--
ALTER TABLE `2022_semifinal_teams`
  MODIFY `id` smallint(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
