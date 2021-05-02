namespace VitaliC_Android.Core.Models
{
    public class Nutrient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Measurement { get; set; }
        public string MeasurementAbbreviation { get; set; }
        public bool GoalIsHigh { get; set; }
        public string Alias { get; set; }
        public int DisplayOrder { get; set; }
        public float Amount { get; set; }
        public string DisplayName { get; set; }
    }
}