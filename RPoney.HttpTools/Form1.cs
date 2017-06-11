using System;
using System.IO;
using System.Linq;
using System.Text;
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

        private readonly Lazy<HttpService> _httpService = new Lazy<HttpService>();
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var refContentType = string.Empty;
                var requestModel = new RequestHeaderModel()
                {
                    Url = txtRequestUrl.Text,
                    Method = cbRequestMethod.Text,
                    Charset = cbCharset.Text,
                    UserAgent = cbUserAgent.Text,
                    Param = txtRequestData.Text,
                    ContentType = cbContentType.Text
                };
                if (!string.IsNullOrWhiteSpace(txtFile.Text))
                {
                    using (var sr = new StringReader(txtFile.Text))
                    {
                        string filename;
                        var fileUrl = new StringBuilder();
                        while ((filename = sr.ReadLine()) != null)
                        {
                            requestModel.FileStream = GetFileStream(filename, ref refContentType);
                            if (!string.IsNullOrWhiteSpace(refContentType))
                            {
                                requestModel.ContentType = refContentType;
                            }
                            try
                            {
                                fileUrl.AppendLine($"{filename}:{_httpService.Value.GetResult(requestModel)}");
                            }
                            catch (Exception ex)
                            {
                                fileUrl.AppendLine($"{filename}上传失败:{ex.Message}");
                            }
                        }
                        txtReponstData.Text = fileUrl.ToString();
                    }
                }
                else
                {
                    txtReponstData.Text = _httpService.Value.GetResult(requestModel);
                }
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

        private Stream GetFileStream(string fileName, ref string contentType)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return null;
            var postStream = new MemoryStream();
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var boundary = DateTime.Now.Ticks.ToString("x");
                var itemBoundaryBytes = Encoding.GetEncoding(cbCharset.Text).GetBytes("\r\n--" + boundary + "\r\n");
                var endBoundaryBytes = Encoding.GetEncoding(cbCharset.Text).GetBytes("\r\n--" + boundary + "--\r\n");
                //请求头部信息
                var sbHeader =
                    $"Content-Disposition:form-data;name=\"media\";filename=\"{Path.GetFileName(fileName)}\"\r\nContent-Type:application/octet-stream\r\n\r\n";
                var postHeaderBytes = Encoding.GetEncoding(cbCharset.Text).GetBytes(sbHeader);
                postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                var buffer = new byte[1024];
                var bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    postStream.Write(buffer, 0, bytesRead);
                }
                postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                contentType = $"multipart/form-data; boundary={boundary}";
            }
            return postStream;
        }
    }
}
