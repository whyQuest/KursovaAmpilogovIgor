using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ecosystem;

namespace TestEcosystem
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Ecotest()
        {
            int[,] net = new int[,]
            {
                { 1, 2, 3, 4, 5 },
                { 6, 7, 8, 9, 10 },
                { 11, 12, 13, 14, 15 },
                { 16, 17, 18, 19, 20 },
                { 21, 22, 23, 24, 25 }
            };
            int m = 5, n = 5;


                Nets nets = new Nets(m, n);
                Wolf wolf = new Wolf(5, 5, nets);
            Rabbit rabbit = new Rabbit(2, 3, nets);
            Carrot carrot = new Carrot(3, 3, nets);

            bool place = false;
            bool eat = false;
            int newX = 3;
            int newY = 3;
            rabbit.Analiz(2, 3, ref newX, ref newY, ref place, ref eat);
            rabbit.Move();
            CellsOK cell = new CellsOK(3, 3);
            cell.Delete();


            Rabbit rabbitcheck = new Rabbit(3, 3, nets);

            Assert.AreEqual(rabbit.Y, rabbitcheck.Y);
        }
    }
    }
