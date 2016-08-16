using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace TalkToLog.Bot.Filters
{
    public class CustomPNGHandler : IHttpHandler
    {
        public bool IsReusable { get { return false; } }
        protected RequestContext RequestContext { get; set; }

        public CustomPNGHandler() : base() { }

        public CustomPNGHandler(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            using (var rectangleFont = new Font("Arial", 12, FontStyle.Bold))
            using (var bitmap = new Bitmap(170, 110, PixelFormat.Format24bppRgb))
            using (var g = Graphics.FromImage(bitmap))
            {
                var metric = this.RequestContext.RouteData.Values["Metric"].ToString() as string;
                var number = this.RequestContext.RouteData.Values["Number"].ToString() as string;
                int intNum;
                if (!int.TryParse(number, out intNum))
                {
                    intNum = 0;
                }
                number = Dialogs.DialogHelper.ConvertHumanReadable(intNum);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                var backgroundColor = Color.DarkRed;
                
                g.Clear(backgroundColor);
                g.DrawString(metric + ": " + number, rectangleFont, new SolidBrush(Color.White), new PointF(18, 43));
                context.Response.ContentType = "image/png";
                bitmap.Save(context.Response.OutputStream, ImageFormat.Png);
            }
        }

    }
}