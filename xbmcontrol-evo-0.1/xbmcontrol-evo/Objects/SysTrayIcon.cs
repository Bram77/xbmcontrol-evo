
using System;
using Gtk;
using Gdk;
using Notifications;

namespace xbmcontrolevo
{
	
	
	public class SysTrayIcon
	{
		private StatusIcon siTray;
		private XbmControlEvo _parent;
		private Notification balloonTip;
		private string genre, year, nowPlayingInfo;
		
		public SysTrayIcon(XbmControlEvo parent)
		{
			_parent 			= parent;
			
			siTray 				= new StatusIcon(_parent.oImages.menu.icon);
			siTray.Visible 		= true;
			siTray.Activate 	+= OnActivate;
			siTray.PopupMenu 	+= OnTrayIconPopup;
			siTray.Tooltip 		= "XBMControl Evo";
			balloonTip			= new Notification();
			
			//ShowBallonTip();
		}
		
		private void OnTrayIconPopup (object o, EventArgs args) 
		{
			_parent.oContextMenu.Show("default", null);
		}
		
		private void OnActivate(object o, EventArgs args)
		{
			_parent.MainWindow.Visible = !_parent.MainWindow.Visible;
		}
		
		public void Show ()
		{
			siTray.Visible = true;
		}
		
		public void Hide ()
		{
			siTray.Visible = false;
		}
		
		public void ShowNowPlayingBallonTip (object o, EventArgs args)
		{
			genre			= (_parent.oNowPlaying.Get("genre") == "Unknown" || _parent.oNowPlaying.Get("genre") == "" || _parent.oNowPlaying.Get("genre") == null)? "" : " [" +_parent.oNowPlaying.Get("genre")+ "]" ;
			year			= (_parent.oNowPlaying.Get("year") == "" || _parent.oNowPlaying.Get("year") == null)? "" : " [" +_parent.oNowPlaying.Get("year")+ "]";
			Pixbuf coverArt = _parent.oNowPlaying.GetCoverArt();
			bool lastfm 	= (_parent.oXbmc.Status.IsPlayingLastFm())? true : false;
			
			nowPlayingInfo	= "* " + _parent.oNowPlaying.Get("artist") + genre + "\n* " + _parent.oNowPlaying.Get("title") + " [" + _parent.oNowPlaying.Get("duration") + "]\n* " + _parent.oNowPlaying.Get("album") + year;
			
			
			//balloonTip.Icon		= (lastfm)? _parent.oImages.button.lastfm : _parent.oImages.button.play;
			balloonTip.Timeout	= 10000;
			balloonTip.Icon		= coverArt.ScaleSimple(60, 60, InterpType.Bilinear);
			balloonTip.Summary	= "XBMControl Evo - Now Playing";
			balloonTip.Summary	+= (lastfm)? "(lastFM)" : "" ;
			balloonTip.Body		= nowPlayingInfo;
			balloonTip.Urgency	= Urgency.Critical;
			balloonTip.AttachToStatusIcon(siTray);
			balloonTip.Show();
			siTray.Tooltip		= nowPlayingInfo;
		}
	}
}
