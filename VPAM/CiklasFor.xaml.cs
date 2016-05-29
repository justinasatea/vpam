using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for CiklasFor.xaml
    /// </summary>
    public partial class CiklasFor : UserControl
    {
        private int objektoID;
        private string kintamasisCiklo;
        string kartojimukiekis;
        private bool butonasOn = true;

        public CiklasFor(string kint)
        {
            InitializeComponent();
            Ikelti.IsEnabled = false;
            kartojimukiekis ="for(" + kint + "=0;"+ kint + "<0;"+ kint + "++){\n";
            kintamasisCiklo = kint;
            n.ToolTip = "Įrašykite sveikąjį skaičių \n\n Mažiausias galimas: 0 \n Didžiausias galimas: 65535";
        }

        

        
        public string kartojimuKiekisKodas
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

        public string Kintamasis
        {

            get { return this.kintamasisCiklo; }
            set
            {
                if (this.kintamasisCiklo != value)
                {
                    this.kintamasisCiklo = value;
                }
            }
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


        private void Ikelti_Click(object sender, RoutedEventArgs e)
        {
            butonasOn = false;
            Ikelti.IsEnabled = butonasOn;
            int j;
            bool result = Int32.TryParse(n.Text, out j);
            kartojimuKiekisKodas = "for(" + kintamasisCiklo + "=0;" + kintamasisCiklo + "<" + j.ToString() + ";" + kintamasisCiklo + "++){\n";
            etikete.Content = j.ToString();

        }

        private void n_TextChanged(object sender, TextChangedEventArgs e)
        {
            int j;
            bool result = Int32.TryParse(n.Text, out j);
            if ((result==true)&&(Ikelti!=null)&&(j>=0)&&(j<=65535))
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
