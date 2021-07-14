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
            myBooleanOutput1.SetEquation("(a.b)+(b.a)");
            myBooleanOutput1.SetParameters("ab", 1, 0);
            bool result = myBooleanOutput1.GetResult();

            MessageBox.Show(result.ToString());
        }
    }
}
