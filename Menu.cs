using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CURS_PDF_Encoder
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void скрытиеИнформацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 F1;
            F1 = new Form1();
            F1.ShowDialog();
        }

        //private void извлечениеИнформацииToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    Form2 F2;
        //    F2 = new Form2();
        //    F2.ShowDialog();
        //}

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AB F2;
            F2 = new AB();
            F2.ShowDialog();

        }

        //private void отладкаToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    PDF_Parsing F2;
        //    F2 = new PDF_Parsing();
        //    F2.ShowDialog();
        //}
    }
}
