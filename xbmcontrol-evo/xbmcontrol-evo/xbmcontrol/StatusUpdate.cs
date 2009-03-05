
using System;
using GLib;

namespace xbmcontrolevo
{
	
	public class StatusUpdate
	{
		private MainWindow _parent;
		
		public StatusUpdate(MainWindow parent)
		{
			_parent = parent;
			Update();
			Start();
		}
		
		public void Start()
		{
			GLib.Timeout.Add(1000, new GLib.TimeoutHandler(Update) );
		}
		
		public bool Update(bool start)
		{
			if (_parent.oXbmc.Status.IsConnected() && start)
			{
				if (!_parent._hsVolume.HasGrab) SetPbVolumePosition();
				if (!_parent._hsProgress.HasGrab) SetPbProgressPosition();
				_parent._tbMute.Active = (_parent.oXbmc.Status.IsMuted())? true : false ;
				_parent._tbStop.Active = (_parent.oXbmc.Status.IsNotPlaying())? true : false ;
				
				_parent.oPlaylist.HighlightNowPlayingEntry();

				SetPlayButtonStatus();
				
				return true;
			}
			else
				return false;
		}
		
		public bool Update()
		{
			return this.Update(true);
		}
		
		private void SetPbVolumePosition()
		{
			_parent._hsVolume.Value = Convert.ToDouble(_parent.oXbmc.Status.GetVolume());
		}
		
		private void SetPbProgressPosition()
		{
			_parent._hsProgress.Value = Convert.ToDouble(_parent.oXbmc.Status.GetProgress());
		}
		
		private void SetPlayButtonStatus()
		{
			if (_parent.oXbmc.Status.IsPlaying())
			{
				_parent._tbPlay.Active 		= true;
				_parent._tbPlay.TooltipText = "Pause";
				_parent._tbPlay.Label 		= "||";
			}
			else
			{
				_parent._tbPlay.Active 		= false;
				_parent._tbPlay.TooltipText = "Play";
				_parent._tbPlay.Label 		= ">";
			}
		}
	}
}
