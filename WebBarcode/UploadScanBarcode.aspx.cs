using OnBarcode.Barcode.BarcodeScanner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBarcode
{
    public partial class UploadScanBarcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            String localSavePath = "~/UploadFiles/";
            string str = string.Empty;
            string strImage = string.Empty;
            string strBarCode = string.Empty;
            if (FileUploadControl.HasFile)
            {
                String fileName = Path.GetFileName(FileUploadControl.FileName);
                localSavePath += fileName;
                FileUploadControl.SaveAs(Server.MapPath(localSavePath));
                Bitmap bitmap = null;
                str = "ok";
                try
                {
                    bitmap = new Bitmap(FileUploadControl.PostedFile.InputStream);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                if (bitmap == null)
                {
                    str = "Your file is not an image";
                }
                else
                {
                    strImage = "http://localhost:" + Request.Url.Port + "/UploadFiles/" + fileName;
                    strBarCode = ReadBarcodeFromFile(Server.MapPath(localSavePath));
                    StatusLabel.Text = strBarCode;
                }
            }
            else
            {
                str = "Please upload the bar code Image.";
            }
        }
        private String ReadBarcodeFromFile(string _Filepath)
        {
            String[] barcodes = BarcodeScanner.Scan(_Filepath, BarcodeType.Code39);
            return barcodes[0];
        }
    }
}