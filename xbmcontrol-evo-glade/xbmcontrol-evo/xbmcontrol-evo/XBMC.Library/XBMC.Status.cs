// ------------------------------------------------------------------------
//    XBMControl - A compact remote controller for XBMC (.NET 3.5)
//    Copyright (C) 2008  Bram van Oploo (bramvano@gmail.com)
//                        Mike Thiels (Mike.Thiels@gmail.com)
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLib;
//using System.Windows.Forms;

namespace XBMC
{
    public class XBMC_Status
    {
        private XBMC_Communicator parent;
        private string mediaNowPlaying = null;
		private bool isConnected = false;
       	//private Timer heartBeatTimer = null;
        //private int connectedInterval = 5000;
        //private int disconnectedInterval = 10000;

        public XBMC_Status(XBMC_Communicator p)
        {
            parent = p;
            //heartBeatTimer = new Timer();
            //heartBeatTimer.Interval = connectedInterval;
            //heartBeatTimer.Tick += new EventHandler(HeartBeat_Tick);
        }
		
		public void SetNowPlayingMedia()
		{
			if (mediaNowPlaying != parent.NowPlaying.Get("filename", true) || mediaNowPlaying == null)
                mediaNowPlaying = parent.NowPlaying.Get("filename");
		}
	
		public bool NewMediaPlaying()
		{	
			string fnMedia = parent.NowPlaying.Get("filename", true);
			
			if (mediaNowPlaying != fnMedia)
	 		{
				mediaNowPlaying = fnMedia;
				return true;
	 		}
	 		else
				return false;
		}
		
		public void StartHeartBeat()
		{
			HeartBeat_Tick();
			GLib.Timeout.Add(5000, new GLib.TimeoutHandler(HeartBeat_Tick) );
		}

        private bool HeartBeat_Tick()
        {
            isConnected = parent.Controls.SetResponseFormat();
            return isConnected;
        }

        public bool IsConnected()
        {
            return isConnected;
        }

        public void EnableHeartBeat()
        {
            HeartBeat_Tick();
            //heartBeatTimer.Enabled = true;
        }

        public void DisableHeartBeat()
        {
            //heartBeatTimer.Enabled = false;
        }

        public bool WebServerEnabled()
        {
            string[] webserverEnabled = parent.Request("WebServerStatus");

            if (webserverEnabled == null)
                return false;
            else
                return (webserverEnabled[0] == "On") ? true : false;
        }

        public bool IsPlaying()
        {
			 return (parent.NowPlaying.Get("playstatus", true) == "Playing") ? true : false;
        }
		
		public bool IsPlayingLastFm()
		{
			SetNowPlayingMedia();
			if (mediaNowPlaying == null || IsNotPlaying() || mediaNowPlaying.Length < 6)
           		return false;
            else
            	return (mediaNowPlaying.Substring(0, 6) == "lastfm") ? true : false;
		}

        public bool IsNotPlaying()
        {
			SetNowPlayingMedia();
            return (mediaNowPlaying == "[Nothing Playing]" || mediaNowPlaying == null) ? true : false;
        }

        public bool IsPaused()
        {
            return (parent.NowPlaying.Get("playstatus", true) == "Paused") ? true : false;
        }

        public bool IsMuted()
        {
            return (GetVolume() == 0) ? true : false;
        }

        public int GetVolume()
        {
			string[] aVolume = parent.Request("GetVolume");
			return (aVolume == null || aVolume[0] == "Error")? 0 : Convert.ToInt32(aVolume[0]) ;
        }

        public int GetProgress()
        {
			string[] aProgress = parent.Request("GetPercentage");
            return (aProgress == null || aProgress[0] == "Error" || aProgress[0] == "0" || Convert.ToInt32(aProgress[0]) > 99)? 1 : Convert.ToInt32(aProgress[0]);
        }

        public bool LastFmEnabled()
        {
            string[] aLastFmUsername = parent.Request("GetGuiSetting(3;lastfm.username)");
            string[] aLastFmPassword = parent.Request("GetGuiSetting(3;lastfm.password)");

            if (aLastFmUsername == null || aLastFmPassword == null)
                return false;
            else
                return (aLastFmUsername[0] == "" || aLastFmPassword[0] == "") ? false : true;
        }

        public bool RepeatEnabled()
        {
            string[] aRepeatEnabled = parent.Request("GetGuiSetting(1;musicfiles.repeat)");
            if (aRepeatEnabled == null)
                return false;
            else
                return (aRepeatEnabled[0] == "False") ? false : true;
        }
    }
}
