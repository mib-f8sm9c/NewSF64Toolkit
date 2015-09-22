using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.DataStructures;

namespace NewSF64Toolkit.Tools.Controls
{
    public partial class RomInfoControl : UserControl
    {
        public RomInfoControl()
        {
            InitializeComponent();
            
            RefreshROMInfo();
        }

        public void RefreshROMInfo()
        {
            if (SF64ROM.Instance.IsROMLoaded)
            {
                txtFilename.Text = SF64ROM.Instance.Filename;
                txtSize.Text = ByteHelper.DisplayValue(SF64ROM.Instance.Size);
                txtTitle.Text = SF64ROM.Instance.HeaderInfo.Title;
                txtGameID.Text = SF64ROM.Instance.HeaderInfo.GameID;
                txtVersion.Text = SF64ROM.Instance.HeaderInfo.Version.ToString();
                txtCRC1.Text = ByteHelper.DisplayValue(SF64ROM.Instance.HeaderInfo.CRC1);
                txtCRC2.Text = ByteHelper.DisplayValue(SF64ROM.Instance.HeaderInfo.CRC2);
            }
            else
            {
                ClearROMInfo();
            }
        }

        public void ClearROMInfo()
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
