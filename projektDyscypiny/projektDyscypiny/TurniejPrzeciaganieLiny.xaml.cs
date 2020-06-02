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

namespace projektDyscypiny
{
    /// <summary>
    /// Interaction logic for TurniejPrzeciaganieLiny.xaml
    /// </summary>
    public partial class TurniejPrzeciaganieLiny : Page
    {
        private static string status = "ELIMINACJE";
        private static int numerMeczu = 0;
        private static int numerDogrywki = 0;
        private static int ilosc = 1; //ilosc druzyn wchodzacych do polfinalow z dogrywki
        private static int nrPolfinalu = 0;
        private static Druzyna druzynaPolfinalA = new Druzyna();
        private static Druzyna druzynaPolfinalB = new Druzyna();
        private static Druzyna wygranaDruzyna = new Druzyna();
        private Mecz dogrywkowyMecz = new Mecz();
        public static List<Druzyna> dogrywkaDruzyny = new List<Druzyna>();
        public static List<Mecz> dogrywkaMecze = new List<Mecz>();
        public static List<Druzyna> polfinalyDruzyny = new List<Druzyna>();
        // List<Druzyna> kopiaDruzyna = PrzeciaganieLiny.listaDruzyna;
        Random random = new Random();
        public TurniejPrzeciaganieLiny()
        {
            InitializeComponent();

            statusLabel.Content = "Aktualny status: " + status;
            statusLabel.FontSize = 17;
            if (PrzeciaganieLiny.listaMeczow.Count > numerMeczu && status == "ELIMINACJE")
            //status == "ELIMINACJE"
            {
                DruzynaALabel.Content = PrzeciaganieLiny.listaMeczow[numerMeczu].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = PrzeciaganieLiny.listaMeczow[numerMeczu].getDruzynaB().getNazwaDruzyny();
                WyswietlRankingStackPanel(PrzeciaganieLiny.listaDruzyna);
            }
            if (status == "DOGRYWKA")
            {
                DruzynaALabel.Content = dogrywkaMecze[numerDogrywki].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = dogrywkaMecze[numerDogrywki].getDruzynaB().getNazwaDruzyny();
                //numerDogrywki++; 
                WyswietlRankingStackPanel(PrzeciaganieLiny.listaDruzyna);
            }
            if (status == "PÓŁFINAŁY")
            {
                DruzynaALabel.Content = PrzeciaganieLiny.listaMeczow[numerMeczu + 1].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = PrzeciaganieLiny.listaMeczow[numerMeczu + 1].getDruzynaB().getNazwaDruzyny();
                // numerMeczu++;
            }
            if (status == "FINAŁ")
            {
                DruzynaALabel.Content = druzynaPolfinalA.getNazwaDruzyny();
                DruzynaBLabel.Content = druzynaPolfinalB.getNazwaDruzyny();
            }

            DruzynaALabel.FontSize = 14;
            DruzynaBLabel.FontSize = 14;
            // WyswietlRankingStackPanel(PrzeciaganieLiny.listaDruzyna); // wrzuciłyśmy do ifa 
            WypisywanieMeczowStackPanel();
        }

        private void WynikiTurniejuClick(object sender, RoutedEventArgs e)
        {

            /*if (numerMeczu < PrzeciaganieLiny.listaMeczow.Count && status == "ELIMINACJE")
            {
                wpiszWyniki();
                numerMeczu++;
            } */
            /* if (numerMeczu == PrzeciaganieLiny.listaMeczow.Count-1 && status == "ELIMINACJE") //dla eliminacji
             {
                 dogrywka();
             } */

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


            else if (numerMeczu < PrzeciaganieLiny.listaMeczow.Count && status == "ELIMINACJE")//if (status == "ELIMINACJE")
            {
                wpiszWyniki();
                if (numerMeczu == PrzeciaganieLiny.listaMeczow.Count - 1) //dla eliminacji
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
                wpiszWynikiPolfinal();
                numerMeczu++;
            }
            else if (status == "FINAŁ")
            {
                rozegrajFinal();
            }
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new TurniejPrzeciaganieLiny();
            //((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiPrzeciaganieLiny();
        }

