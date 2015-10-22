using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.DataStructures;

namespace NewSF64Toolkit.Tools.Controls.ResourceSubControls
{
    public partial class SF64RomResourceControl : ResourceControl
    {
        public SF64RomResourceControl()
        {
            InitializeComponent();
        }

        public override void SetObject(object obj)
        {
            if (!(obj is SF64ROM))
            {
                txtFilename.Text = string.Empty;
                txtSize.Text = string.Empty;
                txtTitle.Text = string.Empty;
                txtGameID.Text = string.Empty;
                txtVersion.Text = string.Empty;
                txtCRC1.Text = string.Empty;
                txtCRC2.Text = string.Empty;
            
                return;
            }

            SF64ROM rom = (SF64ROM)obj;

            txtFilename.Text = rom.Filename;
            txtSize.Text = ByteHelper.DisplayValue(rom.Size);
            txtTitle.Text = rom.HeaderInfo.Title;
            txtGameID.Text = rom.HeaderInfo.GameID;
            txtVersion.Text = rom.HeaderInfo.Version.ToString();
            txtCRC1.Text = ByteHelper.DisplayValue(rom.HeaderInfo.CRC1);
            txtCRC2.Text = ByteHelper.DisplayValue(rom.HeaderInfo.CRC2);
            
        }
    }
}
