using Android.Bluetooth;

namespace BlueToothEatPhone
{
    public class BlueDevice: Java.Lang.Object
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public BluetoothDevice Device { get; set; }

        public string Status { get; set; } = "未绑定";
    }
}