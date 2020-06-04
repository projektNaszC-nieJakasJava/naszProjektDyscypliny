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
    public class DruzynaTests
    {
        public int punkty = 5;
        public int wygrane = 1;
        public int przegrane = 0;


        [TestMethod()]
        public void getNazwaDruzynyTest()
        {
            Druzyna druzyna = new Druzyna("TEST", 4);
            Assert.IsTrue(druzyna.getNazwaDruzyny() == "TEST");
        }


        [TestMethod()]
        public void getID_Druzyna()
        {
            Druzyna druzyna = new Druzyna("TEST", 4);
            Assert.IsTrue(druzyna.getID_Druzyna() == 4);
        }

        [TestMethod()]
        public void getPunkty()
        {

            Druzyna druzyna = new Druzyna("TEST", 4);


            Assert.IsTrue(punkty == 5);
        }

        [TestMethod()]
        public void iloscWygranychTest()
        {
            
            Druzyna druzyna = new Druzyna("TEST", 4);
            Assert.IsTrue(wygrane == 1);
        }

        [TestMethod()]
        public void iloscPrzegranychTest()
        {
            Druzyna druzyna = new Druzyna("TEST", 4);
            
            Assert.IsNotNull(druzyna.iloscPrzegranych());
        }
    }
}