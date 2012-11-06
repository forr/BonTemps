using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public struct Menus
    {
        private ulong? menuID;
        private string entree;
        private string mainCourse;
        private string dessert;
        private int price;

        public ulong? MenuID { get { return this.menuID; } }
        public string Entree { get { return this.entree; } }
        public string MainCourse { get { return this.mainCourse; } }
        public string Dessert { get { return this.dessert; } }
        public int Price { get { return this.price; } }

        public Menus(ulong menuID, string entree, string mainCourse, string dessert, int price)
        {
            this.menuID = menuID;
            this.entree = entree;
            this.mainCourse = mainCourse;
            this.dessert = dessert;
            this.price = price;
        }
    }
}
