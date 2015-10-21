using System.Collections.Generic;
using System;

namespace OOP.Shapes.Triangles
{
	/// <summary>
	/// Triangle with one 90 degrees corner
	/// </summary>
    public class RightTriangle : Triangle
	{
	    private double _edge1;
	    private double _edge2;
	    private string _shapename = "RightTriangle";

	    public RightTriangle(double edge1, double edge2)
            : this(new Dictionary<ParamKeys, object> {
                {ParamKeys.Edge1, edge1 },
                {ParamKeys.Edge2, edge2 },
                {ParamKeys.CoordX, 0 },
                {ParamKeys.CoordY, 0 }
                  })
	    {
	        _edge1 = edge1;
	        _edge2 = edge2;
	    }

	    public RightTriangle(IDictionary<ParamKeys, object> parameters) : base(parameters)
	    {
	        _edge1 = (double) parameters[ParamKeys.Edge1];
	        _edge2 = (double) parameters[ParamKeys.Edge2];
	    }

	    public override string ShapeName
	    {
	        get { return _shapename; }
	    }

	    public override double GetPerimeter()
	    {
	        if (Multiplier != 0)
	        {
                return (_edge1 + _edge2)*Multiplier + Math.Sqrt(Math.Pow(_edge1*Multiplier, 2) + Math.Pow(_edge2*Multiplier, 2));
            }
	        else
	        {
                return _edge1 + _edge2 + Math.Sqrt(Math.Pow(_edge1, 2) + Math.Pow(_edge2, 2));
            }
	    }

	    protected override double Area()
	    {
	        if (Multiplier != 0)
	        {
                return _edge1 * _edge2 *Multiplier/ 2;
            }
	        else
	        {
                return _edge1 * _edge2 / 2;
            }
	    }
	}
}