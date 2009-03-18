
using System;
using Gtk;
using Glade;
using XBMC;
using xbmcontrolevo;

namespace xbmcontrolevo
{		
	public class XbmControlEvo
	{

		public static void Main (string[] args)
		{
			new XbmControlEvo (args);
		}
		
		
		//Window
		[Widget] Window MainWindow;
		
		//[Widget] Gtk.Image ibPrevious;
		
		//ComboBox
		[Widget] ComboBox cbShares;
		[Widget] ComboBox cbPlaylist;
		
		//NoteBook
		[Widget] Notebook nbLeft;
		[Widget] Notebook nbRight;
		
		//HScale
		[Widget] HScale hsProgress;
		[Widget] HScale hsVolume;
		
		//Entry
		//[Widget] Entry eFilterFiles;
		[Widget] Entry eArtistsFilter;
		
		//TreeView
		[Widget] TreeView tvShares;
		[Widget] TreeView tvGenres;
		[Widget] TreeView tvArtists;
		[Widget] TreeView tvAlbums;
		[Widget] TreeView tvSongs;
		[Widget] TreeView tvVideos;
		[Widget] TreeView tvPlaylist;
		[Widget] TreeView tvFiles;
		
		//Objects
		public XBMC_Communicator oXbmc;
		public ShareBrowser oShareBrowser;
		public FileBrowser oFileBrowser;
		public ContextMenu oContextMenu;
		public MenuItems oMenuItems;
		public Controls oControls;
		public Playlist oPlaylist;
		public HelperFunctions oHelper;
		public StatusUpdate oStatusUpdate;
		public SysTrayIcon oSysTrayIcon;
		public GenreBrowser oGenreBrowser;
		public ArtistBrowser oArtistBrowser;
		
		//Internal widgets
		public Window _MainWindow;
		public TreeView _tvShares;
		public TreeView _tvArtists;
		public TreeView _tvGenres;
		public TreeView _tvPlaylist;
		public TreeView _tvFiles;
		public Notebook _nbLeft;
		public Notebook _nbRight;
		public HScale _hsVolume;
		public HScale _hsProgress;
		public Entry _eFilterFiles;
		
		
		public XbmControlEvo (string[] args)
		{
			Application.Init();
			
			Glade.XML guiXml = new Glade.XML ("XbmControlEvo.glade", "MainWindow", null);
			guiXml.Autoconnect (this);

			this.XbmcConnect();
			this.AllowinternalAccess();
			this.InitObjects();
			this.SetStartupvalues();
			
			Application.Run();
		}
		
		private void InitObjects ()
		{
			oShareBrowser 	= new ShareBrowser(this);
			oFileBrowser	= new FileBrowser(this);
			oMenuItems		= new MenuItems(this);
			oContextMenu	= new ContextMenu(this);
			oControls		= new Controls(this);
			oPlaylist		= new Playlist(this);
			oHelper			= new HelperFunctions(this);
			oStatusUpdate	= new StatusUpdate(this);
			oSysTrayIcon	= new SysTrayIcon(this);
			oGenreBrowser	= new GenreBrowser(this);
			oArtistBrowser 	= new ArtistBrowser(this);
		}
		
		private void AllowinternalAccess ()
		{
			_tvShares 		= tvShares;
			_tvArtists		= tvArtists;
			_tvGenres		= tvGenres;
			_tvPlaylist 	= tvPlaylist;
			_tvFiles		= tvFiles;
			_MainWindow 	= MainWindow;
			_nbLeft			= nbLeft;
			_nbRight		= nbRight;
			_hsVolume		= hsVolume;
			_hsProgress 	= hsProgress;
			//_eFilterFiles	= eFilterFiles;
		}
		
		private void XbmcConnect ()
		{
			oXbmc = new XBMC_Communicator();
			oXbmc.SetIp("10.0.0.5");
	        oXbmc.SetConnectionTimeout(4000);
	        oXbmc.SetCredentials("", "");
			oXbmc.Status.StartHeartBeat();
		}
		
		private void SetStartupvalues ()
		{
			this.cbShares.Active = 0;
			this.cbPlaylist.Active = 0;
		}
		
		protected void on_MainWindow_delete_event (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
		
		protected void on_cbShares_changed (object o, EventArgs args)
		{
			try
			{
				this.oShareBrowser.SetCurrentShareType(this.cbShares.Active);
				this.oShareBrowser.Populate();
			}
			catch (Exception e)
			{
				oHelper.Messagebox(e.Message);
			}
		}
		
		protected void on_tvShares_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (args.Event.Button == 1)
				oShareBrowser.ExpandSelectedDirectory();
			else if (args.Event.Button == 3)
				oShareBrowser.ShowContextMenu();
		}
		
		protected void on_tvArtists_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (args.Event.Button == 1)
				oArtistBrowser.ExpandeSelectedItem();
		}
		
		protected void on_tvFiles_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (args.Event.Button == 3)
				oFileBrowser.ShowContextMenu();
		}
		
		protected void on_tvPlaylist_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (args.Event.Button == 3)
				oPlaylist.ShowContextMenu();
		}
		
		protected void on_hsVolume_value_changed (object o, EventArgs args)
		{
			if (hsVolume.HasFocus)
			{
				oXbmc.Controls.SetVolume(Convert.ToInt32(hsVolume.Value));
				hsVolume.TooltipText = Math.Floor((double) hsVolume.Value).ToString()+"%";
			}
		}
		
		protected void on_hsProgress_value_changed (object o, EventArgs args)
		{
			if (hsProgress.HasFocus) 
				oXbmc.Controls.SeekPercentage(Convert.ToInt32(hsProgress.Value));
		}
		
		protected void on_cbPlaylist_changed (object o, EventArgs args)
		{
			oPlaylist.SetCurrentPlaylistType(cbPlaylist.Active.ToString());
		}
		
		protected void on_nbLeft_switch_page (object o, Gtk.SwitchPageArgs args)
		{
			if (args.PageNum == 0)
				oShareBrowser.Populate();
			else if (args.PageNum == 1)
				oGenreBrowser.Populate();
			else if (args.PageNum == 2)
				oArtistBrowser.Populate();	
		}
		
		protected void on_eArtistsFilter_changed (object o, EventArgs args)
		{
			oArtistBrowser.Populate(eArtistsFilter.Text);
		}
	}
}
