using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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
   
 public delegate void IFEventHandler(object sender, EventArgumentai e);
    public partial class SalygaIF : UserControl

    {
        ObservableCollection<string> kintamujuLyginmoZenklai = new ObservableCollection<string>();
        ObservableCollection<string> jungciuLyginmoZenklai = new ObservableCollection<string>();
        ObservableCollection<string> jungciuGalimosReiksmes = new ObservableCollection<string>();
        

        public event IFEventHandler Changed;

        Binding BindKintamieji = new Binding();
        Binding BindJungtys = new Binding();
        private int objektoID;
        public SalygaIF()
        {   
            InitializeComponent();
            this.DataContext = this;
            ComboboxParuosimas();
            BindParuosimas();
            jungciuReiksmiuParuosimas();
            jungciuZenkluParuosimas();
            KintamujuZenkluParuosimas();
            Pradzia();
            salyga_button.ToolTip = "Pridėti sąlygą";
        }
        
        private int kodoid;
        private int Hn = 0;
        private int Vn = 0;
        private string salygosTekstas = "if(1){ \n";
        
        public int HarrayID
        {
           
            get { return this.Hn; }
            set
            {
                if (this.Hn != value)
                {
                    this.Hn = value;
                }
            }
        }

        public int VarrayID
        {
            get { return this.Vn; }
            set
            {
                if (this.Vn != value)
                {
                    this.Vn = value;
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

        public IEnumerable Kintamieji
        {
            get { return (IEnumerable)GetValue(KintamiejiSourceProperty); }
            set
            {
                SetValue(KintamiejiSourceProperty, value);
            }
        }
               
        public IEnumerable Jungtys
        {
            get { return (IEnumerable)GetValue(JungtysSourceProperty); }
            set
            {
                SetValue(JungtysSourceProperty, value);
            }
        }
        public static readonly DependencyProperty KintamiejiSourceProperty = DependencyProperty.RegisterAttached("Kintamieji", typeof(IEnumerable), typeof(SalygaIF));
        public static readonly DependencyProperty JungtysSourceProperty = DependencyProperty.Register("Jungtys", typeof(IEnumerable), typeof(SalygaIF));


        public string Kodas
        {
            get { return this.salygosTekstas; }
            set
            {
                if (this.salygosTekstas != value)
                {
                    this.salygosTekstas = value;
                    
                }
            }
        }

        public int kodoId
        {
            get { return this.kodoid; }
            set
            {
                if (this.kodoid != value)
                {
                    this.kodoid = value;
                  
                }
            }
        }

        protected virtual void OnChanged(EventArgumentai e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }


        private void BindParuosimas()
        {
            BindKintamieji.ElementName = "Control1Name";
            BindKintamieji.Path = new PropertyPath("Kintamieji");
            BindJungtys.ElementName = "Control1Name";
            BindJungtys.Path = new PropertyPath("Jungtys");
           
        }

        private void Pradzia()
        {
             reiksmesX.SetBinding(ComboBox.ItemsSourceProperty, BindJungtys);
             reiksmesX.DisplayMemberPath = "Pavadinimas";
             zenklai.ItemsSource = jungciuLyginmoZenklai;
             reiksmesY.ItemsSource = jungciuGalimosReiksmes;
             reiksmesX.IsEnabled = true;
        }

        private void ComboboxParuosimas()
        {
            reiksmesX.IsEnabled = false;
            zenklai.IsEnabled = false;
            reiksmesY.IsEnabled = false;
            salyga_button.IsEnabled = false;
        }

        private void Checkboxai(int versija)
        {
            ComboboxParuosimas();
            int ver = versija;
            if (versija == 0)
            {

                 reiksmesX.SetBinding(ComboBox.ItemsSourceProperty, BindJungtys);
                if (reiksmesX.Items.Count > 0)
                {
                    reiksmesX.DisplayMemberPath = "Pavadinimas";
                    reiksmesX.IsEnabled = true;
                    zenklai.ItemsSource = jungciuLyginmoZenklai;
                    reiksmesY.ItemsSource = jungciuGalimosReiksmes;
                }
                else
                {
                    MessageBox.Show("Nėra jungčių, kurias būtų galima tikrinti");
                    checkBox.IsChecked = false;
                }
            }
            else
            {
                reiksmesX.SetBinding(ComboBox.ItemsSourceProperty, BindKintamieji);
                if(reiksmesX.Items.Count > 0)
                {
                    reiksmesX.DisplayMemberPath = "mPavadinimas";
                    reiksmesX.IsEnabled = true;
                    zenklai.ItemsSource = kintamujuLyginmoZenklai;
                    reiksmesY.SetBinding(ComboBox.ItemsSourceProperty, BindKintamieji);
                    reiksmesY.DisplayMemberPath = "mPavadinimas";
                }
                else
                {
                  MessageBox.Show("Savo sukurtų kintamųjų šiuo metu neturite");
                  checkBox.IsChecked = false;
                }
            }
        }


        private void KintamujuZenkluParuosimas()
        {
            kintamujuLyginmoZenklai.Add("==");
            kintamujuLyginmoZenklai.Add("!=");
            kintamujuLyginmoZenklai.Add("<=");
            kintamujuLyginmoZenklai.Add(">=");
            kintamujuLyginmoZenklai.Add("<");
            kintamujuLyginmoZenklai.Add(">");
        }
        private void jungciuZenkluParuosimas()
        {
            jungciuLyginmoZenklai.Add("==");
            jungciuLyginmoZenklai.Add("!=");
        }
        private void jungciuReiksmiuParuosimas()
        {
            jungciuGalimosReiksmes.Add("0");
            jungciuGalimosReiksmes.Add("1");
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            ComboboxParuosimas();
            if (checkBox.IsChecked == true){
                Checkboxai(1); 
            }
            else
            {
                Checkboxai(0); 

            }
        }

        private void salyga_button_Click(object sender, RoutedEventArgs e)
        {
            string operacija, skl, skl2, tmp;
            if (reiksmesX.SelectedItem != null)
            {
                if (reiksmesX.SelectedItem.GetType().ToString() == "VPAM.jungtysIO")
                {
                    jungtysIO jungtis = reiksmesX.SelectedItem as jungtysIO;
                    tmp = "P" + jungtis.Registras.ToString() + jungtis.Pozicija.ToString();
                    if (reiksmesY.SelectedValue.ToString() == "1")
                    {
                        operacija = "bit_is_set(";
                    }
                    else
                    {
                        operacija = "bit_is_clear(";
                    }
                    if (zenklai.SelectedValue.ToString() == "==")
                    {
                        skl = "if(";
                        skl2 = ")){\n";
                    }
                    else
                    {
                        skl = "if(!(";
                        skl2 = "))){ \n";
                    }
                    salygosTekstas = skl + operacija + "PIN" + jungtis.Registras.ToString() + "," + tmp + skl2;
                    etikete.ToolTip=salygosTekstas;
                    OnChanged(new EventArgumentai() { tekstasInfo = jungtis.Pavadinimas });
                }
                else
                {
                    string matomas;
                    string nmatomas;
                    KintKurimas kintX = reiksmesX.SelectedItem as KintKurimas;
                    KintKurimas kintY = reiksmesY.SelectedItem as KintKurimas;
                    matomas = kintX.mPavadinimas + zenklai.SelectedValue.ToString() + kintY.mPavadinimas;
                    nmatomas= kintX.mPavadinimas + zenklai.SelectedValue.ToString() + kintY.mPavadinimas;
                    etikete.ToolTip = "if("+matomas+")";
                    salygosTekstas ="if("+nmatomas+"){\n";
                }
            }
            else
            {
                MessageBox.Show("Pasirinkite tikrinamą X'są prieš \n sukuriant sąlygą");
            }
        }

        private void reiksmesX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            zenklai.IsEnabled = true;
        }

        private void reiksmesY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            salyga_button.IsEnabled = true;
        }

        private void zenklai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // MessageBox.Show(zenklai.SelectedValue.ToString());
            reiksmesY.IsEnabled = true;
        }

    }
}
