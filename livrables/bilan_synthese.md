# ZenGo3 - Dév. App. Avancé | 420-D60-CH | Gr. 01

<img width="100%" alt="ZenGo" src="https://github.com/user-attachments/assets/974b4edb-f927-404a-bf9a-c9dfd39d732f" />


# LIVRABLE 5 : BILAN

---

# TABLE DES MATIÈRES

* [1. PROBLÈMES RENCONTRÉS](#1-problèmes-rencontrés)

* [2. FACTEURS CLÉS](#2-facteurs-clés)

  * [2.1 Compréhension du besoin client](#21-compréhension-du-besoin-client)
  * [2.2 Utilisation des User Controls](#22-utilisation-des-user-controls)
  * [2.3 Utilisation des styles](#23-utilisation-des-styles)

* [3. AMÉLIORATION CONTINUE](#3-amélioration-continue)

  * [3.1 Diagramme de classes plus exhaustif](#31-diagramme-de-classes-plus-exhaustif)
  * [3.2 Éviter certaines méthodes de travail risquées](#32-éviter-certaines-méthodes-de-travail-risquées)

* [4. BONS COUPS](#4-bons-coups)

---

# 1. PROBLÈMES RENCONTRÉS

Au cours du développement du projet, plusieurs difficultés techniques et organisationnelles ont été rencontrées.

## Portage de l’application console vers WPF

Le développement initial en format console s’est avéré plus complexe que prévu lors du portage vers une interface WPF.

Dans une application console, l’affichage ne nécessite pas la gestion complète des interactions utilisateur sur une grille graphique. Lors de la transition vers WPF, plusieurs éléments ont dû être repensés :

* gestion des événements utilisateurs;
* interaction avec la grille graphique;
* rafraîchissement dynamique de l’interface;
* liaison entre les données et l’affichage.

Cette transition a demandé davantage de temps de développement et de restructuration que prévu initialement.

---

## Validation insuffisante avec le client

Les validations avec le client n’ont pas été effectuées assez fréquemment durant certaines phases du projet.

Cette situation est principalement liée aux difficultés rencontrées lors du portage vers WPF, ce qui a ralenti certaines démonstrations fonctionnelles.

Cela a eu pour conséquence :

* un risque plus élevé d’écart avec les attentes;
* certaines fonctionnalités développées sans validation immédiate;
* des ajustements tardifs dans le développement.

---

## Séparation des composantes avec MVVM

La mise en place d’une architecture respectant correctement le modèle **MVVM (Model-View-ViewModel)** a représenté un défi important.

Les principales difficultés concernaient :

* la séparation claire des responsabilités;
* l’organisation des classes;
* les communications entre les vues et les données;
* la réduction du couplage dans le code.

Cette problématique a nécessité plusieurs ajustements au cours du développement.

---

## Développement de fonctionnalités non essentielles

Certaines fonctionnalités ont été développées alors qu’elles n’étaient pas essentielles au bon fonctionnement de l’application.

Par exemple :

* la création d’un fichier JSON formaté afin de faciliter la lecture des données de la grille;
* certaines optimisations visuelles secondaires.

Bien que pertinentes pour le développement et le débogage, ces fonctionnalités ont augmenté le temps de réalisation du projet.

---

## Diagramme de classes incomplet

Le diagramme de classes réalisé au début du projet manquait de détails.

Certains éléments importants n’étaient pas suffisamment représentés :

* relations entre les composantes;
* responsabilités exactes des classes;
* structure MVVM;
* flux de données principaux.

Cela a rendu certaines étapes du développement plus difficiles à planifier.

---

# 2. FACTEURS CLÉS

## 2.1 Compréhension du besoin client

La compréhension du besoin client a été un élément essentiel du projet.

Cela a permis :

* de mieux cibler les fonctionnalités importantes;
* d’éviter le développement excessif de fonctionnalités inutiles;
* de maintenir un objectif clair durant le développement.

Une meilleure compréhension du besoin aide également à réduire les pertes de temps et les modifications tardives.

---

## 2.2 Utilisation des User Controls

L’utilisation des **User Controls** a grandement facilité le développement de l’interface.

Cette approche a permis :

* de réutiliser les composantes;
* de rendre le code plus modulaire;
* de simplifier la maintenance;
* d’améliorer l’organisation générale du projet.

Les composantes de l’interface étaient ainsi plus faciles à modifier indépendamment.

---

## 2.3 Utilisation des styles

L’utilisation des styles WPF a permis d’uniformiser l’apparence de l’application.

Les styles ont offert plusieurs avantages :

* cohérence visuelle;
* réduction du code dupliqué;
* personnalisation centralisée de l’interface;
* amélioration de la lisibilité du XAML.

Cette approche a contribué à rendre l’interface plus professionnelle et plus facile à maintenir.

---

# 3. AMÉLIORATION CONTINUE

## 3.1 Diagramme de classes plus exhaustif

Pour les futurs projets, il serait préférable de produire un diagramme de classes plus détaillé dès le début du développement.

Un diagramme plus exhaustif permettrait :

* une meilleure planification;
* une meilleure séparation des responsabilités;
* une compréhension plus claire de l’architecture;
* une réduction des ajustements en cours de projet.

---

## 3.2 Éviter certaines méthodes de travail risquées

Il serait également préférable d’éviter certaines approches de développement pouvant introduire des risques importants.

Par exemple :

* éviter de commencer un projet en console avant un portage complet vers WPF;
* utiliser directement les technologies finales du projet;
* limiter les changements majeurs d’architecture en cours de développement.

Cela permettrait de réduire les imprévus et les retards.

---

# 4. BONS COUPS

Plusieurs bonnes décisions ont contribué positivement au projet.

## Utilisation des User Controls

L’utilisation des **User Controls** pour chaque composante importante de l’application a permis :

* une meilleure modularité;
* une réutilisation efficace du code;
* une maintenance simplifiée;
* une structure plus claire.

---

## Utilisation des styles

L’utilisation des styles a permis :

* une interface cohérente;
* une personnalisation simplifiée;
* une amélioration visuelle générale;
* une meilleure organisation du code XAML.

Ces choix techniques ont contribué à améliorer la qualité globale du projet.
