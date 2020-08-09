using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang; 

namespace BlueToothEatPhone.adapter
{
    public class BlueAdapter : BaseAdapter
    {
        private Context context;
        public List<BlueDevice> Devices { get; set; }
        public override int Count { get; }

        public BlueAdapter(Context context, List<BlueDevice> devices)
        {
            this.context = context;
            SetDevice(devices);
        }

        public void SetDevice(List<BlueDevice> devices)
        {
            this.Devices = new List<BlueDevice>();
            foreach (var device in devices)
            {
                this.Devices.Add(device);
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return Devices[position];
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder viewHolder = null;
            if(convertView == null)
            {
                viewHolder = new ViewHolder();
                convertView = LayoutInflater.From(context).Inflate(Resource.Layout.item_main, parent, false);
                viewHolder.blueName = convertView.FindViewById<TextView>(Resource.Id.tv_blueName);
                viewHolder.blueState = convertView.FindViewById<TextView>(Resource.Id.tv_status);
                convertView.SetTag(0, viewHolder);
            }
            else
            {
                viewHolder = convertView.GetTag(0) as ViewHolder;
            }

            BlueDevice device = Devices[position];
            if(null!= device)
            {
                viewHolder.blueName.Text =("蓝牙名：" + device.Name);
                viewHolder.blueName.Text=device.Status;
            }

            return convertView;
        }
    }

    public class ViewHolder : Java.Lang.Object
    {
        public TextView blueName;
        public TextView blueState;
    }
}