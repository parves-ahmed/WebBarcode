using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBarcode
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            //var Country = Request.QueryString["Country"];
            BindGridView();
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
            var CountryName = Request.QueryString["Country"]; ;
            var items = persons.FindAll(p => p.Country == CountryName);
            PersonGridViewList.DataSource = items;
            PersonGridViewList.DataBind();
        }

        protected void ExportToPDFHtml(object sender, EventArgs e)
        {
            StringReader sr = new StringReader(Request.Form[hfGridHtml1.UniqueID]);
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
    }
}