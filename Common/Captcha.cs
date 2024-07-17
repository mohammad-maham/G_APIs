using System.Drawing.Text;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace G_APIs.Common;

public class Captcha
{
    private readonly ISession _session;

    public Captcha(ISession session)
    {
        _session = session;
    }

    public  string Create()
    {
        var strCaptch = new Random().Next().ToString().Substring(0, 4);
        _session.Set("Captchar", strCaptch);

        var urlCaptcha = @"\Captcha\" + Guid.NewGuid().ToString().Substring(0, 4) + ".jpg";
        GetCaptcha(strCaptch).Save(Directory.GetCurrentDirectory() + @"\wwwroot\" + urlCaptcha, System.Drawing.Imaging.ImageFormat.Gif);
        return urlCaptcha;
    }

    private Bitmap GetCaptcha(string sImageText)
    {

        Bitmap bmpImage = new Bitmap(1, 1);

        int iWidth = 0;
        int iHeight = 0;

        Font MyFont = new Font("Arial", 36, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        Graphics MyGraphics = Graphics.FromImage(bmpImage);
        iWidth = Convert.ToInt32(MyGraphics.MeasureString(sImageText, MyFont).Width) + 20;
        iHeight = Convert.ToInt32(MyGraphics.MeasureString(sImageText, MyFont).Height) + 4;
        bmpImage = new Bitmap(bmpImage, new Size(iWidth, iHeight));
        MyGraphics = Graphics.FromImage(bmpImage);
        MyGraphics.Clear(Color.Beige);
        MyGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        MyGraphics.DrawString(sImageText, MyFont, new SolidBrush(Color.Brown), 10, 4);
        MyGraphics.Flush();
        return (bmpImage);
    }
}
