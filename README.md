# Matatu AI — C#/.NET Card Game

Matatu AI is a C#/.NET implementation of **Matatu**, a Ugandan card game similar to UNO, featuring a computer-controlled opponent that makes autonomous decisions based on the current game state, available cards, and Matatu's rules.

The project explores **game-state modeling, heuristic decision-making, object-oriented software design, and autonomous agent behavior** within a turn-based game environment.

## Overview

The application allows a human player to compete against a computer-controlled opponent.

Rather than selecting cards randomly, the computer evaluates its available actions against the current state of the game and determines which legal move to make.

At a high level, the computer follows a decision cycle:

```text
Observe Current Game State
          ↓
Identify Legal Actions
          ↓
Evaluate Available Cards
          ↓
Apply Strategy and Game Rules
          ↓
Select an Action
          ↓
Update Game State
          ↓
Repeat
```

This creates an autonomous computer opponent capable of responding dynamically as the game changes.

---

## Technologies

* C#
* .NET Framework
* Object-Oriented Programming
* Data Structures
* Algorithmic Decision-Making
* Game-State Modeling
* Heuristic AI

---

## Core Components

### Program

The `Program` class controls the overall execution and flow of the game.

Responsibilities include:

* Starting and ending the game
* Initializing players and the deck
* Managing player and computer turns
* Processing user input
* Updating the active game state
* Detecting game-ending conditions
* Displaying the final result

---

### Logic

The `Logic` class contains the primary game-rule and computer decision-making functionality.

Responsibilities include:

* Determining whether cards are legally playable
* Processing special-card behavior
* Evaluating the computer's available cards
* Selecting actions for the computer player
* Managing rule-based game behavior
* Calculating scores at the end of a game

The computer player uses the current game state and its available cards to determine an appropriate move rather than selecting an action randomly.

---

### Player

The `Player` class represents each participant and manages their interaction with the deck.

Responsibilities include:

* Maintaining the player's hand
* Drawing cards
* Playing cards
* Updating the player's available cards throughout the game

---

## Computer AI

The computer opponent acts as an autonomous game-playing agent.

During its turn, the computer:

1. Observes the current card and active suit.
2. Inspects the cards available in its hand.
3. Determines which cards represent legal actions.
4. Evaluates the available options according to the game's rules and programmed strategy.
5. Selects an action.
6. Updates the game state.
7. Repeats the process on its next turn using the newly updated state.

This creates a basic **heuristic AI system** in which decisions are made dynamically from the environment rather than following a predetermined sequence of moves.

### Current AI Architecture

```text
                 ┌─────────────────┐
                 │   Game State    │
                 └────────┬────────┘
                          │
                          ▼
                 ┌─────────────────┐
                 │   Game Rules    │
                 │ / Legal Actions │
                 └────────┬────────┘
                          │
                          ▼
                 ┌─────────────────┐
                 │ Computer Player │
                 │                 │
                 │ Evaluate Hand   │
                 │ Apply Strategy  │
                 │ Select Action   │
                 └────────┬────────┘
                          │
                          ▼
                 ┌─────────────────┐
                 │ Selected Action │
                 └────────┬────────┘
                          │
                          ▼
                 ┌─────────────────┐
                 │ Updated State   │
                 └─────────────────┘
```

---

## Game Rules

The game begins with a shuffled deck and an initial set of cards for both the human player and computer.

A card can generally be played when its:

* Suit matches the active suit, or
* Value matches the current card

Special cards introduce additional game behavior.

Examples include:

* **Ace** — special game behavior
* **Jack** — special game behavior
* **Eight** — special game behavior
* **Seven** — can trigger a game-ending condition

The game continues until a winning or stopping condition is reached.

---

## Game Flow

```text
Initialize Game
      ↓
Shuffle Deck
      ↓
Deal Cards
      ↓
Start Game Loop
      ↓
Player Turn
      ↓
Update Game State
      ↓
Computer AI Turn
      ↓
Evaluate Available Actions
      ↓
Select Computer Action
      ↓
Update Game State
      ↓
Check End Condition
      ↓
Continue or Calculate Score
```

---

## Scoring

When the game ends, the cards remaining in each player's hand are evaluated to calculate the final score.

The application then determines whether:

* The player won
* The computer won
* The game resulted in a tie

---

## Software Design

The application separates responsibilities across several classes rather than placing all functionality inside a single game loop.

The design emphasizes:

* Separation of game state and game rules
* Object-oriented design
* Encapsulation of player behavior
* Reusable rule-processing logic
* Isolated computer decision-making
* Maintainable game-state transitions

---

## UML Diagram

The following UML diagram illustrates the primary classes and relationships in the application:

![Matatu UML Diagram](images/MatatuC%23\(UNO\)%20UML.drawio%20\(1\).png)

---

## AI Development Roadmap

The current computer opponent uses heuristic and rule-based decision-making.

Future development will expand the project into a broader environment for experimenting with and evaluating different AI strategies.

### Phase 1 — AI Architecture

* Separate computer decision-making into dedicated AI strategy classes
* Introduce a common agent interface
* Add a random-action agent as an experimental baseline
* Improve separation between the game engine and AI decision layer

Example architecture:

```text
IAgent
   │
   ├── RandomAgent
   │
   ├── HeuristicAgent
   │
   └── Future Advanced Agents
```

### Phase 2 — Automated AI Evaluation

Create automated AI-vs-AI simulations capable of running hundreds or thousands of games.

Potential evaluation metrics include:

* Win rate
* Average game length
* Average decision time
* Illegal-action rate
* Performance against different strategies
* Average remaining-card score

This will make it possible to objectively compare AI strategies rather than relying only on manual gameplay.

### Phase 3 — Search-Based AI

Explore more advanced decision-making techniques for reasoning about future game states and uncertainty.

Potential approaches include:

* Monte Carlo simulation
* Monte Carlo Tree Search
* Information-set search techniques

These agents could be compared directly against the existing heuristic system.

### Phase 4 — Adaptive AI

Explore an opponent-modeling system that records player behavior and adjusts strategy over time.

Potential behaviors to analyze include:

* Frequently selected suits
* Special-card usage
* Playing patterns
* Aggressiveness
* Card-management tendencies

### Phase 5 — AI Explanation and Evaluation

Explore integration with Large Language Models to provide human-readable explanations of decisions made by the deterministic game-playing AI.

A future architecture could separate:

```text
Game-Playing AI
       ↓
Selected Decision
       ↓
LLM Explanation Agent
       ↓
AI Evaluation Agent
       ↓
Explanation + Evaluation
```

The game engine would remain responsible for authoritativ
