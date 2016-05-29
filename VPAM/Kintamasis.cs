using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPAM
{
    class Kintamasis : INotifyPropertyChanged
    {
        private string m_pavadinimas;
        private string n_pavadinimas;
        private string tipas;

        public String mPavadinimas
        {
            get { return this.m_pavadinimas; }
            set
            {
                if (this.m_pavadinimas != value)
                {
                    this.m_pavadinimas = value;
                    this.n_pavadinimas = "v_" + value;
                    this.NotifyPropertyChanged("mPavadinimas");
                }
            }
        }

        public String Tipas
        {
            get { return this.tipas; }
            set
            {
                if (this.tipas != value)
                {
                    this.tipas = value;
                    this.NotifyPropertyChanged("Tipas");
                }
            }
        }

        public String nPavadinimas
        {
            get { return this.n_pavadinimas; }
            set
            {
                if (this.n_pavadinimas != value)
                {
                    this.n_pavadinimas = value;
                    this.NotifyPropertyChanged("nPavadinimas");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
   
}
