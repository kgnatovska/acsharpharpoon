using System;
using System.Collections.Generic;
namespace OOP.Shapes
{
	public class Circle : ShapeBase
	{
        double _radius;
	    string _shapename = "Circle";
        

        public Circle(double radius)
            : this(new Dictionary<ParamKeys, object> { 
                {ParamKeys.Radius, radius},
                {ParamKeys.CoordX, 0},
                {ParamKeys.CoordY, 0}
            })
        {
            _radius = radius;
        }

		public Circle(IDictionary<ParamKeys, object> parameters) : base(parameters)
		{
            _radius = (double) parameters[ParamKeys.Radius];
		}

	    public override string ShapeName
	    {
	        get { return _shapename; }
	    }

	    public override double GetPerimeter()
	    {
	        if (Multiplier!=0)
	        {
	            return 2*Math.PI*_radius*Multiplier;
	        }
	        else
	        {
	            return 2*Math.PI*_radius;
	        }
	    }

	    protected override double Area()
	    {
	        if (Multiplier != 0)
	        {
	            return Math.PI*Math.Pow(_radius*Multiplier, 2);
	        }
	        else
	        {
	            return Math.PI*Math.Pow(_radius, 2);
	        }
	    }

	    public override void Move(int deltaX, int deltaY)
	    {
	        CoordX += deltaX;
	        CoordY += deltaY;
	    }
	}
}