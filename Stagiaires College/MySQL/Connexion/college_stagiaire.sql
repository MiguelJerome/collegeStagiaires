-- Supprimer la base de données si elle existe
DROP DATABASE IF EXISTS college_stagiaire;

-- Créer la base de données
CREATE DATABASE college_stagiaire 
CHARACTER SET utf8mb4 
COLLATE utf8mb4_unicode_ci;

-- Utiliser la base de données pour le reste des opérations
USE college_stagiaire;

DROP TABLE IF EXISTS programme;
CREATE TABLE `programme` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) NOT NULL,
  `dureeEnMois` int(11) DEFAULT NULL,
  CONSTRAINT pk_programme PRIMARY KEY (`id`)
);

DROP TABLE IF EXISTS stagiaire;
CREATE TABLE `stagiaire` (
  `id_stagiaire` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) NOT NULL,
  `prenom` varchar(50) NOT NULL,
  `date_naissance` varchar(50) NOT NULL,
  `sexe` varchar(50) NOT NULL,
  `programme_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_stagiaire`),
  KEY `fk_Stagiaire_Programme` (`programme_id`),
  CONSTRAINT `fk_Stagiaire_Programme` FOREIGN KEY (`programme_id`) REFERENCES `programme` (`id`) ON UPDATE CASCADE
);
