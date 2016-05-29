using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPAM
{
    public class Uzraktas: INotifyPropertyChanged
    {

        private string pin;
        private string uzraktoTipas;
        private string uzraktoid;

        public string PinPavadinimas
        {
            get { return this.pin; }
            set
            {
                if (this.pin != value)
                {
                    this.pin = value;
                    this.NotifyPropertyChanged("PinPavadinimas");
                }
            }
        }

        public string UzraktoTipas
        {
            get { return this.uzraktoTipas; }
            set
            {
                if (this.uzraktoTipas != value)
                {
                    this.uzraktoTipas = value;
                    this.NotifyPropertyChanged("UzraktoTipas");
                }
            }
        }
        
        public string UzraktoID
        {
            get { return this.uzraktoid; }
            set
            {
                if (this.uzraktoid != value)
                {
                    this.uzraktoid = value;
                    this.NotifyPropertyChanged("UzraktoID");
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

