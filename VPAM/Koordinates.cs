using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPAM
{
    class Koordinates : INotifyPropertyChanged
    {
        private int obj;
        private int x;
        private int y;
        private string tipas;
        private string vieta;
        private int id;

        public string Vieta
        {
            get { return this.vieta; }
            set
            {
                if (this.vieta != value)
                {
                    this.vieta = value;
                    this.NotifyPropertyChanged("Vieta");
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

        public int ManoID
        {
            get { return this.id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.NotifyPropertyChanged("ManoID");
                }
            }
        }

        public int ObjektoID
        {
            get { return this.obj; }
            set
            {
                if (this.obj != value)
                {
                    this.obj = value;
                    this.NotifyPropertyChanged("Objektas");
                }
            }
        }

        public int Y
        {
            get { return this.y; }
            set
            {
                if (this.y != value)
                {
                    this.y = value;
                    this.NotifyPropertyChanged("Y");
                }
            }
        }

        public int X
        {
            get { return this.x; }
            set
            {
                if (this.x != value)
                {
                    this.x = value;
                    this.NotifyPropertyChanged("X");
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
