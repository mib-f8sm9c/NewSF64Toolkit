using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures
{
    public interface IGameDataStructure
    {
        bool LoadFromBytes(byte[] bytes);

        byte[] GetAsBytes();
    }
}
