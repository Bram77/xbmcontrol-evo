
using System;
using GLib;
using Gtk;

namespace xbmcontrolevo
{
	
	public class StatusUpdate
	{
		private XbmControlEvo _parent;
		private int currentVolume, currentProgress;
		private string progress, nowPlayingFilename, pathNowPlaying;
		private bool isMuted, isNotPlaying, isPlaying;
		
		public StatusUpdate(XbmControlEvo parent)
		{
			_parent 		= parent;
			pathNowPlaying	= null;
		}

		internal void Start()
		{
			GLib.Timeout.Add(1000, new GLib.TimeoutHandler(Update));
		}

		private bool Update()
		{
			if (_parent.oXbmc.Controls.SetResponseFormat())
			{
				_parent.SetConnected(true);
				_parent._iConnectionStatus.SetFromStock("gtk-yes", IconSize.Menu);
				
				currentVolume 		= _parent.oXbmc.Status.GetVolume();
				currentProgress		= _parent.oXbmc.Status.GetProgress();
				progress			= _parent.oXbmc.NowPlaying.Get("time");
				isMuted				= _parent.oXbmc.Status.IsMuted();
				isNotPlaying		= _parent.oXbmc.Status.IsNotPlaying();
				isPlaying			= _parent.oXbmc.Status.IsPlaying();
				nowPlayingFilename	= _parent.oXbmc.NowPlaying.Get("filename", true);
			
				_parent._iConnectionStatus.TooltipText	= "Connected to XBMC";
				_parent._lStatus.Text 					= "Connected to XBMC";
				_parent._hsProgress.TooltipText 		= progress;
				_parent._hsVolume.TooltipText			= currentVolume + "%";
				_parent._tbMute.Active 					= (isMuted)? true : false ;
				_parent._bStop.Active 					= (isNotPlaying)? true : false ;
				_parent._ibPlay.Pixbuf					= (isPlaying)? new Gdk.Pixbuf("Interface/" + _parent.theme + "/buttons/pause_32.png") : new Gdk.Pixbuf("Interface/" + _parent.theme + "/buttons/play_32.png") ;
				
				if (!_parent._hsVolume.HasGrab) 
					_parent._hsVolume.Value = Convert.ToDouble(currentVolume);
				if (!_parent._hsProgress.HasGrab) 
					_parent._hsProgress.Value = Convert.ToDouble(currentProgress);
				
				if (pathNowPlaying != nowPlayingFilename)
				{
					pathNowPlaying = nowPlayingFilename;
					_parent.oPlaylist.Populate();
				}
				
				return true;
			}
			else
			{
				_parent.SetConnected(false);
				_parent._iConnectionStatus.SetFromStock("gtk-no", IconSize.Menu);
				_parent._iConnectionStatus.TooltipText	= "Click this icon to connect to XBMC";
				_parent._lStatus.Text = "Connection to XBMC lost";
				
				return false;
			}
		}
	}
}
