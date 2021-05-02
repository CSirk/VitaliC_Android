using System.Collections.Generic;

namespace VitaliC_Android.Core.Models
{
    public class RecordCollection
    {
        public List<NutritionRecord> UserDailyRecords { get; set; } = new List<NutritionRecord>();
        public List<NutritionRecord> UserSavedRecords { get; set; } = new List<NutritionRecord>();
        public List<NutritionRecord> GlobalSavedRecords { get; set; } = new List<NutritionRecord>();
    }
}