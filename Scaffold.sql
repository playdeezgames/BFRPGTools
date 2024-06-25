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
  PRIMARY KEY (`character_id`),
  UNIQUE KEY `character_name_player_id` (`character_name`,`player_id`),
  KEY `FK_characters_players` (`player_id`),
  CONSTRAINT `FK_characters_players` FOREIGN KEY (`player_id`) REFERENCES `players` (`player_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.characters: ~1 rows (approximately)
INSERT INTO `characters` (`character_id`, `character_name`, `player_id`) VALUES
	(9, 'test', 19);

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
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Dumping data for table basic_fantasy_rpg.character_abilities: ~6 rows (approximately)
INSERT INTO `character_abilities` (`character_ability_id`, `character_id`, `ability_id`, `ability_score`) VALUES
	(37, 9, 6, 10),
	(38, 9, 5, 7),
	(39, 9, 4, 14),
	(40, 9, 2, 17),
	(41, 9, 1, 12),
	(42, 9, 3, 10);

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

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
