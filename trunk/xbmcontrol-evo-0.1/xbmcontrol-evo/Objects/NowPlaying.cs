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
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class NowPlaying
	{
		private XbmControlEvo _parent;
		private string artist, genre, title, duration, album, year;
		private Pixbuf coverArt;
		
		public NowPlaying(XbmControlEvo parent)
		{
			_parent = parent;
		}
		
		public void LoadData ()
		{
			coverArt= new Pixbuf(_parent.oXbmc.NowPlaying.GetCoverArt());
			artist	= _parent.oXbmc.NowPlaying.Get("artist");
			genre	= _parent.oXbmc.NowPlaying.Get("genre");
			title	= _parent.oXbmc.NowPlaying.Get("title");
			duration= _parent.oXbmc.NowPlaying.Get("duration");
			album	= _parent.oXbmc.NowPlaying.Get("album");
			year	= _parent.oXbmc.NowPlaying.Get("year");
		}
		
		public string Get(string field)
		{
			switch(field)
			{
				case "artist":
					return artist;
					break;
				case "genre":
					return genre;
					break;
				case "title":
					return title;
					break;
				case "duration":
					return duration;
					break;
				case "album":
					return album;
					break;
				case "year":
					return year;
					break;
				default:
					return "";
					break;
			}
		}
		
		public Pixbuf GetCoverArt ()
		{
			return coverArt;
		}
	}
}
