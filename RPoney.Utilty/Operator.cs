using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using RPoney;

namespace RPoney.Utilty
{
    /// <summary>
    ///Operator 的摘要说明
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public static long CreateOrderNumber()
        {
            var oid = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999).ToString().PadLeft(2, '0');
            return oid.CLong(0, false);
        }

        /// <summary>
        /// 获取文件名的扩展名
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetExt(string FileName)
        {
            int index = FileName.LastIndexOf(".");
            return FileName.Substring(index);
        }

        /// <summary>
        /// 获取指定长度的字符串(只限中文)
        /// </summary>
        /// <param name="stringToSub">要截取的字符串</param>
        /// <param name="length">截取的长度</param>
        /// <returns></returns>		
        public static string GetChinaString(string stringToSub, int length)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            bool isCut = false;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }

                if (nLength > length)
                {
                    isCut = true;
                    break;
                }
            }
            if (isCut)
                return sb.ToString() + "..";
            else
                return sb.ToString();
        }

        /// <summary>
        /// 防sql注入
        /// </summary>
        /// <param name="str">要过虑的字符串</param>
        /// <returns></returns>
        public static string FiltSQL(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt");
            str = str.Replace("'", "''");
            str = str.Replace("*", "");
            str = str.Replace("\n", "<br/>");
            str = str.Replace("\r\n", "<br/>");
            //str   =   str.Replace("?","");   
            str = str.Replace("select", "");
            str = str.Replace("insert", "");
            str = str.Replace("update", "");
            str = str.Replace("delete", "");
            str = str.Replace("create", "");
            str = str.Replace("drop", "");
            str = str.Replace("delcare", "");
            str = str.Replace("   ", "&nbsp;");
            str = str.Replace("<script>", "");
            str = str.Replace("</script>", "");
            str = str.Trim();
            return str;
        }

        public static bool IsNumeric(string str)
        {
            Regex r = new Regex(@"^\d+(\.)?\d*$");
            if (r.IsMatch(str))
                return true;
            else
                return false;
        }

        #region 生成随即验证码
        /// <summary>
        /// 根据随机数生成验证码
        /// </summary>
        /// <param name="checkCode">随机数</param>
        public static void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 11);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 19);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Chocolate, Color.Brown, Color.DarkCyan, Color.Purple };
            Random rand = new Random();

            //输出不同字体和颜色的验证码字符
            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                Font f = new System.Drawing.Font("Microsoft Sans Serif", 11);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                g.DrawString(checkCode.Substring(i, 1), f, b, (i * 10) + 1, 0, StringFormat.GenericDefault);
            }
            //画一个边框
            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);

            //输出到浏览器
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Web.HttpContext.Current.Response.ClearContent();
            System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
            System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }

        /// <summary>
        /// 获得随机数
        /// </summary>
        /// <param name="num">几位数</param>
        /// <returns>随机数</returns>
        public static string GetValidateCode(int num)
        {
            char[] chars = "0123456789".ToCharArray();
            System.Random random = new Random();
            string validateCode = string.Empty;
            for (int i = 0; i < num; i++)
            {
                char rc = chars[random.Next(0, chars.Length)];
                if (validateCode.IndexOf(rc) > -1)
                {
                    i--;
                    continue;
                }
                validateCode += rc;
            }
            return validateCode;
        }
        #endregion

        #region 导出Excel
        ///// <summary>
        ///// 导出Excel
        ///// </summary>
        ///// <param name="dtData">数据集</param>
        ///// <param name="filename">文件名</param>
        ///// <returns></returns>
        //public static ActionResult ToExcel(DataTable dtData, string filename)
        //{
        //    DataGrid dgExport = null;
        //    // 当前对话  
        //    HttpContext curContext = HttpContext.Current;
        //    // IO用于导出并返回excel文件  
        //    System.IO.StringWriter strWriter = null;
        //    HtmlTextWriter htmlWriter = null;
        //    if (string.IsNullOrEmpty(filename))
        //        filename = DateTime.Now.ToString("yyyyMMddHHmmss");

        //    byte[] str = null;
        //    if (dtData != null)
        //    {
        //        // 设置编码和附件格式  
        //        curContext.Response.ContentType = "application/vnd.ms-excel";
        //        curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //        curContext.Response.Charset = "gb2312";
        //        curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".xls");
        //        // 导出excel文件  
        //        strWriter = new System.IO.StringWriter();
        //        htmlWriter = new HtmlTextWriter(strWriter);

        //        // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid  
        //        dgExport = new DataGrid();
        //        dgExport.DataSource = dtData.DefaultView;
        //        dgExport.AllowPaging = false;
        //        dgExport.DataBind();
        //        dgExport.RenderControl(htmlWriter);
        //        // 返回客户端  
        //        str = Encoding.UTF8.GetBytes(strWriter.ToString());
        //    }
        //    System.Web.Mvc.FileContentResult sfile = new System.Web.Mvc.FileContentResult(str, "attachment;filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".xls");
        //    return sfile;
        //}

        public static void ToExcel(string templateFile, string fileName, DataTable dt, string title = "")
        {
            try
            {
                System.IO.Stream fileStream = null;
                using (ExcelReport report = new ExcelReport())
                {
                    report.ValueMappings.Add("Title", title);
                    report.TemplateFilePath = HttpContext.Current.Server.MapPath(templateFile);
                    report.DataSource = dt;
                    fileStream = report.Generate();
                }
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString());
                HttpContext.Current.Response.ContentType = "application/xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                HttpContext.Current.Response.BinaryWrite(((System.IO.MemoryStream)fileStream).ToArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                RPoney.Log.LoggerManager.Error("Operator.ToExcel", "导出报错到模版文件异常：", ex);
                throw ex;
            }

        }
        public static void ToExcel(string templateFile, string fileName, DataTable dt, Dictionary<string, string> valueMappings)
        {
            try
            {
                System.IO.Stream fileStream = null;
                using (ExcelReport report = new ExcelReport())
                {
                    report.ValueMappings = valueMappings;
                    report.TemplateFilePath = HttpContext.Current.Server.MapPath(templateFile);
                    report.DataSource = dt;
                    fileStream = report.Generate();
                }
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString());
                HttpContext.Current.Response.ContentType = "application/xls"; ;
                System.IO.StringWriter tw = new System.IO.StringWriter();
                HttpContext.Current.Response.BinaryWrite(((System.IO.MemoryStream)fileStream).ToArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                RPoney.Log.LoggerManager.Error("Operator.ToExcel", "导出报错到模版文件异常：", ex);
                throw ex;
            }

        }
        #endregion

        #region Excel转DataTable

        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="fileName">excel文件路径</param>
        /// <param name="sheetName">表格名称</param>
        /// <param name="startRow">起始行 行数索引从0开始</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, int startRow)
        {
            try
            {
                var data = new DataTable();
                var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs);
                var sheet = string.IsNullOrEmpty(sheetName) ? workbook.GetSheetAt(0) : (workbook.GetSheet(sheetName) ?? workbook.GetSheetAt(0));
                if (sheet != null)
                {
                    var firstRow = sheet.GetRow(startRow);
                    var cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        var cell = firstRow.GetCell(i);
                        if (null != cell)
                        {
                            dynamic cellValue;
                            switch (cell.CellType)
                            {
                                case CellType.Numeric://NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                                    if (DateUtil.IsCellDateFormatted(cell))//日期类型
                                    {
                                        cellValue = cell.DateCellValue;
                                    }
                                    else//其他数字类型
                                    {
                                        cellValue = cell.NumericCellValue;
                                    }
                                    break;
                                case CellType.Blank:
                                    cellValue = "";
                                    break;
                                case CellType.Formula://公式
                                    HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(workbook);
                                    cellValue = eva.Evaluate(cell).StringValue;
                                    break;
                                default:
                                    cellValue = cell.StringCellValue;
                                    break;
                            }
                            if (cellValue != null)
                            {
                                var column = new DataColumn(cellValue.ToString());
                                data.Columns.Add(column);
                            }
                        }
                    }
                    startRow = startRow + 1;

                    //最后一列的标号
                    var rowCount = sheet.LastRowNum;
                    for (var i = startRow; i <= rowCount; ++i)
                    {
                        var row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　
                        var dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                RPoney.Log.LoggerManager.Error("Operator.ToExcel", "Excel文件解析报错：", ex);
                return null;
            }
        }
        #endregion



        #region List泛型集合转换为DataTable
        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            return ToDataTable<T>(list, null, null);
        }

        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">需要返回的列的列名</param>
        /// <param name="propertyChineseName">需要返回的列的中文列名</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list, string[] propertyName, string[] propertyChineseName)
        {
            List<string> propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);
            DataTable result = new DataTable();
            if (list.Count > 0)
            {

                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0 || propertyName.Length != propertyChineseName.Length)
                    {
                        result.Columns.Add(pi.Name);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                        {
                            int index = propertyNameList.IndexOf(pi.Name);
                            result.Columns.Add(propertyChineseName[index]);
                        }
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            if (obj != null)
                            {
                                tempList.Add(obj.ToString().Trim());
                            }
                            else
                            {
                                tempList.Add(obj);
                            }
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                if (obj != null)
                                {
                                    tempList.Add(obj.ToString().Trim());
                                }
                                else
                                {
                                    tempList.Add(obj);
                                }
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">需要返回的列的列名</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list, string[] propertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        //var _type = pi.PropertyType;
                        result.Columns.Add(pi.Name);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            if (obj != null)
                            {
                                tempList.Add(obj.ToString().Trim());
                            }
                            else
                            {
                                tempList.Add(obj);
                            }
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                if (obj != null)
                                {
                                    tempList.Add(obj.ToString().Trim());
                                }
                                else
                                {
                                    tempList.Add(obj);
                                }
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        #endregion
    }
}
