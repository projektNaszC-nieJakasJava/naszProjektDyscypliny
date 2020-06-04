using System;
using System.Collections.Generic;
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

namespace projektDyscypiny.Siatka
{
    /// <summary>
    /// Interaction logic for WynikiSiatkowka.xaml
    /// </summary>
    public partial class WynikiSiatkowka : Page
    {
        private static int i;
        public WynikiSiatkowka()
        {
            InitializeComponent();
            WygranaDruzynaLabel.FontSize = 19;
            WygranaDruzynaLabel.Content = TurniejSiatkowka.wygranaDruzyna.getNazwaDruzyny();
            OstatecznyRankingStackPanel();
        }

        private void PowrotClick(object sender, RoutedEventArgs e)
        {
            zapisRankinguPlik();
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new GlowneMenu();
            czyszczenieDanych();
        }

        private void OstatecznyRankingStackPanel()
        {
            i = 3;
            Label label1 = new Label();
            label1.Content = "1. " + TurniejSiatkowka.wygranaDruzyna.getNazwaDruzyny();
            ostatecznyRankingStackPanel.Children.Add(label1);
            Label label2 = new Label();
            label2.Content = "2. " + TurniejSiatkowka.druzynaPolfinalA.getNazwaDruzyny();
            ostatecznyRankingStackPanel.Children.Add(label2);
            foreach (Druzyna druzyna in TurniejSiatkowka.polfinalyDruzyny)
            {

                if (druzyna.getNazwaDruzyny() != TurniejSiatkowka.wygranaDruzyna.getNazwaDruzyny() && druzyna.getNazwaDruzyny() != TurniejSiatkowka.druzynaPolfinalA.getNazwaDruzyny())
                {
                    Label label = new Label();
                    label.Content = i + ". " + druzyna.getNazwaDruzyny();
                    ostatecznyRankingStackPanel.Children.Add(label);
                    ++i;
                }
            }
            for (int j = 4; j < Siatkowka.listaDruzyna.Count; j++)
            {
                Label label3 = new Label();
                label3.Content = j + 1 + ". " + Siatkowka.listaDruzyna[j].getNazwaDruzyny();
                ostatecznyRankingStackPanel.Children.Add(label3);
            }
        }

        private void zapisRankinguPlik()
        {
            i = 3;
            using (StreamWriter streamW = new StreamWriter(("SiatkowkaRankingTurnieju.txt"), true))
            {
                streamW.WriteLine("TURNIEJ");
                streamW.WriteLine("");
                streamW.WriteLine("Ranking:");
                streamW.WriteLine("");
            }
            using (StreamWriter streamW = new StreamWriter(("SiatkowkaRankingTurnieju.txt"), true))
            {
                streamW.WriteLine("1. " + TurniejSiatkowka.wygranaDruzyna.getNazwaDruzyny() + " ID: " + TurniejSiatkowka.wygranaDruzyna.getID_Druzyna());
                streamW.WriteLine("2. " + TurniejSiatkowka.druzynaPolfinalA.getNazwaDruzyny() + " ID: " + TurniejSiatkowka.druzynaPolfinalA.getID_Druzyna());
                foreach (Druzyna druzyna in TurniejSiatkowka.polfinalyDruzyny)
                {

                    if (druzyna.getNazwaDruzyny() != TurniejSiatkowka.wygranaDruzyna.getNazwaDruzyny() && druzyna.getNazwaDruzyny() != TurniejSiatkowka.druzynaPolfinalA.getNazwaDruzyny())
                    {
                        streamW.WriteLine(i + ". " + druzyna.getNazwaDruzyny() + " ID: " + druzyna.getID_Druzyna());
                        ++i;
                    }
                }
                for (int j = 4; j < Siatkowka.listaDruzyna.Count; j++)
                {
                    streamW.WriteLine(j + 1 + ". " + Siatkowka.listaDruzyna[j].getNazwaDruzyny() + " ID: " + Siatkowka.listaDruzyna[j].getID_Druzyna());
                }
                streamW.WriteLine("");


            }

            foreach (Mecz mecz in Siatkowka.listaMeczow)
            {
                using (StreamWriter streamW = new StreamWriter(("SiatkowkaRankingTurnieju.txt"), true))
                {
                    streamW.WriteLine(mecz.getDruzynaA().getNazwaDruzyny() + " VS " + mecz.getDruzynaB().getNazwaDruzyny() + " Glowny sedzia " + mecz.getSedzia().getImie_Sedzia() + " " + mecz.getSedzia().getNazwisko_Sedzia() + " Wynik " + mecz.getPunkty_Dr1() + ":" + mecz.getPunkty_Dr2());
                }
            }
            using (StreamWriter streamW = new StreamWriter(("SiatkowkaRankingTurnieju.txt"), true))
            {
                streamW.WriteLine("");
            }
        }

        private void czyszczenieDanych()
        {
            Siatkowka.listaMeczow.Clear();
            Siatkowka.listaDruzyna.Clear();
            TurniejSiatkowka.dogrywkaDruzyny.Clear();
            TurniejSiatkowka.dogrywkaMecze.Clear();
            TurniejSiatkowka.polfinalyDruzyny.Clear();
            TurniejSiatkowka.nrPolfinalu = 0;
            TurniejSiatkowka.numerDogrywki = 0;
            TurniejSiatkowka.numerMeczu = 0;


        }

     




    }
}
