using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class SF64RomInfo : IResourceInfo
    {
        private SF64ROM _rom;

        public SF64RomInfo(SF64ROM rom)
        {
            _rom = rom;
        }
    }
}
