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
    /// Interaction logic for KintKurimas.xaml
    /// </summary>
    public partial class KintKurimas : UserControl
    {

        private string m_pavadinimas;
        private string n_pavadinimas;
        private string nekint_pav;
        private int verte;
        private int objektoID;
        public KintKurimas(string kint)
        {
            InitializeComponent();
            nekint_pav = "v_" + kint;
            n_pavadinimas = nekint_pav;
            m_pavadinimas = "Naujas Kintamasis" + kint;
            info.ToolTip = "Įveskite vardą ir vertę nuo 0 iki 65535";
            etikete.ToolTip = "Kintamasis dar neturi vardo.";
        }

        public string nPavadinimas
        {

            get { return this.n_pavadinimas; }
            set
            {
                if (this.n_pavadinimas != value)
                {
                    this.n_pavadinimas = value;
                }
            }
        }

        public string mPavadinimas
        {

            get { return this.m_pavadinimas; }
            set
            {
                if (this.m_pavadinimas != value)
                {
                    this.m_pavadinimas = value;
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

        private void patvirtinti_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Patvirtinus keisti kintamojo vardo nebebus galima. \n Patvirtinti ir išsaugoti visam?", "Pranešimas", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
            etikete.Content = "Patvirtinta";
            etikete.ToolTip = m_pavadinimas + " " + verte;
                nPavadinimas = nekint_pav + m_pavadinimas;
                mvardas.IsEnabled = false;
                reiksme.IsEnabled = false;
                patvirtinti.IsEnabled = false;
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
              
            }
           
            
        }

        private void mvardas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if((mvardas.Text != null) && (reiksme != null) && (patvirtinti.Content != null)) { 
           if((mvardas.Text!= null)&&(reiksme!=null)&&(mvardas.Text.Length>1)&&(mvardas.Text.Length<10)){
                m_pavadinimas = mvardas.Text;
                n_pavadinimas = nekint_pav + m_pavadinimas;
                reiksme.IsEnabled = true;
            }
            else
            {
                reiksme.IsEnabled = false;
                patvirtinti.IsEnabled = false;
            }
            }
        }

        private void reiksme_TextChanged(object sender, TextChangedEventArgs e)
        {
            int j;
            bool result = Int32.TryParse(reiksme.Text, out j);
            if ((result == true) && (patvirtinti != null) && (j >= 0) && (j <= 65535))
            {
                verte = j;
                patvirtinti.IsEnabled = true;
            }
            else
            {
                if (patvirtinti != null)
                {
                    patvirtinti.IsEnabled = false;
                }
            }

        }
    }
}
