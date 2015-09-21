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
    public class HexEditorTool : BaseToolkitTool
    {
        private HexEditorControl _hexEditorControl;

        private bool _needInit = true;

        public HexEditorTool()
            : base()
        {
            _hexEditorControl = new HexEditorControl();
            _hexEditorControl.Dock = DockStyle.Fill;
        }

        public override void Activate()
        {
            if (_needInit)
            {
                _hexEditorControl.ResetDMATable();
                _needInit = false;
            }
            else
                _hexEditorControl.RefreshDMATable();

            base.Activate();
        }

        public override void DeActivate()
        {
            base.DeActivate();
        }

        public override void ROMUpdated(SF64ROM.RomUpdateType updateType)
        {
            if (!IsActive)
            {
                switch (updateType)
                {
                    case SF64ROM.RomUpdateType.RomUnloaded:
                    case SF64ROM.RomUpdateType.RomLoaded:
                        _needInit = true;
                        break;
                }
                return;
            }

            switch (updateType)
            {
                case SF64ROM.RomUpdateType.RomUnloaded:
                case SF64ROM.RomUpdateType.RomLoaded:
                    _hexEditorControl.ResetDMATable();
                    break;
                case SF64ROM.RomUpdateType.CRCFixed:
                case SF64ROM.RomUpdateType.Decompressed:
                case SF64ROM.RomUpdateType.RomEdited:
                    _hexEditorControl.RefreshDMATable();
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
                    _hexEditorControl.RefreshDMATable();
                    break;
            }
        }

        public override Control GetToolControl()
        {
            return _hexEditorControl;
        }
    }
}
