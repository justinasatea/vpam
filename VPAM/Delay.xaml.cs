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
    /// Interaction logic for Delay.xaml
    /// </summary>
    /// 

    


    public partial class Delay : UserControl
    {
     
        public Delay()
        {
            InitializeComponent();
        }

        private string saknis = "_delay_";
        private string kodas = "_delay_ms(0); \n";
        private int laikas = 0;      
        private bool butonasOn = true;
        private int objektoID;

        public string Kodas
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            if (mms.IsChecked == true)
            {
                kodas = saknis + "us(" + laikas.ToString() + "); \n";
            }
            if (ms.IsChecked == true)
            {
                kodas = saknis + "ms(" + laikas.ToString() + "); \n";
            }
            if (s.IsChecked == true)
            {
                laikas=laikas*1000;
                kodas = saknis + "ms(" + laikas.ToString() + "); \n";
            }
                       
            butonasOn = false;
            button1.IsEnabled = butonasOn;
            MessageBox.Show("Patvirtinimas užfiksuotas "+ kodas);
            labelis.Content ="NUSTATYTA";

        }


        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            int j;
            bool result = Int32.TryParse(textBox1.Text, out j);
            if ((result == true) && (button1 != null))
            {
                Int32.TryParse(textBox1.Text, out laikas);
                butonasOn = true;
                button1.IsEnabled = butonasOn;
            }
            else
            {
                if (button1 != null)
                {
                    button1.IsEnabled = false;
                }

            }
        }
    }
}
