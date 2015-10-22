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
using NewSF64Toolkit.DataStructures.DMA;
using NewSF64Toolkit.DataStructures.DataObjects;

namespace NewSF64Toolkit.Tools.Controls
{
    public partial class ModelViewerControl : UserControl
    {
        private OpenGLControl _glControl;

        public ModelViewerControl()
        {
            InitializeComponent();

            _glControl = new OpenGLControl();
            this.glPanel.Controls.Add(_glControl);
            _glControl.Dock = DockStyle.Fill;

            cbLevelSelect.SelectedIndex = 0;
        }

        //private void cbLevelSelect_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Include index 4?
        //    if (cbLevelSelect.SelectedIndex == 11 || cbLevelSelect.SelectedIndex == 12 ||
        //        cbLevelSelect.SelectedIndex == 13 || cbLevelSelect.SelectedIndex == 15)
        //    {
        //        btnLoadLevel.Enabled = false;
        //    }
        //    else
        //        btnLoadLevel.Enabled = true;
        //}

        //private int _gameObjCount;
        //private int _selectedGameObject;
        //public int _selectedLevelDMA;


        //private void btnLoadLevel_Click(object sender, EventArgs e)
        //{
        //    int levelDMAIndex = GetLevelDMAIndex();

        //    _selectedLevelDMA = levelDMAIndex;
        //    SF64ROM.Instance.LoadROMResources();

        //    List<SFLevelObject> levelObjects = ((LevelDMAFile)SF64ROM.Instance.DMATable[levelDMAIndex]).LevelObjects;
        //    _gameObjCount = levelObjects.Count;

        //    if (!SF64ROM.Instance.IsROMLoaded || SF64ROM.Instance.DMATable.Count <= levelDMAIndex)
        //    {
        //        //Error message
        //        MessageBox.Show("Rom file not loaded correctly, try reloading the ROM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    if (SF64ROM.Instance.DMATable[levelDMAIndex].DMAInfo.CFlag == 0x01)
        //    {
        //        //Error message
        //        MessageBox.Show("Specified level file is compressed, decompress before trying again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    InitDListNavigEnabled(true);
        //    SetupDList();

        //    _glControl.LevelObjects = levelObjects;
        //    _glControl.SelectedObjectIndex = -1;
        //    _glControl.ReDraww();
        //}

        //private void SetupDList()
        //{
        //    tvLevelInfo.Nodes.Clear();

        //    if (_selectedLevelDMA == -1)
        //        return;

        //    //Load the level loader's game objects into the dlist thing
        //    List<SFLevelObject> objects = ((LevelDMAFile)SF64ROM.Instance.DMATable[_selectedLevelDMA]).LevelObjects;

        //    for (int i = 0; i < objects.Count; i++)
        //    {
        //        tvLevelInfo.Nodes.Add(new TreeNode(string.Format("Object {0} at {1} ({2})", i, objects[i].LvlPos, ByteHelper.DisplayValue(objects[i].ID))));
        //    }
        //}

        //private int GetLevelDMAIndex()
        //{
        //    switch (cbLevelSelect.SelectedIndex)
        //    {
        //        case 0:
        //            return 18;
        //        case 1:
        //            return 19;
        //        case 2:
        //            return 26;
        //        case 3:
        //            return 29;
        //        case 4:
        //            return 29;
        //        case 5:
        //            return 35;
        //        case 6:
        //            return 30;
        //        case 7:
        //            return 36;
        //        case 8:
        //            return 37;
        //        case 9:
        //            return 47;
        //        case 10:
        //            return 53;
        //        case 11:
        //            return -1;
        //        case 12:
        //            return -1;
        //        case 13:
        //            return -1;
        //        case 14:
        //            return 34;
        //        case 15:
        //            return -1;
        //        case 16:
        //            return 38;
        //        case 17:
        //            return 33;
        //        case 18:
        //            return 27;
        //        case 19:
        //            return 31;
        //        case 20:
        //            return 12;
        //        default:
        //            return -1;
        //    }
        //}

        //private void txtMod_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        List<SFLevelObject> objects = ((LevelDMAFile)SF64ROM.Instance.DMATable[_selectedLevelDMA]).LevelObjects;

        //        SFLevelObject obj = objects[_selectedGameObject];
        //        obj.X = Convert.ToInt16(txtModX.Text);
        //        obj.XRot = Convert.ToInt16(txtModXRot.Text);
        //        obj.Y = Convert.ToInt16(txtModY.Text);
        //        obj.YRot = Convert.ToInt16(txtModYRot.Text);
        //        obj.Z = Convert.ToInt16(txtModZ.Text);
        //        obj.ZRot = Convert.ToInt16(txtModZRot.Text);
        //        objects[_selectedGameObject] = obj;


