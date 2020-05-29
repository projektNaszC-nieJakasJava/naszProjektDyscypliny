using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektDyscypiny
{
    public class Druzyna
    {


        private int idDruzyna;
        private string nazwaDruzyny = "";
        private int punkty;
        private int wygrane; 
        private int przegrane;

        public Druzyna(string nazwaDruzyny, int id)
        {
            this.nazwaDruzyny = nazwaDruzyny;
            this.idDruzyna = id;
        }
        


        public int getPunkty() { return punkty; }
        public int getID_Druzyna() { return idDruzyna; }
        public string getNazwaDruzyny() { return nazwaDruzyny; }
        public int setPunkty() { return 1; } 
        public int iloscWygranych() { return 2; } 
        public int iloscPrzegranyc() { return 3; } 
    }
}
