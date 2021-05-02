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

            progressRecords = new List<NutrientProgressRecord>()
            {
                new NutrientProgressRecord
                {
                    NutrientName = "Calories",
                    DisplayName = "Calories",
                    GoalAmount = 2000,
                    RemainingAmount = 1500,
                    ProgressAmount = 500,
                    Measurement = "Calorie",
                    MeasurementAbbreviation = "cal",
                    GoalIsHigh = false,
                    Alias = "",
                    DisplayOrder = 1
                },
                new NutrientProgressRecord
                {
                    NutrientName = "Fat",
                    DisplayName = "Fat",
                    GoalAmount = 30,
                    RemainingAmount = 20,
                    ProgressAmount = 10,
                    Measurement = "Grams",
                    MeasurementAbbreviation = "g",
                    GoalIsHigh = false,
                    Alias = "",
                    DisplayOrder = 2
                },
                new NutrientProgressRecord
                {
                    NutrientName = "Carbohydrates",
                    DisplayName = "Carbs",
                    GoalAmount = 140,
                    RemainingAmount = 0,
                    ProgressAmount = 140,
                    Measurement = "Grams",
                    MeasurementAbbreviation = "g",
                    GoalIsHigh = false,
                    Alias = "",
                    DisplayOrder = 3
                },
                new NutrientProgressRecord
                {
                    NutrientName = "Protein",
                    DisplayName = "Protein",
                    GoalAmount = 190,
                    RemainingAmount = 0,
                    ProgressAmount = 190,
                    Measurement = "Grams",
                    MeasurementAbbreviation = "g",
                    GoalIsHigh = true,
                    Alias = "",
                    DisplayOrder = 4
                }
            };

            SetContentView(Resource.Layout.progress_tracker);

            recylcerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            layoutManager = new LinearLayoutManager(this);

            //layoutManager = new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false);

            recylcerView.SetLayoutManager(layoutManager);

            adapter = new ProgressTrackerAdapter(progressRecords);

            recylcerView.SetAdapter(adapter);

            // Create your application here
            
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