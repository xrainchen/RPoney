using RPoney;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using d = System.Data;

namespace RPoney.Utilty
{
    /// <summary>
    ///ExcelReport 的摘要说明
    /// </summary>
    public class ExcelReport : IDisposable
    {
        private static object lockObj = new object();

        private int bindBeginColumn = -1;

        //开始绑定的列序号
        private int bindEndColumn = -1;

        private IRow bindRow;

        //结束绑定的列序号
        private int bindRowIndex = -1;

        private Dictionary<int, MatchCollection> bindValueMappings = new Dictionary<int, MatchCollection>();

        private HSSFWorkbook book;

        private Regex regFindBind;

        private Regex regFindSingle;

        //用于查找固定单一值匹配的正则
        //用户查找绑定匹配的正则
        private int sheetIndex = 0;

        public ExcelReport()
        {
            //Microsoft.Office.Interop.Excel.ApplicationClass a=new Microsoft.Office.Interop.Excel.ApplicationClass();
            _valueMappings = new Dictionary<string, string>();
            regFindSingle = new Regex("<%=(?<name>\\S+[^%])%>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            regFindBind = new Regex("<%#(?<propertyname>\\S+[^%])%>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// 单元格数据绑定后
        /// </summary>
        public event EventHandler<ExcelReportEventArgs> CellDataBinded;

        /// <summary>
        /// 单元格数据绑定前
        /// </summary>
        public event EventHandler<ExcelReportEventArgs> CellDataBinding;

        /// <summary>
        /// 数据填充完成后
        /// </summary>
        public event EventHandler Generated;

        /// <summary>
        /// 数据开始填充前
        /// </summary>
        public event EventHandler Generating;

        public int SheetIndex
        {
            get { return sheetIndex; }
            set { sheetIndex = value; }
        }

        public static DataTable ImportFromXLS(string xlsFile)
        {
            HSSFWorkbook wbook;
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(xlsFile, FileMode.Open, FileAccess.Read);
            wbook = new HSSFWorkbook(fs);
            ISheet sheet = wbook.GetSheetAt(0);
            int rowIndex = 0;
            IEnumerator rowEnum = sheet.GetRowEnumerator();
            while (rowEnum.MoveNext())
            {
                IRow row = (IRow)rowEnum.Current;

                DataRow datarow = null;
                if (rowIndex != 0)
                {
                    datarow = dt.NewRow();
                }
                for (int i = row.FirstCellNum; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);
                    if (rowIndex == 0)
                    {
                        DataColumn column = new DataColumn(cell.StringCellValue);
                        dt.Columns.Add(column);
                    }
                    else
                    {
                        string value = string.Empty;
                        switch (cell.CellType)
                        {
                            case CellType.Blank:
                                break;

                            case CellType.Boolean:
                                value = cell.BooleanCellValue.ToString();
                                break;

                            case CellType.Error:
                                break;

                            case CellType.Formula:
                                value = cell.RichStringCellValue.String;
                                break;

                            case CellType.Numeric:
                                value = cell.NumericCellValue.ToString();
                                break;

                            case CellType.String:
                                value = cell.StringCellValue;
                                break;

                            case CellType.Unknown:
                                break;

                            default:
                                break;
                        }
                        datarow[i] = value;
                    }
                }

                if (rowIndex != 0)
                {
                    dt.Rows.Add(datarow);
                }
                rowIndex++;
            }
            return dt;
        }

        #region 操作的Excel工作表

        /// <summary>
        /// 操作的Excel工作表
        /// </summary>
        public HSSFWorkbook Book
        {
            get
            {
                return book;
            }
        }

        #endregion 操作的Excel工作表

        #region 模板Excel地址

        private string _templateFilePath;

        /// <summary>
        /// 用于输出Excel的模板Excel地址
        /// </summary>
        public string TemplateFilePath
        {
            get { return _templateFilePath; }
            set { _templateFilePath = value; }
        }

        #endregion 模板Excel地址

        #region 用于固定单一值的映射

        private Dictionary<string, string> _valueMappings;

        /// <summary>
        /// 用于固定单一值的映射
        /// </summary>
        public Dictionary<string, string> ValueMappings
        {
            get { return _valueMappings; }
            set { _valueMappings = value; }
        }

        #endregion 用于固定单一值的映射

        #region 生成Excel的数据源

        private object _dataSource;
        private int dataCount = 0;

        /// <summary>
        /// 生成Excel的数据源
        /// </summary>
        public object DataSource
        {
            get { return _dataSource; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("参数不能为空！");
                if (value is IListSource || value is IList || value is IEnumerable || value is DataTable)
                {
                    _dataSource = value;
                }
                else
                {
                    throw new ArgumentException("数据源必须是DataTable或者是DataView或者实现了IListSource或IList或IEnumerable接口");
                }
                if (_dataSource is IListSource)
                {
                    dataCount = ((IListSource)_dataSource).GetList().Count;
                }
                else if (_dataSource is IList)
                {
                    dataCount = ((IList)_dataSource).Count;
                }
                else if (_dataSource is DataTable)
                {
                    dataCount = ((DataTable)_dataSource).DefaultView.Count;
                }
                else if (_dataSource is IEnumerable)
                {
                    IEnumerator et = ((IEnumerable)_dataSource).GetEnumerator();
                    while (et.MoveNext())
                    {
                        dataCount++;
                    }
                }
            }
        }

        #endregion 生成Excel的数据源

        #region 生成Excel文件

        /// <summary>
        /// 生成Excel文件
        /// </summary>
        /// <param name="filePath">生成文件的地址</param>
        public void Generate(string filePath)
        {
            GenerateBook();
            FileStream newFs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            book.Write(newFs);
            newFs.Close();
            //book.Dispose();
            book = null;
        }

        /// <summary>
        /// 生成，返回内存流
        /// </summary>
        /// <returns></returns>
        public Stream Generate()
        {
            GenerateBook();
            System.IO.MemoryStream ms = new MemoryStream();
            book.Write(ms);
            return ms;
        }

        private void GenerateBook()
        {
            System.IO.MemoryStream ms_source = new MemoryStream();
            if (!File.Exists(TemplateFilePath))
            {
                throw new FileNotFoundException(string.Format("模板文件：{0}不存在，请检查TemplateFilePath属性的值！", TemplateFilePath));
            }

            lock (lockObj)
            {
                byte[] tmpdata = new byte[1024];
                int realLen = 0;
                FileStream fs = new FileStream(TemplateFilePath, FileMode.Open, FileAccess.Read);
                while ((realLen = fs.Read(tmpdata, 0, tmpdata.Length)) > 0)
                {
                    ms_source.Write(tmpdata, 0, realLen);
                }
                fs.Close();
            }

            book = new HSSFWorkbook(ms_source);

            ISheet sheet = book.GetSheetAt(sheetIndex);

            if (Generating != null)
            {
                Generating(this, EventArgs.Empty);
            }
            SetFixed();
            if (bindRowIndex > -1)//需要绑定时
            {
                SetBinding();
            }
            if (Generated != null)
            {
                Generated(this, EventArgs.Empty);
            }
            sheet.ForceFormulaRecalculation = true;//重新计算公式
        }

        #endregion 生成Excel文件

        /// <summary>
        /// 获取对象实体值
        /// </summary>
        /// <param name="obj">要获取的对象</param>
        /// <param name="expression">绑定的表达式</param>
        /// <returns>返回值</returns>
        private string Eval(object obj, string expression)
        {
            object result = DataBinder.Eval(obj, expression);
            if (result == null)
                return string.Empty;
            else
                return result.ToString();
        }

        /// <summary>
        /// 获取对象实体值
        /// </summary>
        /// <param name="obj">要获取的对象</param>
        /// <param name="expression">绑定的表达式</param>
        /// <param name="format">格式化字符串</param>
        /// <returns>返回值</returns>
        private string Eval(object obj, string expression, string format)
        {
            return DataBinder.Eval(obj, expression, format);
        }

        //获取数据源
        private IEnumerable GetDataSource()
        {
            if (DataSource is d.DataTable)
            {
                return ((d.DataTable)DataSource).DefaultView;
            }
            if (DataSource is IListSource)
            {
                return ((IListSource)DataSource).GetList();
            }
            return DataSource as IEnumerable;
        }

        /// <summary>
        /// 在指定的行下方复制一行
        /// </summary>
        /// <param name="sheet">表</param>
        /// <param name="sourceRowIndex">要复制的行位置</param>
        /// <returns></returns>
        private void MyInsertRow(ISheet sheet, int sourceRowIndex, int count)
        {
            //假设sourceRowIndex=3，表示要绑定的为第4行

            if (sourceRowIndex <= sheet.LastRowNum - 1)
            {
                //如果第4行不是最后一行
                //先把第5行到最后的所有行向下移1行
                sheet.ShiftRows(sourceRowIndex + 1, sheet.LastRowNum, count, true, false);
            }

            //然后再把第4行详细移1行
            sheet.ShiftRows(sourceRowIndex, sourceRowIndex, count, true, false);
        }

        private void preBinding(IRow row)
        {
            bindValueMappings.Clear();
            for (int i = bindBeginColumn; i <= bindEndColumn; i++)
            {
                ICell cell = row.GetCell(i);
                if (cell == null)
                    continue;
                if (string.IsNullOrEmpty(cell.StringCellValue))
                    continue;

                bindValueMappings.Add(i, regFindBind.Matches(cell.StringCellValue));
            }
        }

        //替换所有固定表达式值
        private string ReplaceFixValue(string input)
        {
            string result = input;
            foreach (string key in ValueMappings.Keys)
            {
                result = Regex.Replace(result, "<%=" + key + "%>", ValueMappings[key], RegexOptions.IgnoreCase);
            }
            return result;
        }

        //绑定表达式的来源行
        //设置中间绑定部分
        private void SetBinding()
        {
            ISheet sheet = book.GetSheetAt(sheetIndex);
            MyInsertRow(sheet, bindRowIndex, dataCount);
            bindRow = sheet.GetRow(bindRowIndex + dataCount);
            preBinding(bindRow);

            foreach (object obj in GetDataSource())
            {
                IRow row = sheet.GetRow(bindRowIndex);
                //
                if (row == null)
                    row = sheet.CreateRow(bindRowIndex);
                SetBinding(row, obj);//逐行绑定
                bindRowIndex++;
            }
            //删除最后一个绑定行
            if (bindRowIndex == sheet.LastRowNum)
            {
                sheet.CreateRow(bindRowIndex);
            }
            else
            {
                sheet.ShiftRows(bindRowIndex + 1, sheet.LastRowNum, -1, true, true);
            }
        }

        //绑定一行
        private void SetBinding(IRow targetRow, object obj)
        {
            targetRow.Height = bindRow.Height;
            ICell targetCell = null;
            ICell sourceCell = null;
            for (int m = bindRow.FirstCellNum; m < bindRow.LastCellNum; m++)
            {
                #region 复制行

                sourceCell = bindRow.GetCell(m);
                if (sourceCell == null)
                    continue;
                targetCell = targetRow.GetCell(m);
                if (targetCell == null)
                    targetCell = targetRow.CreateCell(m);

                targetCell.CellStyle = sourceCell.CellStyle;
                targetCell.SetCellType(sourceCell.CellType);
                switch (sourceCell.CellType)
                {
                    case CellType.Blank:
                        break;

                    case CellType.Boolean:
                        targetCell.SetCellValue(sourceCell.BooleanCellValue);
                        break;

                    case CellType.Error:
                        targetCell.SetCellValue(sourceCell.ErrorCellValue);
                        break;

                    case CellType.Formula:
                        targetCell.SetCellValue(sourceCell.CellFormula);
                        break;

                    case CellType.Numeric:
                        targetCell.SetCellValue(sourceCell.NumericCellValue);
                        break;

                    case CellType.String:
                        targetCell.SetCellValue(sourceCell.StringCellValue);
                        break;

                    case CellType.Unknown:
                        break;
                }

                #endregion 复制行

                if (string.IsNullOrEmpty(sourceCell.StringCellValue))
                    continue;
                if (!bindValueMappings.ContainsKey(m))//不是被绑定的列
                    continue;
                string newValue = sourceCell.StringCellValue;
                var cellKey = string.Empty;
                foreach (Match mc in bindValueMappings[m])
                {
                    string key = cellKey = mc.Groups["propertyname"].Value;
                    string value = string.Empty;
                    if (key.IndexOf(',') > -1)//有格式化符号
                    {
                        string[] keys = key.Split(',');
                        value = Eval(obj, keys[0], keys[1]);
                    }
                    else
                    {
                        value = Eval(obj, key);
                    }
                    key = key.Replace("{", "\\{").Replace("}", "\\}");//转义字符替换
                    value = value.Replace("&#10;", "\n\r");
                    newValue = Regex.Replace(newValue, "<%#" + key + "%>", value, RegexOptions.IgnoreCase);
                }
                ExcelReportEventArgs e = new ExcelReportEventArgs(targetCell, newValue, obj);
                if (CellDataBinding != null)
                {
                    CellDataBinding(this, e);
                }
                if (!e.Cancel)//如果未取消事件
                {
                    double newDouble = 0;
                    if (cellKey.Contains("Name") || cellKey.Contains("Id"))
                    {
                        targetCell.SetCellValue(newValue);
                    }
                    else if (double.TryParse(newValue, out newDouble))
                    {
                        if (0 == newDouble)
                            targetCell.SetCellValue("-");
                        else
                        {
                            newDouble = newDouble.ToString("0.00").CDouble(0, false);
                            targetCell.SetCellValue(newDouble);
                        }

                    }
                    else if (newValue.EndsWith("%"))
                    {
                        newDouble = newValue.Replace("%", "").CDouble(0, false);
                        if (0 == newDouble)
                            targetCell.SetCellValue("-");
                        else
                        {
                            newDouble = newDouble.ToString("0.00").CDouble(0, false);
                            targetCell.SetCellValue(newDouble + "%");
                        }
                    }
                    else
                    {
                        targetCell.SetCellValue(newValue);
                    }
                }
                if (CellDataBinded != null)
                {
                    CellDataBinded(this, e);
                }
            }
        }

        //设置Excel表头表尾的固定值
        private void SetFixed()
        {
            ISheet sheet = book.GetSheetAt(sheetIndex);
            IEnumerator rowEnum = sheet.GetRowEnumerator();
            while (rowEnum.MoveNext())
            {
                IRow row = (IRow)rowEnum.Current;
                IEnumerator colEnum = row.GetEnumerator();
                while (colEnum.MoveNext())
                {
                    ICell cell = (ICell)colEnum.Current;
                    if (cell.CellType == CellType.String && !string.IsNullOrEmpty(cell.StringCellValue))
                    {
                        if (regFindBind.IsMatch(cell.StringCellValue))
                        {
                            if (bindRowIndex == -1)
                            {
                                bindRowIndex = row.RowNum;//设置要绑定的行所在的行号
                            }
                            if (bindBeginColumn == -1)
                            {
                                //绑定开始的列号
                                bindBeginColumn = cell.ColumnIndex;
                                bindEndColumn = cell.ColumnIndex;
                            }
                            else
                            {
                                //绑定结束的列号
                                bindEndColumn = cell.ColumnIndex;
                            }
                        }
                        cell.SetCellValue(ReplaceFixValue(cell.StringCellValue));
                    }//判断是否是字符
                }//循环列
            }//循环行
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (book != null)
            {
                //book.Dispose();
                book = null;
            }
        }

        #endregion IDisposable 成员
    }

    /// <summary>
    /// 用于报表绑定的事件数据类
    /// </summary>
    public class ExcelReportEventArgs : EventArgs
    {
        private bool cancel = false;
        private ICell cell;

        private object dataItem;

        private string newValue;

        public ExcelReportEventArgs(ICell cell, string newvalue, object dataItem)
        {
            this.cell = cell;
            this.newValue = newvalue;
            this.dataItem = dataItem;
        }

        /// <summary>
        /// 获取或设置是否取消事件
        /// </summary>
        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }

        /// <summary>
        /// 操作的单元格
        /// </summary>
        public ICell Cell
        {
            get { return cell; }
        }

        public object DataItem
        {
            get { return dataItem; }
        }

        /// <summary>
        /// 获取单元格内的新值
        /// </summary>
        public string NewValue
        {
            get { return newValue; }
        }
    }
}
