using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace testas
{
    /// <summary>
    /// Interaction logic for SalygaIF.xaml
    /// </summary>
    

    public partial class SalygaIF : UserControl
    {
        ObservableCollection<string> kintamujuLyginmoZenklai = new ObservableCollection<string>();
        ObservableCollection<string> jungciuLyginmoZenklai = new ObservableCollection<string>();
        ObservableCollection<string> jungciuGalimosReiksmes = new ObservableCollection<string>();
        ListBox listas = new ListBox();
        
        Binding BindKintamieji = new Binding();
        Binding BindJungtys = new Binding();

        public SalygaIF()
        {   
            InitializeComponent();
            this.DataContext = this;
            ComboboxParuosimas();
            jungciuReiksmiuParuosimas();
            jungciuZenkluParuosimas();
            KintamujuZenkluParuosimas();

        }
        
        //ItemsSource="{Binding ElementName=lokalusIF, Path=DataContext.ItemsSource}" DisplayMemberPath="Orientacija"
        private int kodoid;
        private int Hn = 0;
        private int Vn = 0;
        private string salygosTekstas;
        
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

        public string Tekstile
        {
            get { return (string)GetValue(TekstileProperty); }
            set { SetValue(TekstileProperty, value);
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

        public ObservableCollection<Zymeklis> records
        {
            get { return (ObservableCollection<Zymeklis>)GetValue(recordsSourceProperty); }
            set
            {
                SetValue(recordsSourceProperty, value);
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
        public static readonly DependencyProperty KintamiejiSourceProperty = DependencyProperty.Register("Kintamieji", typeof(IEnumerable), typeof(SalygaIF));
        public static readonly DependencyProperty JungtysSourceProperty = DependencyProperty.Register("Jungtys", typeof(IEnumerable), typeof(SalygaIF));
        public static readonly DependencyProperty recordsSourceProperty = DependencyProperty.Register("Zymekliai", typeof(IEnumerable), typeof(SalygaIF));
        public static readonly DependencyProperty TekstileProperty = DependencyProperty.Register("Tekstile", typeof(string), typeof(SalygaIF));



        public string Tekstas
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
       
        private void ComboboxParuosimas()
        {
            AntraCombobox.IsEnabled = false;
            TreciaCombobox.IsEnabled = false;
           // KetvirtaCombobox.IsEnabled = true;
            Prideti.IsEnabled = false;
            BindKintamieji.ElementName = "Control1Name";
            BindKintamieji.Path = new PropertyPath("Kintamieji");
            BindJungtys.ElementName = "Control1Name";
            BindJungtys.Path = new PropertyPath("Jungtys");
        //   KetvirtaCombobox.SetBinding(ComboBox.ItemsSourceProperty, BindKintamieji);
         //   KetvirtaCombobox.DisplayMemberPath = "Orientacija";
        }

        private void Tipas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            ComboBoxItem tipas = (ComboBoxItem) box.SelectedItem;
            if (tipas.Content != null) { 
            string pasirinkimas=tipas.Content.ToString();
                switch (pasirinkimas)
                {
                    case "Jungtis":

                        AntraCombobox.SetBinding(ComboBox.ItemsSourceProperty, BindKintamieji);
                        MessageBox.Show(AntraCombobox.Items.Count.ToString());
                        AntraCombobox.DisplayMemberPath = "Orientacija";
                        TreciaCombobox.IsEnabled = true;
                        AntraCombobox.ToolTip = "Pasirinkite jungtį iš sąrašo";
                        TreciaCombobox.IsEnabled = true; 
                        KetvirtaCombobox.IsEnabled = true;
                        TreciaCombobox.ItemsSource = jungciuLyginmoZenklai;
                       
                
                        break;
                    case "Kintamasis":
                        AntraCombobox.IsEnabled = false;

                        AntraCombobox.SetBinding(ComboBox.ItemsSourceProperty, BindKintamieji);
                        
                        if (AntraCombobox.Items.Count > 0)
                        {

                        AntraCombobox.SelectedItem = 0;
                        //AntraCombobox.ToolTip = AntraCombobox.SelectedValue.ToString();
                        AntraCombobox.DisplayMemberPath = "Orientacija";
                        AntraCombobox.IsEnabled = true;
                        TreciaCombobox.IsEnabled = true;
                        KetvirtaCombobox.IsEnabled = true;
                        TreciaCombobox.ItemsSource = kintamujuLyginmoZenklai;
                        }
                        else
                        {  
                            AntraCombobox.ToolTip = "Pirmiau sukurkite bent 2 kintamuosiuos";
                            MessageBox.Show("Kintamųjų neturite");
                        }

                        //   KetvirtaCombobox.SetBinding(ComboBox.ItemsSourceProperty, BindKintamieji);
                        break;
                    default:
                        break;
                }
            }

        }


        private void KetvirtaCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Prideti.IsEnabled = true;
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

        private void Prideti_Click(object sender, RoutedEventArgs e)
        {
            string code;
            code=TreciaCombobox.SelectedValue.ToString();
            MessageBox.Show(code);
            AntraCombobox.IsEnabled = false;
            TreciaCombobox.IsEnabled = false;
            KetvirtaCombobox.IsEnabled = false;
        }

        private void AntraCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           MessageBox.Show(AntraCombobox.SelectedValue.ToString());
        }
    }
}