        //        //int levelDMAIndex = GetLevelDMAIndex();

        //        //_levelLoader.SaveGameObject(cbLevelSelect.SelectedIndex, F3DEXParser.SelectedGameObject);

        //        //_levelLoader.ExecuteDisplayLists(F3DEXParser.SelectedGameObject);
        //        _glControl.ReDraww();
        //    }
        //    catch (Exception ee) { };
        //}

        //private void tvLevelInfo_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
        //    List<SFLevelObject> objects = ((LevelDMAFile)SF64ROM.Instance.DMATable[_selectedLevelDMA]).LevelObjects;

        //    //Move the camera to the object
        //    SFLevelObject obj = objects[_selectedGameObject];

        //    SFCamera.MoveCameraTo((float)obj.X, (float)obj.Y, (float)obj.Z - obj.LvlPos);
        //}

        //private void LoadModelNavigInfo()
        //{
        //    if (_selectedLevelDMA == -1)
        //    {

        //        txtModX.TextChanged -= txtMod_TextChanged;
        //        txtModXRot.TextChanged -= txtMod_TextChanged;
        //        txtModY.TextChanged -= txtMod_TextChanged;
        //        txtModYRot.TextChanged -= txtMod_TextChanged;
        //        txtModZ.TextChanged -= txtMod_TextChanged;
        //        txtModZRot.TextChanged -= txtMod_TextChanged;

        //        txtModDList.Text = string.Empty;
        //        txtModID.Text = string.Empty;
        //        txtModPos.Text = string.Empty;
        //        txtModUnk.Text = string.Empty;
        //        txtModX.Text = string.Empty;
        //        txtModXRot.Text = string.Empty;
        //        txtModY.Text = string.Empty;
        //        txtModYRot.Text = string.Empty;
        //        txtModZ.Text = string.Empty;
        //        txtModZRot.Text = string.Empty;

        //        txtModX.TextChanged += txtMod_TextChanged;
        //        txtModXRot.TextChanged += txtMod_TextChanged;
        //        txtModY.TextChanged += txtMod_TextChanged;
        //        txtModYRot.TextChanged += txtMod_TextChanged;
        //        txtModZ.TextChanged += txtMod_TextChanged;
        //        txtModZRot.TextChanged += txtMod_TextChanged;

        //    }
        //    else
        //    {
        //        List<SFLevelObject> objects = ((LevelDMAFile)SF64ROM.Instance.DMATable[_selectedLevelDMA]).LevelObjects;
        //        SFLevelObject obj = objects[_selectedGameObject];


        //        txtModX.TextChanged -= txtMod_TextChanged;
        //        txtModXRot.TextChanged -= txtMod_TextChanged;
        //        txtModY.TextChanged -= txtMod_TextChanged;
        //        txtModYRot.TextChanged -= txtMod_TextChanged;
        //        txtModZ.TextChanged -= txtMod_TextChanged;
        //        txtModZRot.TextChanged -= txtMod_TextChanged;

        //        txtModDList.Text = ByteHelper.DisplayValue(obj.DListOffset);
        //        txtModID.Text = obj.ID.ToString();
        //        txtModPos.Text = obj.LvlPos.ToString();
        //        txtModUnk.Text = obj.Unk.ToString();
        //        txtModX.Text = obj.X.ToString();
        //        txtModXRot.Text = obj.XRot.ToString();
        //        txtModY.Text = obj.Y.ToString();
        //        txtModYRot.Text = obj.YRot.ToString();
        //        txtModZ.Text = obj.Z.ToString();
        //        txtModZRot.Text = obj.ZRot.ToString();

        //        txtModX.TextChanged += txtMod_TextChanged;
        //        txtModXRot.TextChanged += txtMod_TextChanged;
        //        txtModY.TextChanged += txtMod_TextChanged;
        //        txtModYRot.TextChanged += txtMod_TextChanged;
        //        txtModZ.TextChanged += txtMod_TextChanged;
        //        txtModZRot.TextChanged += txtMod_TextChanged;
        //    }
        //}

        //private void InitDListNavigEnabled(bool enable)
        //{
        //    if (!enable)
        //    {
        //        txtModDList.Clear();
        //        txtModID.Clear();
        //        txtModPos.Clear();
        //        txtModUnk.Clear();
        //        txtModX.Clear();
        //        txtModXRot.Clear();
        //        txtModY.Clear();
        //        txtModYRot.Clear();
        //        txtModZ.Clear();
        //        txtModZRot.Clear();
        //    }
        //    else
        //    {
        //        _selectedGameObject = 0;
        //        LoadModelNavigInfo();
        //    }
        //}

