using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using API;
using CsvHelper;
using CsvHelper.Configuration;

namespace Gold_API
{
    public class Api : IApi
    {
        static string data = Gold_API.Resources.data;
        public double GetPrice(int index)
        {
            var records= GetRecords();

            int actualRow = index;
            if (actualRow == records.Count)
            {
                actualRow = index-1;
            }
            if (actualRow < 0)
            {
                throw new InvalidDataException("Index Can't be negative");
            }
            if (actualRow >= records.Count)
            {
                throw new InvalidDataException("Index Can't be more than "+(records.Count-1));
            }

            return records[actualRow].Price;
        }

        public double GetPrice(DateTime date)
        {
            var records = GetRecords();

            var record = new List<Gold>(records.Where(x => x.Date.CompareTo(date) == 0));
            if (!record.Any())
            {
                throw new InvalidDataException("No Date " + date.ToShortDateString() + " has been found");
            }
            if (record.Count>1)
            {
                throw new InvalidDataException("Duplicate Dates " + date.ToShortDateString() + " has been found");
            }

            return record[0].Price;
        }

        public IEnumerable<DateTime> GetDates()
        {
             return GetRecords().Select(x => x.Date);
        }

        private static List<Gold> GetRecords()
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(data);
            using (MemoryStream stream = new MemoryStream(byteArray))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<GoldMap>();
                return new List<Gold>(csv.GetRecords<Gold>());
            }
        }
        public class Gold
        {
            public DateTime Date { get; set; }
            public double Price { get; set; }
        }
        public sealed class GoldMap : ClassMap<Gold>
        {
            public GoldMap()
            {
                Map(m => m.Date).TypeConverterOption.Format("MMM-yy");
                Map(m => m.Price);
            }
        }
    }
}
