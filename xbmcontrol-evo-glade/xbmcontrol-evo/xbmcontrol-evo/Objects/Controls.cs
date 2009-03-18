
using System;
using Gtk;

namespace xbmcontrolevo
{
	
	
	public class Controls
	{
		private XbmControlEvo _parent;
		
		public Controls(XbmControlEvo parent)
		{
			_parent = parent;
		}
		
		internal void AddToPlaylist (string caller, string identifier, bool play)
		{
			if (caller == "album" || caller == "artist")
				this.AddFilesToPlaylistById(caller, identifier, play);
			else
				this.AddFilesToPlaylist(caller, identifier, play);
		}
		
		private void AddFilesToPlaylist (string caller, string path, bool play)
        {
			bool added = false;
			
			if (play)
			{	
				_parent.oXbmc.Playlist.Clear();
				_parent.oPlaylist.Clear();
			}
			
			if (caller == "folder")
				added = _parent.oXbmc.Playlist.AddDirectoryContent(path, _parent.oShareBrowser.GetCurrentShareType(), true);
			else if (caller == "file")
				added = _parent.oXbmc.Playlist.AddFilesToPlaylist(path);
            
			if (added)
			{
				if (play) _parent.oXbmc.Playlist.PlaySong(0);
				_parent.oPlaylist.Populate();
			}
			else
			    _parent.oHelper.Messagebox("Could not add file(s) to the playlist");
        }
		
		private void AddFilesToPlaylistById(string caller, string id, bool play)
		{
			string[] songPaths = null;
			
			if (caller == "artist")
            	songPaths = _parent.oXbmc.Database.GetPathsByArtistId(id);
			else if (caller == "album")
				songPaths = _parent.oXbmc.Database.GetPathsByAlbumId(id);

            if (songPaths != null)
            {
				if (play)
				{	
					_parent.oXbmc.Playlist.Clear();
					_parent.oPlaylist.Clear();
				}
				
                for (int x = 0; x < songPaths.Length; x++)
				{
					if (songPaths[x] != "")
                    	_parent.oXbmc.Playlist.AddFilesToPlaylist(songPaths[x]);
				}
				
                if (play) _parent.oXbmc.Playlist.PlaySong(0);
				_parent.oPlaylist.Refresh();
            }
		}
		
		public void GetFileInfo()
		{
			TreeModel selectedModel;
			TreeIter selectedIter	= new TreeIter();
			
			if (_parent._tvShares.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				string filepath = selectedModel.GetValue(selectedIter, 2).ToString();
				
				string sDuration = _parent.oHelper.SecondsToHumanTime(_parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "duration"));
				
				string songInfo = "artist: " + _parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "artist");
				songInfo += "\ntitle: " + _parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "title");
				songInfo += "\nalbum: " + _parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "album");
				songInfo += "\ngenre: " + _parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "genre");
				songInfo += "\nduration: " + sDuration;
				
				Window wSongInfo = new Window("Song Info");
				wSongInfo.SetPosition(WindowPosition.CenterAlways);
				wSongInfo.SetIconFromFile("images/icon.png");
				
				Pango.FontDescription fd = Pango.FontDescription.FromString("Verdana Bold 9");
				
				Label lArtist = new Label(songInfo);
				lArtist.ModifyFont(fd);
				lArtist.Xpad = 20;
				lArtist.Ypad = 20;
				
				wSongInfo.Add(lArtist);
				
				//Gtk.Image iCoverart = new Gtk.Image(new Gdk.Pixbuf(_parent.oXbmc.Media.GetFileThumbnailLocation(filepath)));
				//wSongInfo.Add(iCoverart);
				
				wSongInfo.ShowAll();
				//_parent.Messagebox(songInfo);
			}
		}
	}
}
