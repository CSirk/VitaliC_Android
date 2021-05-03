using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using VitaliC_Android.Core.Models;

namespace VitaliC_Android.App
{
    [Activity(Label = "NutritionGoalActivity")]
    public class NutritionGoalActivity : Activity
    {
        RecyclerView recylcerView;
        RecyclerView.LayoutManager layoutManager;
        NutritionGoalAdapter adapter;
        NutritionGoal _nutritionGoal;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var userNutritionInfo = JsonConvert.DeserializeObject<UserNutritionInfo>(Intent.GetStringExtra("userNutritionInfo"));

            _nutritionGoal = userNutritionInfo.UserDailyNutritionGoal;

            SetContentView(Resource.Layout.activity_nutrition_goal);

            recylcerView = FindViewById<RecyclerView>(Resource.Id.nutritionGoalRecyclerView);

            layoutManager = new LinearLayoutManager(this);

            //layoutManager = new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false);

            recylcerView.SetLayoutManager(layoutManager);

            adapter = new NutritionGoalAdapter(_nutritionGoal);

            recylcerView.SetAdapter(adapter);

        }
    }

    public class NutritionGoalViewHolder : RecyclerView.ViewHolder
    {
        public TextView RecordName { get; set; }
        public TextView NutrientName { get; set; }
        public EditText NutrientAmount { get; set; }

        public NutritionGoalViewHolder(View itemView) : base(itemView)
        {
            RecordName = itemView.FindViewById<TextView>(Resource.Id.recordNameText);
            NutrientName = itemView.FindViewById<TextView>(Resource.Id.nutrientName);
            NutrientAmount = ItemView.FindViewById<EditText>(Resource.Id.nutrientAmount);
        }
    }

    public class NutritionGoalAdapter : RecyclerView.Adapter
    {
        private readonly NutritionGoal _nutritionGoal;

        public NutritionGoalAdapter(NutritionGoal nutrientGoal)
        {
            _nutritionGoal = nutrientGoal;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View NutritionGoalView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.nutrition_goal_card, parent, false);

            NutritionGoalViewHolder nrviewholder = new NutritionGoalViewHolder(NutritionGoalView);
            return nrviewholder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            NutritionGoalViewHolder nreviewholder = holder as NutritionGoalViewHolder;
            nreviewholder.NutrientName.Text = _nutritionGoal.Nutrients[position].Name;
            nreviewholder.NutrientAmount.Text = _nutritionGoal.Nutrients[position].Amount.ToString();
        }


        public override int ItemCount { get { return _nutritionGoal.Nutrients.Count; } }
    }
}