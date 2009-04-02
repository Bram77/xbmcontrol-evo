
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
		private bool run;
		
		public StatusUpdate(XbmControlEvo parent)
		{
			_parent 		= parent;
			pathNowPlaying	= null;
			run 			= false;
		}

		internal void Start()
		{
			run = true;
			GLib.Timeout.Add((uint)(Convert.ToInt32(_parent.oConfiguration.values.updateInterval) * 1000), new GLib.TimeoutHandler(Update));
		}
		
		internal void Stop ()
		{
			run = false;
		}
		
		internal bool IsRunning ()
		{
			return run;
		}

		private bool Update()
		{
			if (run)
			{
				if (_parent.oXbmc.Controls.SetResponseFormat())
				{
					_parent.SetConnected(true);
					_parent.iConnectionStatus.SetFromStock("gtk-yes", IconSize.Menu);
					
					currentVolume 		= _parent.oXbmc.Status.GetVolume();
					currentProgress		= _parent.oXbmc.Status.GetProgress();
					progress			= _parent.oXbmc.NowPlaying.Get("time");
					isMuted				= _parent.oXbmc.Status.IsMuted();
					isNotPlaying		= _parent.oXbmc.Status.IsNotPlaying();
					isPlaying			= _parent.oXbmc.Status.IsPlaying();
					nowPlayingFilename	= _parent.oXbmc.NowPlaying.Get("filename", true);
				
					_parent.iConnectionStatus.TooltipText	= "Connected to XBMC";
					_parent.lStatus.Text 					= "Connected to XBMC";
					_parent.hsProgress.TooltipText 		= progress;
					_parent.hsVolume.TooltipText			= currentVolume + "%";
					_parent.tbMute.Active 					= (isMuted)? true : false ;
					_parent.bStop.Active 					= (isNotPlaying)? true : false ;
					_parent.ibPlay.Pixbuf					= (isPlaying)? _parent.oImages.button.pause : _parent.oImages.button.play ;
					
					if (!_parent.hsVolume.HasGrab) 
						_parent.hsVolume.Value = Convert.ToDouble(currentVolume);
					if (!_parent.hsProgress.HasGrab) 
						_parent.hsProgress.Value = Convert.ToDouble(currentProgress);
					
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
					_parent.iConnectionStatus.SetFromStock("gtk-no", IconSize.Menu);
					_parent.iConnectionStatus.TooltipText	= "Click this icon to connect to XBMC";
					_parent.lStatus.Text = "Connection to XBMC lost";
					
					return false;
				}
			}
			else
				return false;
		}
	}
}
