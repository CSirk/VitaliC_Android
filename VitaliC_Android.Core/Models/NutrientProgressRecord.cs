namespace VitaliC_Android.Core.Models
{
    public class NutrientProgressRecord
    {
        public string NutrientName { get; set; }
        public string Measurement { get; set; }
        public string MeasurementAbbreviation { get; set; }
        public bool GoalIsHigh { get; set; }
        public string Alias { get; set; }
        public int DisplayOrder { get; set; }
        public string DisplayName { get; set; }

        public float ProgressAmount { get; set; }
        public float GoalAmount { get; set; }
        public float RemainingAmount { get; set; }
    }
}