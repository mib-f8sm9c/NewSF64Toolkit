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
using NewSF64Toolkit.OpenGL;

namespace NewSF64Toolkit.Tools.Controls
{
    public partial class ResourceViewControl : UserControl
    {
        OpenGLControl _glControl;
        AdvancedType _currentAdvancedType;

        public ResourceViewControl()
        {
            InitializeComponent();

            lblType.Text = string.Empty;

            _glControl = new OpenGLControl();
            _glControl.Dock = DockStyle.Fill;
            _glControl.Mode = OpenGLControl.DisplayMode.SingleModelView;

            _currentAdvancedType = AdvancedType.None;

            RefreshTreeView();
        }

        public void RefreshTreeView()
        {
            treeView.Nodes.Clear();

            pnlAdvanced.Controls.Clear();

            if (SF64ROM.Instance.IsROMLoaded)
                treeView.Nodes.Add(SF64ROM.Instance.GetTreeNode());
        }

        public void RefreshControl()
        {
            //RefreshTreeView(); //???
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                IResourceInfo info = ResourceInfoFactory.ConvertToResourceInfo(treeView.SelectedNode.Tag);

                lblType.Text = treeView.SelectedNode.Text;
                propertyGrid.SelectedObject = info;

                AdvancedType advancedWindow = ResourceInfoFactory.GetAdvancedType(treeView.SelectedNode.Tag);
                if (advancedWindow != _currentAdvancedType)
                {
                    pnlAdvanced.Controls.Clear();

                    switch (advancedWindow)
                    {
                        case AdvancedType.Model:
                            pnlAdvanced.Controls.Add(_glControl);
                            break;
                        case AdvancedType.None:
                            break;
                    }

                    _currentAdvancedType = advancedWindow;
                }
                
                //Update the advanced window
                switch (_currentAdvancedType)
                {
                    case AdvancedType.Model:
                        LoadModel();
                        break;
                    case AdvancedType.None:
                        break;
                }
            }
        }

        public void LoadModel()
        {
            object drawable = treeView.SelectedNode.Tag;
            if (drawable is RefAdvancedLevelObject)
            {
                RefAdvancedLevelObject ralo = (RefAdvancedLevelObject)drawable;
                _glControl.SingleObjectDLIndices = ralo.GLDisplayListOffset;
            }
            else if (drawable is RefSimpleLevelObject)
            {
                RefSimpleLevelObject rslo = (RefSimpleLevelObject)drawable;
                _glControl.SingleObjectDLIndices = rslo.GLDisplayListOffset;
            }
            else if (drawable is SFLevelObject)
            {
                SFLevelObject levelObj = (SFLevelObject)drawable;
                _glControl.SingleObjectDLIndices = levelObj.GL_DisplayListIndex;
            }
            _glControl.ReDraww();
        }
    }
}
