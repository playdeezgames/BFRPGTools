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
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

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
) ENGINE=InnoDB AUTO_INCREMENT=241 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.character_abilities: ~0 rows (approximately)

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
	`money` DECIMAL(20,2) NOT NULL
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
) ENGINE=InnoDB AUTO_INCREMENT=190 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.character_hit_dice: ~0 rows (approximately)

-- Dumping structure for view basic_fantasy_rpg.character_hit_dice_details
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `character_hit_dice_details` (
	`character_hit_dice_id` INT(11) NOT NULL,
	`character_id` INT(11) NOT NULL,
	`die` INT(11) NOT NULL,
	`modified_die_roll` DECIMAL(36,0) NULL
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
) ENGINE=InnoDB AUTO_INCREMENT=165 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

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
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.players: ~0 rows (approximately)

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

-- Dumping data for table basic_fantasy_rpg.race_ability_maximums: ~3 rows (approximately)
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

-- Dumping data for table basic_fantasy_rpg.race_ability_minimums: ~3 rows (approximately)
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
	c.money
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
