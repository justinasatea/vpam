using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<SalygaIF> ifKolekcija = new ObservableCollection<SalygaIF>(); //Pagrindinis programos kodas
        ObservableCollection<Delay> delayKolekcija = new ObservableCollection<Delay> (); //Pagrindinis programos kodas
        ObservableCollection<CiklasFor> forKolekcija = new ObservableCollection<CiklasFor>(); //Ciklo FOR objektai
        ObservableCollection<Zymeklis> zymekliuKolekcija = new ObservableCollection<Zymeklis>(); //Ciklo FOR objektai
        ObservableCollection<KintKurimas> kintamujuKolekcija = new ObservableCollection<KintKurimas>(); //Drop zymekliai
        ObservableCollection<KintVeiksmai> kintveiksKolekcija = new ObservableCollection<KintVeiksmai>(); //Drop zymekliai
        ObservableCollection<jungtysIO> jungciuKolekcijaIN = new ObservableCollection<jungtysIO>();//Išvadai įvesčiai
        ObservableCollection<jungtysIO> jungciuKolekcijaREF = new ObservableCollection<jungtysIO>();//Išvadai priskyrimui
        ObservableCollection<jungtysIO> jungciuKolekcijaOUT = new ObservableCollection<jungtysIO>();//Išvadai įšvesčiai
        ObservableCollection<Vaikai> vaikuKolekcija = new ObservableCollection<Vaikai>(); //objektui prisklausantys kiti objektai is objektu tinklelio
        ObservableCollection<Koordinates> gridoKoordinates = new ObservableCollection<Koordinates>(); //objektu tinklelyje vaizduojami objektai
        ObservableCollection<Uzraktas> uzraktuKolekcija = new ObservableCollection<Uzraktas>();  //Ciklo FOR objektai
        ObservableCollection<Pertrauktis> pertraukciuKolekcija = new ObservableCollection<Pertrauktis>();  //Pertraukciu objektai
        ObservableCollection<setPORT> setPortKolekcija = new ObservableCollection<setPORT>(); //Isvesties isvadu reiksmes keitimo objektai

        public string bibliotekos;
        public string kintGlobalus = "";
        public string paprogrames  = "";
        public string paprogrames_0 = "";
        public string paprogrames_1 = "";
        public string paprogrames_2 = "";
        public string parengimas   = "";
        public string main_programa = "";
        public string ciklasPagrindinis = "";
        public int objektuSk=0;

        public MainWindow()
        {
            InitializeComponent();
            jungtys_pirmas();
            zymeklis_pirmas();
        }

      
        /// <summary>
        /// Drag įvykiai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void IF_MouseMove(object sender, MouseEventArgs e)
        {
            Label pass = sender as Label;
            DataObject data = new DataObject();
            data.SetData("IF", "IF");
            DragDrop.DoDragDrop(pass, data, DragDropEffects.Move);
        }

        private void FOR_MouseMove(object sender, MouseEventArgs e)
        {
            Label pass = sender as Label;
            DataObject data = new DataObject();
            data.SetData("FOR", "FOR");
            DragDrop.DoDragDrop(pass, data, DragDropEffects.Move);
        }
        private void gridoObjektas_MouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.RightButton == MouseButtonState.Pressed)
            {
                string pavadinimas="DEFAULT";
                int duomenys=0;
                int indeksas = 0;             
                Zymeklis siuntejas = new Zymeklis((SolidColorBrush)(new BrushConverter().ConvertFrom("#ffaacc")),"tekstas");
                DataObject data = new DataObject();
                if (sender is CiklasFor) {
                   
                    pavadinimas = "TevasID";
                    CiklasFor tmp = sender as CiklasFor;
                    indeksas = forKolekcija.IndexOf(tmp);
                    data.SetData("OBJEKTAS", tmp);

                }
                if (sender is SalygaIF)
                {
                    pavadinimas = "IF";
                }

                for (int i = 0; i < gridoKoordinates.Count(); i++)
                {
                    if (gridoKoordinates[i].ObjektoID == indeksas)
                    {
                        duomenys = gridoKoordinates[i].ManoID;

                    }
                }
                data.SetData(pavadinimas, duomenys);
                DragDrop.DoDragDrop(siuntejas, data, DragDropEffects.Move);
            }
        }

        private void Rubish_Drop(object sender, DragEventArgs e)
        {
         
            if (e.Data.GetDataPresent("TevasID"))
            {
                int indeksas = (int) e.Data.GetData("TevasID"); 
              //  Gridas.Children.Remove(e.Data.GetData("OBJEKTAS"));
                gridoKoordinates.RemoveAt(indeksas);
               

            }

            if (e.Data.GetDataPresent("OBJEKTAS"))
            {
                int indeksas = (int)e.Data.GetData("Objektas");
            }
  
        }

        private void DELAY_MouseMove(object sender, MouseEventArgs e)
        {
            Label pass = sender as Label;
            DataObject data = new DataObject();
            data.SetData("DELAY", "DELAY");
            DragDrop.DoDragDrop(pass, data, DragDropEffects.Move);
        }

        private void SETPORT_MouseMove(object sender, MouseEventArgs e)
        {
            Label pass = sender as Label;
            DataObject data = new DataObject();
            data.SetData("SETPORT", "SETPORT");
            DragDrop.DoDragDrop(pass, data, DragDropEffects.Move);
        }
        private void PERTRAUKTIS_MouseMove(object sender, MouseEventArgs e)
        {
            Label pass = sender as Label;
            DataObject data = new DataObject();
            data.SetData("PERTRAUKTIS", "PERTRAUKTIS");
            DragDrop.DoDragDrop(pass, data, DragDropEffects.Move);
        }
        private void NKINT_MouseMove(object sender, MouseEventArgs e)
        {
            Label pass = sender as Label;
            DataObject data = new DataObject();
            data.SetData("KINTAMASIS", "KINTAMASIS");
            DragDrop.DoDragDrop(pass, data, DragDropEffects.Move);
        }
        private void KINTV_MouseMove(object sender, MouseEventArgs e)
        {
            Label pass = sender as Label;
            DataObject data = new DataObject();
            data.SetData("VEIKSMASK", "VEIKSMASK");
            DragDrop.DoDragDrop(pass, data, DragDropEffects.Move);
        }
        private void For_ReMove(object sender, MouseEventArgs e)
        {
            TextBlock asss = sender as TextBlock;
            DataObject data = new DataObject();
            data.SetData("FOR", "FOR");
            DragDrop.DoDragDrop(asss, data, DragDropEffects.Move);
        }
        /// <summary>
        /// zymekliu įvykiai
        /// </summary>
        /// 

        /// 
        private void zymeklis_Drop(object sender, DragEventArgs e)
        {
            Zymeklis zymeklisLocal = sender as Zymeklis;
            zymeklisLocal.AtstatytiImage();
            int n = zymekliuKolekcija.IndexOf(zymeklisLocal);
            ////////////////IF///////////////////////////////////////////////////////////////////        
            if (e.Data.GetDataPresent("IF"))
            {
                if (zymeklisLocal.galimaIF)
                {
                    SalygosKurimas(zymeklisLocal);
                    
                }
                else
                {
                    MessageBox.Show("Šioje programos versijoje sąlyga \"IF\" negali būti \"Else\" srityje");
                }



            }
            //////////////FOR///////////////////////////////////////////////////////////////////
            if (e.Data.GetDataPresent("FOR"))
            {
                ForKurimas(zymeklisLocal);
            }
            if (e.Data.GetDataPresent("DELAY"))
            {
                Delay_kurimas(zymeklisLocal);
            }
            if (e.Data.GetDataPresent("SETPORT"))
            {
               SETPORT_kurimas(zymeklisLocal);
            }
            if (e.Data.GetDataPresent("VEIKSMASK"))
            {
                VEIKSMASK_kurimas(zymeklisLocal);
            }
            if (e.Data.GetDataPresent("PERTRAUKTIS"))
            {
                if (zymeklisLocal.galimaPertr)
                {
                    Pertrauktis_kurimas(zymeklisLocal);
                }
                else
                {
                    MessageBox.Show("Šioje programos versijoje pertrauktys kuriamos tik padėtos ant paskutinio žymeklio");
                }
               
            }
            if (e.Data.GetDataPresent("KINTAMASIS"))
            {
                if (zymeklisLocal.galimaPertr)
                {
                    KINT_kurimas(zymeklisLocal);
                }
                else
                {
                    MessageBox.Show("Šioje programos versijoje kintamieji kuriami tik padėti ant paskutinio žymeklio");
                }

            }

        }
