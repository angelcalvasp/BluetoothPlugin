package md5d02d45f78a84b3bcc426cb294d8d1026;


public class BluetoothConnectionThread
	extends java.lang.Thread
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_run:()V:GetRunHandler\n" +
			"";
		mono.android.Runtime.register ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", BluetoothConnectionThread.class, __md_methods);
	}


	public BluetoothConnectionThread ()
	{
		super ();
		if (getClass () == BluetoothConnectionThread.class)
			mono.android.TypeManager.Activate ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public BluetoothConnectionThread (java.lang.Runnable p0)
	{
		super (p0);
		if (getClass () == BluetoothConnectionThread.class)
			mono.android.TypeManager.Activate ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", "Java.Lang.IRunnable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public BluetoothConnectionThread (java.lang.Runnable p0, java.lang.String p1)
	{
		super (p0, p1);
		if (getClass () == BluetoothConnectionThread.class)
			mono.android.TypeManager.Activate ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", "Java.Lang.IRunnable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public BluetoothConnectionThread (java.lang.String p0)
	{
		super (p0);
		if (getClass () == BluetoothConnectionThread.class)
			mono.android.TypeManager.Activate ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public BluetoothConnectionThread (java.lang.ThreadGroup p0, java.lang.Runnable p1)
	{
		super (p0, p1);
		if (getClass () == BluetoothConnectionThread.class)
			mono.android.TypeManager.Activate ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", "Java.Lang.ThreadGroup, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Java.Lang.IRunnable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public BluetoothConnectionThread (java.lang.ThreadGroup p0, java.lang.Runnable p1, java.lang.String p2)
	{
		super (p0, p1, p2);
		if (getClass () == BluetoothConnectionThread.class)
			mono.android.TypeManager.Activate ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", "Java.Lang.ThreadGroup, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Java.Lang.IRunnable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public BluetoothConnectionThread (java.lang.ThreadGroup p0, java.lang.Runnable p1, java.lang.String p2, long p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == BluetoothConnectionThread.class)
			mono.android.TypeManager.Activate ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", "Java.Lang.ThreadGroup, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Java.Lang.IRunnable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int64, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public BluetoothConnectionThread (java.lang.ThreadGroup p0, java.lang.String p1)
	{
		super (p0, p1);
		if (getClass () == BluetoothConnectionThread.class)
			mono.android.TypeManager.Activate ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", "Java.Lang.ThreadGroup, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}

	public BluetoothConnectionThread (android.bluetooth.BluetoothDevice p0)
	{
		super ();
		if (getClass () == BluetoothConnectionThread.class)
			mono.android.TypeManager.Activate ("Bluetooth.Plugin.Android.BluetoothConnectionThread, Plugin.Bluetooth, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null", "Android.Bluetooth.BluetoothDevice, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
