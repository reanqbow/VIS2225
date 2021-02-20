using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace Spaceframe
{
    //A Spaceframe class defined by outer and inner grids and struts.
    class Spaceframe
    {
        public int outerRes, innerRes;
        public double size;
        public Grid outer = new Grid();
        public Grid inner = new Grid();
        public List<Line> outerFrame = new List<Line>();
        public List<Line> innerFrame = new List<Line>();
        public List<Line> struts = new List<Line>();
        public Vector3d translation = new Vector3d(0.0,0.0,-1.0);

        public Spaceframe()
        {
            outerRes = 0;
            innerRes = 0;
            size = 0.0;
        }

        public Spaceframe(int res, double size, double height)
        {
            this.translation *= height;
            this.outerRes = res;
            this.innerRes = res - 1;
            this.size = size;
            this.outer = new Grid(res, size);
            this.outerFrame = this.outer.DrawGridLne();
            this.inner = this.outer.MidPtGrid();
            this.inner.TranslateGrid(this.translation);
            this.innerFrame = this.inner.DrawGridLne();

            List<Line> struts = new List<Line>();
            foreach (Coordinate c in this.inner.cLst)
            {
                struts.Add(new Line(c.origin, this.outer.GetCoordinate(c.iX, c.iY).origin));
                struts.Add(new Line(c.origin, this.outer.GetCoordinate(c.iX + 1, c.iY).origin));
                struts.Add(new Line(c.origin, this.outer.GetCoordinate(c.iX, c.iY + 1).origin));
                struts.Add(new Line(c.origin, this.outer.GetCoordinate(c.iX + 1, c.iY + 1).origin));
            }
            this.struts = struts;
        }

        public List<Line> DrawSpaceframe()
        {
            List<Line> outer = this.outerFrame;
            List<Line> inner = this.innerFrame;
            List<Line> struts = this.struts;
            outer.AddRange(inner);
            outer.AddRange(struts);
            return outer;
        }
    }
}
