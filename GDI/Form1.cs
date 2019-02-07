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
        public bool IsSelectedComboBox { get; set; }
        private void comboBoxFigure_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsSelectedComboBox = true;

            for (int i = 0; i < 3; i++)
            {
                point = new Point(i + 30, i + 30);
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
                tr.Size = new Size(100, 100);
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
                rt.Size = new Size(100, 100);
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
                cr.Size = new Size(100, 100);
                if (radioButtonFill.Checked)
                {
                    cr.IsFilled = true;

                }
                else
                {

                }
                figures.Add(cr);
            }

            this.Refresh();
        }
        int check = 0;
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



                }
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
