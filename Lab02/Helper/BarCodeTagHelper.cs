using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ZXing.QrCode;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Web;

namespace Lab02.Helper
{
    [HtmlTargetElement("barcode")]
    public class BarCodeTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //base.Process(context, output);
            var content = context.AllAttributes["content"].Value.ToString();
            content = HttpUtility.HtmlDecode(content);
            int height = int.Parse(context.AllAttributes["height"].Value.ToString());
            int width = int.Parse(context.AllAttributes["width"].Value.ToString());
            var barcode = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.CODE_128,
                Options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Height = height,
                    Width = width,
                    Margin = 1,
                }
            };
            var pixelData = barcode.Write(content);
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppArgb))
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    var bitmapdata = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                    try
                    {
                        Marshal.Copy(pixelData.Pixels, 0, bitmapdata.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapdata);
                    }
                    bitmap.Save(memory, ImageFormat.Png);
                    output.TagName = "img";
                    output.Attributes.Clear();
                    output.Attributes.Add("width", width);
                    output.Attributes.Add("height", height);
                    output.Attributes.Add("src", string.Format("data:images/png;base64,{0}",
                        Convert.ToBase64String(memory.ToArray())));
                }
            }
        }
    }
}
