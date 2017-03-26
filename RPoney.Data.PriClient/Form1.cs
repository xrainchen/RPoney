using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RPoney.Data.PriClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = ConnEncrypt.DecryptFile(openFileDialog1.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnStringConfig config = ConnStringConfig.FromConnString(textBox1.Text);
            saveFileDialog1.FileName = config.InitialCatalog;
            saveFileDialog1.ShowDialog();

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var str = textBox1.Text.Trim();
            var fileName = saveFileDialog1.FileName;
            if (string.IsNullOrEmpty(str))
            {
                MessageBox.Show("请输入连接字符串");
            }
            else
            {
                ConnEncrypt.EncryptAndWriteFile(str, fileName);
                MessageBox.Show("文件写入成功");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connString = this.textBox1.Text.Trim();
            ConnStringConfig config = ConnStringConfig.FromConnString(connString);
            config.ConnectionTimeOut = TimeSpan.FromSeconds(1.0);
            bool flag = TryConn(config.ToString());
            if (flag)
            {
                flag = TryConn(connString);
            }
            MessageBox.Show(flag ? "连接成功" : "连接失败");

        }

        private static bool TryConn(string connstring)
        {
            SqlConnection connection = null;
            bool flag;
            try
            {
                connection = new SqlConnection(connstring);
                connection.Open();
                flag = connection.State == ConnectionState.Open;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "连接失败");
                flag = false;
            }
            finally
            {
                if ((connection != null) && (connection.State == ConnectionState.Open))
                {
                    connection.Close();
                }
            }
            return flag;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("功能未开放");
            //ConnCreater creater = new ConnCreater
            //{
            //    Owner = this
            //};
            //creater.SetConnString(this.textBox1.Text.Trim());
            //creater.ShowDialog();

        }
    }
}
