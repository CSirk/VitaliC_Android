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
    [Activity(Label = "NutritionRecordEntryActivity")]
    public class NutritionRecordEntryActivity : Activity
    {
        RecyclerView recylcerView;
        RecyclerView.LayoutManager layoutManager;
        NutritionRecordEntryAdapter adapter;
        List<Nutrient> _nutrients;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var userNutritionInfo = JsonConvert.DeserializeObject<UserNutritionInfo>(Intent.GetStringExtra("userNutritionInfo"));

            _nutrients = userNutritionInfo.BaseNutrients;

            SetContentView(Resource.Layout.nutrition_record_entry);

            recylcerView = FindViewById<RecyclerView>(Resource.Id.nutritionRecordEntryRecyclerView);

            layoutManager = new LinearLayoutManager(this);

            //layoutManager = new GridLayoutManager(this, 1, GridLayoutManager.Horizontal, false);

            recylcerView.SetLayoutManager(layoutManager);

            adapter = new NutritionRecordEntryAdapter(_nutrients);

            recylcerView.SetAdapter(adapter);

        }
    }

    public class NutritionRecordEntryViewHolder : RecyclerView.ViewHolder
    {
        public TextView RecordName { get; set; }
        public TextView NutrientName { get; set; }
        public EditText NutrientAmount { get; set; }

        public NutritionRecordEntryViewHolder(View itemView) : base(itemView)
        {
            RecordName = itemView.FindViewById<TextView>(Resource.Id.recordNameText);
            NutrientName = itemView.FindViewById<TextView>(Resource.Id.nutrientName);
            NutrientAmount = ItemView.FindViewById<EditText>(Resource.Id.nutrientAmount);
        }
    }

    public class NutritionRecordEntryAdapter : RecyclerView.Adapter
    {
        private readonly List<Nutrient> _nutrients;

        public NutritionRecordEntryAdapter(List<Nutrient> nutrients)
        {
            _nutrients = nutrients;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View NutritionRecordEntryView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.nutrition_record_nutrient_card, parent, false);

            NutritionRecordEntryViewHolder nrviewholder = new NutritionRecordEntryViewHolder(NutritionRecordEntryView);
            return nrviewholder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            NutritionRecordEntryViewHolder nreviewholder = holder as NutritionRecordEntryViewHolder;
            nreviewholder.NutrientName.Text = _nutrients[position].Name;
            nreviewholder.NutrientAmount.Text = _nutrients[position].Amount.ToString();
        }


        public override int ItemCount { get { return _nutrients.Count; } }
    }
}