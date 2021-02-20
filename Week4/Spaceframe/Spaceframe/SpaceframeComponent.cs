using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace Spaceframe
{
    public class SpaceframeComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public SpaceframeComponent()
          : base("Spaceframe", "SF",
              "A Spaceframe generator",
              "Frame", "Special")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Resolution", "R", "The resolution of the grid.", GH_ParamAccess.item, 5);
            pManager.AddNumberParameter("Size", "S", "The size of the grid.", GH_ParamAccess.item, 16.0);
            pManager.AddNumberParameter("Height", "H", "The height of the spaceframe.", GH_ParamAccess.item, 1.0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddLineParameter("Spaceframe", "SF", "The result spaceframe.", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int res = 0;
            double s = double.NaN;
            double h = double.NaN;

            DA.GetData("Resolution", ref res);
            DA.GetData("Size", ref s);
            DA.GetData("Height", ref h);

            Spaceframe spaceFrame = new Spaceframe(res, s, h);
            List<Line> result = spaceFrame.DrawSpaceframe();

            DA.SetDataList("Spaceframe", result);

            
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f07d0501-1c6c-401b-8d67-7eaac7cc726c"); }
        }
    }
}
