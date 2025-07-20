Here's a `README.md` file structure that incorporates your game rules along with a description of the gameplay for your Solitaire game.

---

# Solitaire Game

A console-based Solitaire game developed in C#. This game follows the classic rules of Solitaire, where players aim to move all cards to the foundation piles sorted by suit from Ace to King.

---

## Table of Contents

1. [Game Rules](#game-rules)
   - [Moving Cards Between Stack Piles](#moving-cards-between-stack-piles)
   - [Moving Cards to Foundation Piles](#moving-cards-to-foundation-piles)
   - [Drawing Cards from the Stock Pile](#drawing-cards-from-the-stock-pile)
   - [Valid Moves](#valid-moves)
2. [Gameplay](#gameplay)
3. [Controls](#controls)
4. [Screenshots](#screenshots)

---

## Game Rules

### Moving Cards Between Stack Piles
1. Cards in **Stack piles** must be placed in **descending order** (King to Ace).
2. Cards must **alternate colors** (red for hearts/diamonds, black for clubs/spades).
3. **Only a King** can be moved to or placed in an **empty tableau space**.
4. You can select a **face-up card in a tableau pile** and move it to another tableau pile if it follows the descending order and alternating color rules.

### Moving Cards to Foundation Piles
1. Foundation piles are **built up in ascending order** by **suit** (hearts, diamonds, clubs, spades).
2. **Start with an Ace** in each foundation pile, and then build up by rank in ascending order (Ace, 2, 3, ..., King).
3. You can move a card from the tableau piles or the stockpile to the foundation if it is the next card in the sequence for that foundation pile.

### Drawing Cards from the Stock Pile
1. Draw one card at a time from the **stockpile** and place it in the waste pile.
2. If the stockpile becomes empty, you can reshuffle the waste pile to create a new stockpile and continue drawing.

### Valid Moves
1. To make a move, select a **card from the source pile** and choose a **destination**.
2. Moves are allowed between tableau piles if they follow the descending order and alternating color rules.
3. Cards can also be moved from Stack piles or stockpile to the foundation if they follow the correct sequence (Ace to King) and suit.
4. Direct moves between foundation piles and Stack piles are generally not allowed unless it is back from the foundation to the Stack under the rules.

---

## Gameplay

1. **Starting the Game**  
   - When you launch the game, you'll be presented with a start menu. Choose "1. Start Game" to begin, or "2. Exit" to quit.

2. **Dealing the Cards**  
   - The game begins with cards dealt into seven tableau stacks. The foundation piles start empty, and cards in the stockpile are available for drawing.

3. **Making Moves**  
   - Each turn, you can choose from the following moves:
     - Move cards between tableau stacks.
     - Move cards from the stockpile to a tableau or foundation pile.
     - Move cards from tableau piles to foundation piles.

4. **Winning the Game**  
   - To win, arrange all cards into the foundation piles in ascending order by suit (from Ace to King).

5. **Undo and Redo**  
   - You can undo moves if you make a mistake, or redo them if needed. This feature allows for flexibility and error correction.

6. **Quitting the Game**  
   - You can quit the game at any time by selecting the "Quit" option from the main menu.

---

## Controls

- **1. Start Game**: Begins a new game.
- **2. Exit**: Exits the game.
- **Gameplay Options**:
  - Move card from stockpile to stack
  - Move card from stockpile to foundation
  - Move card from stack to foundation
  - Move card from stack to stack
  - Draw the next card in the stockpile
  - Undo and Redo moves
  - Quit the game

Use the menu options displayed in the console to make moves and navigate the game.

---

## Screenshots

### Start Screen
![Start Screen](start.pnj)

### Playing Screen
![Playing Screen](playing.png)

### End Screen
![End Screen](end.pnj)

---

## Conclusion

This console-based Solitaire game provides a straightforward and fun way to play Solitaire in a text-based format. Enjoy organizing the cards, strategizing moves, and mastering Solitaire!

---