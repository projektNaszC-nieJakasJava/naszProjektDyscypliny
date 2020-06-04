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

namespace projektDyscypiny.Ogien
{
    /// <summary>
    /// Interaction logic for WynikiDwaOgnie.xaml
    /// </summary>
    public partial class WynikiDwaOgnie : Page
    {
        private static int i;
        public WynikiDwaOgnie()
        {
            InitializeComponent();
            WygranaDruzynaLabel.FontSize = 19;
            WygranaDruzynaLabel.Content = TurniejDwaOgnie.wygranaDruzyna.getNazwaDruzyny();
            OstatecznyRankingStackPanel();
        }

        private void PowrotClick(object sender, RoutedEventArgs e)
        {
            zapisRankinguPlik();          
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new GlowneMenu();//zmiana okna z dwoch ognii na glowne
            czyszczenieDanych();
        }

        private void OstatecznyRankingStackPanel()
        {
            i = 3;
            Label label1 = new Label();
            label1.Content = "1. " + TurniejDwaOgnie.wygranaDruzyna.getNazwaDruzyny();
            ostatecznyRankingStackPanel.Children.Add(label1);
            Label label2 = new Label();
            label2.Content = "2. " + TurniejDwaOgnie.druzynaPolfinalA.getNazwaDruzyny();
            ostatecznyRankingStackPanel.Children.Add(label2);
            foreach (Druzyna druzyna in TurniejDwaOgnie.polfinalyDruzyny)
            {

                if (druzyna.getNazwaDruzyny() != TurniejDwaOgnie.wygranaDruzyna.getNazwaDruzyny() && druzyna.getNazwaDruzyny() != TurniejDwaOgnie.druzynaPolfinalA.getNazwaDruzyny())
                {
                    Label label = new Label();
                    label.Content = i + ". " + druzyna.getNazwaDruzyny();
                    ostatecznyRankingStackPanel.Children.Add(label);
                    ++i;
                }
            }
            for (int j = 4; j < DwaOgnie.listaDruzyna.Count; j++)
            {
                Label label3 = new Label();
                label3.Content = j + 1 + ". " + DwaOgnie.listaDruzyna[j].getNazwaDruzyny();
                ostatecznyRankingStackPanel.Children.Add(label3);
            }
        }
        private void zapisRankinguPlik()
        {
            i = 3;

            using (StreamWriter streamW = new StreamWriter(("DwaOgnieRankingTurnieju.txt"), true))
            {
                streamW.WriteLine("TURNIEJ");
                streamW.WriteLine("");
                streamW.WriteLine("Ranking:");
                streamW.WriteLine("");
            }

            using (StreamWriter streamW = new StreamWriter(("DwaOgnieRankingTurnieju.txt"), true))
            {
                streamW.WriteLine("1. " + TurniejDwaOgnie.wygranaDruzyna.getNazwaDruzyny() + " ID: " + TurniejDwaOgnie.wygranaDruzyna.getID_Druzyna());
                streamW.WriteLine("2. " + TurniejDwaOgnie.druzynaPolfinalA.getNazwaDruzyny() + " ID: " + TurniejDwaOgnie.druzynaPolfinalA.getID_Druzyna());
                foreach (Druzyna druzyna in TurniejDwaOgnie.polfinalyDruzyny)
                {

                    if (druzyna.getNazwaDruzyny() != TurniejDwaOgnie.wygranaDruzyna.getNazwaDruzyny() && druzyna.getNazwaDruzyny() != TurniejDwaOgnie.druzynaPolfinalA.getNazwaDruzyny())
                    {
                        streamW.WriteLine(i + ". " + druzyna.getNazwaDruzyny() + " ID: " + druzyna.getID_Druzyna());
                        ++i;
                    }
                }
                for (int j = 4; j < DwaOgnie.listaDruzyna.Count; j++)
                {
                    streamW.WriteLine(j + 1 + ". " + DwaOgnie.listaDruzyna[j].getNazwaDruzyny() + " ID: " + DwaOgnie.listaDruzyna[j].getID_Druzyna());
                }
                streamW.WriteLine("");

               


            }

            using (StreamWriter streamW = new StreamWriter(("DwaOgnieRankingTurnieju.txt"), true))
            {
                streamW.WriteLine("Wyniki Meczy");
                streamW.WriteLine("");
            }
            foreach (Mecz mecz in DwaOgnie.listaMeczow)
            {
                using (StreamWriter streamW = new StreamWriter(("DwaOgnieRankingTurnieju.txt"), true))
                {
                    streamW.WriteLine(mecz.getDruzynaA().getNazwaDruzyny() + " VS " + mecz.getDruzynaB().getNazwaDruzyny() + " sedzia " + mecz.getSedzia().getImie_Sedzia() + " " + mecz.getSedzia().getNazwisko_Sedzia() + " Wynik " + mecz.getPunkty_Dr1() + ":" + mecz.getPunkty_Dr2());
                }
            }
            using (StreamWriter streamW = new StreamWriter(("DwaOgnieRankingTurnieju.txt"), true))
            {
                streamW.WriteLine("");
            }
        }
        private void czyszczenieDanych()
        {
            DwaOgnie.listaMeczow.Clear();
            DwaOgnie.listaDruzyna.Clear();
            TurniejDwaOgnie.dogrywkaDruzyny.Clear();
            TurniejDwaOgnie.dogrywkaMecze.Clear();
            TurniejDwaOgnie.polfinalyDruzyny.Clear();
            TurniejDwaOgnie.nrPolfinalu = 0;
            TurniejDwaOgnie.numerDogrywki = 0;
            TurniejDwaOgnie.numerMeczu = 0;
            
        }
       
    }
}
