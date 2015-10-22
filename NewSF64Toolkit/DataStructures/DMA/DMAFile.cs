using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;
using NewSF64Toolkit.DataStructures.DataObjects.F3DEX;
using System.Windows.Forms;

namespace NewSF64Toolkit.DataStructures.DMA
{
    //This default DMAFile class will hold all data as a byte array, but inheriting classes
    // may choose to represent the game data with objects rather than simple byte data. For
    // those classes,  make sure to override GetAsBytes().
    public class DMAFile : IGameDataStructure, IResource
    {
        public DMATableEntry DMAInfo;

        public int Index;

        protected DynamicMemoryMapping _dmaData;

        protected F3DEXCollection _f3dexData;
        public F3DEXCollection F3DEXData { get { return _f3dexData; } }

        public int Size { get { return _dmaData.Size; } }

        //Assume it's compressed if we can't get at the DMAInfo
        public bool IsCompressed { get { return DMAInfo != null ? DMAInfo.CFlag == 0x1 : true; } }

        public DMAFile(byte[] data, int dmaIndex)
        {
            _dmaData = new DynamicMemoryMapping();
            _f3dexData = new F3DEXCollection();

            Index = dmaIndex;

            LoadFromBytes(data);
        }

        public virtual byte[] GetAsBytes()
        {
            byte[] bytes = _dmaData.GetAsBytes();
            //_f3dexData.AddBytesTo(bytes);
            return bytes;
        }

        public virtual bool LoadFromBytes(byte[] bytes)
        {
            _dmaData.ClearMaps();

            _dmaData.AddMemory(0, bytes);

            return true;
        }

        public virtual TreeNode GetTreeNode()
        {
            TreeNode node = new TreeNode();

            node.Text = "DMA " + Index;

            node.Tag = this;

            //foreach (DMAFile dma in DMATable)
            //{
            //    node.Nodes.Add(dma.GetTreeNode());
            //}

            return node;
        }

    }
}
