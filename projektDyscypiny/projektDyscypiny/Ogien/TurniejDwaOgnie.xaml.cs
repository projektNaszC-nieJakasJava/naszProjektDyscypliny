using System;
using System.Collections.Generic;
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

namespace projektDyscypiny.Ogien
{
    /// <summary>
    /// Interaction logic for TurniejDwaOgnie.xaml
    /// </summary>
    public partial class TurniejDwaOgnie : Page
    {
        public static string status = "ELIMINACJE";
        public static int numerMeczu = 0;
        public static int numerDogrywki = 0;
        private static int ilosc = 1; //ilosc druzyn wchodzacych do polfinalow z dogrywki
        public static int nrPolfinalu = 0;
        public static Druzyna druzynaPolfinalA = new Druzyna();
        public static Druzyna druzynaPolfinalB = new Druzyna();
        public static Druzyna wygranaDruzyna = new Druzyna();
        private Mecz dogrywkowyMecz = new Mecz();
        public static List<Druzyna> dogrywkaDruzyny = new List<Druzyna>();
        public static List<Mecz> dogrywkaMecze = new List<Mecz>();
        public static List<Druzyna> polfinalyDruzyny = new List<Druzyna>();
        readonly Random random = new Random();
        public TurniejDwaOgnie()
        {
            InitializeComponent();

            statusLabel.Content = "Aktualny status: " + status;
            statusLabel.FontSize = 17;
            if (DwaOgnie.listaMeczow.Count > numerMeczu && status == "ELIMINACJE")
            //status == "ELIMINACJE"
            {
                DruzynaALabel.Content = DwaOgnie.listaMeczow[numerMeczu].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = DwaOgnie.listaMeczow[numerMeczu].getDruzynaB().getNazwaDruzyny();
                WyswietlRankingStackPanel(DwaOgnie.listaDruzyna);
            }
            if (status == "DOGRYWKA")
            {
                DruzynaALabel.Content = dogrywkaMecze[numerDogrywki].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = dogrywkaMecze[numerDogrywki].getDruzynaB().getNazwaDruzyny();
                WyswietlRankingStackPanel(DwaOgnie.listaDruzyna);
            }
            if (status == "PÓŁFINAŁY")
            {
                DruzynaALabel.Content = DwaOgnie.listaMeczow[numerMeczu + 1].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = DwaOgnie.listaMeczow[numerMeczu + 1].getDruzynaB().getNazwaDruzyny();
            }
            if (status == "FINAŁ")
            {
                DruzynaALabel.Content = druzynaPolfinalA.getNazwaDruzyny();
                DruzynaBLabel.Content = druzynaPolfinalB.getNazwaDruzyny();
            }

            DruzynaALabel.FontSize = 14;
            DruzynaBLabel.FontSize = 14;
            WypisywanieMeczowStackPanel();
        }

        private void WynikiTurniejuClick(object sender, RoutedEventArgs e)
        {
            if (status == "DOGRYWKA")
            {
                if (numerDogrywki == dogrywkaMecze.Count - 1)
                {
                    stworzRanking(dogrywkaDruzyny);
                    for (int i = 0; i < ilosc; i++)
                    {
                        polfinalyDruzyny.Add(dogrywkaDruzyny[i]);
                    }
                    polfinaly(); //przechodzimy do półfinałów po zakończeniu dogrywki
                }
                else
                {
                    wpiszWyniki_Dogrywka();
                    numerDogrywki++;
                }
            }
            else if (numerMeczu < DwaOgnie.listaMeczow.Count && status == "ELIMINACJE")
            {
                wpiszWyniki();
                if (numerMeczu == DwaOgnie.listaMeczow.Count - 1) //dla eliminacji
                {
                    dogrywka();
                }
                else
                {
                    numerMeczu++;
                }
            }
            else if (status == "PÓŁFINAŁY")
            {
                numerMeczu++;
                wpiszWynikiPolfinal();
            }
            else if (status == "FINAŁ")
            {
                numerMeczu++;
                rozegrajFinal();
                status = "KONIEC";
                ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiDwaOgnie();
            }
            if (status != "KONIEC")
                ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new TurniejDwaOgnie();
        }

