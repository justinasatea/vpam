using System;
using System.Collections;
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
    /// Interaction logic for KintVeiksmai.xaml
    /// </summary>
    public partial class KintVeiksmai : UserControl
    {
        Binding BindKintamieji = new Binding();
        private int objektoID;
        private string kodas="";
        public static readonly DependencyProperty KintamiejiSourceProperty = DependencyProperty.Register("Kintamieji", typeof(IEnumerable), typeof(KintVeiksmai));
        public KintVeiksmai()
        {
            InitializeComponent();
            this.DataContext = this;
            Bindruosimas();
            Pradzia();
        }

        private void Bindruosimas()
        {
            BindKintamieji.ElementName = "Veiksmas";
            BindKintamieji.Path = new PropertyPath("Kintamieji");
        }

        private void Pradzia()
        {
            KintamiejiCombox.SetBinding(ComboBox.ItemsSourceProperty, BindKintamieji);
            KintamiejiCombox.DisplayMemberPath = "mPavadinimas";
            etikete.ToolTip = ("Patvirtinkite veiksmą kintamajam");
        }

        public int ObjektoID
        {
            get { return this.objektoID; }
            set
            {
                if (this.objektoID != value)
                {
                    this.objektoID = value;
                }
            }
        }
        public String Kodas
        {
            get { return this.kodas; }
            set
            {
                if (this.kodas != value)
                {
                    this.kodas = value;
                }
            }
        }
        public IEnumerable Kintamieji
        {
            get { return (IEnumerable)GetValue(KintamiejiSourceProperty); }
            set
            {
                SetValue(KintamiejiSourceProperty, value);
            }
        }

        private void Patvirtinti_Click(object sender, RoutedEventArgs e)
        {
            if (KintamiejiCombox.SelectedItem != null)
            {
                KintKurimas kintamasis = KintamiejiCombox.SelectedItem as KintKurimas;
                if (Plius.IsChecked == true)
                {
                    kodas = kintamasis.nPavadinimas + "++; \n";
                    etikete.ToolTip = kintamasis.nPavadinimas + " +1";
                }
                if(Minus.IsChecked == true)
                {
                    kodas = kintamasis.nPavadinimas+" -1";
                }
                etikete.ToolTip = kodas;
                etikete.Content = "Pasirinkta";
            }
            else
            {
                MessageBox.Show("Pasirinkite programuojamą kinatamąjį prieš patvirtinant");
            }
        }


    }
}
