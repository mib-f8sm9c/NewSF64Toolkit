using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.Tools
{
    public enum ToolTypes
    {
        RomInfo,
        HexEditor,
        LevelViewer//,
        //ModelViewer
    }

    public static class ToolkitFactory
    {
        private static Dictionary<ToolTypes, BaseToolkitTool> _tools = new Dictionary<ToolTypes, BaseToolkitTool>();

        public static BaseToolkitTool GetTool(ToolTypes type)
        {
            if (_tools.ContainsKey(type))
                return _tools[type];

            BaseToolkitTool newTool;
            switch (type)
            {
                case ToolTypes.RomInfo:
                    newTool = new RomInfoTool();
                    break;
                case ToolTypes.HexEditor:
                    newTool = new HexEditorTool();
                    break;
                case ToolTypes.LevelViewer:
                default: //To Do: add a default null tool
                    newTool = new LevelViewerTool();
                    break;
            }

            _tools.Add(type, newTool);

            return newTool;
        }
    }
}
