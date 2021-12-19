# 2D GAME

# Requirements Specification

> Made by MAD_P Team

# Table of Contents

- [2D GAME](#2d-game)
- [Requirements Specification](#requirements-specification)
- [Table of Contents](#table-of-contents)
- [Introduction](#introduction)
  - [Purpose of this Document](#purpose-of-this-document)
  - [Purpose of the System](#purpose-of-the-system)
- [Project Overview](#project-overview)
  - [High Level Overview](#high-level-overview)
- [Product Requirements](#product-requirements)
  - [Functional Requirements](#functional-requirements)
    - [Start Game](#start-game)
    - [Card Deck](#card-deck)
    - [Spawning](#spawning)
    - [UI](#ui)
    - [Mana System and Mana Bar](#mana-system-and-mana-bar)
    - [Character](#character)
    - [Structure](#structure)
    - [Gameplay](#gameplay)
    - [Awards](#awards)
    - [Store](#store)
  - [Non-Functional Requirements](#non-functional-requirements)
    - [Availability](#availability)
    - [Game Installation](#game-installation)
    - [Usability](#usability)
    - [Reliability](#reliability)
    - [Performance](#performance)
    - [Compatibility](#compatibility)
    - [Security](#security)
    - [System requirements](#system-requirements)

# Introduction

## Purpose of this Document

The purpose of this document is to provide the necessary background information, requirements, system scope, and high-level technical specifications about the design, development, and implementation processes of a 2D Strategy Game RushNDestroy.

## Purpose of the System

The purpose of the game is for the user to have fun while improving their logical thinking and strategy developing skills.

# Project Overview

## High Level Overview

We are working on a 2D strategy game in which the user can have fun and at the same time enhance their cognitive abilities. This game's play-style is "Player vs. AI" (singleplayer). The goal of the game is to destroy your enemy's main structure whether it would be a castle, a fortresses or any other type of stronghold, while defending your own castle or fortress from enemy attacks. The destruction of the enemy base counts as a direct win. Users will be able to choose their own ways to achieve this. However, it is advised to think about every move you make since enemies will be implemented with various AI features such as path finding, target prioritization, spawning characters which would lead AI to as fast destruction of your base as possible. Because of that, resource management will be vital to your survival. In the game stage progression will be implemented. This means once you complete the stage you are currently in, the stages onwards will become bigger with more paths, obstacles and other environmental challenges such as swamps, etc. Players will be able to get new types of characters, as well as upgrade currently owned ones with currency they get from winning battles. This game is targeted for people who like to develop their strategy skills and managing given character cards as efficiently as possible. Targeted age group is any age group, because this game does not have any explicit graphical content.

# Product Requirements

## Functional Requirements

- Functional requirements are written in a manner of how the gameplay should flow starting from the beggining to the end;
- Abstract template: `type of subject` should `carry out a task` / `to do something`;

- Types of subjects:
  - Player (The person playing the game);
  - Character (An entity that is spawned by the player);
  - Structure (An entity that is already in the gameplay and is not controlled by the player);
  - Entity (Either a character or a sctructre both of which should act the similary);
  - Gameplay (The interval of time during which the player is playing the game).

### Start Game

- The player should be able to press the "Play" to start the match;
- The player should be able to press on level icon to select which level has to be loaded.

### Card Deck

- The player should be able to select a card by pressing and holding on the icon;
- The selected card should be brought up after selecting;
- The card should be removed if the character was spawned;
- New card should be placed in the old card's slot;
- The card should return to the deck if it was not used.

### Spawning

- The character should spawn if the player has enough mana to spawn a desired character;
- The character should not spawn if the player does not have enough mana to spawn the desired character;
- The player should be able to spawn a character in a dedicated spawn area.

### UI

- The player should be able to press "Play" to continue to Save Selection menu;
- The player should be able to press "Quit" to exit the game;
- The player should be able to press the "Start new game" to start playing the game from the beginning;
- The player should be able to press the "Continue" to continue the progress;
- The player should be able to press on pause "( II )" button to stop the gameplay and be brough to pause menu;
- The player should be able to press on "Resume" button to resume the gameplay;
- The player should be able to go back to the list of levels by pressing "Back To Levels" button;
- The player should be able to go back to main menu by pressing "Back To Main Menu" button.

### Mana System and Mana Bar

- The mana bar should show the exact amount of mana the player has;
- The amount of mana should be substacted accordingly to the amount that it costs to spawn a character;
- The mana should be continuously regenerated during the gameplay;
- The amount of mana that it costs to spawn a character should not be substracted if the spawn was unsuccessful.

### Character

- The character should be an individualy thinking unit that is not relying on any other character in any way;
- The character should have its mana price in order to be spawned;
- The character should start acting accordingly after being sucessfully spawned;
- The character should seek for enemy characters or structures after being spawned and change its position accordingly;
- The character should start inflicting damage if it approached enemy entity within its attack range;
- The character should start receiving damage if it is approached by enemy entity;
- The character should die if it received more damage through a period of time than the predefined health it had;
- The character should seek for enemies again after its primary target is killed or no longer in range;
- The character should be removed from the gameplay if it is considered dead.

### Structure

- The structure should seek for enemy characters;
- The structure should select an enemy character to attack;
- The structure should inflict damage to the selected enemy if it within its range;
- The structure should reasign its primary target if the previous target is considered dead;
- The structure should receive damage from enemy character if it is within enemy character's attack range;
- The structure should be destroyed if it has received more damage through a period of time than the predefined health it had;
- The structure should be removed from the gameplay if it is considered dead. 

### Gameplay

- The gameplay should begin when the player selects the level desired to play in;
- The gameplay should continue unless:
  - Player has intervened (Paused or Exited the game);
  - Main enemy or player structure (a castle) was destroyed;
  - Player does not resume the game from the pause screen.

### Awards 

- The player should receive coins and trophies if the game was won;
- The player should not receive coins if the game was lost;
- The player should lose trophies if the game was lost;
- The player should not recieve or lose coins or trophies if the game was tied.

### Store

- The player should be able to upgrade the characters with coins received during the match;
- The player should be able to acquire new characters with obtained coins during the match'
- The player should be able to view information about any character by pressing "Info" button in either upgrade or buy menu.

## Non-Functional Requirements

### Availability

- The application should work after installing without any additional files or software besides the original.

### Game Installation

- Instructions for installing the game should be provided in the README file which is located the root of the project.

### Usability

- Free to use for everyone installing it;
- Easy to use without any additional knowledge about the software required.

### Reliability

- The application should not have any issues or bugs which would result in breaking, crashing or damaging the user's device in any way.

### Performance

- The application should be able to run on relatively weak devices without any high end components.

### Compatibility

- The application should run on Windows, Linux and MacOS operating systems.

### Security

- The application should not collect any information from the player.

### System requirements

- A computer (desktop or laptop) that has enough hardware resources to run a browser and has a dedicated or integrated graphics processing unit (GPU);
- Has Windows 10, Linux or on MacOS 10.9 Maverick installed as an operating system. </br>
**Note: a computer must have a CPU and a GPU in order to run the game. Regarding OS, the game might run on older versions of Windows/Linux/MacOS**
