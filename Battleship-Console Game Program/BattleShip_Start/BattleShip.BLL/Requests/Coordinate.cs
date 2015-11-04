using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;



public class Coordinate
{
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }

    public Coordinate(int x, int y)
    {
        XCoordinate = x;
        YCoordinate = y;
    }


    public override bool Equals(object obj)
    {
        Coordinate otherCoordinate = obj as Coordinate;

        if (otherCoordinate == null)
            return false;

        return otherCoordinate.XCoordinate == this.XCoordinate &&
               otherCoordinate.YCoordinate == this.YCoordinate;
    }

    public override int GetHashCode()
    {
        string uniqueHash = this.XCoordinate.ToString() + this.YCoordinate.ToString() + "00";
        return (Convert.ToInt32(uniqueHash));
    }

}



