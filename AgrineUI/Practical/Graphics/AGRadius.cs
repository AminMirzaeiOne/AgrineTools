using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrineUI.Practical.Graphics
{
    public static class AGRadius
    {
        public static GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
            GraphPath.CloseFigure();
            return GraphPath;
        }

        public static GraphicsPath GetRoundPath(RectangleF rect, float radiusTopLeft, float radiusTopRight, float radiusBottomRight, float radiusBottomLeft)
        {
            GraphicsPath path = new GraphicsPath();


            if (radiusTopLeft > 0)
                path.AddArc(rect.X, rect.Y, radiusTopLeft * 2, radiusTopLeft * 2, 180, 90);
            else
                path.AddLine(rect.X, rect.Y, rect.X, rect.Y);


            path.AddLine(rect.X + radiusTopLeft, rect.Y, rect.Right - radiusTopRight, rect.Y);


            if (radiusTopRight > 0)
                path.AddArc(rect.Right - radiusTopRight * 2, rect.Y, radiusTopRight * 2, radiusTopRight * 2, 270, 90);
            else
                path.AddLine(rect.Right, rect.Y, rect.Right, rect.Y);


            path.AddLine(rect.Right, rect.Y + radiusTopRight, rect.Right, rect.Bottom - radiusBottomRight);


            if (radiusBottomRight > 0)
                path.AddArc(rect.Right - radiusBottomRight * 2, rect.Bottom - radiusBottomRight * 2,
                            radiusBottomRight * 2, radiusBottomRight * 2, 0, 90);
            else
                path.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Bottom);


            path.AddLine(rect.Right - radiusBottomRight, rect.Bottom, rect.X + radiusBottomLeft, rect.Bottom);


            if (radiusBottomLeft > 0)
                path.AddArc(rect.X, rect.Bottom - radiusBottomLeft * 2,
                            radiusBottomLeft * 2, radiusBottomLeft * 2, 90, 90);
            else
                path.AddLine(rect.X, rect.Bottom, rect.X, rect.Bottom);


            path.AddLine(rect.X, rect.Bottom - radiusBottomLeft, rect.X, rect.Y + radiusTopLeft);

            path.CloseFigure();
            return path;
        }

    }
}