/// <summary>
/// Pagalbines funkcijos
/// </summary>
/// 
               private void jungtys_pirmas()
              {
                  for (int i = 0; i < 14; i++)
                        {
                            if (i < 8)
                    {
                    jungciuKolekcijaIN.Add(new jungtysIO()  { Registras = 'D', LockIN = 0, LockOUT = 0, Pavadinimas = "JUNGTIS" + Convert.ToString(i), DDRx = 0, IOx = 0, Pozicija = i });
                    jungciuKolekcijaOUT.Add(new jungtysIO() { Registras = 'D', LockIN = 0, LockOUT = 0, Pavadinimas = "JUNGTIS" + Convert.ToString(i), DDRx = 0, IOx = 0, Pozicija = i });
                    jungciuKolekcijaREF.Add(new jungtysIO() { Registras = 'D', LockIN = 0, LockOUT = 0, Pavadinimas = "JUNGTIS" + Convert.ToString(i), DDRx = 0, IOx = 0, Pozicija = i });
                    }
                   else
                {
                    jungciuKolekcijaIN.Add(new jungtysIO()  { Registras = 'B', LockIN = 0, LockOUT = 0, Pavadinimas = "JUNGTIS" + Convert.ToString(i), DDRx = 0, IOx = 0, Pozicija = i - 8 });
                    jungciuKolekcijaOUT.Add(new jungtysIO() { Registras = 'B', LockIN = 0, LockOUT = 0, Pavadinimas = "JUNGTIS" + Convert.ToString(i), DDRx = 0, IOx = 0, Pozicija = i - 8 });
                    jungciuKolekcijaREF.Add(new jungtysIO() { Registras = 'B', LockIN = 0, LockOUT = 0, Pavadinimas = "JUNGTIS" + Convert.ToString(i), DDRx = 0, IOx = 0, Pozicija = i - 8 });
                }
            }
        }
             private void zymeklis_pirmas()
             {

            zymeklio_kurimas(0, 0, true, true, "V", -1, -1,"","","Brown","Pridėti");
            
        }


        private void zymeklio_kurimas(int x,int y,bool IF,bool Pert,string orien,int pora,int vKolekID,string prad,string pab,string spal,string text)
        {
            objektuSk++;
            int n = zymekliuKolekcija.Count();
            gridoKoordinates.Add(new Koordinates() {X=x, ObjektoID=n, Tipas="zymeklis", Y=y, ManoID = objektuSk });
            Brush bru = (SolidColorBrush)(new BrushConverter().ConvertFrom(spal));
            zymekliuKolekcija.Add(new Zymeklis(bru,text) {X=x, Y=y, galimaIF=IF, galimaPertr=Pert, zymeklioID= objektuSk, Orientacija=orien});
            Gridas.Children.Add(zymekliuKolekcija[n]);
            Grid.SetColumn(zymekliuKolekcija[n], y);
            Grid.SetRow(zymekliuKolekcija[n], x);
            zymekliuKolekcija[n].Drop += zymeklis_Drop;
            zymekliuKolekcija[n].AllowDrop = true;
            zymekliuKolekcija[n].DragEnter += zymeklis_DragEnter;
            zymekliuKolekcija[n].DragLeave += zymeklis_DragLeave;
            zymekliuKolekcija[n].KeyDown += PaspaustasKey;
            zymekliuKolekcija[n].MouseMove += gridoObjektas_MouseMove;
            zymekliuKolekcija[n].ToolTip = gridoKoordinates.Count.ToString();
            VaikuPlanavimas(objektuSk, pora, prad, pab);
            if (n > 0)
            {
                PridetiVaika(vKolekID, objektuSk, 0);
            }
        }


        private void VaikuPlanavimas(int zym, int tev,string prad, string pab)
        {
            vaikuKolekcija.Add(new Vaikai(prad, pab) { ZymeklioID = zym,  TevoID = tev });
        }

        private void PridetiVaika(int zymeklisLocal, int objektas, int artevas)
        {
            for (int i = 0; i < vaikuKolekcija.Count(); i++)
            {
                if (vaikuKolekcija[i].ZymeklioID == zymeklisLocal)
                {
                
                    vaikuKolekcija[i].VaikasAdd(objektas, artevas);
                }
            }
        }

       

        private void Vietos_keitimas_eiluteje_PUSH(int x, int y)
        {
              sekcija_tinkle_add("H");
            for (int i=0; i<gridoKoordinates.Count(); i++)
            {
                if((gridoKoordinates[i].X==x)&&(gridoKoordinates[i].Y>=y))
                {
                    int id;
                    switch (gridoKoordinates[i].Tipas)
                    {
                        case "zymeklis":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "zymeklis");
                            gridoKoordinates[i].Y += 1;
                            zymekliuKolekcija[id].Y += 1;
                            Grid.SetColumn(zymekliuKolekcija[id], gridoKoordinates[i].Y);
                            break;
                        case "KINTAMASIS":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "KINTAMASIS");
                            gridoKoordinates[i].Y += 1;
                            Grid.SetColumn(kintamujuKolekcija[id], gridoKoordinates[i].Y);
                         break;
                        case "VEIKSMASK":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "VEIKSMASK");
                            gridoKoordinates[i].Y += 1;
                            Grid.SetColumn(kintveiksKolekcija[id], gridoKoordinates[i].Y);
                            break;
                        case "FOR":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "FOR");
                            gridoKoordinates[i].Y += 1;
                            Grid.SetColumn(forKolekcija[id], gridoKoordinates[i].Y);
                            break;
                        case "IF":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "IF");
                            gridoKoordinates[i].Y += 1;
                            Grid.SetColumn(ifKolekcija[id], gridoKoordinates[i].Y);
                            break;
                        case "DELAY":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "DELAY");
                            gridoKoordinates[i].Y += 1;
                            Grid.SetColumn(delayKolekcija[id], gridoKoordinates[i].Y);
                            break;
                        case "SETPORT":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "SETPORT");
                            gridoKoordinates[i].Y += 1;
                            Grid.SetColumn(setPortKolekcija[id], gridoKoordinates[i].Y);
                            break;
                        case "PERTRAUKTIS":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "PERTRAUKTIS");
                            gridoKoordinates[i].Y += 1;
                            Grid.SetColumn(pertraukciuKolekcija[id], gridoKoordinates[i].Y);
                            break;
                    }

                }
                        
            }
            
        }

        private void Vietos_keitimas_stulpelyje_PUSH(int x, int y)
        {

            sekcija_tinkle_add("V");
            for (int i = 0; i < gridoKoordinates.Count(); i++)
            {
                if ((gridoKoordinates[i].X >= x))
                {
                    int id = 0;
                        gridoKoordinates[i].X += 1;
                        switch (gridoKoordinates[i].Tipas)
                        {
                            case "zymeklis":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "zymeklis");
                            zymekliuKolekcija[id].X += 1;
                            Grid.SetRow(zymekliuKolekcija[id], gridoKoordinates[i].X);
                            break;
                            case "FOR":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "FOR");
                            Grid.SetRow(forKolekcija[id], gridoKoordinates[i].X);
                            break;
                            case "KINTAMASIS":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "KINTAMASIS");
                            Grid.SetRow(kintamujuKolekcija[id], gridoKoordinates[i].X);
                            break;
                            case "VEIKSMASK":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "VEIKSMASK");
                            Grid.SetRow(kintveiksKolekcija[id], gridoKoordinates[i].X);
                            break;
                            case "IF":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "IF");
                            Grid.SetRow(ifKolekcija[id], gridoKoordinates[i].X);
                            break;
                            case "DELAY":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "DELAY");
                            Grid.SetRow(delayKolekcija[id], gridoKoordinates[i].X);
                            break;
                            case "SETPORT":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "SETPORT");
                            Grid.SetRow(setPortKolekcija[id], gridoKoordinates[i].X);
                            break;
                            case "PERTRAUKTIS":
                            id = VietaKolekcijoje(gridoKoordinates[i].ManoID, "PERTRAUKTIS");
                            Grid.SetRow(pertraukciuKolekcija[id], gridoKoordinates[i].X);
                            break;
                    }

                    }
               }
          }


        private void sekcija_tinkle_add(string z)
        {
            if (z == "V")
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(100, GridUnitType.Pixel);
                Gridas.RowDefinitions.Add(r);

            }
            if (z=="H")
            {
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(100, GridUnitType.Pixel);
                Gridas.ColumnDefinitions.Add(c);
            }
            
       }

        private void zymeklis_DragEnter(object sender, DragEventArgs e)
        {
            Zymeklis zymeklisLocal = sender as Zymeklis;
            zymeklisLocal.keistiImage();
        }
        private void zymeklis_DragLeave(object sender, DragEventArgs e)
        {
            Zymeklis zymeklisLocal = sender as Zymeklis;
            zymeklisLocal.AtstatytiImage();
        }

        

        private void PaspaustasKey(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Ištrinti elemento šiuo metu negalima");
        }


        private void ForKurimas(Zymeklis zymeklisLocal)
        {
            int xobj = zymeklisLocal.X;
            int yobj = zymeklisLocal.Y;
            int xzym = xobj;
            int yzym = yobj;
            bool leidziaif = false;
            bool leidziaPert = false;
            if (zymeklisLocal.Orientacija == "V")
            {
                xzym += 1;
                leidziaif = true;
                Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
                Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
            }
            if (zymeklisLocal.Orientacija == "H")
            {
                yzym+=1;
                Vietos_keitimas_eiluteje_PUSH(xobj, yobj);
                Vietos_keitimas_eiluteje_PUSH(xobj, yobj);
            }

            objektuSk++;
            int manoid = objektuSk;
            int kolekcijosId = forKolekcija.Count();
            forKolekcija.Add(new CiklasFor("i" + objektuSk.ToString()) { ObjektoID = manoid });
            Gridas.Children.Add(forKolekcija[kolekcijosId]);
            forKolekcija[kolekcijosId].MouseMove += gridoObjektas_MouseMove;
            gridoKoordinates.Add(new Koordinates() {X = xobj, ManoID = manoid, Y = yobj, Tipas = "FOR" });
            PridetiVaika(zymeklisLocal.zymeklioID, manoid, 1);
            Grid.SetRow(forKolekcija[kolekcijosId], xobj);
            Grid.SetColumn(forKolekcija[kolekcijosId], yobj);
            zymeklio_kurimas(xzym, yzym, leidziaif, leidziaPert, zymeklisLocal.Orientacija, manoid, zymeklisLocal.zymeklioID,"","}\n","#FFC42B46","Ciklo \n pabaiga");
    }

        private void Delay_kurimas(Zymeklis zymeklisLocal)
        {
            int xobj = zymeklisLocal.X;
            int yobj = zymeklisLocal.Y;
            int xzym = xobj;
            int yzym = yobj;

            if (zymeklisLocal.Orientacija == "V")
            {
                 Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
            }
            if (zymeklisLocal.Orientacija == "H")
            {
                Vietos_keitimas_eiluteje_PUSH(xobj, yobj);
            }
            objektuSk++;
            int manoid = objektuSk;
            int kolekcijosId = delayKolekcija.Count();
            delayKolekcija.Add(new Delay() { ObjektoID = manoid });
            Gridas.Children.Add(delayKolekcija[kolekcijosId]);
            gridoKoordinates.Add(new Koordinates() {ManoID = manoid, X = xobj, Y = yobj, Tipas = "DELAY" });
            Grid.SetRow(delayKolekcija[kolekcijosId], xobj);
            Grid.SetColumn(delayKolekcija[kolekcijosId], yobj);
            PridetiVaika(zymeklisLocal.zymeklioID, manoid, 0);
        }

        private void VEIKSMASK_kurimas(Zymeklis zymeklisLocal)
        {
            int xobj = zymeklisLocal.X;
            int yobj = zymeklisLocal.Y;
            int xzym = xobj;
            int yzym = yobj;

            if (zymeklisLocal.Orientacija == "V")
            {
                Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
            }
            if (zymeklisLocal.Orientacija == "H")
            {
                Vietos_keitimas_eiluteje_PUSH(xobj, yobj);
            }
            objektuSk++;
            int manoid = objektuSk;
            int kolekcijosId = kintveiksKolekcija.Count();
            kintveiksKolekcija.Add(new KintVeiksmai() { ObjektoID = manoid, Kintamieji=kintamujuKolekcija });
            Gridas.Children.Add(kintveiksKolekcija[kolekcijosId]);
            gridoKoordinates.Add(new Koordinates() { ManoID = manoid, X = xobj, Y = yobj, Tipas = "VEIKSMASK" });
            Grid.SetRow(kintveiksKolekcija[kolekcijosId], xobj);
            Grid.SetColumn(kintveiksKolekcija[kolekcijosId], yobj);
            PridetiVaika(zymeklisLocal.zymeklioID, manoid, 0);
        }

        private void KINT_kurimas(Zymeklis zymeklisLocal)
        {
            int xobj = zymeklisLocal.X;
            int yobj = zymeklisLocal.Y;
            int xzym = xobj;
            int yzym = yobj;
            Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
            objektuSk++;
            int manoid = objektuSk;
            int kolekcijosId = kintamujuKolekcija.Count();
            kintamujuKolekcija.Add(new KintKurimas(manoid.ToString()) { ObjektoID = manoid });
            Gridas.Children.Add(kintamujuKolekcija[kolekcijosId]);
            gridoKoordinates.Add(new Koordinates() { ManoID = manoid, X = xobj, Y = yobj, Tipas = "KINTAMASIS" });
            Grid.SetRow(kintamujuKolekcija[kolekcijosId], xobj);
            Grid.SetColumn(kintamujuKolekcija[kolekcijosId], yobj);
            PridetiVaika(zymeklisLocal.zymeklioID, manoid, 0);
          }


        private void Pertrauktis_kurimas(Zymeklis zymeklisLocal)
        {
            int xobj = zymeklisLocal.X;
            int yobj = zymeklisLocal.Y;
            int xzym = xobj;
            int yzym = yobj;
             xzym += 1;
            bool leidziaif = true;
            bool leidziaPert = false;
            Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
            Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
            objektuSk++;
            int manoid = objektuSk;
            int kolekcijosId = pertraukciuKolekcija.Count();
            pertraukciuKolekcija.Add(new Pertrauktis(){ ObjektoID = manoid, Jungtys=jungciuKolekcijaIN });
            Gridas.Children.Add(pertraukciuKolekcija[kolekcijosId]);
            pertraukciuKolekcija[kolekcijosId].Changed += new pertrauktisEventHandler(PertrChanged);
            gridoKoordinates.Add(new Koordinates() { X = xobj, ManoID = manoid, Y = yobj, Tipas = "PERTRAUKTIS" });
            PridetiVaika(zymeklisLocal.zymeklioID, manoid, 1);
            Grid.SetRow(pertraukciuKolekcija[kolekcijosId], xobj);
            Grid.SetColumn(pertraukciuKolekcija[kolekcijosId], yobj);
            zymeklio_kurimas(xzym, yzym, leidziaif, leidziaPert, zymeklisLocal.Orientacija, manoid, zymeklisLocal.zymeklioID, "", "}\n","#FF0D8DAA","Pertraukties \n pabaiga");
        }
        private void PertrChanged(object sender, EventArgumentai e)
        {
            Pertrauktis pertrauktis = sender as Pertrauktis;
            int uzraktoid = -1; int jungtiesSK = 0;
            string pavadinimas = " ";
            //patrikina visus užraktus ar yra užrakintas išvadas su Salygaif objektu 
            for (int i = 0; i < uzraktuKolekcija.Count(); i++)
            {
                if ((uzraktuKolekcija[i].UzraktoTipas == "PERTRAUKTIS") && (uzraktuKolekcija[i].UzraktoID == pertrauktis.ObjektoID.ToString()))
                {
                    uzraktoid = i;
                    pavadinimas = uzraktuKolekcija[i].PinPavadinimas;
                    for (int j = 0; j < uzraktuKolekcija.Count(); j++)
                    {
                        if (uzraktuKolekcija[j].PinPavadinimas == pavadinimas)
                        {
                            jungtiesSK++;
                        }
                    }
                }
            }
            //jei pertraukciu objektas buvo rastas tai jis bus panaikintas, nes kursime naują užraktą parinktam išvadui 
            if (uzraktoid >= 0)
            {
                uzraktuKolekcija.RemoveAt(uzraktoid);
                if (jungtiesSK == 1)
                {
                    for (int j = 0; j < jungciuKolekcijaREF.Count(); j++)
                    {
                        if (jungciuKolekcijaREF[j].Pavadinimas == pavadinimas)
                        {
                            jungciuKolekcijaREF[j].LockIN = 0;
                        }
                    }
                }
            }
            //sukuriamas uzraktas su pertraukties objektu ir jam priskirtu išvadu įvesčiai
            uzraktuKolekcija.Add(new Uzraktas() { UzraktoTipas = "PERTRAUKTIS", UzraktoID = pertrauktis.ObjektoID.ToString(), PinPavadinimas = e.tekstasInfo });
            for (int j = 0; j < jungciuKolekcijaREF.Count(); j++)
            {
                if (jungciuKolekcijaREF[j].Pavadinimas == e.tekstasInfo)
                {
                    jungciuKolekcijaREF[j].LockIN = 1;
                }
            }
            //Sutvarkomos jungciuKolekcijaIN ir jungciuKolekcijaOUT pagal dabartini jungciuKolekcijaREF
            Updatejungtys();
        }


        private void SETPORT_kurimas(Zymeklis zymeklisLocal)
        {
            int xobj = zymeklisLocal.X;
            int yobj = zymeklisLocal.Y;
            int xzym = xobj;
            int yzym = yobj;

            if (zymeklisLocal.Orientacija == "V")
            {
                Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
            }
            if (zymeklisLocal.Orientacija == "H")
            {
                Vietos_keitimas_eiluteje_PUSH(xobj, yobj);
            }
            objektuSk++;
            int manoid = objektuSk;
            int kolekcijosId = setPortKolekcija.Count();
            setPortKolekcija.Add(new setPORT() { JungtysOUT = jungciuKolekcijaOUT, ObjektoID=manoid});
            setPortKolekcija[kolekcijosId].Changed += new setPORTEventHandler(SETChanged);
            setPortKolekcija[kolekcijosId].Canceled += new undoPORTEventHandler(SETCanceled);
            Gridas.Children.Add(setPortKolekcija[kolekcijosId]);
            gridoKoordinates.Add(new Koordinates() { ManoID = manoid, X = xobj, Y = yobj, Tipas = "SETPORT" });
            Grid.SetRow(setPortKolekcija[kolekcijosId], xobj);
            Grid.SetColumn(setPortKolekcija[kolekcijosId], yobj);
            PridetiVaika(zymeklisLocal.zymeklioID, manoid, 0);
           
        }

        private void SETChanged(object sender, EventArgumentai e)
        {
            setPORT setport = sender as setPORT;
            uzraktuKolekcija.Add(new Uzraktas() { UzraktoTipas = "SETPORT", UzraktoID = setport.ObjektoID.ToString(), PinPavadinimas = e.tekstasInfo });
            for (int j = 0; j < jungciuKolekcijaREF.Count(); j++)
            {
                if (jungciuKolekcijaREF[j].Pavadinimas == e.tekstasInfo)
                {
                    jungciuKolekcijaREF[j].LockOUT = 1;
                }
            }
            Updatejungtys();
        }


        private void SETCanceled(object sender, EventArgumentai e)
        {

            setPORT setport = sender as setPORT;
            int uzraktoid = -1; int jungtiesSK = 0;
            string pavadinimas = " ";
            //patrikina visus užraktus ar yra užrakintas išvadas su setPORT objektu 
            for (int i = 0; i < uzraktuKolekcija.Count(); i++)
            {
                if ((uzraktuKolekcija[i].PinPavadinimas == e.tekstasInfo) && (uzraktuKolekcija[i].UzraktoID == setport.ObjektoID.ToString()))
                {
                    uzraktoid = i;
                    pavadinimas = uzraktuKolekcija[i].PinPavadinimas;
                    for (int j = 0; j < uzraktuKolekcija.Count(); j++)
                    {
                        if (uzraktuKolekcija[j].PinPavadinimas == pavadinimas)
                        {
                            jungtiesSK++;
                        }
                    }
                }
            }
            //setPORT objektas buvo rastas tai jis bus panaikintas
            if (uzraktoid >= 0)
            {
                uzraktuKolekcija.RemoveAt(uzraktoid);
                if (jungtiesSK == 1) // jei buvo tik vienas užraktas išvadui, jį atrakinsime įvesčiai
                {
                    for (int j = 0; j < jungciuKolekcijaREF.Count(); j++)
                    {
                        if (jungciuKolekcijaREF[j].Pavadinimas == pavadinimas)
                        {
                            jungciuKolekcijaREF[j].LockOUT = 0;
                        }
                    }
                }
            }
            Updatejungtys();
        }



        private void SalygosKurimas(Zymeklis zymeklisLocal)
        {
            
            int xobj = zymeklisLocal.X;
            int yobj = zymeklisLocal.Y;
            int xzym = xobj+1;
            int yzym = yobj;
            int xzym1 = xobj;
            int yzym1 = yobj+1;
            
            objektuSk++;
            int manoid = objektuSk;
            Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
            Vietos_keitimas_stulpelyje_PUSH(xobj, yobj);
            Vietos_keitimas_eiluteje_PUSH(xobj, yobj);
            int kolekcijosId = ifKolekcija.Count();
            ifKolekcija.Add(new SalygaIF() { Jungtys = jungciuKolekcijaIN, Kintamieji = kintamujuKolekcija, ObjektoID = manoid});
            Gridas.Children.Add(ifKolekcija[kolekcijosId]);
            ifKolekcija[kolekcijosId].Changed += new IFEventHandler(IFChanged);
            gridoKoordinates.Add(new Koordinates() {ManoID=manoid, X = xobj, Y = yobj, Tipas = "IF" });
            Grid.SetRow(ifKolekcija[kolekcijosId], xobj);
            Grid.SetColumn(ifKolekcija[kolekcijosId], yobj);
            zymeklio_kurimas(xzym, yzym, true, false, "V", manoid, zymeklisLocal.zymeklioID,"", "} \n","#FF9B142C","Sąlygos TAIP\n pabaiga");
            zymeklio_kurimas(xzym1, yzym1, false, false, "H", manoid, zymeklisLocal.zymeklioID,"else{ \n", "} \n","#FF9B142C","Sąlygos \n pabaiga");
            PridetiVaika(zymeklisLocal.zymeklioID, manoid, 1);
           
        }

        private void IFChanged(object sender, EventArgumentai e)
        {
            
            SalygaIF salygaif = sender as SalygaIF;
            int uzraktoid =-1; int jungtiesSK=0;
            string pavadinimas=" ";
            //patrikina visus užraktus ar yra užrakintas išvadas su Salygaif objektu 
            for (int i = 0; i < uzraktuKolekcija.Count(); i++)
            {
                if ((uzraktuKolekcija[i].UzraktoTipas == "IF") && (uzraktuKolekcija[i].UzraktoID == salygaif.ObjektoID.ToString()))
                {
                    uzraktoid = i;
                    pavadinimas = uzraktuKolekcija[i].PinPavadinimas;
                    for (int j = 0; j < uzraktuKolekcija.Count(); j++) { 
                        if (uzraktuKolekcija[j].PinPavadinimas == pavadinimas)
                            {
                             jungtiesSK++;
                            }
                }
              } 
           }
            //jei salygos objektas buvo rastas tai jis bus panaikintas, nes kursime naują užraktą SalygaIF parinktam išvadui
            if (uzraktoid>=0){
                uzraktuKolekcija.RemoveAt(uzraktoid);
                 if (jungtiesSK == 1)
                {
                    for (int j = 0; j < jungciuKolekcijaREF.Count(); j++)
                    {
                        if (jungciuKolekcijaREF[j].Pavadinimas == pavadinimas)
                        {
                            jungciuKolekcijaREF[j].LockIN = 0;
                        }
                    }
               }
            }
            //sukuriamas uzraktas su SalygaIF ir jam priskirtu išvadu įvesčiai
            uzraktuKolekcija.Add(new Uzraktas() { UzraktoTipas = "IF", UzraktoID = salygaif.ObjektoID.ToString(), PinPavadinimas = e.tekstasInfo });
            for (int j = 0; j < jungciuKolekcijaREF.Count(); j++)
            {
                if (jungciuKolekcijaREF[j].Pavadinimas == e.tekstasInfo)
                {
                    jungciuKolekcijaREF[j].LockIN = 1;
                }
            }
            Updatejungtys();
   }

        private void Updatejungtys()
        {

            jungciuKolekcijaIN.Clear();
            jungciuKolekcijaOUT.Clear();
            for (int i = 0; i < jungciuKolekcijaREF.Count(); i++)
            {
                if (jungciuKolekcijaREF[i].LockIN == 0)
                {
                    jungciuKolekcijaOUT.Add(jungciuKolekcijaREF[i]);
                }
                if (jungciuKolekcijaREF[i].LockOUT == 0)
                {
                    jungciuKolekcijaIN.Add(jungciuKolekcijaREF[i]);
                }
            }
        }




        private void kodasC_Click(object sender, RoutedEventArgs e)
        {
            kelti.IsEnabled = true;
            kodasC.Text ="";
            bibliotekos ="";
            kintGlobalus= "";
            paprogrames ="";
            paprogrames_0 = "";
            paprogrames_1 = "";
            paprogrames_2 = "";
            parengimas = "";
            main_programa = "";
            ciklasPagrindinis = "";
            kodoKurimas(0," ", "pagrCiklas","");
            bibliotekos = "#include <avr/io.h> \n"; 
            if (delayKolekcija.Count() > 0)
            {
                bibliotekos += "#include <util/delay.h> \n";
            }
            if (pertraukciuKolekcija.Count() > 0)
            {
                if (paprogrames_0 != "")
                {
                    paprogrames_0 += "} \n";
                }
                if (paprogrames_2 != "")
                {
                    paprogrames_2 += "} \n";
                }
                bibliotekos += "#include <avr/interrupt.h> \n";
                paprogrames += "/*Pertrauktys*/ \n" + paprogrames_0 + paprogrames_2 + pertraukciuParuosimas()+"\n";
                main_programa += "initInterrupt();\n";
            }
            kodasC.Text += "\n/*Bibliotekos*/ \n" + bibliotekos;
            kodasC.Text += "\n/*Glob. kintamieji*/ \n" + kintGlobalus + "\n";
            kodasC.Text += "\n/*Papgrogramės*/ \n" + paprogrames + "\n";
            kodasC.Text += "\n/*Programa*/ \n" + "\n int main(void) \n{\n"+isvaduParengimas()+main_programa + "\n";
            kodasC.Text += "\n/*Pagrindinis ciklas*/ \n" + " while(1){ \n" + ciklasPagrindinis + "\n";
            kodasC.Text += "\n         } \n return(0); /*Programos pabaiga*/  \n}";
        }


        private void kodoKurimas(int ID, string tarpas,string siuntimas, string siuntimasPaprg)
        {
            int id=0;
            string lygiavimas = tarpas;
            string siuntimasVaikams= "pagrCiklas";
            int nariuSk = vaikuKolekcija[ID].VaikuSar.Count();
            if (siuntimas == "pagrCiklas")
            {
                ciklasPagrindinis += tarpas + vaikuKolekcija[ID].pradKodas;
            }
            if(siuntimas=="paprogrames")
            {
                pertraukciuKodas(vaikuKolekcija[ID].pradKodas, siuntimasPaprg);
            }

            for (int tmp=0; tmp<nariuSk; tmp++)
            {
                int idGridKolekcijoje = 0;
                for(int i=0; i < gridoKoordinates.Count(); i++)
                {
                    if (gridoKoordinates[i].ManoID == vaikuKolekcija[ID].VaikuSar[tmp])
                    {
                        idGridKolekcijoje = gridoKoordinates.IndexOf(gridoKoordinates[i]);
                    }
                }

                switch (gridoKoordinates[idGridKolekcijoje].Tipas)
                    {
                    case "FOR":
                        id = VietaKolekcijoje(gridoKoordinates[idGridKolekcijoje].ManoID, "FOR");
                        kintGlobalus += "volatile uint8_t "+ forKolekcija[id].Kintamasis+ ";\n";
                        if (siuntimas == "pagrCiklas")
                          {
                            ciklasPagrindinis += tarpas+ forKolekcija[id].kartojimuKiekisKodas;
                            siuntimasVaikams ="pagrCiklas";
                          }
                        if (siuntimas == "paprogrames" )
                         {
                           pertraukciuKodas(forKolekcija[id].kartojimuKiekisKodas, siuntimasPaprg);
                           siuntimasVaikams = "paprogrames";
                        }
                       break;
                    case "IF":
                        id = VietaKolekcijoje(gridoKoordinates[idGridKolekcijoje].ManoID, "IF");
                        if (siuntimas == "pagrCiklas")
                        {
                            //   MessageBox.Show("Patekau ir cia");
                            ciklasPagrindinis +=tarpas+ ifKolekcija[id].Kodas;
                            siuntimasVaikams = "pagrCiklas";
                            
                        }
                        if (siuntimas == "paprogrames")
                        {
                            pertraukciuKodas(ifKolekcija[id].Kodas, siuntimasPaprg);
                            siuntimasVaikams = "paprogrames";
                        }
                        break;
                    case "DELAY":
                        id = VietaKolekcijoje(gridoKoordinates[idGridKolekcijoje].ManoID, "DELAY");
                        if (siuntimas == "pagrCiklas")
                            {
                            ciklasPagrindinis+=tarpas+ delayKolekcija[id].Kodas;
                            }
                        if (siuntimas == "paprogrames")
                        {
                            pertraukciuKodas(delayKolekcija[id].Kodas, siuntimasPaprg);
                           }                      
                     break;
                    case "VEIKSMASK":
                        id = VietaKolekcijoje(gridoKoordinates[idGridKolekcijoje].ManoID, "VEIKSMASK");
                        if (siuntimas == "pagrCiklas")
                        {
                            ciklasPagrindinis += tarpas + kintveiksKolekcija[id].Kodas;
                        }
                        if (siuntimas == "paprogrames")
                        {
                            pertraukciuKodas(kintveiksKolekcija[id].Kodas, siuntimasPaprg);
                        }
                        break;
                    case "KINTAMASIS":
                        id = VietaKolekcijoje(gridoKoordinates[idGridKolekcijoje].ManoID, "KINTAMASIS");
                             kintGlobalus += "volatile uint16_t " + kintamujuKolekcija[id].nPavadinimas + ";\n";
                        break;
                    case "SETPORT":
                        id = VietaKolekcijoje(gridoKoordinates[idGridKolekcijoje].ManoID, "SETPORT");
                        if (siuntimas == "pagrCiklas")
                        {
                            ciklasPagrindinis += tarpas + setPortKolekcija[id].Kodas;
                            siuntimasVaikams = "pagrCiklas";
                        }
                        if (siuntimas == "paprogrames")
                        {
                            pertraukciuKodas(setPortKolekcija[id].Kodas, siuntimasPaprg);
                        }
                        break;
                    case "PERTRAUKTIS":
                        id = VietaKolekcijoje(gridoKoordinates[idGridKolekcijoje].ManoID, "PERTRAUKTIS");
                        //   pertraukciuKodas(pertraukciuKolekcija[id].ParuosimasPCICR, pertraukciuKolekcija[id].ParuosimasPCINT);
                        pertrVektoriai(pertraukciuKolekcija[id].FunkcijaISR, pertraukciuKolekcija[id].ParuosimasPCICR);
                        siuntimasPaprg = pertraukciuKolekcija[id].ParuosimasPCICR;
                        siuntimasVaikams = "paprogrames";
                        
                        break;
                    case "zymeklis":
                        id = zymekliuKolekcija.IndexOf(zymekliuKolekcija[gridoKoordinates[idGridKolekcijoje].ObjektoID]);
                     break;
                }

                if (vaikuKolekcija[ID].TevuSar[tmp]==1)
                {
                    
                    for (int i = 0; i < vaikuKolekcija.Count(); i++)
                    {
                        if (vaikuKolekcija[i].TevoID == gridoKoordinates[idGridKolekcijoje].ManoID)
                        {
                           kodoKurimas(i, tarpas+"  ", siuntimasVaikams, siuntimasPaprg);
                        }
                    }
                }
            }
            if (siuntimas == "pagrCiklas")
            {
                ciklasPagrindinis += tarpas+ vaikuKolekcija[ID].pabKodas;
            }
            if (siuntimas == "paprogrames")
            {
                pertraukciuKodas(vaikuKolekcija[ID].pabKodas, siuntimasPaprg);
            }

            //kodo kurimo pabaiga
        }


        public string isvaduParengimas()
        {
            string PORTD= "\n PORTD = PORTD";
            string PORTB= "\n PORTB = PORTB";
            string DDRD = "\n DDRD  = DDRD";
            string DDRB = "\n DDRB  = DDRB";
            for (int i=0; i < jungciuKolekcijaREF.Count(); i++)
            {
                if (jungciuKolekcijaREF[i].Registras.ToString() == "B")
                {
                    if (jungciuKolekcijaREF[i].LockOUT == 1)
                    {
                        DDRB += "|(1<<PB" + jungciuKolekcijaREF[i].Pozicija.ToString()+")"; 
                    }
                    else
                    {
                        if (isvaduItampa.IsChecked == true)
                        {
                         PORTB += "| (1 << PB" + jungciuKolekcijaREF[i].Pozicija.ToString()+")";
                        }
                    }
                }
                if (jungciuKolekcijaREF[i].Registras.ToString() == "D")
                {
                    if (jungciuKolekcijaREF[i].LockOUT == 1)
                    {
                        DDRD += "|(1<<PD" + jungciuKolekcijaREF[i].Pozicija.ToString() + ")";
                    }
                    else
                    {
                        if (isvaduItampa.IsChecked == true)
                        {
                            PORTD += "| (1 << PD" + jungciuKolekcijaREF[i].Pozicija.ToString() + ")";
                        }
                    }
                }
              }
                PORTB += "; ";
                PORTD += "; ";
                DDRD += "; ";
                DDRB += "; ";
            return "/*Prievadų B ir D išvadų paruošimas*/"+PORTB + DDRB + PORTD + DDRD;
        }


        //Pertraukčių vektorių priskyrimas paprogramėms 
        public void pertrVektoriai(string kodas, string vektorius)
        {
            //Pasirenkama kuriam vektoriui kuriamos paprogramės
            switch (vektorius)
            {
                case "0":
                    if (paprogrames_0 == "") //Jei pirmoji pertrauktis kurianti ISR() pertraukčių vektoriui
                    {
                        paprogrames_0 = "ISR(PCINT0_vect){ \n";
                    }
                    paprogrames_0 += kodas; //Pertraukties kodas pridedamas prie ISR()
                break;
                case "2":
                    if (paprogrames_2 == "")
                    {
                        paprogrames_2 = "ISR(PCINT2_vect){ \n";
                    }
                    paprogrames_2 += kodas;
                break;
            }
        }

        public void pertraukciuKodas(string kodas, string paprograme)
        {
            //Pasirenkama paprogramė, kuriai pridedamas kodas
            switch (paprograme)
            {
                case "0":
                    paprogrames_0 += kodas; //Pertraukties kodas pridedamas prie ISR()
                    break;
                case "2":
                    paprogrames_2 += kodas;
                    break;
            }
        }



        public string pertraukciuParuosimas()
        {
            string kodas = "";

            if (pertraukciuKolekcija.Count > 0)
            {
                ObservableCollection<string> vector0 = new ObservableCollection<string>();
                ObservableCollection<string> vector2 = new ObservableCollection<string>();
                for (int i = 0; i < pertraukciuKolekcija.Count(); i++)
                {
                    if (pertraukciuKolekcija[i].ParuosimasPCICR == "0")
                    {
                        if (!(vector0.Contains(pertraukciuKolekcija[i].ParuosimasPCINT)))
                        {
                            vector0.Add(pertraukciuKolekcija[i].ParuosimasPCINT);
                        }
                    }
                    if (pertraukciuKolekcija[i].ParuosimasPCICR == "2")
                    {
                        if (!(vector0.Contains(pertraukciuKolekcija[i].ParuosimasPCINT)))
                        {
                            vector2.Add(pertraukciuKolekcija[i].ParuosimasPCINT);
                        }
                    }
                }
                if (vector0.Count() > 0)
                {
                    kodas += "PCMSK0=PCMSK0";
                    for (int i=0; i < vector0.Count(); i++)
                    {
                        kodas += "|(1 << PCINT" + vector0[i]+")";
                    }
                    kodas += "; \n PCICR |= (1 << PCIE0); \n";
                }
                if (vector2.Count() > 0)
                {
                    kodas += "PCMSK2=PCMSK2";
                    for (int i = 0; i < vector2.Count(); i++)
                    {
                        kodas += "|(1 << PCINT" + vector2[i] + ")";
                    }
                    kodas += "; \n PCICR |= (1 << PCIE2); \n";
                }
                kodas += "sei(); \n}";
                kodas = "void initInterrupt(void) {\n" + kodas;
                }
            return kodas;
        }

        public int VietaKolekcijoje(int manoID, string tipas )
        {
            int id = 0;
            switch (tipas)
            {
                case "zymeklis":
                    for(int i=0;i< zymekliuKolekcija.Count(); i++)
                    {
                        if (zymekliuKolekcija[i].zymeklioID == manoID)
                        {
                            id = i;
                            goto zfinish;
                        }
                    }
                    zfinish:;
                    break;
                case "FOR":
                    for (int i = 0; i < forKolekcija.Count(); i++)
                    {

                        if (forKolekcija[i].ObjektoID == manoID)
                        {
                            MessageBox.Show("Rastas id" + i.ToString());
                            id = i;
                            goto ffinish;
                        }
                    }
                    ffinish:;
                 break;
                case "PERTRAUKTIS":
                    for (int i = 0; i < pertraukciuKolekcija.Count(); i++)
                    {
                    
                        if (pertraukciuKolekcija[i].ObjektoID == manoID)
                        {
                            id = i;
                            goto pfinish;
                        }
                    }
                    pfinish:;
                    break;
                case "KINTAMASIS":
                    for (int i = 0; i < kintamujuKolekcija.Count(); i++)
                    {

                        if (kintamujuKolekcija[i].ObjektoID == manoID)
                        {
                            id = i;
                            goto kfinish;
                        }
                    }
                    kfinish:;
                break;
                case "VEIKSMASK":
                    for (int i = 0; i < kintveiksKolekcija.Count(); i++)
                    {

                        if (kintveiksKolekcija[i].ObjektoID == manoID)
                        {
                            id = i;
                            goto kvfinish;
                        }
                    }
                    kvfinish:;
                    break;
                case "IF":
                    for (int i = 0; i < ifKolekcija.Count(); i++)
                    {
                        if (ifKolekcija[i].ObjektoID == manoID)
                        {
                            id = i;
                            goto iffinish;
                        }
                    }
                    iffinish:;
                    break;
                case "DELAY":
                    for (int i = 0; i < delayKolekcija.Count(); i++)
                    {
                        if (delayKolekcija[i].ObjektoID == manoID)
                        {
                            id = i;
                            goto dfinish;
                        }
                    }
                    dfinish:;
                    break;
                case "SETPORT":
                    for (int i = 0; i < setPortKolekcija.Count(); i++)
                    {
                        if (setPortKolekcija[i].ObjektoID == manoID)
                        {
                            id = i;
                            goto setfinish;
                        }
                    }
                    setfinish:;
                    break;
            }
            return id;
        }

        private void atstatyti_Click(object sender, RoutedEventArgs e)
        {
            kodasC.Text = "";
            ifKolekcija.Clear();
            delayKolekcija.Clear();
            forKolekcija.Clear();
            zymekliuKolekcija.Clear();
            kintamujuKolekcija.Clear();
            kintveiksKolekcija.Clear();
            jungciuKolekcijaIN.Clear();
            jungciuKolekcijaOUT.Clear();
            jungciuKolekcijaREF.Clear();
            vaikuKolekcija.Clear();
            gridoKoordinates.Clear();
            uzraktuKolekcija.Clear();
            pertraukciuKolekcija.Clear();
            pertraukciuKolekcija.Clear();
            setPortKolekcija.Clear();
            Gridas.Children.Clear();
            jungtys_pirmas();
            zymeklis_pirmas();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            kitasCOM.IsEnabled = true;
        }

        private void kitaLizdas_Unchecked(object sender, RoutedEventArgs e)
        {
            kitasCOM.IsEnabled = false;
        }

        private void kelti_Click(object sender, RoutedEventArgs e)
        {
            string COM = "COM5";
            if(kitasLizdas.IsChecked==true)
            {
                int selectedIndex = kitasCOM.SelectedIndex;
                COM = "COM" + (selectedIndex + 1).ToString();
            }
            if (kitaLizdas_sav.IsChecked == true)
            {
                COM = lizdas.Text;
            }

            string strCmdText;
            string avrgcc1 = "/K cd c:\\& avr-gcc -Os -DF_CPU=16000000L -mmcu=atmega328p -c -o c:\\VPAM\\Programa.o c:\\VPAM\\Programa.c";
            string avrgcc2 = "& avr-gcc -mmcu=atmega328p c:\\VPAM\\Programa.o -o c:\\VPAM\\Programa.elf ";
            string avrobj = "& avr-objcopy -O ihex c:\\VPAM\\Programa.elf c:\\VPAM\\Programa.hex ";
            string avrdude = "& cd c:\\VPAM & avrdude -p m328p -P" + COM+" -c arduino -e -U flash:w:Programa.hex";


            string path = @"c:\VPAM";
            DirectoryInfo di = Directory.CreateDirectory(path);
            File.Create(@"c:\MyTest.c");
            path = @"c:\VPAM\Programa.c";
            // This text is added only once to the file.
            File.WriteAllText(path, kodasC.Text);
            strCmdText = "/K cd c:\\" + "& cd ";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            System.Diagnostics.Process.Start("CMD.exe", avrgcc1+avrgcc2+avrobj+avrdude);
        }

        private void kitasCOM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
    
        }

        private void kitaLizdas_sav_Checked(object sender, RoutedEventArgs e)
        {
            lizdas.IsEnabled = true;
        }
    }
}
