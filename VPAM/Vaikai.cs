using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VPAM
{
    class Vaikai : INotifyPropertyChanged
    {
        private int zymeklioid;
        private int tevoid;
        private string pradzia="";
        private string pabaiga="";
        private List<int> vaikai = new List<int>();
        private List<int> tevai = new List<int>();

        public Vaikai(string pr, string pb)
        {
              pradzia = pr;
              pabaiga = pb;
       }

        public int ZymeklioID
        {
            get { return zymeklioid; }
            set { this.zymeklioid = value; this.NotifyPropertyChanged("ZymeklioID"); }
        }

        public string pradKodas
        {
            get { return pradzia; }
            set { this.pradzia = value; this.NotifyPropertyChanged("pradKodas"); }
        }

        public string pabKodas
        {
            get { return pabaiga; }
            set { this.pabaiga = value; this.NotifyPropertyChanged("pabKodas"); }
        }
        public int TevoID
        {
            get { return tevoid; }
            set { this.tevoid = value; this.NotifyPropertyChanged("TevoID"); }
        }

        public void VaikasAdd(int i, int j)
        {
            
            vaikai.Add(i);
            tevai.Add(j);
            this.NotifyPropertyChanged("VaikuSar"); 
        }
        public void VaikasRem(int i)
        {
            tevai.RemoveAt(vaikai.IndexOf(i)); 
            vaikai.Remove(i);
            this.NotifyPropertyChanged("VaikuSar");
        }

        public List<int> VaikuSar
        {
            get { return vaikai; }
            set { this.vaikai = value; this.NotifyPropertyChanged("VaikuSar"); }
        }
        public List<int> TevuSar
        {
            get { return tevai; }
            set { this.tevai = value; this.NotifyPropertyChanged("TevuSar"); }
        }

        public void Listas()
        {
            for (int i = 0; i < vaikai.Count; i++)
            {
                MessageBox.Show("Vaikas:" + vaikai[i].ToString());
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
