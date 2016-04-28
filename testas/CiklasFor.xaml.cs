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

namespace testas
{
    /// <summary>
    /// Interaction logic for CiklasFor.xaml
    /// </summary>
    public partial class CiklasFor : UserControl
    {
        public CiklasFor()
        {
            InitializeComponent();
            Ikelti.IsEnabled = false;
        }
        private int kodoid;
        private int Hn = 0;
        private int Vn = 0;
        string kartojimukiekis;
        private bool butonasOn = true;


        public string kartojimuKiekis
        {

            get { return this.kartojimukiekis; }
            set
            {
                if (this.kartojimukiekis != value)
                {
                    this.kartojimukiekis = value;
                }
            }
        }



        private void Ikelti_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Ikelti.Content.ToString());
            Ikelti.IsEnabled = butonasOn;
            butonasOn = false;
            kartojimukiekis = n.Text;

        }

        private void n_TextChanged(object sender, TextChangedEventArgs e)
        {
            int j;
            bool result = Int32.TryParse(n.Text, out j);
            if ((result==true)&&(Ikelti!=null))
            {
                butonasOn = true;
                Ikelti.IsEnabled = butonasOn;
            }
            else
            {
              if(Ikelti != null){
                Ikelti.IsEnabled = false;
                }
               
            }

        }
    }
}
