
using System;
using Gtk;
using Gdk;
using XBMC;

namespace xbmcontrolevo
{
	
	
	public class MenuItems
	{
		private XBMC_Communicator _Xbmc;
		private MainWindow _parent;
		
		public MenuItems(MainWindow parent)
		{
			_parent = parent;
			_Xbmc = parent.Xbmc;
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
		
		public ImageMenuItem Play()
		{
			Gtk.Image img		= new Gtk.Image("images/play_16.png");
			ImageMenuItem play 	= new ImageMenuItem("Play");
			play.Image 			= img;
			play.Activated 		+= delegate { _Xbmc.Controls.Play(); };
			
			return play;
		}
		
		public ImageMenuItem PlayRecursive(string mediaPath, string mediaType)
		{
			Gtk.Image img				= new Gtk.Image("images/play_16.png");
			ImageMenuItem playRecursive	= new ImageMenuItem("Play Recursive");
			playRecursive.Image 		= img;
			playRecursive.Activated 	+= delegate { _parent.controls.AddDirectoryContentToPlaylist(mediaPath, mediaType, true, true); };
		
			return playRecursive;
		}
		
		public ImageMenuItem PlayFile(string filePath)
		{
			Gtk.Image img			= new Gtk.Image("images/play_16.png");
			ImageMenuItem playFile	= new ImageMenuItem("Play");
			playFile.Image 			= img;
			playFile.Activated 		+= delegate { _parent.controls.AddFileToPlaylist(filePath, true); };
		
			return playFile;
		}
		
		public ImageMenuItem Next()
		{
			Gtk.Image img		= new Gtk.Image("images/next_16.png");
			ImageMenuItem next 	= new ImageMenuItem("Next");
			next.Image 			= img;
			next.Activated 		+= delegate { _Xbmc.Controls.Next(); };
			
			return next;
		}
		
		public ImageMenuItem Previous()
		{
			Gtk.Image img			= new Gtk.Image("images/next_16.png");
			ImageMenuItem previous 	= new ImageMenuItem("Next");
			previous.Image 			= img;
			previous.Activated 		+= delegate { _Xbmc.Controls.Previous(); };
			
			return previous;
		}
		
		public ImageMenuItem Stop()
		{
			Gtk.Image img		= new Gtk.Image("images/stop_16.png");
			ImageMenuItem stop 	= new ImageMenuItem("Stop");
			stop.Image 			= img;
			stop.Activated 		+= delegate { _Xbmc.Controls.Stop(); };
			
			return stop;
		}
		
		public ImageMenuItem Mute()
		{
			Gtk.Image img		= new Gtk.Image("images/mute_16.png");
			ImageMenuItem mute 	= new ImageMenuItem("Mute");
			mute.Image 			= img;
			mute.Activated 		+= delegate { _Xbmc.Controls.SetVolume(0); };
			
			return mute;
		}
		
		public ImageMenuItem Configuration()
		{
			Gtk.Image img			= new Gtk.Image("images/configuration_16.png");
			ImageMenuItem config 	= new ImageMenuItem("Configuration");
			config.Image 			= img;
			config.Activated 		+= delegate { Application.Quit(); };
			
			return config;
		}
	
	}
}
