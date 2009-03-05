
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
			Gtk.Image img		= new Gtk.Image("images/quit_16.png");
			ImageMenuItem quit  = new ImageMenuItem("Quit");
			quit.Image 			= img;
			quit.Activated 		+= delegate { Application.Quit(); };
			
			return quit;
		}
		
		public ImageMenuItem PlayPause()
		{
			ImageMenuItem playPause;
			Gtk.Image img;
				
			if (_parent.oXbmc.Status.IsPlaying())
			{
				img			= new Gtk.Image("images/pause_16.png");
				playPause	= new ImageMenuItem("Pause");
			}
			else
			{
				img			= new Gtk.Image("images/play_16.png");
				playPause 	= new ImageMenuItem("Play");
			}
			
			playPause.Image 	= img;
			playPause.Activated	+= delegate { PlayPause(); };
			playPause.Activated += delegate { _parent.oXbmc.Controls.Play(); };
			
			return playPause;
		}
		
		public ImageMenuItem PlayDirectory(string mediaPath, string mediaType)
		{
			Gtk.Image img				= new Gtk.Image("images/play_16.png");
			ImageMenuItem playRecursive	= new ImageMenuItem("Play");
			playRecursive.Image 		= img;
			playRecursive.Activated 	+= delegate { _parent.oControls.AddDirectoryContentToPlaylist(mediaPath, true, true); };
		
			return playRecursive;
		}
		
		public ImageMenuItem EnqueDirectory(string mediaPath, string mediaType)
		{
			Gtk.Image img					= new Gtk.Image("images/add_16.png");
			ImageMenuItem enqueRecursive	= new ImageMenuItem("Enque");
			enqueRecursive.Image 			= img;
			enqueRecursive.Activated 		+= delegate { _parent.oControls.AddDirectoryContentToPlaylist(mediaPath, false, true); };
		
			return enqueRecursive;
		}
		
		public ImageMenuItem PlayFile(string filePath)
		{
			Gtk.Image img			= new Gtk.Image("images/play_16.png");
			ImageMenuItem playFile	= new ImageMenuItem("Play");
			playFile.Image 			= img;
			playFile.Activated 		+= delegate { _parent.oControls.AddFileToPlaylist(filePath, true); };
		
			return playFile;
		}
		
		public ImageMenuItem EnqueFile(string filePath)
		{
			Gtk.Image img			= new Gtk.Image("images/add_16.png");
			ImageMenuItem enqueFile	= new ImageMenuItem("Enque");
			enqueFile.Image 		= img;
			enqueFile.Activated 	+= delegate { _parent.oControls.AddFileToPlaylist(filePath, false); };
		
			return enqueFile;
		}
		
		public ImageMenuItem Next()
		{
			Gtk.Image img		= new Gtk.Image("images/next_16.png");
			ImageMenuItem next 	= new ImageMenuItem("Next");
			next.Image 			= img;
			next.Activated 		+= delegate { _parent.oXbmc.Controls.Next(); };
			
			return next;
		}
		
		public ImageMenuItem Previous()
		{
			Gtk.Image img			= new Gtk.Image("images/next_16.png");
			ImageMenuItem previous 	= new ImageMenuItem("Next");
			previous.Image 			= img;
			previous.Activated 		+= delegate { _parent.oXbmc.Controls.Previous(); };
			
			return previous;
		}
		
		public ImageMenuItem Stop()
		{
			Gtk.Image img		= new Gtk.Image("images/stop_16.png");
			ImageMenuItem stop 	= new ImageMenuItem("Stop");
			stop.Image 			= img;
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
		
		public ImageMenuItem ShowFileInfo()
		{

			Gtk.Image img			= new Gtk.Image("images/information_16.png");
			ImageMenuItem config 	= new ImageMenuItem("Show info");
			config.Image 			= img;
			config.Activated 		+= delegate { _parent.oControls.GetFileInfo(); };
			
			return config;
		}
		
		public ImageMenuItem Configuration()
		{
			Gtk.Image img			= new Gtk.Image("images/configuration_16.png");
			ImageMenuItem config 	= new ImageMenuItem("Configuration");
			config.Image 			= img;
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
	}
}
