using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.Tools.Controls;
using NewSF64Toolkit.Settings;
using NewSF64Toolkit.DataStructures;

namespace NewSF64Toolkit.Tools
{
    public class ResourceViewTool : BaseToolkitTool
    {
        private ResourceViewControl _resourceViewControl;

        public ResourceViewTool()
            : base()
        {
            _resourceViewControl = new ResourceViewControl();
            _resourceViewControl.Dock = DockStyle.Fill;
        }

        public override void Activate()
        {
            //_romInfoControl.RefreshROMInfo();

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
                case SF64ROM.RomUpdateType.CRCFixed:
                case SF64ROM.RomUpdateType.Decompressed:
                case SF64ROM.RomUpdateType.RomEdited:
                    _resourceViewControl.RefreshTreeView();
                    break;
            }
        }

        public override void SettingsUpdated(ToolSettings.SettingsUpdatedType updateType)
        {
            if (!IsActive)
                return;

            switch (updateType)
            {
                case ToolSettings.SettingsUpdatedType.BaseSystemChange:
                    _resourceViewControl.RefreshControl();
                    break;
            }
        }

        public override Control GetToolControl()
        {
            return _resourceViewControl;
        }
    }
}
