using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Label> kodoKolekcija = new ObservableCollection<Label>(); //Pagrindinis programos kodas
        ObservableCollection<INotifyPropertyChanged> objektuKontrole = new ObservableCollection<INotifyPropertyChanged>(); 
        ObservableCollection<UserControl> gridoObjektai = new ObservableCollection<UserControl>(); //Nudropinti kontroleriai
        ObservableCollection<Zymeklis> zymekliuKolekcija = new ObservableCollection<Zymeklis>(); //Drop zymekliai
        ObservableCollection<jungtysIO> jungciuKolekcija = new ObservableCollection<jungtysIO>();

        public MainWindow()
        {
            InitializeComponent();
            gridoObjektai.Add(new SalygaIF() { Kintamieji = zymekliuKolekcija});

            // kodoSarasas.ItemsSource = kodoKolekcija; //Rodo Liste kodo kolekcija
            zymeklis_pirmas();
            //  kodoSarasas.ItemsSource = zymekliuKolekcija;
            Gridas.Children.Add(gridoObjektai[0]);
            Grid.SetColumn(gridoObjektai[0],0);
            Grid.SetRow(gridoObjektai[0], 3);
            
        }
/// <summary>
/// Drag įvykiai
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void Blokas_MouseMove(object sender, MouseEventArgs e)
        {
            TextBlock asss = sender as TextBlock;
            DataObject data = new DataObject();
            data.SetData("IF", "IF");
            DragDrop.DoDragDrop(asss, data, DragDropEffects.Move);
           
        }

        private void Blokas1_MouseMove(object sender, MouseEventArgs e)
        {
            TextBlock asss = sender as TextBlock;
            DataObject data = new DataObject();
            data.SetData("For", "For");
            DragDrop.DoDragDrop(asss, data, DragDropEffects.Move);
        }

        private void Blokas2_MouseMove(object sender, MouseEventArgs e)
        {
            TextBlock asss = sender as TextBlock;
            DataObject data = new DataObject();
            data.SetData("Set", "Set");
            DragDrop.DoDragDrop(asss, data, DragDropEffects.Move);
        }

        private void Blokas3_MouseMove(object sender, MouseEventArgs e)
        {
            TextBlock asss = sender as TextBlock;
            DataObject data = new DataObject();
            data.SetData("While", "While");
            DragDrop.DoDragDrop(asss, data, DragDropEffects.Move);
        }

/// <summary>
/// zymekliu įvykiai
/// </summary>
/// 
           private void zymeklis_pirmas()
        {
            zymeklio_kurimas(0, 1, 0, true, "V");
            zymeklio_kurimas(2, 2, 0, true, "H");
            //x, y, ifID, leidziaIF, char
        }
        /// 
        private void zymeklis_Drop(object sender, DragEventArgs e)
        {
            Zymeklis zymeklisLocal = sender as Zymeklis;
            int n = zymekliuKolekcija.IndexOf(zymeklisLocal);
            zymekliuKolekcija[n].keistiImage();
                    
            if (e.Data.GetDataPresent("IF"))
            {
                if (zymeklisLocal.galimaIF)
                {
                    MessageBox.Show(n.ToString());
                    zymekliuKolekcija[n].galimaIF = false;
                    
                  
                }
                else
                {
                    MessageBox.Show("Šioje programos versijoje sąlyga IF negali būti kitoje sąlygoje");
                }
                //zymeklio_kurimas();
                // Dar bus tvarkoma     if(Grid.GetColumn(Zymeklis)==0)
                   string salygosPadetis = (string)e.Data.GetData("SalygaIF");



            }

            if (e.Data.GetDataPresent("FOR"))
            {
                
            }


        }
/// <summary>
/// Pagalbines funkcijos
/// </summary>

        private void zymeklio_kurimas(int x, int y, int ID, bool leidziaIF, string orientacija)
        {
           
            zymekliuKolekcija.Add(new Zymeklis() {X=x, Y=y, galimaIF=leidziaIF, atskaitosID=ID, Orientacija=orientacija});
            int n = zymekliuKolekcija.Count - 1;
            zymekliuKolekcija[n].Drop += zymeklis_Drop;
            zymekliuKolekcija[n].AllowDrop = true;
            zymekliuKolekcija[n].DragEnter += zymeklis_DragEnter;
            zymekliuKolekcija[n].DragLeave += zymeklis_DragLeave;
            zymekliuKolekcija[n].KeyDown += zymeklis_KeyDown;
            Gridas.Children.Add(zymekliuKolekcija[n]);
            Grid.SetColumn(zymekliuKolekcija[n],x);
            Grid.SetRow(zymekliuKolekcija[n],y);

        }


        private void zymeklis_DragEnter(object sender, DragEventArgs e)
        {
            Zymeklis zymeklisLocal = sender as Zymeklis;
            zymeklisLocal.keistiImage();
        }
        private void zymeklis_DragLeave(object sender, DragEventArgs e)
        {
            Zymeklis zymeklisLocal = sender as Zymeklis;
            zymeklisLocal.keistiImage();
        }

        private void zymeklis_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Delete))
            {
                Zymeklis zymeklisLocal = sender as Zymeklis;
                Gridas.Children.Remove(zymeklisLocal);
                zymekliuKolekcija.Remove(zymeklisLocal);
                MessageBox.Show("Istrinta");
            }
           
        }

        private void ciklasFor_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Delete))
            {
                CiklasFor ciklasLocal = sender as CiklasFor;
                Gridas.Children.Remove(ciklasLocal);
                gridoObjektai.Remove(ciklasLocal);
                MessageBox.Show("Istrinta");
            }
        }

        private void ForKurimas()
        {
            gridoObjektai.Add(new CiklasFor() { X = x, Y = y, galimaIF = leidziaIF, atskaitosID = ID, Orientacija = orientacija });
            int n = zymekliuKolekcija.Count - 1;
            zymekliuKolekcija[n].Drop += zymeklis_Drop;
            zymekliuKolekcija[n].AllowDrop = true;
            zymekliuKolekcija[n].DragEnter += zymeklis_DragEnter;
            zymekliuKolekcija[n].DragLeave += zymeklis_DragLeave;
            zymekliuKolekcija[n].KeyDown += zymeklis_KeyDown;
            Gridas.Children.Add(zymekliuKolekcija[n]);
            Grid.SetColumn(zymekliuKolekcija[n], x);
            Grid.SetRow(zymekliuKolekcija[n], y);
        }

        private void SalygosKurimas()
        {
           
        }

    }
}
