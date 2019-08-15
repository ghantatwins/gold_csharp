using System;
using System.Collections.Generic;
using System.Linq;
using API;

namespace Buy_Sell_Gold
{
    public class ProfitMaker
    {
        
        public double GetProfitValue(IApi api)
        {
            var dates = new List<DateTime>(api.GetDates());
            List<double> buyHeap=new List<double>();
            List<double> sellHeap= new List<double>();
            foreach (var date in dates)
            {
                var price = api.GetPrice(date);
                if (buyHeap.Count == 0)
                {
                    buyHeap.Add(price);
                }

                else if (price > buyHeap[buyHeap.Count-1])
                {
                    if (sellHeap.Count < buyHeap.Count)
                    {
                        sellHeap.Add(price);
                    }
                    else if (sellHeap.Count == buyHeap.Count)
                    {
                        var root = sellHeap[sellHeap.Count-1];
                        if (root < price)
                        {
                            sellHeap.RemoveAt(sellHeap.Count - 1);
                            buyHeap.Add(root);
                        }

                        else if (buyHeap.Count < dates.Count / 2)
                        {
                            sellHeap.Add(price);
                        }
                    }
                }
                else
                {
                    buyHeap.RemoveAt(buyHeap.Count - 1);
                    buyHeap.Add(price);
                }
            }
            return sellHeap.Sum()-buyHeap.Sum();
        }
    }
}
