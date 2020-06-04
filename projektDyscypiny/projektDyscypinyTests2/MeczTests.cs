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
    public class MeczTests
    {
        private int punkty_Dr1 =2;
        private int punkty_Dr2 =3;

        [TestMethod()]
        public void getPunkty_Dr1Test()
        {

            Assert.IsTrue(punkty_Dr1 == 2);
        }

        public void getPunkty_Dr2Test()
        {

            Assert.IsNotNull(punkty_Dr2);
        }
    }
}