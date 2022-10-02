using System;
using System.Net.Security;

namespace Kata
{
    class priceCalc
    {   
         public static Dictionary<int,Discount> keysDiscount=new();
           
        public static void Main()
        {
            Discount myDiscount = new Discount(true, 0.2);
            Discount myUPCDiscount = new Discount(false, 0.3);

            keysDiscount.Add(12345,myUPCDiscount);
            product myProduct = new("ball", 12345, 15.1567, 0.20, myDiscount, true);
            print(myProduct);
        }
        
        private static void print(product myProduct)
        {
            Console.WriteLine($"TAX={myProduct.tax * 100}% universal Discount ={myProduct.discount} UPC-discount ={myProduct.calcUPCDiscount()}");
            Console.WriteLine($"Tax ammount =${myProduct.price} * {myProduct.tax}={myProduct.calcPriceAfterTax()},discount = $ {myProduct.price} *{myProduct.discount}= {myProduct.calcDiscount()}, UPC discount ={keysDiscount[myProduct.UPC].discountValue}");
            Console.WriteLine($"Program prints price $ {myProduct.calcPriceAfterAllDiscount()}");
            Console.WriteLine(value: $"Program reports total discount amount{myProduct.calcAllDiscount()}");
           
        }
    }
    class product
    {
        public String name;
        public int UPC;
        public double price;
        public Discount discount;
        public bool haveDiscount;
        public double tax;
        public product(String name, int UPC, double price, double tax, Discount discount, bool haveDiscount)
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
            double myprice;
            if (this.discount.beforeTax)
            {
            myprice= this.price *this.discount.discountValue;
            }
            else
            {
            myprice= calcPriceAfterTax() *this.discount.discountValue;
            }
            return myprice;
        }
        public double calcUPCDiscount()
        {
            double myprice;
            if (priceCalc.keysDiscount[this.UPC].beforeTax)
            {
                myprice = this.price * this.discount.discountValue;
            }
             myprice = this.price * UPCDiscount();
            return myprice;
        }
        public double calcPriceAfterAllDiscount()
        {
            var myprice = this.price-calcDiscount()-this.calcUPCDiscount(); 
            return myprice;
        }

        private double UPCDiscount()
        {
            if (priceCalc.keysDiscount.ContainsKey(this.UPC)) return priceCalc.keysDiscount[this.UPC].discountValue;
            else return 0;
        }

        internal object calcAllDiscount()
        {
            return this.calcDiscount() + this.calcUPCDiscount();   
        }
    }
     class Discount
    {
        public bool beforeTax { set; get; }
        public double discountValue { set; get; }
        public Discount(bool beforeTax, double discountValue)
        {
            this.beforeTax = beforeTax;
            this.discountValue = discountValue;
        }
    }
}