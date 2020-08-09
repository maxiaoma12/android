using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BlueToothEatPhone
{
    public class BluetoothBroadcastReceiver:  BroadcastReceiver
    {
        public BluetoothBroadcastReceiver(List<BlueDevice> setDevices)
        {
            this.setDevices = setDevices;
        }

        private List<BlueDevice> setDevices;
        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            //搜索到的蓝牙设备
            if(action == BluetoothDevice.ActionFound)
            {
                BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                //搜索到的不是已经配对的蓝牙设备
                if(device.BondState != Bond.Bonded)
                {
                    BlueDevice blueDevice = new BlueDevice()
                    {
                        Name = device.Name,
                        Address = device.Address,
                        Device = device
                    };
                    setDevices.Add(blueDevice);

                    Toast.MakeText(context, "搜索结果......"+device.Name,  ToastLength.Long).Show();
                }
                /**当绑定的状态改变时*/
            }
            else if(action == BluetoothDevice.ActionBondStateChanged)
            {
                BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                switch (device.BondState)
                {
                    case Bond.Bonding:
                        Toast.MakeText(context, "正在配对......" + device.Name, ToastLength.Long).Show();
                        break;
                    case Bond.Bonded:
                        Toast.MakeText(context, "完成配对" + device.Name, ToastLength.Long).Show();
                        break;
                    case Bond.None:
                        Toast.MakeText(context, "取消配对" + device.Name, ToastLength.Long).Show();
                        break;
                    default:break;
                }
            }
            //搜索完成
            else if (action == BluetoothAdapter.ActionDiscoveryFinished)
            {
                Toast.MakeText(context, "搜索完成......", ToastLength.Long).Show();
            }
        }
    }
}