using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDI
{
    public partial class Form1 : Form
    {
        IFactory FigureFactory { get; set; }
        public Form1()
        {
            InitializeComponent();
            List<string> figures = new List<string>() { "Triangle", "Circle", "Rectangle" };
            comboBoxFigure.Items.AddRange(figures.ToArray());
        }
        List<Point> GetPointsForTriAngle(Point point, int width, int height)
        {
            List<Point> points = new List<Point>();
            int x1 = point.X; int y1 = point.Y;
            int x2 = x1 - width / 2; int y2 = y1 + height;
            int x3 = x1 + width / 2; int y3 = y2;
            try
            {
                Point point1 = new Point(x1, y1);
                Point point2 = new Point(x2, y2);
                Point point3 = new Point(x3, y3);
                points.Add(point1);
                points.Add(point2);
                points.Add(point3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return points;
        }
        public bool IsSelectedComboBox { get; set; }
        private void comboBoxFigure_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsSelectedComboBox = true;

            for (int i = 0; i < 3; i++)
            {
                point = new Point(i * 2 + 30, i * 2 + 30);
                points.Add(point);
            }
            var item = comboBoxFigure.SelectedItem.ToString();
            if (item == "Rectangle")
            {
                FigureFactory = new RectangleFactory();
            }
            else if (item == "Circle")
            {
                FigureFactory = new CircleFactory();
            }
            else if (item == "Triangle")
            {
                FigureFactory = new TriangleFactory();
            }
        }
        public Color FigureColor { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            var result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FigureColor = colorDialog.Color;
            }
        }
        public Point point { get; set; }
        public List<Point> points = new List<Point>();
        List<IFigure> figures = new List<IFigure>();
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

            Pen pen = new Pen(FigureColor);
            Brush brush = new SolidBrush(FigureColor);

            if (FigureFactory.GetFigure() is TriAngle tr)
            {
                tr.Color = FigureColor;
                tr.Point = e.Location;
                tr.Size = new Size(int.Parse(textBoxWidth.Text), int.Parse(textBoxHeight.Text));
                if (radioButtonFill.Checked)
                {

                    tr.IsFilled = true;
                }
                else
                {
                    tr.IsFilled = false;
                }
                figures.Add(tr);
            }
            else if (FigureFactory.GetFigure() is Rectangle rt)
            {

                rt.Color = FigureColor;
                rt.Point = e.Location;
                rt.Size = new Size(int.Parse(textBoxWidth.Text), int.Parse(textBoxHeight.Text));
                if (radioButtonFill.Checked)
                {
                    rt.IsFilled = true;

                }
                else
                {
                    rt.IsFilled = false;

                }
                figures.Add(rt);
            }
            else if (FigureFactory.GetFigure() is Circle cr)
            {
                cr.Color = FigureColor;
                cr.Point = e.Location;
                cr.Size = new Size(int.Parse(textBoxWidth.Text), int.Parse(textBoxHeight.Text));
                if (radioButtonFill.Checked)
                {
                    cr.IsFilled = true;

                }
                else
                {
                    cr.IsFilled = false;
                }
                figures.Add(cr);
            }

            this.Refresh();
        }
        int check = 0;
        public Graphics Graphics { get; set; }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            using (var a = e.Graphics)
            {
                foreach (var item in figures)
                {
                    Pen pen = new Pen(item.Color);
                    Brush brush = new SolidBrush(item.Color);

                    if (item is Circle cr)
                    {

                        if (cr.IsFilled)
                        {
                            a.FillEllipse(brush, cr.Point.X, cr.Point.Y, cr.Size.Width, cr.Size.Height);
                        }
                        else
                        {
                            a.DrawEllipse(pen, cr.Point.X, cr.Point.Y, cr.Size.Width, cr.Size.Height);

                        }
                    }
                    else if (item is TriAngle tr)
                    {
                        List<Point> points1 = new List<Point>();
                        points1 = GetPointsForTriAngle(tr.Point, tr.Size.Width, tr.Size.Height);

                        if (tr.IsFilled)
                        {
                            a.FillPolygon(brush, points1.ToArray());
                        }
                        else
                        {
                            a.DrawPolygon(pen, points1.ToArray());
                        }
                    }
                    else if (item is Rectangle rt)
                    {
                        if (rt.IsFilled)
                        {
                            a.FillRectangle(brush, rt.Point.X, rt.Point.Y, rt.Size.Width, rt.Size.Height);
                        }
                        else
                        {
                            a.DrawRectangle(pen, rt.Point.X, rt.Point.Y, rt.Size.Width, rt.Size.Height);

                        }
                    }


                }
                Graphics = a;
            }



        }

        private void textBoxY_TextChanged(object sender, EventArgs e)
        {
            IsSelectedComboBox = true;
        }

        private void textBoxY_Enter(object sender, EventArgs e)
        {
            IsSelectedComboBox = true;
        }
        int i = 0;
        private void buttonSave_Click(object sender, EventArgs e)
        {
            ++i;
            Bitmap myBitmap = new Bitmap("Climber.jpg");
            Graphics.DrawImage(myBitmap, 10, 10);
        }
    }
    interface IFigure
    {
        Point Point { get; set; }
        Size Size { get; set; }
        Color Color { get; set; }
        bool IsFilled { get; set; }

    }
    class Rectangle : IFigure
    {

        public Point Point { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool IsFilled { get; set; }
    }
    class Circle : IFigure
    {
        public Point Point { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool IsFilled { get; set; }
    }
    class TriAngle : IFigure
    {
        public Point Point { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool IsFilled { get; set; }
    }
    interface IFactory
    {
        IFigure GetFigure();
    }
    class TriangleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new TriAngle();

        }
    }
    class CircleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new Circle();
        }
    }
    class RectangleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new Rectangle();
        }
    }
}
