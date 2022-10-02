using System;
namespace Kata
{
    class priceCalc
    {
        public static void Main()
        {
            product myProduct = new("ball", 20405, 15.1567, 0.20, 0.15, true);
            Console.WriteLine($"Product price reported as {myProduct.price} before tax and {myProduct.calcPriceAfterTax()} after {myProduct.tax * 100}% tax  ");
            if (myProduct.haveDiscount)
            {
                Console.WriteLine($", And with {myProduct.discount * 100}% discont its {myProduct.calcPriceAfterDiscount()}");
            }
            else
            {
                Console.WriteLine("Program doesn’t show any discounted amount.");

            }
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

        public double calcPriceAfterDiscount()
        {
            return calcPriceAfterTax() * (1 - this.discount);
        }

    }
}