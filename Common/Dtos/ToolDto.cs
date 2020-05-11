using System;

namespace Common.Dtos
{
    public class ToolDto
    {
        public string SerialNumber { get; set; }

        public string CurrentSerialNumber { get; set; }

        public string Label { get; set; }

        public string ImageUrl { get; set; }

        public DateTime NextInspectionDate { get; set; }
    }
}