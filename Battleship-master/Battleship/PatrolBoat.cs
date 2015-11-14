using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class PatrolBoat : Ship
    {
        public PatrolBoat(uint x, uint y) : this(x, y, Directions.Horizontal)
        {
        }

        public PatrolBoat(uint x, uint y, Directions direction) : base(x, y, 1, direction)
        {
        }

        public override bool Equals(object obj)
        {
            PatrolBoat boat = obj as PatrolBoat;
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            return X == boat.X && Y == boat.Y;
        }

        public bool Equals(PatrolBoat boat)
        {
            if (boat == null)
                return false;
            return X == boat.X && Y == boat.Y;
        }

        public static bool operator ==(PatrolBoat a, PatrolBoat b)
        {
            if (object.ReferenceEquals(a, b))
                return true;
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(PatrolBoat a, PatrolBoat b)
        {
            return !(a == b);
        }
    }
}
