using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.ProgramTools.Controls;

namespace NewSF64Toolkit.ProgramTools
{
    public class RomInfoTool : IToolkitTool
    {
        private bool _isActive;
        public bool IsActive { get { return _isActive; } }

        private RomInfoControl _romInfoControl;

        public void ROMUpdated()
        {

        }
        
        public RomInfoTool()
        {
            //Init
            _romInfoControl = new RomInfoControl();
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
            return _romInfoControl;
        }
    }
}
