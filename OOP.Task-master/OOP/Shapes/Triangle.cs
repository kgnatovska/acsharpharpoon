using System.Collections.Generic;
using System;

namespace OOP.Shapes
{
    // TODO: use Heron's formula for area
    public class Triangle : ShapeBase
    {
        private double _edge1;
        private double _edge2;
        private double _edge3;
        private string _shapename = "Triangle";

        public Triangle(double edge1, double edge2, double edge3)
            : this(new Dictionary<ParamKeys, object>{
                {ParamKeys.Edge1, edge1 },
                {ParamKeys.Edge2, edge2 },
                {ParamKeys.Edge3, edge3 },
                {ParamKeys.CoordX, 0 },
                {ParamKeys.CoordY, 0 }
            })
        {
            _edge1 = edge1;
            _edge2 = edge2;
            _edge3 = edge3;
        }

        public Triangle(IDictionary<ParamKeys, object> parameters) : base(parameters)
        {
            _edge1 = (double) parameters[ParamKeys.Edge1];
            _edge2 = (double) parameters[ParamKeys.Edge2];
            _edge3 = (double) parameters[ParamKeys.Edge3];
        }

        public override string ShapeName
        {
            get { return _shapename; }
        }

        public override double GetPerimeter()
        {
            if (Multiplier != 0)
            {
                return (_edge1 + _edge2 + _edge3)*Multiplier;
            }
            else
            {
                return _edge1 + _edge2 + _edge3;
            }
        }

        protected override double Area()
        {
            if (Multiplier != 0)
            {
                var s = (_edge1 + _edge2 + _edge3) * Multiplier / 2;
                return Math.Sqrt(s * (s - _edge1 * Multiplier) * (s - _edge2 * Multiplier) * (s - _edge3 * Multiplier));
            }
            else
            {
                var s = (_edge1 + _edge2 + _edge3) / 2;
                return Math.Sqrt(s * (s - _edge1) * (s - _edge2) * (s - _edge3));
            }
        }

        public override void Move(int deltaX, int deltaY)
        {
            CoordX += deltaX;
            CoordY += deltaY;
        }
    }
}