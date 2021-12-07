# 2D GAME

# Technical Specification

> Made by MAD_P Team

</br>

<!-- General comments:
- The fact that Unity is used is mentioned only in passing. I think that is a central piece of information. Another key piece of info - an overview of which aspects of the game are handled by Unity, and which parts are provided by your code. This is currently missing. From that an explanation how each of your parts should be/is implemented could follow.
- A good part of the document could be considered functional requirements (how things should function), while this document should explain how things should be implemented in Unity.
- I think a good deal of information could be extracted from the diagrams, but it would be important to: 1) use a clear and consistent notation; 2) introduce a higher-level diagram/text explaining the high-level view (that hides some details). For example, that the key concepts in the game's implementation are: a character, a structure, an attribute, a map, a difficulty, a reward, a resource, etc. And then lower-level diagrams could explain each concept further, e.g. what types of structures are there, which parts of their implementation are shared, etc.
- The approach of describing UI components is also a possible one, but it should be consistent - e.g. take a full window screenshot, then highlight what components are seen on it, and then go down into details explaining how each component is implemented. Now it only shows a tile map and health bar, but I believe there are many more components in the UI.

 

Specific comments:
- List of components used for game's structure - what is a component in this context? Somehow "Game world", "Spawn sound" and "Hit marker" do not seem to be things at the same level of abstraction. "Spawn sound" or "Hit marker" seems more like a graphical/audio asset, while "Game world" sounds like something bigger?
- Unity Entity Component System - it's difficult to understand the significance of this section without knowing what role the pools play/how they are used in the game. Also, the note about strctures appears before structures pool is mentioned. Shouldn't that be a lower-level bullet point?
- "Health system" - the first paragraph and "Visual entity health bar" section looks more like the description of the functional requirements for the health system (and therefore should be in the other specification).
- "Health system > Object purpose" - this seems like an explanation of how some Health Bar UI component should be implemented (i.e. that it is composed of multiple lower-level UI components (slider, border, etc.). This is useful. A way to improve it could be to be explicit about that by naming a section "Component: Health Bar" (or similar) and in it explaining everything about this component - what it should look like, what lower-level components it composes, etc.
- "State Machine" - UML State Diagram could have been used here (or any kind of an image, really). The problem with textual description is that it does not cover all state transitions. For example, for "Idle" it is said that "the next state is set" - but which state is "next"? Also, if a unit is in a "Fighting" state and kills its target, would it remain in "Fighting" state or go back to "Seeking" state?
- "Entity combat > Overall idea" - it's said that "when entities of different teams detect each other, they will start fighting to reach enemy’s castle", but previously it was said that "After being spawned the unit is targeting the enemy tower and moving forward to it. If interupted by opponent unit, the secondary target is set to the newly detected unit." - that seems to be inconsistent.
- "Entity combat > Overall idea" - "Cool additional feature that we are currently working ..." - that is something to track in project tasks lists, and maybe mention in weekly report, but not in the technical specification.
- "Entity combat > How it works" - that sounds more like functional requirements
- "UML Deployement Diagrams" - there are no UML deployment diagrams here, or even any UML diagrams for that matter :( If using your own notation, then you should explain what does a box mean, what does an arrow and its direction mean, etc.
- "UML Deployement Diagrams > Overview of whole diagram" - the image is not readable - the image quality is too low.
- "UML Deployement Diagrams > Rewards" - it's not clear why Resources and Rewards are grouped into one -->

## Table of Contents

- [2D GAME](#2d-game)
- [Technical Specification](#technical-specification)
  - [Table of Contents](#table-of-contents)
- [Introduction](#introduction)
  - [Purpose of this Document](#purpose-of-this-document)
- [High-Level Overview](#high-level-overview)
  - [Structural Aspects](#structural-aspects)
  - [Dynamic Aspects](#dynamic-aspects)
    - [Health system](#health-system)
      - [**Object purpose**](#object-purpose)
      - [**Entity health scripting**](#entity-health-scripting)
        - [**Entity**](#entity)
        - [**Health bar**](#health-bar)
      - [**Health with object collision**](#health-with-object-collision)
    - [Tilemap system](#tilemap-system)
      - [**Properties of used tiles**](#properties-of-used-tiles)
    - [AI system](#ai-system)
    - [State Machine](#state-machine)
      - [**Properties of each state**](#properties-of-each-state)
    - [Entity Combat](#entity-combat)
      - [**Overall idea**](#overall-idea)
- [UML Deployement Diagrams](#uml-deployement-diagrams)
- [Technologies and tools used](#technologies-and-tools-used)
  - [Game engine](#game-engine)
    - [Unity Game Development Engine](#unity-game-development-engine)
  - [IDE](#ide)
    - [VS Code](#vs-code)
  - [For task management](#for-task-management)
    - [Gitkraken](#gitkraken)
  - [For creating wireframes, brainstorming](#for-creating-wireframes-brainstorming)
    - [Miro](#miro)
  - [Version control for our codebase](#version-control-for-our-codebase)
    - [Gitlab](#gitlab)
  - [Time tracking for weekly reports](#time-tracking-for-weekly-reports)
    - [Toggl](#toggl)
  - [For creating graphical assets](#for-creating-graphical-assets)
    - [Illustrator](#illustrator)
  - [For connecting to VU MIF Gitlab](#for-connecting-to-vu-mif-gitlab)
    - [Cisco AnyConnect](#cisco-anyconnect)

</br>

# Introduction

## Purpose of this Document

The purpose of this document is to provide the necessary technical information about this project for the up-keep and improving the 2D game.

<br>

# High-Level Overview

## Structural Aspects

Our 2D game consists of many parts and modules in order to make the game as easy as possible to maintain in the future. Here is the list of components used for game's structure:

- Game world
- Game characters
- Visuals
  - Character textures
  - Level textures
  - Card deck
    <br>
    ![Deck](images/deck.png)
  - Level selection
    <br>
    ![Arenas](images/arenaSelection.png)
- Sound efects
  - Spawn sound
  - Fighting sound
  - Death sound
  - Structure destruction sound
- Entity data
  - Health
  - Speed
  - Type
    - Structure
    - Ground
    - Air
  - Attack type
    - Close
    - Ranged
  - Attack range
  - Attack rate
  - Attack damage
- Particle effects
  - Spawn effect
  - Hit marker
  - Structure collapse effect

## Dynamic Aspects
### Health system
#### **Object purpose**

- *Canvas* object purpose is to collect all child objects and scale it to pleasing size also ensuring that the health bar is visually shown in a world space above entities
- *Health bar* object purpose is to store Fill and Border objects, has a slider component, which lets to configure health value and visually display it. HealthBar script is pinned with this object for functionality
- *Fill* object is a UI image of inner bar, which values are driven by the Slider component
  - Slider is a class in UnityEngine.UI. This slider controls the fill from minimum value to maximum when used. To use this slider as a health bar it is made non-interactive, with no navigation and no transition, with direction selected from left to right.
- *Border* object is a UI image, it represents the outer image of the health bar


#### **Entity health scripting**

##### **Entity**
        public int maxHealth = Value;
        public int currentHealth;
Variables. A certain maxHealth value is given to entities depending on it's type and characteristics. CurrentHealth is a constantly updating value, when interacted with. 

      void Start()
      {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
      }
At start a spawned entity recieves a full maximum health it is provided with and sets it to currentHealth. Visual representation of a health bar is set to maxHealth in current visual state.

##### **Health bar**

      public Slider slider;
      public Gradient gradient; 
      public Image fill; 
      private float health;

      public void SetMaxHealth(float health)
        {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);                     
        }

     public void SetHealth(float health)
        {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        }
</br>

#### **Health with object collision**

One of the classic fighting principles are based on collision between entities. When entities are provided with corresponding tags, it is easier to determine, which entities belong to a player and which to an enemy.
To make the object collide with other objects, components are added

- RigidBody 2D, puts the sprites under control of physics engine
- Box Collider 2D, collider interacts with 2D physics engine

When a collider contacts the current object, OnCollision2D(Collision2D) message is sent from MonoBehaviour class. When this function is inwoked, opponent tag is recognised and attack function is called to deal damage to an opponent entity.

![EntityDamageOnCollision](images/EntityDamageOnCollision.png)

This way damage and health is modified by alternating values for game entities.

![EntityHealthOnCollision](images/EntityHealthOnCollision.png)

</br>

### Tilemap system

The video game is based on a 2D tilemap, which lets to create an enviroment for characters in action. A collection of each individual tile forms a tileset, which is later customized for unique scenes.

#### **Properties of used tiles**

- seamless distinction between tiles
- 128x128 pixel tile size
- colors and paterns based on texture imitation
- vertical and horizontal directions for scene variation
- using layer 0 to imitate base grounds

![TileMap](images/TileMapVer1.1.png)

</br>

### AI system

Game entities are provided with their own thinking system. The system is like a brain to a placeable unit. Every placeable character is unique and independent unit, with it's own AI system. The AI system has several different features. Primary feature is fidning the path (Pathfinding). After being spawned the unit is targeting the enemy tower and moving forward to it. If interupted by opponent unit, the secondary target is set to the newly detected unit. Upon arrival to the secondary target, the units will inflict damage to each other, with the help of Health System (see above). Until one of the units is killed, the pathfinding to the primary target is continued. The changes of what the unit is doing is regulated by a State Machine.

</br>

### State Machine

State machine will split different jobs an unit has to do regarding to its actions or position in the field. The states will allow to change what the unit is supposed to do at certain points of the game. The states are split into following:

- Dragged
- Idle
- Seeking
- Fighting
- Dead

#### **Properties of each state**

- Idle. Idle state happens right after the unit is spawned into the field. As the name reffers, in this state the unit does not do anything. The unit is added to the game hierarchy and according to the current situation in the field, the next state is set.
- Seeking. In this state, the unit is moving towards a target. Whether it is a primary target (The Tower), or a secondary target (A detected opponent unit).
- Fighting. This state is triggered after Seeking state is sucessfully completed (The unit arrived to the location or is within range to attack its target). During this state, damage is inflicted to the opponent unit (character or structure).
- Dead. This state occurs when the unit runs out of health points that are inflicted during Fighting state. The unit is immediately removed from the field and no longer exists in the hierarchy of the game.

### Entity Combat

#### **Overall idea**

In the game when entities of different teams detect each other, they will start fighting to reach enemie’s castle. For this to work, these game objects need to be able to receive and deal damage to other objects. Different enemy entities will not have the same responses to start attacking. By design some entities are more agile and will perform the attack quicker than others. By design, their attacks will deal slightly less damage than standard numbers. On the other hand, entites with slower reflexes will deal increased damage.


# UML Deployement Diagrams

1. Overview of whole diagram
    <br>
    ![Diagram](images/diagram.jpg)
2. Characters
    <br>
    ![Characters](images/characters.jpg)
3. Structures
    <br>
    ![Structures](images/structures.jpg)
4. Menu UI
    <br>
    ![UI](images/ui.jpg)
5. AI
    <br>
    ![AI](images/ai.jpg)
6. Rewards
    <br>
    ![Rewards](images/rewards.jpg)
7. Arenas
    <br>
    ![Arenas](images/arenas.jpg)

# Technologies and tools used

## Game engine
### [Unity Game Development Engine](https://unity.com)
## IDE
### [VS Code](https://code.visualstudio.com)
## For task management
### [Gitkraken](https://www.gitkraken.com)
## For creating wireframes, brainstorming
### [Miro](https://miro.com/)
## Version control for our codebase
### [Gitlab](https://about.gitlab.com)
## Time tracking for weekly reports
### [Toggl](https://toggl.com)
## For creating graphical assets
### [Illustrator](https://www.adobe.com/products/photoshop.html)
## For connecting to VU MIF Gitlab
### [Cisco AnyConnect](https://www.cisco.com/c/en/us/products/security/anyconnect-secure-mobility-client/index.html)


</br>
