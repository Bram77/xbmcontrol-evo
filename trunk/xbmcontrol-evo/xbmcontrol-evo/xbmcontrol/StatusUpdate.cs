
using System;
using GLib;
using Gtk;

namespace xbmcontrolevo
{
	
	public class StatusUpdate
	{
		private MainWindow _parent;
		private bool firstRun;
		
		public StatusUpdate(MainWindow parent)
		{
			_parent 	= parent;
			firstRun 	= true;
			
			Start();
		}
		
		public void Start()
		{
			GLib.Timeout.Add(1000, new GLib.TimeoutHandler(Update) );
		}
		
		public bool Update()
		{
			if (_parent.oXbmc.Status.IsConnected())
			{
				bool showDefaults = (!_parent.oXbmc.Status.IsNotPlaying())? true : false;
				_parent.oXbmc.Controls.SetResponseFormat();

				if (!_parent._hsVolume.HasGrab) SetPbVolumePosition();
				if (!_parent._hsProgress.HasGrab) SetPbProgressPosition();
				_parent._tbMute.Active = (_parent.oXbmc.Status.IsMuted())? true : false ;
				_parent._tbStop.Active = (_parent.oXbmc.Status.IsNotPlaying())? true : false ;
				
				SetPlayButtonStatus();

				if ((_parent.oNowPlaying.GetInfoNowShowing() != _parent.oXbmc.NowPlaying.Get("filename") || _parent.oXbmc.Status.NewMediaPlaying() || firstRun) 
				    	&& _parent._nbDataContainer.CurrentPage == 0)
				{
					//_parent._fixedNowPlaying.Visible = false;
					_parent._imgLoading.Visible = true;
					_parent.oNowPlaying.ShowData();
					_parent._imgLoading.Visible = false;
					//_parent._fixedNowPlaying.Visible = true;
					
					firstRun = false;
				}
				
				if (_parent.oXbmc.Status.NewMediaPlaying() && _parent._nbDataContainer.CurrentPage == 1)
					_parent.oPlaylist.Populate();
				
				_parent.oNowPlaying.ShowProgress(showDefaults);
				
				return true;
			}
			else
				return false;
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
				_parent._tbPlay.Image		= new Gtk.Image(Gtk.Stock.MediaPause, Gtk.IconSize.LargeToolbar);
			}
			else
			{
				_parent._tbPlay.Active 		= false;
				_parent._tbPlay.TooltipText = "Play";
				_parent._tbPlay.Image		= new Gtk.Image(Gtk.Stock.MediaPlay, Gtk.IconSize.LargeToolbar);
			}
		}
	}
}
