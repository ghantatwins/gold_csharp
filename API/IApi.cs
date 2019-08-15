using System;
using System.Collections.Generic;

namespace API
{
    public interface IApi
    {
        double GetPrice(int index);
        double GetPrice(DateTime date);
        IEnumerable<DateTime> GetDates();
    }
}