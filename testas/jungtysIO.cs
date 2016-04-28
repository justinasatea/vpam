using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testas { 
    public class jungtysIO : INotifyPropertyChanged
    {
        private char registras;
        private String pavadinimas;
        private int ddrx;
        private int iox;
        private int pozicija;
        

        public char Registras
        {
            get { return this.registras; }
            set
            {
                if (this.registras != value)
                {
                    this.registras = value;
                    this.NotifyPropertyChanged("Registras");
                }
            }
        }

        public String Pavadinimas
        {
            get { return this.pavadinimas; }
            set
            {
                if (this.pavadinimas != value)
                {
                    this.pavadinimas = value;
                    this.NotifyPropertyChanged("Pavadinimas");
                }
            }
        }

        public int DDRx
        {
            get { return this.ddrx; }
            set
            {
                if (this.ddrx != value)
                {
                    this.ddrx = value;
                    this.NotifyPropertyChanged("DDRx");
                }
            }
        }

        
        public int IOx
        {
            get { return this.iox; }
            set
            {
                if (this.iox != value)
                {
                    this.iox = value;
                    this.NotifyPropertyChanged("IOx");
                }
            }
        }

        public int Pozicija
        {
            get { return this.pozicija; }
            set
            {
                if (this.pozicija != value)
                {
                    this.pozicija = value;
                    this.NotifyPropertyChanged("Pozicija");
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