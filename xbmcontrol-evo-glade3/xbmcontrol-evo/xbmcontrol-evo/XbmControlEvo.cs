
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
		[Widget] ToggleButton bStop;
		
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
		public Configuration oConfiguration;
		
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
		public ToggleButton _bStop;
		
		//Settings
		public string theme;
		private bool isConnected;
		
		
		public XbmControlEvo (string[] args)
		{
			Application.Init();
			
			theme 		= "default";
			isConnected = false;
			
			Glade.XML wMainWindowXml 	= new Glade.XML ("Interface/" +theme+ ".glade", "MainWindow", null);
			wMainWindowXml.Autoconnect (this);

			this.AllowinternalAccess();
			this.InitObjects();
			
			Application.Run();
		}
		
		private void InitObjects ()
		{
			oXbmc 			= new XBMC_Communicator();
			oConfiguration	= new Configuration(this);
			oShareBrowser 	= new ShareBrowser(this);
			oFileBrowser	= new FileBrowser(this);
			oMenuItems		= new MenuItems(this);
			oContextMenu	= new ContextMenu(this);
			oControls		= new Controls(this);
			oPlaylist		= new Playlist(this);
			oHelper			= new HelperFunctions(this);
			oSysTrayIcon	= new SysTrayIcon(this);
			oGenreBrowser	= new GenreBrowser(this);
			oArtistBrowser 	= new ArtistBrowser(this);
			oAlbumBrowser	= new AlbumBrowser(this);
			oStatusUpdate	= new StatusUpdate(this);
			
			this.XbmcConnect();
			if (IsConnected())
			{
				oStatusUpdate.Start();
				SetStartupvalues ();
			}
			else
			{
				oHelper.Messagebox("Could not connect to XBMC with the current configuration.");
				nbRight.CurrentPage = 3;
			}
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
			_bStop				= bStop;
			//_eFilterFiles	= eFilterFiles;
		}
		
		private void XbmcConnect ()
		{
			oXbmc.SetIp("10.0.0.5");
	        oXbmc.SetConnectionTimeout(1000);
	        oXbmc.SetCredentials("", "");
			this.isConnected = (oXbmc.Status.WebServerEnabled()) ? true : false ;
			//oXbmc.Status.StartHeartBeat();
		}

		public void SetConnected (bool connected)
		{
			this.isConnected = connected;
		}
		
		public bool IsConnected ()
		{
			return this.isConnected;
		}
		
		private void SetStartupvalues ()
		{
			cbShares.Active 	= 0;
			cbPlaylist.Active 	= 0;
			
			MainWindow.Icon		= new Gdk.Pixbuf("Interface/" + theme + "/icons/icon.png");
			ibPrevious			= new Gtk.Image(new Gdk.Pixbuf("Interface/" + theme + "/buttons/previous_32.png"));
			ibPlay 				= new Gtk.Image(new Gdk.Pixbuf("Interface/" + theme + "/buttons/play_32.png"));
			ibStop 				= new Gtk.Image(new Gdk.Pixbuf("Interface/" + theme + "/buttons/stop_32.png"));
			ibNext 				= new Gtk.Image(new Gdk.Pixbuf("Interface/" + theme + "/buttons/next_32.png"));
		}
		
		protected void on_MainWindow_delete_event (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
		
		protected void on_cbShares_changed (object o, EventArgs args)
		{
			if (this.IsConnected())
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
		}
		
		protected void on_tvShares_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (this.IsConnected())
			{
				if (args.Event.Button == 1)
					oShareBrowser.ExpandSelectedDirectory();
				else if (args.Event.Button == 3)
					oShareBrowser.ShowContextMenu();
			}
		}
		
		protected void on_tvArtists_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (this.IsConnected())
			{
				if (args.Event.Button == 1)
					oArtistBrowser.ExpandeSelectedItem();
				else if (args.Event.Button == 3)
					oArtistBrowser.ShowContextMenu();
			}
		}
		
		protected void on_tvAlbums_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (this.IsConnected())
			{
				if (args.Event.Button == 1)
					oAlbumBrowser.GetAlbumSongs();
				else if (args.Event.Button == 3)
					oAlbumBrowser.ShowContextMenu();
			}
		}
		
		protected void on_tvFiles_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (this.IsConnected())
				if (args.Event.Button == 3) oFileBrowser.ShowContextMenu();
		}
		
		protected void on_tvPlaylist_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (this.IsConnected())
				if (args.Event.Button == 3) oPlaylist.ShowContextMenu();
		}
		
		protected void on_hsVolume_value_changed (object o, EventArgs args)
		{
			if (this.IsConnected())
			{
				if (hsVolume.HasFocus)
				{
					oXbmc.Controls.SetVolume(Convert.ToInt32(hsVolume.Value));
					hsVolume.TooltipText = Math.Floor((double) hsVolume.Value).ToString()+"%";
				}
			}
		}
		
		protected void on_hsProgress_value_changed (object o, EventArgs args)
		{
			if (this.IsConnected())
				if (hsProgress.HasFocus) oXbmc.Controls.SeekPercentage(Convert.ToInt32(hsProgress.Value));
		}
		
		protected void on_cbPlaylist_changed (object o, EventArgs args)
		{
			if (this.IsConnected()) oPlaylist.SetCurrentPlaylistType(cbPlaylist.Active.ToString());
		}
		
		protected void on_nbLeft_switch_page (object o, Gtk.SwitchPageArgs args)
		{
			if (this.IsConnected())
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
		}
		
		protected void on_nbRight_switch_page (object o, Gtk.SwitchPageArgs args)
		{
			if (this.IsConnected())
				if (args.PageNum == 2) oPlaylist.SelectNowPlayingEntry();
		}
		
		protected void on_eArtistsFilter_changed (object o, EventArgs args)
		{
			if (this.IsConnected()) oArtistBrowser.Populate(eArtistsFilter.Text);
		}
		
		protected void on_eAlbumsFilter_changed (object o, EventArgs args)
		{
			if (this.IsConnected()) oAlbumBrowser.Populate(eAlbumsFilter.Text);
		}
		
		protected void on_tbMute_released (object o, EventArgs args)
		{
			if (this.IsConnected()) oXbmc.Controls.ToggleMute();
		}
		
		protected void on_bPrevious_released (object o, EventArgs args)
		{
			if (this.IsConnected()) oXbmc.Controls.Previous();
		}
		
		protected void on_bPlay_released (object o, EventArgs args)
		{
			if (this.IsConnected()) oXbmc.Controls.Play();
		}
		
		protected void on_bStop_released (object o, EventArgs args)
		{
			if (this.IsConnected()) oXbmc.Controls.Stop();
		}
		
		protected void on_bNext_released (object o, EventArgs args)
		{
			if (this.IsConnected()) oXbmc.Controls.Next();
		}
		
		protected void on_bPlaylistClear_clicked (object o, EventArgs args)
		{
			if (this.IsConnected()) oPlaylist.Clear();
		}
		
		protected void on_bPlaylistRemove_clicked (object o, EventArgs args)
		{
			if (this.IsConnected()) oPlaylist.RemoveSelectedItems();
		}
		
		protected void on_bPlaylistRefresh_clicked (object o, EventArgs args)
		{
			if (this.IsConnected()) oPlaylist.Refresh();
		}
		
		protected void on_iConnectionStatus_button_release_event (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (!IsConnected())
			{
				iConnectionStatus.SetFromStock("gtk-connect", IconSize.Menu);
				oStatusUpdate.Start();
			}
			
			oHelper.Messagebox("test");
		}
	}
}
