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
            List<string> figures = new List<string>() { "TriAngle", "Circle", "Rectangle" };
            comboBoxFigure.Items.AddRange(figures.ToArray());


        }
        public bool IsSelectedComboBox { get; set; }
        private void comboBoxFigure_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsSelectedComboBox = true;

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
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            point = new Point(e.Location.X, e.Location.Y);
            this.Refresh();
        }
        int check = 0;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (comboBoxFigure.SelectedItem != null && IsSelectedComboBox)
            {
                Pen pen = new Pen(FigureColor);
                Brush brush = new SolidBrush(FigureColor);
                using (var a = e.Graphics)
                {
                    if (FigureFactory.GetFigure() is TriAngle tr)
                    {
                        
                    }
                    else if (FigureFactory.GetFigure() is Rectangle rt)
                    {


                        if (radioButtonFill.Checked)
                        {
                            a.FillRectangle(brush, point.X, point.Y, int.Parse(textBoxWidth.Text)
                                                       , int.Parse(textBoxHeight.Text));
                        }
                        else
                        {
                            a.DrawRectangle(pen, point.X, point.Y, int.Parse(textBoxWidth.Text)
                                                       , int.Parse(textBoxHeight.Text));
                        }

                    }
                    else if (FigureFactory.GetFigure() is Circle cr)
                    {

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
