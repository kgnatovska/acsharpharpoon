using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Cruiser : Ship
    {
        public Cruiser(uint x, uint y) : this(x, y, Directions.Horizontal)
        {
        }

        public Cruiser(uint x, uint y, Directions direction) : base(x, y, 2, direction)
        {
        }

        public override bool Equals(object obj)
        {
            Cruiser cruiser = obj as Cruiser;
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            return X == cruiser.X && Y == cruiser.Y && Direction == cruiser.Direction;
        }

        public bool Equals(Cruiser cruiser)
        {
            if (cruiser == null)
                return false;
            return X == cruiser.X && Y == cruiser.Y && Direction == cruiser.Direction;
        }

        public static bool operator ==(Cruiser a, Cruiser b)
        {
            if (object.ReferenceEquals(a, b))
                return true;
            return a.X == b.X && a.Y == b.Y && a.Direction == b.Direction;
        }

        public static bool operator !=(Cruiser a, Cruiser b)
        {
            return !(a == b);
        }
    }
}
