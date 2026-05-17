using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.Win32;
using YukkuriMovieMaker.Project;
using YukkuriMovieMaker.Project.Items;
using Brushes = System.Windows.Media.Brushes;
using Rectangle = System.Windows.Shapes.Rectangle;
using Size = System.Drawing.Size;

namespace YMM4_Snipping_Tool {
    /// <summary>
    /// ScreenshotWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ScreenshotWindow : Window {

        public static MainWindow mainWindow_;

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

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

        public void RunShiftDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.LeftShift) {
                MainCanvas.Children.Remove(rectangle);
                rectangle = new Rectangle { };
                IsFirstClicked = false;
            }
            else if (e.Key == Key.Tab) {
                using (Bitmap map = new Bitmap((int) rectangle.Width, (int) rectangle.Height)) {
                    using (Graphics graphics = Graphics.FromImage(map)) {
                        var startPoint = new System.Drawing.Point((int) Canvas.GetLeft(rectangle), (int) Canvas.GetTop(rectangle));
                        var imageSize = new Size((int) rectangle.Width, (int) rectangle.Height);
                        graphics.CopyFromScreen(startPoint, startPoint, imageSize);
                        IntPtr hBitMap = map.GetHbitmap();
                        var image = Imaging.CreateBitmapSourceFromHBitmap(
                            hBitMap,
                            IntPtr.Zero,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions()
                            );                  
                        mainWindow_.ViewImage.Source = image;
                        map.Save("temp.png");

                        var info = MainViewModel.info_;
                        ImageItem imageItem = new ImageItem("temp.png");
                        imageItem.Length = 100;
                        ImageItem[] imageItems = [imageItem];
                        info.Timeline.TryAddItems(imageItems, info.Timeline.CurrentFrame, 0);
                        
                        this.Close();
                        ButtonCommand.ScreenshotWindow = null;
                        DeleteObject(hBitMap);
                    }
                }
            }
        }
    }


}
