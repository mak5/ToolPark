using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Tool
    {
        public string SerialNumber { get; set; }

        public string Label { get; set; }

        public string ImageUrl { get; set; }

        public DateTime NextInspectionDate { get; set; }
    }
}
