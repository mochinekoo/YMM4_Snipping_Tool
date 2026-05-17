using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace YMM4_Snipping_Tool {
    /// <summary>
    /// ScreenshotWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ScreenshotWindow : Window {

        //図形
        private Rectangle rectangle;
        private bool IsFirstClicked;
        private double startX, startY;


        public ScreenshotWindow() {
            InitializeComponent();
        }

        public void RunMouseDown(object sender, MouseButtonEventArgs e) {
            var window = ButtonCommand.ScreenshotWindow;
            var point = Mouse.GetPosition(window);
            if (!IsFirstClicked) {
                IsFirstClicked = true;
                rectangle = new Rectangle { };
                rectangle.Fill = Brushes.Gray;
                rectangle.Width = 1;
                rectangle.Height = 1;
                this.startX = point.X;
                this.startY = point.Y;
                Canvas.SetLeft(rectangle, point.X);
                Canvas.SetTop(rectangle, point.Y);
                MainCanvas.Children.Add(rectangle);
            } else {
                double x = Math.Min(point.X, startX);
                double y = Math.Min(point.Y, startY);

                double width = Math.Abs(point.X - startX);
                double height = Math.Abs(point.Y - startY);
                Canvas.SetLeft(rectangle, x);
                Canvas.SetTop(rectangle, y);
                rectangle.Width = width;
                rectangle.Height = height;
            }

        }

        public void RunMouseLeave(object sender, MouseEventArgs e) {

        }

        public void RunShiftDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.LeftShift) {
                MainCanvas.Children.Remove(rectangle);
                rectangle = new Rectangle { };
                IsFirstClicked = false;
            }
        }
    }


}
