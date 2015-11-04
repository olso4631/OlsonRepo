using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using BattleShip.BLL.GameLogic;

namespace BattleShip.UI
{
    class GameManager
    {
        private Player player1;
        private Player player2;
        private Player currentPlayer;
        private Player opponentPlayer;
        PlaceShipRequest shipRequest = new PlaceShipRequest();
        bool gameover = false;


        public void StartGame()
        {
            Console.WriteLine("Welcome to Battleship! Press Enter When Ready!");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Great, lets begin! What is your name Player 1?");
            string P1 = Console.ReadLine();
            player1 = CreatePlayer(P1);
            player1.number = 1;

            Console.WriteLine("What is your name Player 2?");
            string P2 = Console.ReadLine();
            player2 = CreatePlayer(P2);
            player2.number = 2;

            SetUpBoard();

        }

        private void SetUpBoard()
        {
            Console.Clear();
            NextPlayer();
            askshipspot();
            NextPlayer();
            askshipspot();
            NextPlayer();
            ProcessTurns();
        }

        private void ProcessTurns()
        {
            bool again = true;
            do
            {
                do
                {
                    opponentPlayer.theirBoard.Display();
                    gameover = UserFireShotPrompt();
                    NextPlayer();
                } while (!gameover);

                Console.WriteLine("You've Won, {0}! CONGRATS!!!", opponentPlayer.name);
                Console.ReadLine();
                Console.WriteLine("Would you like to play again?");
                string playAgain = Console.ReadLine().ToUpper();
                if (playAgain.Substring(0, 1) == "Y")
                {
                    Console.Clear();
                    again = false;
                   
                    SetUpBoard();
                }


            } while (!again);
        }

