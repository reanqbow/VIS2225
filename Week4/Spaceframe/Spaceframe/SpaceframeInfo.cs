using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace Spaceframe
{
    public class SpaceframeInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Spaceframe";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "A Spaceframe generator, defined by upper and lower grid and dimension of struts.";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("8103b16a-f5e5-42f1-bc03-6fc2a9500991");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Yubo Zhao";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "reanqbow@gmail.com";
            }
        }
    }
}
