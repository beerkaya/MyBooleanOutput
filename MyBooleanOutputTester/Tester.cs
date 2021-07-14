using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBooleanOutputTester
{
    public partial class Tester : Form
    {
        public Tester()
        {
            InitializeComponent();
        }

        private void Tester_Load(object sender, EventArgs e)
        {
            myBooleanOutput1.SetEquation("0+0+a");
            myBooleanOutput1.SetParameters("a", 1);
            bool result = myBooleanOutput1.GetResult();

            MessageBox.Show(result.ToString());
        }
    }
}
