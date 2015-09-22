using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.Settings;

namespace NewSF64Toolkit.Tools
{
    //Toolkit Mode here means that it is a major function in the program that the user interacts with (ex. Level Viewer, Text Editor).
    public abstract class BaseToolkitTool
    {
        public BaseToolkitTool()
        {
            SF64ROM.RomUpdated += ROMUpdated;
            ToolSettings.SettingsUpdated += SettingsUpdated;
        }

        public abstract Control GetToolControl();

        public virtual void Activate()
        {
            IsActive = true;
        }

        public virtual void DeActivate()
        {
            IsActive = false;
        }

        public bool IsActive { get; protected set; }

        public abstract void ROMUpdated(SF64ROM.RomUpdateType updateType);

        public abstract void SettingsUpdated(ToolSettings.SettingsUpdatedType updateType);

    }
}
