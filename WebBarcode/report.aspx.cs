using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Npgsql;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using BorderStyle = NPOI.SS.UserModel.BorderStyle;
using iTextSharp.tool.xml;

namespace WebBarcode
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void ExportToPDF(object sender, EventArgs e)
        {
            StringReader sr = new StringReader(Request.Form[hfGridHtml.UniqueID]);
            Document pdfDoc = new Document(PageSize.A4, 20f, 10f, 10f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=HTML.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }

        private void BindGridView()
        {
            List<UserDetails> persons = new List<UserDetails>()
             {
                 new UserDetails() {ID="1001", Name="ABCD", City ="City1", Country="Bangladesh"},
                 new UserDetails() {ID="1002", Name="PQRS", City ="City2", Country="Bangladesh"},
                 new UserDetails() {ID="1003", Name="XYZZ", City ="City3", Country="UK"},
                 new UserDetails() {ID="1004", Name="LMNO", City ="City4", Country="UK"},
            };
            List<string> employee = new List<string>();
            employee.Add("moon");
            EmployeeGridViewList.DataSource = persons;
            EmployeeGridViewList.DataBind();
        }

        protected void btnExportToWord_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=Employees.doc");
            Response.ContentType = "application/word";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);
            EmployeeGridViewList.HeaderRow.Style.Add("background-color", "#FFFFFF");
            EmployeeGridViewList.RenderControl(htw);
            Response.Write(stringWriter.ToString());
            Response.End();
        }

        private void CreateCell(IRow CurrentRow, int CellIndex, string Value, HSSFCellStyle Style)
        {
            ICell Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);
            Cell.CellStyle = Style;
        }

        private void CreateLink(HSSFWorkbook workbook, IRow CurrentRow, int CellIndex, string Value, String SheetName, String Country)
        {
            ICell Cell;

            ICellStyle hlink_style = workbook.CreateCellStyle();

            IFont hlink_font = workbook.CreateFont();

            hlink_font.Underline = FontUnderlineType.Single;

            hlink_font.Color = HSSFColor.Blue.Index;

            hlink_style.SetFont(hlink_font);
             
            ISheet sheet2 = workbook.CreateSheet(SheetName);
            LinkedSheet(sheet2, Country);
            //sheet2.CreateRow(0).CreateCell(0).SetCellValue("Target ICell");
            Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);

            HSSFHyperlink link = new HSSFHyperlink(HyperlinkType.Document);
            String address = "'" + SheetName + "'" + "!A1";
            //link.Address = ("'Target ISheet'!A2");
            link.Address = (address);

            Cell.Hyperlink = (link);

            Cell.CellStyle = (hlink_style);
        }

        private void LinkedSheet(ISheet sheet2, String Country)
        {
            List<UserDetails> persons = new List<UserDetails>()
             {
                 new UserDetails() {ID="1001", Name="ABCD", City ="City1", Country="Bangladesh"},
                 new UserDetails() {ID="1002", Name="PQRS", City ="City2", Country="Bangladesh"},
                 new UserDetails() {ID="1003", Name="XYZZ", City ="City3", Country="UK"},
                 new UserDetails() {ID="1004", Name="LMNO", City ="City4", Country="UK"},
            };

            var items = persons.FindAll(p => p.Country == Country);

            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

            DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(items), (typeof(DataTable)));
            //sheet2.CreateRow(0).CreateCell(0).SetCellValue("Target ICell");

            //ISheet excelSheet = workbook.CreateSheet("Target ISheet");

            List<String> columns = new List<string>();
            IRow row = sheet2.CreateRow(0);
            int columnIndex = 0;

            foreach (System.Data.DataColumn column in table.Columns)
            {
                columns.Add(column.ColumnName);
                row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                columnIndex++;
            }

            int rowIndex = 1;
            foreach (DataRow dsrow in table.Rows)
            {
                row = sheet2.CreateRow(rowIndex);
                int cellIndex = 0;
                foreach (String col in columns)
                {
                    row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
                    cellIndex++;
                }

                rowIndex++;
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {

            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Hyperlink");
            IRow HeaderRow = sheet.CreateRow(0);

            // Defining a border
            HSSFCellStyle borderedCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            borderedCellStyle.BorderLeft = BorderStyle.Medium;
            borderedCellStyle.BorderTop = BorderStyle.Medium;
            borderedCellStyle.BorderRight = BorderStyle.Medium;
            borderedCellStyle.BorderBottom = BorderStyle.Medium;
            borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;


            CreateCell(HeaderRow, 0, "Chesis No.", borderedCellStyle);
            CreateCell(HeaderRow, 1, "Color", borderedCellStyle);
            CreateCell(HeaderRow, 2, "Type", borderedCellStyle);
            CreateCell(HeaderRow, 3, "CC", borderedCellStyle);
            CreateCell(HeaderRow, 4, "Other", borderedCellStyle);

            IRow BodyRow = sheet.CreateRow(1);

            CreateCell(BodyRow, 0, "ch06789", borderedCellStyle);
            CreateCell(BodyRow, 1, "Red", borderedCellStyle);
            CreateCell(BodyRow, 2, "Motor Cycle", borderedCellStyle);
            CreateCell(BodyRow, 3, "100cc", borderedCellStyle);
            //CreateCell(BodyRow, 4, "100000", borderedCellStyle);
            CreateLink(workbook, BodyRow, 4, "SubForm", "Target ISheet", "Bangladesh");
            
            IRow BodyRow2 = sheet.CreateRow(2);

            CreateCell(BodyRow2, 0, "ch012345", borderedCellStyle);
            CreateCell(BodyRow2, 1, "Black", borderedCellStyle);
            CreateCell(BodyRow2, 2, "Motor Cycle", borderedCellStyle);
            CreateCell(BodyRow2, 3, "150cc", borderedCellStyle);
            //CreateCell(BodyRow, 4, "100000", borderedCellStyle);
            CreateLink(workbook, BodyRow2, 4, "SubForm", "Target ISheet2", "UK");

            int lastColumNum = sheet.GetRow(0).LastCellNum;
            for (int i = 0; i <= lastColumNum; i++)
            {
                sheet.AutoSizeColumn(i);
                GC.Collect();
            }

            // List<UserDetails> persons = new List<UserDetails>()
            // {
            //     new UserDetails() {ID="1001", Name="ABCD", City ="City1", Country="USA"},
            //     new UserDetails() {ID="1002", Name="PQRS", City ="City2", Country="INDIA"},
            //     new UserDetails() {ID="1003", Name="XYZZ", City ="City3", Country="CHINA"},
            //     new UserDetails() {ID="1004", Name="LMNO", City ="City4", Country="UK"},
            //};

            // // Lets converts our object data to Datatable for a simplified logic.
            // // Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

            // DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(persons), (typeof(DataTable)));


            // ISheet excelSheet = workbook.CreateSheet("Target ISheet");

            // List<String> columns = new List<string>();
            // IRow row = excelSheet.CreateRow(0);
            // int columnIndex = 0;

            // foreach (System.Data.DataColumn column in table.Columns)
            // {
            //     columns.Add(column.ColumnName);
            //     row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
            //     columnIndex++;
            // }

            // int rowIndex = 1;
            // foreach (DataRow dsrow in table.Rows)
            // {
            //     row = excelSheet.CreateRow(rowIndex);
            //     int cellIndex = 0;
            //     foreach (String col in columns)
            //     {
            //         row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
            //         cellIndex++;
            //     }

            //     rowIndex++;
            // }

            // //IWorkbook workbook = new XSSFWorkbook();
            // ICell cell;

            // ICellStyle hlink_style = workbook.CreateCellStyle();

            // IFont hlink_font = workbook.CreateFont();

            // hlink_font.Underline = FontUnderlineType.Single;

            // hlink_font.Color = HSSFColor.Blue.Index;

            // hlink_style.SetFont(hlink_font);


            // //ISheet sheet2 = workbook.CreateSheet("Target ISheet");

            // //sheet2.CreateRow(0).CreateCell(0).SetCellValue("Target ICell");

            // IRow srow = sheet.CreateRow(3);

            // cell = srow.CreateCell(1);
            // cell.SetCellValue("moon");
            // //cell = row.CreateCell(3);
            // //cell.SetCellValue("Worksheet Link");

            // XSSFHyperlink link = new XSSFHyperlink(HyperlinkType.Document);

            // link.Address = ("'Target ISheet'!A2");

            // cell.Hyperlink = (link);

            // cell.CellStyle = (hlink_style);

            using (MemoryStream exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Employees.xls"));

                Response.BinaryWrite(exportData.ToArray());

                // in lieu of Response.End(), which is said to kill the memorystream and did return exceptions,
                // used the following.  Found the same three lines in different forums, one of which was:
                // https://stackoverflow.com/questions/20988445/how-to-avoid-response-end-thread-was-being-aborted-exception-during-the-exce
                Response.Flush(); // Sends all currently buffered output to the client.
                Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Employees.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            EmployeeGridViewList.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
#pragma warning disable CS0612 // Type or member is obsolete  
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
#pragma warning restore CS0612 // Type or member is obsolete  
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            EmployeeGridViewList.AllowPaging = true;
            EmployeeGridViewList.DataBind();
        }

    }
}