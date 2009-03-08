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
	internal MediaInfo oMediaInfo;
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

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{	
		oXbmc = new XBMC_Communicator();
		oXbmc.SetIp("10.0.0.5");
        oXbmc.SetConnectionTimeout(4000);
        oXbmc.SetCredentials("", "");
		oXbmc.Status.StartHeartBeat();
		
		this.SetIconFromFile("images/icon.png");

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
		oMediaInfo		= new MediaInfo(this);
		
		this.ModifyBase(StateType.Normal, new Gdk.Color(255, 250, 250));
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
	
	public string SecondsToHumanTime(string seconds)
	{
		double totalSeconds = Convert.ToDouble(seconds);
		double iHours		= Math.Floor(((double) totalSeconds/60) /60);
		double iMinutes 	= Math.Floor((double) totalSeconds/60);
		double iSeconds 	= totalSeconds-(Math.Floor((double) totalSeconds/60)*60);
		
		string sHours		= (iHours < 10)? "0" +iHours.ToString() : iHours.ToString();
		string sMinutes 	= (iMinutes < 10)? "0" +iMinutes.ToString() : iMinutes.ToString();
		string sSeconds 	= (iSeconds < 10)? "0" +iSeconds.ToString() : iSeconds.ToString(); 
		string sDuration 	= sHours + ":" + sMinutes + ":" + sSeconds;

		return sDuration;
	}
	
	public byte[] Base64DecodeString(string inputStr) 
    {
      byte[] encodedByteArray = Convert.FromBase64CharArray(inputStr.ToCharArray(), 0, inputStr.Length);
      return encodedByteArray;
    }   
	
	protected virtual void cbShareBrowser_changed (object o, System.EventArgs args)
	{
		oShareBrowser.SetCurrentShareType(cbShareType.Active);
		oShareBrowser.Populate();
	}
	
	protected virtual void cbPlaylistType_changed (object sender, System.EventArgs e)
	{
		oPlaylist.SetCurrentPlaylistType(cbPlaylistType.Active);
		oPlaylist.Populate();
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
	
	protected virtual void aRefreshPlaylist_activated (object sender, System.EventArgs e)
	{
		oPlaylist.Populate();
	}

	protected virtual void hsVolume_valueChanged (object sender, System.EventArgs e)
	{
		oXbmc.Controls.SetVolume(Convert.ToInt32(hsVolume.Value));
		hsVolume.TooltipText = Math.Floor((double) hsVolume.Value).ToString()+"%";
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
		oXbmc.Controls.Next();
	}

	protected virtual void tbStop_released (object sender, System.EventArgs e)
	{
		oXbmc.Controls.Stop();
	}

	protected virtual void aClearPlaylist_click (object sender, System.EventArgs e)
	{
		oXbmc.Playlist.Clear();
		oPlaylist.Populate();
	}

	protected virtual void bRepeat_click (object sender, System.EventArgs e)
	{
		oXbmc.Controls.ToggleRepeatModes();
	}
	
	protected virtual void tvPlaylist_buttonRelease (object o, Gtk.ButtonReleaseEventArgs args)
	{
		if (args.Event.Button == 3) oContextMenu.Show("playlist", null, null);
	}

	protected virtual void aRemoveSelected_activated (object sender, System.EventArgs e)
	{
		oPlaylist.RemoveSelectedItem();
	}

	protected virtual void aPlaySelected_activated (object sender, System.EventArgs e)
	{
		oPlaylist.PlaySelectedItem();
	}

	protected virtual void bPartyMode_released (object sender, System.EventArgs e)
	{
		if (!oXbmc.Controls.TogglePartymode()) 
			Messagebox("XBMC did not accept the command!");
	}

	protected virtual void bShuffle_release (object sender, System.EventArgs e)
	{
		if (!oXbmc.Controls.ToggleShuffle()) 
			Messagebox("XBMC did not accept the command!");
	}
}