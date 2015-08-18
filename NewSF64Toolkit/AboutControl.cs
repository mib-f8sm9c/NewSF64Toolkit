using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewSF64Toolkit
{
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();

            this.lblVersion.Text = string.Format("V.{0}.{1}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major, 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor);
        }
    }
}
