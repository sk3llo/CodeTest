using System;

using Android.Views;
using Android.Support.V7.Widget;
using Android.Widget;
using System.Collections.Generic;
using Testproject.ViewModels;
using Android.Util;

namespace Testproject.Droid
{
    public class RVAdapter : RecyclerView.Adapter
    {

        List<LoginModel> mainList;

        public RVAdapter(List<LoginModel> mainList)
        {
            this.mainList = mainList;
        }

        public override int ItemCount
        {
            get
            {
                return mainList.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Log.Debug("", $"{position}");
            Log.Debug("", $"{mainList}");
            LoginViewHolder vh = holder as LoginViewHolder;
            vh.Login.Text = mainList[position].login;
            vh.Password.Text = mainList[position].password;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                    Inflate(Resource.Layout.rv_item, parent, false);
            LoginViewHolder vh = new LoginViewHolder(itemView);
            return vh;
        }
    }

    // Holder
    public class LoginViewHolder : RecyclerView.ViewHolder
    {
        public TextView Login { get; private set; }
        public TextView Password { get; private set; }

        public LoginViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            Login = itemView.FindViewById<TextView>(Resource.Id.rv_item_login);
            Password = itemView.FindViewById<TextView>(Resource.Id.rv_item_password);
        }
    }
}
