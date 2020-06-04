using System;
using System.Collections.Generic;
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

namespace projektDyscypiny.Siatka
{
    /// <summary>
    /// Interaction logic for TurniejSiatkowka.xaml
    /// </summary>
    public partial class TurniejSiatkowka : Page
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
        readonly Random random1 = new Random();
        readonly Random random2 = new Random();

        public TurniejSiatkowka()
        {



            InitializeComponent();

            



            statusLabel.Content = "Aktualny status: " + status;
            statusLabel.FontSize = 17;
            if (Siatkowka.listaMeczow.Count > numerMeczu && status == "ELIMINACJE")
            //status == "ELIMINACJE"
            {
                DruzynaALabel.Content = Siatkowka.listaMeczow[numerMeczu].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = Siatkowka.listaMeczow[numerMeczu].getDruzynaB().getNazwaDruzyny();
                DruzynaALabel1.Content = DruzynaALabel.Content;
                DruzynaALabel2.Content = DruzynaALabel.Content;
                DruzynaBLabel1.Content = DruzynaBLabel.Content;
                DruzynaBLabel2.Content = DruzynaBLabel.Content;
                WyswietlRankingStackPanel(Siatkowka.listaDruzyna);
            }
            if (status == "DOGRYWKA")
            {
                DruzynaALabel.Content = dogrywkaMecze[numerDogrywki].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = dogrywkaMecze[numerDogrywki].getDruzynaB().getNazwaDruzyny();
                DruzynaALabel1.Content = DruzynaALabel.Content;
                DruzynaALabel2.Content = DruzynaALabel.Content;
                DruzynaBLabel1.Content = DruzynaBLabel.Content;
                DruzynaBLabel2.Content = DruzynaBLabel.Content;
                WyswietlRankingStackPanel(Siatkowka.listaDruzyna);
            }
            if (status == "PÓŁFINAŁY")
            {
                DruzynaALabel.Content = Siatkowka.listaMeczow[numerMeczu + 1].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = Siatkowka.listaMeczow[numerMeczu + 1].getDruzynaB().getNazwaDruzyny();
                DruzynaALabel1.Content = DruzynaALabel.Content;
                DruzynaALabel2.Content = DruzynaALabel.Content;
                DruzynaBLabel1.Content = DruzynaBLabel.Content;
                DruzynaBLabel2.Content = DruzynaBLabel.Content;
            }
            if (status == "FINAŁ")
            {
                DruzynaALabel.Content = druzynaPolfinalA.getNazwaDruzyny();
                DruzynaBLabel.Content = druzynaPolfinalB.getNazwaDruzyny();
                DruzynaALabel1.Content = DruzynaALabel.Content;
                DruzynaALabel2.Content = DruzynaALabel.Content;
                DruzynaBLabel1.Content = DruzynaBLabel.Content;
                DruzynaBLabel2.Content = DruzynaBLabel.Content;
            }

            DruzynaALabel.FontSize = 14;
            DruzynaBLabel.FontSize = 14;
            DruzynaALabel1.FontSize = 14;
            DruzynaBLabel1.FontSize = 14;
            DruzynaALabel2.FontSize = 14;
            DruzynaBLabel2.FontSize = 14;
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
            else if (numerMeczu < Siatkowka.listaMeczow.Count && status == "ELIMINACJE")
            {
                wpiszWyniki();
                if (numerMeczu == Siatkowka.listaMeczow.Count - 1) //dla eliminacji
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
                ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiSiatkowka();
            }
            if (status != "KONIEC")
                ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new TurniejSiatkowka();
        }


