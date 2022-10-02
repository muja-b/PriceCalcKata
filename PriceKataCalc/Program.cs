using System;
namespace Kata
{
    class priceCalc
    {
        public static void Main()
        {
            product myProduct = new("ball", 20405, 15.1567,0.20);
            Console.WriteLine($"Product price reported as {myProduct.price} before tax and {myProduct.price+myProduct.calcTax()} after {myProduct.tax*100}% tax. ");

        }
       
    }
    class product{
        public String name;
        public int UPC;
        public double price;
        public double tax;
        public product (String name,int UPC,double price, double tax) { 
            this.name = name; 
            this.UPC = UPC;
            this.price = Math.Round(price, 2);
            this.tax = tax;
            return;
        }
         public double calcTax() {
            var tax = this.price*this.tax;
            return tax;
        } 
            }
}