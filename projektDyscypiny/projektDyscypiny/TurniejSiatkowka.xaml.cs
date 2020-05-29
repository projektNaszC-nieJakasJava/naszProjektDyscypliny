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
    /// Interaction logic for TurniejSiatkowka.xaml
    /// </summary>
    public partial class TurniejSiatkowka : Page
    {

        public TurniejSiatkowka()
        {
            InitializeComponent();
        }

        private void WynikiTurniejuClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).GlowneOkno.Content = new WynikiSiatkowka();

        }

        private void Set1_KeyUp(object sender, KeyEventArgs e)
        {






            if (Set1Druzyna1.Text != "" && Set1Druzyna2.Text != "")
            {
                try
                {

                    int druzyna1 = Int32.Parse(Set1Druzyna1.Text);
                    int druzyna2 = Int32.Parse(Set1Druzyna2.Text);

                    if ((druzyna1 >= 21 || druzyna2 >= 21) && ( (Math.Abs(druzyna1 - druzyna2) == 2)  ||  ((druzyna1==21 && druzyna2<=19) || (druzyna2 == 21 && druzyna1 <= 19) )  )) 
                    {
                      
                            Set1Druzyna1.Background = new SolidColorBrush(Colors.White);
                            Set1Druzyna2.Background = new SolidColorBrush(Colors.White);
                            set2.IsEnabled = true;                       
                    }

                    else
                    {
                        Set1Druzyna1.Background = (druzyna1 < druzyna2 || druzyna1 == druzyna2) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
                        Set1Druzyna2.Background = druzyna1 > druzyna2  ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
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

            if (Set2Druzyna1.Text != "" && Set2Druzyna2.Text != "")
            {
                int druzyna1 = Int32.Parse(Set2Druzyna1.Text);
                int druzyna2 = Int32.Parse(Set2Druzyna2.Text);

                int druzyna3 = Int32.Parse(Set1Druzyna1.Text);
                int druzyna4 = Int32.Parse(Set1Druzyna2.Text);

                if ((druzyna1 >= 21 || druzyna2 >= 21) && ((Math.Abs(druzyna1 - druzyna2) == 2) || ((druzyna1 == 21 && druzyna2 <= 19) || (druzyna2 == 21 && druzyna1 <= 19))))
                {
                   
                        Set2Druzyna1.Background = new SolidColorBrush(Colors.White);
                        Set2Druzyna2.Background = new SolidColorBrush(Colors.White);

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

                    Set2Druzyna1.Background = (druzyna1 < druzyna2 || druzyna1 == druzyna2) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
                    Set2Druzyna2.Background =  druzyna1 > druzyna2 ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
                    set3.IsEnabled = false;
                }

              


                }
        }

        private void Set3_KeyUp(object sender, KeyEventArgs e)
        {
            if (Set3Druzyna1.Text != "" && Set3Druzyna2.Text != "")
            {
                int druzyna1 = Int32.Parse(Set3Druzyna1.Text);
                int druzyna2 = Int32.Parse(Set3Druzyna2.Text);

                if ((druzyna1 == 15 && druzyna2 < 15) || (druzyna2 == 15 && druzyna1 < 15))
                {
                    
                        Set3Druzyna1.Background = new SolidColorBrush(Colors.White);
                        Set3Druzyna2.Background = new SolidColorBrush(Colors.White);
                        
                    
                   
                }
                else
                {
                    Set3Druzyna1.Background = (druzyna1 < druzyna2 || druzyna1==druzyna2) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
                    Set3Druzyna2.Background =  druzyna1 > druzyna2 ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
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
