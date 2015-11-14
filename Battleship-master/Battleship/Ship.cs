using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battleship
{
    public class Ship : IShip
    {
        public uint X { get; set; }
        public uint Y { get; set; }
        public uint EndX { get; set; }
        public uint EndY { get; set; }
        public uint Length { get; set; }
        public Directions Direction { get; set; }

        public Ship(uint x, uint y, uint length, Directions direction)
        {
            X = x;
            Y = y;
            Length = length;
            Direction = direction;
            if (Direction == Directions.Horizontal)
            {
                EndY = y;
                EndX = x + length - 1;
            }
            else
            {
                EndX = x;
                EndY = y + length - 1;
            }
        }

        protected static Dictionary<char, uint> LetterToX = new Dictionary<char, uint>
        {
            {'A', 1},
            {'B', 2},
            {'C', 3},
            {'D', 4},
            {'E', 5},
            {'F', 6},
            {'G', 7},
            {'H', 8},
            {'I', 9},
            {'J', 10}
        };

        protected static Dictionary<uint, char> XToLetter = new Dictionary<uint, char>()
        {
            {1, 'A'},
            {2, 'B'},
            {3, 'C'},
            {4, 'D'},
            {5, 'E'},
            {6, 'F'},
            {7, 'G'},
            {8, 'H'},
            {9, 'I'},
            {10, 'J'}
        };

        protected static Dictionary<Directions, string> DirectionToSymbol = new Dictionary<Directions, string>()
        {
            {Directions.Horizontal, "-" },
            {Directions.Vertiacal, "|" }
        };  

        protected static Dictionary<uint, string> shipTypes = new Dictionary<uint, string>

        {
            {1, "PatrolBoat"},
            {2, "Cruiser"},
            {3, "Submarine"},
            {4, "AircraftCarrier"}
        };

        protected static string oneDigitPattern = "^[A-J][1-9](x[1-4])?(-|\\|)?$"; //to match notation with 0-9 y position in TryParse/Parse
        protected static string twoDigitsPattern = "^[A-J]10(x[1-4])?(-|\\|)?$";   //to match notation with 10 y position in TryParse/Parse
        protected static Regex regexOneDigit = new Regex(oneDigitPattern);
        protected static Regex regexTwoDigits = new Regex(twoDigitsPattern);

        public static bool TryParse(string notation, out Ship pos)
        {
            pos = null;
            if (!regexOneDigit.IsMatch(notation) && !regexTwoDigits.IsMatch(notation))
            {
                return false;
            }
            else
            {
                pos = Parse(notation);
                return true;
            }    
        }

        public static Ship Parse(string notation)
        {
            if (!regexOneDigit.IsMatch(notation) && !regexTwoDigits.IsMatch(notation))
                throw new NotAShipException();
            uint x = LetterToX[notation[0]];                           //map numeric value to first char in the notation - x position
            uint y = Convert.ToUInt32(notation[1].ToString());    //set one digit number as y position
            if (regexTwoDigits.IsMatch(notation))
            {                                                     //overwride y position with 10 (the only possible
                y = Convert.ToUInt32(10);                        //two digits value) if notation matches two digits regex
            }   
            uint length = 1;                                      //set default length if it not given in the notation
            Directions direction = Directions.Horizontal;          //set default direction if it not given in the notation
            int lengthIndex;
            if (notation.Contains('x'))
            {
                lengthIndex = notation.IndexOf('x') + 1;                    //overwrite length by value given after 'x'
                length = Convert.ToUInt32(notation[lengthIndex].ToString());
            }
            if (notation.Contains('|'))
                direction = Directions.Vertiacal;                   //overwrite direction if it is given in the notation
            return WhatShipAmI(x, y, length, direction);
        }

        public static Ship WhatShipAmI(uint x, uint y, uint length, Directions direction) //returns ship object of appropriate type based on length
        {
            switch (length)
            {
                case 1:
                    return new PatrolBoat(x, y, direction);
                case 2:
                    return new Cruiser(x, y, direction);
                case 3:
                    return new Submarine(x, y, direction);
                default:
                    return new AircraftCarrier(x, y, direction);
            }
        }

        public bool FitsInSquare(byte squareHeight, byte squareWidth)
        {
            return EndX <= squareWidth && EndY <= squareHeight;
        }

        public bool OverlapsWith(Ship ship)
        {
            var rightship = IsRight(ship);
            var leftship = IsLeft(ship);
            var uppership = IsUpper(ship);
            var lowership = IsLower(ship);
            return Convert.ToInt32(rightship.X) - Convert.ToInt32(leftship.EndX) <= 1 && Convert.ToInt32(lowership.Y) - Convert.ToInt32(uppership.EndY) <= 1;
        }

        protected Ship IsUpper(Ship ship)
        {
            if (Y <= ship.Y)
                return this;
            return ship;
        }

        protected Ship IsLeft(Ship ship)
        {
            if (X <= ship.X)
                return this;
            return ship;
        }

        protected Ship IsRight(Ship ship)
        {
            if (X > ship.X)
                return this;
            return ship;
        }

        protected Ship IsLower(Ship ship)
        {
            if (Y > ship.Y)
                return this;
            return ship;
        }

        public override string ToString()
        {
            return string.Format($"{XToLetter[(char)X]}{Y}x{Length}{DirectionToSymbol[Direction]}");
        }

    }
}
