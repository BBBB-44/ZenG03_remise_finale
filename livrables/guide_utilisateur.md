# ZenGo3 - Dév. App. Avancé | 420-D60-CH | Gr. 01

<img width="100%" alt="ZenGo" src="https://github.com/user-attachments/assets/9130bb60-f3d9-4bf7-872d-439315fc52b1" />


# LIVRABLE 4 : GUIDE UTILISATEUR

---

# TABLE DES MATIÈRES

* [1. INTRODUCTION](#1-introduction)

  * [1.1 Contexte du projet](#11-contexte-du-projet)
  * [1.2 Fonction](#12-fonction)
  * [1.3 Public cible](#13-public-cible)
  * [1.4 Présentation de l’application](#14-présentation-de-lapplication)
  * [1.5 Objectifs principaux](#15-objectifs-principaux)
  * [1.6 Fonctionnalités principales](#16-fonctionnalités-principales)
  * [1.7 Portée et limites de l’application](#17-portée-et-limites-de-lapplication)

* [2. GUIDE D’INSTALLATION](#2-guide-dinstallation)

  * [2.1 Prérequis](#21-prérequis)
  * [2.2 Étapes d’installation](#22-étapes-dinstallation)

    * [2.2.1 Télécharger l’application](#221-télécharger-lapplication)
    * [2.2.2 Extraire le dossier compressé](#222-extraire-le-dossier-compressé)
    * [2.2.3 Lancer l’application](#223-lancer-lapplication)

* [3. VUE D’ENSEMBLE](#3-vue-densemble)

  * [3.1 Logique de fonctionnement](#31-logique-de-fonctionnement)

* [4. ACCUEIL ET RÉGLAGES DE LA PARTIE](#4-accueil-et-réglages-de-la-partie)

  * [4.1 Page d’accueil](#41-page-daccueil)
  * [4.2 Réglages de la partie](#42-réglages-de-la-partie)

* [5. PAGE DU JEU](#5-page-du-jeu)

  * [5.1 Boutons d’actions](#51-boutons-dactions)
  * [5.2 Affichage des informations noir et blanc](#52-affichage-des-informations-noir-et-blanc)
  * [5.3 Grille du jeu](#53-grille-du-jeu)

* [6. PAGE DE FIN DE PARTIE](#6-page-de-fin-de-partie)

  * [6.1 Affichage des informations de la partie](#61-affichage-des-informations-de-la-partie)
  * [6.2 Boutons d’actions](#62-boutons-dactions)

* [7. SAUVEGARDE DANS UN FICHIER JSON](#7-sauvegarde-dans-un-fichier-json)

* [8. PROBLÈMES POSSIBLES](#8-problèmes-possibles)

* [9. CONTACT](#9-contact)

---

# 1. INTRODUCTION

## 1.1 Contexte du projet

Dans le cadre du cours **420-D60-CH - Développement d’Applications Avancé**, ce projet vise à appliquer les concepts d’analyse, de conception et de développement d’une application complète.

### Définition des utilisateurs

Deux joueurs souhaitant jouer au jeu de Go sur une interface locale Windows. Les utilisateurs doivent connaître les règles de base du jeu ou être prêts à les apprendre via l'application.

### Exemple de situation utilisateur

Deux étudiants ou amateurs du jeu de Go souhaitent s'affronter en local sur un même ordinateur. L'un joue les pierres noires, l'autre les pierres blanches, selon les règles officielles.

---

## 1.2 Fonction

Dans le cadre du cours **420-D60-CH - Développement d’Applications Avancé**, ce projet consiste à concevoir et développer une application permettant de jouer au Go sur un poste informatique.

Ce document regroupe :

* le guide d’installation, permettant de mettre en service l’application;
* le guide d’utilisation, permettant de comprendre et utiliser les fonctionnalités.

Ce document a pour objectif de permettre à un utilisateur, même sans connaissances techniques, d’installer et d’utiliser l’application de manière autonome.

---

## 1.3 Public cible

Ce guide s’adresse principalement à des utilisateurs :

* finaux souhaitant jouer au Go entre amis;
* ayant peu ou pas de connaissances techniques;
* désirant comprendre le fonctionnement de l’application.

Le document est rédigé de manière claire et structurée afin d’être accessible à tous.

---

## 1.4 Présentation de l’application

L’application permet de gérer efficacement une partie de Go.

Elle offre une solution centralisée pour :

* personnaliser les réglages;
* jouer une partie;
* calculer automatiquement les points;
* afficher le résultat final de la partie.

L’application met l’accent sur :

* la simplicité d’utilisation;
* la clarté des informations affichées;
* l’automatisation des calculs;
* une expérience de jeu fluide et intuitive.

---

## 1.5 Objectifs principaux

Les principaux objectifs de l’application sont les suivants :

* permettre à deux joueurs de jouer au Go localement;
* offrir une interface simple et intuitive;
* automatiser le calcul des points en fin de partie;
* respecter les règles principales du jeu de Go;
* sauvegarder les données de partie dans un fichier local.

---

## 1.6 Fonctionnalités principales

L’application propose plusieurs fonctionnalités importantes :

* création rapide d’une partie;
* sélection des paramètres de jeu;
* affichage du plateau interactif;
* gestion automatique des tours;
* possibilité de passer son tour;
* abandon de la partie;
* calcul automatique du score;
* affichage du gagnant;
* sauvegarde des parties au format JSON.

---

## 1.7 Portée et limites de l’application

Les données sont stockées localement et ne sont pas synchronisées en ligne.

### Limites actuelles

* aucune fonctionnalité multijoueur en ligne;
* aucune connexion Internet requise;
* aucune gestion de compte utilisateur;
* sauvegarde locale uniquement;
* l’application est conçue principalement pour un usage local.

### Portée du projet

L’application est destinée à un usage personnel ou en petit groupe, bien que celle-ci ne devrait conceptuellement pas représenter de problème en grand groupe.

---

# 2. GUIDE D’INSTALLATION

## 2.1 Prérequis

Avant d’installer l’application, assurez-vous que votre environnement respecte les exigences suivantes :

### Système d’exploitation

* Windows 10 ou version plus récente

### Logiciels requis

* Aucun accès Internet requis après installation

### Matériel

* Minimum 4 Go de RAM recommandé
* 200 Mo d’espace disque disponible

---

## 2.2 Étapes d’installation

Suivez à la lettre les étapes suivantes afin d’utiliser l’application. 

### 2.2.1 Télécharger l’application

Rendez-vous sur le répertoire racine « main » du projet GitHub et téléchargez le tout.

### 2.2.2 Extraire le dossier compressé

Ouvrir le dossier .zip et appuyer sur extraire tout. Sélectionnez un emplacement facile d’accès que vous pourrez retrouver.

### 2.2.3 Lancer l’application

Lancer l'application. La création du fichier de sauvegarde se fera automatiquement à la fin de chaque partie.

---

# 3. VUE D’ENSEMBLE

L’application ZenGo3 est structurée autour d’un déroulement simple permettant à deux joueurs de jouer une partie complète de Go sur le même ordinateur.

Le joueur peut :

1. accéder à la page d’accueil;
2. configurer les paramètres de la partie;
3. lancer une nouvelle partie;
4. jouer sur la grille interactive;
5. terminer la partie;
6. consulter les résultats;
7. sauvegarder les données de partie.

---

## 3.1 Logique de fonctionnement

Le fonctionnement général de l’application repose sur les principes suivants :

* alternance automatique des tours;
* validation des coups joués;
* mise à jour dynamique du plateau;
* calcul automatique des territoires et points;
* gestion des actions de fin de partie.

Le système permet également de détecter :

* les captures de pierres;
* les tours passés;
* la fin de partie;
* le gagnant final.

---

# 4. ACCUEIL ET RÉGLAGES DE LA PARTIE

## 4.1 Page d’accueil

<img width="100%" alt="598198186-8e7d5f85-7955-48c7-ad67-9bf15d18d511" src="https://github.com/user-attachments/assets/9fc822b2-ea45-4b03-b35e-a9dc6074809e" />


La page d’accueil constitue le point d’entrée principal de l’application.

Elle permet notamment :

* de démarrer une nouvelle partie;
* d’accéder aux réglages;
* de consulter les informations générales du jeu.

L’interface est conçue pour être simple et intuitive afin de faciliter la prise en main rapide de l’application.

---

## 4.2 Réglages de la partie

Avant le début de la partie, les joueurs peuvent configurer différents paramètres :

* taille du plateau;
* règles de partie;
* nom des joueurs;
* options d’affichage.

Ces réglages permettent de personnaliser l’expérience de jeu selon les préférences des utilisateurs.

---

# 5. PAGE DU JEU

<img width="100%" alt="598198806-968b5870-b20d-484d-9ad0-447ad35a3875" src="https://github.com/user-attachments/assets/1d9bea19-f597-453c-93bf-c95c23418d93" />


## 5.1 Boutons d’actions

Durant une partie, plusieurs boutons d’actions sont disponibles :

### Passer le tour

Permet au joueur actuel de passer son tour sans jouer de pierre.

Lorsque les deux joueurs passent leur tour consécutivement, la partie peut se terminer automatiquement.

### Abandonner

Permet à un joueur d’abandonner immédiatement la partie.

L’autre joueur est alors déclaré gagnant automatiquement.

Notes : La validation du joueur est néscessaire, afin d'éviter un abandon accidentel.

<img width="100%" alt="598199629-57f2b859-86aa-4553-acb3-3e3b5dd10517" src="https://github.com/user-attachments/assets/0ef07e22-c495-40d2-9e04-36ec256cd520" />


---

## 5.2 Affichage des informations noir et blanc

L’interface affiche en permanence les informations des deux joueurs :

* couleur du joueur;
* score actuel;
* joueur actif;
* nombre de captures;
* état de la partie.

Cela permet aux joueurs de suivre facilement l’évolution de la partie.

---

## 5.3 Grille du jeu

La grille représente le plateau principal du jeu de Go.

Fonctionnalités de la grille :

* placement des pierres;
* détection des intersections;
* affichage dynamique des coups;
* gestion des captures;
* mise à jour automatique après chaque tour.

Le plateau réagit directement aux actions des joueurs via les clics souris.

---

# 6. PAGE DE FIN DE PARTIE

<img width="100%" alt="598199771-4d83c4a6-32b1-4daa-9689-faf8b1cadc04" src="https://github.com/user-attachments/assets/7a4ee212-41e3-4e7a-b71d-68127d8af428" />


## 6.1 Affichage des informations de la partie

À la fin d’une partie, l’application affiche :

* le score final des joueurs;
* le nombre de territoires;
* le nombre de captures;
* le gagnant;
* les statistiques principales de la partie.

Cette page permet de résumer rapidement le déroulement de la partie.

---

## 6.2 Boutons d’actions

### Rejouer

Permet de démarrer immédiatement une nouvelle partie avec les mêmes paramètres.

### Retour au menu

Permet de revenir à la page d’accueil principale de l’application.

---

# 7. SAUVEGARDE DANS UN FICHIER JSON

L’application permet de sauvegarder certaines données de partie dans un fichier au format JSON.

Les informations pouvant être sauvegardées incluent :

* noms des joueurs;
* score final;
* taille du plateau;
* gagnant de la partie.

Cette sauvegarde permet :

* de conserver une trace des parties jouées;
* d’effectuer des analyses ultérieures;
* de faciliter le débogage de l’application.

Les fichiers JSON sont stockés localement sur l’ordinateur de l’utilisateur.

---

# 8. PROBLÈMES POSSIBLES

Voici quelques problèmes pouvant être rencontrés lors de l’utilisation de l’application :

| Problème                       | Cause possible                    | Solution                              |
| ------------------------------ | --------------------------------- | ------------------------------------- |
| L’application ne démarre pas   | Fichiers manquants                | Réinstaller l’application             |
| La grille ne répond pas        | Erreur temporaire                 | Redémarrer l’application              |
| La sauvegarde échoue           | Permissions insuffisantes         | Vérifier les droits d’écriture        |
| L’affichage est incorrect      | Résolution d’écran incompatible   | Ajuster la résolution Windows         |
| Les scores semblent incorrects | Mauvaise compréhension des règles | Vérifier les règles officielles du Go |

---

# 9. CONTACT

Pour toute question, problème technique ou suggestion d’amélioration :

Contactez-nous :
* Par téléphone au 1-800-105-1045;
* Par courriel au ZenGo3@ZenGo3.com;
* Par message envers Tommy-Frédéric Bernaquez

