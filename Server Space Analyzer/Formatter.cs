using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Space_Analyzer
{
   class Formatter
    {
        private static readonly int B_PER_KB = 1024;

        public Formatter()
        {

        }

        public string format(string the_volume, string the_name, string the_total, string the_free)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(formatCapacity(the_free));
            builder.Append(" ");
            builder.Append(formatCapacity(the_total));
            builder.Append(" ");
            builder.Append(formatCapacity(the_free));
            return builder.ToString();
        }

        public string formatName(string the_volume, string the_name)
        {
            StringBuilder builder = new StringBuilder();
            if (!"".Equals(the_volume))
            {
                builder.Append(the_volume);
                builder.Append(" ");
            }
            builder.Append("(");
            builder.Append(the_name);
            builder.Append(")");
            return builder.ToString();
        }

        public string formatCapacity(string the_capacity)
        {
            double capacity;
            int decimal_points;
            string unit;
            double capacity_in_b = System.Convert.ToDouble(the_capacity);
            double capacity_in_kb = capacity_in_b / B_PER_KB;
            double capacity_in_mb = capacity_in_kb / B_PER_KB;
            double capacity_in_gb = capacity_in_mb / B_PER_KB;
            if (capacity_in_gb < 1)
            {
                capacity = capacity_in_mb;
                decimal_points = 0;
                unit = "MB";
            }
            else
            {
                capacity = capacity_in_gb;
                decimal_points = 2;
                unit = "GB";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(Math.Round(capacity, decimal_points));
            builder.Append(" ");
            builder.Append(unit);
            return builder.ToString();
        }
    }
}
