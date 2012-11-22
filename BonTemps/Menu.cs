using System;
namespace BonTemps
{
    public struct Menu
    {
        private ulong? menuID;
        private string entree;
        private string mainCourse;
        private string dessert;
        private int price;

        public static Menu Null = new Menu(null);
        public ulong? MenuID { get { return this.menuID; } set { this.menuID = value; } }
        public string Entree { get { return this.entree; } set { this.entree = value; } }
        public string MainCourse { get { return this.mainCourse; } set { this.mainCourse = value; } }
        public string Dessert { get { return this.dessert; } set { this.dessert = value; } }
        public int Price { get { return this.price; } set { this.price = value; } }

        public Menu(ulong? menuID)
        {
            this.menuID = menuID;
            this.entree = String.Empty;
            this.mainCourse = String.Empty;
            this.dessert = String.Empty;
            this.price = -1;
        }
        public Menu(ulong menuID, string entree, string mainCourse, string dessert, int price)
            : this(menuID)
        {
            this.entree = entree;
            this.mainCourse = mainCourse;
            this.dessert = dessert;
            this.price = price;
        }
        public override string ToString()
        {
            object[] info = new object[] {
                this.menuID.ToString(), this.entree, this.mainCourse, this.dessert, this.price.ToString()
            };
            return String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n", info);
        }
    }
}
