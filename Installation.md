# Prérequis

Avant de lancer le projet, les éléments suivants doivent être installés :

- **Node.js 20**
- **.NET 10 SDK**

## Vérifier les versions

```bash
node --v
dotnet --version
```
## Cloner le repertoire

```bash
git clone https://github.com/tarski5082/Ecommerce---Clean-Architecture
```

## Installer les dependances et la base de donnees

```bash
cd BackEnd
dotnet restore

cd Infrastructure
mysql -u root -p < db.sql
```
Entrer le mot de passe de votre base de donnees


```bash
cd ../..
cd FrontEnd
npm install
```
