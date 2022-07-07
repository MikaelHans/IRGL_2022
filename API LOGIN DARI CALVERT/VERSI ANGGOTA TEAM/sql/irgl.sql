-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 28, 2022 at 07:11 PM
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
-- Table structure for table `2022_main_participants`
--

CREATE TABLE `2022_main_participants` (
  `id` int(11) NOT NULL,
  `team_id` int(11) NOT NULL,
  `name` text NOT NULL,
  `date_of_birth` datetime NOT NULL,
  `city_of_birth` text NOT NULL,
  `school_grade` int(11) NOT NULL,
  `address` text NOT NULL,
  `line_id` text NOT NULL,
  `email` text NOT NULL,
  `phone_number` text NOT NULL,
  `studentid_filepath` text NOT NULL,
  `date_of_registration` timestamp NOT NULL DEFAULT current_timestamp(),
  `allergy` mediumtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `2022_main_participants`
--

INSERT INTO `2022_main_participants` (`id`, `team_id`, `name`, `date_of_birth`, `city_of_birth`, `school_grade`, `address`, `line_id`, `email`, `phone_number`, `studentid_filepath`, `date_of_registration`, `allergy`) VALUES
(52, 23, 'Nicholas Vincent Khoe Lie', '2006-05-24 00:00:00', 'Plano Texas', 10, 'Jl. Pitajaya no.25, Padalarang', 'unowned', 'nicholas.vincent.2024@student.cahayabangsa.org', '+62 895371951151', 'fb635e609766b545e3f0.jpg', '2022-04-28 14:14:10', 'Peanuts'),
(53, 23, 'Scarlet Raine Surya', '2006-10-14 00:00:00', 'Bandung', 10, 'Jl. Yakin no 8, Bandung', 'unowned', 'scarlett.raine.2024@student.cahayabangsa.org', '+62 82281819009', '6dac9df92e627a4779b2.png', '2022-04-28 14:14:10', ''),
(56, 25, 'Muhammad Lanang Zalkifla Harits', '2005-11-13 00:00:00', 'Situbondo', 10, 'Jl. Semeru No. 59 Mimbaan Panji Situbondo Jawa Timur', 'lanangz_', 'muh.lanangzalkifla@gmail.com', '081907337904', 'c535388d39b69a3befaa.jpg', '2022-05-13 14:58:36', ''),
(57, 25, 'Mirabilis Sera Alym Maqomy', '2005-08-18 00:00:00', 'Situbondo', 10, 'KP. Barat Kebun RT. 003 RW. 002 Jl. Raya Wringin Anom, Wringin Anom Panarkan SItubondo Jawa Timur', 'ioforia15', 'varezelviros@gmail.com', '089516298553', 'f0184ddbc9b03b86be82.pdf', '2022-05-13 14:58:36', ''),
(58, 25, 'Kamelia Rizkiana', '2006-11-24 00:00:00', 'Situbondo', 10, 'Desa Kendit Kec. Kendit Kab. Situbondo Jawa Timur', 'rzkrzk0050', 'rizkianakamelia@gmail.com', '089610351031', 'b499f4acd3a518e9e6d7.pdf', '2022-05-13 14:58:36', ''),
(59, 28, 'Odelia Liem', '2005-09-30 00:00:00', 'Surabaya', 11, 'Merapi no 7, sawahan, surabaya, jawa timur', 'odelialiem', 'odelialie16@gmail.com', '081249266337', 'e80459ae4e67a5f3a39c.jpeg', '2022-05-24 06:33:01', ''),
(60, 28, 'Bradley Widjaja', '2005-05-26 00:00:00', 'Surabaya', 11, 'Pakuwon Indah the mansion pf5-37, surabaya, jawa timur', 'bradley_0123', 'bradleywidjaja507@gmail.com', '+62 821-7182-8877', '2d937e2c1e2df0e206ca.jpeg', '2022-05-24 06:33:01', ''),
(61, 28, 'Nicholas Nathan Surianto', '2005-04-28 00:00:00', 'Surabaya', 11, 'Darmo harapan indah 4-uu6, surabaya, jawa timur', '085931080586', 'nicholas.shaw05@gmail.com', '085931080586', '7c0ae3b3cc455e4440dc.jpeg', '2022-05-24 06:33:01', ''),
(62, 31, 'Ignatius Loyola Revalino Chandra Kurniawan', '2005-02-10 00:00:00', 'Klaten', 11, 'Villa Taman Cibodas, M4-27, RT002-RW011, Periuk, Kota Tangerang, Banten', 'revalbro', 'revalinochandra10@gmail.com', '081315107583', 'f42b0eb5eca4bd162cb1.jpg', '2022-05-31 15:48:32', ''),
(63, 31, 'Gregorius Setiadharma', '2005-09-20 00:00:00', 'Kota Tangerang', 11, 'Perum Batuceper Permai Blok M no 12, RT05-RW09, Batuceper, Kota Tangerang, Banten', 'gregoriussd', 'gregoriuss.d2005@gmail.com', '0819881758', '4559b88fa6c8086dabd8.jpg', '2022-05-31 15:48:32', ''),
(64, 31, 'Andreas Damar Bagus Kusumo', '2005-04-16 00:00:00', 'Kota Tangerang', 11, 'Jl. Liga Utara Blok B2 No.8, Victoria Park, Kota Tangerang, Banten', 'themazerunnerseries', 'damar.damar1609@gmail.com', '08111642005', '92820983ce7e0184ee57.jpg', '2022-05-31 15:48:32', ''),
(65, 32, 'Janice Angela Tee', '2005-08-20 00:00:00', 'Surabaya', 12, 'Jl. Puncak Permai Utara no.16, Surabaya, Jawa Timur', 'janiceangelatee123', 'janiceangelatee@gmail.com', '081259560675', '5c7cb91e2dfac3d6965f.jpg', '2022-06-02 00:56:15', ''),
(66, 32, 'Bennett Sebastian Tandra', '2005-06-24 00:00:00', 'Surabaya', 12, 'Alam Galaxy Cluster Ravenala Gallery D3-23, Surabaya, Jawa Timur', 'bennett_tan', 'bennettsebastiantandra@gmail.com', '085648586967', '4fd798afa29c57decd15.jpg', '2022-06-02 00:56:15', ''),
(67, 32, 'Vincent Vinardi Hartono', '2004-12-18 00:00:00', 'Braunschweig', 12, 'G10 no-32, Grand Palais, Wisata Bukit Mas, Surabaya, Jawa Timur', 'novitasarihartono', 'vincentvinardih@yahoo.com', '082140880773', 'e7c16be573c38fd710f6.jpg', '2022-06-02 00:56:15', ''),
(68, 33, 'Michael Stanley Hartono', '2005-12-13 00:00:00', 'Surabaya', 11, 'Taman Pondok Indah MX-7 Wiyung, Surabaya, Jawa Timur', 'mkstnl', 'michaelxstanleyy@gmail.com', '081252975252', 'f00f229734725453c719.jpeg', '2022-06-03 12:21:09', ''),
(69, 33, 'Jennifer Tanjaya', '2005-05-29 00:00:00', 'Surabaya', 11, 'Alam Hijau E11-8 Citraland, Surabaya, Jawa Timur', '29112000._.deardream', 'jennifertanjaya29@gmail.com', '08113452855', '62cf6fd1294c85bb72f3.jpeg', '2022-06-03 12:21:09', 'milk and dairy products in general'),
(70, 33, 'Delicia Erin Win', '2005-10-28 00:00:00', 'Surabaya', 11, 'Graha Famili Blok O No. 285, Surabaya, Jawa Timur', 'deliciaew28', 'deliciaerinwin@gmail.com', '081259153387', '4733dbac8544acfbea59.jpeg', '2022-06-03 12:21:09', ''),
(71, 34, 'Jonathan Velim', '2004-12-10 00:00:00', 'Surabaya', 11, 'Jalan Kemlaten 12 No. 8, Surabaya', 'gotterhaltebeschutze', 'jonathanvelim@gmail.com', '081717538588', 'opulentsplendorwarrior.jpg', '2022-06-05 10:57:03', ''),
(72, 34, 'Juan Nathanael Effendy', '2005-04-13 00:00:00', 'Surabaya', 11, 'Perumahan Delta Sari Baru, Delta Kencana, No 63, Waru', 'juannathanaele', 'juannathanael2005@gmail.com', '08113499095', 'opulentsplendorwarrior.jpg', '2022-06-05 10:57:03', ''),
(73, 34, 'Michael Kurniawan', '2005-05-21 00:00:00', 'Bandung', 11, 'Taman Pondok Jati No. 11 Geluran, Kec. Taman, Kabupaten Sidoarjo, Jawa Timur', 'dawienner', 'albertjuga2013@gmail.com', '087841170037', 'opulentsplendorwarrior.jpg', '2022-06-05 10:57:03', ''),
(79, 37, 'Christopher Octave Sinjaya', '2005-05-08 00:00:00', 'Surabaya', 11, 'Surabaya, Indonesia, Jawa', 'christopherosgamer', 'christopheroctavesinjaya@gmail.com', '085791949799', 'c0350ba045474ac0b311.pdf', '2022-06-06 07:08:05', ''),
(80, 37, 'Jonathan Wongso', '2005-09-08 00:00:00', 'Surabaya', 11, 'Surabaya, Indonesia, Jawa', 'team_firest', 'wongso_jonathan@yahooc.com', '081222021588', 'a65931275b7c77f5e0f4.pdf', '2022-06-06 07:08:05', ''),
(81, 37, 'Kevin Wijaya', '2005-03-13 00:00:00', 'Surabaya', 11, 'Surabaya, Indonesia, Jawa', 'kev96744', '2017071746@student.pppkpetra.sch.id', '082245666381', 'a89ca9a34be835943fa8.pdf', '2022-06-06 07:08:05', ''),
(82, 38, 'Bryan Valencio', '2005-05-12 00:00:00', 'Surabaya', 11, 'Lebak arum 2 no 50, Kenjeran', 'bryavalenciohokgiono', 'bryanvalencio@gmail.com', '089697080037', 'aa84ab1fd8017e38876a.pdf', '2022-06-09 12:44:57', ''),
(83, 38, 'Elisabeth Cheryl Xaviera', '2006-01-12 00:00:00', 'Kediri', 11, 'Dapuan baru no 21-23, krembangan utara', 'cherylxaviera', 'elisabethcherylxaviera@gmail.com', '081332277310', '4abeb2c19921e4396fce.pdf', '2022-06-09 12:44:57', ''),
(84, 38, 'Jenica Wibisono', '2005-11-16 00:00:00', 'Surabaya', 11, 'Gatotan 48', 'margaretajenica', 'jenicawibisono@gmail.com', '081252648782', '9f14ce0b70886a43b160.jpeg', '2022-06-09 12:44:57', ''),
(85, 39, 'Louise Vennesia Aristha Prajugo', '2005-10-30 00:00:00', 'Surabaya', 12, 'Jl. Pucang Jajar no.33, Surabaya, Jawa Timur', 'aristhaprajugo', 'aristha2005@gmail.com', '089643202320', 'af315247218bb23c4de8.jpg', '2022-06-10 15:48:59', ''),
(86, 39, 'Liliana Djaja Witama', '2005-02-01 00:00:00', 'Surabaya', 12, 'Jl. Lebak Indah Mas 2 Kavling 16, Surabaya, Jawa Timur', 'lilianadj05', 'lilianadj05@gmail.com', '081239185511', '02b5fb5575d469fd5945.jpg', '2022-06-10 15:48:59', ''),
(87, 39, 'Aloysia Jennifer Harijadi', '2005-06-25 00:00:00', 'Surabaya', 12, 'Jl. Semalang Indah gang 5 no.7, Surabaya, Jawa Timur', '_a.jennifer', 'aloysiajennifer@gmail.com', '082232425587', 'cb722bcc28044e2adac9.jpg', '2022-06-10 15:48:59', ''),
(88, 40, 'Nicolas Lee', '2005-11-09 00:00:00', 'Medan', 11, 'Jl. Marelan Raya Pasar II, Medan Marelan, Medan, Sumatera Utara', 'nicolaslee321', 'nicolaslee2311@gmail.com', '085323320496', '0c458628cea82e70e64c.jpg', '2022-06-11 08:27:48', ''),
(89, 40, 'Wilson Jacklim', '2005-02-23 00:00:00', 'Panipahan', 11, 'Jl. Platina Raya, Komplek Taman Platina Baru no 15, Titipapan, Medan, Sumatera Utara', '230205wj', 'wils2160@gmail.com', '081370666856', '957a6f9d0f4497cd45ef.jpg', '2022-06-11 08:27:48', ''),
(90, 40, 'Stevenson Timothy', '2005-11-06 00:00:00', 'Medan', 11, 'Jalan Jala IV Paya Pasir Lingk. 03 No. 41 E, Medan Marelan, Medan, Sumatera Utara', 'son_0623', 'stevensontimothy06@gmail.com', '089515051061', 'cf27eb6461698715a5fb.jpg', '2022-06-11 08:27:48', ''),
(91, 41, 'Benedictus Giri Cahya Saputra', '2005-06-17 00:00:00', 'Jakarta', 12, 'Jl. Merah Delima Raya blok CA1 no. 9 Mustika Karang Satria, Tambun Utara, Bekasi', 'benedictus.saputra', 'benedictussaputra2@gmail.com', '085711329676', '21ce56a0740249316ba3.jpeg', '2022-06-12 07:35:24', ''),
(92, 41, 'Gregorius Aristo Febrian', '2005-02-10 00:00:00', 'Timika', 12, 'JL. Budi Utomo, RT.010, RW.000, Kel. Pasar Sentral, Kec. Mimika Baru, Kab. Mimika', 'gregorius_aristo', 'gregoriusaristo@gmail.com', '081268055628', '53c235b73f4380db634b.jpg', '2022-06-12 07:35:24', ''),
(93, 41, 'Samuel Adibrata Harliman', '2005-03-04 00:00:00', 'Jakarta', 12, 'Perum. Legenda Wisata, Zona Mozart, Blok G3 no.1, Kel. Wanaherang, Kec. Gunung Putri, Kab. Bogor', 'samueladibratah', 'samadibrata@gmail.com', '085880636900', '0a23a56f04041670f57a.jpg', '2022-06-12 07:35:24', ''),
(94, 42, 'Ainun Jariyah', '2005-06-13 00:00:00', 'Bondowoso', 10, 'Jl. KH Agus Salim, Blindungan Bondowoso', 'ainunjariyah.d', 'jariyaha94@gmail.com', '0857-4532-4633', '47714b02c40b968133c9.jpg', '2022-06-13 04:21:26', ''),
(95, 42, 'Octavia Eka Nurtriana Ningrum', '2005-10-31 00:00:00', 'Bondowoso', 10, 'PONCOGATI, Kec Curahdami Kab Bondowoso', 'jiyuu.', 'octaviaenn@gmail.com', '0889-9627-0785', 'ef410178ce986dca1790.jpg', '2022-06-13 04:21:26', ''),
(96, 42, 'Raihan Ahmad Hafidz Gymnastiar', '2005-09-14 00:00:00', 'Bondowoso', 10, 'Perum Kembang Permai KK-18, Kec Bondowoso, Kab Bondowoso', '010p010pp10', 'rai85han@gmail.com', '0851-5668-5725', 'd19268dcf4fdb395db38.jpg', '2022-06-13 04:21:26', ''),
(99, 44, 'Gabriel Beato Djoananda', '2005-02-08 00:00:00', 'Semarang', 12, 'Jl. Kartini No. 1 Muntilan 56411', 'gbd825', 'gabrielbedjo@gmail.com', '085156781249', '55ae3829ee9ec90779a1.jpg', '2022-06-16 03:45:12', ''),
(100, 44, 'Elvin Terriano Hadi', '2006-12-02 00:00:00', 'Tarakan', 11, 'Jl. Kartini No. 1 Muntilan 56411', 'elvin-terriano-hadi', 'elvinhadi@gmail.com', '085249188688', '124c9eabc71f75bf25b3.pdf', '2022-06-16 03:45:12', ''),
(101, 44, 'Rachel Sunarko', '2005-02-11 00:00:00', 'Bandung', 12, 'Jl. Kartini No. 1 Muntilan 56411', 'awesome4r', 'rachelsunarko7@gmail.com', '08112192046', '1b7901e5d15727d9b5f3.jpeg', '2022-06-16 03:45:12', ''),
(102, 45, 'Gabriella Clairine', '2005-11-18 00:00:00', 'Surabaya', 12, 'Manyar Tirtoasri VIII - 28', 'gabriellac2005', 'gabriella.clairine2005@gmail.com', '082140829100', '87b7ef7e8819f4022972.jpg', '2022-06-18 15:09:50', ''),
(103, 45, 'Goodwill Bryan Lion', '2005-07-28 00:00:00', 'Surabaya', 12, 'Graha Famili Blok B 63', 'goodwilllion', 'goodwilllion28@gmail.com', '089505081178', '2c0c9776db9d7dba5192.jpg', '2022-06-18 15:09:50', ''),
(104, 45, 'David Bakti Lodianto', '2005-10-01 00:00:00', 'Lumajang', 12, 'Baratajaya XX - 87', 'davidbakti_lodianto', 'david.lodianto@gmail.com', '081335509301', '7362d1cf26427bdfa346.jpg', '2022-06-18 15:09:50', ''),
(105, 46, 'Kezia Eleanore Justina', '2005-11-18 00:00:00', 'Surabaya', 10, 'Jl. Rungkut Asri Barat 14 No. 22 Surabaya, Jawa Timur', 'keziaeleanorej', 'kezia.eleanore@gmail.com', '089636337072', '05d891e77ef038bc729e.jpg', '2022-06-21 13:42:23', ''),
(106, 46, 'Stefany Eliana Wijaya', '2005-10-01 00:00:00', 'Surabaya', 10, 'Kutisari Indah Utara 5 No. 26, Surabaya, Jawa Timur', 'stefany_elwi', 'stefanywijaya0125@gmail.com', '082221117110', 'ee55a94f5f2dd43ffe6f.pdf', '2022-06-21 13:42:23', ''),
(107, 46, 'Catherine Natalie', '2006-12-20 00:00:00', 'Surabaya', 10, 'Jl. Taman Pondok Jati Ai No. 4, Sidoarjo, Jawa Timur', 'catherinen06', 'catherinenatalie21@gmail.com', '088989013739', 'd7a39d197cf480c875b4.pdf', '2022-06-21 13:42:23', ''),
(111, 48, 'Samuel Nehas Akerina', '2005-12-25 00:00:00', 'Bogor', 12, 'Jl. Otista Gg. Kebon Kelapa No. 29. RT. 3 RW. 1 Baranangsiang, Bogor Timur, Bogor', 'junisamuel', 'samuelnakerina@gmail.com', '081291568150', 'b0c9b2f8d71522fa7636.pdf', '2022-06-25 12:47:15', ''),
(112, 48, 'Louis Alexander Pekandi', '2005-03-28 00:00:00', 'Bogor', 12, 'Jl. Anggrek 1 no. 10, RT. 3 RW. 1 Pakuan 1, kota Bogor', 'bernardp87linechat', 'louispekandi@gmail.com', '089637840749', '5318420f03aac4f2db06.jpg', '2022-06-25 12:47:15', ''),
(113, 48, 'Kevin Pratama', '2005-06-24 00:00:00', 'Bogor', 12, 'Jl. Raya Puncak Ruko Permata No. 2, Kp. Pasanggrahan RT. 1 RW. 5 Cisarua, Bogor', 'kevinpra8888', 'kevinpratama12341@gmail.com', '087737330999', '930afb696409b114d2a0.jpg', '2022-06-25 12:47:15', ''),
(114, 49, 'Muhammad Naufal Muzaki', '2005-11-08 00:00:00', 'Pekanbaru', 10, 'Pekanbaru, Riau', 'mnaufalmuzaki', 'm.naufal.muzaki811@gmail.com', '089630144790', '876e482071279f1e55b2.png', '2022-06-27 15:12:19', ''),
(115, 49, 'Dhimas Putra Sulistio', '2005-12-13 00:00:00', 'Pekanbaru', 10, 'Pekanbaru, Riau', 'dhimassulistio', 'dhimassulistio2@gmail.com', '082180730895', '4e49a97f13c740dedb75.jpeg', '2022-06-27 15:12:19', ''),
(116, 49, 'Hannan Afif Darmawan', '2006-03-11 00:00:00', 'Jakarta', 10, 'Pekanbaru, Riau', 'helloinformatics', 'hannanafifd@gmail.com', '081364500625', '0703ec0a4be6bf79726e.pdf', '2022-06-27 15:12:19', ''),
(117, 50, 'Dummy A', '2022-06-28 23:49:31', 'A', 10, 'A', 'A', 'a@irgl.com', 'A', 'A', '2022-06-28 16:49:31', 'A'),
(118, 50, 'Dummy B', '2022-06-28 23:49:31', 'B', 11, 'B', 'B', 'b@irgl.com', 'B', 'B', '2022-06-28 16:50:56', 'B'),
(119, 50, 'Dummy C', '2022-06-28 23:51:02', 'C', 12, 'C', 'C', 'c@irgl.com', 'C', 'C', '2022-06-28 16:51:27', 'C');

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
-- Indexes for table `2022_main_participants`
--
ALTER TABLE `2022_main_participants`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `2022_main_teams`
--
ALTER TABLE `2022_main_teams`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `2022_semifinal_teams`
--
ALTER TABLE `2022_semifinal_teams`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `2022_main_participants`
--
ALTER TABLE `2022_main_participants`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=120;

--
-- AUTO_INCREMENT for table `2022_main_teams`
--
ALTER TABLE `2022_main_teams`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=52;

--
-- AUTO_INCREMENT for table `2022_semifinal_teams`
--
ALTER TABLE `2022_semifinal_teams`
  MODIFY `id` smallint(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
