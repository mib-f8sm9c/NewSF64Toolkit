using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.Settings;

namespace NewSF64Toolkit
{
    public partial class ProgramSettingsForm : Form
    {
        public ProgramSettingsForm()
        {
            InitializeComponent();

            cbAutoCRC.Checked = ToolSettings.Instance.AutoCRCFix;
            cbAutoDecompress.Checked = ToolSettings.Instance.AutoDecompress;
            cbShowDebug.Checked = ToolSettings.Instance.DisplayDebugTools;
        }

        private void ProgramSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ToolSettings.Instance.AutoCRCFix = cbAutoCRC.Checked;
            ToolSettings.Instance.AutoDecompress = cbAutoDecompress.Checked;
            ToolSettings.Instance.DisplayDebugTools = cbShowDebug.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
