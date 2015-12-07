using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;
using NewSF64Toolkit.DataStructures.DataObjects;
using NewSF64Toolkit.Tools.ResourceInfo;

namespace NewSF64Toolkit.Tools.Controls
{
    public partial class ResourceViewControl : UserControl
    {
        public ResourceViewControl()
        {
            InitializeComponent();

            lblType.Text = string.Empty;

            if (SF64ROM.Instance.IsROMLoaded)
                treeView.Nodes.Add(SF64ROM.Instance.GetTreeNode());
        }

        public void RefreshTreeView()
        {
            treeView.Nodes.Clear();

            if (SF64ROM.Instance.IsROMLoaded)
                treeView.Nodes.Add(SF64ROM.Instance.GetTreeNode());
        }

        public void RefreshControl()
        {
            //if (treeView.SelectedNode != null && pnlControl.Controls.Count > 0)
            //    ((ResourceControl)pnlControl.Controls[0]).SetObject(treeView.SelectedNode.Tag);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                IResourceInfo info = ResourceInfoFactory.ConvertToResourceInfo(treeView.SelectedNode.Tag);

                lblType.Text = treeView.SelectedNode.Text;
                propertyGrid.SelectedObject = info;
            }
        }

    }
}
