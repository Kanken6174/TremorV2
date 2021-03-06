﻿using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System;

namespace TremorV2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool anim = false;
        bool LidOpen = false;
        bool InAction = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Camera.Position = new Point3D((Camera.Position.X - e.Delta / 360D), (Camera.Position.Y - e.Delta / 360D), (Camera.Position.Z - e.Delta / 360D));
        }

        private async void RandomCard(object sender, RoutedEventArgs e)
        {
            /*Random rnd = new Random();
            int facevalue,counter=0;
            counter++;
            facevalue = rnd.Next(1, 53);
            c1.Content = counter;
            Uri Carte = new Uri("pack://application:,,,/TremorV2;component/" + facevalue + ".png", UriKind.RelativeOrAbsolute);
            BitmapImage CardFace = new BitmapImage(Carte);
            CardBack.ImageSource = CardFace;*/
            anim = !anim;
            while (anim)
            {
                await Task.Delay(10);
                if (XOR.Value != 180)
                    XOR.Value++;
                else
                    XOR.Value = -180;
            }
        }

        private async void LidToggle(object sender, RoutedEventArgs e)
        {
            if (!InAction)
            {
                InAction = !InAction;
                if (LidOpen)
                {
                    LidOpen = !LidOpen;
                    while (LidAngle.Value != 0)
                    {
                        LidAngle.Value--;
                        await Task.Delay(10);
                    }

                }
                else
                {
                    LidOpen = !LidOpen;
                    while (LidAngle.Value != 14)
                    {
                        LidAngle.Value--;
                        await Task.Delay(10);
                    }
                }
                InAction = !InAction;
            }
        }

        private void BarToggle(object sender, RoutedEventArgs e)
        {
            if (vscroll.IsVisible)
            {
                vscroll.Visibility = Visibility.Hidden;
                hscroll.Visibility = Visibility.Hidden;
                XOR.Visibility = Visibility.Hidden;
                LidAngle.Visibility = Visibility.Hidden;
            }
            else
            {
                vscroll.Visibility = Visibility.Visible;
                hscroll.Visibility = Visibility.Visible;
                XOR.Visibility = Visibility.Visible;
                LidAngle.Visibility = Visibility.Visible;
            }


        }

        private async void LidToggleXYZ(object sender, RoutedEventArgs e)
        {
            InAction = true;
            anim = false;
            if (LidOpen)
            {
                LidOpen = !LidOpen;
                while (LidAngle.Value != 0)
                {
                    LidAngle.Value--;
                    await Task.Delay(10);
                }
            }

            do
                await TransformTo(6, -28, 1, 300);
            while (InAction);

            LidOpen = !LidOpen;
            while (LidAngle.Value != 14)
            {
                LidAngle.Value++;
                await Task.Delay(10);
            }
            InAction = false;
        }

        private async Task TransformTo(double xr, double yr, double zr, int maxwait)
        {
            double x = Math.Ceiling(xr);
            double y = Math.Ceiling(yr);
            double z = Math.Ceiling(zr);

            vscroll.Value = Math.Ceiling(vscroll.Value);
            hscroll.Value = Math.Ceiling(hscroll.Value);
            XOR.Value = Math.Ceiling(XOR.Value);

            bool finishX = false, finishY = false, finishZ = false;
            int c = 0;
            InAction = true;

            while (!finishX || !finishY || !finishZ)
            {

                await Task.Delay(10);
                if (vscroll.Value != x)
                {
                    if (vscroll.Value > x)
                        vscroll.Value--;
                    else
                        vscroll.Value++;
                }
                else
                    finishX = true;

                if (hscroll.Value != y)
                {
                    if (hscroll.Value > y)
                        hscroll.Value--;
                    else
                        hscroll.Value++;
                }
                else
                    finishY = true;

                if (XOR.Value != z)
                {
                    if (XOR.Value > z)
                        XOR.Value--;
                    else
                        XOR.Value++;
                }
                else
                    finishZ = true;

                c++;

                if(c> maxwait)
                {
                    vscroll.Value = x;
                    hscroll.Value = y;
                    XOR.Value = z;
                    finishX = true;
                    finishY = true;
                    finishZ = true;
                }
            }
            InAction = false;

        }
    }
}
