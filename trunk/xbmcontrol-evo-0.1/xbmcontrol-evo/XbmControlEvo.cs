
using System;
using System.IO;
using System.Reflection;
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
		public Images oImages;
		
		//Settings
		public string theme;
		private bool isConnected;
		public string appDir, appUserDir, configFile;
		public string interfaceDir;
		public bool DEBUG;
		
		public XbmControlEvo (string[] args)
		{
			Application.Init();
			
			DEBUG 			= true;
			theme 			= "default";
			isConnected 	= false;
			appDir			= Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
			interfaceDir	= appDir + "/Interface";
			appUserDir		= Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/.xbmcontrol-evo";
			configFile		= appUserDir + "/config.xml";
			
			if (!Directory.Exists(appUserDir)) 
				Directory.CreateDirectory(appUserDir);
			
			if (!Directory.Exists(appUserDir + "/Interface/default") || !File.Exists(appUserDir + "/Interface/default.glade"))
				CopyDefaultThemes(appDir + "/Interface", appUserDir + "/Interface", true);
			
			string gladeFilePath 		= (File.Exists(appUserDir + "/Interface/" + theme + ".glade"))? appUserDir + "/Interface/" + theme + ".glade" : appUserDir + "/Interface/default.glade" ;
			Glade.XML wMainWindowXml 	= new Glade.XML(gladeFilePath, null, null);
			wMainWindowXml.Autoconnect(this);
			
			InitObjects();
			ApplyTheme();
			XbmcConnect();
			Application.Run();
		}
		
		//Window
		[Glade.Widget] internal Window MainWindow;
		
		//[Widget] Gtk.Image ibPrevious;
		
		//ComboBox
		[Glade.Widget] internal ComboBox cbShares;
		[Glade.Widget] internal ComboBox cbPlaylist;
		[Glade.Widget] internal ComboBox cbTheme;
		
		//NoteBook
		[Glade.Widget] internal Notebook nbLeft;
		[Glade.Widget] internal Notebook nbRight;
		
		//Button
		[Glade.Widget] internal Button bPrevious;
		[Glade.Widget] internal Button bPlay;
		[Glade.Widget] internal ToggleButton bStop;
		[Glade.Widget] internal Button bNext;
		[Glade.Widget] internal Button bPlaylistClear;
		[Glade.Widget] internal Button bPlaylistRefresh;
		[Glade.Widget] internal Button bPlaylistRemove;
		[Glade.Widget] internal Button bPlaylistPlay;
		[Glade.Widget] internal Button bConnect;
		
		//ToggleButton
		[Glade.Widget] internal ToggleButton tbMute;
		
		//HScale
		[Glade.Widget] internal HScale hsProgress;
		[Glade.Widget] internal HScale hsVolume;
		
		//Label
		[Glade.Widget] internal Label lStatus;
		
		//Entry
		[Glade.Widget] internal Entry eArtistsFilter;
		[Glade.Widget] internal Entry eAlbumsFilter;
		[Glade.Widget] internal Entry eIpAddress;
		[Glade.Widget] internal Entry eUsername;
		[Glade.Widget] internal Entry ePassword;
		
		//SpinButton
		[Glade.Widget] internal SpinButton sbUpdateInterval;
		[Glade.Widget] internal SpinButton sbConnectionTimeout;
		
		//CheckButton
		[Glade.Widget] internal CheckButton chbShowInSystemTray;
		[Glade.Widget] internal CheckButton chbShowInTaskbar;
		[Glade.Widget] internal CheckButton chbCloseToSystemTray;
		
		//TreeView
		[Glade.Widget] internal TreeView tvShares;
		[Glade.Widget] internal TreeView tvGenres;
		[Glade.Widget] internal TreeView tvArtists;
		[Glade.Widget] internal TreeView tvAlbums;
		[Glade.Widget] internal TreeView tvSongs;
		[Glade.Widget] internal TreeView tvVideos;
		[Glade.Widget] internal TreeView tvPlaylist;
		[Glade.Widget] internal TreeView tvFiles;
		
		
		/// <summary>
	    ///  Initialise objects
	    /// </summary>
		private void InitObjects ()
		{
			oHelper			= new HelperFunctions(this);
			oImages			= new Images(this);
			oConfiguration 	= new Configuration(this);
			oXbmc 			= new XBMC_Communicator();
			oShareBrowser 	= new ShareBrowser(this);
			oFileBrowser	= new FileBrowser(this);
			oMenuItems		= new MenuItems(this);
			oContextMenu	= new ContextMenu(this);
			oSysTrayIcon 	= new SysTrayIcon(this);
			oControls		= new Controls(this);
			oPlaylist		= new Playlist(this);
			oGenreBrowser	= new GenreBrowser(this);
			oArtistBrowser 	= new ArtistBrowser(this);
			oAlbumBrowser	= new AlbumBrowser(this);
			oStatusUpdate	= new StatusUpdate(this);
		}
		
		private void XbmcConnect ()
		{
			oStatusUpdate.Stop();
			if (oConfiguration.values.ipAddress != "")
			{
				oXbmc.SetIp(oConfiguration.values.ipAddress);
		        oXbmc.SetConnectionTimeout(Convert.ToInt32(oConfiguration.values.connectionTimeout) * 1000);
		        oXbmc.SetCredentials(oConfiguration.values.username, oConfiguration.values.password);
				this.isConnected = (oXbmc.Status.WebServerEnabled()) ? true : false ;
				
				if (IsConnected())
				{
					if (!oStatusUpdate.IsRunning()) 
						oStatusUpdate.Start();
					SetStartupvalues();
					oStatusUpdate = new StatusUpdate(this);
				}
				else
				{
					oStatusUpdate.Stop();
					oHelper.Messagebox("Could not connect to XBMC with the current configuration");
					nbRight.CurrentPage = 3;
				}
			}
			else
			{
				oStatusUpdate.Stop();
				oHelper.Messagebox("A valid ip address or hostname is required");
				nbRight.CurrentPage = 3;
			}
		}

		public void SetConnected (bool connected)
		{
			isConnected = connected;
		}
		
		public bool IsConnected ()
		{
			return isConnected;
		}
		
		private void SetStartupvalues ()
		{
			cbShares.Active 			= 0;
			cbPlaylist.Active 			= 0;
			MainWindow.SkipTaskbarHint 	= (oConfiguration.values.showInTaskbar)? false : true;
			
			if (oConfiguration.values.showInSystemTray)
				oSysTrayIcon.Show();
			else
				oSysTrayIcon.Hide();
		}
		
		protected void CopyDefaultThemes (string src, string dest, bool recursive)
		{
			DirectoryInfo source = new DirectoryInfo(src);
	        if (!source.Exists) throw new DirectoryNotFoundException("Source directory not found: " + source.FullName);
			
	        DirectoryInfo target = new DirectoryInfo(dest);
	        if (!target.Exists) target.Create();
	
	        foreach (FileInfo file in source.GetFiles())
	        {
	            file.CopyTo(Path.Combine(target.FullName, file.Name), true);
	        }

			if (!recursive) return;
			
	        foreach (DirectoryInfo directory in source.GetDirectories())
	        {
	            CopyDefaultThemes((src+"/"+directory.Name), Path.Combine(target.FullName, directory.Name), recursive);
	        }
		}
		
		protected void ApplyTheme ()
		{
			MainWindow.Icon			= oImages.menu.icon;
			bPrevious.Image			= new Image(oImages.button.previous);
			bPlay.Image				= new Image(oImages.button.play);
			bStop.Image				= new Image(oImages.button.stop);
			bNext.Image				= new Image(oImages.button.next);
			tbMute.Image			= new Image(oImages.menu.mute);
			bPlaylistClear.Image 	= new Image(oImages.menu.clear);
			bPlaylistRefresh.Image	= new Image(oImages.menu.refresh);
			bPlaylistRemove.Image	= new Image(oImages.menu.minus);
			bPlaylistPlay.Image		= new Image(oImages.menu.play);
			bConnect.Image			= new Image(oImages.menu.disconnect);
		}
		
		protected void on_MainWindow_delete_event (object sender, DeleteEventArgs a)
		{
			if (oConfiguration.values.closeToSystemTray)
			{
				MainWindow.Visible = false;
				a.RetVal = true;
			}
			else
			{
				Application.Quit();
				a.RetVal = true;
			}
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
			{
				if (args.PageNum == 2) 
					oPlaylist.SelectNowPlayingEntry();	
			}
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
		
		protected void on_bConfigurationSave_released (object o, EventArgs args)
		{
			oConfiguration.Save();
			XbmcConnect();
		}
		
		protected void on_bConnect_released (object o, EventArgs args)
		{
			if (IsConnected())
				oStatusUpdate.Stop();
			else
			{
				XbmcConnect();
				oStatusUpdate.Start();
			}
		}
		
		protected void on_chbCloseToSystemTray_released (object o, EventArgs args)
		{
			if (chbCloseToSystemTray.Active)
			{
				chbShowInSystemTray.Active 		= true;
				chbShowInSystemTray.Sensitive 	= false;
				chbShowInTaskbar.Sensitive 		= true;
			}
			else
				chbShowInSystemTray.Sensitive = true;
		}
		
		protected void on_chbShowInTaskbar_released (object o, EventArgs args)
		{
			if (!chbShowInTaskbar.Active)
			{
				chbShowInSystemTray.Sensitive 	= false;
				chbShowInSystemTray.Active 		= true;
			}
			else
				chbShowInSystemTray.Sensitive 	= true;
		}
		
		protected void on_chbShowInSystemTray_released (object o, EventArgs args)
		{
			if (!chbShowInSystemTray.Active)
			{
				chbShowInTaskbar.Sensitive 	= false;
				chbShowInTaskbar.Active 		= true;
			}
			else
				chbShowInTaskbar.Sensitive 	= true;
		}
	}
}
