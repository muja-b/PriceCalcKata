using System;
namespace Kata
{
    class priceCalc
    {
        public static void Main()
        {
            product myProduct = new("ball", 20405, 15.1567,0.20,0.15);
            Console.WriteLine($"Product price reported as {myProduct.price} before tax and {myProduct.calcPriceAfterTax()} after {myProduct.tax*100}% tax, And with {myProduct.discount*100}% discont its {myProduct.calcPriceAfterDiscount()}  ");

        }
       
    }
    class product{
        public String name;
        public int UPC;
        public double price;
        public double discount;
        public double tax;
        public product (String name,int UPC,double price, double tax,double discount) { 
            this.name = name; 
            this.UPC = UPC;
            this.price = Math.Round(price, 2);
            this.discount = discount;
            this.tax = tax;
            return;
        }
         public double calcTax() {
            var tax = this.price*this.tax;
            return tax;
        }
        public double calcPriceAfterTax() { 
        return this.price + this.calcTax();
        }

        public double calcPriceAfterDiscount() {
            return calcPriceAfterTax() * (this.discount-1);
            }
}