        private bool UserFireShotPrompt()
        {
            bool validShot = false;
            bool validLength = false;
            int xNum = 0;
            int yNum = 0;


            do
            {
                Console.WriteLine("Where would you like to  fire, {0}", currentPlayer.name);
                string playerInput = Console.ReadLine().ToUpper();
                if (validLength = (playerInput.Length >= 2 && playerInput.Length <= 3))
                {

                    string letter = playerInput.Substring(0, 1);
                    string number = (playerInput.Substring(1));
                    xNum = ParseNumber(number);
                    yNum = ParseLetter(letter);
                    if (xNum <= 10)
                    {

                        if ((validShot = (letter == "A" || letter == "B" || letter == "C" || letter == "D" || letter == "E" || letter == "F" || letter == "G" || letter == "H" || letter == "I" || letter == "J") && (number == "1" || number == "2" || number == "3" || number == "4" || number == "5" || number == "6" || number == "7" || number == "8" || number == "9" || number == "10")))
                        {
                            Coordinate coordinate = new Coordinate(xNum, yNum);
                            coordinate.XCoordinate = xNum;
                            coordinate.YCoordinate = yNum;
                            string H = "H";
                            string M = "M";

                            FireShotResponse fireResponse = opponentPlayer.theirBoard.FireShot(coordinate);
                            if (fireResponse.ShotStatus == ShotStatus.Hit)
                            {
                                Console.WriteLine("DIRECT HIT!");
                                Console.ReadLine();
                                H = "H";
                                opponentPlayer.theirBoard._boardarray[yNum, xNum] = H;
                                return false;
                            }
                            if (fireResponse.ShotStatus == ShotStatus.Miss)
                            {
                                Console.WriteLine("You've missed!");
                                Console.ReadLine();
                                opponentPlayer.theirBoard._boardarray[yNum, xNum] = M;
                                return false;

                            }
                            if (fireResponse.ShotStatus == ShotStatus.Duplicate)
                            {
                                Console.WriteLine("That was a duplicate shot, Try again!");
                                Console.ReadLine();
                                validLength = false;

                            }
                            if (fireResponse.ShotStatus == ShotStatus.HitAndSunk)
                            {
                                Console.WriteLine("You've sunk their ship! Congrats!");
                                Console.ReadLine();
                                opponentPlayer.theirBoard._boardarray[yNum, xNum] = H;
                                return false;
                            }
                            if (fireResponse.ShotStatus == ShotStatus.Invalid)
                            {
                                Console.WriteLine("That was an invalid shot, do better!");
                                validLength = false;
                            }
                            if (fireResponse.ShotStatus == ShotStatus.Victory)
                            {
                                opponentPlayer.theirBoard._boardarray[yNum, xNum] = H;
                                return true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("That was not a valid chioce, please be better!");
                            validLength = false;
                        }
                    }
                    else

                    {
                        Console.WriteLine("That was not a valid shot, please try again! ");
                        validLength = false;
                    }
                }
                else
                {
                    Console.WriteLine("That was an invalid shot, please try again!");
                }
            } while (!validShot || !validLength);
            return false;
        }

        public int ParseNumber(string number)
        {
            switch (number)
            {
                case "1":
                    return 1;
                case "2":
                    return 2;
                case "3":
                    return 3;
                case "4":
                    return 4;
                case "5":
                    return 5;
                case "6":
                    return 6;
                case "7":
                    return 7;
                case "8":
                    return 8;
                case "9":
                    return 9;
                case "10":
                    return 10;
                default:
                    return 100;
            }
        }

        private int ParseLetter(string letter)
        {

            switch (letter)
            {

                case "A":
                    return 1;
                case "B":
                    return 2;
                case "C":
                    return 3;
                case "D":
                    return 4;
                case "E":
                    return 5;
                case "F":
                    return 6;
                case "G":
                    return 7;
                case "H":
                    return 8;
                case "I":
                    return 9;
                case "J":
                    return 10;
                default: return -100;
            }
        }


        public void askshipspot()
        {
            Console.Clear();
            ShipPlacement placement;
            bool validCoord = false;
            bool validLength = false;

            do
            {
                Console.WriteLine("Where would you like your starting Submarine coordinate to be, {0}", currentPlayer.name);
                string PlayerInput = Console.ReadLine().ToUpper();
                if (validLength = (PlayerInput.Length > 0))
                {
                    string letter = PlayerInput.Substring(0, 1);
                    string number = PlayerInput.Substring(1);
                    ShipType shiptype = new ShipType();
                    shiptype = ShipType.Submarine;


                    if (validCoord = (letter == "A" || letter == "B" || letter == "C" || letter == "D" || letter == "E" || letter == "F" || letter == "G" || letter == "H" || letter == "I" || letter == "J") && (number == "1" || number == "2" || number == "3" || number == "4" || number == "5" || number == "6" || number == "7" || number == "8" || number == "9" || number == "10"))
                    {

                        int xNum = ParseLetter(letter);
                        int yNum = ParseNumber(number);

                        if (yNum <= 10)
                        {
                            Coordinate coord = new Coordinate(yNum, xNum);
                            shipRequest.Coordinate = coord;

                            ShipDirection shipDirection = new ShipDirection();
                            shipDirection = directions();
                            shipRequest.Direction = shipDirection;
                            shipRequest.ShipType = shiptype;
                            placement = currentPlayer.theirBoard.PlaceShip(shipRequest);

                            if (placement == ShipPlacement.NotEnoughSpace)
                            {
                                Console.WriteLine("Crap shot placement, not enough space, choose wiser!");
                                validCoord = false;
                            }
                            if (placement == ShipPlacement.Overlap)
                            {
                                Console.WriteLine("Can't do that, you already have a boat there, choose wiser!");
                                validCoord = true;
                            }
                        }
                        else
                        {
                            validCoord = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("That was not a valid input!");
                        validCoord = false;
                    }
                }
                else
                {
                    Console.WriteLine("That was not a valid input, do better!");

                }

            } while (!validCoord || !validLength);

            do
            {
                Console.WriteLine("Where would you like your starting Destroyer coordinate to be, {0}", currentPlayer.name);
                string PlayerInput = Console.ReadLine().ToUpper();
                if (validLength = (PlayerInput.Length > 0))
                {
                    string letter = PlayerInput.Substring(0, 1);
                    string number = PlayerInput.Substring(1);
                    ShipType shiptype = new ShipType();
                    shiptype = ShipType.Destroyer;
                    bool ship = false;

                    if (validCoord = (letter == "A" || letter == "B" || letter == "C" || letter == "D" || letter == "E" || letter == "F" || letter == "G" || letter == "H" || letter == "I" || letter == "J") && (number == "1" || number == "2" || number == "3" || number == "4" || number == "5" || number == "6" || number == "7" || number == "8" || number == "9" || number == "10"))
                    {
                        int xNum = ParseLetter(letter);
                        int yNum = ParseNumber(number);

                        Coordinate coord = new Coordinate(yNum, xNum);
                        shipRequest.Coordinate = coord;

                        ShipDirection shipDirection = new ShipDirection();
                        shipDirection = directions();
                        shipRequest.Direction = shipDirection;
                        shipRequest.ShipType = shiptype;
                        placement = currentPlayer.theirBoard.PlaceShip(shipRequest);

                        if (placement == ShipPlacement.NotEnoughSpace)
                        {
                            Console.WriteLine("Crap shot placement, enough space, choose wiser!");
                            validCoord = false;
                        }
                        if (placement == ShipPlacement.Overlap)
                        {
                            Console.WriteLine("Can't do that, you already have a boat there, choose wiser!");
                            validCoord = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("That was not a valid spot, do better!");
                    }
                }
                else
                {
                    Console.WriteLine("That was not a valid input!");
                    validCoord = false;

                }


            } while (!validCoord);

            do
            {
                Console.WriteLine("Where would you like your starting Carrier coordinate to be, {0}", currentPlayer.name);
                string playerInput = Console.ReadLine().ToUpper();
                if (validLength = (playerInput.Length > 0))
                {
                    string letter = playerInput.Substring(0, 1);
                    string number = playerInput.Substring(1);
                    ShipType shiptype = new ShipType();
                    shiptype = ShipType.Carrier;
                    bool ship = false;

                    if (validCoord = (letter == "A" || letter == "B" || letter == "C" || letter == "D" || letter == "E" || letter == "F" || letter == "G" || letter == "H" || letter == "I" || letter == "J") && (number == "1" || number == "2" || number == "3" || number == "4" || number == "5" || number == "6" || number == "7" || number == "8" || number == "9" || number == "10"))
                    {
                        int xNum = ParseLetter(letter);
                        int yNum = ParseNumber(number);

                        Coordinate coord = new Coordinate(yNum, xNum);
                        shipRequest.Coordinate = coord;

                        ShipDirection shipDirection = new ShipDirection();
                        shipDirection = directions();
                        shipRequest.Direction = shipDirection;
                        shipRequest.ShipType = shiptype;
                        placement = currentPlayer.theirBoard.PlaceShip(shipRequest);

                        if (placement == ShipPlacement.NotEnoughSpace)
                        {
                            Console.WriteLine("Crap shot placement, enough space, choose wiser!");
                            validCoord = false;
                        }
                        if (placement == ShipPlacement.Overlap)
                        {
                            Console.WriteLine("Can't do that, you already have a boat there, choose wiser!");
                            validCoord = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("That was not a valid spot, do better!");
                    }
                }
                else
                {
                    Console.WriteLine("That was not a valid input!");
                    validCoord = false;

                }

            } while (!validCoord);

            do
            {  
                Console.WriteLine("Where would you like your starting Battleship coordinate to be, {0}", currentPlayer.name);
                string playerInput = Console.ReadLine().ToUpper();
                if (validLength = (playerInput.Length > 0))
                {
                    string letter = playerInput.Substring(0, 1);
                    string number = playerInput.Substring(1);
                    ShipType shiptype = new ShipType();
                    shiptype = ShipType.Battleship;


                    if (validCoord = (letter == "A" || letter == "B" || letter == "C" || letter == "D" || letter == "E" || letter == "F" || letter == "G" || letter == "H" || letter == "I" || letter == "J") && (number == "1" || number == "2" || number == "3" || number == "4" || number == "5" || number == "6" || number == "7" || number == "8" || number == "9" || number == "10"))
                    {
                        int xNum = ParseLetter(letter);
                        int yNum = ParseNumber(number);

                        Coordinate coord = new Coordinate(yNum, xNum);
                        shipRequest.Coordinate = coord;

                        ShipDirection shipDirection = new ShipDirection();
                        shipDirection = directions();
                        shipRequest.Direction = shipDirection;
                        shipRequest.ShipType = shiptype;
                        placement = currentPlayer.theirBoard.PlaceShip(shipRequest);

                        if (placement == ShipPlacement.NotEnoughSpace)
                        {
                            Console.WriteLine("Crap shot placement, enough space, choose wiser!");
                            validCoord = false;
                        }
                        if (placement == ShipPlacement.Overlap)
                        {
                            Console.WriteLine("Can't do that, you already have a boat there, choose wiser!");
                            validCoord = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("That was not a valid spot, do better!");
                    }
                }
                else
                {
                    Console.WriteLine("That was not a valid input!");
                    validCoord = false;

                }

            } while (!validCoord);

            do
            {
                Console.WriteLine("Where would you like your starting Cruiser coordinate to be, {0}", currentPlayer.name);
                string playerInput = Console.ReadLine().ToUpper();
                if (validLength = (playerInput.Length > 0))
                {
                    string letter = playerInput.Substring(0, 1);
                    string number = playerInput.Substring(1);
                    ShipType shiptype = new ShipType();
                    shiptype = ShipType.Cruiser;

                    if (validCoord = (letter == "A" || letter == "B" || letter == "C" || letter == "D" || letter == "E" || letter == "F" || letter == "G" || letter == "H" || letter == "I" || letter == "J") && (number == "1" || number == "2" || number == "3" || number == "4" || number == "5" || number == "6" || number == "7" || number == "8" || number == "9" || number == "10"))
                    {
                        int xNum = ParseLetter(letter);
                        int yNum = ParseNumber(number);

                        Coordinate coord = new Coordinate(yNum, xNum);
                        shipRequest.Coordinate = coord;

                        ShipDirection shipDirection = new ShipDirection();
                        shipDirection = directions();
                        shipRequest.Direction = shipDirection;
                        shipRequest.ShipType = shiptype;
                        placement = currentPlayer.theirBoard.PlaceShip(shipRequest);

                        if (placement == ShipPlacement.NotEnoughSpace)
                        {
                            Console.WriteLine("Crap shot placement, enough space, choose wiser!");
                            validCoord = false;
                        }
                        if (placement == ShipPlacement.Overlap)
                        {
                            Console.WriteLine("Can't do that, you already have a boat there, choose wiser!");
                            validCoord = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("That was not a valid spot, do better!");
                    }
                }
                else
                {
                    Console.WriteLine("That was not a valid input!");
                    validCoord = false;
                }

            } while (!validCoord);

        }

        public ShipDirection directions()
        {
            bool validinput = false;
            while (!validinput)
            {

                Console.WriteLine("Which direction do you want it to face, Up, Down, Left, or Right?");
                string userInput = Console.ReadLine().ToUpper();

                if (validinput = userInput == "RIGHT" || userInput == "LEFT" || userInput == "DOWN" || userInput == "UP")
                {
                    string letter = userInput.Substring(0, 1);
                    if (letter == "U")
                    {
                        ShipDirection up = ShipDirection.Up;
                        return up;
                    }
                    if (letter == "D")
                    {
                        ShipDirection down = ShipDirection.Down;
                        return down;
                    }
                    if (letter == "L")
                    {
                        ShipDirection left = ShipDirection.Left;
                        return left;
                    }
                    if (letter == "R")
                    {
                        ShipDirection right = ShipDirection.Right;
                        return right;
                    }
                }
                else
                {
                    Console.WriteLine("That was not a valid input, please do better!");

                }
            }
            return 0;
        }

        public Player CreatePlayer(string player)
        {
            Player newplayer = new Player();
            newplayer.name = player;

            newplayer.theirBoard = new Board();
            return newplayer;
        }

        private void NextPlayer()
        {

            if (currentPlayer == null || currentPlayer.number == 2)
            {
                currentPlayer = player1;
                opponentPlayer = player2;
            }
            else
            {
                currentPlayer = player2;
                opponentPlayer = player1;
            }

        }
    }

}