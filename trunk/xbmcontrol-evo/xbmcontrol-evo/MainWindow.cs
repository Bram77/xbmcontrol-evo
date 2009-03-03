using System;
using Gtk;
using Gdk;
using XBMC;
using xbmcontrolevo;

public partial class MainWindow: Gtk.Window
{	
	internal XBMC_Communicator Xbmc;
	internal ShareBrowser _ShareBrowser;
	internal SysTrayIcon _Trayicon;
	public ContextMenu contextMenu;
	public MenuItems menuItems;
	public Controls controls;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Xbmc = new XBMC_Communicator();
		Xbmc.SetIp("10.0.0.5");
        Xbmc.SetConnectionTimeout(4000);
        Xbmc.SetCredentials("", "");

		Build ();
		
		controls		= new Controls(this);
		menuItems		= new MenuItems(this);
		contextMenu 	= new ContextMenu(this);
		_ShareBrowser 	= new ShareBrowser(this.tvShareBrowser, this);
		_Trayicon 		= new SysTrayIcon(this);
		
		_ShareBrowser.Populate("music");
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected bool in_stringArray(string s, string[] a)
	{
		bool inArray = false;
		foreach (string entry in a)
			if (entry == s) inArray=true;
		
		return inArray;
	}
	
	protected virtual void OnExit (object sender, System.EventArgs e)
	{
		Application.Quit();
	}
	
	public void Messagebox(string message)
	{
		MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, message);
		md.Modal = true;
		ResponseType result = (ResponseType)md.Run();                 
        if (result == ResponseType.Ok) md.Destroy();
	}
	
	protected virtual void PopulateShareBrowser (object o, System.EventArgs args)
	{
		_ShareBrowser.Populate(cbShareType.ActiveText);
	}
		
	protected virtual void ExpandDirectory (object o, Gtk.ButtonReleaseEventArgs args)
	{
		if (args.Event.Button == 1)
			_ShareBrowser.ExpandSelectedDirectory();
		else if (args.Event.Button == 3)
			_ShareBrowser.ShowContextMenu();
	}	
}