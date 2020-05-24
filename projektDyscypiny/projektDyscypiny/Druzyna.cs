using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektDyscypiny
{
    class Druzyna
    {
        private int idDruzyna;
        private String nazwaDruzyny = "";
        private int punkty;
        private static int iloscDruzyn; // ?? ma być statyczna
        private int wygrane; // (iloscWygranych) trzeba było zmienić bo powtarza się z metodą
        private int przegrane;// (iloscPrzegranych)

        public Druzyna(String nazwaDruzyny) { }
        public int getPunkty() { return punkty; }
        public int getID_Druzyna() { return idDruzyna; }
        public String getNazwaDruzyny() { return nazwaDruzyny; }
        public int setPunkty() { return 1; } // do napisania
        public int iloscWygranych() { return 2; } //do napisania/ może zmiana nazwy
        public int iloscPrzegranyc() { return 3; } //do napisania/ może zmiana nazwy
    }
}
