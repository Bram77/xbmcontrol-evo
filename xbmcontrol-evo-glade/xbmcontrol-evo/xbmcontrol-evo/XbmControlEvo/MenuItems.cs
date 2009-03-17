
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class MenuItems
	{
		private MainWindow _parent;
		
		public MenuItems(MainWindow parent)
		{
			_parent = parent;
		}
		
		public ImageMenuItem Seperator()
		{
			return new ImageMenuItem();
		}
		
		public ImageMenuItem Quit()
		{
			ImageMenuItem quit  = new ImageMenuItem("Quit");
			quit.Image 			= new Gtk.Image(Gtk.Stock.Quit, IconSize.Menu);;
			quit.Activated 		+= delegate { Application.Quit(); };
			
			return quit;
		}
		
		public ImageMenuItem PlayPause()
		{
			ImageMenuItem playPause;
				
			if (_parent.oXbmc.Status.IsPlaying())
			{
				playPause		= new ImageMenuItem("Pause");
				playPause.Image = new Gtk.Image(Gtk.Stock.MediaPause, IconSize.Menu);
			}
			else
			{
				playPause 		= new ImageMenuItem("Play");
				playPause.Image = new Gtk.Image(Gtk.Stock.MediaPlay, IconSize.Menu);
			}
			
			playPause.Activated	+= delegate { PlayPause(); };
			playPause.Activated += delegate { _parent.oXbmc.Controls.Play(); };
			
			return playPause;
		}
		
		public ImageMenuItem PlayDirectory(string mediaPath, string mediaType)
		{
			ImageMenuItem playRecursive	= new ImageMenuItem("Play");
			playRecursive.Image 		= new Gtk.Image(Gtk.Stock.MediaPlay, IconSize.Menu);;
			playRecursive.Activated 	+= delegate { _parent.oControls.AddDirectoryContentToPlaylist(mediaPath, true); };
		
			return playRecursive;
		}
		
		public ImageMenuItem EnqueDirectory(string mediaPath, string mediaType)
		{
			ImageMenuItem enqueRecursive	= new ImageMenuItem("Enque");
			enqueRecursive.Image 			= new Gtk.Image(Gtk.Stock.Add, IconSize.Menu);;
			enqueRecursive.Activated 		+= delegate { _parent.oControls.AddDirectoryContentToPlaylist(mediaPath, false); };
		
			return enqueRecursive;
		}
		
		public ImageMenuItem PlayFile(string filePath)
		{
			ImageMenuItem playFile	= new ImageMenuItem("Play");
			playFile.Image 			= new Gtk.Image(Gtk.Stock.MediaPlay, IconSize.Menu);;
			playFile.Activated 		+= delegate { _parent.oControls.AddFileToPlaylist(filePath, true); };
			
			return playFile;
		}
		
		public ImageMenuItem EnqueFile(string filePath)
		{
			ImageMenuItem enqueFile	= new ImageMenuItem("Enque");
			enqueFile.Image 		= new Gtk.Image(Gtk.Stock.Add, IconSize.Menu);;
			enqueFile.Activated 	+= delegate { _parent.oControls.AddFileToPlaylist(filePath, false); };
		
			return enqueFile;
		}
		
		public ImageMenuItem Next()
		{
			ImageMenuItem next 	= new ImageMenuItem("Next");
			next.Image 			= new Gtk.Image(Gtk.Stock.MediaNext, IconSize.Menu);;
			next.Activated 		+= delegate { _parent.oXbmc.Controls.Next(); };
			
			return next;
		}
		
		public ImageMenuItem Previous()
		{
			ImageMenuItem previous 	= new ImageMenuItem("Previous");
			previous.Image 			= new Gtk.Image(Gtk.Stock.MediaPrevious, IconSize.Menu);;
			previous.Activated 		+= delegate { _parent.oXbmc.Controls.Previous(); };
			
			return previous;
		}
		
		public ImageMenuItem Stop()
		{
			ImageMenuItem stop 	= new ImageMenuItem("Stop");
			stop.Image 			= new Gtk.Image(Gtk.Stock.MediaStop, IconSize.Menu);;
			stop.Activated 		+= delegate { _parent.oXbmc.Controls.Stop(); };
			
			return stop;
		}
		
		public ImageMenuItem Mute()
		{
			Gtk.Image img		= new Gtk.Image("images/volume_mute_16.png");
			
			string text = (_parent.oXbmc.Status.IsMuted())? "Unmute" : "Mute" ;
			ImageMenuItem mute 	= new ImageMenuItem(text);
			
			mute.Image 			= img;
			mute.Activated 		+= delegate { _parent.oXbmc.Controls.ToggleMute(); };
			
			return mute;
		}
		
		public ImageMenuItem VolumeUp()
		{
			int currentVolume 	= _parent.oXbmc.Status.GetVolume();
			int newVolume		= ( (currentVolume+10) > 100 )? 100 : currentVolume+10 ;
			
			Gtk.Image img			= new Gtk.Image("images/volume_up_16.png");
			ImageMenuItem volumeUp 	= new ImageMenuItem("Increase volume");
			volumeUp.Image 			= img;
			volumeUp.Activated 		+= delegate { _parent.oXbmc.Controls.SetVolume(newVolume); };
			
			return volumeUp;
		}
		
		public ImageMenuItem VolumeDown()
		{
			int currentVolume 	= _parent.oXbmc.Status.GetVolume();
			int newVolume		= ( (currentVolume-10) < 0 )? 0 : currentVolume-10 ;
			
			Gtk.Image img				= new Gtk.Image("images/volume_down_16.png");
			ImageMenuItem volumeDown 	= new ImageMenuItem("Decrease volume");
			volumeDown.Image 			= img;
			volumeDown.Activated 		+= delegate { _parent.oXbmc.Controls.SetVolume(newVolume); };
			
			return volumeDown;
		}
		
		public ImageMenuItem ShowSongInfo(string caller)
		{
			ImageMenuItem config 	= new ImageMenuItem("Show info");
			config.Image 			= new Gtk.Image(Gtk.Stock.Info, IconSize.Menu);
			if (caller == "sharebrowser")
				config.Activated += delegate { _parent.oShareBrowser.ShowSongInfoPopup(); };
			else if (caller == "playlist")
				config.Activated += delegate { _parent.oPlaylist.ShowSongInfoPopup(); };
			
			return config;
		}
		
		public ImageMenuItem Configuration()
		{
			ImageMenuItem config 	= new ImageMenuItem("Configuration");
			config.Image 			= new Gtk.Image(Gtk.Stock.Preferences, IconSize.Menu);
			config.Activated 		+= delegate { Application.Quit(); };
			
			return config;
		}
		
		public ImageMenuItem CollapseAll()
		{
			Gtk.Image img			= new Gtk.Image("images/collapse_16.gif");
			ImageMenuItem collapse 	= new ImageMenuItem("Collapse All");
			collapse.Image 			= img;
			collapse.Activated 		+= delegate { _parent.CollapseAllShares(); };
			
			return collapse;
		}
		
		public ImageMenuItem PlayPlaylistEntry()
		{
			ImageMenuItem playEntry 	= new ImageMenuItem("Play");
			playEntry.Image 			= new Gtk.Image(Gtk.Stock.MediaPlay, IconSize.Menu);
			playEntry.Activated 		+= delegate { _parent.oPlaylist.PlaySelectedItem(); };
			
			return playEntry;
		}
		
		public ImageMenuItem RemovePlaylistEntry()
		{
			ImageMenuItem removeEntry 	= new ImageMenuItem("Remove");
			removeEntry.Image 			= new Gtk.Image(Gtk.Stock.Remove, IconSize.Menu);
			removeEntry.Activated 		+= delegate { _parent.oPlaylist.RemoveSelectedItem(); };
			
			return removeEntry;
		}
		
		public ImageMenuItem ClearPlaylist()
		{
			ImageMenuItem clearPlaylist	= new ImageMenuItem("Clear");
			clearPlaylist.Image 		= new Gtk.Image(Gtk.Stock.Clear, IconSize.Menu);
			clearPlaylist.Activated 	+= delegate { _parent.oPlaylist.Clear(); };
			
			return clearPlaylist;
		}
		
		/*
		public ImageMenuItem SaveSelectedFile()
		{
			ImageMenuItem saveAs 	= new ImageMenuItem("Save As");
			saveAs.Image 			= new Gtk.Image(Gtk.Stock.SaveAs, IconSize.Menu);
			saveAs.Activated 		+= delegate { _parent.oShareBrowser.SaveSelectedFile(); };
			
			return saveAs;
		}
		*/
		
		public ImageMenuItem RefreshPlaylist()
		{
			ImageMenuItem refreshPlaylist	= new ImageMenuItem("Refresh");
			refreshPlaylist.Image 			= new Gtk.Image(Gtk.Stock.Refresh, IconSize.Menu);
			refreshPlaylist.Activated 		+= delegate { _parent.oPlaylist.Populate(); };
			
			return refreshPlaylist;
		}
	}
}
