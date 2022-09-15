using System;
using System.Net.Security;

namespace Kata
{
    class priceCalc
    {   
         public static Dictionary<int,Discount> keysDiscount=new();
           
        public static void Main()
        {
            Discount myDiscount = new Discount(true, false, 0.2);
            Discount myUPCDiscount = new Discount(false,true, 0.3);
            keysDiscount.Add(12345,myUPCDiscount);
            List<cost>costs= new List<cost>();
            costs.Add(new cost(true,.1,0));
            costs.Add(new cost(true,0,2.4));
            costs.Add(new cost(true,0,2.0));
            costs.Add(new cost(true,0,2.8));
            product myProduct = new("ball", 12345, 15.1567, 0.20, myDiscount, true,costs);
            print(myProduct);
        }
        
        private static void print(product myProduct)
        {
            Console.WriteLine($"TAX={myProduct.tax * 100}% Discount ={myProduct.discount.discountValue} UPC-discount ={myProduct.calcUPCDiscount()}");
            Console.WriteLine($"Cost:{myProduct.calcCosts()}");
            Console.WriteLine($"Tax ammount =${myProduct.price} * {myProduct.tax}={myProduct.calcPriceAfterTax()},discount = $ {myProduct.price} *{myProduct.discount}= {myProduct.calcDiscount()}, UPC discount ={keysDiscount[myProduct.UPC].discountValue}");
            Console.WriteLine($"Program prints price $ {myProduct.calcPriceAfterAllDiscount()}");
            Console.WriteLine(value: $"Program reports total discount amount {myProduct.calcAllDiscount()}");
           
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
        public product(String name, int UPC, double price, double tax, Discount discount, bool haveDiscount,List<cost>costs)
        {
            this.name = name;
            this.UPC = UPC;
            this.price = Math.Round(price, 2);
            this.discount = discount;
            this.tax = tax;
            this.haveDiscount = haveDiscount;
            this.Costs = costs;
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
                if (Discount.CombiningAddictive)
                {
                    myprice = (this.price - this.calcDiscount()) * this.discount.discountValue;

                }
                else
                {
                    myprice = (this.price - this.calcDiscount()) * this.discount.discountValue;
                }
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

        public double calcAllDiscount()
        {
            return this.calcDiscount() + this.calcUPCDiscount();   
        }

        public double calcCosts()
        {
            double sum = 0;
            foreach(cost i in this.Costs)
            {
                if (i.isPercentage) sum += this.price;
                else sum += i.FlatCost;
            }
            return sum;
        }
    }
     class Discount
    {
        public bool beforeTax { set; get; }
        public static bool  CombiningAddictive { get; set; }
        public double discountValue { set; get; }
        public Discount(bool beforeTax,bool Combining, double discountValue)
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
                this.percentage =Math.Round(percentage,2);
                FlatCost = flatCost;
            }
        public double calcCosts(product myproduct)
        {
            if (this.isPercentage)
            {
                return myproduct.price * this.percentage;
            }else
            {
                return this.FlatCost;
            }
        }
        }

}