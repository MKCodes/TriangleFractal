using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalSummative
{
    class Triangle
    {
       private static Random generator = new Random();
      
        //This method uses two points to find their midpoints
        private static Point midpointofVertices(Point firstPoint, Point secondPoint)
        {
            return new Point((firstPoint.X + secondPoint.X) / 2, (firstPoint.Y + secondPoint.Y) / 2);
        }

        //This method creates a triangle
        private static void drawTriangle(Graphics graphics, Pen pen, Point P, Point Q, Point R)
        {
            pen.Color = Color.FromArgb(generator.Next(210), generator.Next(210), generator.Next(210));
            graphics.DrawLine(pen, P, Q);
            graphics.DrawLine(pen, P, R);
            graphics.DrawLine(pen, R, Q);
       
        }
        public static void drawSierpinskisTriangle(Graphics graphics, Pen pen, Point P, Point Q, Point R, int repititions)
         {
            //Base case
            if (repititions == 0)
            {
                drawTriangle(graphics, pen, P, Q, R);
            }
            // Alters the coordinates of each triangle to draw them in three disctinct locations
            else
            {
                drawSierpinskisTriangle(graphics, pen, midpointofVertices(Q, P), midpointofVertices(Q, R), Q, repititions - 1);
                drawSierpinskisTriangle(graphics, pen, midpointofVertices(P, Q), midpointofVertices(P, R), P, repititions - 1);
                drawSierpinskisTriangle(graphics, pen, midpointofVertices(R, P), midpointofVertices(R, Q), R, repititions - 1);

            }
        }

    }
}
