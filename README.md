# Ben Carter 2020-COMP1000-CW2

This is my submission for the dungeon crawler game. The objective of the game is the reach the finish whilst collecting the most gold and not dying to monsters.

[CMD Crawler Video](https://youtu.be/vkjW_XZ5tpI)

## Running the game
Detailed instructions on how to start/run the game are included in the above video.

## Controls
 * 'W' - to move north (up)
 * 'A' - to move west (left)
 * 'S' - to move south (down)
 * 'D' - to move east (right)
 * Spacebar - to attack monsters when positioned next to them
 * 'E' - to pickup gold when positioned next to gold

![](https://i.imgur.com/pJ163T7.png)

## Starting game
To start the game, the user must first choose their desired map, either 'Simple.map', or 'Advanced.map', followed by the 'play' command
 * When starting game either use command 'load simple.map' or 'load advanced.map'
 * When map has been loaded successfully the game will present the message below:

![](https://imgur.com/JvhCwix.png)

if message does not appear please check for spelling mistakes.
Once the map has successfully been initialized and the play command has been issued the game should start :)

![](https://imgur.com/1Am4HJ1.png)

## Entities
The Dungeon Crawler contains 3 entities known to the player:
 * M - monsters (enemies)
 * G - gold (collectable)
 * @ - the player

## Movement
The player can move using the above controls, '.' represents an empty space, which the player can move onto, the player can also move over gold. '#' represents walls, which the user cannot move over/pass, the same principle applies to monsters ('M'), though the user will take damage if they collide with them!

Photo of map:

![](https://imgur.com/HoKkmAb.png)

## Picking up gold
 * In order to pick up gold press the 'E' key
 * the character must be standing next to the gold, not on top (examples are shown below)
 * Number of gold picked up is shown under map

Photo of gold count:

![](https://imgur.com/k6VRLjE.png)

Examples of where player must be positioned to collect gold:

![](https://imgur.com/wM2MdaG.png)
![](https://imgur.com/BRSuhbz.png)

## Attacking monstsers
 * To attack a monster you must be standing next to them and use the spacebar
 * If you collide with them you will lose health, health is displayed below gold, an image of the players health is below:

![](https://imgur.com/wk5CLHW.png)

An example of where you must be positioned to attack monsters is shown below:

![](https://imgur.com/9C8tAb8.png)

## Finishing game
To finish a game simply move your character over the finish checkpoint (E), and the game will quit.
