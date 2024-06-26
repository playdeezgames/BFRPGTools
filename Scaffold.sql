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
  PRIMARY KEY (`ability_id`),
  UNIQUE KEY `ability_name` (`ability_name`),
  UNIQUE KEY `ability_abbreviation` (`ability_abbreviation`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.abilities: ~6 rows (approximately)
INSERT INTO `abilities` (`ability_id`, `ability_name`, `ability_abbreviation`) VALUES
	(1, 'Strength', 'STR'),
	(2, 'Intelligence', 'INT'),
	(3, 'Wisdom', 'WIS'),
	(4, 'Dexterity', 'DEX'),
	(5, 'Constitution', 'CON'),
	(6, 'Charisma', 'CHA');

-- Dumping structure for table basic_fantasy_rpg.characters
CREATE TABLE IF NOT EXISTS `characters` (
  `character_id` int(11) NOT NULL AUTO_INCREMENT,
  `character_name` varchar(50) NOT NULL,
  `player_id` int(11) NOT NULL,
  `race_id` int(11) NOT NULL,
  PRIMARY KEY (`character_id`),
  UNIQUE KEY `character_name_player_id` (`character_name`,`player_id`),
  KEY `FK_characters_players` (`player_id`),
  KEY `FK_characters_races` (`race_id`),
  CONSTRAINT `FK_characters_players` FOREIGN KEY (`player_id`) REFERENCES `players` (`player_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_characters_races` FOREIGN KEY (`race_id`) REFERENCES `races` (`race_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.characters: ~1 rows (approximately)
INSERT INTO `characters` (`character_id`, `character_name`, `player_id`, `race_id`) VALUES
	(11, 'puup', 19, 2);

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
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.character_abilities: ~6 rows (approximately)
INSERT INTO `character_abilities` (`character_ability_id`, `character_id`, `ability_id`, `ability_score`) VALUES
	(49, 11, 1, 7),
	(50, 11, 2, 9),
	(51, 11, 3, 12),
	(52, 11, 4, 15),
	(53, 11, 5, 8),
	(54, 11, 6, 11);

-- Dumping structure for view basic_fantasy_rpg.character_ability_details
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `character_ability_details` (
	`character_id` INT(11) NOT NULL,
	`character_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_score` INT(11) NOT NULL,
	`ability_id` INT(11) NOT NULL,
	`ability_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`ability_abbreviation` VARCHAR(3) NOT NULL COLLATE 'latin1_general_ci'
) ENGINE=MyISAM;

-- Dumping structure for view basic_fantasy_rpg.character_details
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `character_details` (
	`character_id` INT(11) NOT NULL,
	`character_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci',
	`race_id` INT(11) NOT NULL,
	`race_name` VARCHAR(50) NOT NULL COLLATE 'latin1_general_ci'
) ENGINE=MyISAM;

-- Dumping structure for table basic_fantasy_rpg.classes
CREATE TABLE IF NOT EXISTS `classes` (
  `class_id` int(11) NOT NULL AUTO_INCREMENT,
  `class_name` varchar(50) NOT NULL,
  PRIMARY KEY (`class_id`),
  UNIQUE KEY `class_name` (`class_name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.classes: ~6 rows (approximately)
INSERT INTO `classes` (`class_id`, `class_name`) VALUES
	(1, 'Cleric'),
	(2, 'Fighter'),
	(5, 'Fighter/Magic-User'),
	(3, 'Magic-User'),
	(6, 'Magic-User/Thief'),
	(4, 'Thief');

-- Dumping structure for table basic_fantasy_rpg.players
CREATE TABLE IF NOT EXISTS `players` (
  `player_id` int(11) NOT NULL AUTO_INCREMENT,
  `player_name` varchar(50) NOT NULL,
  PRIMARY KEY (`player_id`),
  UNIQUE KEY `player_name` (`player_name`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.players: ~1 rows (approximately)
INSERT INTO `players` (`player_id`, `player_name`) VALUES
	(19, 'test');

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
  PRIMARY KEY (`race_id`),
  UNIQUE KEY `race_name` (`race_name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.races: ~4 rows (approximately)
INSERT INTO `races` (`race_id`, `race_name`) VALUES
	(1, 'Dwarf'),
	(2, 'Elf'),
	(3, 'Halfling'),
	(4, 'Human');

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

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `character_ability_details`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `character_ability_details` AS SELECT 
	c.character_id,
	c.character_name,
	ca.ability_score,
	a.ability_id,
	a.ability_name,
	a.ability_abbreviation
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
	r.race_name
FROM
	characters c
	JOIN races r ON c.race_id=r.race_id ;

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

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
