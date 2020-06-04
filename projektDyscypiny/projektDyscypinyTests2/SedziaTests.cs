using Microsoft.VisualStudio.TestTools.UnitTesting;
using projektDyscypiny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektDyscypiny.Tests
{
    [TestClass()]
    public class SedziaTests
    {
        [TestMethod()]
        public void getID_SedziaTest()
        {
            Sedzia druzyna = new Sedzia("IMIE", "NAZWISKO", 3);
            Assert.IsTrue(druzyna.getImie_Sedzia() == "IMIE");
        }

        [TestMethod()]
        public void getNazwisko_Sedzia()
        {
            Sedzia druzyna = new Sedzia("IMIE", "NAZWISKO", 3);
            Assert.IsNotNull(druzyna.getNazwisko_Sedzia());
        }

        [TestMethod()]
        public void getID_Sedzia()
        {
            Sedzia druzyna = new Sedzia("IMIE", "NAZWISKO", 3);
            Assert.IsTrue(druzyna.getID_Sedzia() == 3);
           
        }


      
    }
}