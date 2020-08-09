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

namespace BlueToothEatPhone.Listener
{
    public class BluetoothProfileServiceListener :  Java.Lang.Object,IBluetoothProfileServiceListener
    { 

        public Context context { get; set; }

        public void OnServiceConnected([GeneratedEnum] ProfileType profile, IBluetoothProfile proxy)
        {
            try
            {
                //if(profile == ProfileType.Headset)
                //{

                //}else if(profile == ProfileType.A2dp)
                //{
                //    var a2dp = (BluetoothA2dp)proxy;
                //    /**使用A2DP的协议连接蓝牙设备（使用了反射技术调用连接的方法）*/
                //    if (a2dp.GetConnectionState(currDevice)!= ProfileState.Connected)
                //    {
                //        //a2dp.GetType().GetMethod("connect").Invoke(a2dp, currDevice);
                //        Toast.MakeText(context, "请播放音乐", ToastLength.Short).Show();
                //    }
                //}
                if (profile == ProfileType.A2dp)
                {
                    String deviceName = "红米手机";

                    BluetoothDevice result = null;

                    var devices = BluetoothAdapter.DefaultAdapter.BondedDevices;
                    if (devices != null)
                    {
                        foreach (BluetoothDevice device in devices)
                        {
                            if (deviceName == device.Name)
                            {
                                result = device;
                                break;
                            }
                        }
                    }
                    var connect = Java.Lang.Class.FromType(typeof(BluetoothA2dp)).GetDeclaredMethod("connect", Java.Lang.Class.FromType(typeof(BluetoothDevice)));
                    connect.Invoke((Java.Lang.Object)proxy, result);
                    Toast.MakeText(context, "请播放音乐", ToastLength.Short).Show();
                }
            }
            catch(Exception ex)
            { 
                Toast.MakeText(context, ex.Message.ToString(), ToastLength.Short).Show();
            }
        }

        public void OnServiceDisconnected([GeneratedEnum] ProfileType profile)
        { 
        }
    }
}