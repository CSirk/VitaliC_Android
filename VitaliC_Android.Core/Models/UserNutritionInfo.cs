using System;
using System.Collections.Generic;
using System.Text;

namespace VitaliC_Android.Core.Models
{
    public class UserNutritionInfo
    {
        public UserFitnessProfile UserProfile { get; set; } = new UserFitnessProfile();
        public NutritionGoal UserDailyNutritionGoal { get; set; } = new NutritionGoal();
        public NutritionGoal RecommendedNutritionGoal { get; set; } = new NutritionGoal();
        public RecordCollection RecordCollection { get; set; } = new RecordCollection();
        public List<NutrientProgressRecord> NutrientProgressRecords { get; set; } = new List<NutrientProgressRecord>();
        public List<Nutrient> BaseNutrients { get; set; } = new List<Nutrient>();
        public List<NutritionAward> NutritionAwards { get; set; } = new List<NutritionAward>();
    }
}
