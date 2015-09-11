using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.ProgramTools
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
        private static Dictionary<ToolTypes, IToolkitTool> _tools = new Dictionary<ToolTypes,IToolkitTool>();

        public static IToolkitTool GetTool(ToolTypes type)
        {
            if (_tools.ContainsKey(type))
                return _tools[type];

            IToolkitTool newTool;
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
