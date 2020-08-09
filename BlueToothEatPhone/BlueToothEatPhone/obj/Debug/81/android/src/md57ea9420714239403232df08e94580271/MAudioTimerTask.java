package md57ea9420714239403232df08e94580271;


public class MAudioTimerTask
	extends java.util.TimerTask
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_run:()V:GetRunHandler\n" +
			"";
		mono.android.Runtime.register ("BlueToothEatPhone.utils.MAudioTimerTask, BlueToothEatPhone", MAudioTimerTask.class, __md_methods);
	}


	public MAudioTimerTask ()
	{
		super ();
		if (getClass () == MAudioTimerTask.class)
			mono.android.TypeManager.Activate ("BlueToothEatPhone.utils.MAudioTimerTask, BlueToothEatPhone", "", this, new java.lang.Object[] {  });
	}


	public void run ()
	{
		n_run ();
	}

	private native void n_run ();

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
