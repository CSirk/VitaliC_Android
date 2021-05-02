using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Newtonsoft.Json;
using VitaliC_Android.Core.Models;
using System.IO;

namespace VitaliC_Android.App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
        private string[] _listOfPageNames = new string[] { "User Profile", "Nutrition Tracker", "Test" };
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.activity_main_list_item, _listOfPageNames);

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                switch(args.Position)
                {
                    case 0:
                        var fitnessProfileIntent = new Intent(this, typeof(UserFitnessProfileActivity));
                        fitnessProfileIntent.PutExtra("userFitnessProfile", JsonConvert.SerializeObject(new UserFitnessProfile()));
                        StartActivity(fitnessProfileIntent);
                        break;
                    case 1:
                        var progressTrackerIntent = new Intent(this, typeof(ProgressTrackerActivity));
                        progressTrackerIntent.PutExtra("progressTrackerIntent", JsonConvert.SerializeObject(new UserFitnessProfile()));
                        StartActivity(progressTrackerIntent);
                        break;
                }
                //Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}