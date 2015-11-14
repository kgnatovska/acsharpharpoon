using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Submarine : Ship
    {
        public Submarine(uint x, uint y) : this(x, y, Directions.Horizontal)
        {
        }

        public Submarine(uint x, uint y, Directions direction) : base(x, y, 3, direction)
        {
        }

        public override bool Equals(object obj)
        {
            Submarine submarine = obj as Submarine;
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            return X == submarine.X && Y == submarine.Y && Direction == submarine.Direction;
        }

        public bool Equals(Submarine submarine)
        {
            if (submarine == null)
                return false;
            return X == submarine.X && Y == submarine.Y && Direction == submarine.Direction;
        }

        public static bool operator ==(Submarine a, Submarine b)
        {
            if (object.ReferenceEquals(a, b))
                return true;
            return a.X == b.X && a.Y == b.Y && a.Direction == b.Direction;
        }

        public static bool operator !=(Submarine a, Submarine b)
        {
            return !(a == b);
        }
    }
}
