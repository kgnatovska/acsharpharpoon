using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class AircraftCarrier : Ship
    {
        public AircraftCarrier(uint x, uint y) : this(x, y, Directions.Horizontal)
        {
        }

        public AircraftCarrier(uint x, uint y, Directions direction) : base(x, y, 4, direction)
        {
        }

        public override bool Equals(object obj)
        {
            AircraftCarrier aircraftCarrier = obj as AircraftCarrier;
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            return X == aircraftCarrier.X && Y == aircraftCarrier.Y && Direction == aircraftCarrier.Direction;
        }

        public bool Equals(AircraftCarrier aircraftCarrier)
        {
            if (aircraftCarrier == null)
                return false;
            return X == aircraftCarrier.X && Y == aircraftCarrier.Y && Direction == aircraftCarrier.Direction;
        }

        public static bool operator ==(AircraftCarrier a, AircraftCarrier b)
        {
            if (object.ReferenceEquals(a, b))
                return true;
            return a.X == b.X && a.Y == b.Y && a.Direction == b.Direction;
        }

        public static bool operator !=(AircraftCarrier a, AircraftCarrier b)
        {
            return !(a == b);
        }
    }
}
