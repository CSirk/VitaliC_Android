using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using VitaliC_Android.Core.Models;

namespace VitaliC_Android.App
{
    [Activity(Label = "UserFitnessProfileActivity")]
    public class UserFitnessProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_user_fitness_profile);

            var backingFile = Intent.GetStringExtra("backingFile");
            var userNutritionInfo = JsonConvert.DeserializeObject<UserNutritionInfo>(Intent.GetStringExtra("userNutritionInfo"));

            TextView userIdTextView = FindViewById<TextView>(Resource.Id.userIdTextView);
            TextView userAgeTextView = FindViewById<TextView>(Resource.Id.userAgeTextView);
            Button logOutButton = FindViewById<Button>(Resource.Id.logOutButton);

            userIdTextView.Text = userNutritionInfo.UserProfile.UserId.ToString();
            userAgeTextView.Text = userNutritionInfo.UserProfile.Age.ToString();

            logOutButton.Click += (sender, e) =>
            {
                File.Delete(backingFile);

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
        }
    }
}