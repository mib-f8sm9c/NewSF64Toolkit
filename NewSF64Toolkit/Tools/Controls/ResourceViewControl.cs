using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.Tools.Controls.ResourceSubControls;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;
using NewSF64Toolkit.DataStructures.DataObjects;

namespace NewSF64Toolkit.Tools.Controls
{
    public partial class ResourceViewControl : UserControl
    {
        private enum ResourceType
        {
            SF64Rom,
            DMAFile,
            LevelDMAFile,
            ReferenceDMAFile,
            DialogueDMAFile,
            HeaderDMAFile,
            DMATableDMAFile,
            RamTableEntry,
            Empty
        }

        public ResourceViewControl()
        {
            InitializeComponent();

            _controls = new Dictionary<ResourceType, ResourceControl>();

            if (SF64ROM.Instance.IsROMLoaded)
                treeView.Nodes.Add(SF64ROM.Instance.GetTreeNode());

            //SetControl(SF64ROM.Instance);
        }

        public void RefreshTreeView()
        {
            treeView.Nodes.Clear();

            if (SF64ROM.Instance.IsROMLoaded)
                treeView.Nodes.Add(SF64ROM.Instance.GetTreeNode());
        }

        public void RefreshControl()
        {
            if (treeView.SelectedNode != null && pnlControl.Controls.Count > 0)
                ((ResourceControl)pnlControl.Controls[0]).SetObject(treeView.SelectedNode.Tag);
        }

        public void SetControl(object baseObject)
        {
            ResourceType type;

            if (baseObject == null)
            {
                type = ResourceType.Empty;
            }
            else if (baseObject.GetType() == typeof(SF64ROM))
            {
                type = ResourceType.SF64Rom;
            }
            else if (baseObject.GetType() == typeof(DMAFile))
            {
                type = ResourceType.DMAFile;
            }
            else if (baseObject.GetType() == typeof(DialogueDMAFile))
            {
                type = ResourceType.DialogueDMAFile;
            }
            else if (baseObject.GetType() == typeof(HeaderDMAFile))
            {
                type = ResourceType.HeaderDMAFile;
            }
            else if (baseObject.GetType() == typeof(LevelDMAFile))
            {
                type = ResourceType.LevelDMAFile;
            }
            else if (baseObject.GetType() == typeof(ReferenceDMAFile))
            {
                type = ResourceType.ReferenceDMAFile;
            }
            else if (baseObject.GetType() == typeof(DMATableDMAFile))
            {
                type = ResourceType.DMATableDMAFile;
            }
            else if (baseObject.GetType() == typeof(RAMTableEntry))
            {
                type = ResourceType.RamTableEntry;
            }
            else
            {
                type = ResourceType.Empty;
            }

            ResourceControl ctl = getControl(type);

            ctl.SetObject(baseObject);

            if (pnlControl.Controls.Count > 0 && pnlControl.Controls[0] != ctl)
                pnlControl.Controls.Clear();

            pnlControl.Controls.Add(ctl);
            //ctl.Dock = DockStyle.Fill;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                SetControl(treeView.SelectedNode.Tag);
            }
        }

        private Dictionary<ResourceType, ResourceControl> _controls;

        private ResourceControl getControl(ResourceType type)
        {
            if (!_controls.ContainsKey(type))
            {
                switch (type)
                {
                    case ResourceType.SF64Rom:
                        _controls.Add(type, new SF64RomResourceControl());
                        break;
                    case ResourceType.DMAFile:
                    case ResourceType.DialogueDMAFile:
                    case ResourceType.DMATableDMAFile:
                    case ResourceType.HeaderDMAFile:
                    case ResourceType.ReferenceDMAFile:
                        _controls.Add(type, new DMAResourceControl());
                        break;
                    case ResourceType.LevelDMAFile:
                        _controls.Add(type, new LevelDMAResourceControl());
                        break;
                    case ResourceType.RamTableEntry:
                        _controls.Add(type, new RamTableResourceControl());
                        break;
                    case ResourceType.Empty:
                        _controls.Add(type, new ResourceControl());
                        break;
                }
            }

            return _controls[type];
        }
    }
}