        //private void tvLevelInfo_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    int objIndex = e.Node.Index;

        //    if (objIndex < _gameObjCount)
        //    {
        //        _selectedGameObject = objIndex;
        //        LoadModelNavigInfo();
        //        _glControl.SelectedObjectIndex = objIndex;
        //        _glControl.ReDraww();
        //    }
        //}

        public void RefreshGL()
        {
            _glControl.ReDraww();
        }

        //public void UpdateLevelInfo()
        //{
        //    //Load the level loader's game objects into the dlist thing
        //    List<SFLevelObject> objects = ((LevelDMAFile)SF64ROM.Instance.DMATable[_selectedLevelDMA]).LevelObjects;

        //    tvLevelInfo.BeginUpdate();
        //    for (int i = 0; i < objects.Count; i++)
        //    {
        //        tvLevelInfo.Nodes[i].Text = string.Format("Object {0} at {1} ({2})", i, objects[i].LvlPos, ByteHelper.DisplayValue(objects[i].ID));
        //    }
        //    tvLevelInfo.EndUpdate();

        //    LoadModelNavigInfo();
        //}

        public void ResetGL()
        {
            //_gameObjCount = 0;
            //_selectedLevelDMA = -1;

            //InitDListNavigEnabled(true);
            //SetupDList();

            _glControl.LevelObjects = new List<SFLevelObject>();
            _glControl.SelectedObjectIndex = -1;
            _glControl.ReDraww();
        }



        
        //private bool CheckAddressValidity(byte bankNo, uint offset)
        //{
        //    if (offset == 0) return false;

        //    if (!MemoryManager.Instance.HasBank(bankNo))
        //    {
        //        ErrorLog.Add(string.Format("- Warning: Segment 0x{0:X2} was not initialized, cannot access offset 0x{0:X6}!\n", bankNo, offset));
        //        return false;
        //    }
        //    else if (!MemoryManager.Instance.LocateBank(bankNo, offset).IsValid())
        //    {
        //        ErrorLog.Add(string.Format("- Warning: Offset 0x{0:X6} is out of bounds for segment 0x{0:X2}!\n", offset, bankNo));
        //        return false;
        //    }

        //    return true;
        //}

