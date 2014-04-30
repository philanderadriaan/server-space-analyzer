using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Disk_Space_Analyzer
{
    public class Formatter
    {
        private static readonly int B_PER_KB = 1024;

        public Formatter()
        {

        }

        public String format(String the_volume, String the_name, String the_total, String the_free)
        {
            String name = formatName(the_volume, the_name);
            String total = formatCapacity(the_total);
            String free = formatCapacity(the_free);
            StringBuilder string_builder = new StringBuilder();
            string_builder.Append(name);
            string_builder.Append(" ");
            string_builder.Append(total);
            string_builder.Append(" ");
            string_builder.Append(free);
            String formatted_string = string_builder.ToString();
            return formatted_string;
        }

        public String formatName(String the_volume, String the_name)
        {
            Boolean has_volume = !"".Equals(the_volume);
            StringBuilder string_builder = new StringBuilder();
            if (has_volume)
            {
                string_builder.Append(the_volume);
                string_builder.Append(" ");
            }
            string_builder.Append("(");
            string_builder.Append(the_name);
            string_builder.Append(")");
            String formatted_name = string_builder.ToString();
            return formatted_name;
        }

        public String formatCapacity(String the_capacity)
        {
            double capacity;
            int decimal_points;
            String unit;
            double capacity_in_b = System.Convert.ToDouble(the_capacity);
            double capacity_in_kb = capacity_in_b / B_PER_KB;
            double capacity_in_mb = capacity_in_kb / B_PER_KB;
            double capacity_in_gb = capacity_in_mb / B_PER_KB;
            Boolean below_one_gb = capacity_in_gb < 1;
            if (below_one_gb)
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
            double capacity_output = Math.Round(capacity, decimal_points);
            StringBuilder string_builder = new StringBuilder();
            string_builder.Append(capacity_output);
            string_builder.Append(" ");
            string_builder.Append(unit);
            String formatted_capacity = string_builder.ToString();
            return formatted_capacity;
        }
    }
}
