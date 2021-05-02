using System;
using System.Collections.Generic;
using System.Text;

namespace VitaliC_Android.Core.Models
{
    public class NutritionRecord
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RecordType { get; set; }
        public string Date { get; set; }
        public string RecordName { get; set; }

        public List<BasicNutrient> Nutrients { get; set; }
    }
}
