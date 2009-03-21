
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
		
		//ToggleButton
		[Widget] ToggleButton tbMute;
		
		//HScale
		[Widget] HScale hsProgress;
		[Widget] HScale hsVolume;
		
		//Image
		[Widget] Image iConnectionStatus;
		[Widget] Image ibPrevious;
		[Widget] Image ibPlay;
		[Widget] Image ibStop;
		[Widget] Image ibNext;
		
		//Label
		[Widget] Label lStatus;
		
		//Entry
		//[Widget] Entry eFilterFiles;
		[Widget] Entry eArtistsFilter;
		[Widget] Entry eAlbumsFilter;
		
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
		public AlbumBrowser oAlbumBrowser;
		
		//Internal widgets
		public Window _MainWindow;
		public TreeView _tvShares;
		public TreeView _tvArtists;
		public TreeView _tvGenres;
		public TreeView _tvAlbums;
		public TreeView _tvPlaylist;
		public TreeView _tvFiles;
		public Notebook _nbLeft;
		public Notebook _nbRight;
		public HScale _hsVolume;
		public HScale _hsProgress;
		public Entry _eFilterFiles;
		public Gtk.Image _iConnectionStatus;
		public Label _lStatus;
		public ToggleButton _tbMute;
		public Image _ibPlay;
		
		//Settings
		public string theme;
		
		
		public XbmControlEvo (string[] args)
		{
			Application.Init();
			
			theme = "default";
			
			Glade.XML wMainWindowXml 	= new Glade.XML ("Interface/" +theme+ ".glade", "MainWindow", null);
			wMainWindowXml.Autoconnect (this);

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
			oAlbumBrowser	= new AlbumBrowser(this);
		}
		
		private void AllowinternalAccess ()
		{
			_tvShares 			= tvShares;
			_tvArtists			= tvArtists;
			_tvGenres			= tvGenres;
			_tvAlbums			= tvAlbums;
			_tvPlaylist 		= tvPlaylist;
			_tvFiles			= tvFiles;
			_MainWindow 		= MainWindow;
			_nbLeft				= nbLeft;
			_nbRight			= nbRight;
			_hsVolume			= hsVolume;
			_hsProgress 		= hsProgress;
			_iConnectionStatus 	= iConnectionStatus;
			_lStatus			= lStatus;
			_tbMute				= tbMute;
			_ibPlay				= ibPlay;
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
			
			MainWindow.Icon = new Gdk.Pixbuf("Interface/" + theme + "/icons/icon.png");
			
			ibPrevious	= new Gtk.Image(new Gdk.Pixbuf("Interface/" + theme + "/buttons/previous_32.png"));
			ibPrevious.ShowAll();
			ibPlay 		= new Gtk.Image(new Gdk.Pixbuf("Interface/" + theme + "/buttons/play_32.png"));
			ibStop 		= new Gtk.Image(new Gdk.Pixbuf("Interface/" + theme + "/buttons/stop_32.png"));
			ibNext 		= new Gtk.Image(new Gdk.Pixbuf("Interface/" + theme + "/buttons/next_32.png"));
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
			else if (args.Event.Button == 3)
				oArtistBrowser.ShowContextMenu();
		}
		
		protected void on_tvAlbums_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (args.Event.Button == 1)
				oAlbumBrowser.GetAlbumSongs();
			else if (args.Event.Button == 3)
				oAlbumBrowser.ShowContextMenu();
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
			else if (args.PageNum == 3)
				oAlbumBrowser.Populate();
		}
		
		protected void on_nbRight_switch_page (object o, Gtk.SwitchPageArgs args)
		{
			if (args.PageNum == 2)
				oPlaylist.SelectNowPlayingEntry();
		}
		
		protected void on_eArtistsFilter_changed (object o, EventArgs args)
		{
			oArtistBrowser.Populate(eArtistsFilter.Text);
		}
		
		protected void on_eAlbumsFilter_changed (object o, EventArgs args)
		{
			oAlbumBrowser.Populate(eAlbumsFilter.Text);
		}
		
		protected void on_tbMute_released (object o, EventArgs args)
		{
			oXbmc.Controls.ToggleMute();
		}
	}
}
