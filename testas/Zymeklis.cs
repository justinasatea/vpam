using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace testas
{
    public class Zymeklis : Button
    {
        Image newImage = new Image();
        TextBlock _textBlock = null;
        private int x;
        private int y;
        private int atskaitosId=0;
        private string orientacija = "V";
        private bool leidziaIfus = false;
        private bool efektas = true;
        private BitmapImage Bitmapas;

        public Zymeklis()
        {
       //     BitmapImage Bitmapas = new BitmapImage(new Uri("Icons/Pliusas.png", UriKind.Relative));
            keistiImage();
            StackPanel panel = new StackPanel();
                panel.Children.Add(newImage);

            _textBlock = new TextBlock();

            this.Content = panel;
           
        }
        public void keistiImage()
        {
            if (orientacija == "H")
            {
                if (efektas)
                {
                    newImage.Source = new BitmapImage(new Uri("Icons/Pliusas2.png", UriKind.Relative));

                }
                else
                {
                    newImage.Source = new BitmapImage(new Uri("Icons/Pliusas.png", UriKind.Relative));
                }
            }
            else
            {
                if (efektas)
                {
                    newImage.Source = new BitmapImage(new Uri("Icons/Pliusas2V.png", UriKind.Relative));

                }
                else
                {
                    newImage.Source = new BitmapImage(new Uri("Icons/PliusasV.png", UriKind.Relative));
                }
            }
            efektas = !efektas;
        }

        public string Orientacija
        {
            get
            {
                return orientacija;
            }
            set
            {
                if (this.orientacija != value)
                {
                    this.orientacija = value;
         //this.NotifyPropertyChanged("Orientacija");
                }

            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                if (this.y != value)
                {
                     this.y = value;
                }
               
            }
        }

        public bool galimaIF
        {
            get
            {
                return leidziaIfus;
            }
            set
            {
                if (this.leidziaIfus != value)
                {
                    this.leidziaIfus = value;
                }

            }
        }

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                if (this.x != value)
                {
                    this.x = value;
                }

            }
        }

        public int atskaitosID
        {
            get
            {
                return atskaitosId;
            }
            set
            {
                if (this.atskaitosId != value)
                {
                    this.atskaitosId = value;
                }

            }
        }

   //     public event PropertyChangedEventHandler PropertyChanged;

   //     public void NotifyPropertyChanged(string propName)
    // //   {
     //       if (this.PropertyChanged != null)
     //           this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
    //    }
        // Properties
    }
    
}
