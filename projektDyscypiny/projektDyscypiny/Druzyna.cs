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
        private int punkty = 0;
        private int wygrane = 0; 
        private int przegrane = 0;

        public Druzyna() { }
        public Druzyna(string nazwaDruzyny, int id)
        {
            this.nazwaDruzyny = nazwaDruzyny;
            this.idDruzyna = id;
        }

        public Druzyna(string nazwaDruzyny)
        {
            this.nazwaDruzyny = nazwaDruzyny;
        }


        public int getPunkty() { return punkty; }
        public int getID_Druzyna() { return idDruzyna; }
        public string getNazwaDruzyny() { return nazwaDruzyny; }
        public int punktyDogrywka() { return this.punkty ++; } 
        public void iloscWygranych() { wygrane ++; } 
        public int iloscPrzegranych() { return this.przegrane + 1; } 
        public int getWygrane() { return wygrane; }
        public void setWygrane(int wygrane) { this.wygrane = wygrane; }

    }
}
