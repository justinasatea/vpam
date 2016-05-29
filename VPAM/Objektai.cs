using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPAM
{
    class Objektai: INotifyPropertyChanged
    {
        private int id;
        private string tipas;


        public int ID
        {
            get { return this.id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.NotifyPropertyChanged("ID");
                }
            }
        }

        public string Tipas
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


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
