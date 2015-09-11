using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.ProgramTools.Controls;

namespace NewSF64Toolkit.ProgramTools
{
    public class LevelViewerTool : IToolkitTool
    {
        private bool _isActive;
        public bool IsActive { get { return _isActive; } }

        private LevelViewerControl _levelViewerControl;

        public void ROMUpdated()
        {
            _levelViewerControl = new LevelViewerControl();
        }

        public LevelViewerTool()
        {
            //Init
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
            return _levelViewerControl;
        }
    }
}
