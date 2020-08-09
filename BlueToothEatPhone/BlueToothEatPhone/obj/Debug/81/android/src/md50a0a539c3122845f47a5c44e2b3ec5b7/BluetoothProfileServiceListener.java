package md50a0a539c3122845f47a5c44e2b3ec5b7;


public class BluetoothProfileServiceListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.bluetooth.BluetoothProfile.ServiceListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onServiceConnected:(ILandroid/bluetooth/BluetoothProfile;)V:GetOnServiceConnected_ILandroid_bluetooth_BluetoothProfile_Handler:Android.Bluetooth.IBluetoothProfileServiceListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onServiceDisconnected:(I)V:GetOnServiceDisconnected_IHandler:Android.Bluetooth.IBluetoothProfileServiceListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BlueToothEatPhone.Listener.BluetoothProfileServiceListener, BlueToothEatPhone", BluetoothProfileServiceListener.class, __md_methods);
	}


	public BluetoothProfileServiceListener ()
	{
		super ();
		if (getClass () == BluetoothProfileServiceListener.class)
			mono.android.TypeManager.Activate ("BlueToothEatPhone.Listener.BluetoothProfileServiceListener, BlueToothEatPhone", "", this, new java.lang.Object[] {  });
	}


	public void onServiceConnected (int p0, android.bluetooth.BluetoothProfile p1)
	{
		n_onServiceConnected (p0, p1);
	}

	private native void n_onServiceConnected (int p0, android.bluetooth.BluetoothProfile p1);


	public void onServiceDisconnected (int p0)
	{
		n_onServiceDisconnected (p0);
	}

	private native void n_onServiceDisconnected (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
