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

namespace NewSF64Toolkit.Tools.Debug.Controls
{
    public partial class F3DEXViewerControl : UserControl
    {
        private OpenGLControl _glControl;

        public F3DEXViewerControl()
        {
            InitializeComponent();

            _glControl = new OpenGLControl();
            this.glPanel.Controls.Add(_glControl);
            _glControl.Dock = DockStyle.Fill;
            _glControl.Mode = OpenGLControl.DisplayMode.SingleModelView;
        }

        private void btnLoadLevel_Click(object sender, EventArgs e)
        {
            //if (!SF64ROM.Instance.IsROMLoaded || SF64ROM.Instance.DMATable.Count <= levelDMAIndex)
            //{
            //    //Error message
            //    MessageBox.Show("Rom file not loaded correctly, try reloading the ROM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //if (SF64ROM.Instance.DMATable[levelDMAIndex].DMAInfo.CFlag == 0x01)
            //{
            //    //Error message
            //    MessageBox.Show("Specified level file is compressed, decompress before trying again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            int dmaIndex = Convert.ToInt32(txtDMA.Text);
            int offset = Convert.ToInt32(txtOffset.Text, 16);

            F3DEXParser _f3dex = new F3DEXParser();

            //_f3dex.InitInvalidModels();

            byte[] dmaBytes = SF64ROM.Instance.DMATable[dmaIndex].GetAsBytes();

            //We know that the 0x190 is the limit for the simple objects
            //if (obj.ID < 0x190)
            //{
            //    obj.DListOffset = _referenceDMA.SimpleObjects[obj.ID].DListOffset;

            //    // dlist offset sanity checks
            //    if (((obj.DListOffset & 3) != 0x0) ||							// dlist offset not 4 byte aligned
            //        ((obj.DListOffset & 0xFF000000) == 0x80000000))	// dlist offset lies in ram, leave it alone for now
            //        obj.DListOffset = 0x00;

            //    else if (obj.DListOffset != 0x00 && (byte)((obj.DListOffset & 0xFF000000) >> 24) == 0x06) //Need segment 6
            //    {
            //        //Load through the F3DEX parser, assign the DisplayListIndex to the Simple Object in the Reference DMA
            //        //In the future we'll serialize the F3DEX commands/textures/vertices too, but for functional purposes
            //        // we'll just use the blank binary
            //        if (_referenceDMA.SimpleObjects[obj.ID].GLDisplayListOffset == F3DEXParser.InvalidBox)
            //            _referenceDMA.SimpleObjects[obj.ID].GLDisplayListOffset = _f3dex.ReadGameObject(levelBytes, obj.DListOffset); //KEEP AN EYE OUT FOR ANY CROSS-DMA REFERENCES
            //    }
            //}

            try
            {
                _glControl.SingleObjectDLIndices = _f3dex.ReadGameObject(SF64ROM.Instance.DMATable[dmaIndex], dmaBytes, (uint)offset);
            }
            catch
            {
                _glControl.SingleObjectDLIndices = F3DEXParser.InvalidBox;
            }

            _glControl.SelectedObjectIndex = -1;
            _glControl.ReDraww();
        }

        public void RefreshGL()
        {
            _glControl.ReDraww();
        }

        public void ResetGL()
        {
            _glControl.LevelObjects = new List<SFLevelObject>();
            _glControl.SelectedObjectIndex = -1;
            _glControl.ReDraww();
        }

    }
}
