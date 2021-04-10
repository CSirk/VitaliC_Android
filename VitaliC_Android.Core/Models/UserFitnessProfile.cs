using System;
using System.Collections.Generic;
using System.Text;

namespace VitaliC_Android.Core.Models
{
    public class UserFitnessProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string LastDateTracked { get; set; }
        public string TrackedStreakStart { get; set; }
        public int DaysTracked { get; set; }
        public int DaysTrackedStreak { get; set; }
    }
}
