package md59fbe5aeb2894969c365e8946dc48f9c8;


public class BlueDevice
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BlueToothEatPhone.BlueDevice, BlueToothEatPhone", BlueDevice.class, __md_methods);
	}


	public BlueDevice ()
	{
		super ();
		if (getClass () == BlueDevice.class)
			mono.android.TypeManager.Activate ("BlueToothEatPhone.BlueDevice, BlueToothEatPhone", "", this, new java.lang.Object[] {  });
	}

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
