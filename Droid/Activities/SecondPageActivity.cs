using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Newtonsoft.Json;
using Testproject.ViewModels;

namespace Testproject.Droid
{
    [Activity(Label = "Saved Logins")]
    public class SecondPageActivity : AppCompatActivity
    {

        RecyclerView mRV;
        RecyclerView.LayoutManager mLayoutManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.second_page);

            var data = JsonConvert.DeserializeObject<List<LoginModel>>(Intent.GetStringExtra("mainList"));

            mRV = FindViewById<RecyclerView>(Resource.Id.savedRecyclerView);

            mRV.SetAdapter(new RVAdapter(data));

            mLayoutManager = new LinearLayoutManager(this);
            mRV.SetLayoutManager(mLayoutManager);
        }
    }
}
