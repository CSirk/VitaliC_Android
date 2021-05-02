using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using VitaliC_Android.Core.Models;

namespace VitaliC_Android.App
{
    [Activity(Label = "Activity1")]
    public class ProgressTrackerActivity : Activity
    {
        RecyclerView recylcerView;
        RecyclerView.LayoutManager layoutManager;
        ProgressTrackerAdapter adapter;
        List<NutrientProgressRecord> progressRecords;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var userNutritionInfo = JsonConvert.DeserializeObject<UserNutritionInfo>(Intent.GetStringExtra("userNutritionInfo"));

            progressRecords = userNutritionInfo.NutrientProgressRecords;

            SetContentView(Resource.Layout.progress_tracker);

            recylcerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            layoutManager = new LinearLayoutManager(this);

            //layoutManager = new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false);

            recylcerView.SetLayoutManager(layoutManager);

            adapter = new ProgressTrackerAdapter(progressRecords);

            recylcerView.SetAdapter(adapter);

        }
    }

    public class ProgressTrackerViewHolder : RecyclerView.ViewHolder
    {
        public TextView NutrientName { get; set; }
        public TextView NutrientProgressAmount { get; set; }
        public TextView NutrientGoalAmount { get; set; }
        public TextView NutrientRemainingAmount { get; set; }
        public ProgressBar ProgressBar { get; set; }


        public ProgressTrackerViewHolder(View itemView) : base(itemView)
        {
            NutrientName = itemView.FindViewById<TextView>(Resource.Id.nutrientName);
            NutrientProgressAmount = itemView.FindViewById<TextView>(Resource.Id.nutrientProgressAmount);
            NutrientGoalAmount = itemView.FindViewById<TextView>(Resource.Id.nutrientGoalAmount);
            NutrientRemainingAmount = itemView.FindViewById<TextView>(Resource.Id.nutrientRemainingAmount);

            ProgressBar = ItemView.FindViewById<ProgressBar>(Resource.Id.pb);
        }
    }

    public class ProgressTrackerAdapter : RecyclerView.Adapter
    {
        private readonly List<NutrientProgressRecord> _nutrientProgressRecords;

        public ProgressTrackerAdapter(List<NutrientProgressRecord> nutrientProgressRecords)
        {
            _nutrientProgressRecords = nutrientProgressRecords;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View ProgressTrackerView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.progress_tracker_card, parent, false);

            ProgressTrackerViewHolder nrviewholder = new ProgressTrackerViewHolder(ProgressTrackerView);
            return nrviewholder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ProgressTrackerViewHolder nrviewholder = holder as ProgressTrackerViewHolder;
            nrviewholder.NutrientName.Text = _nutrientProgressRecords[position].NutrientName;
            nrviewholder.NutrientGoalAmount.Text = "Goal: " + _nutrientProgressRecords[position].GoalAmount.ToString();
            nrviewholder.NutrientProgressAmount.Text = "Progress: " + _nutrientProgressRecords[position].ProgressAmount.ToString();
            nrviewholder.NutrientRemainingAmount.Text = "Remaining: " + _nutrientProgressRecords[position].RemainingAmount.ToString();

            var percentComplete = (int)((_nutrientProgressRecords[position].ProgressAmount / _nutrientProgressRecords[position].GoalAmount) * 100);

            nrviewholder.ProgressBar.Progress = percentComplete <= 100 ? percentComplete : 100;

            if (_nutrientProgressRecords[position].GoalIsHigh && percentComplete >= 100
                || !_nutrientProgressRecords[position].GoalIsHigh && percentComplete < 100)
                nrviewholder.ProgressBar.ProgressDrawable.SetColorFilter(Color.LimeGreen, PorterDuff.Mode.Multiply);
            else if (_nutrientProgressRecords[position].GoalIsHigh && percentComplete < 100
                || !_nutrientProgressRecords[position].GoalIsHigh && percentComplete >= 100)
                nrviewholder.ProgressBar.ProgressDrawable.SetColorFilter(Color.Red, PorterDuff.Mode.Multiply);

        }


        public override int ItemCount { get { return _nutrientProgressRecords.Count; } }
    }
}