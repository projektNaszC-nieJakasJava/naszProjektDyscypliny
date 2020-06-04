﻿using projektDyscypiny.Lina;
using projektDyscypiny.Ogien;
using projektDyscypiny.Siatka;
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

namespace projektDyscypiny
{
    /// <summary>
    /// Interaction logic for GlowneMenu.xaml
    /// </summary>
    public partial class GlowneMenu : Page
    {
        public GlowneMenu()
        {
            InitializeComponent();
        }

        private void DwaOgnieClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new DwaOgnie();
        }

        private void SiatkowkaClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new Siatkowka();
        }

        private void PrzeciaganieLinyClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new PrzeciaganieLiny();
        }
    }
}
