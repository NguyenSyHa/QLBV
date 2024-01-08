using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace QLBV.Class
{
    internal class Trans
    {
        public static void MakeTransparent(Control control, Graphics g)
        {
            Control parent = control.Parent;
            if(parent !=null)
            {
                Rectangle rectangle = control.Bounds;
                Control.ControlCollection controls = parent.Controls;
                int index = controls.IndexOf(control);
                Bitmap bitmap = null;
                for(int i = controls.Count - 1; i > index; i --)
                {
                    Control control1 = controls[i];
                    if(control1.Bounds.IntersectsWith(rectangle))
                    {
                        if(bitmap == null)
                        {
                            bitmap = new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
                        }
                        control1.DrawToBitmap(bitmap, control1.Bounds);
                    }    
                }  
                if(bitmap != null)
                {
                    g.DrawImage((Image)bitmap, control.ClientRectangle, rectangle, (GraphicsUnit)GraphicsUnit.Pixel);
                    bitmap.Dispose();
                }    
            }    
        }
    }
    internal static class ExtenssionMethods
    {
        public static Color FromHex(this string hex) => ColorTranslator.FromHtml(hex);
    }
}
