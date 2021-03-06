﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for Siatkowka.xaml
    /// </summary>
    public partial class Siatkowka : Page
    {
        public static List<Sedzia> listaSedziow = new List<Sedzia>();
        public static List<Druzyna> listaDruzyna = new List<Druzyna>();
        public static List<Mecz> listaMeczow = new List<Mecz>();

        Random random = new Random();
        Random random1 = new Random();
        Random random2 = new Random();
        public Siatkowka()
        {
            InitializeComponent();

            try
            {
                using (StreamReader streamR = new StreamReader("SiatkowkaSedziaDane.txt"))
                {
                    string linia;
                    while ((linia = streamR.ReadLine()) != null)
                    {
                        char[] oddzielanieWyrazow = { ';'};
                        string[] podzialListy = linia.Split(oddzielanieWyrazow);
                        Label label = new Label();
                        int intID = Int32.Parse(podzialListy[2]);
                        label.Content = "Imie: " + podzialListy[0] + " Nazwisko: " + podzialListy[1] + " ID: " + intID;
                        sedziowieStackPanel.Children.Add(label);
                        Sedzia sedzia = new Sedzia(podzialListy[0], podzialListy[1], intID);
                        label.Tag = sedzia.getID_Sedzia();
                        listaSedziow.Add(sedzia);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Brak pliku z sędziami");
            }

            try
            {
                using (StreamReader streamR = new StreamReader("SiatkowkaDruzynaDane.txt"))
                {
                    string linia;
                    while ((linia = streamR.ReadLine()) != null)
                    {
                        char[] oddzielanieWyrazow = { ';' };
                        string[] podzialListy = linia.Split(oddzielanieWyrazow);
                        Label label = new Label();
                        int intID = Int32.Parse(podzialListy[1]);
                        label.Content = "Nazwa druzyny: " + podzialListy[0] + " ID: " + intID;
                        druzynyStackPanel.Children.Add(label);
                        Druzyna druzyna = new Druzyna(podzialListy[0], intID);
                        label.Tag = druzyna.getID_Druzyna();
                        listaDruzyna.Add(druzyna);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Brak pliku z drużynami");
            }

            try
            {
                using (StreamReader streamR = new StreamReader("SiatkowkaRankingTurnieju.txt"))
                {
                    string linia;
                    while ((linia = streamR.ReadLine()) != null)
                    {
                        Label label = new Label();
                        label.Content = linia;
                        wypisywanieTurnieji.Children.Add(label);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Brak pliku z Rankingiem meczy");
            }
        }

        private void Powrot(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new GlowneMenu();
        }

        private void RozpocznijTurniejClick(object sender, RoutedEventArgs e)
        {
            if (listaSedziow.Count >= 3 && listaDruzyna.Count >= 4)
            {
                losowanie();
                TurniejSiatkowka.status = "ELIMINACJE";
                ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new TurniejSiatkowka();
            }
            else
            {
                MessageBox.Show("Jeśli chcesz rozpocząć turniej SIATKÓWKI w bazie musi być co najmniej: 3 sędziów i 4 drużyny");
                MessageBox.Show("Sprawdź w ocpjach 'wyświetl' czy posiadasz wystarczającą ilość");
            }
        }

        int WygenerujSedziaID()
        {
            int maxID = 0;

            foreach (var sedzia in listaSedziow)
            {
                if (sedzia.getID_Sedzia() >= maxID)
                {
                    maxID = sedzia.getID_Sedzia();
                }
            }
            return maxID + 1;
        }

        private void DodajSedziegoClick(object sender, RoutedEventArgs e)
        {
            string imie = SedziaImie.Text;
            string nazwisko = SedziaNazwisko.Text;
            int id = 1;
            if (listaSedziow.Count != 0)
            {
                id = WygenerujSedziaID();
            }

            if (SedziaImie.Text == "" || SedziaNazwisko.Text == "")
            {
                SedziaImie.Text = "";
                SedziaNazwisko.Text = "";
                MessageBox.Show("Nie wpisano wszystkich danych");
            }
            else
            {
                Sedzia sedzia = new Sedzia(imie, nazwisko, id);

                using (StreamWriter streamW = new StreamWriter(("SiatkowkaSedziaDane.txt"), true))
                {
                    streamW.WriteLine(sedzia.getImie_Sedzia() + ";" + sedzia.getNazwisko_Sedzia() + ";" + sedzia.getID_Sedzia());
                }

                WyswietlSedziowStackPanel(sedzia);
                listaSedziow.Add(sedzia);
                SedziaImie.Text = "";
                SedziaNazwisko.Text = "";
                MessageBox.Show("Pomyślnie dodano sędziego");
            }
        }

        void WyswietlSedziowStackPanel(Sedzia sedzia)
        {
            Label label = new Label();
            label.Content = "Imie: " + sedzia.getImie_Sedzia() + " Nazwisko: " + sedzia.getNazwisko_Sedzia() + " ID: " + sedzia.getID_Sedzia();
            label.Tag = sedzia.getID_Sedzia();
            sedziowieStackPanel.Children.Add(label);
        }

        void UsunSedziegoStackPanel(Sedzia sedzia)
        {
            Label label;
            foreach (var child in sedziowieStackPanel.Children)
            {
                label = child as Label;
                if ((int)label.Tag == sedzia.getID_Sedzia())
                {
                    sedziowieStackPanel.Children.Remove(label);
                    break;
                }
            }
        }

        private void UsunSedziegoClick(object sender, RoutedEventArgs e)
        {
            string id2 = UsuwanieSedziegoTextBox.Text;

            if (UsuwanieSedziegoTextBox.Text == "")
            {
                MessageBox.Show("Nie wpisano ID");
            }
            else
            {
                try
                {
                    int id = Int32.Parse(UsuwanieSedziegoTextBox.Text);

                    foreach (Sedzia sedzia in listaSedziow)
                    {
                        if (id == sedzia.getID_Sedzia())
                        {
                            UsunSedziegoStackPanel(sedzia);
                            listaSedziow.Remove(sedzia);
                            UsuwanieSedziegoTextBox.Text = "";
                            UsuwanieSedziegoTextBox.Text = "";
                            MessageBox.Show("Usunięto sędziego");
                            File.WriteAllText("SiatkowkaSedziaDane.txt", string.Empty);   // Czyszczenie pliku

                            using (StreamWriter streamW = new StreamWriter(("SiatkowkaSedziaDane.txt"), true))
                                foreach (Sedzia sedzia1 in listaSedziow)
                            {                               
                                    streamW.WriteLine(sedzia1.getImie_Sedzia() + ";" + sedzia1.getNazwisko_Sedzia() + ";" + sedzia1.getID_Sedzia());                              
                            }
                            id = 0;
                            break;
                        }
                    }

                    if (id != 0)
                    {
                        UsuwanieSedziegoTextBox.Text = "";
                        MessageBox.Show("NIe ma sedziego o takim ID");
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("ID nie może skladać się z liter");
                    UsuwanieSedziegoTextBox.Text = "";
                }
            }
        }

        private void SzukajSedziegoClick(object sender, RoutedEventArgs e)
        {
            if (SedziaIDTextBox.Text == "")
            {
                MessageBox.Show("Nie wpisano ID");
            }
            else
            {
                try
                {
                    int idSedzia = Int32.Parse(SedziaIDTextBox.Text);

                    foreach (var sedzia in listaSedziow)
                    {
                        if (idSedzia == sedzia.getID_Sedzia())
                        {
                            MessageBox.Show("Imie: " + sedzia.getImie_Sedzia() + " Nazwisko: " + sedzia.getNazwisko_Sedzia() + " ID: " + sedzia.getID_Sedzia());
                            SedziaIDTextBox.Text = "";
                            idSedzia = 0;
                            break;
                        }
                    }

                    if (idSedzia != 0)
                    {
                        SedziaIDTextBox.Text = "";
                        MessageBox.Show("NIe ma sedziego o takim ID");
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("ID nie może skladać się z liter");
                    SedziaIDTextBox.Text = "";
                }
            }
        }






        int wygenerujDruzynaID()
        {
            int maxID = 0;

            foreach (var druzyna in listaDruzyna)
            {
                if (druzyna.getID_Druzyna() >= maxID)
                {
                    maxID = druzyna.getID_Druzyna();
                }
            }

            return maxID + 1;
        }

        private void DodajDruzyneClick(object sender, RoutedEventArgs e)
        {
            string nazwa = DruzynaNazwa.Text;
            int id = 1;
            if (listaDruzyna.Count != 0)
            {
                id = wygenerujDruzynaID();
            }
            if (DruzynaNazwa.Text == "")
            {
                MessageBox.Show("Nie wpisano nazwy drużyny");
            }
            else
            {
                Druzyna druzyna = new Druzyna(nazwa, id);

                using (StreamWriter streamW = new StreamWriter(("SiatkowkaDruzynaDane.txt"), true))
                {
                    streamW.WriteLine(druzyna.getNazwaDruzyny() + ";" + druzyna.getID_Druzyna());
                }
                WyswietlDruzynyStackPanel(druzyna);
                listaDruzyna.Add(druzyna);
                DruzynaNazwa.Text = "";
                MessageBox.Show("Pomyślnie dodano drużynę");
            }
        }

        void WyswietlDruzynyStackPanel(Druzyna druzyna)
        {
            Label label = new Label();
            label.Content = "Nazwa druzyny: " + druzyna.getNazwaDruzyny() + " ID: " + druzyna.getID_Druzyna();
            label.Tag = druzyna.getID_Druzyna();
            druzynyStackPanel.Children.Add(label);
        }

        void WycofajDruzyneStackPanel(Druzyna druzyna)
        {
            Label label;
            foreach (var child in druzynyStackPanel.Children)
            {
                label = child as Label;
                if ((int)label.Tag == druzyna.getID_Druzyna())
                {
                    druzynyStackPanel.Children.Remove(label);
                    break;
                }
            }
        }

        private void WycofajDruzyneClick(object sender, RoutedEventArgs e)
        {
            if (WycofanieDruzynyTextBox.Text == "")
            {
                MessageBox.Show("Nie wpisano ID");
            }
            else
            {
                try
                {
                    int id = Int32.Parse(WycofanieDruzynyTextBox.Text);
                    foreach (var druzyna in listaDruzyna)
                    {
                        if (id == druzyna.getID_Druzyna())
                        {
                            WycofajDruzyneStackPanel(druzyna);
                            listaDruzyna.Remove(druzyna);
                            WycofanieDruzynyTextBox.Text = "";
                            MessageBox.Show("Wycofano drużyne");
                            File.WriteAllText("SiatkowkaDruzynaDane.txt", string.Empty);  //czyszczenie
                            using (StreamWriter streamW = new StreamWriter(("SiatkowkaDruzynaDane.txt"), true))
                                foreach (Druzyna druzyna1 in listaDruzyna)
                            {
                               
                                    streamW.WriteLine(druzyna1.getNazwaDruzyny() + ";" + druzyna1.getID_Druzyna());
                                
                            }
                            id = 0;
                            break;
                        }
                    }
                    if (id != 0)
                    {
                        WycofanieDruzynyTextBox.Text = "";
                        MessageBox.Show("NIe ma drużyny o takim ID");
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("ID nie może skladać się z liter");
                    WycofanieDruzynyTextBox.Text = "";
                }
            }

        }

        private void SzukajDruzynaClick(object sender, RoutedEventArgs e)
        {
            if (DruzynaIDTextBox.Text == "")
            {
                MessageBox.Show("Nie wpisano ID");
            }
            else
            {
                try
                {
                    int idDruzyna = Int32.Parse(DruzynaIDTextBox.Text);


                    foreach (var druzyna in listaDruzyna)
                    {
                        if (idDruzyna == druzyna.getID_Druzyna())
                        {
                            MessageBox.Show("Nazwa druzyny: " + druzyna.getNazwaDruzyny() + " ID: " + druzyna.getID_Druzyna());
                            DruzynaIDTextBox.Text = "";
                            idDruzyna = 0;
                            break;
                        }
                    }
                    if (idDruzyna != 0)
                    {
                        DruzynaIDTextBox.Text = "";
                        MessageBox.Show("Nie istnieje drużyna o takim ID");
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("ID nie może skladać się z liter");
                    DruzynaIDTextBox.Text = "";
                }
            }
        }

        private void losowanie() //losowanie do meczy
        {
            for (int i = 0; i < listaDruzyna.Count - 1; i++)
                for (int j = i + 1; j < listaDruzyna.Count; j++)
                {
                    int indexSedziego = random.Next(listaSedziow.Count); //losowanie indexu sedziego
                    int indexSedziego1 = random1.Next(listaSedziow.Count); //losowanie indexu sedziego
                    int indexSedziego2 = random2.Next(listaSedziow.Count); //losowanie indexu sedziego
                    listaMeczow.Add(new Mecz(listaDruzyna[i], listaDruzyna[j], listaSedziow[indexSedziego], listaSedziow[indexSedziego1], listaSedziow[indexSedziego2]));
                }
        }
    }
}
