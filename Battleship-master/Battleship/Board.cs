using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Board
    {
        private List<Ship> _shipsOnBoard = new List<Ship>();
        private int _patrolBoatCount = 4;
        private int _cruiserCount = 3;
        private int _submarineCount = 2;
        private int _aircraftCarrierCount = 1;
        public void Add(Ship ship)
        {
            AddShip(ship);
            switch (ship.GetType().ToString())
            {
                case "Battleship.PatrolBoat":
                    _patrolBoatCount -= 1;
                    break;
                case "Battleship.Cruiser":
                    _cruiserCount -= 1;
                    break;
                case "Battleship.Submarine":
                    _submarineCount -= 1;
                    break;
                case "Battleship.AircraftCarrier":
                    _aircraftCarrierCount -= 1;
                    break;
            }
        }

        public void Add(string notation)
        {
            Ship position = Ship.Parse(notation);
            AddShip(position);
        }

        protected void AddShip(Ship ship)
        {
            if (!ship.FitsInSquare(10, 10))
                throw new ArgumentOutOfRangeException();
            foreach (var itemOnBoard in _shipsOnBoard)
            {
                if (ship.OverlapsWith(itemOnBoard))
                    throw new ShipOverlapException($"Ship {itemOnBoard} overlaps with {ship}");
            }
            _shipsOnBoard.Add(ship);
        }

        public void Validate()
        {
            if (GetAll().Count < 10)
                throw new BoardIsNotReadyException($"There is not sufficient count of ships. We need: PatrolBoat ({_patrolBoatCount}), Cruiser ({_cruiserCount}), Submarine ({_submarineCount}), AircraftCarrier ({_aircraftCarrierCount})");
        }

        public List<Ship> GetAll()
        {
            return _shipsOnBoard;
        }
    }
}
