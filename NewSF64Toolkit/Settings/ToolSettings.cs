using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

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

        private bool _autoDecompress;
        public bool AutoDecompress
        {
            get
            {
                return _autoDecompress;
            }
            set
            {
                _autoDecompress = value;
                SettingsUpdated(SettingsUpdatedType.BaseSystemChange);
            }
        }

        private bool _autoCRCFix;
        public bool AutoCRCFix
        {
            get
            {
                return _autoCRCFix;
            }
            set
            {
                _autoCRCFix = value;
                SettingsUpdated(SettingsUpdatedType.BaseSystemChange);
            }
        }

        private bool _displayDebugTools;
        public bool DisplayDebugTools
        {
            get
            {
                return _displayDebugTools;
            }
            set
            {
                _displayDebugTools = value;
                SettingsUpdated(SettingsUpdatedType.BaseSystemChange);
            }
        }

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

        private List<string> _recentlyOpened;
        public List<string> RecentlyOpened
        {
            get
            {
                return _recentlyOpened;
            }
        }
        public void AddRecentlyOpened(string path)
        {
            if (_recentlyOpened.Contains(path))
            {
                UpdateRecentlyOpenedToTop(_recentlyOpened.IndexOf(path));
            }
            else
            {
                _recentlyOpened.Insert(0, path);
                if (_recentlyOpened.Count > 5)
                    _recentlyOpened.RemoveAt(5);
            }
        }

        public void UpdateRecentlyOpenedToTop(int index)
        {
            string str = _recentlyOpened[index];
            _recentlyOpened.RemoveAt(index);
            AddRecentlyOpened(str);
        }

        public ToolSettings()
        {
            _displayInHex = true;
            _useWireframe = false;
            _autoCRCFix = false;
            _autoDecompress = false;
            _displayDebugTools = false;
            _recentlyOpened = new List<string>();
        }

        public void Load()
        {
            if (File.Exists("settings.xml"))
            {
                using (XmlTextReader xml = new XmlTextReader("settings.xml"))
                {
                    while (xml.Read())
                    {
                        switch (xml.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (xml.Name == "settings")
                                {
                                    while (xml.MoveToNextAttribute())
                                    {
                                        switch (xml.Name)
                                        {
                                            case "usewireframe":
                                                _useWireframe = bool.Parse(xml.Value);
                                                break;
                                            case "displayhex":
                                                _displayInHex = bool.Parse(xml.Value);
                                                break;
                                            case "autocrc":
                                                _autoCRCFix = bool.Parse(xml.Value);
                                                break;
                                            case "autodecompress":
                                                _autoDecompress = bool.Parse(xml.Value);
                                                break;
                                            case "displaydebugtools":
                                                _displayDebugTools = bool.Parse(xml.Value);
                                                break;
                                        }
                                    }
                                }
                                else if (xml.Name == "recentlyopenedfile")
                                {
                                    _recentlyOpened.Add(xml.ReadString());
                                    //string str;
                                    //while (!string.IsNullOrEmpty((str = xml.ReadElementString("recentlyopenedfile"))))
                                    //    _recentlyOpened.Add(str);
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void Save()
        {
            using (XmlWriter xml = XmlWriter.Create("settings.xml"))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("settings");
                xml.WriteAttributeString("usewireframe", _useWireframe.ToString());
                xml.WriteAttributeString("displayhex", _displayInHex.ToString());
                xml.WriteAttributeString("autocrc", _autoCRCFix.ToString());
                xml.WriteAttributeString("autodecompress", _autoDecompress.ToString());
                xml.WriteAttributeString("displaydebugtools", _displayDebugTools.ToString());


                xml.WriteStartElement("recentlyopened");
                foreach (string str in _recentlyOpened)
                {
                    xml.WriteElementString("recentlyopenedfile", str);
                }

                xml.WriteEndElement();
                xml.WriteEndElement();

                xml.WriteEndDocument();
                xml.Close();
            }
        }
    }
}
