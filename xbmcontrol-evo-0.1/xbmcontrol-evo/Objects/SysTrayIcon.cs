
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
			
			this.siTray 			= new StatusIcon(this._parent.oImages.menu.icon);
			this.siTray.Visible 	= true;
			this.siTray.Activate 	+= OnActivate;
			this.siTray.PopupMenu 	+= OnTrayIconPopup;
			this.siTray.Tooltip 	= "XBMControl Evo";
			this.balloonTip			= new Notification();
			
			//ShowBallonTip();
		}
		
		private void OnTrayIconPopup (object o, EventArgs args) 
		{
			this._parent.oContextMenu.Show("default", null);
		}
		
		private void OnActivate(object o, EventArgs args)
		{
			this._parent.MainWindow.Visible = !this._parent.MainWindow.Visible;
		}
		
		public void Show ()
		{
			this.siTray.Visible = true;
		}
		
		public void Hide ()
		{
			this.siTray.Visible = false;
		}
		
		public void ShowNowPlayingBallonTip (object o, EventArgs args)
		{
			string genre	= (this._parent.oNowPlaying.Get("genre") == "Unknown" || this._parent.oNowPlaying.Get("genre") == "" || this._parent.oNowPlaying.Get("genre") == null)? "" : " [" +this._parent.oNowPlaying.Get("genre")+ "]" ;
			string year		= (this._parent.oNowPlaying.Get("year") == "" || this._parent.oNowPlaying.Get("year") == null)? "" : " [" +this._parent.oNowPlaying.Get("year")+ "]";
			Pixbuf coverArt = this._parent.oNowPlaying.GetCoverArt();
			bool lastfm 	= (this._parent.oXbmc.Status.IsPlayingLastFm())? true : false;
			nowPlayingInfo	= string.Format("~ {1}{2}{0}~ {3} [{4}]{0}~ {5}{6}", Environment.NewLine, this._parent.oNowPlaying.Get("artist"), genre, this._parent.oNowPlaying.Get("title"), this._parent.oNowPlaying.Get("duration"), this._parent.oNowPlaying.Get("album"), year);
			
			
			//this.balloonTip.Icon		= (lastfm)? this._parent.oImages.button.lastfm : this._parent.oImages.button.play;
			this.balloonTip.Timeout		= 10000;
			this.balloonTip.Icon		= coverArt.ScaleSimple(100, 100, InterpType.Bilinear);
			this.balloonTip.Summary		= "XBMControl Evo - Now Playing";
			this.balloonTip.Summary		+= (lastfm)? "(lastFM)" : "" ;
			this.balloonTip.Body		= nowPlayingInfo;
			this.balloonTip.Urgency		= Urgency.Critical;
			this.balloonTip.AttachToStatusIcon(siTray);
			this.balloonTip.Show();
			this.siTray.Tooltip			= nowPlayingInfo;
		}
	}
}
