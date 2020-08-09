using System;
using System.Collections.Generic;
using System.Linq;
using Android;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BlueToothEatPhone.adapter;
using BlueToothEatPhone.Listener;
using BlueToothEatPhone.utils;
using static Android.Views.View;

namespace BlueToothEatPhone
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity,  IOnClickListener
    {
        private static readonly int ENABLE_BLUE = 100;
        private static readonly string MAINACTIVITY = "MainActivity";

        /// <summary>
        /// 自定义返回码
        /// </summary>
        private static readonly int MY_PERMISSION_REQUEST_CONSTANT = 128;//123456
         
        Button btnStart;
        Button btnPublic;
        Button btnSearch;
        Button btnStop;
        Button btnPlay;
        Button btnStopPlay;
        Button btnStartService;

        ListView lsBlue;

        private BluetoothAdapter mBluetoothAdapter;
        private List<BluetoothDevice> devices;
        private List<BlueDevice> setDevices = new List<BlueDevice>();

        private BlueAdapter blueAdapter;
        private IntentFilter mFilter;

        /// <summary>
        /// 蓝牙音频传输协议
        /// </summary>
        private BluetoothA2dp a2dp;

        /// <summary>
        /// 需要连接的蓝牙设备
        /// </summary>
        private BluetoothDevice currentBluetoothDevice;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            if (Build.VERSION.SdkInt > BuildVersionCodes.M)
            {
                if(ActivityCompat.CheckSelfPermission(this,Manifest.Permission.AccessCoarseLocation)
                    != Android.Content.PM.Permission.Granted)
                {  
                    /**动态添加权限：ACCESS_FINE_LOCATION*/
                    ActivityCompat.RequestPermissions(this,
                        new string[] { Manifest.Permission.AccessFineLocation },
                        MY_PERMISSION_REQUEST_CONSTANT);
                }
            }


            btnStart = (Button)FindViewById<Button>(Resource.Id.btn_start);
            btnPublic = (Button)FindViewById<Button>(Resource.Id.btn_public);
            btnSearch = (Button)FindViewById<Button>(Resource.Id.btn_search);
            btnPlay = FindViewById<Button>(Resource.Id.btn_play);
            btnStop = FindViewById<Button>(Resource.Id.btn_stop);
            btnStopPlay = FindViewById<Button>(Resource.Id.btn_stop_play);
            btnStartService = FindViewById<Button>(Resource.Id.btn_start_service);

            lsBlue = FindViewById<ListView>(Resource.Id.ls_blue);

            mReceiver = new BluetoothBroadcastReceiver(setDevices);
            audioUtils = new AudioUtils();
            initDate();
            setListener();
        }

        private BroadcastReceiver mReceiver;

        private void initDate()
        {
            mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

            /**注册搜索蓝牙receiver*/
            mFilter = new IntentFilter(BluetoothAdapter.ActionDiscoveryFinished);
            mFilter.AddAction(BluetoothDevice.ActionFound);
            mFilter.AddAction(BluetoothDevice.ActionBondStateChanged);
            RegisterReceiver(mReceiver, mFilter);

            getBondedDevices();
        }
        /// <summary>
        /// 获取所有已经绑定的蓝牙设备并显示
        /// </summary>
        private void getBondedDevices()
        {
            if (setDevices.Count != 0)
            {
                setDevices.Clear();
                setDevices = new List<BlueDevice>();
            }


            devices = mBluetoothAdapter.BondedDevices.ToList();
            foreach(BluetoothDevice device in devices)
            {
                BlueDevice blueDevice = new BlueDevice()
                {
                    Name = device.Name,
                    Address = device.Address,
                    Device = device,
                    Status = "已配对"
                };
                setDevices.Add(blueDevice);
            }

            if(null == blueAdapter)
            {
                blueAdapter = new BlueAdapter(this, setDevices);
                lsBlue.SetAdapter(blueAdapter);
            }
            else
            {
                blueAdapter.SetDevice( setDevices);
                blueAdapter.NotifyDataSetChanged();
            }
        }

        private void setListener() {
            btnStart.SetOnClickListener(this);
            btnPlay.SetOnClickListener(this);
            btnPublic.SetOnClickListener(this);
            btnSearch.SetOnClickListener(this);
            btnStop.SetOnClickListener(this);
            btnStopPlay.SetOnClickListener(this);
            btnStartService.SetOnClickListener(this);

            //lsBlue.SetOnClickListener(new AdapterView.onit)
        }

        private AudioUtils audioUtils ;
        public void OnClick(View v)
        {
            if (null == mBluetoothAdapter) return;
            switch (v.Id)
            {
                case Resource.Id.btn_start:
                    //如果本地没有开启蓝牙，则开启
                    if (!mBluetoothAdapter.Enable())
                    {
                        // 我们通过startActivityForResult()方法发起的Intent将会在onActivityResult()回调方法中获取用户的选择，比如用户单击了Yes开启，
                        // 那么将会收到RESULT_OK的结果，
                        // 如果RESULT_CANCELED则代表用户不愿意开启蓝牙
                        Intent mIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                        StartActivityForResult(mIntent, ENABLE_BLUE);
                    }
                    else
                    {
                        Toast.MakeText(this, "蓝牙已开启", ToastLength.Short).Show();
                        getBondedDevices();
                    }
                    break;
                case Resource.Id.btn_public:
                    Intent intent = new Intent(BluetoothAdapter.ActionRequestDiscoverable);
                    Intent.PutExtra(BluetoothAdapter.ExtraDiscoverableDuration, 180);//180可见时间
                    StartActivity(intent);
                    break;
                case Resource.Id.btn_search:
                    //ShowDialog()
                    //如果正在搜索，就先取消搜索
                    if (mBluetoothAdapter.IsDiscovering)
                    {
                        mBluetoothAdapter.CancelDiscovery();
                    }
                    //开始搜索蓝牙设备，搜索到的蓝牙设备通过广播返回
                    mBluetoothAdapter.StartDiscovery();
                    break;
                case Resource.Id.btn_stop:
                    //关闭蓝牙
                    if (mBluetoothAdapter.Enable())
                    {
                        mBluetoothAdapter.Disable();
                    }
                    break;
                case Resource.Id.btn_play:
                    audioUtils.playMedia(this);
                    break;
                case Resource.Id.btn_stop_play:
                    audioUtils.stopPlay();
                    break;
                case Resource.Id.btn_start_service:
                    contectBuleDevices();
                    break;
            }
        }
        /// <summary>
        /// 开始连接蓝牙设备
        /// </summary>
        private void contectBuleDevices()
        {
            var mProfileServiceListener = new BluetoothProfileServiceListener() {
                context =this
            };
            /**使用A2DP协议连接设备*/
            mBluetoothAdapter.GetProfileProxy(this, mProfileServiceListener, ProfileType.A2dp);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode == ENABLE_BLUE)
            {
                if(resultCode == Result.Ok)
                {
                    Toast.MakeText(this, "蓝牙开启成功", ToastLength.Short).Show();
                    getBondedDevices();
                }else if(resultCode == Result.Canceled)
                {
                    Toast.MakeText(this, "蓝牙开启失败", ToastLength.Short).Show();
                }
            }
        }
    }
}

