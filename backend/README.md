# Projet de Web API avec .NET

## Description du Projet

Cette API web, développée en ASP.NET Core, permet de gérer des équipes de football et des championnats. Utilisant Entity Framework Core pour la gestion de la base de données (alwaysData), elle offre des endpoints sécurisés par une authentification JWT (JSON Web Token) et une gestion fine des autorisations via des rôles d'utilisateur. Le projet suit une architecture RESTful et est conçu pour être évolutif.

Les technologies utilisées incluent ASP.NET Core, Entity Framework Core et JWT pour l'authentification. La gestion des utilisateurs et des rôles est assurée par ASP.NET Identity, permettant une sécurisation des accès aux différentes ressources de l'API.

## Objectifs du Projet

L'objectif de cette API est de fournir une solution simple pour gérer des entités liées au football, à savoir :

- Gestion des équipes de football : Création, mise à jour, consultation et suppression d'équipes, avec des champs tels que le nom de l'équipe et le pays.
- Gestion des championnats : Création, mise à jour, consultation et suppression des championnats, incluant le nom et le niveau du championnat.
- Authentification et autorisation : Implémentation d'un système d'authentification JWT, avec des rôles d'utilisateur prédéfinis (Admin, Manager, User), permettant de restreindre ou autoriser les actions sur les équipes et championnats selon le rôle.
    - Admin : Accès complet (CRUD pour toutes les entités).
    - User : Accès en lecture seule.


## Pré-requis

Avant de commencer, assurez-vous d'avoir installé :
- **Terraform** : Pour la gestion de l'infrastructure.
- **Un compte AWS** : Pour déployer les services requis.

## Instructions de Déploiement

1. **Cloner le dépôt GitHub** :
   ```bash
   git clone https://github.com/Borloo/api-dotnet.git
   ```
2. **Ouvrir le projet avec Visual Studio** 

3. **Exécuter le projet**

4. 


## Problèmes rencontrés

#### Déploiement sur Azure

Nous n'avons malheureusement pas pu effectuer le déploiement de l'application sur Azure car les comptes étudiants de l'écôle prévus à cet effet ne fonctionnait pas (à cause du changement des adresses mail). Le déploiement sur Azure nécessitait donc d'avoir un abonnement et donc des accès payants. Nous nous excusons pour ce soucis qui a empêché de mener à bien à 100% ce projet.

## Réalisateurs

Ce projet a été réalisé par deux étudiants en Master M1IL à l'IPI Blagnac du Campus IGS :

- Maxime ETCHEVERRIA
- Julien DEVIENNE-OUSMER

## Remerciements

Nous tenons à exprimer nos remerciements à notre enseignant, Thomas Bureau Du Colombier, pour sa formation tout au long de ce module. :rocket:
