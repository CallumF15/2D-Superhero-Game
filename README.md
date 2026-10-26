# 2D Superhero Game — IP3_2D_CF

A 2D superhero flying game developed in Unity, originally created as a university group project exploring an augmented reality (AR)-inspired gaming experience for an in-car environment.

The player takes control of a flying superhero, navigating the skies, collecting coins and power-ups, avoiding enemy attacks, and fighting against a villain.

Current development focuses on refining player movement, expanding boss attack behaviours, and improving the game's overall code architecture and project structure.

## About the Project

This project was originally developed as part of a university assignment. Our team was tasked with creating a game for children that simulated an augmented reality experience within a car setting.

We chose to develop a 2D flying superhero game designed around accessible controls and straightforward gameplay. Players fly through the environment, collect items, avoid incoming attacks, and engage in combat with the main villain.

The original version explored gesture-based controls using a Leap Motion controller. The current version uses mouse-based controls, with further development focused on refining gameplay systems and improving the underlying codebase.

## Gameplay

- **Flying:** Control the superhero's movement and direction.
- **Collectibles:** Gather coins and power-ups throughout the level.
- **Combat:** Shoot projectiles to fight the main villain.
- **Boss encounters:** Dodge incoming attacks and fight back against the boss.
- **Game States:** Gameplay can transition between playing, paused, and game-over states.

## Controls

### Mouse Controls

- **Move mouse:** The player adjusts their direction based on the mouse position.
- **Left-click:** Fire a projectile.
- **Hold left-click:** Holding continuously fires projectiles.

### Legacy Leap Motion Controls

Earlier versions of the project experimented with gesture-based controls using a Leap Motion Controller.

The original control scheme used the pitch of the player's hand to determine the character's angle. Closing a fist triggered the player's shooting action.

Leap Motion support is not currently available in the latest version and may require additional work to restore compatibility.

## Technical Development

The project is being refactored incrementally, with a focus on maintainability, modularity, extensibility, and compatibility with newer Unity versions.

### Input System Migration

The migration from Unity's legacy Input Manager to the **new Unity Input System package** has been completed.

Changes include:

- Replacing legacy mouse-position queries with the new Input System API.
- Updating mouse button and keyboard input handling.
- Integrating the `InputSystemUIInputModule` to maintain compatibility with Unity UI.
- Removing dependencies on the legacy input API.

### Player Movement

Player movement and mouse-based aiming have been substantially reworked.

Current improvements include:

- Updating mouse input handling to work with the new Input System.
- Refining player rotation and directional movement.
- Improving the separation between movement calculations and input handling.

Movement is mostly implemented, with further refinement still ongoing.

### Game State Management

A centralised **GameManager** has been introduced to manage the overall game state and provide a consistent approach to controlling gameplay.

The GameManager follows the **Singleton pattern**, allowing other gameplay systems to access the current game state without requiring individual references to the manager.

Current functionality includes:

- **Singleton Architecture:** Provides a centralised GameManager instance accessible throughout the project.
- **Game States:** Introduces a `GameState` enum to represent the current state of the game:
  - `Playing` – Normal gameplay.
  - `Paused` – Gameplay is suspended.
  - `GameOver` – The player has died and gameplay has ended.
- **Pause and Resume:** Centralises pause and resume functionality by controlling the game's time scale and managing active gameplay scripts.
- **Player Death Handling:** Provides a centralised method for handling player death and transitioning into the game-over state.
- **Script Management:** Tracks and disables active scripts when pausing, preserving their previous enabled states so they can be restored when gameplay resumes.
- **Scene Restarting:** Supports restarting the current scene and restoring the game's time scale.

This architecture provides a foundation for additional game states and simplifies gameplay transitions as the project grows.

### Power-up System

The power-up system has been improved to provide better visual feedback and more organised collectible behaviour.

Current improvements include:

- **Power-up notifications:** Collecting a power-up triggers an on-screen notification to inform the player that an effect has been applied.
- **Modular power-up behaviour:** Separates collision detection, effect application, and notification handling.
- **Floating animation:** Power-ups gently move up and down using a sine-wave animation, making collectibles more visually noticeable.
- **Independent rotation:** Separates the visual model from its collision object, allowing the power-up to rotate without affecting its collision orientation.
- **Collision handling:** Uses 2D trigger detection to identify the player and activate the appropriate power-up effect.
- **Object cleanup:** Removes collected power-ups and their associated child objects from the scene.

These improvements aim to make collectibles more readable, reusable, and easier to extend with additional power-up types.

### Boss Attack Architecture

The boss attack system has undergone significant architectural improvements and is nearing completion.

Current improvements include:

- **AttackController:** Manages the main boss attack loop and separates core gameplay from testing gameplay.
- **AttackDecision:** Determines attack properties, including attack type, elemental behaviour, and whether projectiles track the player.
- **Modular attack execution:** Uses an abstract execution method to allow individual attack types to implement their own projectile spawning patterns and behaviours.
- **Extensibility:** The architecture is designed to make adding new attack types and behaviours easier without unnecessarily changing existing systems.
- **Testing and refinement:** The system is undergoing final refinements, with additional boss attacks being developed and integrated.

### Unity Project Modernisation

Additional work has been carried out to improve compatibility with newer Unity versions.

This includes:

- Updating project settings and addressing deprecated serialized asset formats.
- Reviewing older materials, shaders, and project assets for compatibility.
- Updating Unity-generated Visual Studio project files to reflect the current project structure.
- Reviewing legacy dependencies and packages, particularly those associated with the original Leap Motion implementation.

## Project Status

This is an ongoing personal development project based on an earlier university assignment.

**Completed:**
- Migration from the legacy Input Manager to the new Unity Input System.
- Integration of the new input system with Unity UI.
- Introduction of centralised GameManager architecture using the Singleton pattern.
- Implementation of playing, paused, and game-over states.
- Basic power-up floating and rotation animations.
- Power-up collection notifications and effect handling.

**In Progress:**
- Refining player movement and mouse-based aiming.
- Finalising the boss attack architecture.
- Developing and integrating additional boss attack behaviours.
- Improving game state transitions and UI integration.
- Improving code organisation, maintainability, and project structure.
- Addressing remaining legacy asset and compatibility issues.

The latest version should be treated as the primary version of the project. Older versions may differ in functionality, dependencies, and control support.
