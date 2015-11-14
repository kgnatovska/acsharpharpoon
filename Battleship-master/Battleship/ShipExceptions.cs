using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class NotAShipException : Exception
    {
    }

    public class ShipOverlapException : Exception
    {
        public ShipOverlapException(string message) : base(message)
        { }
    }

    public class BoardIsNotReadyException : Exception
    {
        public BoardIsNotReadyException(string message) : base(message)
        { }
    }
}
