using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace Spaceframe
{
    public enum Position
    {
        Inner,
        OuterX,
        OuterY,
        Vertex
    }

    //A Coordinate class that documents a point coordinate and its X,Y indices.
    class Coordinate
    {
        public int iX, iY;
        public Point3d origin;
        private Position pos;

        public Coordinate()
        {
            iX = 0;
            iY = 0;
            origin = new Point3d(0.0, 0.0, 0.0);
        }
        public Coordinate(int x, int y, Point3d pt)
        {
            this.iX = x;
            this.iY = y;
            this.origin = pt;
        }
        public void ChangePosStatus(Position p)
        {
            this.pos = p;
        }
        public Position GetPos()
        {
            return this.pos;
        }
        public void ChangeIndex(int x, int y)
        {
            this.iX = x;
            this.iY = y;
        }
        public Point3d DrawPt()
        {
            return this.origin;
        }
        public void Translate(Vector3d v)
        {
            this.origin = this.origin + v;
        }
    }
}