        private void wpiszWyniki()
        {

            if (((Set2Druzyna1.Text != "" && Set2Druzyna2.Text != "") && (Set2Druzyna1.Background == Set2Druzyna2.Background) && set2.IsEnabled == true) || ((Set3Druzyna1.Text != "" && Set3Druzyna2.Text != "") && (Set3Druzyna1.Background == Set3Druzyna2.Background) && set3.IsEnabled == true))
            {


                if ((Int32.Parse(Set2Druzyna1.Text) > Int32.Parse(Set2Druzyna2.Text)) && set3.IsEnabled == false)
                {
                    foreach (Druzyna druzyna in Siatkowka.listaDruzyna)
                    {
                        if (Siatkowka.listaMeczow[numerMeczu].getDruzynaA().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                        {
                            druzyna.iloscWygranych();
                        }
                    }
                    Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyA(); //wynik meczu np. 1:0
                }
                else if ((Int32.Parse(Set2Druzyna1.Text) < Int32.Parse(Set2Druzyna2.Text)) && set3.IsEnabled == false)
                {
                    foreach (Druzyna druzyna in Siatkowka.listaDruzyna)
                    {
                        if (Siatkowka.listaMeczow[numerMeczu].getDruzynaB().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                        {
                            druzyna.iloscWygranych();
                        }
                    }
                    Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyB();
                }
                else if (Set3Druzyna1.Text == "" || Set3Druzyna2.Text == "")
                {
                    MessageBox.Show("Błąd, wprowadz dane jeszcze raz zgodnie z zasadami");
                    numerMeczu--;
                }
                else if (Int32.Parse(Set3Druzyna1.Text) > Int32.Parse(Set3Druzyna2.Text))
                {
                    foreach (Druzyna druzyna in Siatkowka.listaDruzyna)
                    {
                        if (Siatkowka.listaMeczow[numerMeczu].getDruzynaA().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                        {
                            druzyna.iloscWygranych();
                        }
                    }
                    Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyA(); //wynik meczu np. 1:0
                }
                else
                {
                    foreach (Druzyna druzyna in Siatkowka.listaDruzyna)
                    {
                        if (Siatkowka.listaMeczow[numerMeczu].getDruzynaB().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                        {
                            druzyna.iloscWygranych();
                        }
                    }
                    Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyB();
                }

            }
            else
            {
                MessageBox.Show("Błąd, wprowadz dane jeszcze raz zgodnie z zasadami");
                numerMeczu--;
            }

        }


        private void wpiszWynikiPolfinal()
        {
            stworzRanking(polfinalyDruzyny);

            if (((Set2Druzyna1.Text != "" && Set2Druzyna2.Text != "") && (Set2Druzyna1.Background == Set2Druzyna2.Background) && set2.IsEnabled == true) || ((Set3Druzyna1.Text != "" && Set3Druzyna2.Text != "") && (Set3Druzyna1.Background == Set3Druzyna2.Background) && set3.IsEnabled == true))
            {
                if ((Int32.Parse(Set2Druzyna1.Text) > Int32.Parse(Set2Druzyna2.Text)) && set3.IsEnabled == false)
                {
                    if (nrPolfinalu == 0)
                    {
                        druzynaPolfinalA = polfinalyDruzyny[0];
                        nrPolfinalu++;
                        Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyA();
                    }
                    else
                    {
                        druzynaPolfinalB = polfinalyDruzyny[2];
                        MessageBox.Show("Do finału przechodzą: " + druzynaPolfinalA.getNazwaDruzyny() + " i " + druzynaPolfinalB.getNazwaDruzyny());
                        Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyA();
                        finaly();
                    }
                }
                else if ((Int32.Parse(Set2Druzyna1.Text) < Int32.Parse(Set2Druzyna2.Text)) && set3.IsEnabled == false)
                {
                    if (nrPolfinalu == 0)
                    {
                        druzynaPolfinalA = polfinalyDruzyny[1];
                        nrPolfinalu++;
                        Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyB();
                    }
                    else
                    {
                        druzynaPolfinalB = polfinalyDruzyny[3];
                        MessageBox.Show("Do finału przechodzą:" + druzynaPolfinalA.getNazwaDruzyny() + " i " + druzynaPolfinalB.getNazwaDruzyny());
                        Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyB();
                        finaly();
                    }
                }
                else if (Set3Druzyna1.Text == "" || Set3Druzyna2.Text == "")
                {
                    MessageBox.Show("Błąd, wprowadz dane jeszcze raz zgodnie z zasadami");
                    numerMeczu--;
                }
                else if (Int32.Parse(Set3Druzyna1.Text) > Int32.Parse(Set3Druzyna2.Text))
                {
                    if (nrPolfinalu == 0)
                    {
                        druzynaPolfinalA = polfinalyDruzyny[0];
                        nrPolfinalu++;
                        Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyA();
                    }
                    else
                    {
                        druzynaPolfinalB = polfinalyDruzyny[2];
                        MessageBox.Show("Do finału przechodzą: " + druzynaPolfinalA.getNazwaDruzyny() + " i " + druzynaPolfinalB.getNazwaDruzyny());
                        Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyA();
                        finaly();
                    }
                }
                else
                {
                    if (nrPolfinalu == 0)
                    {
                        druzynaPolfinalA = polfinalyDruzyny[1];
                        nrPolfinalu++;
                        Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyB();
                    }
                    else
                    {
                        druzynaPolfinalB = polfinalyDruzyny[3];
                        MessageBox.Show("Do finału przechodzą:" + druzynaPolfinalA.getNazwaDruzyny() + " i " + druzynaPolfinalB.getNazwaDruzyny());
                        Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyB();
                        finaly();
                    }
                }


            }
            else
            {
                MessageBox.Show("Błąd, wprowadz dane jeszcze raz zgodnie z zasadami");
                numerMeczu--;


            }
        }

        
        private void rozegrajFinal()
        {
            if (((Set2Druzyna1.Text != "" && Set2Druzyna2.Text != "") && (Set2Druzyna1.Background == Set2Druzyna2.Background) && set2.IsEnabled == true) || ((Set3Druzyna1.Text != "" && Set3Druzyna2.Text != "") && (Set3Druzyna1.Background == Set3Druzyna2.Background) && set3.IsEnabled == true))
            {
                if ((Int32.Parse(Set2Druzyna1.Text) > Int32.Parse(Set2Druzyna2.Text)) && set3.IsEnabled == false)
                {
                    wygranaDruzyna = druzynaPolfinalA;
                    druzynaPolfinalA = druzynaPolfinalB;
                    Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyA();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiSiatkowka();
                }
                else if ((Int32.Parse(Set2Druzyna1.Text) < Int32.Parse(Set2Druzyna2.Text)) && set3.IsEnabled == false)
                {
                    wygranaDruzyna = druzynaPolfinalB;
                    Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyB();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiSiatkowka();

                }

                else if (Set3Druzyna1.Text == "" || Set3Druzyna2.Text == "")
                {
                    MessageBox.Show("Błąd, wprowadz dane jeszcze raz zgodnie z zasadami");
                    numerMeczu--;
                }

                else if (Int32.Parse(Set3Druzyna1.Text) > Int32.Parse(Set3Druzyna2.Text))
                {
                    wygranaDruzyna = druzynaPolfinalA;
                    druzynaPolfinalA = druzynaPolfinalB;
                    Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyA();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiSiatkowka();
                }
                else
                {
                    wygranaDruzyna = druzynaPolfinalB;
                    Siatkowka.listaMeczow[numerMeczu].wygranaDruzynyB();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiSiatkowka();
                }



            }
            else
            {
                MessageBox.Show("Błąd, wprowadz dane jeszcze raz zgodnie z zasadami");
                numerMeczu--;


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
            foreach (Mecz mecz in Siatkowka.listaMeczow)
            {
                Label label = new Label();
               
                label.Content = mecz.getDruzynaA().getNazwaDruzyny() + " VS " + mecz.getDruzynaB().getNazwaDruzyny() + " " + mecz.getPunkty_Dr1() + ":" + mecz.getPunkty_Dr2() + " (Glowny sędzia: " + mecz.getSedzia().getImie_Sedzia() + " " + mecz.getSedzia().getNazwisko_Sedzia() + ")" ;

                wypisywanieMeczowStackPanel.Children.Add(label);
            }
        }



        private void dogrywka()
        {
            stworzRanking(Siatkowka.listaDruzyna);
            for (int i = 0; i < Siatkowka.listaDruzyna.Count; i++)//szukanie druzyn z tą samą ilości pkt co czwarta
            {
                if (Siatkowka.listaDruzyna[i].getWygrane() == Siatkowka.listaDruzyna[3].getWygrane())
                {
                    dogrywkaDruzyny.Add(Siatkowka.listaDruzyna[i]);
                }
            }
            if (Siatkowka.listaDruzyna.Count == 4) //przypadek gdy nie trzeba robic dogrywek, cztery pierwsze druzyny wchodza do polfinalow
            {
                for (int i = 0; i < 4; i++)
                {
                    polfinalyDruzyny.Add(Siatkowka.listaDruzyna[i]);
                }
                polfinaly(); //przechodzimy do półfinałów bo mamy tylko 4 drużyny i wszystkie od razu przechodzą 
            }
            else if (Siatkowka.listaDruzyna[3].getWygrane() == Siatkowka.listaDruzyna[4].getWygrane()) //przypadek gdy trzeba zrobic dogrywki
            {
                status = "DOGRYWKA";
                for (int i = 0; i < 3; i++)
                {
                    if (Siatkowka.listaDruzyna[i].getWygrane() == Siatkowka.listaDruzyna[3].getWygrane())
                        ilosc++;
                    else
                        polfinalyDruzyny.Add(Siatkowka.listaDruzyna[i]);
                }
                for (int i = 0; i < dogrywkaDruzyny.Count - 1; i++)
                {
                    for (int j = i + 1; j < dogrywkaDruzyny.Count; j++)
                    {
                        int indexSedziego = random.Next(Siatkowka.listaSedziow.Count); //losowanie indexu sedziego
                        int indexSedziego1 = random1.Next(Siatkowka.listaSedziow.Count); //losowanie indexu sedziego
                        int indexSedziego2 = random2.Next(Siatkowka.listaSedziow.Count); //losowanie indexu sedziego
                        dogrywkowyMecz = new Mecz(dogrywkaDruzyny[i], dogrywkaDruzyny[j], Siatkowka.listaSedziow[indexSedziego], Siatkowka.listaSedziow[indexSedziego1], Siatkowka.listaSedziow[indexSedziego2]);
                        dogrywkaMecze.Add(dogrywkowyMecz);
                    }
                }
            }
            else //przypadek gdy nie trzeba robic dogrywek, cztery pierwsze druzyny wchodza do polfinalow
            {
                for (int i = 0; i < 4; i++)
                {
                    polfinalyDruzyny.Add(Siatkowka.listaDruzyna[i]);
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
                int indexSedziego = random.Next(Siatkowka.listaSedziow.Count);
                int indexSedziego1 = random1.Next(Siatkowka.listaSedziow.Count);
                int indexSedziego2 = random2.Next(Siatkowka.listaSedziow.Count);
                Siatkowka.listaMeczow.Add(new Mecz(polfinalyDruzyny[i], polfinalyDruzyny[j], Siatkowka.listaSedziow[indexSedziego], Siatkowka.listaSedziow[indexSedziego1], Siatkowka.listaSedziow[indexSedziego2]));
            }
        }


        private void finaly()
        {
            status = "FINAŁ";
            int indexSedziego = random.Next(Siatkowka.listaSedziow.Count);
            int indexSedziego1 = random1.Next(Siatkowka.listaSedziow.Count);
            int indexSedziego2 = random2.Next(Siatkowka.listaSedziow.Count);
            Siatkowka.listaMeczow.Add(new Mecz(druzynaPolfinalA, druzynaPolfinalB, Siatkowka.listaSedziow[indexSedziego], Siatkowka.listaSedziow[indexSedziego1], Siatkowka.listaSedziow[indexSedziego2]));
        }



        private void wpiszWyniki_Dogrywka()
        {

            if (((Set2Druzyna1.Text != "" && Set2Druzyna2.Text != "") && (Set2Druzyna1.Background == Set2Druzyna2.Background) && set2.IsEnabled == true) || ((Set3Druzyna1.Text != "" && Set3Druzyna2.Text != "") && (Set3Druzyna1.Background == Set3Druzyna2.Background) && set3.IsEnabled == true))
            {
                if ((Int32.Parse(Set2Druzyna1.Text) > Int32.Parse(Set2Druzyna2.Text)) && set3.IsEnabled == false)
                {
                    foreach (Druzyna druzyna in dogrywkaDruzyny)
                    {
                        if (dogrywkaMecze[numerDogrywki].getDruzynaA().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                        {
                            druzyna.punktyDogrywka();
                        }
                    }
                }
                else if ((Int32.Parse(Set2Druzyna1.Text) < Int32.Parse(Set2Druzyna2.Text)) && set3.IsEnabled == false)
                {
                    foreach (Druzyna druzyna in dogrywkaDruzyny)
                    {
                        if (dogrywkaMecze[numerDogrywki].getDruzynaB().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                        {
                            druzyna.punktyDogrywka();
                        }
                    }
                }
                else if (Set3Druzyna1.Text == "" || Set3Druzyna2.Text == "")
                {
                    MessageBox.Show("Błąd, wprowadz dane jeszcze raz zgodnie z zasadami");
                    numerMeczu--;
                }
                else if (Int32.Parse(Set3Druzyna1.Text) > Int32.Parse(Set3Druzyna2.Text))
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
            else
            {
                MessageBox.Show("Błąd, wprowadz dane jeszcze raz zgodnie z zasadami");
                numerMeczu--;


            }

        }


        private void Set1_KeyUp(object sender, KeyEventArgs e)
        {
            var white = new SolidColorBrush(Colors.White);
            var red = new SolidColorBrush(Colors.Red);


            if (Set1Druzyna1.Text != "" && Set1Druzyna2.Text != "")
            {
                try
                {

                    int druzyna1 = Int32.Parse(Set1Druzyna1.Text);
                    int druzyna2 = Int32.Parse(Set1Druzyna2.Text);

                    if ((druzyna1 >= 21 || druzyna2 >= 21) && ((Math.Abs(druzyna1 - druzyna2) == 2) || ((druzyna1 == 21 && druzyna2 <= 19) || (druzyna2 == 21 && druzyna1 <= 19))))
                    {

                        Set1Druzyna1.Background = white;
                        Set1Druzyna2.Background = white;
                        set2.IsEnabled = true;
                    }

                    else
                    {
                        Set1Druzyna1.Background = (druzyna1 < druzyna2 || druzyna1 == druzyna2) ? red : white;
                        Set1Druzyna2.Background = druzyna1 > druzyna2 ? red : white;
                        set2.IsEnabled = false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Wynik nie może zawierać liter");
                    Set1Druzyna1.Text = "";
                    Set1Druzyna2.Text = "";
                }
            }



        }


        private void Set2_KeyUp(object sender, KeyEventArgs e)
        {
            var white = new SolidColorBrush(Colors.White);
            var red = new SolidColorBrush(Colors.Red);


            if (Set2Druzyna1.Text != "" && Set2Druzyna2.Text != "")
            {
                try
                {

                    int druzyna1 = Int32.Parse(Set2Druzyna1.Text);
                    int druzyna2 = Int32.Parse(Set2Druzyna2.Text);

                    int druzyna3 = Int32.Parse(Set1Druzyna1.Text);
                    int druzyna4 = Int32.Parse(Set1Druzyna2.Text);

                    if ((druzyna1 >= 21 || druzyna2 >= 21) && ((Math.Abs(druzyna1 - druzyna2) == 2) || ((druzyna1 == 21 && druzyna2 <= 19) || (druzyna2 == 21 && druzyna1 <= 19))))
                    {

                        Set2Druzyna1.Background = white;
                        Set2Druzyna2.Background = white;

                        if (((druzyna3 > druzyna4 && druzyna1 > druzyna2) || (druzyna3 < druzyna4 && druzyna1 < druzyna2)))
                        {
                            set3.IsEnabled = false;
                        }
                        else
                        {
                            set3.IsEnabled = true;
                        }

                    }
                    else
                    {

                        Set2Druzyna1.Background = (druzyna1 < druzyna2 || druzyna1 == druzyna2) ? red : white;
                        Set2Druzyna2.Background = druzyna1 > druzyna2 ? red : white;
                        set3.IsEnabled = false;
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Wynik nie może zawierać liter");
                    Set1Druzyna1.Text = "";
                    Set1Druzyna2.Text = "";
                }



            }
        }

        private void Set3_KeyUp(object sender, KeyEventArgs e)
        {
            var white = new SolidColorBrush(Colors.White);
            var red = new SolidColorBrush(Colors.Red);
            if (Set3Druzyna1.Text != "" && Set3Druzyna2.Text != "")
            {
                try
                {
                    int druzyna1 = Int32.Parse(Set3Druzyna1.Text);
                    int druzyna2 = Int32.Parse(Set3Druzyna2.Text);

                    if ((druzyna1 == 15 && druzyna2 < 15) || (druzyna2 == 15 && druzyna1 < 15))
                    {

                        Set3Druzyna1.Background = white;
                        Set3Druzyna2.Background = white;



                    }
                    else
                    {
                        Set3Druzyna1.Background = (druzyna1 < druzyna2 || druzyna1 == druzyna2) ? red : white;
                        Set3Druzyna2.Background = druzyna1 > druzyna2 ? red : white;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Wynik nie może zawierać liter");
                    Set1Druzyna1.Text = "";
                    Set1Druzyna2.Text = "";
                }

            }
        }

        private void Set2_GotMouseCapture(object sender, MouseEventArgs e)
        {
            set1.IsEnabled = false;  //przy kliknieciu na texbox w II secie wykonanie działania (zamknięcie I seta)
        }

        private void Set3_GotMouseCapture(object sender, MouseEventArgs e)
        {
            set2.IsEnabled = false; //przy kliknieciu na texbox w III secie wykonanie działania (zamknięcie II seta)
        }
    }
}
