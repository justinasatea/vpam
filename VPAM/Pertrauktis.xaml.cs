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
    public delegate void pertrauktisEventHandler(object sender, EventArgumentai e);
    public partial class Pertrauktis : UserControl
    {
        public Pertrauktis()
        {
            InitializeComponent();
            this.DataContext = this;
            Bindruosimas();
            etikete.ToolTip ="Pasirinkite jungtį pertraukčiai";
        }
       

        private string isr = "//**Nieko nera//";
        private string paruosimasPCICR = "";
        private string paruosimasPCINT = "";
        private int objektoID;
        Binding BindJungtys = new Binding();
        public static readonly DependencyProperty JungtysSourceProperty = DependencyProperty.Register("Jungtys", typeof(IEnumerable), typeof(Pertrauktis));
        public event pertrauktisEventHandler Changed;
        public IEnumerable Jungtys
        {
            get { return (IEnumerable)GetValue(JungtysSourceProperty); }
            set
            {
                SetValue(JungtysSourceProperty, value);
            }
        }

        private void Bindruosimas()
        {
            BindJungtys.ElementName = "interrupt";
            BindJungtys.Path = new PropertyPath("Jungtys");
            comboBox.SetBinding(ComboBox.ItemsSourceProperty, BindJungtys);
        }

        protected virtual void OnChanged(EventArgumentai e)
        {
            if (Changed != null)
            {
                Changed(this, e);
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
        public string FunkcijaISR
        {
            get { return this.isr; }
            set
            {
                if (this.isr != value)
                {
                    this.isr = value;
                }
            }
        }

        public string ParuosimasPCICR
        {

            get { return this.paruosimasPCICR; }
            set
            {
                if (this.paruosimasPCICR != value)
                {
                    this.paruosimasPCICR = value;
                }
            }
        }
        public string ParuosimasPCINT
        {

            get { return this.paruosimasPCINT; }
            set
            {
                if (this.paruosimasPCINT != value)
                {
                    this.paruosimasPCINT = value;
                }
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            prideti.IsEnabled = true;
        }

        private void prideti_Click(object sender, RoutedEventArgs e)
        {
             if (comboBox.SelectedItem != null)
            {
                jungtysIO jungtis = comboBox.SelectedItem as jungtysIO;
                int i = 0;
                if (jungtis.Registras.ToString() == "D")
                {
                    paruosimasPCICR = "2";
                    i = 16 + jungtis.Pozicija;
                    paruosimasPCINT = i.ToString();
                    
                }
                if (jungtis.Registras.ToString() == "B")
                {
                    paruosimasPCICR = "0";
                    paruosimasPCINT = jungtis.Pozicija.ToString();
                }

                if (ON.IsChecked == true)
                {
                    isr = "if(bit_is_set(PIN";
                }
                else
                {
                    isr = "if(bit_is_clear(PIN";
                }
                isr += jungtis.Registras.ToString() + ", P" + jungtis.Registras.ToString() + jungtis.Pozicija.ToString() + ")){ \n";
                etikete.Content = "Papildyta";
                etikete.ToolTip = (isr);
                OnChanged(new EventArgumentai() { tekstasInfo = jungtis.Pavadinimas });
            }
        }
    }
}
