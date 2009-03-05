
using System;
using Gtk;

namespace xbmcontrolevo
{
	
	
	public class Controls
	{
		private MainWindow _parent;
		
		public Controls(MainWindow parent)
		{
			_parent = parent;
		}
		
		public void AddDirectoryContentToPlaylist(string directory, bool play, bool recursive)
        {
			if (play) _parent.oXbmc.Playlist.Clear();
            _parent.oXbmc.Playlist.AddDirectoryContent(directory, _parent._cbShareType.ActiveText, recursive);
            if (play) _parent.oXbmc.Playlist.PlaySong(0);
			_parent.oPlaylist.Populate();
        }
		
		public void AddFileToPlaylist(string filePath, bool play)
        {
			if (play) _parent.oXbmc.Playlist.Clear();
            _parent.oXbmc.Playlist.AddFilesToPlaylist(filePath);
            if (play) _parent.oXbmc.Playlist.PlaySong(0);
			_parent.oPlaylist.Populate();
        }
		
		public void GetFileInfo()
		{
			TreeModel selectedModel;
			TreeIter selectedIter	= new TreeIter();
			
			if (_parent._tvShareBrowser.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				string filepath = selectedModel.GetValue(selectedIter, 2).ToString();
				_parent.Messagebox(_parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "title"));
			}
		}
	}
}
