# 2D GAME

# Requirements Specification

> Made by MAD_P Team

# Table of Contents #

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
    - [1. Game mechanics](#1-game-mechanics)
    - [2. Game characters and structures](#2-game-characters-and-structures)
    - [3. Health system](#3-health-system)
    - [4. Resources](#4-resources)
    - [5. Rewarding system](#5-rewarding-system)
    - [6. Entity combat](#6-entity-combat)
  - [Non-Functional Requirements](#non-functional-requirements)
    - [1. Game menu](#1-game-menu)
    - [2. Game Media](#2-game-media)
    - [3. Graphics](#3-graphics)
    - [4. Gameplay](#4-gameplay)
    - [5. System requirements](#5-system-requirements)
    - [6. Game installation](#6-game-installation)

# Introduction #

## Purpose of this Document

The purpose of this document is to provide the necessary background information, requirements, system scope, and high-level technical specifications for the out-sourcing of the design, development, and implementation of a 2D Strategy Game MAD_P.

## Purpose of the System

The purpose of the game is for the user to have fun while improving their logical thinking and strategy developing skills.

# Project Overview #

## High Level Overview

We are working on a 2D strategy game in which the user can have fun and at the same time enhance their cognitive abilities. This game's play-style is "Player vs. AI" (singleplayer). The goal of the game is to destroy your enemy’s main structure whether it would be a castle, a fortresses or any other type of stronghold, while defending your own castle or fortress from enemy attacks. The destruction of the enemy base counts as a direct win. Users will be able to choose their own ways to achieve this. However, it is advised to think about every move you make since enemies will be implemented with various AI features such as path finding, target prioritization, spawning characters which would lead AI to as fast destruction of your base as possible. Because of that, resource management will be vital to your survival. The game will implement stage progression. This means once you complete the stage you are currently in, the stages onwards will become bigger with more paths, obstacles and other environmental challenges such as swamps, etc. Players will be able to get new types of characters, as well as upgrade currently owned ones with currency they get from winning battles. This game is targeted for people who like to develop their strategy skills and managing given character cards as efficiently as possible. Targeted age group is any age group, because this game does not have any explicit graphical content.

# Product Requirements #

## Functional Requirements

### 1. Game mechanics

- The game is Player versus computer AI (singleplayer);
- The game menu, both in game and in the main menu is controlled only with a mouse;
- Spawn zones are marked in a dim yellow color.
- Multiple difficulty levels
  - Normal;
  - Hard.

### 2. Game characters and structures

- Player can control only which character spawns and where it spawns by selecting the wanted character in the card deck and dragging it onto the marked spawning zone;
- Player characters and enemy characters are controlled by the game AI;
- Player characters/structures and enemy characters/structures are able to recognise and attack opposing team's characters/structures;
- Characters can inflict damage on enemy structures and enemy characters;
- Structures can inflict damage on any opposing team's character;
- Structures and characters can attack only opposing team's structures or characters;
- After reaching minimal health limit structures are destroyed;
- After reaching minimal health limit characters are killed.

- Attributes of characters
  - Name (for example: warrior, archer, dragon, zeppelin);
  - Type (for example: ground-to-ground, ground-to-air, air-to-air, air-to-ground);
  - Speed (speed determines how fast character can travel from one point to another);
  - Health bar (health bar with remaining health is casted over each character);
  - Attack damage;
  - Attack rate (delay between attacks);
  - Attack range (from how far character can inflict damage to opposing characters or structures);
  - "Mana" cost;
  - Upgrade level.

- Attributes of structures
  - Name (for example: castle, tower);
  - Health bar (health bar with remaining health is casted over each structure);
  - Attack damage;
  - Attack rate;
  - Attack range. </br>
  **Note: default type of all structures is ground-to-air**

### 3. Health system
- Health

  Game entities are provided with a health system, to make them mortal by giving them health value. This value is decreased by encountering enemies or traps. Game objects are destroyed if health value is equal or less than zero. Entities can also gain health by encountering certain objects, which provide health points. Entities cannot exceed the maximum health value, which they are provided with.

- Visual entity health bar. Game entities are provided with a visual display of their current health status. The health bar is presented in a bar shape and uses a fixed color gradient

  - Green color status when current health > 66.6%
    <br>
    ![HealthBar](images/fullHealth.png)
  - Yellow color status when current health > 33.3%
    <br>
    ![HealthBar](images/halfHealth.png)
  - Red color status when current health > 0%
    <br>
    ![HealthBar](images/lowHealth.png)

The health bar is always located slightly above the entity.

### 4. Resources
  - A resource called "mana" is used in order to spawn characters;
  - Each character's spawning cost can be determined by strength of the character'.
  - 1 amount of "mana" is generated every 5 seconds;
  - Maximum amount of "mana" that can be stored for spawning characters is 10.

### 5. Rewarding system
1. Coins
  - Received coins are determined by how fast the player manages to win the match and selected difficulty of the game;
  - Player can spend coins to get new characters or upgrade currently owned ones;
  - If the Player loses the match, no coins are received.
2. Trophies
  - For winning a match player gets "trophies", which determine whether the user can progress to other arenas. Gained "trophy" count is determined by how fast the player manages to win the match and selected difficulty of the game;
  - For losing a match a certain amount of "trophies" is deducted.

### 6. Entity combat

When entity approaches its foe and gets close enough, a certain fighting animation is initialized. If he was in the danger zone (attack range) it loses part of its health.
<br>
![EntityCombat](images/entity-combat.png)

<br>

## Non-Functional Requirements

### 1. Game menu

- The purpose of the game menu is to provide a navigation of the game for the User.
- **Game menu items**
  - Start a new game button allows the user to select a new game;
  - Level picking button allows the user to select the level he wants to play at;
  - Continue button allows the user to carry on with game progress;
  - Exit game button allows the user to leave the current game window;
  - Options button allows the user to customize game preferences (brightness, sound, etc.) to their needs;
  - Credits button allows the user to view game credits.

### 2. Game Media

- Music, sound effects and animations are at medium intervention.

### 3. Graphics

1. Arenas textures, player sprites, particle effects are done in cartoon-like style.
2. Game must ensure a smooth framerate
    - Users might have a hard time playing with low or unstable framerate. The goal is to have a stable 60 FPS.

### 4. Gameplay

1. Multiple levels
  - First level is basic.
  - Further levels have bigger map, some may have obsticles, like swamps, etc.
2. Destruction of opposing team's main structure counts as a win.
3. Match clock
    - One match can take up to 3 minutes;
    - Once match clock reaches 1 minute mark, "mana" generation speed changes to 1 "mana" generated every 2 seconds;
    - Once match clock reaches 30 seconds mark, the winner is the one which manages to destroy whichever structure of the opposing team.
    - Once match clock reaches 0 seconds mark, the match ends in a "tie". In this case the user will not get any reward.
4. Natural obstacles
    - Swamp
      - Once a character decides to go over a swamp in order to potentially reach it's goal faster it gets slowed down.

### 5. System requirements

- A computer (desktop or laptop) that has enough hardware resources to run a browser and has a dedicated or integrated graphics processing unit (GPU);
- Has Windows 10 or on MacOS 10.9 Maverick installed as an operating system. </br>
**Note: a computer must have a CPU and a GPU in order to run the game. Regarding OS, the game might run on older versions of Windows/MacOS**

### 6. Game installation

1. Extract archive into an empty folder;
2. Navigate to the folder in which you have extracted your game;
3. Open a file named “MADP_game”.
