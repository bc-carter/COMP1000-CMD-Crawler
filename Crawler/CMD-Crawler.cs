using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace Crawler
{
    /**
     * The main class of the Dungeon Crawler Application
     * 
     * You may add to your project other classes which are referenced.
     * Complete the templated methods and fill in your code where it says "Your code here".
     * Do not rename methods or variables which already exist or change the method parameters.
     * You can do some checks if your project still aligns with the spec by running the tests in UnitTest1
     * 
     * For Questions do contact us!
     */
    public class CMDCrawler
    {
        /**
         * use the following to store and control the next movement of the yser
         */
        public enum PlayerActions {NOTHING, NORTH, EAST, SOUTH, WEST, PICKUP, ATTACK, QUIT };
        private PlayerActions action = PlayerActions.NOTHING;

        /**
         * tracks if the game is running
         */
        private bool active = true;
        public bool isPlaying = false;
        private string[] mapFile; // stores each line of map
        private char[][] mapFile2 = new char[0][];
        private char[][] mapTiles = new char[0][]; // stores each char of map

        private int yPos; // store x coordinates
        private int xPos; // stores y coordinates
       
        private int prevYPos;
        private int prevXPos;

        private int gold = 0;
        private int health = 3; 

        private bool wasGold = false; // if true, the player is currently stood on gold



        /**
         * Reads user input from the Console
         * 
         * Please use and implement this method to read the user input.
         * 
         * Return the input as string to be further processed
         * 
         */
        private string ReadUserInput()
        {
            string inputRead = string.Empty;
            
            // Your code here
            if (isPlaying == true) // if play command has been issued, then read key inputs
            {
                ConsoleKeyInfo info = Console.ReadKey();
                char inputtedKey = info.KeyChar;
                inputRead = inputtedKey.ToString();
            }
            else // if play command hasn't been issued read line command inputs
            {
                inputRead = Console.ReadLine();
            }

            return inputRead;
        }

        /**
         * Processed the user input string
         * 
         * takes apart the user input and does control the information flow
         *  * initializes the map ( you must call InitializeMap)
         *  * starts the game when user types in Play
         *  * sets the correct playeraction which you will use in the GameLoop
         */
        public void ProcessUserInput(string input)
        {
            // Your Code here
            input = input.ToLower();
            switch (input)  // switch statement sets PlayerActions depending on input, also calls initialize map when command is entered, and starts game when play command is used
            {
                case "load Simple.map":
                    InitializeMap("Simple.map");
                    break;
                case "load simple.map":
                    InitializeMap("Simple.map");
                    break;
                case "load Advanced.map":
                    InitializeMap("Advanced.map");
                    break;
                case "load advanced.map":
                    InitializeMap("Advanced.map");
                    break;
                case "load Extra.amp":
                    InitializeMap("Extra.map");
                    break;
                case "load extra.map":
                    InitializeMap("Extra.map");
                    break;
                case "play":
                    action = PlayerActions.NOTHING;
                    isPlaying = true;
                    break;
                case "w":
                    action = PlayerActions.NORTH;
                    break;
                case "a":
                    action = PlayerActions.WEST;
                    break;
                case "s":
                    action = PlayerActions.SOUTH;
                    break;
                case "d":
                    action = PlayerActions.EAST;
                    break;
                case " ":
                    action = PlayerActions.ATTACK;
                    break;
                case "e":
                    action = PlayerActions.PICKUP;
                    break;
            }
            if (isPlaying == false)
            {
                action = PlayerActions.NOTHING;
            }
        }

        /**
         * The Main Game Loop. 
         * It updates the game state.
         * 
         * This is the method where you implement your game logic and alter the state of the map/game
         * use playeraction to determine how the character should move/act
         * the input should tell the loop if the game is active and the state should advance
         */
        public void GameLoop(bool active)
        {
            // Your code here
            bool correctMove = false;
            bool gameEnd = false;
            

            if (isPlaying == true)
            {   
                
                try
                {
                    try { Console.Clear(); }   catch (IOException) { } // prevents console.clear() errors by catching errors
                    if (action == PlayerActions.NORTH)
                    {
                        GetPlayerPosition();
                        prevYPos = yPos;
                        prevXPos = xPos;
                        correctMove = true;
                        yPos -= 1; //adjusts coords relevant to movement

                        if (mapTiles[yPos][xPos] == '.') // checks for empty space
                        {
                            mapTiles[yPos][xPos] = '@'; // replaces empty space with player (@)
                            if (wasGold == true) // used to replace gold with G if walked over
                            {
                                mapTiles[prevYPos][prevXPos] = 'G';
                                wasGold = false;
                            }
                            else
                            {
                                mapTiles[prevYPos][prevXPos] = '.';
                            }
                        }
                        else if (mapTiles[yPos][xPos] == 'E') // checks if player is on the finish
                        {
                            mapTiles[yPos][xPos] = '@';
                            mapTiles[prevYPos][prevXPos] = '.';
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("You defeated the dungeon crawler with " + gold +" gold! :)");
                            gameEnd = true; // used to end game when player reaches E
                        }
                        else if (mapTiles[yPos][xPos] == '#') 
                        {
                            yPos = prevYPos; // prevents player from moving onto walls
                        }
                        else if (mapTiles[yPos][xPos] == 'G') // allows player to move onto gold
                        {
                            mapTiles[yPos][xPos] = '@';
                            mapTiles[prevYPos][prevXPos] = '.';
                            wasGold = true; // used to keep track of when player coords are over gold
                        }
                        else if (mapTiles[yPos][xPos] == 'M') // if player collides with monster, remove 1 health
                        {
                            yPos = prevYPos;
                            health -= 1;
                        }
                        else
                        {
                            xPos = prevXPos;
                        }

                    }
                    else if (action == PlayerActions.SOUTH)
                    {
                        GetPlayerPosition();
                        prevYPos = yPos;
                        prevXPos = xPos;
                        correctMove = true;
                        yPos += 1;

                        if (mapTiles[yPos][xPos] == '.')
                        {
                            mapTiles[yPos][xPos] = '@';
                            if (wasGold == true)
                            {
                                mapTiles[prevYPos][prevXPos] = 'G';
                                wasGold = false;
                            }
                            else
                            {
                                mapTiles[prevYPos][prevXPos] = '.';
                            }
                        }
                        else if (mapTiles[yPos][xPos] == 'E')
                        {
                            mapTiles[yPos][xPos] = '@';
                            mapTiles[prevYPos][prevXPos] = '.';
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("You defeated the dungeon crawler with " + gold +" gold! :)");
                            gameEnd = true;
                        }
                        else if (mapTiles[yPos][xPos] == '#')
                        {
                            yPos = prevYPos;
                        }
                        else if (mapTiles[yPos][xPos] == 'G')
                        {
                            mapTiles[yPos][xPos] = '@';
                            mapTiles[prevYPos][prevXPos] = '.';
                            wasGold = true;
                        }
                        else if (mapTiles[yPos][xPos] == 'M')
                        {
                            yPos = prevYPos;
                            health -= 1;
                        }
                        else
                        {
                            xPos = prevXPos;
                        }

                    }
                    else if (action == PlayerActions.EAST)
                    {
                        GetPlayerPosition();
                        prevYPos = yPos;
                        prevXPos = xPos;
                        correctMove = true;
                        xPos += 1;

                        if (mapTiles[yPos][xPos] == '.')
                        {
                            mapTiles[yPos][xPos] = '@';
                            if (wasGold == true)
                            {
                                mapTiles[prevYPos][prevXPos] = 'G';
                                wasGold = false;
                            }
                            else
                            {
                                mapTiles[prevYPos][prevXPos] = '.';
                            }
                        }
                        else if (mapTiles[yPos][xPos] == 'E')
                        {
                            mapTiles[yPos][xPos] = '@';
                            mapTiles[prevYPos][prevXPos] = '.';
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("You defeated the dungeon crawler with " + gold +" gold! :)");
                            gameEnd = true; 
                        }
                        else if (mapTiles[yPos][xPos] == '#')
                        {
                            xPos = prevXPos; 
                        }
                        else if (mapTiles[yPos][xPos] == 'G') 
                        {
                            mapTiles[yPos][xPos] = '@';
                            mapTiles[prevYPos][prevXPos] = '.';
                            wasGold = true; 
                        }
                        else if (mapTiles[yPos][xPos] == 'M')
                        {
                            xPos = prevXPos;
                            health -= 1;
                        }
                        else
                        {
                            xPos = prevXPos;
                        }
                    }
                    else if (action == PlayerActions.WEST)
                    {
                        GetPlayerPosition();
                        prevYPos = yPos;
                        prevXPos = xPos;
                        correctMove = true;
                        
                        xPos -= 1;

                        if (mapTiles[yPos][xPos] == '.')
                        {
                            mapTiles[yPos][xPos] = '@';
                            if (wasGold == true)
                            {
                                mapTiles[prevYPos][prevXPos] = 'G';
                                wasGold = false;
                            }
                            else
                            {
                                mapTiles[prevYPos][prevXPos] = '.';
                            }
                        }
                        else if (mapTiles[yPos][xPos] == 'E')
                        {
                            mapTiles[yPos][xPos] = '@';
                            mapTiles[prevYPos][prevXPos] = '.';
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("You defeated the dungeon crawler with " + gold +" gold! :)");
                            gameEnd = true;
                        }
                        else if (mapTiles[yPos][xPos] == '#')
                        {
                            xPos = prevXPos;
                        }
                        else if (mapTiles[yPos][xPos] == 'G')
                        {
                            mapTiles[yPos][xPos] = '@';
                            mapTiles[prevYPos][prevXPos] = '.';
                            wasGold = true;
                        }
                        else if (mapTiles[yPos][xPos] == 'M')
                        {
                            xPos = prevXPos;
                            health -= 1;
                        }
                        else
                        {
                            xPos = prevXPos;
                        }
                    }
                    else if (action == PlayerActions.ATTACK)
                    {
                        GetPlayerPosition();
                        if (mapTiles[yPos + 1][xPos] == 'M') { mapTiles[yPos + 1][xPos] = '.'; }
                        if (mapTiles[yPos - 1][xPos] == 'M') { mapTiles[yPos - 1][xPos] = '.'; }
                        if (mapTiles[yPos][xPos + 1] == 'M') { mapTiles[yPos][xPos + 1] = '.'; }
                        if (mapTiles[yPos][xPos - 1] == 'M') { mapTiles[yPos][xPos - 1] = '.'; }
                    }
                    else if (action == PlayerActions.PICKUP) // checks surrounding tiles which are G when pickup is triggered
                    {
                        GetPlayerPosition();
                        if (mapTiles[yPos + 1][xPos] == 'G') // changes gold tiles to empty tiles after pickup
                        {
                            mapTiles[yPos + 1][xPos] = '.';
                            gold += 1;
                        }
                        if (mapTiles[yPos - 1][xPos] == 'G')
                        {
                            mapTiles[yPos -1][xPos] = '.';
                            gold += 1;
                        }
                        if (mapTiles[yPos][xPos + 1] == 'G')
                        {
                            mapTiles[yPos][xPos + 1] = '.';
                            gold += 1;
                        }
                        if (mapTiles[yPos][xPos - 1] == 'G')
                        {
                            mapTiles[yPos][xPos - 1] = '.';
                            gold += 1;
                        }
                    }
                    if (correctMove == false)
                    {
                        action = PlayerActions.NOTHING;
                    }
                    if (gameEnd == true)
                    {
                        isPlaying = false;
                        action = PlayerActions.QUIT;
                    }
                }
                
                catch (Exception)
                {
                    xPos = prevXPos;
                    yPos = prevYPos;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Game now playing:");
                Console.WriteLine(" - Use W, A, S, and D to move the player ('@')");
                Console.WriteLine(" - Use Spacebar when standing next to a monster ('M') to attack");
                Console.WriteLine(" - Use E when standing next to a piece of gold ('G') to pickup the gold");
                Console.WriteLine(" ");
                Console.ResetColor();
                for (int i = 0; i < mapTiles.Length; i++)
                {
                    Console.WriteLine(mapTiles[i]);
                }
                Console.WriteLine("  ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Gold: " + gold);
                Console.ForegroundColor = ConsoleColor.Red;
                if (health == 3) { Console.WriteLine("Health: ♥ ♥ ♥"); }
                if (health == 2) { Console.WriteLine("Health: ♥ ♥"); }
                if (health == 1) { Console.WriteLine("Health: ♥"); }
                if (health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("You died due to 0 health :'(");
                    action = PlayerActions.QUIT;
                }
                Console.ResetColor();
            }

        
        }

        /**
        * Map and GameState get initialized
        * mapName references a file name 
        * 
        * Create a private object variable for storing the map in Crawler and using it in the game.
        */
        public bool InitializeMap(String mapName)
        {
            bool initSuccess = false;

            // Your code here
            try
            {
                mapFile = File.ReadAllLines(@"./maps/" + mapName); // stores string array of map
                mapFile2 = new char[mapFile.Length][]; // original map
                mapTiles = new char[mapFile.Length][]; // map which is updated during gameplay
                for (int lines = 0; lines < mapFile.Length; lines++)
                {
                    mapFile2[lines] = mapFile[lines].ToCharArray();
                    mapTiles[lines] = mapFile[lines].ToCharArray();

                }
                
                for (int y = 0; y < mapTiles.Length; y++)
                {
                    for (int x = 0; x < mapTiles[y].Length; x++)
                    {
                        if (mapTiles[y][x] == 'S') // replaces start point with player ("@")
                        {
                            mapTiles[y][x] = '@';
                            mapFile2[y][x] = '@';
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(mapName + " is successfully loaded, type 'play' to start the game");
                Console.ResetColor();
                initSuccess = true;
            }
            catch (Exception)
            {
                initSuccess = false;
            }
            return initSuccess;
            
        }

        /**
         * Returns a representation of the currently loaded map
         * before any move was made.
         */
        public char[][] GetOriginalMap()
        {
            char[][] map = new char[0][];

            // Your code here
            

            return mapFile2;
        }

        /*
         * Returns the current map state 
         * without altering it 
         */
        public char[][] GetCurrentMapState()
        {
            // the map should be map[y][x]
            char[][] map = new char[0][];

            // Your code here
            


            return mapTiles;
        }

        /**
         * Returns the current position of the player on the map
         * 
         * The first value is the x corrdinate and the second is the y coordinate on the map
         */
        public int[] GetPlayerPosition()
        {
            int[] position = { 0, 0 };

            // Your code here
            for (int y = 0; y < mapTiles.Length; y++) // searches running map for the player ("@") and stores coordinates in variables xPos and yPos, for use in othe methods
            {
                for (int x = 0; x < mapTiles[y].Length; x++)
                {
                    if (mapTiles[y][x] == '@')
                    {
                        position[0] = x;
                        position[1] = y;
                        xPos = x;
                        yPos = y;
                    }
                }

            }
            return position;
        }

        /**
        * Returns the next player action
        * 
        * This method does not alter any internal state
        */
        public int GetPlayerAction()
        {
            
            // Your code here
            return (int)action;
        }


        public bool GameIsRunning()
        {
            bool running = false;
            // Your code here 
            if (isPlaying == true) 
            {
                running = true;
            }
            return running;
        }

        /**
         * Main method and Entry point to the program
         * ####
         * Do not change! 
        */
        static void Main(string[] args)
        {
            CMDCrawler crawler = new CMDCrawler();
            string input = string.Empty;
            Console.WriteLine("Welcome to the Commandline Dungeon!" +Environment.NewLine+ 
                "May your Quest be filled with riches!"+Environment.NewLine);
            
            // Loops through the input and determines when the game should quit
            while (crawler.active && crawler.action != PlayerActions.QUIT)
            {
                Console.WriteLine("Your Command: ");
                input = crawler.ReadUserInput();
                Console.WriteLine(Environment.NewLine);

                crawler.ProcessUserInput(input);
            
                crawler.GameLoop(crawler.active);
            }

            Console.WriteLine("See you again" +Environment.NewLine+ 
                "In the CMD Dungeon! ");


        }


    }
}
