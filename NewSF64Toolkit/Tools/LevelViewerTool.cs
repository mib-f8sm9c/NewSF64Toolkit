using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.Tools.Controls;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.Settings;

namespace NewSF64Toolkit.Tools
{
    public class LevelViewerTool : BaseToolkitTool
    {
        private LevelViewerControl _levelViewerControl;

        public LevelViewerTool()
            : base()
        {
            _levelViewerControl = new LevelViewerControl();
            _levelViewerControl.Dock = DockStyle.Fill;
        }

        public override void Activate()
        {

            base.Activate();
        }

        public override void DeActivate()
        {
            base.DeActivate();
        }

        public override void ROMUpdated(SF64ROM.RomUpdateType updateType)
        {
            if (IsActive)
                ;
        }

        public override void SettingsUpdated(ToolSettings.SettingsUpdatedType updateType)
        {
            if (IsActive)
                ;
        }

        public override Control GetToolControl()
        {
            return _levelViewerControl;
        }
    }
}
