using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;
using NewSF64Toolkit.DataStructures.DataObjects;
using System.ComponentModel;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class ListInfo : IResourceInfo
    {
        private System.Collections.IList _list;

        public ListInfo(System.Collections.IList list)
        {
            _list = list;
        }

        [CategoryAttribute("List Info"), DescriptionAttribute("Count of objects in the list")]
        public int Count
        {
            get { return _list.Count; }
        }
    }
}
