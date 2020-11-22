using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;

namespace TremorV2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Camera.Position = new Point3D((Camera.Position.X - e.Delta / 360D), (Camera.Position.Y - e.Delta / 360D), (Camera.Position.Z - e.Delta / 360D));
        }

        private void RandomCard(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            int facevalue,counter=0;
            counter++;
            facevalue = rnd.Next(1, 53);
            c1.Content = counter;
            Uri Carte = new Uri("pack://application:,,,/TremorV2;component/" + facevalue + ".png", UriKind.RelativeOrAbsolute);
            BitmapImage CardFace = new BitmapImage(Carte);
            CardBack.ImageSource = CardFace;
        }    
    }
}
