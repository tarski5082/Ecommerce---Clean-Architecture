CREATE DATABASE IF NOT EXISTS store;
USE store;



CREATE TABLE Localite(
    id INT PRIMARY KEY AUTO_INCREMENT,
    codePostal INT,
    ville VARCHAR(50),
    province VARCHAR(50)
);


CREATE TABLE AdresseFacturation(
    id INT PRIMARY KEY AUTO_INCREMENT, 
    rue VARCHAR(70),
    numero INT,
    boite VARCHAR(5),
    id_localite INT,
    FOREIGN KEY (id_localite) REFERENCES Localite(id)
);

CREATE TABLE AdresseLivraison(
    id INT PRIMARY KEY AUTO_INCREMENT,
    rue VARCHAR(70),
    numero INT,
    boite VARCHAR(5),
    id_localite INT,
    FOREIGN KEY (id_localite) REFERENCES Localite(id)
);


CREATE TABLE IF NOT EXISTS Users (
    Id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    Username VARCHAR(256) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    Nom TEXT,
    Prenom TEXT,
    IsAdmin BOOLEAN DEFAULT FALSE,
    IdFacturation INT,
    IdLivraison INT,
    CreatedAt DATETIME(6) DEFAULT (UTC_TIMESTAMP()),
    UpdatedAt DATETIME(6) DEFAULT (UTC_TIMESTAMP()) ON UPDATE CURRENT_TIMESTAMP(6),
    

);
