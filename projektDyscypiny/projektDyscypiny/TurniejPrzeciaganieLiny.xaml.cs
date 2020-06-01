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
        List<Druzyna> dogrywkaDruzyny = new List<Druzyna>();
        public  static List<Mecz> dogrywkaMecze = new List<Mecz>();
        List<Druzyna> polfinalyDruzyny = new List<Druzyna>();
       // List<Druzyna> kopiaDruzyna = PrzeciaganieLiny.listaDruzyna;
        Random random = new Random();
        public TurniejPrzeciaganieLiny()
        {
            InitializeComponent();

            statusLabel.Content = "Aktualny status: " + status;
            statusLabel.FontSize = 17;
            if (PrzeciaganieLiny.listaMeczow.Count > numerMeczu)
            //status == "ELIMINACJE"
            {
                DruzynaALabel.Content = PrzeciaganieLiny.listaMeczow[numerMeczu].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = PrzeciaganieLiny.listaMeczow[numerMeczu].getDruzynaB().getNazwaDruzyny();
            }
            if (status == "DOGRYWKA")
            {
                DruzynaALabel.Content = dogrywkaMecze[numerDogrywki].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = dogrywkaMecze[numerDogrywki].getDruzynaB().getNazwaDruzyny();
                //numerDogrywki++;
            }
            if (status == "PÓŁFINAŁY")
            {
                DruzynaALabel.Content = PrzeciaganieLiny.listaMeczow[numerMeczu].getDruzynaA().getNazwaDruzyny();
                DruzynaBLabel.Content = PrzeciaganieLiny.listaMeczow[numerMeczu].getDruzynaB().getNazwaDruzyny();
            }
            DruzynaALabel.FontSize = 14;
            DruzynaBLabel.FontSize = 14;
            WyswietlRankingStackPanel();
            WypisywanieMeczowStackPanel();
        }

        private void WynikiTurniejuClick(object sender, RoutedEventArgs e)
        {
            
            if (numerMeczu == PrzeciaganieLiny.listaMeczow.Count)
            {
                dogrywka();
                
            }
            
            if (status == "DOGRYWKA")
            {
                wpiszWyniki_Dogrywka();
                numerDogrywki++;
            }

            
            if (status == "ELIMINACJE")
            {
                wpiszWyniki();
                numerMeczu++;
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

        private void stworzRanking()
        {
            Druzyna druzynaPom = new Druzyna();
            for (int i = 0; i < PrzeciaganieLiny.listaDruzyna.Count - 1; i++)
            {
                if (PrzeciaganieLiny.listaDruzyna[i].getWygrane() < PrzeciaganieLiny.listaDruzyna[i + 1].getWygrane())
                {
                    druzynaPom = PrzeciaganieLiny.listaDruzyna[i];
                    PrzeciaganieLiny.listaDruzyna[i] = PrzeciaganieLiny.listaDruzyna[i + 1];
                    PrzeciaganieLiny.listaDruzyna[i + 1] = druzynaPom;
                }
            }
        }
        private void WyswietlRankingStackPanel()
        {
            stworzRanking();
            foreach (Druzyna druzyna in PrzeciaganieLiny.listaDruzyna)
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
            int ilosc = 1; //ilosc druzyn wchodzacych do polfinalow z dogrywki
            for (int i = 0; i < PrzeciaganieLiny.listaDruzyna.Count; i++)
            {
                if (PrzeciaganieLiny.listaDruzyna[i].getWygrane() == PrzeciaganieLiny.listaDruzyna[3].getWygrane())
                {
                    dogrywkaDruzyny.Add(PrzeciaganieLiny.listaDruzyna[i]);
                }
            }
            if (PrzeciaganieLiny.listaDruzyna.Count == 4) //przypadek gdy nie trzeba robic dogrywek, cztery pierwsze druzyny wchodza do polfinalow
            {
                for (int i = 0; i < 4; i++)
                    polfinalyDruzyny.Add(PrzeciaganieLiny.listaDruzyna[i]);
                status = "PÓŁFINAŁY";
            }
            else if (PrzeciaganieLiny.listaDruzyna[3].getWygrane() == PrzeciaganieLiny.listaDruzyna[4].getWygrane()) //przypadek gdy trzeba zrobic dogrywki
            {
                status = "DOGRYWKA";
                for (int i = 0; i < 3; i++)
                {
                    if (PrzeciaganieLiny.listaDruzyna[i].getWygrane() == PrzeciaganieLiny.listaDruzyna[3].getWygrane())
                        ilosc++;
                    else
                        polfinalyDruzyny.Add(PrzeciaganieLiny.listaDruzyna[i]);
                }
                for (int i = 0; i < dogrywkaDruzyny.Count - 1; i++)
                    for (int j = i + 1; j < dogrywkaDruzyny.Count; j++)
                    {
                        int indexSedziego = random.Next(PrzeciaganieLiny.listaSedziow.Count - 1); //losowanie indexu sedziego
                        dogrywkaMecze.Add(new Mecz(dogrywkaDruzyny[i], dogrywkaDruzyny[j], PrzeciaganieLiny.listaSedziow[indexSedziego]));
                    }
            }
            else //przypadek gdy nie trzeba robic dogrywek, cztery pierwsze druzyny wchodza do polfinalow
            {
                //dogrywkaDruzyny.Clear();
                polfinaly();
            }

        }

        private void polfinaly()
        {
            //dodanie do listy wygranych druzyn z dogrywki o ile byla
            status = "PÓŁFINAŁY";
            for (int i = 0, j = 1; i < 2; i+=2, j+=2)
            {
                int indexSedziego = random.Next(PrzeciaganieLiny.listaSedziow.Count - 1);
                PrzeciaganieLiny.listaMeczow.Add(new Mecz(polfinalyDruzyny[i], polfinalyDruzyny[j], PrzeciaganieLiny.listaSedziow[indexSedziego]));
            }
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
                dogrywkaMecze[numerDogrywki].wygranaDruzynyA(); //wynik meczu np. 1:0
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
                dogrywkaMecze[numerDogrywki].wygranaDruzynyB();
            }

        }
    }
}