        private void wpiszWyniki()
        {

            if (DruzynaARadioButton.IsChecked == true)
            {
                foreach (Druzyna druzyna in PrzeciaganieLiny.listaDruzyna)
                {
                    if (PrzeciaganieLiny.listaMeczow[numerMeczu].getDruzynaA().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                    {
                        druzyna.iloscWygranych();
                    }
                }
                PrzeciaganieLiny.listaMeczow[numerMeczu].wygranaDruzynyA(); //wynik meczu np. 1:0
            }
            else
            {
                foreach (Druzyna druzyna in PrzeciaganieLiny.listaDruzyna)
                {
                    if (PrzeciaganieLiny.listaMeczow[numerMeczu].getDruzynaB().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                    {
                        druzyna.iloscWygranych();
                    }
                }
                PrzeciaganieLiny.listaMeczow[numerMeczu].wygranaDruzynyB();
            }

            File.WriteAllText("PrzeciaganieLinyMeczeDane.txt", string.Empty);
            using (StreamWriter streamW = new StreamWriter(("PrzeciaganieLinyMeczeDane.txt"), true))
                foreach (Mecz mecz in PrzeciaganieLiny.listaMeczow)
                {
                    streamW.WriteLine(mecz.getDruzynaA().getNazwaDruzyny() + ";" + mecz.getDruzynaB().getNazwaDruzyny() + ";" + mecz.getSedzia().getImie_Sedzia() + ";" + mecz.getSedzia().getNazwisko_Sedzia() + ";" + mecz.getPunkty_Dr1() + ";" + mecz.getPunkty_Dr2());
                }
        }

        private void wpiszWynikiPolfinal()
        {
            stworzRanking(polfinalyDruzyny);
            if (DruzynaARadioButton.IsChecked == true)
            {
                MessageBox.Show("D " + polfinalyDruzyny[0].getNazwaDruzyny() + " i " + polfinalyDruzyny[1].getNazwaDruzyny() + polfinalyDruzyny[2].getNazwaDruzyny() + polfinalyDruzyny[3].getNazwaDruzyny());
                if (nrPolfinalu == 0)
                {
                    druzynaPolfinalA = polfinalyDruzyny[0];
                    nrPolfinalu++;
                    MessageBox.Show("Pierwsza grupa z półfinałów: " + druzynaPolfinalA.getNazwaDruzyny());
                }
                else
                {
                    druzynaPolfinalB = polfinalyDruzyny[2];
                    MessageBox.Show("Do finału przechodzą: " + druzynaPolfinalA.getNazwaDruzyny() + " i " + druzynaPolfinalB.getNazwaDruzyny());
                    finaly();
                }
            }
            else
            {
                if (nrPolfinalu == 0)
                {
                    druzynaPolfinalA = polfinalyDruzyny[1];
                    nrPolfinalu++;
                }
                else
                {
                    druzynaPolfinalB = polfinalyDruzyny[3];
                    MessageBox.Show("Do finału przechodzą:" + druzynaPolfinalA.getNazwaDruzyny() + " i " + druzynaPolfinalB.getNazwaDruzyny());
                    finaly();
                }
            }
        }
        private void rozegrajFinal()
        {

            if (DruzynaARadioButton.IsChecked == true)
            {
                wygranaDruzyna = druzynaPolfinalA;
                MessageBox.Show("Wygrała drużyna: " + wygranaDruzyna);
            }
            else
            {
                wygranaDruzyna = druzynaPolfinalB;
                MessageBox.Show("Wygrała drużyna: " + wygranaDruzyna);
            }
        }

        private void stworzRanking(List<Druzyna> druzyny)
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
        private void ostatecznyRanking()
        {

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
            foreach (Mecz mecz in PrzeciaganieLiny.listaMeczow)
            {
                Label label = new Label();
                label.Content = mecz.getDruzynaA().getNazwaDruzyny() + " VS " + mecz.getDruzynaB().getNazwaDruzyny() + " " + mecz.getPunkty_Dr1() + ":" + mecz.getPunkty_Dr2() + " (sędzia: " + mecz.getSedzia().getImie_Sedzia() + " " + mecz.getSedzia().getNazwisko_Sedzia() + ")";

                wypisywanieMeczowStackPanel.Children.Add(label);
            }
        }


        private void dogrywka()
        {
            stworzRanking(PrzeciaganieLiny.listaDruzyna);
            for (int i = 0; i < PrzeciaganieLiny.listaDruzyna.Count; i++)//szukanie druzyn z tą samą ilości pkt co czwarta
            {
                if (PrzeciaganieLiny.listaDruzyna[i].getWygrane() == PrzeciaganieLiny.listaDruzyna[3].getWygrane())
                {
                    dogrywkaDruzyny.Add(PrzeciaganieLiny.listaDruzyna[i]);
                    MessageBox.Show("dodajemy do dogrywki");
                }
            }
            if (PrzeciaganieLiny.listaDruzyna.Count == 4) //przypadek gdy nie trzeba robic dogrywek, cztery pierwsze druzyny wchodza do polfinalow
            {
                for (int i = 0; i < 4; i++)
                {
                    polfinalyDruzyny.Add(PrzeciaganieLiny.listaDruzyna[i]);
                }
                polfinaly(); //przechodzimy do półfinałów bo mamy tylko 4 drużyny i wszystkie od razu przechodzą 
            }
            else if (PrzeciaganieLiny.listaDruzyna[3].getWygrane() == PrzeciaganieLiny.listaDruzyna[4].getWygrane()) //przypadek gdy trzeba zrobic dogrywki
            {
                status = "DOGRYWKA";
                for (int i = 0; i < 3; i++)
                {
                    if (PrzeciaganieLiny.listaDruzyna[i].getWygrane() == PrzeciaganieLiny.listaDruzyna[3].getWygrane())
                    { ilosc++; MessageBox.Show("zwiekszamy ilosc"); }
                    else
                    { polfinalyDruzyny.Add(PrzeciaganieLiny.listaDruzyna[i]); MessageBox.Show("dodajemy do polfinalow"); }
                }
                for (int i = 0; i < dogrywkaDruzyny.Count - 1; i++)
                {
                    for (int j = i + 1; j < dogrywkaDruzyny.Count; j++)
                    {
                        int indexSedziego = random.Next(PrzeciaganieLiny.listaSedziow.Count - 1); //losowanie indexu sedziego
                        dogrywkowyMecz = new Mecz(dogrywkaDruzyny[i], dogrywkaDruzyny[j], PrzeciaganieLiny.listaSedziow[indexSedziego]);
                        dogrywkaMecze.Add(dogrywkowyMecz);
                        MessageBox.Show("tworzymy mecz dogrywkowy");
                        MessageBox.Show("mmkkk  " + dogrywkaMecze.Count);
                    }
                }
                MessageBox.Show("Trzeba przeprowadzić dogrywki");
            }
            else //przypadek gdy nie trzeba robic dogrywek, cztery pierwsze druzyny wchodza do polfinalow
            {
                //dogrywkaDruzyny.Clear();
                for (int i = 0; i < 4; i++)
                {
                    polfinalyDruzyny.Add(PrzeciaganieLiny.listaDruzyna[i]);
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
                int indexSedziego = random.Next(PrzeciaganieLiny.listaSedziow.Count - 1);
                PrzeciaganieLiny.listaMeczow.Add(new Mecz(polfinalyDruzyny[i], polfinalyDruzyny[j], PrzeciaganieLiny.listaSedziow[indexSedziego]));
            }
        }

        private void finaly()
        {
            status = "FINAŁ";
            int indexSedziego = random.Next(PrzeciaganieLiny.listaSedziow.Count - 1);
            PrzeciaganieLiny.listaMeczow.Add(new Mecz(druzynaPolfinalA, druzynaPolfinalB, PrzeciaganieLiny.listaSedziow[indexSedziego]));
        }
        private void wpiszWyniki_Dogrywka()
        {

            if (DruzynaARadioButton.IsChecked == true)
            {
                MessageBox.Show("mm" + numerDogrywki + "  " + dogrywkaMecze.Count);
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