        private void wpiszWyniki()
        {

            if (DruzynaARadioButton.IsChecked == true)
            {
                foreach (Druzyna druzyna in DwaOgnie.listaDruzyna)
                {
                    if (DwaOgnie.listaMeczow[numerMeczu].getDruzynaA().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                    {
                        druzyna.iloscWygranych();
                    }
                }
                DwaOgnie.listaMeczow[numerMeczu].wygranaDruzynyA(); //wynik meczu np. 1:0
            }
            else
            {
                foreach (Druzyna druzyna in DwaOgnie.listaDruzyna)
                {
                    if (DwaOgnie.listaMeczow[numerMeczu].getDruzynaB().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                    {
                        druzyna.iloscWygranych();
                    }
                }
                DwaOgnie.listaMeczow[numerMeczu].wygranaDruzynyB();
            }

         
        }

        private void wpiszWynikiPolfinal()
        {
            stworzRanking(polfinalyDruzyny);
            if (DruzynaARadioButton.IsChecked == true)
            {
                if (nrPolfinalu == 0)
                {
                    druzynaPolfinalA = polfinalyDruzyny[0];
                    nrPolfinalu++;
                    DwaOgnie.listaMeczow[numerMeczu].wygranaDruzynyA();
                }
                else
                {
                    druzynaPolfinalB = polfinalyDruzyny[2];
                    MessageBox.Show("Do finału przechodzą: " + druzynaPolfinalA.getNazwaDruzyny() + " i " + druzynaPolfinalB.getNazwaDruzyny());
                    DwaOgnie.listaMeczow[numerMeczu].wygranaDruzynyA();
                    finaly();
                }
            }
            else
            {
                if (nrPolfinalu == 0)
                {
                    druzynaPolfinalA = polfinalyDruzyny[1];
                    nrPolfinalu++;
                    DwaOgnie.listaMeczow[numerMeczu].wygranaDruzynyB();
                }
                else
                {
                    druzynaPolfinalB = polfinalyDruzyny[3];
                    MessageBox.Show("Do finału przechodzą:" + druzynaPolfinalA.getNazwaDruzyny() + " i " + druzynaPolfinalB.getNazwaDruzyny());
                    DwaOgnie.listaMeczow[numerMeczu].wygranaDruzynyB();
                    finaly();
                }
            }
        }
        private void rozegrajFinal()
        {
            if (DruzynaARadioButton.IsChecked == true)
            {
                wygranaDruzyna = druzynaPolfinalA;
                druzynaPolfinalA = druzynaPolfinalB;
                DwaOgnie.listaMeczow[numerMeczu].wygranaDruzynyA();
                ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiDwaOgnie();
            }
            else
            {
                wygranaDruzyna = druzynaPolfinalB;
                DwaOgnie.listaMeczow[numerMeczu].wygranaDruzynyB();
                ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiDwaOgnie();
            }
        }

        protected void stworzRanking(List<Druzyna> druzyny)
        {
            Druzyna druzynaPom = new Druzyna();
            for (int i = 0; i < druzyny.Count - 1; i++)
            {
                if (druzyny[i].getWygrane() < druzyny[i + 1].getWygrane())
                {
                    druzynaPom = druzyny[i];
                    druzyny[i] = druzyny[i + 1];
                    druzyny[i + 1] = druzynaPom;
                }
            }
        }

        private void WyswietlRankingStackPanel(List<Druzyna> druzyny)
        {
            stworzRanking(druzyny);
            foreach (Druzyna druzyna in druzyny)
            {
                Label label = new Label();
                label.Content = druzyna.getNazwaDruzyny() + " wygrane " + druzyna.getWygrane();
                label.Tag = druzyna.getID_Druzyna();
                rankingStackPanel.Children.Add(label);
            }
        }

        private void WypisywanieMeczowStackPanel()
        {
            foreach (Mecz mecz in DwaOgnie.listaMeczow)
            {
                Label label = new Label();
                label.Content = mecz.getDruzynaA().getNazwaDruzyny() + " VS " + mecz.getDruzynaB().getNazwaDruzyny() + " " + mecz.getPunkty_Dr1() + ":" + mecz.getPunkty_Dr2() + " (sędzia: " + mecz.getSedzia().getImie_Sedzia() + " " + mecz.getSedzia().getNazwisko_Sedzia() + ")";

                wypisywanieMeczowStackPanel.Children.Add(label);
            }
        }


        private void dogrywka()
        {
            stworzRanking(DwaOgnie.listaDruzyna);
            for (int i = 0; i < DwaOgnie.listaDruzyna.Count; i++)//szukanie druzyn z tą samą ilości pkt co czwarta
            {
                if (DwaOgnie.listaDruzyna[i].getWygrane() == DwaOgnie.listaDruzyna[3].getWygrane())
                {
                    dogrywkaDruzyny.Add(DwaOgnie.listaDruzyna[i]);
                }
            }
            if (DwaOgnie.listaDruzyna.Count == 4) //przypadek gdy nie trzeba robic dogrywek, cztery pierwsze druzyny wchodza do polfinalow
            {
                for (int i = 0; i < 4; i++)
                {
                    polfinalyDruzyny.Add(DwaOgnie.listaDruzyna[i]);
                }
                polfinaly(); //przechodzimy do półfinałów bo mamy tylko 4 drużyny i wszystkie od razu przechodzą 
            }
            else if (DwaOgnie.listaDruzyna[3].getWygrane() == DwaOgnie.listaDruzyna[4].getWygrane()) //przypadek gdy trzeba zrobic dogrywki
            {
                status = "DOGRYWKA";
                for (int i = 0; i < 3; i++)
                {
                    if (DwaOgnie.listaDruzyna[i].getWygrane() == DwaOgnie.listaDruzyna[3].getWygrane())
                        ilosc++;
                    else
                        polfinalyDruzyny.Add(DwaOgnie.listaDruzyna[i]);
                }
                for (int i = 0; i < dogrywkaDruzyny.Count - 1; i++)
                {
                    for (int j = i + 1; j < dogrywkaDruzyny.Count; j++)
                    {
                        int indexSedziego = random.Next(DwaOgnie.listaSedziow.Count); //losowanie indexu sedziego
                        dogrywkowyMecz = new Mecz(dogrywkaDruzyny[i], dogrywkaDruzyny[j], DwaOgnie.listaSedziow[indexSedziego]);
                        dogrywkaMecze.Add(dogrywkowyMecz);
                    }
                }
            }
            else //przypadek gdy nie trzeba robic dogrywek, cztery pierwsze druzyny wchodza do polfinalow
            {
                for (int i = 0; i < 4; i++)
                {
                    polfinalyDruzyny.Add(DwaOgnie.listaDruzyna[i]);
                }
                polfinaly();
            }

        }

        private void polfinaly()
        {
            //dodanie do listy wygranych druzyn z dogrywki o ile byla
            status = "PÓŁFINAŁY";
            for (int i = 0, j = 1; i < 3; i += 2, j += 2) //tworzymy mecz dla 1 i 2 drużyny wg rankingu, a potem dla 3 i 4
            {
                int indexSedziego = random.Next(DwaOgnie.listaSedziow.Count);
                DwaOgnie.listaMeczow.Add(new Mecz(polfinalyDruzyny[i], polfinalyDruzyny[j], DwaOgnie.listaSedziow[indexSedziego]));
            }
        }

        private void finaly()
        {
            status = "FINAŁ";
            int indexSedziego = random.Next(DwaOgnie.listaSedziow.Count);
            DwaOgnie.listaMeczow.Add(new Mecz(druzynaPolfinalA, druzynaPolfinalB, DwaOgnie.listaSedziow[indexSedziego]));
        }
        private void wpiszWyniki_Dogrywka()
        {

            if (DruzynaARadioButton.IsChecked == true)
            {
                foreach (Druzyna druzyna in dogrywkaDruzyny)
                {
                    if (dogrywkaMecze[numerDogrywki].getDruzynaA().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                    {
                        druzyna.punktyDogrywka();
                    }
                }
            }
            else
            {
                foreach (Druzyna druzyna in dogrywkaDruzyny)
                {
                    if (dogrywkaMecze[numerDogrywki].getDruzynaB().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                    {
                        druzyna.punktyDogrywka();
                    }
                }
            }

        }
    }
}
