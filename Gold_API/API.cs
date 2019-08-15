using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Gold_API
{
    public class Api
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

        private static List<Gold> GetRecords()
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(data);
            using (MemoryStream stream = new MemoryStream(byteArray))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = false;
                return new List<Gold>(csv.GetRecords<Gold>());
            }
        }
        public class Gold
        {
            public DateTime Date { get; set; }
            public double Price { get; set; }
        }
    }
}
