using System.Collections.Generic;

namespace VitaliC_Android.Core.Models
{
    public class NutritionGoal
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string GoalType { get; set; }
        public List<BasicNutrient> Nutrients { get; set; } = new List<BasicNutrient>();
    }
}