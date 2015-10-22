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
    public class ModelViewerTool : BaseToolkitTool
    {
        private ModelViewerControl _levelViewerControl;

        public ModelViewerTool()
            : base()
        {
            _levelViewerControl = new ModelViewerControl();
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

            if (!IsActive)
                return;

            switch (updateType)
            {
                case SF64ROM.RomUpdateType.RomUnloaded:
                case SF64ROM.RomUpdateType.RomLoaded:
                case SF64ROM.RomUpdateType.RomEdited:
                    _levelViewerControl.ResetGL();
                    break;
            }
        }

        public override void SettingsUpdated(ToolSettings.SettingsUpdatedType updateType)
        {
            if (!IsActive)
                return;

            switch (updateType)
            {
                case ToolSettings.SettingsUpdatedType.WireframeChange:
                    _levelViewerControl.RefreshGL();
                    break;
                case ToolSettings.SettingsUpdatedType.BaseSystemChange:
                    _levelViewerControl.UpdateLevelInfo();
                    break;
            }
        }

        public override Control GetToolControl()
        {
            return _levelViewerControl;
        }
    }
}
