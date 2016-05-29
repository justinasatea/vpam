using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for setPORT.xaml
    /// </summary> 
    public delegate void setPORTEventHandler(object sender, EventArgumentai e);
    public delegate void undoPORTEventHandler(object sender, EventArgumentai e);
    public partial class setPORT : UserControl
    {
        Binding BindJungtysOUT = new Binding();
        ObservableCollection<string> kodasKolekcija = new ObservableCollection<string>();
        ObservableCollection<string> pavadinimuKolekcija = new ObservableCollection<string>();
        ObservableCollection<string> tooltipKolekcija = new ObservableCollection<string>();
        public static readonly DependencyProperty JungtysOUTSourceProperty = DependencyProperty.Register("JungtysOUT", typeof(IEnumerable), typeof(setPORT));
        private string kodas="\n";
        public event setPORTEventHandler Changed;
        public event undoPORTEventHandler Canceled;
        private int objektoid;
        
        public setPORT()
        {
            InitializeComponent();
            this.DataContext = this;
            Bindruosimas();
            Pradzia();
        }


        private void Bindruosimas()
        {
            BindJungtysOUT.ElementName = "SetPORT";
            BindJungtysOUT.Path = new PropertyPath("JungtysOUT");
        }

        private void Pradzia()
        {   
            JungtysCombox.SetBinding(ComboBox.ItemsSourceProperty, BindJungtysOUT);
            kodasKolekcija.Add(kodas);
            etikete.ToolTip = ("Turimas kodas: \n --Tuščia--");
        }


        public int ObjektoID
        {
            get { return this.objektoid; }
            set
            {
                if (this.objektoid != value)
                {
                    this.objektoid = value;
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
                    this.kodas =value;
                }               
            }
        }

        public IEnumerable JungtysOUT
        {
            get { return (IEnumerable)GetValue(JungtysOUTSourceProperty); }
            set
            {
                SetValue(JungtysOUTSourceProperty, value);
            }
        }

        private void Patvirtinti_Click(object sender, RoutedEventArgs e)
        {
            if (JungtysCombox.SelectedItem != null) { 
            jungtysIO jungtis = JungtysCombox.SelectedItem as jungtysIO;
            string tmp=""; //Kuriamas kodas vertyklei
            string tmp2 = ""; //Rodomas paaiškinamas naudotojui
            if (ON.IsChecked == true)
            {
                tmp = "PORT" + jungtis.Registras.ToString() + "|=" + "(1<<P" + jungtis.Registras.ToString() + jungtis.Pozicija.ToString() + ");\n";
                tmp2 = "Įjungta " + jungtis.Pavadinimas + "\n";
                }
            if (OFF.IsChecked == true)
            {
                tmp = "PORT" + jungtis.Registras.ToString() + "&=(~(1<<P" + jungtis.Registras.ToString() + jungtis.Pozicija.ToString() + "));\n";
                tmp2 = "Išjungta " + jungtis.Pavadinimas+"\n";
            }
            if (SWITCH.IsChecked == true)
            {
                tmp = "PORT" + jungtis.Registras.ToString() + "^=(1<<P" + jungtis.Registras.ToString() + jungtis.Pozicija.ToString() + ");\n";
                tmp2 = "Pakeista " + jungtis.Pavadinimas + "\n";
            }
            kodasKolekcija.Add(tmp);
            pavadinimuKolekcija.Add(jungtis.Pavadinimas);
            tooltipKolekcija.Add(tmp2);
            kodoKeitimas(); //Kuriamo kodo papildymas
            Undo.IsEnabled = true;
            etikete.Content="Papildyta";
            OnChanged(new EventArgumentai() { tekstasInfo = jungtis.Pavadinimas });
            }
            else
            {
                MessageBox.Show("Pasirinkite programuojamą jungtį prieš spaudžiant \n \"Papildyti\" programos kodą");
            }
        }

        
        private void kodoKeitimas()
        {
            kodas = "";
            etikete.ToolTip = "";
            for (int i=0; i < kodasKolekcija.Count(); i++)
            {
                kodas += kodasKolekcija[i];
            }
            for (int i = 0; i <pavadinimuKolekcija.Count(); i++)
            {
                etikete.ToolTip += tooltipKolekcija[i];
            }
            etikete.ToolTip = "Jau turimas kodas:\n" + etikete.ToolTip;
        }

        protected virtual void OnChanged(EventArgumentai e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }
        protected virtual void OnCanceled(EventArgumentai e)
        {
            if (Canceled != null)
            {
                Canceled(this, e);
            }
        }

        private void JungtysCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JungtysCombox.SelectedItem != null)
            {
            Patvirtinti.IsEnabled = true;
            jungtysIO jungtis = JungtysCombox.SelectedItem as jungtysIO;
            JungtysCombox.ToolTip = jungtis.Pavadinimas;
            }
            
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (kodasKolekcija.Count() > 1)
            {
                OnCanceled(new EventArgumentai() { tekstasInfo = pavadinimuKolekcija[pavadinimuKolekcija.Count()-1]});
                kodasKolekcija.RemoveAt(kodasKolekcija.Count()-1);
                pavadinimuKolekcija.RemoveAt(pavadinimuKolekcija.Count() - 1);
                tooltipKolekcija.RemoveAt(tooltipKolekcija.Count() - 1);
                kodoKeitimas();
                etikete.Content = "Atšaukta";
            }
            if (kodasKolekcija.Count() <= 1)
            {
                Undo.IsEnabled = false;
            }
        }
    }
}
