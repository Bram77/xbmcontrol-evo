//  
//  Copyright (C) 2009 Bram van Oploo
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

using System;
using System.IO;
using Gdk;

namespace xbmcontrolevo
{
	public struct sMenu
	{
		public Pixbuf clear, collapse, configure, info, minus, mute, next, pause, play, plus, previous, refresh, stop, volume_down, volume_up;
		public Pixbuf artist, cd, connect, disconnect, file, file_music, file_picture, file_video, folder_closed, folder_open, icon, pixel;
		
		public sMenu (string path)
		{
			clear 		= new Pixbuf(path + "clear_16.png");
			collapse 	= new Pixbuf(path + "collapse_16.png");
			configure	= new Pixbuf(path + "configure_16.png");
			info		= new Pixbuf(path + "info_16.png");
			minus 		= new Pixbuf(path + "remove_16.png");
			mute 		= new Pixbuf(path + "volume_mute_16.png");
			next		= new Pixbuf(path + "next_16.png");
			pause		= new Pixbuf(path + "pause_16.png");
			play		= new Pixbuf(path + "play_16.png");
			plus		= new Pixbuf(path + "add_16.png");
			previous	= new Pixbuf(path + "previous_16.png");
			refresh		= new Pixbuf(path + "refresh_16.png");
			stop		= new Pixbuf(path + "stop_16.png");
			volume_down = new Pixbuf(path + "volume_down_16.png");
			volume_up	= new Pixbuf(path + "volume_up_16.png");
			
			artist			= new Pixbuf(path + "mic_16.png");
			cd				= new Pixbuf(path + "cd_16.png");
			connect			= new Pixbuf(path + "connect_16.png");
			disconnect		= new Pixbuf(path + "disconnect_16.png");
			file			= new Pixbuf(path + "file_files.png");
			file_music		= new Pixbuf(path + "file_music.png");
			file_picture	= new Pixbuf(path + "file_pictures.png");
			file_video		= new Pixbuf(path + "file_video.png");
			folder_closed	= new Pixbuf(path + "folder_closed.png");
			folder_open		= new Pixbuf(path + "folder_open.png");
			icon			= new Pixbuf(path + "icon.png");
			pixel			= new Pixbuf(path + "pixel.gif");
		}
	}
	
	public struct sButton
	{
		public Pixbuf next, pause, play, previous, stop;
		public Pixbuf dvd, lastfm, playlist, drive, drive_network, shoutcast;
		
		public sButton (string path)
		{
			next		= new Pixbuf(path + "next_32.png");
			pause		= new Pixbuf(path + "pause_32.png");
			play		= new Pixbuf(path + "play_32.png");
			previous	= new Pixbuf(path + "previous_32.png");
			stop		= new Pixbuf(path + "stop_32.png");
			
			dvd				= new Pixbuf(path + "dvd_32.png");
			lastfm			= new Pixbuf(path + "lastfm_32.png");
			playlist		= new Pixbuf(path + "playlist_32.png");
			drive			= new Pixbuf(path + "share_32.png");
			drive_network	= new Pixbuf(path + "share_network_32.png");
			shoutcast		= new Pixbuf(path + "shoutcast_32.png");
		}
	}
	
	public class Images
	{
		private XbmControlEvo _parent;
		public sButton button;
		public sMenu menu;
		public string imagePath;
		
		public Images(XbmControlEvo parent)
		{
			_parent 	= parent;
			
			imagePath 	= _parent.appUserDir + "/Interface/" + _parent.theme + "/";
			menu 		= new sMenu(imagePath);
			button		= new sButton(imagePath);
		}
	}
}
