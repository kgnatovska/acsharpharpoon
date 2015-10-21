using System;
using System.Collections.Generic;

namespace OOP.Shapes.Triangles
{
	/// <summary>
	/// triangle where all edges are equal
	/// </summary>
    public class EquilateralTriangle : Triangle
	{
	    private double _edge;
	    private string _shapename = "EquilateralTriangle";

	    public EquilateralTriangle(double edge)
	        : this(new Dictionary<ParamKeys, object>{
                {ParamKeys.Edge1, edge },
                {ParamKeys.CoordX, 0 },
                {ParamKeys.CoordY, 0 }
	        })
	    {
	        _edge = edge;
	    }

	    public EquilateralTriangle(IDictionary<ParamKeys, object> parameters) : base(parameters)
	    {
	        _edge = (double) parameters[ParamKeys.Edge1];
	    }

	    public override string ShapeName
	    {
	        get { return _shapename; }
	    }

	    public override double GetPerimeter()
	    {
	        if (Multiplier != 0)
	        {
                return _edge * 3 * Multiplier;
            }
	        else
	        {
                return _edge * 3;
            }
	    }

	    protected override double Area()
	    {
	        if (Multiplier != 0)
	        {
                return Math.Pow(_edge*Multiplier, 2) * Math.Sqrt(3) / 4;
            }
	        else
	        {
                return Math.Pow(_edge, 2) * Math.Sqrt(3) / 4;
            }
	    }
	}
}