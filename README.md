
# ZenGO3

Projet réalisé dans le cadre du cours **420-D60-CH - Développement d’Applications Avancé**.

ZenGO3 est une application WPF permettant à deux joueurs de jouer localement au jeu de Go sur un ordinateur Windows.

---

# TABLE DES MATIÈRES

* [Description du projet](#description-du-projet)
* [Technologies utilisées](#technologies-utilisées)
* [Architecture du projet](#architecture-du-projet)
* [Structure des dossiers](#structure-des-dossiers)
* [Code source](#code-source)
* [Livrables](#livrables)
* [Fonctionnalités principales](#fonctionnalités-principales)
* [Architecture MVVM](#architecture-mvvm)

---

# Description du projet

L’objectif du projet est de développer une application de jeu de Go possédant :

* une interface graphique WPF;
* une gestion locale des parties;
* un système de score automatique;
* une architecture basée sur le modèle MVVM;
* une sauvegarde des données en JSON.

Le projet met l’accent sur :

* la modularité;
* la séparation des responsabilités;
* la réutilisation des composantes;
* la clarté de l’interface utilisateur.

---

# Technologies utilisées

* C#
* .NET
* WPF
* XAML
* Architecture MVVM
* JSON

---

# Architecture du projet

Le projet utilise l’architecture **MVVM (Model - View - ViewModel)** afin de séparer :

* la logique métier;
* l’interface utilisateur;
* la gestion des données.

Cette approche facilite :

* la maintenance;
* les tests;
* la modularité;
* la lisibilité du code.

---

# Structure des dossiers

```text
ZenGO3/
│
├── Models/
├── Services/
├── ViewModels/
├── Views/
├── Livrables/
│
├── App.xaml
├── App.xaml.cs
├── AssemblyInfo.cs
├── ZenGo3.csproj
└── ZenGo3.sln
```

---

# Code source

Le code principal de l’application est situé dans les fichiers suivants :

```text
README.md

TestProject1/TestProject1.csproj
TestProject1/UnitTest1.cs

ZenGo3/App.xaml
ZenGo3/App.xaml.cs

ZenGo3/AssemblyInfo.cs

ZenGo3/Models/GameStats.cs

ZenGo3/Services/SaveToJson.cs

ZenGo3/ViewModels/GameViewVM.cs
ZenGo3/ViewModels/RelayCommand.cs
ZenGo3/ViewModels/Window1VM.cs

ZenGo3/Views/EndGameUC.xaml
ZenGo3/Views/EndGameUC.xaml.cs
ZenGo3/Views/GameView.xaml
ZenGo3/Views/GameView.xaml.cs
ZenGo3/Views/GridCanvas.xaml
ZenGo3/Views/GridCanvas.xaml.cs
ZenGo3/Views/SettingsUC.xaml
ZenGo3/Views/SettingsUC.xaml.cs
ZenGo3/Views/Styles/GlobalStyles.xaml
ZenGo3/Views/Window1.xaml
ZenGo3/Views/Window1.xaml.cs

ZenGo3/ZenGo3.csproj
ZenGo3/ZenGo3.sln

livrables/bilan_synthese.md
livrables/guide_utilisateur.md

.gitignore
```

---

# Livrables

Les livrables du projet sont situés dans le dossier suivant :

```text
ZenGO3/Livrables/
```

Fichiers disponibles :

```text
ZenGO3/Livrables/guide_utilisateur.md
ZenGO3/Livrables/bilan_synthese.md
```

---

# Fonctionnalités principales

L’application permet notamment :

* jouer une partie locale de Go;
* gérer les tours automatiquement;
* placer les pierres sur une grille interactive;
* passer un tour;
* abandonner une partie;
* calculer automatiquement les scores;
* afficher la fin de partie;
* sauvegarder les données dans un fichier JSON.

---

# Architecture MVVM

## Models

Contiennent les données et structures principales de l’application.

Exemple :

```text
Models/GameStats.cs
```

---

## Services

Contiennent les services utilitaires.

Exemple :

```text
Services/SaveToJson.cs
```

---

## ViewModels

Contiennent la logique de l’application et les commandes.

Exemples :

```text
ViewModels/GameViewVM.cs
ViewModels/RelayCommand.cs
ViewModels/Window1VM.cs
```

---

## Views

Contiennent les interfaces graphiques XAML.

Exemples :

```text
Views/GameView.xaml
Views/SettingsUC.xaml
Views/EndGameUC.xaml
Views/GridCanvas.xaml
```

---

# Auteur

Projet réalisé dans le cadre du cours :

**420-D60-CH - Développement d’Applications Avancé**
CÉGEP de Chicoutimi
