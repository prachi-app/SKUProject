using System;
using System.Collections.Generic;


namespace SKUPrice
{
    class Program
    {

        public class Order
        {
            
            public List<string> SKUItem;
            private List<double> UnitCost;
            private List<double> UnitCostPercentage;
            private List<double> SaleOrderPrice;

            private double TotalUnitCost;
            private double Groupprice;
            private double LastItemPrice;

            public Order()
            {
                SKUItem = new List<string>();
                UnitCost = new List<double>();
                UnitCostPercentage = new List<double>();
                SaleOrderPrice = new List<double>();
                TotalUnitCost = 0;
                Groupprice = 0;
                LastItemPrice = 0;
            }
            private void ParseSKUItem(string GroupName)
            {
                string[] str = GroupName.Split("-");
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] != "G")
                    {
                        SKUItem.Add(str[i]);
                        
                    }
                }
            }
            public void getUnitCost(string strGrp)
            {
                ParseSKUItem(strGrp);
                
                foreach (var item in SKUItem)
                {

                    UnitCost.Add(SampleDictinoryData.GetPrice(item));
                    
                }
            }
            public double CalculateTotalUnitCost() {

                
                foreach (var item in UnitCost)
                {
                    TotalUnitCost = TotalUnitCost + item;
                }
                
                return TotalUnitCost;

            }
            public void getPercentageUnitCost() {


                for (int i = 0; i < UnitCost.Count - 1; i++)
                {
                    

                    UnitCostPercentage.Add(Math.Round((UnitCost[i] / TotalUnitCost), 2) * 100);//round off 
                    

                }


            }
            public double getGroupPrice() {


                for (int i = 0; i < SaleOrderPrice.Count; i++)
                {
                    Groupprice = SaleOrderPrice[i] + Groupprice;
                }

                return Math.Round((Groupprice),2);
            }

            public void CalculateSOPrice(double price)
            {
                for (int i = 0; i < UnitCost.Count - 1; i++)
                {
                    SaleOrderPrice.Add(Math.Round(price * (UnitCostPercentage[i] / 100), 2));
                }

                
                
            }

            //Calculate the last Item Price
            public double getLastItemSOPrice(double price) {

                LastItemPrice = Math.Round(price - Groupprice, 2);

                return LastItemPrice;



            }
            
            //Method to take GroupOrder and price .
            //it will return object with list of SKU and groupPrice;
            public ItemwithGroupPrice ProcessOrder(string Order,double Price)
            {
                
                ItemwithGroupPrice Itemobj = new ItemwithGroupPrice();

                getUnitCost(Order);
                CalculateTotalUnitCost();
                getPercentageUnitCost();
                CalculateSOPrice(Price);
               
                Itemobj.groupPrice = getGroupPrice();
                foreach(var sku in SKUItem)
                {
                    Itemobj.SKU.Add(sku);
                }
                
                return Itemobj;
            }

        }
            
        
        public class ItemwithGroupPrice
        {
            public List<string> SKU;
            public double groupPrice;

            public ItemwithGroupPrice()
            {
                SKU = new List<string>();
                groupPrice = 0;
            }
        }

        //Global Dictionary to fetch Price against SKU
        static class SampleDictinoryData
        {
            static Dictionary<string, double> SampleData = new Dictionary<string, double>
            { 
               { "208",6.82},
               {"225",1.94},
               {"237",1.73},
               {"258",8.02},
               { "214",3.92},
               { "215",5.68},
               { "219",5.23}
            };
            public static double GetPrice(string word)
            {
                // Try to get the result in the static Dictionary
                double result;
                if (SampleData.TryGetValue(word, out result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
        }
        static void Main(string[] args)
        {
            
            Order SaleOrder = new Order();
            
       
            ItemwithGroupPrice ItemObject = new ItemwithGroupPrice();

            Console.WriteLine("Enter the SKU Group:");
            string SKUgroup = Console.ReadLine();

            
            Console.WriteLine("Enter the price:");
            double pricetosend = Convert.ToDouble(Console.ReadLine());


            
            ItemObject = SaleOrder.ProcessOrder(SKUgroup,pricetosend);

            Console.WriteLine("Item SKU :");
            foreach (var item in ItemObject.SKU)
            {
                Console.WriteLine(item);

            }
            Console.WriteLine("Group price for the Item :");
            Console.WriteLine(ItemObject.groupPrice);
            
                      


        }
    }
}
