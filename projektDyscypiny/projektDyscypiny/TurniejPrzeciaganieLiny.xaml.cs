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
        private string status = "ELIMINACJE";
        private string druzynaA;
        private string druzynaB;
        private static int numerMeczu = 0;
        List<Mecz> mecze = new List<Mecz>();
        List<Druzyna> druzyny = new List<Druzyna>();
        public TurniejPrzeciaganieLiny()
        {
            InitializeComponent();

            odczytPliku();
            odczytPlikuDruzyny();

            statusLabel.Content = "Aktualny status: " + status;
            statusLabel.FontSize = 17;
            DruzynaALabel.Content = mecze[numerMeczu].getDruzynaA().getNazwaDruzyny();
            DruzynaBLabel.Content = mecze[numerMeczu].getDruzynaB().getNazwaDruzyny();
            DruzynaALabel.FontSize = 14;
            DruzynaBLabel.FontSize = 14;

        }

        private void WynikiTurniejuClick(object sender, RoutedEventArgs e)
        {
            wpiszWyniki();
            numerMeczu++;
            //WyswietlRankingStackPanel();
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new TurniejPrzeciaganieLiny();
            //((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiPrzeciaganieLiny();
        }

        private void wpiszWyniki()
        {

            if (DruzynaARadioButton.IsChecked == true)
            {
                foreach (Druzyna druzyna in druzyny)
                {
                    if (mecze[numerMeczu].getDruzynaA().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                    {
                        druzyna.iloscWygranych();
                    }
                }
                mecze[numerMeczu].wygranaDruzynyA(); //wynik meczu np. 1:0
                //mecze[numerMeczu].getDruzynaA().iloscWygranych(); //inkrementowanie wygranych we wszystkich meczach
                //mecze[numerMeczu].getDruzynaB().iloscPrzegranych();
            }
            else
            {
                foreach (Druzyna druzyna in druzyny)
                {
                    if (mecze[numerMeczu].getDruzynaB().getNazwaDruzyny() == druzyna.getNazwaDruzyny())
                    {
                        druzyna.iloscWygranych();
                    }
                }
                mecze[numerMeczu].wygranaDruzynyB();
                //mecze[numerMeczu].getDruzynaB().iloscWygranych();
                //mecze[numerMeczu].getDruzynaA().iloscPrzegranych();
            }

            File.WriteAllText("PrzeciaganieLinyMeczeDane.txt", string.Empty);
            using (StreamWriter streamW = new StreamWriter(("PrzeciaganieLinyMeczeDane.txt"), true))
                foreach (Mecz mecz in mecze)
                {
                    streamW.WriteLine(mecz.getDruzynaA().getNazwaDruzyny() + ";" + mecz.getDruzynaB().getNazwaDruzyny() + ";" + mecz.getSedzia().getImie_Sedzia() + ";" + mecz.getSedzia().getNazwisko_Sedzia() + ";" + mecz.getPunkty_Dr1() + ";" + mecz.getPunkty_Dr2());
                }
            sumowanieWygranych();
        }

        private void odczytPliku()
        {
            using (StreamReader streamR = new StreamReader("PrzeciaganieLinyMeczeDane.txt"))
            {
                string linia;
                while ((linia = streamR.ReadLine()) != null)
                {
                    char[] oddzielanieWyrazow = { ';' };
                    string[] podzialListy = linia.Split(oddzielanieWyrazow);
                    Label label = new Label();
                    int punktyDruzynyA = Int32.Parse(podzialListy[4]);
                    int punktyDruzynyB = Int32.Parse(podzialListy[5]);
                    Druzyna druzynaA = new Druzyna(podzialListy[0]);
                    Druzyna druzynaB = new Druzyna(podzialListy[1]);
                    druzynaA.setPunkty(punktyDruzynyA);
                    Sedzia sedzia = new Sedzia(podzialListy[2], podzialListy[3]);
                    mecze.Add(new Mecz(druzynaA, druzynaB, sedzia, punktyDruzynyA, punktyDruzynyB));
                }
            }
        }

        private void odczytPlikuDruzyny()
        {
            using (StreamReader streamR = new StreamReader("PrzeciaganieLinyDruzynaDane.txt"))
            {
                string linia;
                while ((linia = streamR.ReadLine()) != null)
                {
                    char[] oddzielanieWyrazow = { ';' };
                    string[] podzialListy = linia.Split(oddzielanieWyrazow);
                    Label label = new Label();
                    int iloscWygranych = Int32.Parse(podzialListy[2]);
                    int druzynaID = Int32.Parse(podzialListy[1]);
                    Druzyna druzyna = new Druzyna(podzialListy[0], druzynaID);
                    druzyna.setWygrane(iloscWygranych);
                    druzyny.Add(druzyna);
                }
            }
        }

        private void sumowanieWygranych() //nadpisywanie pliku z druzynami
        {
            File.WriteAllText("PrzeciaganieLinyDruzynaDane.txt", string.Empty);
            using (StreamWriter streamW = new StreamWriter(("PrzeciaganieLinyDruzynaDane.txt"), true))
                foreach (Druzyna druzyna in druzyny)
                {
                    streamW.WriteLine(druzyna.getNazwaDruzyny() + ";" + druzyna.getID_Druzyna() + ";" + druzyna.getWygrane());
                }
        }

        private void stworzRanking()
        {
            Druzyna druzynaPom = new Druzyna();
            for (int i = 0; i < druzyny.Count - 2; i++)
            {
                if (druzyny[i].getWygrane() < druzyny[i+1].getWygrane())
                {
                    druzynaPom = druzyny[i];
                    druzyny[i] = druzyny[i + 1];
                    druzyny[i + 1] = druzynaPom;
                }
            }
        }
       /* void WyswietlRankingStackPanel()
        {
            Label label = new Label();
            //()
            {
                //label.Content = druzyna.getNazwaDruzyny() + " wygrane " + druzyna.getWygrane();
                //label.Tag = druzyna.getWygrane();
                //rankingStackPanel.Children.Add(label);
            }
        } */

    }
}
