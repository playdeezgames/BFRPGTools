-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               11.4.2-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for basic_fantasy_rpg
CREATE DATABASE IF NOT EXISTS `basic_fantasy_rpg` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_general_ci */;
USE `basic_fantasy_rpg`;

-- Dumping structure for table basic_fantasy_rpg.abilities
CREATE TABLE IF NOT EXISTS `abilities` (
  `ability_id` int(11) NOT NULL AUTO_INCREMENT,
  `ability_name` varchar(50) NOT NULL,
  `ability_abbreviation` varchar(3) NOT NULL,
  `affects_hit_dice` tinyint(1) NOT NULL,
  PRIMARY KEY (`ability_id`),
  UNIQUE KEY `ability_name` (`ability_name`),
  UNIQUE KEY `ability_abbreviation` (`ability_abbreviation`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.abilities: ~6 rows (approximately)
INSERT INTO `abilities` (`ability_id`, `ability_name`, `ability_abbreviation`, `affects_hit_dice`) VALUES
	(1, 'Strength', 'STR', 0),
	(2, 'Intelligence', 'INT', 0),
	(3, 'Wisdom', 'WIS', 0),
	(4, 'Dexterity', 'DEX', 0),
	(5, 'Constitution', 'CON', 1),
	(6, 'Charisma', 'CHA', 0);

-- Dumping structure for table basic_fantasy_rpg.characters
CREATE TABLE IF NOT EXISTS `characters` (
  `character_id` int(11) NOT NULL AUTO_INCREMENT,
  `character_name` varchar(50) NOT NULL,
  `player_id` int(11) NOT NULL,
  `race_class_id` int(11) NOT NULL,
  `experience_points` int(11) NOT NULL,
  `character_description` varchar(500) NOT NULL,
  `money` decimal(20,2) NOT NULL,
  PRIMARY KEY (`character_id`),
  UNIQUE KEY `character_name_player_id` (`character_name`,`player_id`),
  KEY `FK_characters_players` (`player_id`),
  KEY `FK_characters_race_classes` (`race_class_id`),
  CONSTRAINT `FK_characters_players` FOREIGN KEY (`player_id`) REFERENCES `players` (`player_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_characters_race_classes` FOREIGN KEY (`race_class_id`) REFERENCES `race_classes` (`race_class_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.characters: ~1 rows (approximately)

-- Dumping structure for table basic_fantasy_rpg.character_abilities
CREATE TABLE IF NOT EXISTS `character_abilities` (
  `character_ability_id` int(11) NOT NULL AUTO_INCREMENT,
  `character_id` int(11) NOT NULL,
  `ability_id` int(11) NOT NULL,
  `ability_score` int(11) NOT NULL,
  PRIMARY KEY (`character_ability_id`),
  UNIQUE KEY `character_id_ability_id` (`character_id`,`ability_id`),
  KEY `FK_character_abilities_abilities` (`ability_id`),
  CONSTRAINT `FK_character_abilities_abilities` FOREIGN KEY (`ability_id`) REFERENCES `abilities` (`ability_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_character_abilities_characters` FOREIGN KEY (`character_id`) REFERENCES `characters` (`character_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=259 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.character_abilities: ~6 rows (approximately)

-- Dumping structure for view basic_fantasy_rpg.character_ability_details
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `character_ability_details` (
	`character_id` INT(11) NOT NULL,
	`character_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_score` INT(11) NOT NULL,
	`ability_id` INT(11) NOT NULL,
	`ability_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_abbreviation` VARCHAR(3) NOT NULL COLLATE 'latin1_general_ci',
	`ability_modifier` INT(11) NULL,
	`affects_hit_dice` TINYINT(1) NOT NULL
) ENGINE=MyISAM;

-- Dumping structure for view basic_fantasy_rpg.character_details
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `character_details` (
	`character_id` INT(11) NOT NULL,
	`character_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`race_id` INT(11) NOT NULL,
	`race_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`player_id` INT(11) NOT NULL,
	`player_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`class_id` INT(11) NOT NULL,
	`class_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`experience_points` INT(11) NOT NULL,
	`level` INT(11) NOT NULL,
	`hit_die` INT(11) NOT NULL,
	`hit_points` DECIMAL(59,0) NULL,
	`character_description` VARCHAR(500) NOT NULL COLLATE 'latin1_general_ci',
	`attack_bonus` INT(11) NOT NULL,
	`money` DECIMAL(20,2) NOT NULL,
	`class_level_id` INT(11) NOT NULL
) ENGINE=MyISAM;

-- Dumping structure for table basic_fantasy_rpg.character_hit_dice
CREATE TABLE IF NOT EXISTS `character_hit_dice` (
  `character_hit_dice_id` int(11) NOT NULL AUTO_INCREMENT,
  `character_id` int(11) NOT NULL,
  `die` int(11) NOT NULL,
  `die_roll` int(11) NOT NULL,
  PRIMARY KEY (`character_hit_dice_id`),
  UNIQUE KEY `character_id_index` (`character_id`,`die`) USING BTREE,
  CONSTRAINT `FK__characters` FOREIGN KEY (`character_id`) REFERENCES `characters` (`character_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=217 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.character_hit_dice: ~9 rows (approximately)

-- Dumping structure for view basic_fantasy_rpg.character_hit_dice_details
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `character_hit_dice_details` (
	`character_hit_dice_id` INT(11) NOT NULL,
	`character_id` INT(11) NOT NULL,
	`die` INT(11) NOT NULL,
	`modified_die_roll` DECIMAL(36,0) NULL
) ENGINE=MyISAM;

-- Dumping structure for view basic_fantasy_rpg.character_saving_throw_details
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `character_saving_throw_details` (
	`character_id` INT(11) NOT NULL,
	`character_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`saving_throw` INT(11) NOT NULL,
	`saving_throw_id` INT(11) NOT NULL,
	`saving_throw_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`saving_throw_bonus` INT(11) NULL
) ENGINE=MyISAM;

-- Dumping structure for table basic_fantasy_rpg.classes
CREATE TABLE IF NOT EXISTS `classes` (
  `class_id` int(11) NOT NULL AUTO_INCREMENT,
  `class_name` varchar(50) NOT NULL,
  `hit_die` int(11) NOT NULL,
  PRIMARY KEY (`class_id`),
  UNIQUE KEY `class_name` (`class_name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.classes: ~6 rows (approximately)
INSERT INTO `classes` (`class_id`, `class_name`, `hit_die`) VALUES
	(1, 'Cleric', 6),
	(2, 'Fighter', 8),
	(3, 'Magic-User', 4),
	(4, 'Thief', 4),
	(5, 'Fighter/Magic-User', 8),
	(6, 'Magic-User/Thief', 4);

-- Dumping structure for table basic_fantasy_rpg.class_ability_minimums
CREATE TABLE IF NOT EXISTS `class_ability_minimums` (
  `class_ability_minimum_id` int(11) NOT NULL AUTO_INCREMENT,
  `class_id` int(11) NOT NULL,
  `ability_id` int(11) NOT NULL,
  `minimum_score` int(11) NOT NULL,
  PRIMARY KEY (`class_ability_minimum_id`),
  UNIQUE KEY `class_id_ability_id` (`class_id`,`ability_id`),
  KEY `FK_class_ability_minimums_abilities` (`ability_id`),
  CONSTRAINT `FK_class_ability_minimums_abilities` FOREIGN KEY (`ability_id`) REFERENCES `abilities` (`ability_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_class_ability_minimums_classes` FOREIGN KEY (`class_id`) REFERENCES `classes` (`class_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.class_ability_minimums: ~8 rows (approximately)
INSERT INTO `class_ability_minimums` (`class_ability_minimum_id`, `class_id`, `ability_id`, `minimum_score`) VALUES
	(1, 1, 3, 9),
	(2, 2, 1, 9),
	(3, 5, 1, 9),
	(4, 5, 2, 9),
	(5, 3, 2, 9),
	(6, 6, 4, 9),
	(7, 6, 2, 9),
	(8, 4, 4, 9);

-- Dumping structure for view basic_fantasy_rpg.class_ability_ranges
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `class_ability_ranges` (
	`class_id` INT(11) NOT NULL,
	`class_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_id` INT(11) NOT NULL,
	`ability_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_abbreviation` VARCHAR(3) NOT NULL COLLATE 'latin1_general_ci',
	`minimum_score` INT(11) NULL,
	`maximum_score` INT(2) NOT NULL
) ENGINE=MyISAM;

-- Dumping structure for table basic_fantasy_rpg.class_levels
CREATE TABLE IF NOT EXISTS `class_levels` (
  `class_level_id` int(11) NOT NULL AUTO_INCREMENT,
  `class_id` int(11) NOT NULL,
  `level` int(11) NOT NULL,
  `hit_dice` int(11) NOT NULL,
  `hit_point_bonus` int(11) NOT NULL,
  `experience_points` int(11) NOT NULL,
  `attack_bonus` int(11) NOT NULL,
  PRIMARY KEY (`class_level_id`),
  UNIQUE KEY `class_id_level` (`class_id`,`level`),
  CONSTRAINT `FK_class_levels_classes` FOREIGN KEY (`class_id`) REFERENCES `classes` (`class_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=154 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.class_levels: ~120 rows (approximately)
INSERT INTO `class_levels` (`class_level_id`, `class_id`, `level`, `hit_dice`, `hit_point_bonus`, `experience_points`, `attack_bonus`) VALUES
	(1, 1, 1, 1, 0, 0, 1),
	(2, 1, 2, 2, 0, 1500, 1),
	(3, 1, 3, 3, 0, 3000, 2),
	(4, 1, 4, 4, 0, 6000, 2),
	(5, 1, 5, 5, 0, 12000, 3),
	(6, 1, 6, 6, 0, 24000, 3),
	(13, 1, 7, 7, 0, 48000, 4),
	(14, 1, 8, 8, 0, 90000, 4),
	(15, 1, 9, 9, 0, 180000, 5),
	(16, 1, 10, 9, 1, 270000, 5),
	(17, 1, 11, 9, 2, 360000, 5),
	(18, 1, 12, 9, 3, 450000, 6),
	(19, 1, 13, 9, 4, 540000, 6),
	(20, 1, 14, 9, 5, 630000, 6),
	(21, 1, 15, 9, 6, 720000, 7),
	(22, 1, 16, 9, 7, 810000, 7),
	(23, 1, 17, 9, 8, 900000, 7),
	(24, 1, 18, 9, 9, 990000, 8),
	(25, 1, 19, 9, 10, 1080000, 8),
	(26, 1, 20, 9, 11, 1170000, 8),
	(27, 2, 1, 1, 0, 0, 1),
	(28, 2, 2, 2, 0, 2000, 2),
	(33, 2, 3, 3, 0, 4000, 2),
	(34, 2, 4, 4, 0, 8000, 3),
	(35, 2, 5, 5, 0, 16000, 4),
	(36, 2, 6, 6, 0, 32000, 4),
	(37, 2, 7, 7, 0, 64000, 5),
	(38, 2, 8, 8, 0, 120000, 6),
	(39, 2, 9, 9, 0, 240000, 6),
	(40, 2, 10, 9, 2, 360000, 6),
	(41, 2, 11, 9, 4, 480000, 7),
	(42, 2, 12, 9, 6, 600000, 7),
	(43, 2, 13, 9, 8, 720000, 8),
	(44, 2, 14, 9, 10, 840000, 8),
	(45, 2, 15, 9, 12, 960000, 8),
	(46, 2, 16, 9, 14, 1080000, 9),
	(47, 2, 17, 9, 16, 1200000, 9),
	(48, 2, 18, 9, 18, 1320000, 10),
	(49, 2, 19, 9, 20, 1440000, 10),
	(50, 2, 20, 9, 22, 1560000, 10),
	(51, 3, 1, 1, 0, 0, 1),
	(52, 3, 2, 2, 0, 2500, 1),
	(53, 3, 3, 3, 0, 5000, 1),
	(54, 3, 4, 4, 0, 10000, 2),
	(55, 3, 5, 5, 0, 20000, 2),
	(56, 3, 6, 6, 0, 40000, 3),
	(57, 3, 7, 7, 0, 80000, 3),
	(58, 3, 8, 8, 0, 150000, 3),
	(59, 3, 9, 9, 0, 300000, 4),
	(60, 3, 10, 9, 1, 450000, 4),
	(61, 3, 11, 9, 2, 600000, 4),
	(62, 3, 12, 9, 3, 750000, 4),
	(63, 3, 13, 9, 4, 900000, 5),
	(64, 3, 14, 9, 5, 1050000, 5),
	(65, 3, 15, 9, 6, 1200000, 5),
	(66, 3, 16, 9, 7, 1350000, 6),
	(67, 3, 17, 9, 8, 1500000, 6),
	(68, 3, 18, 9, 9, 1650000, 6),
	(69, 3, 19, 9, 10, 1800000, 7),
	(70, 3, 20, 9, 11, 1950000, 7),
	(71, 4, 1, 1, 0, 0, 1),
	(72, 4, 2, 2, 0, 1250, 1),
	(73, 4, 3, 3, 0, 2500, 2),
	(74, 4, 4, 4, 0, 5000, 2),
	(75, 4, 5, 5, 0, 10000, 3),
	(76, 4, 6, 6, 0, 20000, 3),
	(77, 4, 7, 7, 0, 40000, 4),
	(78, 4, 8, 8, 0, 75000, 4),
	(79, 4, 9, 9, 0, 150000, 5),
	(80, 4, 10, 9, 2, 225000, 5),
	(81, 4, 11, 9, 4, 300000, 5),
	(82, 4, 12, 9, 6, 375000, 6),
	(83, 4, 13, 9, 8, 450000, 6),
	(84, 4, 14, 9, 10, 525000, 6),
	(85, 4, 15, 9, 12, 600000, 7),
	(86, 4, 16, 9, 14, 675000, 7),
	(87, 4, 17, 9, 16, 750000, 7),
	(88, 4, 18, 9, 18, 825000, 8),
	(89, 4, 19, 9, 20, 900000, 8),
	(90, 4, 20, 9, 22, 975000, 8),
	(102, 5, 1, 1, 0, 0, 1),
	(103, 5, 2, 2, 0, 4500, 2),
	(104, 5, 3, 3, 0, 9000, 2),
	(105, 5, 4, 4, 0, 18000, 3),
	(106, 5, 5, 5, 0, 36000, 4),
	(107, 5, 6, 6, 0, 72000, 4),
	(108, 5, 7, 7, 0, 144000, 5),
	(109, 5, 8, 8, 0, 270000, 6),
	(110, 5, 9, 9, 0, 540000, 6),
	(111, 5, 10, 9, 2, 810000, 6),
	(112, 5, 11, 9, 4, 1080000, 7),
	(113, 5, 12, 9, 6, 1350000, 7),
	(114, 5, 13, 9, 8, 1620000, 8),
	(115, 5, 14, 9, 10, 1890000, 8),
	(116, 5, 15, 9, 12, 2160000, 8),
	(117, 5, 16, 9, 14, 2430000, 9),
	(118, 5, 17, 9, 16, 2700000, 9),
	(119, 5, 18, 9, 18, 2970000, 10),
	(120, 5, 19, 9, 20, 3240000, 10),
	(121, 5, 20, 9, 22, 3510000, 10),
	(134, 6, 1, 1, 0, 0, 1),
	(135, 6, 2, 2, 0, 3750, 1),
	(136, 6, 3, 3, 0, 7500, 2),
	(137, 6, 4, 4, 0, 15000, 2),
	(138, 6, 5, 5, 0, 30000, 3),
	(139, 6, 6, 6, 0, 60000, 3),
	(140, 6, 7, 7, 0, 120000, 4),
	(141, 6, 8, 8, 0, 225000, 4),
	(142, 6, 9, 9, 0, 450000, 5),
	(143, 6, 10, 9, 2, 675000, 5),
	(144, 6, 11, 9, 4, 900000, 5),
	(145, 6, 12, 9, 6, 1125000, 6),
	(146, 6, 13, 9, 8, 1350000, 6),
	(147, 6, 14, 9, 10, 1575000, 6),
	(148, 6, 15, 9, 12, 1800000, 7),
	(149, 6, 16, 9, 14, 2025000, 7),
	(150, 6, 17, 9, 16, 2250000, 7),
	(151, 6, 18, 9, 18, 2475000, 8),
	(152, 6, 19, 9, 20, 2700000, 8),
	(153, 6, 20, 9, 22, 2925000, 8);

-- Dumping structure for view basic_fantasy_rpg.class_level_ranges
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `class_level_ranges` (
	`class_level_id` INT(11) NOT NULL,
	`class_id` INT(11) NOT NULL,
	`level` INT(11) NOT NULL,
	`hit_dice` INT(11) NOT NULL,
	`hit_point_bonus` INT(11) NOT NULL,
	`minimum_experience_points` INT(11) NOT NULL,
	`maximum_experience_points` BIGINT(11) NULL
) ENGINE=MyISAM;

-- Dumping structure for table basic_fantasy_rpg.class_level_saving_throws
CREATE TABLE IF NOT EXISTS `class_level_saving_throws` (
  `class_level_saving_throw_id` int(11) NOT NULL AUTO_INCREMENT,
  `class_level_id` int(11) NOT NULL,
  `saving_throw_id` int(11) NOT NULL,
  `saving_throw` int(11) NOT NULL,
  PRIMARY KEY (`class_level_saving_throw_id`),
  UNIQUE KEY `class_level_id_saving_throw_id` (`class_level_id`,`saving_throw_id`)
) ENGINE=InnoDB AUTO_INCREMENT=755 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.class_level_saving_throws: ~600 rows (approximately)
INSERT INTO `class_level_saving_throws` (`class_level_saving_throw_id`, `class_level_id`, `saving_throw_id`, `saving_throw`) VALUES
	(1, 1, 1, 11),
	(2, 2, 1, 10),
	(3, 3, 1, 10),
	(4, 4, 1, 9),
	(5, 5, 1, 9),
	(6, 6, 1, 9),
	(7, 13, 1, 9),
	(8, 14, 1, 8),
	(9, 15, 1, 8),
	(10, 16, 1, 8),
	(11, 17, 1, 8),
	(12, 18, 1, 7),
	(13, 19, 1, 7),
	(14, 20, 1, 7),
	(15, 21, 1, 7),
	(16, 22, 1, 6),
	(17, 23, 1, 6),
	(18, 24, 1, 6),
	(19, 25, 1, 6),
	(20, 26, 1, 5),
	(21, 1, 2, 12),
	(22, 2, 2, 11),
	(23, 3, 2, 11),
	(24, 4, 2, 10),
	(25, 5, 2, 10),
	(26, 6, 2, 10),
	(27, 13, 2, 10),
	(28, 14, 2, 9),
	(29, 15, 2, 9),
	(30, 16, 2, 9),
	(31, 17, 2, 9),
	(32, 18, 2, 8),
	(33, 19, 2, 8),
	(34, 20, 2, 8),
	(35, 21, 2, 8),
	(36, 22, 2, 7),
	(37, 23, 2, 7),
	(38, 24, 2, 7),
	(39, 25, 2, 7),
	(40, 26, 2, 6),
	(41, 1, 3, 14),
	(42, 2, 3, 13),
	(43, 3, 3, 13),
	(44, 4, 3, 13),
	(45, 5, 3, 13),
	(46, 6, 3, 12),
	(47, 13, 3, 12),
	(48, 14, 3, 12),
	(49, 15, 3, 12),
	(50, 16, 3, 11),
	(51, 17, 3, 11),
	(52, 18, 3, 11),
	(53, 19, 3, 11),
	(54, 20, 3, 10),
	(55, 21, 3, 10),
	(56, 22, 3, 10),
	(57, 23, 3, 10),
	(58, 24, 3, 9),
	(59, 25, 3, 9),
	(60, 26, 3, 9),
	(61, 1, 4, 16),
	(62, 2, 4, 15),
	(63, 3, 4, 15),
	(64, 4, 4, 15),
	(65, 5, 4, 15),
	(66, 6, 4, 14),
	(67, 13, 4, 14),
	(68, 14, 4, 14),
	(69, 15, 4, 14),
	(70, 16, 4, 13),
	(71, 17, 4, 13),
	(72, 18, 4, 13),
	(73, 19, 4, 13),
	(74, 20, 4, 12),
	(75, 21, 4, 12),
	(76, 22, 4, 12),
	(77, 23, 4, 12),
	(78, 24, 4, 11),
	(79, 25, 4, 11),
	(80, 26, 4, 11),
	(81, 1, 5, 15),
	(82, 2, 5, 14),
	(83, 3, 5, 14),
	(84, 4, 5, 14),
	(85, 5, 5, 14),
	(86, 6, 5, 13),
	(87, 13, 5, 13),
	(88, 14, 5, 13),
	(89, 15, 5, 13),
	(90, 16, 5, 12),
	(91, 17, 5, 12),
	(92, 18, 5, 12),
	(93, 19, 5, 12),
	(94, 20, 5, 11),
	(95, 21, 5, 11),
	(96, 22, 5, 11),
	(97, 23, 5, 11),
	(98, 24, 5, 10),
	(99, 25, 5, 10),
	(100, 26, 5, 10),
	(101, 27, 1, 12),
	(102, 28, 1, 11),
	(103, 33, 1, 11),
	(104, 34, 1, 11),
	(105, 35, 1, 11),
	(106, 36, 1, 10),
	(107, 37, 1, 10),
	(108, 38, 1, 9),
	(109, 39, 1, 9),
	(110, 40, 1, 9),
	(111, 41, 1, 9),
	(112, 42, 1, 8),
	(113, 43, 1, 8),
	(114, 44, 1, 7),
	(115, 45, 1, 7),
	(116, 46, 1, 7),
	(117, 47, 1, 7),
	(118, 48, 1, 6),
	(119, 49, 1, 6),
	(120, 50, 1, 5),
	(121, 27, 2, 13),
	(122, 28, 2, 12),
	(123, 33, 2, 12),
	(124, 34, 2, 11),
	(125, 35, 2, 11),
	(126, 36, 2, 11),
	(127, 37, 2, 11),
	(128, 38, 2, 10),
	(129, 39, 2, 10),
	(130, 40, 2, 9),
	(131, 41, 2, 9),
	(132, 42, 2, 9),
	(133, 43, 2, 9),
	(134, 44, 2, 8),
	(135, 45, 2, 8),
	(136, 46, 2, 7),
	(137, 47, 2, 7),
	(138, 48, 2, 7),
	(139, 49, 2, 7),
	(140, 50, 2, 6),
	(141, 27, 3, 14),
	(142, 28, 3, 14),
	(143, 33, 3, 14),
	(144, 34, 3, 13),
	(145, 35, 3, 13),
	(146, 36, 3, 12),
	(147, 37, 3, 12),
	(148, 38, 3, 12),
	(149, 39, 3, 12),
	(150, 40, 3, 11),
	(151, 41, 3, 11),
	(152, 42, 3, 10),
	(153, 43, 3, 10),
	(154, 44, 3, 10),
	(155, 45, 3, 10),
	(156, 46, 3, 9),
	(157, 47, 3, 9),
	(158, 48, 3, 8),
	(159, 49, 3, 8),
	(160, 50, 3, 8),
	(161, 27, 4, 15),
	(162, 28, 4, 15),
	(163, 33, 4, 15),
	(164, 34, 4, 14),
	(165, 35, 4, 14),
	(166, 36, 4, 14),
	(167, 37, 4, 14),
	(168, 38, 4, 13),
	(169, 39, 4, 13),
	(170, 40, 4, 12),
	(171, 41, 4, 12),
	(172, 42, 4, 12),
	(173, 43, 4, 12),
	(174, 44, 4, 11),
	(175, 45, 4, 11),
	(176, 46, 4, 10),
	(177, 47, 4, 10),
	(178, 48, 4, 10),
	(179, 49, 4, 10),
	(180, 50, 4, 9),
	(181, 27, 5, 17),
	(182, 28, 5, 16),
	(183, 33, 5, 16),
	(184, 34, 5, 15),
	(185, 35, 5, 15),
	(186, 36, 5, 15),
	(187, 37, 5, 15),
	(188, 38, 5, 14),
	(189, 39, 5, 14),
	(190, 40, 5, 13),
	(191, 41, 5, 13),
	(192, 42, 5, 13),
	(193, 43, 5, 13),
	(194, 44, 5, 12),
	(195, 45, 5, 12),
	(196, 46, 5, 11),
	(197, 47, 5, 11),
	(198, 48, 5, 11),
	(199, 49, 5, 11),
	(200, 50, 5, 10),
	(201, 51, 1, 13),
	(202, 52, 1, 13),
	(203, 53, 1, 13),
	(204, 54, 1, 12),
	(205, 55, 1, 12),
	(206, 56, 1, 12),
	(207, 57, 1, 12),
	(208, 58, 1, 11),
	(209, 59, 1, 11),
	(210, 60, 1, 11),
	(211, 61, 1, 11),
	(212, 62, 1, 10),
	(213, 63, 1, 10),
	(214, 64, 1, 10),
	(215, 65, 1, 10),
	(216, 66, 1, 9),
	(217, 67, 1, 9),
	(218, 68, 1, 9),
	(219, 69, 1, 9),
	(220, 70, 1, 8),
	(221, 51, 2, 14),
	(222, 52, 2, 14),
	(223, 53, 2, 14),
	(224, 54, 2, 13),
	(225, 55, 2, 13),
	(226, 56, 2, 12),
	(227, 57, 2, 12),
	(228, 58, 2, 11),
	(229, 59, 2, 11),
	(230, 60, 2, 10),
	(231, 61, 2, 10),
	(232, 62, 2, 10),
	(233, 63, 2, 10),
	(234, 64, 2, 9),
	(235, 65, 2, 9),
	(236, 66, 2, 8),
	(237, 67, 2, 8),
	(238, 68, 2, 7),
	(239, 69, 2, 7),
	(240, 70, 2, 6),
	(241, 51, 3, 13),
	(242, 52, 3, 13),
	(243, 53, 3, 13),
	(244, 54, 3, 12),
	(245, 55, 3, 12),
	(246, 56, 3, 11),
	(247, 57, 3, 11),
	(248, 58, 3, 10),
	(249, 59, 3, 10),
	(250, 60, 3, 9),
	(251, 61, 3, 9),
	(252, 62, 3, 9),
	(253, 63, 3, 9),
	(254, 64, 3, 8),
	(255, 65, 3, 8),
	(256, 66, 3, 7),
	(257, 67, 3, 7),
	(258, 68, 3, 6),
	(259, 69, 3, 6),
	(260, 70, 3, 5),
	(261, 51, 4, 16),
	(262, 52, 4, 15),
	(263, 53, 4, 15),
	(264, 54, 4, 15),
	(265, 55, 4, 15),
	(266, 56, 4, 14),
	(267, 57, 4, 14),
	(268, 58, 4, 14),
	(269, 59, 4, 14),
	(270, 60, 4, 13),
	(271, 61, 4, 13),
	(272, 62, 4, 13),
	(273, 63, 4, 13),
	(274, 64, 4, 12),
	(275, 65, 4, 12),
	(276, 66, 4, 12),
	(277, 67, 4, 12),
	(278, 68, 4, 11),
	(279, 69, 4, 11),
	(280, 70, 4, 11),
	(281, 51, 5, 15),
	(282, 52, 5, 14),
	(283, 53, 5, 14),
	(284, 54, 5, 13),
	(285, 55, 5, 13),
	(286, 56, 5, 13),
	(287, 57, 5, 13),
	(288, 58, 5, 12),
	(289, 59, 5, 12),
	(290, 60, 5, 11),
	(291, 61, 5, 11),
	(292, 62, 5, 11),
	(293, 63, 5, 11),
	(294, 64, 5, 10),
	(295, 65, 5, 10),
	(296, 66, 5, 9),
	(297, 67, 5, 9),
	(298, 68, 5, 9),
	(299, 69, 5, 9),
	(300, 70, 5, 8),
	(301, 71, 1, 13),
	(302, 72, 1, 12),
	(303, 73, 1, 12),
	(304, 74, 1, 11),
	(305, 75, 1, 11),
	(306, 76, 1, 11),
	(307, 77, 1, 11),
	(308, 78, 1, 10),
	(309, 79, 1, 10),
	(310, 80, 1, 9),
	(311, 81, 1, 9),
	(312, 82, 1, 9),
	(313, 83, 1, 9),
	(314, 84, 1, 8),
	(315, 85, 1, 8),
	(316, 86, 1, 7),
	(317, 87, 1, 7),
	(318, 88, 1, 7),
	(319, 89, 1, 7),
	(320, 90, 1, 6),
	(321, 71, 2, 14),
	(322, 72, 2, 14),
	(323, 73, 2, 14),
	(324, 74, 2, 13),
	(325, 75, 2, 13),
	(326, 76, 2, 13),
	(327, 77, 2, 13),
	(328, 78, 2, 12),
	(329, 79, 2, 12),
	(330, 80, 2, 12),
	(331, 81, 2, 12),
	(332, 82, 2, 10),
	(333, 83, 2, 10),
	(334, 84, 2, 10),
	(335, 85, 2, 10),
	(336, 86, 2, 9),
	(337, 87, 2, 9),
	(338, 88, 2, 9),
	(339, 89, 2, 9),
	(340, 90, 2, 8),
	(341, 71, 3, 13),
	(342, 72, 3, 12),
	(343, 73, 3, 12),
	(344, 74, 3, 12),
	(345, 75, 3, 12),
	(346, 76, 3, 11),
	(347, 77, 3, 11),
	(348, 78, 3, 11),
	(349, 79, 3, 11),
	(350, 80, 3, 10),
	(351, 81, 3, 10),
	(352, 82, 3, 10),
	(353, 83, 3, 10),
	(354, 84, 3, 9),
	(355, 85, 3, 9),
	(356, 86, 3, 9),
	(357, 87, 3, 9),
	(358, 88, 3, 8),
	(359, 89, 3, 8),
	(360, 90, 3, 8),
	(361, 71, 4, 16),
	(362, 72, 4, 15),
	(363, 73, 4, 15),
	(364, 74, 4, 14),
	(365, 75, 4, 14),
	(366, 76, 4, 13),
	(367, 77, 4, 13),
	(368, 78, 4, 12),
	(369, 79, 4, 12),
	(370, 80, 4, 11),
	(371, 81, 4, 11),
	(372, 82, 4, 10),
	(373, 83, 4, 10),
	(374, 84, 4, 9),
	(375, 85, 4, 9),
	(376, 86, 4, 8),
	(377, 87, 4, 8),
	(378, 88, 4, 7),
	(379, 89, 4, 7),
	(380, 90, 4, 6),
	(381, 71, 5, 15),
	(382, 72, 5, 14),
	(383, 73, 5, 14),
	(384, 74, 5, 13),
	(385, 75, 5, 13),
	(386, 76, 5, 13),
	(387, 77, 5, 13),
	(388, 78, 5, 12),
	(389, 79, 5, 12),
	(390, 80, 5, 11),
	(391, 81, 5, 11),
	(392, 82, 5, 11),
	(393, 83, 5, 11),
	(394, 84, 5, 10),
	(395, 85, 5, 10),
	(396, 86, 5, 9),
	(397, 87, 5, 9),
	(398, 88, 5, 9),
	(399, 89, 5, 9),
	(400, 90, 5, 8),
	(528, 102, 1, 12),
	(529, 102, 2, 13),
	(530, 102, 3, 13),
	(531, 102, 4, 15),
	(532, 102, 5, 15),
	(533, 103, 1, 11),
	(534, 103, 2, 12),
	(535, 103, 3, 13),
	(536, 103, 4, 15),
	(537, 103, 5, 14),
	(538, 104, 1, 11),
	(539, 104, 2, 12),
	(540, 104, 3, 13),
	(541, 104, 4, 15),
	(542, 104, 5, 14),
	(543, 105, 1, 11),
	(544, 105, 2, 11),
	(545, 105, 3, 12),
	(546, 105, 4, 14),
	(547, 105, 5, 13),
	(548, 106, 1, 11),
	(549, 106, 2, 11),
	(550, 106, 3, 12),
	(551, 106, 4, 14),
	(552, 106, 5, 13),
	(553, 107, 1, 10),
	(554, 107, 2, 11),
	(555, 107, 3, 11),
	(556, 107, 4, 14),
	(557, 107, 5, 13),
	(558, 108, 1, 10),
	(559, 108, 2, 11),
	(560, 108, 3, 11),
	(561, 108, 4, 14),
	(562, 108, 5, 13),
	(563, 109, 1, 9),
	(564, 109, 2, 10),
	(565, 109, 3, 10),
	(566, 109, 4, 13),
	(567, 109, 5, 12),
	(568, 110, 1, 9),
	(569, 110, 2, 10),
	(570, 110, 3, 10),
	(571, 110, 4, 13),
	(572, 110, 5, 12),
	(573, 111, 1, 9),
	(574, 111, 2, 9),
	(575, 111, 3, 9),
	(576, 111, 4, 12),
	(577, 111, 5, 11),
	(578, 112, 1, 9),
	(579, 112, 2, 9),
	(580, 112, 3, 9),
	(581, 112, 4, 12),
	(582, 112, 5, 11),
	(583, 113, 1, 8),
	(584, 113, 2, 9),
	(585, 113, 3, 9),
	(586, 113, 4, 12),
	(587, 113, 5, 11),
	(588, 114, 1, 8),
	(589, 114, 2, 9),
	(590, 114, 3, 9),
	(591, 114, 4, 12),
	(592, 114, 5, 11),
	(593, 115, 1, 7),
	(594, 115, 2, 8),
	(595, 115, 3, 8),
	(596, 115, 4, 11),
	(597, 115, 5, 10),
	(598, 116, 1, 7),
	(599, 116, 2, 8),
	(600, 116, 3, 8),
	(601, 116, 4, 11),
	(602, 116, 5, 10),
	(603, 117, 1, 7),
	(604, 117, 2, 7),
	(605, 117, 3, 7),
	(606, 117, 4, 10),
	(607, 117, 5, 9),
	(608, 118, 1, 7),
	(609, 118, 2, 7),
	(610, 118, 3, 7),
	(611, 118, 4, 10),
	(612, 118, 5, 9),
	(613, 119, 1, 6),
	(614, 119, 2, 7),
	(615, 119, 3, 6),
	(616, 119, 4, 10),
	(617, 119, 5, 9),
	(618, 120, 1, 6),
	(619, 120, 2, 7),
	(620, 120, 3, 6),
	(621, 120, 4, 10),
	(622, 120, 5, 9),
	(623, 121, 1, 5),
	(624, 121, 2, 6),
	(625, 121, 3, 5),
	(626, 121, 4, 9),
	(627, 121, 5, 8),
	(655, 134, 1, 13),
	(656, 134, 2, 14),
	(657, 134, 3, 13),
	(658, 134, 4, 16),
	(659, 134, 5, 15),
	(660, 135, 1, 12),
	(661, 135, 2, 14),
	(662, 135, 3, 12),
	(663, 135, 4, 15),
	(664, 135, 5, 14),
	(665, 136, 1, 12),
	(666, 136, 2, 14),
	(667, 136, 3, 12),
	(668, 136, 4, 15),
	(669, 136, 5, 14),
	(670, 137, 1, 11),
	(671, 137, 2, 13),
	(672, 137, 3, 12),
	(673, 137, 4, 14),
	(674, 137, 5, 13),
	(675, 138, 1, 11),
	(676, 138, 2, 13),
	(677, 138, 3, 12),
	(678, 138, 4, 14),
	(679, 138, 5, 13),
	(680, 139, 1, 11),
	(681, 139, 2, 12),
	(682, 139, 3, 11),
	(683, 139, 4, 13),
	(684, 139, 5, 13),
	(685, 140, 1, 11),
	(686, 140, 2, 12),
	(687, 140, 3, 11),
	(688, 140, 4, 13),
	(689, 140, 5, 13),
	(690, 141, 1, 10),
	(691, 141, 2, 11),
	(692, 141, 3, 10),
	(693, 141, 4, 12),
	(694, 141, 5, 12),
	(695, 142, 1, 10),
	(696, 142, 2, 11),
	(697, 142, 3, 10),
	(698, 142, 4, 12),
	(699, 142, 5, 12),
	(700, 143, 1, 9),
	(701, 143, 2, 10),
	(702, 143, 3, 9),
	(703, 143, 4, 11),
	(704, 143, 5, 11),
	(705, 144, 1, 9),
	(706, 144, 2, 10),
	(707, 144, 3, 9),
	(708, 144, 4, 11),
	(709, 144, 5, 11),
	(710, 145, 1, 9),
	(711, 145, 2, 10),
	(712, 145, 3, 9),
	(713, 145, 4, 10),
	(714, 145, 5, 11),
	(715, 146, 1, 9),
	(716, 146, 2, 10),
	(717, 146, 3, 9),
	(718, 146, 4, 10),
	(719, 146, 5, 11),
	(720, 147, 1, 8),
	(721, 147, 2, 9),
	(722, 147, 3, 8),
	(723, 147, 4, 9),
	(724, 147, 5, 10),
	(725, 148, 1, 8),
	(726, 148, 2, 9),
	(727, 148, 3, 8),
	(728, 148, 4, 9),
	(729, 148, 5, 10),
	(730, 149, 1, 7),
	(731, 149, 2, 8),
	(732, 149, 3, 7),
	(733, 149, 4, 8),
	(734, 149, 5, 9),
	(735, 150, 1, 7),
	(736, 150, 2, 8),
	(737, 150, 3, 7),
	(738, 150, 4, 8),
	(739, 150, 5, 9),
	(740, 151, 1, 7),
	(741, 151, 2, 7),
	(742, 151, 3, 6),
	(743, 151, 4, 7),
	(744, 151, 5, 9),
	(745, 152, 1, 7),
	(746, 152, 2, 7),
	(747, 152, 3, 6),
	(748, 152, 4, 7),
	(749, 152, 5, 9),
	(750, 153, 1, 6),
	(751, 153, 2, 6),
	(752, 153, 3, 5),
	(753, 153, 4, 6),
	(754, 153, 5, 8);

-- Dumping structure for function basic_fantasy_rpg.get_ability_modifier
DELIMITER //
CREATE FUNCTION `get_ability_modifier`(`ability_score` INT
) RETURNS int(11)
BEGIN
	CASE
		WHEN ability_score=3 THEN
			RETURN -3;
		WHEN ability_score>=4 AND ability_score<=5 THEN
			RETURN -2;
		WHEN ability_score>=6 AND ability_score<=8 THEN
			RETURN -1;
		WHEN ability_score>=9 AND ability_score<=12 THEN
			RETURN 0;
		WHEN ability_score>=13 AND ability_score<=15 THEN
			RETURN 1;
		WHEN ability_score>=16 AND ability_score<=17 THEN
			RETURN 2;
		WHEN ability_score=18 THEN
			RETURN 3;
	END CASE;
END//
DELIMITER ;

-- Dumping structure for procedure basic_fantasy_rpg.nuke_player_data
DELIMITER //
CREATE PROCEDURE `nuke_player_data`()
BEGIN
	DELETE FROM character_hit_dice;
	DELETE FROM character_abilities;
	DELETE FROM characters;
	DELETE FROM players;
END//
DELIMITER ;

-- Dumping structure for table basic_fantasy_rpg.players
CREATE TABLE IF NOT EXISTS `players` (
  `player_id` int(11) NOT NULL AUTO_INCREMENT,
  `player_name` varchar(50) NOT NULL,
  PRIMARY KEY (`player_id`),
  UNIQUE KEY `player_name` (`player_name`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.players: ~1 rows (approximately)

-- Dumping structure for view basic_fantasy_rpg.player_details
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `player_details` (
	`player_id` INT(11) NOT NULL,
	`player_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`character_count` BIGINT(21) NOT NULL
) ENGINE=MyISAM;

-- Dumping structure for table basic_fantasy_rpg.races
CREATE TABLE IF NOT EXISTS `races` (
  `race_id` int(11) NOT NULL AUTO_INCREMENT,
  `race_name` varchar(50) NOT NULL,
  `maximum_hit_die` int(11) NOT NULL,
  PRIMARY KEY (`race_id`),
  UNIQUE KEY `race_name` (`race_name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.races: ~4 rows (approximately)
INSERT INTO `races` (`race_id`, `race_name`, `maximum_hit_die`) VALUES
	(1, 'Dwarf', 8),
	(2, 'Elf', 6),
	(3, 'Halfling', 8),
	(4, 'Human', 8);

-- Dumping structure for table basic_fantasy_rpg.race_ability_maximums
CREATE TABLE IF NOT EXISTS `race_ability_maximums` (
  `race_ability_maximum_id` int(11) NOT NULL AUTO_INCREMENT,
  `race_id` int(11) NOT NULL,
  `ability_id` int(11) NOT NULL,
  `maximum_score` int(11) NOT NULL,
  PRIMARY KEY (`race_ability_maximum_id`),
  UNIQUE KEY `race_id_ability_id` (`race_id`,`ability_id`),
  KEY `FK_race_ability_maximums_abilities` (`ability_id`),
  CONSTRAINT `FK_race_ability_maximums_abilities` FOREIGN KEY (`ability_id`) REFERENCES `abilities` (`ability_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_race_ability_maximums_races` FOREIGN KEY (`race_id`) REFERENCES `races` (`race_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.race_ability_maximums: ~2 rows (approximately)
INSERT INTO `race_ability_maximums` (`race_ability_maximum_id`, `race_id`, `ability_id`, `maximum_score`) VALUES
	(1, 1, 6, 17),
	(2, 2, 5, 17),
	(3, 3, 1, 17);

-- Dumping structure for table basic_fantasy_rpg.race_ability_minimums
CREATE TABLE IF NOT EXISTS `race_ability_minimums` (
  `race_ability_minimum_id` int(11) NOT NULL AUTO_INCREMENT,
  `race_id` int(11) NOT NULL,
  `ability_id` int(11) NOT NULL,
  `minimum_score` int(11) NOT NULL,
  PRIMARY KEY (`race_ability_minimum_id`),
  UNIQUE KEY `race_id_ability_id` (`race_id`,`ability_id`),
  KEY `FK_race_ability_minimums_abilities` (`ability_id`),
  CONSTRAINT `FK_race_ability_minimums_abilities` FOREIGN KEY (`ability_id`) REFERENCES `abilities` (`ability_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_race_ability_minimums_races` FOREIGN KEY (`race_id`) REFERENCES `races` (`race_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.race_ability_minimums: ~2 rows (approximately)
INSERT INTO `race_ability_minimums` (`race_ability_minimum_id`, `race_id`, `ability_id`, `minimum_score`) VALUES
	(1, 1, 5, 9),
	(2, 2, 2, 9),
	(3, 3, 4, 9);

-- Dumping structure for view basic_fantasy_rpg.race_ability_ranges
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `race_ability_ranges` (
	`race_id` INT(11) NOT NULL,
	`race_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_id` INT(11) NOT NULL,
	`ability_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_abbreviation` VARCHAR(3) NOT NULL COLLATE 'latin1_general_ci',
	`minimum_score` INT(11) NULL,
	`maximum_score` INT(11) NULL
) ENGINE=MyISAM;

-- Dumping structure for table basic_fantasy_rpg.race_classes
CREATE TABLE IF NOT EXISTS `race_classes` (
  `race_class_id` int(11) NOT NULL AUTO_INCREMENT,
  `race_id` int(11) NOT NULL,
  `class_id` int(11) NOT NULL,
  PRIMARY KEY (`race_class_id`),
  UNIQUE KEY `race_id_class_id` (`race_id`,`class_id`),
  KEY `FK__classes` (`class_id`),
  CONSTRAINT `FK__classes` FOREIGN KEY (`class_id`) REFERENCES `classes` (`class_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK__races` FOREIGN KEY (`race_id`) REFERENCES `races` (`race_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.race_classes: ~16 rows (approximately)
INSERT INTO `race_classes` (`race_class_id`, `race_id`, `class_id`) VALUES
	(1, 1, 1),
	(2, 1, 2),
	(3, 1, 4),
	(4, 2, 1),
	(5, 2, 2),
	(6, 2, 3),
	(7, 2, 4),
	(8, 2, 5),
	(9, 2, 6),
	(11, 3, 1),
	(10, 3, 2),
	(12, 3, 4),
	(13, 4, 1),
	(14, 4, 2),
	(15, 4, 3),
	(16, 4, 4);

-- Dumping structure for view basic_fantasy_rpg.race_class_ability_ranges
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `race_class_ability_ranges` (
	`race_class_id` INT(11) NOT NULL,
	`race_id` INT(11) NOT NULL,
	`race_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`class_id` INT(11) NOT NULL,
	`class_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_id` INT(11) NOT NULL,
	`ability_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_abbreviation` VARCHAR(3) NOT NULL COLLATE 'latin1_general_ci',
	`minimum_score` INT(11) NULL,
	`maximum_score` INT(11) NULL
) ENGINE=MyISAM;

-- Dumping structure for view basic_fantasy_rpg.race_class_details
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `race_class_details` (
	`race_class_id` INT(11) NOT NULL,
	`race_id` INT(11) NOT NULL,
	`race_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`class_id` INT(11) NOT NULL,
	`class_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`hit_die_size` INT(11) NOT NULL,
	`maximum_hit_dice` INT(11) NULL
) ENGINE=MyISAM;

-- Dumping structure for table basic_fantasy_rpg.race_saving_throw_bonuses
CREATE TABLE IF NOT EXISTS `race_saving_throw_bonuses` (
  `race_saving_throw_bonus_id` int(11) NOT NULL AUTO_INCREMENT,
  `race_id` int(11) NOT NULL,
  `saving_throw_id` int(11) NOT NULL,
  `saving_throw_bonus` int(11) NOT NULL,
  PRIMARY KEY (`race_saving_throw_bonus_id`),
  UNIQUE KEY `race_id_saving_throw_id` (`race_id`,`saving_throw_id`),
  KEY `FK_race_saving_throw_bonuses_saving_throws` (`saving_throw_id`),
  CONSTRAINT `FK_race_saving_throw_bonuses_races` FOREIGN KEY (`race_id`) REFERENCES `races` (`race_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_race_saving_throw_bonuses_saving_throws` FOREIGN KEY (`saving_throw_id`) REFERENCES `saving_throws` (`saving_throw_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.race_saving_throw_bonuses: ~13 rows (approximately)
INSERT INTO `race_saving_throw_bonuses` (`race_saving_throw_bonus_id`, `race_id`, `saving_throw_id`, `saving_throw_bonus`) VALUES
	(1, 1, 4, 3),
	(2, 1, 1, 4),
	(3, 1, 2, 4),
	(4, 1, 3, 4),
	(5, 1, 5, 4),
	(6, 2, 3, 1),
	(7, 2, 2, 2),
	(8, 2, 5, 2),
	(9, 3, 1, 4),
	(10, 3, 4, 3),
	(11, 3, 2, 4),
	(12, 3, 3, 4),
	(13, 3, 5, 4);

-- Dumping structure for table basic_fantasy_rpg.saving_throws
CREATE TABLE IF NOT EXISTS `saving_throws` (
  `saving_throw_id` int(11) NOT NULL AUTO_INCREMENT,
  `saving_throw_name` varchar(50) NOT NULL,
  PRIMARY KEY (`saving_throw_id`),
  UNIQUE KEY `saving_throw_name` (`saving_throw_name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.saving_throws: ~4 rows (approximately)
INSERT INTO `saving_throws` (`saving_throw_id`, `saving_throw_name`) VALUES
	(1, 'Death Ray or Poison'),
	(4, 'Dragon Breath'),
	(2, 'Magic Wands'),
	(3, 'Paralysis or Petrify'),
	(5, 'Spells');

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `character_ability_details`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `character_ability_details` AS SELECT 
	c.character_id,
	c.character_name,
	ca.ability_score,
	a.ability_id,
	a.ability_name,
	a.ability_abbreviation,
	get_ability_modifier(ca.ability_score) AS ability_modifier,
	a.affects_hit_dice
FROM
	characters c
	JOIN character_abilities ca ON ca.character_id=c.character_id
	JOIN abilities a ON ca.ability_id=a.ability_id ;

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `character_details`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `character_details` AS SELECT 
	c.character_id,
	c.character_name,
	r.race_id,
	r.race_name,
	p.player_id,
	p.player_name,
	cl.class_id,
	cl.class_name,
	c.experience_points,
	clr.`level`,
	LEAST(r.maximum_hit_die, cl.hit_die) AS hit_die,
	SUM(chd.modified_die_roll)+clr.hit_point_bonus AS hit_points,
	c.character_description,
	lvl.attack_bonus,
	c.money,
	lvl.class_level_id
FROM
	characters c
	JOIN race_classes rc ON c.race_class_id=rc.race_class_id
	JOIN races r ON rc.race_id=r.race_id 
	JOIN players p ON c.player_id=p.player_id 
	JOIN classes cl ON rc.class_id=cl.class_id 
	JOIN class_level_ranges clr ON clr.class_id=rc.class_id AND c.experience_points>=clr.minimum_experience_points AND c.experience_points<clr.maximum_experience_points 
	JOIN character_hit_dice_details chd ON chd.character_id=c.character_id AND chd.die<=clr.hit_dice
	JOIN class_levels lvl ON clr.class_level_id=lvl.class_level_id
GROUP BY
	c.character_id,
	c.character_name,
	r.race_id,
	r.race_name,
	p.player_id,
	p.player_name,
	cl.class_id,
	cl.class_name,
	c.experience_points,
	clr.`level`,
	LEAST(r.maximum_hit_die, cl.hit_die) ;

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `character_hit_dice_details`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `character_hit_dice_details` AS SELECT
	chd.character_hit_dice_id,
	chd.character_id,
	chd.die,
	GREATEST(1,chd.die_roll + sum(a.affects_hit_dice * get_ability_modifier(ca.ability_score))) modified_die_roll
FROM
	character_hit_dice chd
	LEFT JOIN character_abilities ca ON ca.character_id=chd.character_id
	LEFT JOIN abilities a ON a.ability_id=ca.ability_id
GROUP BY
	chd.character_hit_dice_id,
	chd.character_id,
	chd.die,
	chd.die_roll ;

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `character_saving_throw_details`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `character_saving_throw_details` AS SELECT 
	cd.character_id,
	cd.character_name,
	clst.saving_throw,
	st.saving_throw_id,
	st.saving_throw_name,
	COALESCE(rstb.saving_throw_bonus,0) AS saving_throw_bonus
FROM
	character_details cd 
	CROSS JOIN saving_throws st
	JOIN class_level_saving_throws clst ON clst.class_level_id=cd.class_level_id AND clst.saving_throw_id=st.saving_throw_id 
	LEFT JOIN race_saving_throw_bonuses rstb ON rstb.race_id=cd.race_id AND rstb.saving_throw_id=st.saving_throw_id ;

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `class_ability_ranges`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `class_ability_ranges` AS SELECT 
	c.class_id,
	c.class_name,
	a.ability_id,
	a.ability_name,
	a.ability_abbreviation,
	COALESCE(cam.minimum_score,3) AS minimum_score,
	18 AS maximum_score
FROM
	classes c
	CROSS JOIN abilities a
	LEFT JOIN class_ability_minimums cam ON cam.class_id=c.class_id AND cam.ability_id=a.ability_id ;

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `class_level_ranges`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `class_level_ranges` AS SELECT 
	lvl.class_level_id,
	lvl.class_id,
	lvl.`level`,
	lvl.hit_dice,
	lvl.hit_point_bonus,
	lvl.experience_points AS minimum_experience_points,
	COALESCE(nxt.experience_points, 2147483647) AS maximum_experience_points
FROM
	class_levels lvl 
	LEFT JOIN class_levels nxt ON nxt.`level`=lvl.`level`+1 AND nxt.class_id=lvl.class_id ;

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `player_details`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `player_details` AS SELECT 
	p.player_id,
	p.player_name,
	COUNT(c.character_id) character_count
FROM
	players p
	LEFT JOIN characters c ON c.player_id=p.player_id
GROUP BY
	p.player_id, p.player_name ;

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `race_ability_ranges`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `race_ability_ranges` AS SELECT 
	r.race_id,
	r.race_name,
	a.ability_id,
	a.ability_name,
	a.ability_abbreviation,
	COALESCE(ramin.minimum_score,3) AS minimum_score,
	COALESCE(ramax.maximum_score,18) AS maximum_score
FROM
	races r
	CROSS JOIN abilities a
	LEFT JOIN race_ability_minimums ramin ON ramin.race_id=r.race_id AND ramin.ability_id=a.ability_id
	LEFT JOIN race_ability_maximums ramax ON ramax.race_id=r.race_id AND ramax.ability_id=a.ability_id ;

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `race_class_ability_ranges`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `race_class_ability_ranges` AS SELECT 
	rc.race_class_id,
	rc.race_id,
	r.race_name,
	cl.class_id,
	cl.class_name,
	a.ability_id,
	a.ability_name,
	a.ability_abbreviation,
	GREATEST(COALESCE(ramin.minimum_score,3),COALESCE(clam.minimum_score,3),3) minimum_score,
	LEAST(COALESCE(ramax.maximum_score,18),18) maximum_score
FROM
	race_classes rc
	CROSS JOIN abilities a
	JOIN races r ON rc.race_id=r.race_id
	JOIN classes cl ON cl.class_id=rc.class_id
	LEFT JOIN race_ability_minimums ramin ON ramin.race_id=rc.race_id AND ramin.ability_id=a.ability_id
	LEFT JOIN race_ability_maximums ramax ON ramax.race_id=rc.race_id AND ramax.ability_id=a.ability_id
	LEFT JOIN class_ability_minimums clam ON clam.class_id=rc.class_id AND clam.ability_id=a.ability_id ;

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `race_class_details`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `race_class_details` AS SELECT 
	rc.race_class_id,
	r.race_id,
	r.race_name,
	cl.class_id,
	cl.class_name,
	LEAST(r.maximum_hit_die, cl.hit_die) AS hit_die_size,
	MAX(lvl.hit_dice) AS maximum_hit_dice
FROM
	race_classes rc
	JOIN races r ON rc.race_id=r.race_id
	JOIN classes cl ON rc.class_id=cl.class_id 
	JOIN class_levels lvl ON lvl.class_id=rc.class_id
GROUP BY
	rc.race_class_id,
	r.race_id,
	r.race_name,
	cl.class_id,
	cl.class_name,
	LEAST(r.maximum_hit_die, cl.hit_die) ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
