using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBarcode
{
    public partial class TestOtherBarcodeFont : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void button6_Click(object sender, EventArgs e)
        {
            string barCode = txtBarcode.Text;
            System.Drawing.Image image;
            int width = 148, height = 55;
            string fileSavePath = AppDomain.CurrentDomain.BaseDirectory + "BarcodePattern.jpg";
            if (File.Exists(fileSavePath))
                File.Delete(fileSavePath);
            GetBarcode(height, width, BarcodeLib.TYPE.CODE128, barCode, out image, fileSavePath);
        }
        public void GetBarcode(int height, int width, BarcodeLib.TYPE type, string code, out System.Drawing.Image image, string fileSaveUrl)
        {
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            try
            {
                image = null;
                BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                b.BackColor = System.Drawing.Color.White;//Picture background color
                b.ForeColor = System.Drawing.Color.Black;//Barcode color
                b.IncludeLabel = true;
                //b.Alignment = BarcodeLib.AlignmentPositions.LEFT;
                //b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;// code display position
                b.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;//Image Format
                System.Drawing.Font font = new System.Drawing.Font("verdana", 10f);//Font settings
                b.LabelFont = font;
                b.Height = height;//Picture height setting (px unit)
                b.Width = width;//Picture width setting (px unit)

                image = b.Encode(type, code);//Generate picture
                image.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                PlaceHolder1.Controls.Add(imgBarCode);
            }
            catch (Exception ex)
            {

                image = null;
            }
        }
    }
}