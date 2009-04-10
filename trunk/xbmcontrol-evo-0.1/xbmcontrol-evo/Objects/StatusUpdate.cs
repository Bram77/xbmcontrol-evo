
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
		private bool isMuted, isNotPlaying, isPlaying, isRunning;
		
		public StatusUpdate(XbmControlEvo parent)
		{
			_parent 		= parent;
			pathNowPlaying	= null;
			isRunning		= false;
		}

		internal void Start()
		{
			_parent.SetConnected(true);
			GLib.Timeout.Add((uint)(Convert.ToInt32(_parent.oConfiguration.values.updateInterval) * 1000), new GLib.TimeoutHandler(Update));
		}
		
		internal void Stop ()
		{
			_parent.SetConnected(false);
		}
		
		internal bool IsRunning()
		{
			return isRunning;
		}

		private bool Update()
		{
			if (_parent.oXbmc.Controls.SetResponseFormat() && _parent.IsConnected())
			{
				isRunning = true;
				_parent.SetConnected(true);
				_parent.bConnect.Image 			= new Image(_parent.oImages.menu.connect);
				_parent.lStatus.Text			= "Connected to " + _parent.oConfiguration.values.identifier + " ";
				_parent.bConnect.TooltipText	= "Disconnect from XBMC";
				
				currentVolume 		= _parent.oXbmc.Status.GetVolume();
				currentProgress		= _parent.oXbmc.Status.GetProgress();
				progress			= _parent.oXbmc.NowPlaying.Get("time");
				isMuted				= _parent.oXbmc.Status.IsMuted();
				isNotPlaying		= _parent.oXbmc.Status.IsNotPlaying();
				isPlaying			= _parent.oXbmc.Status.IsPlaying();
				nowPlayingFilename	= _parent.oXbmc.NowPlaying.Get("filename", true);
				
				_parent.hsProgress.TooltipText 			= progress;
				_parent.hsVolume.TooltipText			= currentVolume + "%";
				_parent.tbMute.Active 					= (isMuted)? true : false ;
				_parent.bShuffle.Active					= (_parent.oXbmc.Status.ShuffleEnabled())? true : false ;
				_parent.bStop.Active 					= (isNotPlaying)? true : false ;
				_parent.bPlay.Image						= (isPlaying)? new Image(_parent.oImages.button.pause) : new Image(_parent.oImages.button.play) ;
				
				if (!_parent.hsVolume.HasFocus) 
					_parent.hsVolume.Value = Convert.ToDouble(currentVolume);
				if (!_parent.hsProgress.HasFocus) 
					_parent.hsProgress.Value = Convert.ToDouble(currentProgress);
				
				if (pathNowPlaying != nowPlayingFilename)
				{
					if (!_parent.oXbmc.Status.IsNotPlaying()) 
					{
						_parent.oNowPlaying.LoadData();
						_parent.oSysTrayIcon.ShowNowPlayingBallonTip(null, null);
					}	
					
					pathNowPlaying = nowPlayingFilename;
					_parent.oPlaylist.Populate();
				}
				
				return true;
			}
			else
			{
				isRunning = false;
				_parent.SetConnected(false);
				_parent.bConnect.Image			= new Image(_parent.oImages.menu.disconnect);
				_parent.lStatus.Text			= (_parent.oConfiguration.values.ipAddress == "")? "No ip address configured for "+ _parent.oConfiguration.values.identifier : "Connection with " +_parent.oConfiguration.values.identifier+ " timed out ";
				_parent.bConnect.TooltipText 	= "Connect to XBMC";
				
				return false;
			}
		}
	}
}
