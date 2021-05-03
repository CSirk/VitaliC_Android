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

namespace VitaliC_Android.App
{
    [Activity(Label = "UsernameEntryActivity")]
    public class UserIdEntryActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_user_id_entry);
            var backingFile = Intent.GetStringExtra("backingFile");

            EditText usernameTextBox = FindViewById<EditText>(Resource.Id.userIdTextBox);
            Button submitUsernameButton = FindViewById<Button>(Resource.Id.submitUserIdButton);

            submitUsernameButton.Click += async (sender, e) =>
            {
                using (var writer = System.IO.File.CreateText(backingFile))
                {
                    await writer.WriteAsync(usernameTextBox.Text);
                }

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            // Create your application here
        }
    }
}