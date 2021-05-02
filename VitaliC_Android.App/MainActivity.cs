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

namespace VitaliC_Android.App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
        private string[] _listOfPageNames = new string[] { "User Profile", "View Progress", "Nutrition Record Entry" };
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var backingFile = System.IO.Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "UserId.txt");

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


                ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.activity_main_list_item, _listOfPageNames);

                ListView.TextFilterEnabled = true;

                ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
                {
                    switch (args.Position)
                    {
                        case 0:
                            var fitnessProfileIntent = new Intent(this, typeof(UserFitnessProfileActivity));
                            fitnessProfileIntent.PutExtra("userNutritionInfo", JsonConvert.SerializeObject(userNutritionInfo));
                            fitnessProfileIntent.PutExtra("backingFile", backingFile);
                            StartActivity(fitnessProfileIntent);
                            break;
                        case 1:
                            var progressTrackerIntent = new Intent(this, typeof(ProgressTrackerActivity));
                            progressTrackerIntent.PutExtra("userNutritionInfo", JsonConvert.SerializeObject(userNutritionInfo));
                            StartActivity(progressTrackerIntent);
                            break;
                        case 2:
                            var nutritionRecordEntryIntent = new Intent(this, typeof(NutritionRecordEntryActivity));
                            nutritionRecordEntryIntent.PutExtra("userNutritionInfo", JsonConvert.SerializeObject(userNutritionInfo));
                            StartActivity(nutritionRecordEntryIntent);
                            break;
                    }
                    //Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
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