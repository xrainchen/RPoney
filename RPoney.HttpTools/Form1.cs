﻿using System;
using System.Linq;
using System.Windows.Forms;
using RPoney.HttpTools.Model;

namespace RPoney.HttpTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var requestModel = new RequestHeaderModel()
                {
                    Url = txtRequestUrl.Text,
                    Method = cbRequestMethod.Text,
                    Charset = cbCharset.Text,
                    UserAgent = cbUserAgent.Text,
                    Param = txtRequestData.Text,
                    ContentType = cbContentType.Text
                };
                txtReponstData.Text = new HttpService().GetResult(requestModel);
                //var requestUrl = txtRequestUrl.Text;
                //var reqeustMethod = cbRequestMethod.Text;
                //var encoding = Encoding.GetEncoding(cbCharset.Text);
                //switch (reqeustMethod.ToLower())
                //{
                //    case "get":
                //        txtReponstData.Text = RequestHelper.HttpGet(requestUrl, null, encoding);
                //        break;
                //    case "post":
                //        var contentType = cbContentType.Text;
                //        var requestData = txtRequestData.Text;
                //        var stream = new MemoryStream();
                //        var formDataBytes = string.IsNullOrWhiteSpace(requestData) ? new byte[0] : encoding.GetBytes(requestData);
                //        stream.Write(formDataBytes, 0, formDataBytes.Length);
                //        stream.Seek(0, SeekOrigin.Begin);
                //        txtReponstData.Text = RequestHelper.HttpPost(requestUrl, contentType, null, stream, null, null, encoding);
                //        break;
                //    default:
                //        txtRequestData.Text = txtReponstData.Text = string.Empty;
                //        break;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbRequestMethod.SelectedIndex = 0;
            cbContentType.SelectedIndex = 0;
            cbCharset.SelectedIndex = 0;
            cbUserAgent.SelectedIndex = 0;
        }

        private void btnSltFile_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog()
            {
                Multiselect = true
            };
            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            foreach (var file in fileDialog.FileNames)
            {
                txtFile.Text += file + Environment.NewLine;
            }
        }
    }
}
