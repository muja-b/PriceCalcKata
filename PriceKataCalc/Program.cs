using System;
using System.Net.Security;

namespace Kata
{
    class priceCalc
    {   
         public static Dictionary<int,double> keysDiscount=new();
           
        public static void Main()
        {
            keysDiscount.Add(12345, 0.12);
            product myProduct = new("ball", 20405, 15.1567, 0.20, 0.15, true);
            print(myProduct);
        }
        
        private static void print(product myProduct)
        {
            Console.WriteLine($"TAX={myProduct.tax * 100}% universal Discount ={myProduct.discount} UPC-discount ={myProduct.calcUPCDiscount}");
            Console.WriteLine($"Tax ammount =${myProduct.price} * {myProduct.tax}={myProduct.calcPriceAfterTax},discount = $ {myProduct.price} *{myProduct.discount}= {myProduct.calcDiscount}, UPC discount ={keysDiscount[myProduct.UPC]}");
            Console.WriteLine($"Program prints price $ {myProduct.calcPriceAfterAllDiscount}");
            Console.WriteLine(value: $"Program reports total discount amount{myProduct.calcAllDiscount()}");
           
        }
    }
    class product
    {
        public String name;
        public int UPC;
        public double price;
        public double discount;
        public bool haveDiscount;
        public double tax;
        public product(String name, int UPC, double price, double tax, double discount, bool haveDiscount)
        {
            this.name = name;
            this.UPC = UPC;
            this.price = Math.Round(price, 2);
            this.discount = discount;
            this.tax = tax;
            this.haveDiscount = haveDiscount;
            return;
        }
        public double calcTax()
        {
            var tax = this.price * this.tax;
            return tax;
        }
        public double calcPriceAfterTax()
        {
            return this.price + this.calcTax();
        }

        public double calcDiscount()
        {
            var myprice= calcPriceAfterTax() *this.discount;
            return myprice;
        }
        public double calcUPCDiscount()
        {
            var myprice = this.price * UPCDiscount();
            return myprice;
        }
        public double calcPriceAfterAllDiscount()
        {
            var myprice = this.price-calcDiscount()-this.calcUPCDiscount(); 
            return myprice;
        }

        private double UPCDiscount()
        {
            if (priceCalc.keysDiscount.ContainsKey(this.UPC)) return priceCalc.keysDiscount[this.UPC];
            else return 0;
        }

        internal object calcAllDiscount()
        {
            return this.calcDiscount() + this.calcUPCDiscount();   
        }
    }
}