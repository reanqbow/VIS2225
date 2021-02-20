using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace Spaceframe
{
    //A grid class that is defined by int res, double size, and a list of Coordinates.
    class Grid
    {
        public double size;
        public int res;
        public List<Coordinate> cLst = new List<Coordinate>();

        public Grid()
        {
            size = 0.0;
            res = 0;
        }
        //Constructor of Grid.
        public Grid(int res, double size)
        {
            this.size = size;
            this.res = res;
            double interval = size / (res - 1);
            List<Coordinate> cLst = new List<Coordinate>();
            for (int i = 0; i < res; i++)
            {
                for (int j = 0; j < res; j++)
                {
                    Coordinate c = new Coordinate(i, j, new Point3d(i * interval, j * interval, 0.0));
                    //Assign the Position status to every Coordinate in the Grid.
                    if (c.iX == res - 1 && c.iY == res - 1)
                    {
                        c.ChangePosStatus(Position.Vertex);
                    }
                    else if (c.iX == res - 1 && c.iY < res - 1)
                    {
                        c.ChangePosStatus(Position.OuterX);
                    }
                    else if (c.iY == res - 1 && c.iX < res - 1)
                    {
                        c.ChangePosStatus(Position.OuterY);
                    }
                    else
                    {
                        c.ChangePosStatus(Position.Inner);
                    }
                    cLst.Add(c);
                }
            }
            this.cLst = cLst;
        }

        //Change the value of res and adjust the Position status of each Coordinate.
        public void ChangeRes(int r)
        {
            this.res = r;
            foreach (Coordinate c in this.cLst)
            {
                if (c.iX == r - 1 && c.iY == r - 1)
                {
                    c.ChangePosStatus(Position.Vertex);
                }
                else if (c.iX == r - 1 && c.iY < r - 1)
                {
                    c.ChangePosStatus(Position.OuterX);
                }
                else if (c.iY == r - 1 && c.iX < r - 1)
                {
                    c.ChangePosStatus(Position.OuterY);
                }
                else
                {
                    c.ChangePosStatus(Position.Inner);
                }
            }
        }

        public void ChangeSize(double s)
        {
            this.size = s;
        }

        public Coordinate GetCoordinate(int x, int y)
        {
            List<Coordinate> cLst = this.cLst;
            foreach (Coordinate c in cLst)
            {
                if (c.iX == x && c.iY == y)
                {
                    return c;
                }
            }
            return new Coordinate();
        }

        //The drawing method to create points.
        public List<Point3d> DrawGridPts()
        {
            List<Coordinate> newLst = this.cLst;
            List<Point3d> result = new List<Point3d>();
            foreach (Coordinate item in newLst)
            {
                result.Add(item.DrawPt());
            }
            return result;
        }

        //Translation method.
        public void TranslateGrid(Vector3d v)
        {
            foreach (Coordinate c in this.cLst)
            {
                c.origin = c.origin + v;
            }
        }

        //To create a new Grid that is defined by midpoints.
        public Grid MidPtGrid()
        {
            Grid newGrid = new Grid();
            newGrid.ChangeSize(this.size * (this.res - 1) / this.res);
            Coordinate first = this.cLst[0];
            Coordinate next = this.GetCoordinate(first.iX + 1, first.iY + 1);
            double newX = (next.origin.X - first.origin.X) / 2.0;
            double newY = (next.origin.Y - first.origin.Y) / 2.0;
            Vector3d v = new Vector3d(newX, newY, 0.0);
            foreach (Coordinate c in this.cLst)
            {
                if (c.GetPos() == Position.Inner)
                {
                    Coordinate newC = new Coordinate(c.iX, c.iY, c.origin);
                    newGrid.cLst.Add(newC);
                }
            }
            newGrid.ChangeRes(this.res - 1);
            newGrid.TranslateGrid(v);
            return newGrid;
        }

        //The drawing method to create lines.
        public List<Line> DrawGridLne()
        {
            List<Coordinate> newLst = new List<Coordinate>();
            foreach (Coordinate c in this.cLst)
            {
                newLst.Add(c);
            }
            List<Line> result = new List<Line>();
            foreach (Coordinate item in newLst)
            {
                switch (item.GetPos())
                {
                    case Position.Vertex:
                        break;
                    case Position.Inner:
                        Coordinate next = this.GetCoordinate(item.iX, item.iY + 1);
                        Coordinate next1 = this.GetCoordinate(item.iX + 1, item.iY);
                        result.Add(new Line(item.origin, next.origin));
                        result.Add(new Line(item.origin, next1.origin));
                        break;
                    case Position.OuterY:
                        Coordinate next2 = this.GetCoordinate(item.iX + 1, item.iY);
                        result.Add(new Line(item.origin, next2.origin));
                        break;
                    case Position.OuterX:
                        Coordinate next3 = this.GetCoordinate(item.iX, item.iY + 1);
                        result.Add(new Line(item.origin, next3.origin));
                        break;
                }
            }
            return result;
        }
    }
}