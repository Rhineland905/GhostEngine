using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace GhostEngine.DesktopGL.Editor.UI
{
    
    internal class WorkSpace
    {
        public static Panel Area { get; private set; }

        public static Panel CreateWorkSpace()
        {
            Area = new Panel
            {
                Background = null, 
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };


            return Area;
        }

        public static Rectangle ScreenBounds
        {
            get
            {
                if (Area == null)
                {
                    return Rectangle.Empty;
                }

                var topLeft = Area.ToGlobal(Point.Zero);
                return new Rectangle(topLeft.X, topLeft.Y, Area.ActualBounds.Width, Area.ActualBounds.Height);
            }
        }
    }
}
