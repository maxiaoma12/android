using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace BlueToothEatPhone.utils
{
    public class AudioUtils
    {
        private MediaPlayer mediaPlayer;
        private Timer mTimer;
        private TimerTask mTimerTask;

        public void playMedia(Context activity)
        {
            mediaPlayer = MediaPlayer.Create(activity, Resource.Raw.m1 );//Android.Net.Uri.Parse("https://down01.pingshu88.com:8011/1/ps/%E8%A2%81%E9%98%94%E6%88%90_%E7%BE%A4%E8%8B%B1%E4%BC%9A/%E8%A2%81%E9%98%94%E6%88%90_%E7%BE%A4%E8%8B%B1%E4%BC%9A_01.mp3?t=u8qn5506fd2db068a7e97b1b85ea0d851b649&m=5F2F9DBE")
            mTimer = new Timer();
            mTimerTask = new MAudioTimerTask();
            mTimer.Schedule(mTimerTask, 0, 10);
            mediaPlayer.Start();
        }

        public void stopPlay()
        {
            mediaPlayer.Stop();
        }
    }

    public class MAudioTimerTask : TimerTask
    {
        public override void Run()
        {
             
        }
    }
}