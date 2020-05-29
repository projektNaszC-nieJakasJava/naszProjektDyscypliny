using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektDyscypiny
{  
    public class Sedzia
        {
            private string imieSedzia;
            private string nazwiskoSedzia;
            private int idSedzia ;          

            public Sedzia(string imieSedzia, string nazwiskoSedzia, int sedzia )
            {
                this.imieSedzia = imieSedzia;
                this.nazwiskoSedzia = nazwiskoSedzia;
                this.idSedzia = sedzia;
               

            }
            public int getID_Sedzia() { return idSedzia; }
            public string getImie_Sedzia() { return imieSedzia; }
            public string getNazwisko_Sedzia() { return nazwiskoSedzia; }
        }
    
}
