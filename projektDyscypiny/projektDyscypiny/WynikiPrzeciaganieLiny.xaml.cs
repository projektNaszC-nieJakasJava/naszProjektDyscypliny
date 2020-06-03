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

namespace projektDyscypiny
{
    /// <summary>
    /// Interaction logic for WynikiPrzeciaganieLiny.xaml
    /// </summary>
    public partial class WynikiPrzeciaganieLiny : Page
    {
        private static int i;
        public WynikiPrzeciaganieLiny()
        {
            InitializeComponent();
            WygranaDruzynaLabel.FontSize = 19;
            WygranaDruzynaLabel.Content = TurniejPrzeciaganieLiny.wygranaDruzyna.getNazwaDruzyny();
            OstatecznyRankingStackPanel();
        }

        private void PowrotClick(object sender, RoutedEventArgs e)
        {
            zapisRankinguPlik();
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new GlowneMenu();//zmiana okna z przec. liny na glowne
            czyszczenieDanych();
        }

        private void OstatecznyRankingStackPanel()
        {
            i = 3;
            Label label1 = new Label();
            label1.Content = "1. " + TurniejPrzeciaganieLiny.wygranaDruzyna.getNazwaDruzyny();
            ostatecznyRankingStackPanel.Children.Add(label1);
            Label label2 = new Label();
            label2.Content = "2. " +  TurniejPrzeciaganieLiny.druzynaPolfinalA.getNazwaDruzyny();
            ostatecznyRankingStackPanel.Children.Add(label2);
            foreach (Druzyna druzyna in TurniejPrzeciaganieLiny.polfinalyDruzyny)
            {
                
                if(druzyna.getNazwaDruzyny()!= TurniejPrzeciaganieLiny.wygranaDruzyna.getNazwaDruzyny() && druzyna.getNazwaDruzyny() != TurniejPrzeciaganieLiny.druzynaPolfinalA.getNazwaDruzyny())
                {
                    Label label = new Label();
                    label.Content = i + ". " + druzyna.getNazwaDruzyny();
                    ostatecznyRankingStackPanel.Children.Add(label);
                    ++i;
                }
            }
            for(int j = 4;j<PrzeciaganieLiny.listaDruzyna.Count;j++)
            {
                Label label3 = new Label();
                label3.Content = j+1 + ". " + PrzeciaganieLiny.listaDruzyna[j].getNazwaDruzyny();
                ostatecznyRankingStackPanel.Children.Add(label3);
            }
        }
        private void zapisRankinguPlik()
        {
            i = 3;
            File.WriteAllText("PrzeciaganieLinyRankingTurnieju.txt", string.Empty);
            using (StreamWriter streamW = new StreamWriter(("PrzeciaganieLinyRankingTurnieju.txt"), true))
            {
                streamW.WriteLine("1. " + TurniejPrzeciaganieLiny.wygranaDruzyna.getNazwaDruzyny() + " ID: " + TurniejPrzeciaganieLiny.wygranaDruzyna.getID_Druzyna());
                streamW.WriteLine("2. "+ TurniejPrzeciaganieLiny.druzynaPolfinalA.getNazwaDruzyny() + " ID: " + TurniejPrzeciaganieLiny.druzynaPolfinalA.getID_Druzyna()); 
                foreach (Druzyna druzyna in TurniejPrzeciaganieLiny.polfinalyDruzyny)
                {

                    if (druzyna.getNazwaDruzyny() != TurniejPrzeciaganieLiny.wygranaDruzyna.getNazwaDruzyny() && druzyna.getNazwaDruzyny() != TurniejPrzeciaganieLiny.druzynaPolfinalA.getNazwaDruzyny())
                    { 
                        streamW.WriteLine(i + ". " + druzyna.getNazwaDruzyny() + " ID: " + druzyna.getID_Druzyna());
                        ++i;
                    }
                }
                for (int j = 4; j < PrzeciaganieLiny.listaDruzyna.Count; j++)
                {
                    streamW.WriteLine(j+1 + ". " + PrzeciaganieLiny.listaDruzyna[j].getNazwaDruzyny() + " ID: " + PrzeciaganieLiny.listaDruzyna[j].getID_Druzyna());
                }
            }
        }
        private void czyszczenieDanych()
        {
            PrzeciaganieLiny.listaMeczow.Clear();
            PrzeciaganieLiny.listaDruzyna.Clear();
            TurniejPrzeciaganieLiny.dogrywkaDruzyny.Clear();
            TurniejPrzeciaganieLiny.dogrywkaMecze.Clear();
            TurniejPrzeciaganieLiny.polfinalyDruzyny.Clear();
            TurniejPrzeciaganieLiny.nrPolfinalu = 0;
            TurniejPrzeciaganieLiny.numerDogrywki = 0;
            TurniejPrzeciaganieLiny.numerMeczu = 0;
            File.WriteAllText("PrzeciaganieLinyMeczeDane.txt", string.Empty);
        }
    }
}
