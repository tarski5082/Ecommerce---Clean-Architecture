CREATE DATABASE IF NOT EXISTS store;
USE store;

-- =====================================================
-- Suppression des tables
-- =====================================================

DROP TABLE IF EXISTS Article;
DROP TABLE IF EXISTS Panier;
DROP TABLE IF EXISTS Utilisateur;
DROP TABLE IF EXISTS Produit;
DROP TABLE IF EXISTS Adresse;
DROP TABLE IF EXISTS Categorie;
DROP TABLE IF EXISTS Localite;

-- =====================================================
-- Localite
-- =====================================================

CREATE TABLE Localite (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CodePostal INT,
    Ville VARCHAR(50),
    Province VARCHAR(50)
);

-- =====================================================
-- Categorie
-- =====================================================

CREATE TABLE Categorie (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nom VARCHAR(80)
);

-- =====================================================
-- Adresse
-- =====================================================

CREATE TABLE Adresse (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Rue VARCHAR(70),
    Numero INT,
    Boite VARCHAR(5),
    IdLocalite INT,
    FOREIGN KEY (IdLocalite) REFERENCES Localite(Id)
);

-- =====================================================
-- Produit
-- =====================================================

CREATE TABLE Produit (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nom VARCHAR(250),
    Inventaire INT UNSIGNED NOT NULL,
    PrixUnitaire DECIMAL(10,2),
    ImageUrl VARCHAR(2048),
    IdCategorie INT,
    FOREIGN KEY (IdCategorie) REFERENCES Categorie(Id)
);

-- =====================================================
-- Utilisateur
-- =====================================================

CREATE TABLE Utilisateur (
    Id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    Username VARCHAR(256) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    Nom TEXT,
    Prenom TEXT,
    IsAdmin BOOLEAN DEFAULT FALSE,
    IdFacturation INT DEFAULT NULL,
    IdLivraison INT DEFAULT NULL,
    CreatedAt DATETIME(6) DEFAULT (UTC_TIMESTAMP()),
    UpdatedAt DATETIME(6) DEFAULT (UTC_TIMESTAMP())
        ON UPDATE CURRENT_TIMESTAMP(6),

    FOREIGN KEY (IdFacturation) REFERENCES Adresse(Id),
    FOREIGN KEY (IdLivraison) REFERENCES Adresse(Id)
);

-- =====================================================
-- Panier
-- =====================================================

CREATE TABLE Panier (
    Id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    UserId CHAR(36),
    Etat ENUM('en attente', 'effectue')
        NOT NULL DEFAULT 'en attente',
    Livraison ENUM(
        'en attente',
        'confirme',
        'en preparation',
        'expedie',
        'livree'
    ) NOT NULL DEFAULT 'en attente',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (UserId)
        REFERENCES Utilisateur(Id)
        ON DELETE CASCADE
);

-- =====================================================
-- Article
-- =====================================================

CREATE TABLE Article (
    Id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    ProduitId INT NOT NULL,
    Quantite INT UNSIGNED NOT NULL,
    PanierId CHAR(36) NOT NULL,

    FOREIGN KEY (ProduitId)
        REFERENCES Produit(Id),

    FOREIGN KEY (PanierId)
        REFERENCES Panier(Id)
        ON DELETE CASCADE
);

-- =====================================================
-- Données : Categories
-- =====================================================

INSERT INTO Categorie (Id, Nom) VALUES
(1, 'Informatique'),
(2, 'Téléphones'),
(3, 'Audio'),
(4, 'Gaming'),
(5, 'Accessoires');

-- =====================================================
-- Données : Produits
-- =====================================================

INSERT INTO Produit
    (Id, Nom, Inventaire, PrixUnitaire, ImageUrl, IdCategorie)
