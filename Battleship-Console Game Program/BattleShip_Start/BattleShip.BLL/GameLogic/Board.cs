using System;
using System.Collections.Generic;
using System.Linq;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.BLL.GameLogic
{
    public class Board
    {
        public Dictionary<Coordinate, ShotHistory> ShotHistory;
        private Ship[] _ships;
        private int _currentShipIndex;
        ShotStatus shotStatus = new ShotStatus();
        

        public string[,] _boardarray = new string[11, 11] { { " ", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, { "A" , " "  , " "  , " "  , " " , " "  , " " , " " , " " , " " , " " },{ "B" , " " , " " , " " , " " , " " , " " , " " , " " , " " , " " }, { "C" , " "  , " "  , " "  , " " , " "  , " " , " " , " " , " " , " " },
        { "D" , " "  , " "  , " "  , " " , " "  , " " , " " , " " , " " , " " }, { "E" , " "  , " "  , " "  , " " , " "  , " " , " " , " " , " " , " " }, { "F" , " "  , " "  , " "  , " " , " "  , " " , " " , " " , " " , " " }, { "G" , " "  , " "  , " "  , " " , " "  , " " , " " , " " , " " , " " }, { "H" , " "  , " "  , " "  , " " , " "  , " " , " " , " " , " " , " " },
        { "I" , " "  , " "  , " "  , " " , " "  , " " , " " , " " , " " , " " }, { "J" , " "  , " "  , " "  , " " , " "  , " " , " " , " " , " " , " " } };




        public Board()
        {
            ShotHistory = new Dictionary<Coordinate, ShotHistory>();
            _ships = new Ship[5];
            _currentShipIndex = 0;//dont worry
        }

        public FireShotResponse FireShot(Coordinate coordinate)
        {
            var response = new FireShotResponse();

            // is this coordinate on the board?
            if (!IsValidCoordinate(coordinate))
            {
                response.ShotStatus = ShotStatus.Invalid;
                return response;
            }

            // did they already try this position?
            if (ShotHistory.ContainsKey(coordinate))
            {
                response.ShotStatus = ShotStatus.Duplicate;
                return response;
            }

            CheckShipsForHit(coordinate, response);
            CheckForVictory(response);

            return response;            
        }

        private void CheckForVictory(FireShotResponse response)
        {
            if (response.ShotStatus == ShotStatus.HitAndSunk)
            {
                // did they win?
                if (_ships.All(s => s.IsSunk))
                    response.ShotStatus = ShotStatus.Victory;
            }
        }

        private void CheckShipsForHit(Coordinate coordinate, FireShotResponse response)
        {
            response.ShotStatus = ShotStatus.Miss;

            foreach (var ship in _ships)
            {
                // no need to check sunk ships

                if (ship.IsSunk)
                    continue;

                ShotStatus status = ship.FireAtShip(coordinate);

                switch (status)
                {
                    case ShotStatus.HitAndSunk:
                        response.ShotStatus = ShotStatus.HitAndSunk;
                        response.ShipImpacted = ship.ShipName;
                        ShotHistory.Add(coordinate, Responses.ShotHistory.Hit);
                        break;
                    case ShotStatus.Hit:
                        response.ShotStatus = ShotStatus.Hit;
                        response.ShipImpacted = ship.ShipName;
                        ShotHistory.Add(coordinate, Responses.ShotHistory.Hit);
                        //if (ShotStatus.Hit == ShotHistory) ;
                    
                        break;
                }

                // if they hit something, no need to continue looping
                if (status != ShotStatus.Miss)
                    break;
            }

            if (response.ShotStatus == ShotStatus.Miss)
            {
                ShotHistory.Add(coordinate, Responses.ShotHistory.Miss);
            }
        }

        private bool IsValidCoordinate(Coordinate coordinate)
        {
            return coordinate.XCoordinate >= 1 && coordinate.XCoordinate <= 10 &&
            coordinate.YCoordinate >= 1 && coordinate.YCoordinate <= 10;
        }

        public ShipPlacement PlaceShip(PlaceShipRequest request)
        {
            if (_currentShipIndex > 4)
                throw new Exception("You can not add another ship, 5 is the limit!");

            if (!IsValidCoordinate(request.Coordinate))
                return ShipPlacement.NotEnoughSpace;

            Ship newShip = ShipCreator.CreateShip(request.ShipType);
            switch (request.Direction)
            {
                case ShipDirection.Down:
                    return PlaceShipDown(request.Coordinate, newShip);
                case ShipDirection.Up:
                    return PlaceShipUp(request.Coordinate, newShip);
                case ShipDirection.Left:
                    return PlaceShipLeft(request.Coordinate, newShip);
                default:
                    return PlaceShipRight(request.Coordinate, newShip);
            }

        }

        private ShipPlacement PlaceShipRight(Coordinate coordinate, Ship newShip)
        {
            // x coordinate gets bigger
            int positionIndex = 0;
            int maxX = coordinate.XCoordinate + newShip.BoardPositions.Length;

            for (int i = coordinate.XCoordinate; i < maxX; i++)
            {
                var currentCoordinate = new Coordinate(i, coordinate.YCoordinate);

                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacement.NotEnoughSpace;

                if (OverlapsAnotherShip(currentCoordinate))
                    return ShipPlacement.Overlap;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacement.Ok;
        }

        private ShipPlacement PlaceShipLeft(Coordinate coordinate, Ship newShip)
        {
            // x coordinate gets smaller
            int positionIndex = 0;
            int minX = coordinate.XCoordinate - newShip.BoardPositions.Length;

            for (int i = coordinate.XCoordinate; i > minX; i--)
            {
                var currentCoordinate = new Coordinate(i, coordinate.YCoordinate);

                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacement.NotEnoughSpace;

                if (OverlapsAnotherShip(currentCoordinate))
                    return ShipPlacement.Overlap;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacement.Ok;
        }

        private ShipPlacement PlaceShipUp(Coordinate coordinate, Ship newShip)
        {
            // y coordinate gets smaller
            int positionIndex = 0;
            int minY = coordinate.YCoordinate - newShip.BoardPositions.Length;

            for (int i = coordinate.YCoordinate; i > minY; i--)
            {
                var currentCoordinate = new Coordinate(coordinate.XCoordinate, i);

                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacement.NotEnoughSpace;

                if (OverlapsAnotherShip(currentCoordinate))
                    return ShipPlacement.Overlap;

                newShip.BoardPositions[positionIndex] = currentCoordinate; 
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacement.Ok;
        }

        private ShipPlacement PlaceShipDown(Coordinate coordinate, Ship newShip)
        {
            // y coordinate gets bigger
            int positionIndex = 0;
            int maxY = coordinate.YCoordinate + newShip.BoardPositions.Length;
            
            for (int i = coordinate.YCoordinate; i < maxY; i++)
            {
                var currentCoordinate = new Coordinate(coordinate.XCoordinate, i);
                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacement.NotEnoughSpace;

                if (OverlapsAnotherShip(currentCoordinate))
                    return ShipPlacement.Overlap;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacement.Ok;
        }

        private void AddShipToBoard(Ship newShip)
        {
            _ships[_currentShipIndex] = newShip;
            _currentShipIndex++;
        }

        private bool OverlapsAnotherShip(Coordinate coordinate)
        {
            foreach (var ship in _ships)
            {
                if (ship != null)
                {
                    if (ship.BoardPositions.Contains(coordinate))
                        return true;
                }
            }

            return false;
        }

        public void Display()
        {
            
                Console.Clear();
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[0, 0], _boardarray[0, 1], _boardarray[0, 2], _boardarray[0, 3], _boardarray[0, 4], _boardarray[0, 5], _boardarray[0, 6], _boardarray[0, 7], _boardarray[0, 8], _boardarray[0, 9], _boardarray[0, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[1, 0], _boardarray[1, 1], _boardarray[1, 2], _boardarray[1, 3], _boardarray[1, 4], _boardarray[1, 5], _boardarray[1, 6], _boardarray[1, 7], _boardarray[1, 8], _boardarray[1, 9], _boardarray[1, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[2, 0], _boardarray[2, 1], _boardarray[2, 2], _boardarray[2, 3], _boardarray[2, 4], _boardarray[2, 5], _boardarray[2, 6], _boardarray[2, 7], _boardarray[2, 8], _boardarray[2, 9], _boardarray[2, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[3, 0], _boardarray[3, 1], _boardarray[3, 2], _boardarray[3, 3], _boardarray[3, 4], _boardarray[3, 5], _boardarray[3, 6], _boardarray[3, 7], _boardarray[3, 8], _boardarray[3, 9], _boardarray[3, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[4, 0], _boardarray[4, 1], _boardarray[4, 2], _boardarray[4, 3], _boardarray[4, 4], _boardarray[4, 5], _boardarray[4, 6], _boardarray[4, 7], _boardarray[4, 8], _boardarray[4, 9], _boardarray[4, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[5, 0], _boardarray[5, 1], _boardarray[5, 2], _boardarray[5, 3], _boardarray[5, 4], _boardarray[5, 5], _boardarray[5, 6], _boardarray[5, 7], _boardarray[5, 8], _boardarray[5, 9], _boardarray[5, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[6, 0], _boardarray[6, 1], _boardarray[6, 2], _boardarray[6, 3], _boardarray[6, 4], _boardarray[6, 5], _boardarray[6, 6], _boardarray[6, 7], _boardarray[6, 8], _boardarray[6, 9], _boardarray[6, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[7, 0], _boardarray[7, 1], _boardarray[7, 2], _boardarray[7, 3], _boardarray[7, 4], _boardarray[7, 5], _boardarray[7, 6], _boardarray[7, 7], _boardarray[7, 8], _boardarray[7, 9], _boardarray[7, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[8, 0], _boardarray[8, 1], _boardarray[8, 2], _boardarray[8, 3], _boardarray[8, 4], _boardarray[8, 5], _boardarray[8, 6], _boardarray[8, 7], _boardarray[8, 8], _boardarray[8, 9], _boardarray[8, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[9, 0], _boardarray[9, 1], _boardarray[9, 2], _boardarray[9, 3], _boardarray[9, 4], _boardarray[9, 5], _boardarray[9, 6], _boardarray[9, 7], _boardarray[9, 8], _boardarray[9, 9], _boardarray[9, 10]);
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} |", _boardarray[10, 0], _boardarray[10, 1], _boardarray[10, 2], _boardarray[10, 3], _boardarray[10, 4], _boardarray[10, 5], _boardarray[10, 6], _boardarray[10, 7], _boardarray[10, 8], _boardarray[10, 9], _boardarray[10, 10]);
           


        }
    }
}
