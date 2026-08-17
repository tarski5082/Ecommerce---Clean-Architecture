# Prérequis

Avant de lancer le projet, les programmes suivants doivent être installés :

- **Node.js (version 20.11.1 ou supérieure)**
- **npm (généralement installé avec Node.js)**
- **.NET 10 SDK**
- **MySql**

## Vérifier les versions

```bash
node --v
```
```bash
dotnet --version
```
## Cloner le répertoire

```bash
git clone https://github.com/tarski5082/Ecommerce---Clean-Architecture
```
```bash
cd Ecommerce---Clean-Architecture
```

## Installer les dependances et la base de donnees

```bash
cd BackEnd
```
```bash
dotnet restore
```

```bash
cd Infrastructure
```
```bash
mysql -u root -p < db.sql
```
Entrer le mot de passe de votre base de donnees

Ouvrer le fichier appsettings.Development.json

Dans la chaine de caractere a cote de DefaultConnection: "Server=localhost;Database=store;Uid=root;Pwd=root;"
remplacer root a cote de Pwd par le mot de passe de votre root dans votre base de donnes.



```bash
cd ../..
```
```bash
cd FrontEnd
```
```bash
npm install
```

## Demarrer le projet

Aller a la racine du projet celle qui contient le dossier FrontEnd et BackEnd
Ouvrir le terminal a partir de la

```bash
cd BackEnd/Api
```

```bash
dotnet run
```


Ouvrer un second Terminal a la racine du projet 
```bash
cd FrontEnd/src/app
```
```bash
ng serve --open
```





| Utilisateur   | MotDePasse | Admin |
| -------       | -----------| :---: |
| merel         |  merel     |   ✅  |
| socrate       |  socrate   |    ❌ |