        public void StartReadingLevelDataAt(byte bankNo, uint index)
        {
            //This will need to parse the commands from the level data and pick off the F3DEX
            SFGfx.sv_ClearStructures(false);
            SFGfx.gl_ClearRenderer(true);
            SFGfx.GameObjects.Clear();
            SFGfx.GameObjCount = 0;

            byte limbCount = MemoryManager.Instance.ReadByte(bankNo, index + 3); //This should be the number of limbs
             
            //First 4 bytes are things I don't know (+ the limb count), next 8 bytes are header pointers, then it starts the data
            index += 0xC;

            List<stuffToAdd> hierarchyTesting = new List<stuffToAdd>();
            stuffToAdd topStuff = new stuffToAdd();

            for(int i = 0; i < limbCount; i++)
            {
                if (!CheckAddressValidity(bankNo, index)) break;

                SFGfx.GameObject newObj = new SFGfx.GameObject();


                newObj.LvlPos = 0;
                newObj.Z = (short)MemoryManager.Instance.ReadFloat(bankNo, index + 0x4);
                newObj.Y = (short)MemoryManager.Instance.ReadFloat(bankNo, index + 0x8);
                newObj.X = (short)MemoryManager.Instance.ReadFloat(bankNo, index + 0xC);
                newObj.XRot = (short)((double)MemoryManager.Instance.ReadUShort(bankNo, index + 0x10) / 182.0444444);
                newObj.YRot = (short)((double)MemoryManager.Instance.ReadUShort(bankNo, index + 0x12) / 182.0444444);
                newObj.ZRot = (short)((double)MemoryManager.Instance.ReadUShort(bankNo, index + 0x14) / 182.0444444);
                newObj.ID = 0;
                newObj.Unk = 0;

                // default dlist offset to 0
                newObj.DListOffset = MemoryManager.Instance.ReadUInt(bankNo, index);

                //// if object id == 0xffff, break out because this marks end of data!
                //if (newObj.ID == 0xFFFF) break;

                //// if object id < 0x190, get offset like this
                //if (newObj.ID < 0x190)
                //{
                //    //NOTE: SET -2 TO DMA 1
                //    newObj.DListOffset = MemoryManager.Instance.ReadUInt((byte)0xFF, (0xC72E4 + ((uint)newObj.ID * 0x24)));
                //}

                // dlist offset sanity checks
                if (((newObj.DListOffset & 3) != 0x0) ||							// dlist offset not 4 byte aligned
                  ((newObj.DListOffset & 0xFF000000) == 0x80000000))	// dlist offset lies in ram
                    newObj.DListOffset = 0x00;
                

                stuffToAdd newHier = new stuffToAdd();
                newHier.id = index + 0x04000000;
                newHier.siblingID = MemoryManager.Instance.ReadUInt(bankNo, index + 0x18);
                newHier.childID = MemoryManager.Instance.ReadUInt(bankNo, index + 0x1C);

                index += 0x20;

                //if (newObj.DListOffset != 0x00)
                //{
                    SFGfx.GameObjects.Add(newObj);

                    newHier.gameObjectCount = SFGfx.GameObjCount;

                    SFGfx.GameObjCount++;
                //}
                //else
                 //   newHier.gameObjectCount = -1;

                hierarchyTesting.Add(newHier);
            }

            //Apply hierarchy stuff. For now, assume that there is only 1 at the top which is not a child
            stuffToAdd topLevel = hierarchyTesting.SingleOrDefault(x => hierarchyTesting.Count(y => y.childID == x.id || y.siblingID == x.id) == 0);
            Queue<stuffToAdd> nextTopLevel = new Queue<stuffToAdd>();
            while (topLevel.id != 0)
            {
                //Apply transformations each step down
                if (topLevel.childID != 0)
                {
                    SFGfx.GameObject parentObject = SFGfx.GameObjects[topLevel.gameObjectCount];

                    stuffToAdd childNode = hierarchyTesting.SingleOrDefault(x => topLevel.childID == x.id);
                    while (childNode.id != 0)
                    {
                        //Do the adding here
                        SFGfx.GameObject gameObject = SFGfx.GameObjects[childNode.gameObjectCount];

                        gameObject.X += parentObject.X;
                        gameObject.Y += parentObject.Y;
                        gameObject.Z += parentObject.Z;
                        gameObject.XRot += parentObject.XRot;
                        gameObject.YRot += parentObject.YRot;
                        gameObject.ZRot += parentObject.ZRot;

                        SFGfx.GameObjects[childNode.gameObjectCount] = gameObject;

                        if (childNode.siblingID != 0)
                            childNode = hierarchyTesting.SingleOrDefault(x => childNode.siblingID == x.id);
                        else
                            childNode.id = 0;
                    }
                }

                if(topLevel.siblingID != 0)
                    nextTopLevel.Enqueue(hierarchyTesting.SingleOrDefault(x => topLevel.siblingID == x.id));
                if(topLevel.childID != 0)
                    nextTopLevel.Enqueue(hierarchyTesting.SingleOrDefault(x => topLevel.childID == x.id));

                if (nextTopLevel.Count > 0)
                    topLevel = nextTopLevel.Dequeue();
                else
                    topLevel.id = 0;
            }

            for (int j = hierarchyTesting.Count - 1; j >= 0; j--)
            {
                if (SFGfx.GameObjects[hierarchyTesting[j].gameObjectCount].DListOffset == 0x0)
                {
                    SFGfx.GameObjects.RemoveAt(hierarchyTesting[j].gameObjectCount);
                    SFGfx.GameObjCount--;
                }
            }

            ExecuteDisplayLists();

            SFCamera.Reset();
        }

        private void btnLoadLevel_Click(object sender, EventArgs e)
        {
            //Load the model

            //Instructions: Point it to the line before the start of the two header pointers.

            byte bankNo = (byte)Convert.ToInt32(txtBank.Text, 16);
            int dma = Convert.ToInt32(txtDMA.Text, 10);
            uint offset = Convert.ToUInt32(txtOffset.Text, 16);

            MemoryManager.Instance.ClearBanks();

            //Initiate the level loading. Grab the correct offset info and pass it to the F3DEX parser
            DMATableEntry offsetTableDMA = _rom.DMATable[1];
            MemoryManager.Instance.AddBank((byte)0xFF, offsetTableDMA.DMAData, (uint)0x0);

            offset &= 0x00FFFFFF;

            //_glControl.Clear();
            MemoryManager.Instance.AddBank(bankNo, _rom.DMATable[dma].DMAData, 0x00);

            StartReadingLevelDataAt(bankNo, offset);

            //InitDListNavigEnabled(true);
            //SetupDList();

            _glControl.ReDraww();
        }

    }

    public struct stuffToAdd
    {
        public uint id;
        public uint siblingID;
        public uint childID;
        public int gameObjectCount;
    }

}
