using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Newtonsoft.Json;
using VitaliC_Android.Core.Models;
using System.IO;
using VitaliC_Android.Core.Helpers;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace VitaliC_Android.App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private string[] _listOfPageNames = new string[] { "User Profile", "View Progress", "Nutrition Record Entry", "View/Edit Nutrition Goals" };
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var backingFile = System.IO.Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "UserId.txt");

        SetContentView(Resource.Layout.activity_main);

            //Check for userId file, and if it doesnt exist show the entry view
            if (!File.Exists(backingFile))
            {
                var userIdEntryIntent = new Intent(this, typeof(UserIdEntryActivity));
                userIdEntryIntent.PutExtra("backingFile", backingFile);
                StartActivity(userIdEntryIntent);
            }
            else
            {
                var userId = "";
                using (var reader = new StreamReader(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "UserId.txt")))
                {
                    userId = reader.ReadToEnd();
                }

                var httpHelper = new HttpHelper<UserNutritionInfo>();
                var userNutritionInfo = await httpHelper.GetAsync
                    ($"https://streetsofsmashvilleapi.azurewebsites.net/api/NutritionTrackerApp/GetNutritionInfoByUserId?userId={userId}&today=true");
                var userNutritionInfoJson = JsonConvert.SerializeObject(userNutritionInfo);

                var progressBar = FindViewById<ProgressBar>(Resource.Id.loadingProgressBar);
                progressBar.Visibility = Android.Views.ViewStates.Invisible;

                var listView = FindViewById<ListView>(Resource.Id.menuListView);
                listView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.main_list_item, _listOfPageNames);
                listView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
                {
                    switch (args.Position)
                    {
                        case 0:
                            var fitnessProfileIntent = new Intent(this, typeof(UserFitnessProfileActivity));
                            fitnessProfileIntent.PutExtra("userNutritionInfo", userNutritionInfoJson);
                            fitnessProfileIntent.PutExtra("backingFile", backingFile);
                            StartActivity(fitnessProfileIntent);
                            break;
                        case 1:
                            var progressTrackerIntent = new Intent(this, typeof(ProgressTrackerActivity));
                            progressTrackerIntent.PutExtra("userNutritionInfo", userNutritionInfoJson);
                            StartActivity(progressTrackerIntent);
                            break;
                        case 2:
                            var nutritionRecordEntryIntent = new Intent(this, typeof(NutritionRecordEntryActivity));
                            nutritionRecordEntryIntent.PutExtra("userNutritionInfo", userNutritionInfoJson);
                            StartActivity(nutritionRecordEntryIntent);
                            break;
                        case 3:
                            var nutritionGoalIntent = new Intent(this, typeof(NutritionGoalActivity));
                            nutritionGoalIntent.PutExtra("userNutritionInfo", userNutritionInfoJson);
                            StartActivity(nutritionGoalIntent);
                            break;
                    }
                };
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}