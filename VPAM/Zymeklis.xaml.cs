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

namespace VPAM
{
    /// <summary>
    /// Interaction logic for Zymeklis.xaml
    /// </summary>
    public partial class Zymeklis : UserControl
    {
        private int x;
        private int y;
        private int Id = 0;
        private string orientacija = "V";
        private bool leidziaIfus = false;
        private bool leidziaPertr = false;
        private bool efektas = true;

        public Zymeklis(Brush spalva, string etikete)
        {
            InitializeComponent();
            elipse.Fill=spalva;
            tekstas.Content = etikete;
        }

        public void keistiImage()
        {
               x1.Fill = Brushes.Green;
               x2.Fill = Brushes.Green; 
        }

        public void AtstatytiImage()
        {
            x1.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("Yellow"));
            x2.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("Yellow"));
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

        public bool galimaPertr
        {
            get
            {
                return leidziaPertr;
            }
            set
            {
                if (this.leidziaPertr != value)
                {
                    this.leidziaPertr = value;
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

        public int zymeklioID
        {
            get
            {
                return Id;
            }
            set
            {
                if (this.Id != value)
                {
                    this.Id = value;
                }

            }
        }
    }
}
