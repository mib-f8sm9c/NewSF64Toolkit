using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewSF64Toolkit.ProgramTools
{
    //Toolkit Mode here means that it is a major function in the program that the user interacts with (ex. Level Viewer, Text Editor).
    public interface IToolkitTool
    {
        Control GetToolControl();

        void Activate();

        void DeActivate();

        bool IsActive { get; }

        void ROMUpdated();

    }
}
