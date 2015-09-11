using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.ProgramTools.Controls;

namespace NewSF64Toolkit.ProgramTools
{
    public class HexEditorTool : IToolkitTool
    {
        private bool _isActive;
        public bool IsActive { get { return _isActive; } }

        private HexEditorControl _hexEditorControl;

        public void ROMUpdated()
        {

        }

        public HexEditorTool()
        {
            //Init
            _hexEditorControl = new HexEditorControl();
        }

        public void Activate()
        {
            //Essentially refresh

            _isActive = true;
        }

        public void DeActivate()
        {
            //Essentially refresh

            _isActive = false;
        }

        public Control GetToolControl()
        {
            return _hexEditorControl;
        }
    }
}
