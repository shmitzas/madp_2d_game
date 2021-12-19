<!-- General comments:
- The purpose of this document is for outsourcing the design and development of the game? Who will you outsource it to? :D
- The game mechanics are not really clear. Screen prototypes would also help a lot.
- Is it a single-player game where you play against one AI-controlled enemy? Is there any interaction between human players playing the same game (even if it is seeing a leaderboard / scores of other players).
- Requirements should be uniquely identified.
- Requirements should be prioritized. 

Comments about specifics:
- User 1: Not really a requirement?
- User 2: When can a user spawn a new troop? Is there some cost? Is there a limit of troops that can be spawned? Are there different types of troops? In what locations can troops be spawned? Are there pre-requisites (buildings or technologies researched)? etc.
- User 3: Is it something a user chooses in the UI? If not, is it really a requirement for the software?
- User input 1: Not clear if this is really a requirement
- User input 2: What does it mean to "select a troop"? Is there some menu? Can I right-click or left-click on that? Are there keyboard shortcuts? etc.
- Characters 1: How can a player control enemy characters? How can the game control player's characters?
- Characters 2: What does it mean if a character "recognizes" another character?
- Characters 3: How does a character inflict damage? Is the amount of damage the same for all troops? Is damage dealt over range or in close-quarters only?
- Characters 4: How are they affected by damage? Does it just reduce health or does it have side-effects as well?
- Characters 5: Do characters die? What happens then - do they leave loot or just disappear? Can health be restored?
- Rewards 1: What if I lose the game? Do I lose points? Are other ways to earn points?
- Rewards 2: Are points the only way to buy troops? If so, how do I buy troops in the first game (before winning any)? What are troop upgrades, how do they work? What does it mean to "own a troop" (do they transfer between games?)
- Game menu: These are functional requirements. Can I stop the game mid-game, and resume from the same place? Can I then choose to start a new game? What are game credits and how do they work?
- Media: "medium intervention" is subjective. Being able to adjust sound volume/other preferences is more of a functional requirement.
- Graphics: "eye-appealing", "smooth" is subjective. Framerate is more of a "performance" requirement.
- Gameplay: Being able to choose difficulty level and themes might be more of a functional requirement (esp. if they change behavior).
- System requirements: Can you run exe files on MacOS (installation mentions exe file)? Can a computer NOT have either a dedicated or integrated GPU? If not, then it's not worth mentioning it, as all computers automatically fulfill this part of the requirement.
- Game installation: This is more of a guide than 3 standalone requirements. -->

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
    - [Installation](#installation)
    - [Start Game](#start-game)
    - [Gameplay](#gameplay)
    - [Card Deck](#card-deck)
  - [Non-Functional Requirements](#non-functional-requirements)
    - [Availability](#availability)
    - [Usability](#usability)
    - [Reliability](#reliability)
    - [Performance](#performance)
    - [Compatibility](#compatibility)
    - [Security](#security)
    - [5. System requirements](#5-system-requirements)
    - [6. Game installation](#6-game-installation)

# Introduction

## Purpose of this Document

The purpose of this document is to provide the necessary background information, requirements, system scope, and high-level technical specifications for the out-sourcing of the design, development, and implementation of a 2D Strategy Game MAD_P.

## Purpose of the System

The purpose of the game is for the user to have fun while improving their logical thinking and strategy developing skills.

# Project Overview

## High Level Overview

We are working on a 2D strategy game in which the user can have fun and at the same time enhance their cognitive abilities. This game's play-style is "Player vs. AI" (singleplayer). The goal of the game is to destroy your enemy's main structure whether it would be a castle, a fortresses or any other type of stronghold, while defending your own castle or fortress from enemy attacks. The destruction of the enemy base counts as a direct win. Users will be able to choose their own ways to achieve this. However, it is advised to think about every move you make since enemies will be implemented with various AI features such as path finding, target prioritization, spawning characters which would lead AI to as fast destruction of your base as possible. Because of that, resource management will be vital to your survival. The game will implement stage progression. This means once you complete the stage you are currently in, the stages onwards will become bigger with more paths, obstacles and other environmental challenges such as swamps, etc. Players will be able to get new types of characters, as well as upgrade currently owned ones with currency they get from winning battles. This game is targeted for people who like to develop their strategy skills and managing given character cards as efficiently as possible. Targeted age group is any age group, because this game does not have any explicit graphical content.

# Product Requirements

## Functional Requirements

### Installation

- Go to [GitHub Repository](https://github.com/shmitzas/madp_uni_project)
- Go to "Builds" directory
- Pick your platform
- Download archived file
- Extract it in your prefered destination directory
- Run the game by opening a file named "RushNDestroy"

### Start Game

- The player should be able to press the "Play" to start the match.
- The player should be able to press on level icon to select which he/she wants to play in.

### Gameplay

- The player should be able to select a card by pressing and holding on the character icon.
- The player should be able to drag the selected card on the desired location on the screen.
- The player should be able to release the card and spawn the character.
- The character should spawn if the player has placed it on a highlighted field.
- The card should be returend to the deck if it was placed on not highlighted area.
- The spawned character should start moving towards the enemy structres.
- The character should continuously seek for other enemies within its range.
- The character should inflict damage to other entities if approached withing its range.
- The character should receive damage from other entities if it is withing their range.
- The character should die if it received the amount of damage equal to his health.
- The entity should select the next target if its primary target is dead.
- The entity should be removed from the gameplay after it is dead.

### Card Deck

- The player should be able to select a card by pressing and holding on the icon.
- The selected card should be brought up after selecting.
- The card should be removed if the character was spawned.
- New card should be placed in the old card's slot.
- The card should return to the deck if it was not used.**Note

### UI

- The UI should contain of a stop button which stops the game upon being pressed.
- The UI should contain a card deck and mana generation bar after the gameplay is started.

### Mana System and Mana Bar

- The mana bar should show the exact amount of mana the player has.
- The amount of mana should be substacted accordingly to the amount that it costs to spawn a character.
- The mana should be continuously regenerated during the gameplay.

### Character

- The characters should start acting accordingly after being sucessfully spawned.
- The character should seek for enemy characters or structures after being spawned and change its position accordingly.
- T


## Non-Functional Requirements

### Availability

- The application should work after installing without any additional files or software besides the original.

### Usability

- Free to use for everyone installing it.
- Easy to use without any additional knowledge about the software required.

### Reliability

- The application should not have any issues or bugs which would result in breaking, crashing or damaging the user's device in any way.

### Performance

- The application should be able to run on relatively weak devices without any high end components.

### Compatibility

- The application should run on MacOS, Windows and Linux operating systems.

### Security

- The application should not collect any information from the player.

### 5. System requirements

- A computer (desktop or laptop) that has enough hardware resources to run a browser and has a dedicated or integrated graphics processing unit (GPU).
- Has Windows 10, Linux or on MacOS 10.9 Maverick installed as an operating system. </br>
**Note: a computer must have a CPU and a GPU in order to run the game. Regarding OS, the game might run on older versions of Windows/Linux/MacOS**

### 6. Game installation

1. Extract archive into an empty folder.
2. Navigate to the folder in which you have extracted your game.
3. Open a file named "RushNDestroy".
