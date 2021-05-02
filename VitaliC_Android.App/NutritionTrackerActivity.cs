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
using VitaliC_Android.Core.Models;

namespace VitaliC_Android.App
{
    [Activity(Label = "Activity1")]
    public class NutritionTrackerActivity : Activity
    {
        RecyclerView recylcerView;
        RecyclerView.LayoutManager layoutManager;
        NutritionRecordAdapter adapter;
        List<NutritionRecord> nutritionRecords;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            nutritionRecords = new List<NutritionRecord>()
            {
                new NutritionRecord
                {
                    UserId = "Cody test",
                    RecordName = "Orange",
                    RecordType = "UserDaily"
                },
                new NutritionRecord
                {
                    UserId = "Cody test 2",
                    RecordName = "Apple",
                    RecordType = "UserSaved"
                },
                new NutritionRecord
                {
                    UserId = "Cody test 3",
                    RecordName = "Pear",
                    RecordType = "UserDaily"
                },
                new NutritionRecord
                {
                    UserId = "Cody test 4",
                    RecordName = "Banana",
                    RecordType = "UserDaily"
                },
                new NutritionRecord
                {
                    UserId = "Cody test 5",
                    RecordName = "Starfruit",
                    RecordType = "UserDaily"
                },
            };

            SetContentView(Resource.Layout.nutrition_tracker);

            recylcerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            layoutManager = new LinearLayoutManager(this);

            recylcerView.SetLayoutManager(layoutManager);

            adapter = new NutritionRecordAdapter(nutritionRecords);

            recylcerView.SetAdapter(adapter);

            // Create your application here
            
        }
    }

    public class NutritionRecordViewHolder : RecyclerView.ViewHolder
    {
        public TextView Text { get; set; }
        public TextView Text2 { get; set; }
        public TextView Text3 { get; set; }

        public NutritionRecordViewHolder(View itemView) : base(itemView)
        {
            Text = itemView.FindViewById<TextView>(Resource.Id.textView1);
            Text2 = itemView.FindViewById<TextView>(Resource.Id.textView2);
            Text3 = itemView.FindViewById<TextView>(Resource.Id.textView3);
        }
    }

    public class NutritionRecordAdapter : RecyclerView.Adapter
    {
        private readonly List<NutritionRecord> _nutritionRecords;

        public NutritionRecordAdapter(List<NutritionRecord> nutritionRecords)
        {
            _nutritionRecords = nutritionRecords;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View nutritionRecordView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.card_item, parent, false);

            NutritionRecordViewHolder nrviewholder = new NutritionRecordViewHolder(nutritionRecordView);
            return nrviewholder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            NutritionRecordViewHolder nrviewholder = holder as NutritionRecordViewHolder;
            nrviewholder.Text.Text = _nutritionRecords[position].UserId;
            nrviewholder.Text2.Text = _nutritionRecords[position].RecordType;
            nrviewholder.Text3.Text = _nutritionRecords[position].RecordName;

        }


        public override int ItemCount { get { return _nutritionRecords.Count; } }
    }
}