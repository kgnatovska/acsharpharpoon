using System.Collections.Generic;
namespace OOP
{
	public abstract class ShapeBase : IShape
	{
	    protected ShapeBase(int coordX, int coordY)
        {
            this.CoordX = coordX;
            this.CoordY = coordY;
        }

	    protected ShapeBase() : this(0, 0)
        {
        }

	    protected ShapeBase(IDictionary<ParamKeys, object> parameters) :
            this(
                parameters.Keys.Contains(ParamKeys.CoordX) ? (int)parameters[ParamKeys.CoordX] : 0,
                parameters.Keys.Contains(ParamKeys.CoordY) ? (int)parameters[ParamKeys.CoordY] : 0
            )
        {
        }

        public abstract string ShapeName { get; }
        public int CoordX { get; protected set; }
        public int CoordY { get; protected set; }
        public byte Multiplier { get; set; }

		public abstract double GetPerimeter();
		protected abstract double Area();
		public abstract void Move(int deltaX, int deltaY);

        public override string ToString()
        {
            return
                $"Shape information: Name : {ShapeName}, X : {CoordX}, Y : {CoordY}, Perimeter : {GetPerimeter()}, Square : {GetArea()}";
        }

        public double GetArea()
        {
            return Area();
        }
        
	}
}