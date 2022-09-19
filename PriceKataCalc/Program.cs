using System;
using System.Net.Security;
using System.Text.RegularExpressions;

namespace Kata
{
    class priceCalc
    {
        public static Dictionary<int, Discount> keysDiscount = new();
        public static void Main()
        {
            Discount myDiscount, myUPCDiscount;
            GenerateDiscounts(out myDiscount, out myUPCDiscount);
            keysDiscount.Add(12345, myUPCDiscount);
            List<string> Curr = GetCurrencies();
            product myProduct = new("ball", 12345, 15.1567, 0.20, myDiscount, true, FillCosts(), Curr[0]);
            print(myProduct);
        }

        private static List<string> GetCurrencies()
        {
            return new() { "NIS", "USD", "JRD" };
        }

        private static void GenerateDiscounts(out Discount myDiscount, out Discount myUPCDiscount)
        {
            Discount.CAP = new cost(false, 0, 0.1);
            myDiscount = new Discount(true, false, 0.2);
            myUPCDiscount = new Discount(false, true, 0.3);
        }

        private static List<cost> FillCosts()
        {
            List<cost> costs = new List<cost>();
            costs.Add(new cost(true, .1, 0));
            costs.Add(new cost(true, 0, 2.4));
            costs.Add(new cost(true, 0, 2.0));
            costs.Add(new cost(true, 0, 2.8));
            return costs;
        }

        private static void print(product myProduct)
        {
            Console.WriteLine($"TAX={ myProduct.tax * 100}% Discount ={myProduct.discount.discountValue} UPC-discount ={myProduct.calcUPCDiscount()}");
            Console.WriteLine($"Cost:{prescision.ParseFor2Dicimal( myProduct.calcCosts())}");
            Console.WriteLine($"Tax ammount ={myProduct.Currency}{myProduct.price} * {myProduct.tax}={prescision.ParseFor2Dicimal( myProduct.calcPriceAfterTax())},discount = {myProduct.Currency} {myProduct.price} *{myProduct.discount}= {prescision.ParseFor2Dicimal( myProduct.calcDiscount())}, UPC discount ={keysDiscount[myProduct.UPC].discountValue}");
            Console.WriteLine($"Program prints price {myProduct.Currency} {prescision.ParseFor2Dicimal(myProduct.calcPriceAfterAllDiscount())}");
            Console.WriteLine(value: $"Program reports total discount amount {myProduct.Currency} {prescision.ParseFor2Dicimal( myProduct.calcAllDiscount())}");

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
        public List<cost> Costs;
        public string Currency;
            public product(String name, int UPC, double price, double tax, Discount discount, bool haveDiscount, List<cost> costs, string currency)
        {
            this.name = name;
            this.UPC = UPC;
            this.price = Math.Round(price, 2);
            this.discount = discount;
            this.tax = tax;
            this.haveDiscount = haveDiscount;
            this.Costs = costs;
            this.Currency = currency;
        }
        public double calcTax()
        {
            var tax = prescision.ParseFor4Dicimal(this.price * this.tax);
            return tax;
        }
        public double calcPriceAfterTax()
        {
            return prescision.ParseFor4Dicimal(this.price + this.calcTax());
        }

        public double calcDiscount()
        {
            double myprice;
            if (this.discount.beforeTax)
            {
                myprice = this.price * this.discount.discountValue;
                myprice = Limit(myprice);
                myprice = prescision.ParseFor4Dicimal(myprice);
            }
            else
            {
                myprice = calcPriceAfterTax() * this.discount.discountValue;
                myprice = Limit(myprice);
                myprice = prescision.ParseFor4Dicimal(myprice);
            }
            return myprice;
        }

        private double Limit(double myprice)
        {
            if (!Discount.CAP.isPercentage)
            {
                if (myprice > Discount.CAP.FlatCost) myprice = Discount.CAP.FlatCost;
            }
            else
            {
                if (myprice > Discount.CAP.percentage * this.price) myprice = Discount.CAP.percentage * this.price;
            }

            return myprice;
        }

        public double calcUPCDiscount()
        {
            double myprice;
            if (priceCalc.keysDiscount[this.UPC].beforeTax)
            {
                if (Discount.CombiningAddictive)
                {
                    myprice = (this.price - this.price * UPCDiscount());
                    myprice = Limit(myprice);
                    myprice = prescision.ParseFor4Dicimal(myprice);
                }
                else
                {
                    myprice = (this.price - this.price * UPCDiscount());
                    myprice = Limit(myprice);
                    myprice = prescision.ParseFor4Dicimal(myprice);
                }
            }
            else
                myprice = this.price * UPCDiscount();
                myprice=Limit(myprice);
                myprice = prescision.ParseFor4Dicimal(myprice);
            return myprice;
        }
        public double calcPriceAfterAllDiscount()
        {
            var myprice = prescision.ParseFor4Dicimal( this.price - calcDiscount() - this.calcUPCDiscount());
            return myprice;
        }

        private double UPCDiscount()
        {
            if (priceCalc.keysDiscount.ContainsKey(this.UPC)) return priceCalc.keysDiscount[this.UPC].discountValue;
            else return 0;
        }

        public double calcAllDiscount()
        {
            return prescision.ParseFor4Dicimal( this.calcDiscount() + this.calcUPCDiscount());
        }

        public double calcCosts()
        {
            double sum = 0;
            foreach (cost i in this.Costs)
            {
                if (i.isPercentage) sum += this.price;
                else sum += i.FlatCost;
            }
            return prescision.ParseFor4Dicimal( sum);
        }
    }
    class Discount
    {
        public bool beforeTax { set; get; }
        public static bool CombiningAddictive { get; set; }
        public static cost? CAP;
        public double discountValue { set; get; }
        public Discount(bool beforeTax, bool Combining, double discountValue)
        {
            this.beforeTax = beforeTax;
            CombiningAddictive = Combining;
            this.discountValue = discountValue;
        }
    }
    class cost
    {
        public bool isPercentage;
        public double percentage;
        public double FlatCost;
        public cost(bool isPercentage, double percentage, double flatCost)
        {
            this.isPercentage = isPercentage;
            this.percentage = Math.Round(percentage, 2);
            FlatCost = flatCost;
        }
        public double calcCosts(product myproduct)
        {
            if (this.isPercentage)
            {
                return myproduct.price * this.percentage;
            }
            else
            {
                return this.FlatCost;
            }
        }
    }
    public class prescision
    {
        public static double ParseFor4Dicimal(double input)
        {
            return Math.Round(input, 4);
        }
        public static double ParseFor2Dicimal(double input)
        {
            return Math.Round(input, 2);
        }
    }

}