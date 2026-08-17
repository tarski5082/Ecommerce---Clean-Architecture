# Prérequis

Avant de lancer le projet, les programmes suivants doivent être installés :

- **Node.js (version 20.11.1 ou supérieure)**
- **npm (généralement installé avec Node.js)**
- **.NET 10 SDK**
- **MySql**

## Vérifier les versions

```bash
node --v
dotnet --version
```
## Cloner le repertoire

```bash
git clone https://github.com/tarski5082/Ecommerce---Clean-Architecture
cd Ecommerce---Clean-Architecture
```

## Installer les dependances et la base de donnees

```bash
cd BackEnd
dotnet restore

cd Infrastructure
mysql -u root -p < db.sql
```
Entrer le mot de passe de votre base de donnees

Ouvrer le fichier appsettings.Development.json

Dans la chaine de caractere a cote de DefaultConnection: "Server=localhost;Database=store;Uid=root;Pwd=root;"
remplacer root a cote de Pwd par le mot de passe de votre root dans votre base de donnes.



```bash
cd ../..
cd FrontEnd
npm install
```

## Demarrer le projet

Aller a la racine du projet celle qui contient le dossier FrontEnd et BackEnd
Ouvrir le terminal a partir de la

```bash
cd BackEnd/Api
dotnet run
```


Ouvrer un second Terminal a la racine du projet 
```bash
cd FrontEnd/src/app
ng serve --open
```





| Utilisateur   | MotDePasse | Admin |
| -------       | -----------| :---: |
| merel         |  merel     |   ✅  |
| socrate       |  socrate   |    ❌ |
