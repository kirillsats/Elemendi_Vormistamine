using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class TeineVorm : Form
    {
        public TeineVorm(int w,int h)
        {
            this.Width = w;
            this.Height = h;
            this.MouseDoubleClick += TeineVorm_MouseDoubleClick;
            
        }

        private void TeineVorm_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            
            if (cd.ShowDialog()==DialogResult.OK)
            {
                this.BackColor = cd.Color;
            }
        }
    }
}
