
using System;

namespace xbmcontrolevo
{
	
	
	public class Controls
	{
		private MainWindow _parent;
		
		public Controls(MainWindow parent)
		{
			_parent = parent;
		}
		
		public void AddDirectoryContentToPlaylist(string directory, string shareType, bool play, bool recursive)
        {
            if (play) _parent.Xbmc.Playlist.Clear();
            _parent.Xbmc.Playlist.AddDirectoryContent(directory, shareType, recursive);
            if (play) _parent.Xbmc.Playlist.PlaySong(0);
        }
		
		public void AddFileToPlaylist(string filePath, bool play)
        {
			if (play) _parent.Xbmc.Playlist.Clear();
            _parent.Xbmc.Playlist.AddFilesToPlaylist(filePath);
            if (play) _parent.Xbmc.Playlist.PlaySong(0);
        }
	}
}
