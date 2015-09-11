using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.DataStructures;

namespace NewSF64Toolkit.ProgramTools.Controls
{
    public partial class RomInfoControl : UserControl
    {
        public RomInfoControl()
        {
            InitializeComponent();
            
            RefreshROMInfo();
        }

        private void RefreshROMInfo()
        {
            if (SF64ROM.Instance.IsROMLoaded)
            {
                txtFilename.Text = SF64ROM.Instance.Filename;
                txtSize.Text = ToolSettings.DisplayValue(SF64ROM.Instance.Size);
                txtTitle.Text = SF64ROM.Instance.Info.Title;
                txtGameID.Text = SF64ROM.Instance.Info.GameID;
                txtVersion.Text = SF64ROM.Instance.Info.Version.ToString();
                txtCRC1.Text = ToolSettings.DisplayValue(SF64ROM.Instance.Info.CRC1);
                txtCRC2.Text = ToolSettings.DisplayValue(SF64ROM.Instance.Info.CRC2);
            }
            else
            {
                ClearROMInfo();
            }
        }

        private void ClearROMInfo()
        {
            txtFilename.Text = string.Empty;
            txtSize.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtGameID.Text = string.Empty;
            txtVersion.Text = string.Empty;
            txtCRC1.Text = string.Empty;
            txtCRC2.Text = string.Empty;
        }
    }
}
