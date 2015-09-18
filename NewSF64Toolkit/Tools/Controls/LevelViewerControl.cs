using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.OpenGL;
using NewSF64Toolkit.OpenGL.F3DEX;

namespace NewSF64Toolkit.Tools.Controls
{
    public partial class LevelViewerControl : UserControl
    {
        private OpenGLControl _glControl;
        //private F3DEXParser _parser;
        //private StarFoxLevelLoader _levelLoader;

        public LevelViewerControl()
        {
            InitializeComponent();

            _glControl = new OpenGLControl();
            this.glPanel.Controls.Add(_glControl);
            _glControl.Dock = DockStyle.Fill;

            //_parser = new F3DEXParser(_glControl);
            //_levelLoader = new StarFoxLevelLoader(_parser);

            cbLevelSelect.SelectedIndex = 0;
        }

        private void cbLevelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Include index 4?
            if (cbLevelSelect.SelectedIndex == 11 || cbLevelSelect.SelectedIndex == 12 ||
                cbLevelSelect.SelectedIndex == 13 || cbLevelSelect.SelectedIndex == 15)
            {
                btnLoadLevel.Enabled = false;
            }
            else
                btnLoadLevel.Enabled = true;
        }

        private void btnLoadLevel_Click(object sender, EventArgs e)
        {
            int levelDMAIndex = GetLevelDMAIndex();

            SFGfx.SelectedLevelDMA = levelDMAIndex;
            SF64ROM.Instance.LoadROMResources();

            if (!SF64ROM.Instance.IsROMLoaded || SF64ROM.Instance.DMATable.Count <= levelDMAIndex)
            {
                //Error message
                MessageBox.Show("Rom file not loaded correctly, try reloading the ROM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SF64ROM.Instance.DMATable[levelDMAIndex].DMAInfo.CFlag == 0x01)
            {
                //Error message
                MessageBox.Show("Specified level file is compressed, decompress before trying again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ////To be fixed in the future
            //MemoryManager.Instance.ClearBanks();

            ////Initiate the level loading. Grab the correct offset info and pass it to the F3DEX parser
            //DMAFile offsetTableDMA = SF64ROM.Instance.DMATable[1];
            //MemoryManager.Instance.AddBank((byte)0xFF, offsetTableDMA.DMAData, (uint)0x0);

            //uint offset = ByteHelper.ReadUInt(offsetTableDMA.DMAData, 0xCE158 + cbLevelSelect.SelectedIndex * 0x04);
            //byte segment = (byte)((offset & 0xFF000000) >> 24);
            //offset &= 0x00FFFFFF;

            ////_glControl.Clear();
            //MemoryManager.Instance.AddBank(segment, SF64ROM.Instance.DMATable[levelDMAIndex].DMAData, 0x00);

            //_levelLoader.StartReadingLevelDataAt(segment, offset);

            //InitDListNavigEnabled(true);
            //SetupDList();

            _glControl.ReDraww();
        }

        private void SetupDList()
        {
            tvLevelInfo.Nodes.Clear();

            //Load the level loader's game objects into the dlist thing
            for (int i = 0; i < SFGfx.GameObjCount; i++)
            {
                tvLevelInfo.Nodes.Add(new TreeNode(string.Format("Object {0} at {1} ({2})", i, SFGfx.GameObjects[i].LvlPos, ByteHelper.DisplayValue(SFGfx.GameObjects[i].ID))));
            }
        }

        private int GetLevelDMAIndex()
        {
            switch (cbLevelSelect.SelectedIndex)
            {
                case 0:
                    return 18;
                case 1:
                    return 19;
                case 2:
                    return 26;
                case 3:
                    return 29;
                case 4:
                    return 29;
                case 5:
                    return 35;
                case 6:
                    return 30;
                case 7:
                    return 36;
                case 8:
                    return 37;
                case 9:
                    return 47;
                case 10:
                    return 53;
                case 11:
                    return -1;
                case 12:
                    return -1;
                case 13:
                    return -1;
                case 14:
                    return 34;
                case 15:
                    return -1;
                case 16:
                    return 38;
                case 17:
                    return 33;
                case 18:
                    return 27;
                case 19:
                    return 31;
                case 20:
                    return 12;
                default:
                    return -1;
            }
        }

        private void txtMod_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SFGfx.GameObject obj = SFGfx.GameObjects[SFGfx.SelectedGameObject];
                obj.X = Convert.ToInt16(txtModX.Text);
                obj.XRot = Convert.ToInt16(txtModXRot.Text);
                obj.Y = Convert.ToInt16(txtModY.Text);
                obj.YRot = Convert.ToInt16(txtModYRot.Text);
                obj.Z = Convert.ToInt16(txtModZ.Text);
                obj.ZRot = Convert.ToInt16(txtModZRot.Text);
                SFGfx.GameObjects[SFGfx.SelectedGameObject] = obj;


                //int levelDMAIndex = GetLevelDMAIndex();

                //_levelLoader.SaveGameObject(cbLevelSelect.SelectedIndex, SFGfx.SelectedGameObject);

                //_levelLoader.ExecuteDisplayLists(SFGfx.SelectedGameObject);
                _glControl.ReDraww();
            }
            catch (Exception ee) { };
        }

        private void btnModSnapTo_Click(object sender, EventArgs e)
        {
            //Move the camera to the object
            SFGfx.GameObject obj = SFGfx.GameObjects[SFGfx.SelectedGameObject];

            SFCamera.MoveCameraTo((float)obj.X, (float)obj.Y, (float)obj.Z - obj.LvlPos);
        }

        private void tvLevelInfo_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            btnModSnapTo_Click(sender, e);
        }

        private void LoadModelNavigInfo()
        {
            SFGfx.GameObject obj = SFGfx.GameObjects[SFGfx.SelectedGameObject];

            txtModX.TextChanged -= txtMod_TextChanged;
            txtModXRot.TextChanged -= txtMod_TextChanged;
            txtModY.TextChanged -= txtMod_TextChanged;
            txtModYRot.TextChanged -= txtMod_TextChanged;
            txtModZ.TextChanged -= txtMod_TextChanged;
            txtModZRot.TextChanged -= txtMod_TextChanged;

            txtModDList.Text = ByteHelper.DisplayValue(obj.DListOffset);
            txtModID.Text = obj.ID.ToString();
            txtModPos.Text = obj.LvlPos.ToString();
            txtModUnk.Text = obj.Unk.ToString();
            txtModX.Text = obj.X.ToString();
            txtModXRot.Text = obj.XRot.ToString();
            txtModY.Text = obj.Y.ToString();
            txtModYRot.Text = obj.YRot.ToString();
            txtModZ.Text = obj.Z.ToString();
            txtModZRot.Text = obj.ZRot.ToString();

            txtModX.TextChanged += txtMod_TextChanged;
            txtModXRot.TextChanged += txtMod_TextChanged;
            txtModY.TextChanged += txtMod_TextChanged;
            txtModYRot.TextChanged += txtMod_TextChanged;
            txtModZ.TextChanged += txtMod_TextChanged;
            txtModZRot.TextChanged += txtMod_TextChanged;

        }

        private void InitDListNavigEnabled(bool enable)
        {
            btnModSnapTo.Enabled = enable;

            if (!enable)
            {
                txtModDList.Clear();
                txtModID.Clear();
                txtModPos.Clear();
                txtModUnk.Clear();
                txtModX.Clear();
                txtModXRot.Clear();
                txtModY.Clear();
                txtModYRot.Clear();
                txtModZ.Clear();
                txtModZRot.Clear();
            }
            else
            {
                SFGfx.SelectedGameObject = 0;
                LoadModelNavigInfo();
            }
        }

        private void tvLevelInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int objIndex = e.Node.Index;

            if (objIndex < SFGfx.GameObjCount)
            {
                SFGfx.SelectedGameObject = objIndex;
                LoadModelNavigInfo();
                _glControl.ReDraww();
            }
        }

    }
}