VALUES
('Lenovo Yoga Slim 7', 25, 750,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/fc/f5/a6/27719164/3756-1.jpg', 1),

('MacBook Air M3', 15, 1300,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/72/85/63/23299442/1540-1/tsp20260218220918/Apple-MacBook-Air-13-6-512Go-D-16-Go-RAM-Puce-M3-CPU-8-coeurs-GPU-10-coeurs-Minuit.jpg', 1),

('Samsung Galaxy Tab S10 FE', 30, 750,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/82/57/ab/28006274/1540-1/tsp20260619155057/Samsung-Galaxy-Tab-S10-FE-tablette-android-ecran-10-9-pouces-27-7-cm-WiFi-5-Bluetooth-5-3-RAM-12-Go-stockage-256-Go-camera-principale-13-mpx-batterie-8000-mAh-gris.jpg', 1),

('Clavier mécanique Logitech G413', 40, 90,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/73/93/41/21074803/1540-1/tsp20260220031329/Clavier-Gaming-filaire-Azerty-Logitech-G-G413-SE-Noir.jpg', 5),

('Souris Logitech G502', 50, 80,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/4e/2a/30/19933774/1505-1/tsp20260601032040/Souris-Gaming-Sans-Fil-Logitech-G502-X-Lightspeed-pour-PC-ou-Mac-Noir.jpg', 5),

('iPhone 15', 20, 800,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/a8/c7/16/1540-2/tsp20260428173815/Apple-iPhone-15-6-1-128-Go-bleu-Reconditionne-avec-Batterie-neuve.jpg#88c92110-066a-4b38-b6cc-7875a9b0ce2e', 2),

('Samsung S24', 18, 900,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/a6/f6/5a/22738598/1540-1/tsp20260319135101/Smartphone-Samsung-Galaxy-S24-6-2-5G-Nano-SIM-256-Go-Noir.jpg', 2),

('Google Pixel 11', 12, 800,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/24/34/c6/29766692/1540-1/tsp20260813091724/Smartphone-Google-Pixel-11-6-3-OLED-5G-Double-SIM-256-Go-Noir-Volcanique.jpg', 2),

('Casque Sony WH-1000XM5', 22, 350,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/d9/63/25/19227609/1540-1/tsp20260612111322/Casque-audio-arceau-sans-fil-Sony-WH1000XM5-noir-a-reduction-de-bruit.jpg', 3),

('AirPods Pro 2', 35, 280,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/0a/e4/06/17228810/1540-1/tsp20260620100141/Apple-AirPods-Pro-2eme-generation-Blanc-avec-boitier-de-charge-MagSafe-Lightning-Ecouteurs-sans-fil-True-Wirele.jpg', 3),

('Enceinte JBL Flip 6', 28, 130,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/d2/2d/06/17182162/1540-1/tsp20260523184546/Enceinte-portable-etanche-sans-fil-Bluetooth-JBL-Flip-6-Bleu.jpg', 3),

('PlayStation 5', 10, 550,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/2d/8d/b7/28806445/1540-1/tsp20260730103933/Console-Sony-PS5-Slim-Edition-Standard-Blanc-et-Noir.jpg', 4),

('Manette Xbox Wireless Controller', 25, 65,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/ff/be/33/20168447/1540-1/tsp20260530085138/Manette-sans-fil-Microsoft-Xbox-Elite-Series-2-Core-Blanc.jpg', 4),

('Nintendo Switch OLED', 14, 350,
 'https://static.fnac-static.com/multimedia/Images/FR/MDM/91/ea/f6/16181905/1540-1/tsp20260619170440/Nintendo-Switch-modele-OLED-avec-station-d-accueil-et-manettes-Joy-Con-blanches.jpg', 4),

('Casque Gaming HyperX Cloud II', 30, 90,
 'https://static.fnac-static.com/multimedia/Images/60/36/9D/14/21615456-1505-1540-1/tsp20260527082658/Casque-PC-sans-fil-gaming-HyperX-Cloud-Stinger-2-Noir.jpg', 4);




