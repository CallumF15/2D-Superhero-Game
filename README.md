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
- **Combat:** Shoot projectiles to fight enemies and the main villain.
- **Boss encounters:** Dodge incoming attacks and fight back against the boss.

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

**In Progress:**
- Refining player movement and mouse-based aiming.
- Finalising the boss attack architecture.
- Developing and integrating additional boss attack behaviours.
- Improving code organisation, maintainability, and project structure.
- Addressing remaining legacy asset and compatibility issues.

The latest version should be treated as the primary version of the project. Older versions may differ in functionality, dependencies, and control support.
