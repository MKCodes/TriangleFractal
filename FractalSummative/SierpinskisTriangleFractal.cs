using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalSummative
{
    public partial class SierpinskisTriangleFractal : Form
    {
        private static Graphics fractalDrawingSurface;
        private Bitmap fractalBitmap = new Bitmap(1000, 1000);
        private int pointCount = 0;
        Point Q;
        Point R;
        Point S;
        int numberOfRepititions;
        public SierpinskisTriangleFractal()
        {
            InitializeComponent();
            fractalDrawingSurface = Graphics.FromImage(fractalBitmap);
            FractalPictureBox.Visible = false;
        }

        Pen blackPen = new Pen(Color.Black);

        //This method checks for input errors. For instance, it checks if there is any spaces between the input or if the input is not a float.
        private Boolean validateInput()
        {
            int result = 0;
            float secondResult = 0;
            if (Int32.TryParse(NumOfLevelsTextBox.Text.Trim(), out result) && float.TryParse(BottomLeftVertexXTextBox.Text, out secondResult) && float.TryParse(BottomRightVertexXTextBox.Text, out secondResult)
                && float.TryParse(BottomLeftVertexYTextBox.Text, out secondResult) && float.TryParse(BottomRightVertexYTextBox.Text, out secondResult) && float.TryParse(TopVertexXTextBox.Text, out secondResult) &&
                float.TryParse(TopVertexXTextBox.Text, out secondResult))
            {
                return true;
            }
            return false;
        }
        private void DrawButton_Click(object sender, EventArgs e)
        {

            ClearImage();
            FractalPictureBox.Visible = false;

            if (validateInput())
            {
                    Canvas.Refresh();
            }
            else
            {
                MessageBox.Show("Please input valid values!");
            }
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
             Q = new Point(Int32.Parse(TopVertexXTextBox.Text), Int32.Parse(TopVertexYTextBox.Text)) ;
             R = new Point(Int32.Parse(BottomLeftVertexXTextBox.Text), Int32.Parse(BottomLeftVertexYTextBox.Text));
             S = new Point(Int32.Parse(BottomRightVertexXTextBox.Text), Int32.Parse(BottomRightVertexYTextBox.Text));
             numberOfRepititions = int.Parse(NumOfLevelsTextBox.Text);
            Triangle.drawSierpinskisTriangle(e.Graphics, blackPen, Q, R, S, numberOfRepititions);
            e.Graphics.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FractalPictureBox.Visible = true;
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
        }

        //This method allows the user to click points on the picturebox they want to use as vertices (which will be used for drawing them later on)
        private void FractalPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            numberOfRepititions = int.Parse(NumOfLevelsTextBox.Text);
           
            //used so that each point is a different colour
            Random generator = new Random();

            if (pointCount == 0)
            {
                //first point
                ClearImage();
                fractalDrawingSurface.FillEllipse(new SolidBrush(Color.FromArgb(generator.Next(255), generator.Next(255), generator.Next(255))), e.X, e.Y, 5, 5);
                Q.X = e.X;
                Q.Y = e.Y;
                pointCount++;
               
            }
            else if (pointCount == 1)
            {
                //second point
                fractalDrawingSurface.FillEllipse(new SolidBrush(Color.FromArgb(generator.Next(255), generator.Next(255), generator.Next(255))), e.X, e.Y, 5, 5);
                R.X = e.X;
                R.Y = e.Y;
                pointCount++;
            }
            else 
            {
                //third point
                fractalDrawingSurface.FillEllipse(new SolidBrush(Color.FromArgb(generator.Next(255), generator.Next(255), generator.Next(255))), e.X, e.Y, 5, 5);
                S.Y = e.Y;
                S.X = e.X;
                pointCount++; 
            }
            FractalPictureBox.Refresh();
        }

        //This method clears points on the picturebox
        private static void ClearImage()
        {
            fractalDrawingSurface.Clear(Color.White);
        }

        //This method is used draw the points and it drwas the triangle when pointCount is equal to three
        private void FractalPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(fractalBitmap, 0, 0);

            //This checks if there are 3 points picked on the picture box and draws the triangle if there is
            if (pointCount == 3)
            {
                //draws triangle recursively
                Triangle.drawSierpinskisTriangle(e.Graphics, blackPen, Q, R, S, numberOfRepititions);
                //resets point counter
                pointCount = 0;
            }
        }

        private void closeAppButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
