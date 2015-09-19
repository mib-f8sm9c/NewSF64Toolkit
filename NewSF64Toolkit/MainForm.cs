using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Design;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.Tools;
using NewSF64Toolkit.Settings;
using NewSF64Toolkit.OpenGL.F3DEX;

namespace NewSF64Toolkit
{
    public partial class MainForm : Form
    {
        private const string STATUS_NO_FILE_LOADED = "No file loaded";
        private const string STATUS_FILE_LOADED = "ROM file loaded";

        private string[] VALID_ROM_EXTENSIONS = { ".ROM", ".Z64", ".N64" };

        private BaseToolkitTool _currentTool;

        public MainForm()
        {
            InitializeComponent();

            ToolSettings.Load();

            tsStatus.Text = STATUS_NO_FILE_LOADED;

            SwitchToolkitMode(ToolTypes.RomInfo);
            UpdateMenuStripToolCheckState(menuStripToolsInfo);
        }

        #region Event handlers

        private void menuStripFileLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "sf64.z64";
            openFileDialog.DefaultExt = "";

            //Load the rom here
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            
            string romFile = openFileDialog.FileName;

            if (!File.Exists(romFile))
            {
                //Error message
                MessageBox.Show("File not found, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!HasRomExtension(romFile))
            {
                //Error message
                MessageBox.Show("Specified file is not a rom file, please check the extension and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!SF64ROM.LoadFromROM(romFile))
            {
                MessageBox.Show("Unable to properly load the ROM, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SF64ROM.ResetRom();
                return;
            }

            //This will happen if it did not find an appropriate GameID/Version match
            if (!SF64ROM.Instance.IsValidRom)
            {
                MessageBox.Show("Unable to identify the ROM, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SF64ROM.ResetRom();
                return;
            }

            tsStatus.Text = STATUS_FILE_LOADED;
            string fileName = Path.GetFileName(romFile);
            saveFileDialog.DefaultExt = Path.GetExtension(romFile);
            saveFileDialog.FileName = fileName;


            //We need a way to discriminate the endianess of the system, and keep the data
            //    right side forward. EDIT: Endianness is described at header of ROM file, see
            //    http://www.emutalk.net/archive/index.php/t-16045.html


        }

        private void menuStripViewHex_Click(object sender, EventArgs e)
        {
            ToolSettings.Instance.DisplayInHex = menuStripViewHex.Checked;
        }

        private void menuStripFileSave_Click(object sender, EventArgs e)
        {
            if (!SF64ROM.Instance.IsROMLoaded || !SF64ROM.Instance.IsValidRom)
            {
                //Error message
                MessageBox.Show("No valid ROM file loaded currently, please load a ROM and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //TEST THIS LATER PLEASE
            if (!SF64ROM.Instance.HasGoodChecksum)
            {
                DialogResult result = MessageBox.Show("ROM has bad CRCs, fix?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    SF64ROM.Instance.FixCRC();
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            SF64ROM.SaveRomTo(saveFileDialog.FileName);
        }

        //Needs to be fixed, now that the system has been changed up
        private void menuStripFileLoadDMA_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();

            ////Load up DMA tables directly
            //openFileDialog.FileName = "layout.txt";
            //openFileDialog.DefaultExt = ".txt";

            //if(openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //{
            //    return;
            //}

            //if(!File.Exists(openFileDialog.FileName))
            //{
            //    //Error message
            //    MessageBox.Show("File not found, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //uint fileSize = 0;
            //List<string> dmaFileNames = new List<string>();
            //string fileName;

            //using(StreamReader reader = new StreamReader(openFileDialog.FileName))
            //{
            //    string firstLine = reader.ReadLine();

            //    string length = firstLine.Substring(0, 8);
            //    fileSize = Convert.ToUInt32(length, 16);
            //    fileName = firstLine.Split(' ')[1];

            //    while(!reader.EndOfStream)
            //    {
            //        dmaFileNames.Add(reader.ReadLine());
            //    }
            //}

            //if(dmaFileNames.Count == 0 || fileSize == 0)
            //{
            //    //Error message
            //    MessageBox.Show("Incorrect layout format, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //List<byte[]> dmaEntries = new List<byte[]>();

            //string directory = Path.GetDirectoryName(openFileDialog.FileName);

            //foreach(string dmaFilename in dmaFileNames)
            //{
            //    string fullDmaPath = Path.Combine(directory, dmaFilename);

            //    if(!File.Exists(fullDmaPath))
            //    {
            //        //Error message
            //        MessageBox.Show("DMA entry not found, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    using(StreamReader reader = new StreamReader(fullDmaPath))
            //    {
            //        byte[] bytes = new byte[reader.BaseStream.Length];

            //        reader.BaseStream.Read(bytes, 0, (int)reader.BaseStream.Length);

            //        dmaEntries.Add(bytes);
            //    }
            //}

            //SF64ROM.LoadFromDMATables(fileName, dmaEntries);

            ////This will happen if it did not find an appropriate GameID/Version match
            //if (!SF64ROM.Instance.IsValidRom)
            //{
            //    MessageBox.Show("Unable to identify the ROM, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    SF64ROM.ResetRom();
            //    return;
            //}

            //tsStatus.Text = STATUS_FILE_LOADED;
        }

        //Needs to be fixed, now that the system has been changed up
        private void menuStripFileSaveDMA_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();

            ////Save DMA tables directly

            //if (!SF64ROM.Instance.IsROMLoaded || !SF64ROM.Instance.IsValidRom)
            //{
            //    //Error message
            //    MessageBox.Show("No valid ROM file loaded currently, please load a ROM and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //if(folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //    return;

            //string outputFolderPath = folderBrowserDialog.SelectedPath;

            //if(!Directory.Exists(outputFolderPath))
            //    Directory.CreateDirectory(outputFolderPath);

            //string layoutFilePath = Path.Combine(outputFolderPath, "layout.txt");

            //List<string> layoutText = new List<string>();
            //int totalDMALength = 0x0;

            //layoutText.Add("00000000 rebuilt.z64");

            //for (int i = 0; i < SF64ROM.Instance.DMATable.Count; i++)
            //{
            //    DMAFile dma = SF64ROM.Instance.DMATable[i];

            //    //Need to append filepath!!!!
            //    string fileName = string.Format("{0:00}_{1:X8}-{2:X8}_vs{3:X8}.{4}", i, dma.PStart, dma.PEnd, dma.VStart, (dma.CompFlag == 0x1 ? "mio" : "bin"));

            //    layoutText.Add(string.Format("{0:00}_{1:X8}-{2:X8}_vs{3:X8}.bin", i, dma.PStart, dma.PEnd, dma.VStart));

            //    //Write file
            //    string outputFile = Path.Combine(outputFolderPath, fileName);
            //    using(StreamWriter writer = new StreamWriter(outputFile))
            //    {
            //        writer.BaseStream.Write(dma.DMAData, 0, dma.DMAData.Length);
            //    }
                
            //    //if compressed, decompress and make new file
            //    if(dma.CompFlag == 0x1)
            //    {
            //        byte[] decompressedData = null;
            //        if(StarFoxRomInfo.DecompressMIO0(dma.DMAData, out decompressedData))
            //        {
            //            fileName = Path.ChangeExtension(fileName, "bin");
            //            outputFile = Path.Combine(outputFolderPath, fileName);
            //            using(StreamWriter writer = new StreamWriter(outputFile))
            //            {
            //                writer.BaseStream.Write(decompressedData, 0, decompressedData.Length);
            //            }

            //            totalDMALength += decompressedData.Length;
            //        }
            //    }
            //    else
            //    {
            //        totalDMALength += dma.DMAData.Length;
            //    }
            //}

            //totalDMALength = (totalDMALength / 0x400000 + 1) * 0x400000;

            //layoutText[0] = string.Format("{0:X8} rebuilt.z64", totalDMALength);

            //using(StreamWriter writer = new StreamWriter(layoutFilePath))
            //{
            //    foreach(string text in layoutText)
            //        writer.WriteLine(text);
            //}

        }

        private void menuStripROMFixCRCs_Click(object sender, EventArgs e)
        {
            SF64ROM.Instance.FixCRC();
        }

        private void menuStripROMDecompress_Click(object sender, EventArgs e)
        {
            SF64ROM.Instance.Decompress();
        }

        private void menuStripViewWireframe_Click(object sender, EventArgs e)
        {
            ToolSettings.Instance.UseWireframe = menuStripViewWireframe.Checked;
        }

        private void menuStripToolsInfo_Click(object sender, EventArgs e)
        {
            SwitchToolkitMode(ToolTypes.RomInfo);
            UpdateMenuStripToolCheckState(menuStripToolsInfo);
        }

        private void menuStripToolsHex_Click(object sender, EventArgs e)
        {
            SwitchToolkitMode(ToolTypes.HexEditor);
            UpdateMenuStripToolCheckState(menuStripToolsHex);
        }

        private void menuStripToolsLevel_Click(object sender, EventArgs e)
        {
            SwitchToolkitMode(ToolTypes.LevelViewer);
            UpdateMenuStripToolCheckState(menuStripToolsLevel);
        }

        #endregion

        #region Private methods

        private bool HasRomExtension(string fileName)
        {
            string ext = Path.GetExtension(fileName.ToUpper());

            return VALID_ROM_EXTENSIONS.Contains(ext);
        }

        private void UpdateMenuStripToolCheckState(ToolStripMenuItem checkedItem)
        {
            foreach (ToolStripMenuItem m in menuStripTools.DropDownItems)
            {
                if (m == checkedItem)
                    m.CheckState = CheckState.Indeterminate;
                else
                    m.CheckState = CheckState.Unchecked;
            }
        }

        private void SwitchToolkitMode(ToolTypes type)
        {
            if (_currentTool != null)
            {
                pnlCurrentTool.Controls.Remove(_currentTool.GetToolControl());
                _currentTool.DeActivate();
            }

            _currentTool = ToolkitFactory.GetTool(type);

            _currentTool.Activate();
            pnlCurrentTool.Controls.Add(_currentTool.GetToolControl());

        }

        #endregion

    }
}
