# 2D Superhero Game — IP3_2D_CF

A 2D superhero flying game developed in Unity, originally created as a university group project exploring an augmented reality (AR)-inspired gaming experience for an in-car environment.

The player takes control of a flying superhero, navigating the skies, collecting coins and power-ups, avoiding enemy attacks, and fighting against a villain.

**Current development focuses on improving the game's code architecture, boss attack systems, and overall project structure.**

## About the Project

This project was originally developed as part of a university assignment. Our team was tasked with creating a game for children that simulated an augmented reality experience within a car setting.

We chose to develop a 2D flying superhero game designed around accessible controls and straightforward gameplay. Players fly through the environment, collect items, avoid incoming attacks, and engage in combat with the main villain.

The original version explored gesture-based controls using a Leap Motion controller. The current version supports mouse controls, with further development focused on refining gameplay systems and improving the underlying codebase.

## Gameplay

- **Flying:** Control the superhero's movement and direction.
- **Collectibles:** Gather coins and power-ups throughout the level.
- **Combat:** Shoot projectiles to fight enemies and the main villain.
- **Boss encounters:** Dodge incoming attacks and fight back against the boss.

## Controls

### Mouse Controls

- **Move mouse:** The player adjusts their direction based on the mouse position.
- **Left-click:** Fire a projectile.
- **Hold left-click:** Continuously fire while holding the button.

### Legacy Leap Motion Controls

Earlier versions of the project experimented with gesture-based controls using a [Leap Motion Controller](https://www.ultraleap.com/product/leap-motion-controller/).

The original control scheme used the pitch of the player's hand to determine the character's angle. Closing a fist triggered the player's shooting action.

Leap Motion support is not currently available in the latest version and may require additional work to restore compatibility.

## Technical Development

The project is being refactored incrementally, with a focus on maintainability, modularity, and extensibility.

Current architectural improvements include:

- **AttackController:** Manages the main boss attack loop and separates core gameplay from testing gameplay.
- **AttackDecision:** Determines attack properties, including attack type, elemental behaviour, and whether projectiles track the player.
- **Modular attack execution:** Uses an abstract execution method to allow individual attack types to implement their own projectile spawning patterns and behaviours.
- **Code organisation:** Ongoing improvements to naming conventions, project structure, and separation of responsibilities.

These changes aim to make it easier to develop new boss attacks, test gameplay behaviours, and maintain the project as it evolves.

## Project Status

This is an ongoing personal development project based on an earlier university assignment. The codebase and project structure are being improved over successive versions, with further gameplay and architectural refinements planned.

The latest version should be treated as the primary version of the project. Older versions may differ in functionality and control support.
