using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.Tools.Debug.Controls;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.Settings;

namespace NewSF64Toolkit.Tools.Debug
{
    public class F3DEXViewerTool : BaseToolkitTool
    {
        private F3DEXViewerControl _f3dexViewerControl;

        public F3DEXViewerTool()
            : base()
        {
            _f3dexViewerControl = new F3DEXViewerControl();
            _f3dexViewerControl.Dock = DockStyle.Fill;
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
                    _f3dexViewerControl.ResetGL();
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
                    _f3dexViewerControl.RefreshGL();
                    break;
                case ToolSettings.SettingsUpdatedType.BaseSystemChange:
                    //_f3dexViewerControl.UpdateLevelInfo();
                    break;
            }
        }

        public override Control GetToolControl()
        {
            return _f3dexViewerControl;
        }
    }
}
