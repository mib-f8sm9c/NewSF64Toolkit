using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.Settings
{
    public class ToolSettings
    {
        private static ToolSettings _instance;

        public static ToolSettings Instance { get { if (_instance == null) _instance = new ToolSettings(); return _instance; } }

        public enum SettingsUpdatedType
        {
            BaseSystemChange,
            WireframeChange
        }

        public delegate void SettingsUpdatedEvent(SettingsUpdatedType type);

        public static SettingsUpdatedEvent SettingsUpdated = delegate { };

        private bool _displayInHex;
        public bool DisplayInHex
        {
            get
            {
                return _displayInHex;
            }
            set
            {
                _displayInHex = value;
                SettingsUpdated(SettingsUpdatedType.BaseSystemChange);
            }
        }

        private bool _useWireframe;
        public bool UseWireframe
        {
            get
            {
                return _useWireframe;
            }
            set
            {
                _useWireframe = value;
                SettingsUpdated(SettingsUpdatedType.WireframeChange);
            }
        }

        public ToolSettings()
        {
            _displayInHex = true;
            _useWireframe = false;
        }

        public static void Load()
        {
            //Eventually have an xml file loading here
            _instance = new ToolSettings();
        }
    }
}
