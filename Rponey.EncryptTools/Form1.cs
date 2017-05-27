using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPoney;

namespace Rponey.EncryptTools
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var control in groupBox1.Controls)
            {
                var radio = control as RadioButton;
                if (radio != null && radio.Checked)
                {
                    var sourceText = textBox2.Text;
                    if (radio.Text == @"加密")
                    {
                        textBox3.Text = sourceText.Encrypt(textBox1.Text.CInt(0, false));
                    }
                    else
                    {
                        textBox3.Text = sourceText.Decrypt(textBox1.Text.CInt(0, false));
                    }
                    break;
                }
            }
        }
    }
}
