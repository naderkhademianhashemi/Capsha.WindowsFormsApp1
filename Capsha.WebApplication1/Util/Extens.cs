using System.Drawing;

namespace Capsha.WebApplication1.Util
{
    public static class Extens
    {
        public static (byte[], string) GetCaptchaIMG(bool noisy)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var charCount = 6;
            var chars = new int[charCount];
            for (int i = 0; i < charCount; i++)
            {
                var t = rand.Next(-1, 2);
                switch (t)
                {
                    case -1:
                        chars[i] = rand.Next(48, 57);
                        break;
                    case 0:
                        chars[i] = rand.Next(97, 122);
                        break;
                    default:
                        chars[i] = rand.Next(97, 122);
                        break;
                }

            }

            string c = string.Join("", chars.Select(ch => (char)ch));
            var captcha = string.Format("{0}", c);

            //image stream
            var mem = new MemoryStream();
            using (mem)
            {
                using (var bmp = new Bitmap(130, 30))
                using (var gfx = Graphics.FromImage(bmp))
                {
                    gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    //add noise
                    if (noisy)
                    {
                        int i, r, x, y;
                        var pen = new Pen(Color.Yellow);
                        for (i = 1; i < 10; i++)
                        {
                            pen.Color = Color.FromArgb(
                            rand.Next(0, 255),
                            rand.Next(0, 255),
                            rand.Next(0, 255));

                            r = rand.Next(0, (130 / 3));
                            x = rand.Next(0, 130);
                            y = rand.Next(0, 30);

                            gfx.DrawEllipse(pen, x - r, y - r, r, r);
                        }
                    }

                    //add question
                    gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                    //render as Jpeg
                    bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);

                }

                return (mem.GetBuffer(), captcha);
            }

        }
    }
}
