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
using System.Text.RegularExpressions;
using iTextSharp;
using iTextSharp.text;
using Microsoft.Win32;
using System.IO;
using iTextSharp.text.pdf;
using System.Printing;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Windows.Ink;

namespace paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string windowsname { get; set; } = "Bez tytułu - Paint";
        private int diameter = (int)Sizes.small;
        private Brush brushcolor = Brushes.Black;
        private bool ispaint = false;
        private bool israse = false;
        private bool text = false;
        public List<Stroke> old= new List<Stroke>();
        public int oldata=0;
        
        private enum Sizes
        {
            small =1,
            medium = 4,
            large = 8,
        }
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;



        }
        public void Update(string filename)
        {
            MessageBox.Show(filename + "_ " + windowsname);
            windowsname = filename;
            MessageBox.Show(filename + "_ " + windowsname);
            
            this.DataContext = this;


        }

       

     
     
         void paintcirclelite( Point position)
        {
            TextBox tekscior = new TextBox();
            Thickness myThickness = new Thickness();
            myThickness.Top = position.Y;
            myThickness.Left= position.X;
            Ellipse newellipse = new Ellipse();
            tekscior.Text = "";
            tekscior.Margin= myThickness;
            inkstroke.Children.Add(tekscior );
            tekscior.Focus();


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            brushcolor = Brushes.Red;
            paintcanvas.Color = (Color)ColorConverter.ConvertFromString("red");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            brushcolor = Brushes.Blue;
            paintcanvas.Color = (Color)ColorConverter.ConvertFromString("blue");

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            brushcolor = Brushes.Black;
            paintcanvas.Color = (Color)ColorConverter.ConvertFromString("black");

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            diameter = (int)Sizes.small;
            paintcanvas.Width= diameter;
        }
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            diameter = (int)Sizes.medium;
            paintcanvas.Width = diameter;

        }
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            diameter = (int)Sizes.large;
            paintcanvas.Width = diameter;

        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            int count = inkstroke.Strokes.Count;
            if (count > 0)
            {
                Stroke ty = inkstroke.Strokes[count-1];
                    old.Add(ty);

                
                    oldata++;
                    old[oldata-1] = inkstroke.Strokes[count - 1];

                inkstroke.Strokes.RemoveAt(count - 1);
                
            }

        }
        


        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            brushcolor = Brushes.White;
            this.inkstroke.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            text = true;
            //do dokończenia
        }

        private void paintcanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (text)
            {
                Point mouseposition = e.GetPosition(inkstroke);
                paintcirclelite(mouseposition);
                text = false;
            }
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog1 = new OpenFileDialog();
            openfiledialog1.Title = "Load Image";
            openfiledialog1.DefaultExt = "savedimage";
            openfiledialog1.Filter = "Image (.jpg)|.jpg"; 
            openfiledialog1.ShowDialog();

            if (openfiledialog1.FileName == "") return;

            FileStream fs = File.Open(openfiledialog1.FileName, FileMode.Open);
            StrokeCollection mystrokes = new StrokeCollection(fs);
            inkstroke.Strokes = mystrokes;
            windowsname = openfiledialog1.FileName;
            Update(windowsname);
            fs.Close();
        }

    

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            int count = inkstroke.Strokes.Count;
            if (count > 0 && oldata>0)
            {
                inkstroke.Strokes.Add(old[oldata-1]);
                oldata--;
            }
        }

        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            SaveFileDialog openfiledialog1 = new SaveFileDialog();
            openfiledialog1.Title = "Save Image";
            openfiledialog1.DefaultExt = "savedimage";
            openfiledialog1.Filter = "Image (.jpg)|.jpg"; // Filter files by extension
            openfiledialog1.ShowDialog();

            if (openfiledialog1.FileName == "") return;

            FileStream fs = File.Open(openfiledialog1.FileName, FileMode.OpenOrCreate);
            inkstroke.Strokes.Save(fs);
              windowsname = openfiledialog1.FileName;
            Update(windowsname);
        fs.Close();
        }
    }
}
  