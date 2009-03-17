
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
		
		public void AddDirectoryContentToPlaylist(string directory, bool play)
        {
			if (play) _parent.oXbmc.Playlist.Clear();
			bool added = _parent.oXbmc.Playlist.AddDirectoryContent(directory, _parent.oShareBrowser.GetCurrentShareType(), true);
            
			if (added)
			{
				if (play) _parent.oXbmc.Playlist.PlaySong(0);
				_parent.oPlaylist.Populate();
			}
			else
			    _parent.oHelper.Messagebox("Could not add directory content to the playlist");
        }
		
		public void AddFileToPlaylist(string filePath, bool play)
        {
			if (play) _parent.oXbmc.Playlist.Clear();
            bool added = _parent.oXbmc.Playlist.AddFilesToPlaylist(filePath);
			
			if (added)
			{
            	if (play) _parent.oXbmc.Playlist.PlaySong(0);
				_parent.oPlaylist.Populate();
			}
			else
				_parent.oHelper.Messagebox("Could not add the file to the playlist");
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
