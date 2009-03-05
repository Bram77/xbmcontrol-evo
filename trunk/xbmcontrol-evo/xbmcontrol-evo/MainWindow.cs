using System;
using System.Threading;
using Gtk;
using Gdk;
using XBMC;
using xbmcontrolevo;

public partial class MainWindow: Gtk.Window
{	
	internal XBMC_Communicator oXbmc;
	internal ShareBrowser oShareBrowser;
	internal SysTrayIcon oTrayicon;
	public ContextMenu oContextMenu;
	public MenuItems oMenuItems;
	public Controls oControls;
	public Playlist oPlaylist;
	public StatusUpdate oStatusUpdate;
	public TreeView _tvShareBrowser;
	public TreeView _tvPlaylist;
	public ComboBox _cbShareType;
	public HScale _hsVolume;
	public HScale _hsProgress;
	
	//Buttons
	public ToggleButton _tbMute;
	public Button _bPrevious;
	public ToggleButton _tbPlay;
	public ToggleButton _tbStop;
	public Button _bNext;
	
	//public RefreshTimerState _RefreshTimerState = new RefreshTimerState();
	//private TimerCallback _RefreshTimerDelegate;
	//private Timer _RefreshTimer;
	//private int refreshInterval = 1000;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{	
		oXbmc = new XBMC_Communicator();
		oXbmc.SetIp("10.0.0.5");
        oXbmc.SetConnectionTimeout(4000);
        oXbmc.SetCredentials("", "");

		//_RefreshTimerDelegate 		= new Timercallback(Xbmc.Status.Refresh);
		//_RefreshTimer 				= new Timer(_RefreshTimerDelegate, _RefreshTimerState, 0, refreshInterval);
		//_RefreshTimerState.timer	= _RefreshTimer.;

		Build ();
		
		//Make static widgets publicly accessable
		_tvShareBrowser = this.tvShareBrowser;
		_tvPlaylist		= this.tvPlaylist;
		_cbShareType	= this.cbShareType;
		_hsVolume		= this.hsVolume;
		_hsProgress 	= this.hsProgress;
		_tbMute			= this.tbMute;
		_bPrevious		= this.bPrevious;
		_tbPlay			= this.tbPlay;
		_tbStop			= this.tbStop;
		_bNext			= this.bNext;
		
		//Create objects used
		oPlaylist 		= new Playlist(this);
		oControls		= new Controls(this);
		oMenuItems		= new MenuItems(this);
		oContextMenu 	= new ContextMenu(this);
		oShareBrowser 	= new ShareBrowser(this);
		oTrayicon 		= new SysTrayIcon(this);
		oStatusUpdate	= new StatusUpdate(this);
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
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
	
	public bool Confirm(string message)
	{
		MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Question, ButtonsType.YesNo, message);
		md.Modal = true;
		ResponseType result = (ResponseType)md.Run(); 
		
        if (result == ResponseType.Yes)
		{
			md.Destroy();
			return true;
	 	}
	 	else
		{
			md.Destroy();
			return false; 
		}
	}
	
	protected virtual void change_cbShareBrowser (object o, System.EventArgs args)
	{
		oShareBrowser.Populate(cbShareType.Active);
	}
		
	protected virtual void tvShareBrowser_release (object o, Gtk.ButtonReleaseEventArgs args)
	{
		if (args.Event.Button == 1)
			oShareBrowser.ExpandSelectedDirectory();
		else if (args.Event.Button == 3)
			oShareBrowser.ShowContextMenu();
	}	

	protected virtual void click_UpdateMusicLibrary (object sender, System.EventArgs e)
	{
		bool startIndexing = Confirm("XBMC will now start indexing your music library.\n\nAre you sure you want to continue?");
		if (startIndexing) oXbmc.Controls.UpdateLibrary("music");
	}
	
	protected virtual void click_UpdateVideoLibrary (object sender, System.EventArgs e)
	{
		bool startIndexing = Confirm("XBMC will now start indexing your video shares.\n\nAre you sure you want to continue?");
		if (startIndexing) oXbmc.Controls.UpdateLibrary("video");
	}
	
	public void CollapseAllShares()
	{
		tvShareBrowser.CollapseAll();
	}
	
	protected virtual void click_RefreshPlaylist (object sender, System.EventArgs e)
	{
		oPlaylist.Populate();
	}

	protected virtual void hsVolume_valueChanged (object sender, System.EventArgs e)
	{
		oXbmc.Controls.SetVolume(Convert.ToInt32(hsVolume.Value));
	}

	protected virtual void hsProgress_changeValue (object o, Gtk.ChangeValueArgs args)
	{
		oXbmc.Controls.SeekPercentage(Convert.ToInt32(hsProgress.Value));
	}

	protected virtual void tbMute_released (object sender, System.EventArgs e)
	{
		oXbmc.Controls.ToggleMute();
	}

	protected virtual void tbPlay_released (object sender, System.EventArgs e)
	{
		oXbmc.Controls.Play();
	}

	protected virtual void bPrevious_released (object sender, System.EventArgs e)
	{
		oXbmc.Controls.Previous();
	}

	protected virtual void bNext_released (object sender, System.EventArgs e)
	{
		oXbmc.Controls.Previous();
	}

	protected virtual void tbStop_released (object sender, System.EventArgs e)
	{
		oXbmc.Controls.Stop();
	}

}