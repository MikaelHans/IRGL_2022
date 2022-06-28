-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 28, 2022 at 06:54 PM
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
-- Table structure for table `2022_main_teams`
--

CREATE TABLE `2022_main_teams` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `password` text NOT NULL,
  `leader_id` int(11) DEFAULT NULL,
  `participant_count` int(11) NOT NULL,
  `school` text NOT NULL,
  `status` int(11) NOT NULL DEFAULT 0,
  `verificator` text DEFAULT NULL,
  `date_of_verification` datetime DEFAULT NULL,
  `payment_filepath` text NOT NULL,
  `date_of_registration` timestamp NOT NULL DEFAULT current_timestamp(),
  `reset_password_code` text DEFAULT NULL,
  `date_of_reset_request` timestamp NULL DEFAULT NULL,
  `login_token` text DEFAULT NULL,
  `date_of_last_login` timestamp NULL DEFAULT NULL,
  `start_time` text NOT NULL,
  `status_game_penyisihan` int(11) NOT NULL,
  `Poin` int(11) NOT NULL,
  `leader_name` text NOT NULL,
  `informasi_dari` varchar(40) NOT NULL,
  `informasi_text` varchar(150) NOT NULL,
  `pre_elim_token` varchar(34) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `2022_main_teams`
--

INSERT INTO `2022_main_teams` (`id`, `name`, `password`, `leader_id`, `participant_count`, `school`, `status`, `verificator`, `date_of_verification`, `payment_filepath`, `date_of_registration`, `reset_password_code`, `date_of_reset_request`, `login_token`, `date_of_last_login`, `start_time`, `status_game_penyisihan`, `Poin`, `leader_name`, `informasi_dari`, `informasi_text`, `pre_elim_token`) VALUES
(23, 'Blank', '$2y$10$wIXk/.ftrLzPdXVt8mv3IOAzj8/qW6pckSamh9sf6zfYnTngUFgS.', 52, 2, 'Cahaya Bangsa Classical School', 1, 'Amelia Syatriadi', '2022-05-10 19:02:23', '82b566f6a69a3d37b5c0.png', '2022-04-28 14:14:10', NULL, NULL, '062123e7e6fc9ba9d3827ee37a1f474b', '2022-05-28 16:00:42', '', 0, 0, 'Nicholas Vincent Khoe Lie', '', '', ''),
(25, 'SMASA BERKIBAR', '$2y$10$J9atVNqRMaRUqrZwbCDiJO6JNjluDy62Qkx0bEY/xBRI3Z6SpcjU.', 56, 3, 'SMA Negeri 1 Situbondo', 1, 'Amelia Syatriadi', '2022-05-16 22:48:49', 'f795d3e059266ee6ca50.jpeg', '2022-05-13 14:58:36', NULL, NULL, '74d3c8ecd1e48a1910b8b64ff98cc323', '2022-06-24 14:31:36', '', 0, 0, 'Muhammad Lanang Zalkifla Harits', '[0]', '', ''),
(28, 'Amin Hogi', '$2y$10$5RGYGhRS7ZiQF3hR17sRm.d6R066aA.tb0Tyj/SWhoP5oPSHk.KtG', 59, 3, 'SMA KRISTEN PETRA 1', 1, 'Amelia Syatriadi', '2022-05-25 23:29:16', '1b35297a27cb031934e8.jpeg', '2022-05-24 06:33:01', NULL, NULL, NULL, NULL, '', 0, 0, 'Odelia Liem', '[1]', '', ''),
(31, 'Sumber Makmur', '$2y$10$qA5q.vw/H/cSWHTe0fI5kO1r1/T1VmqsV5XKR98jlYOjC.gBuHrVC', 62, 3, 'SMA Strada St. Thomas Aquino', 1, 'Amelia Syatriadi', '2022-06-01 01:05:17', 'ad1a6ea9e95c0c11ded8.jpg', '2022-05-31 15:48:32', NULL, NULL, 'b98c2582a1b36f350f02a8a0f9d8f6d2', '2022-06-08 15:26:42', '', 0, 0, 'Ignatius Loyola Revalino Chandra Kurniawan', '[1]', '', ''),
(32, 'gatau', '$2y$10$N1l1Fumrci/qOPh9NXYAI.HgZAYLRy1I8xvlLthzIYbGZ04QP326a', 65, 3, 'SMA Kristen Gloria 1', 1, 'Amelia Syatriadi', '2022-06-02 09:26:43', '027675a6f0d7b2a4b582.jpg', '2022-06-02 00:56:15', NULL, NULL, NULL, NULL, '', 0, 0, 'Janice Angela Tee', '[3]', '', ''),
(33, 'Tiga Serangkai', '$2y$10$IJGnT9eLIiYk2rAl8abgfOjiOmyAa7IkjOZgSwi.bXChP0uTHy4T.', 68, 3, 'SMA JAC Surabaya', 1, 'Amelia Syatriadi', '2022-06-05 01:29:23', 'b24bfbdf92f9b8574846.jpg', '2022-06-03 12:21:09', NULL, NULL, 'fa0609221d0ebbebad544922a4bcc106', '2022-06-07 00:26:44', '', 0, 0, 'Michael Stanley Hartono', '[3]', '', ''),
(34, 'Opulent Splendor Warrior', '$2y$10$D7A/az9NWuEvaQA5v5K4Y.hS2aG7MfGNg8EqsVFH0oQYvpYP60Pyq', 71, 3, 'SMAK Petra 5 Surabaya', 1, 'Amelia Syatriadi', '2022-06-11 11:32:40', 'aedb57ed9654516761c0.jpeg', '2022-06-05 10:57:03', NULL, NULL, '0bb57b551bfd069413dd5fe7501f96b0', '2022-06-16 10:30:44', '', 0, 0, 'Jonathan Velim', '[0,3]', '', ''),
(37, 'EXCALIFUR', '$2y$10$Fx6I2PtRNDXNNeOQjbb3V.ivgEpo315RQ561c4J18FSgJWb7JKjMy', 79, 3, 'SMA Kristen Petra 1', 1, 'Amelia Syatriadi', '2022-06-08 02:13:45', '58e830fecbd2f8c2eaf7.jpg', '2022-06-06 07:08:05', NULL, NULL, '62992540af77505c731eb5b5e72d1504', '2022-06-10 06:19:01', '', 0, 0, 'Christopher Octave Sinjaya', '[1]', '', ''),
(38, 'Fratorian', '$2y$10$4lgs.pInaP3CCsuFaUDaqOZAYPdD/klc0QcY5U1u1AWrs9Qy578lC', 82, 3, 'SMAK Frateran Surabaya', 1, 'Amelia Syatriadi', '2022-06-09 23:18:08', '4f5e374a0f9d7c81aceb.jpeg', '2022-06-09 12:44:57', NULL, NULL, NULL, NULL, '', 0, 0, 'Bryan Valencio', '[1]', '', ''),
(39, 'AKM', '$2y$10$L4TgingTr/dcHdxVTjuh2..jGw3MVL4TJEJwlZuhpvCe0JzrSq0Oa', 85, 3, 'SMAK St. Louis 1', 1, 'Amelia Syatriadi', '2022-06-11 03:22:17', '736268ddec845416463f.jpg', '2022-06-10 15:48:59', NULL, NULL, NULL, NULL, '', 0, 0, 'Louise Vennesia Aristha Prajugo', '[1,3]', '', ''),
(40, 'Artemis', '$2y$10$GGaaCKPcv4F2vCWr0vpmZudl2Fn2myA4afSD2Gz0Yti4SRIp2Crk6', 88, 3, 'Dr. Wahidin Sudirohusodo', 1, 'Amelia Syatriadi', '2022-06-11 20:30:00', '37df978864f6e2ad6da7.jpg', '2022-06-11 08:27:48', NULL, NULL, '08cd4c7298507606e3ac90b88e9144d9', '2022-06-13 13:37:28', '', 0, 0, 'Nicolas Lee', '[1,3]', '', ''),
(41, 'The Big Show', '$2y$10$LqAvy0xGpR.D4HXuXf62OOxBkxGbqdeD8U9ZZShBjxGLvfdfFhUES', 91, 3, 'SMA PL Van Lith', 1, 'Amelia Syatriadi', '2022-06-13 17:13:04', 'c62885d6d92e97ae7cb6.jpeg', '2022-06-12 07:35:24', NULL, NULL, '0d6112faf5fbb988a4f8edf45209bd2c', '2022-06-17 22:42:16', '', 0, 0, 'Benedictus Giri Cahya Saputra', '[0]', '', ''),
(42, 'Fictoary', '$2y$10$f0F3g1KYjUNi8hv4Gi6Q..amioz0Nds4ZhJ2D748NtH3P2999jyKS', 94, 3, 'SMAN 2 BONDOWOSO', 1, 'Amelia Syatriadi', '2022-06-13 17:07:37', 'c91e4c86af4ea993f85f.jpeg', '2022-06-13 04:21:26', NULL, NULL, 'f8dc0c2fc4375f4dd7371e6203c88ecf', '2022-06-14 03:47:47', '', 0, 0, 'Ainun Jariyah', '[0,3]', '', ''),
(44, 'BOONK', '$2y$10$e5WkWdUH6lwL/Jk7j7nkOu7B9XZHAoQHIO/jnTyzmWYTfAJjXIHqa', 99, 3, 'SMAS Pangudi Luhur Van Lith', 0, NULL, NULL, 'fc3760658065679904ca.jpeg', '2022-06-16 03:45:12', NULL, NULL, NULL, NULL, '', 0, 0, 'Gabriel Beato Djoananda', '[0]', '', ''),
(45, 'KSbestie', '$2y$10$eiQ.IqOYWL6ly7xly6VbZOOnkM9vm6lyYGfFq8FymIbI4b4MrDz12', 102, 3, 'SMAK St. Louis 1 Surabaya', 1, 'Amelia Syatriadi', '2022-06-22 21:52:30', 'KSBestie.jpg', '2022-06-18 15:09:50', NULL, NULL, NULL, NULL, '', 0, 0, 'Gabriella Clairine', '[1]', '', ''),
(46, 'Dalmations', '$2y$10$c/wrbHy0S4Ulmu5.EJyGIeWLuOZe0XoyS2BMg3xQyFRwCEae/w6.6', 105, 3, 'SMA Kr. Petra 5 Surabaya', 1, 'Amelia Syatriadi', '2022-06-23 01:00:09', '73606538031372b5ce22.jpg', '2022-06-21 13:42:23', NULL, NULL, NULL, NULL, '', 0, 0, 'Kezia Eleanore Justina', '[0]', '', ''),
(48, 'Trethankaras', '$2y$10$JMLd5anfKkdzIOJt5z/8Fes44mRAA.lu3Wn2EF5SW3zjt90VcMdp.', 111, 3, 'SMA Regina Pacis Bogor', 1, 'Amelia Syatriadi', '2022-06-28 21:05:05', 'c7080a3adaee4417d152.jpg', '2022-06-25 12:47:15', NULL, NULL, NULL, NULL, '', 0, 0, 'Samuel Nehas Akerina', '[0]', '', ''),
(49, 'Comozzo', '$2y$10$Scsn60A5BFDDSqaDGjTL4uhfsoQn32XISGuQswoVGLEj8g98nOmoq', 114, 3, 'MAN 2 Pekanbaru', 1, 'Amelia Syatriadi', '2022-06-28 21:05:07', '29a914a4840556500eee.jpeg', '2022-06-27 15:12:19', NULL, NULL, NULL, NULL, '', 0, 0, 'Muhammad Naufal Muzaki', '[2]', '', ''),
(50, 'dummy', '$2y$10$lRvDTLx3GoUglIDUqguEjOswh5HanaQsPwsfu/onkc0ywtamAyrDK', 69, 1, 'Sinlui', 1, 'Pak Alex', '2022-06-28 22:31:37', 'FREE', '2022-06-28 15:31:37', NULL, NULL, NULL, NULL, '', 1, 0, 'Sergius', '', '', '');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `2022_main_teams`
--
ALTER TABLE `2022_main_teams`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `2022_main_teams`
--
ALTER TABLE `2022_main_teams`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=52;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
