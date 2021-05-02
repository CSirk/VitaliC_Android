namespace VitaliC_Android.Core.Models
{
    public class NutritionAward
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public string Description { get; set; }
        public int MinimumPercentValue { get; set; }
        public int MaximumPercentValue { get; set; }
        public int BlackCount { get; set; }
        public int BronzeCount { get; set; }
        public int SilverCount { get; set; }
        public int GoldCount { get; set; }
        public int PlatinumCount { get; set; }
        public int DiamondCount { get; set; }
    }
}