
using System;
using Gtk;

namespace xbmcontrolevo
{
	
	
	public class Playlist
	{
		private MainWindow _parent;
		private TreeStore tsPlaylist;
		private TreeViewColumn tvcNumber = new TreeViewColumn();
		private TreeViewColumn tvcTitle = new TreeViewColumn();
		
		public Playlist(MainWindow parent)
		{
			tsPlaylist	= new TreeStore (typeof (string), typeof (string));
			_parent 	= parent;
			
			Populate();
		}
		
		public void Populate()
		{
			foreach (TreeViewColumn col in _parent._tvPlaylist.Columns) 
	        	_parent._tvPlaylist.RemoveColumn(col);
			
			tsPlaylist.Clear();
			
			tvcNumber 	= _parent._tvPlaylist.AppendColumn ("nr", new Gtk.CellRendererText (), "text", 0);
			tvcTitle	= _parent._tvPlaylist.AppendColumn ("song", new Gtk.CellRendererText (), "text", 1);
			
			_parent._tvPlaylist.ColumnsAutosize();
			
			GetPlaylistEntries();
		}
		
		private void GetPlaylistEntries()
		{
			string[] aPlaylistEntries = _parent.oXbmc.Playlist.Get(true, true);
			
			if (aPlaylistEntries != null)
            {
                for (int x = 0; x < aPlaylistEntries.Length; x++)
                {
                    if (aPlaylistEntries[x] != "")
                        tsPlaylist.AppendValues ((x+1).ToString(), aPlaylistEntries[x]);
                }
				
				_parent._tvPlaylist.Model = tsPlaylist;
				_parent._tvPlaylist.ShowAll();
            }
		}
		
		public void HighlightNowPlayingEntry()
        {
			int itemCount				= _parent.oXbmc.Playlist.GetLength();
			string itemPlaying 			= _parent.oXbmc.NowPlaying.Get("songno");
            //TreePath tpItemPlaying  	= new TreePath(itemPlaying);
			
            if (itemCount > 0 && Convert.ToInt32(itemPlaying) < itemCount)
			{
				TreeIter tiNowPLaying = new TreeIter();
				
             	if (tsPlaylist.GetIterFromString(out tiNowPLaying, itemPlaying))
					_parent._tvPlaylist.Selection.SelectIter(tiNowPLaying);
			}
        }
		
		private int GetSelectedItem()
		{
			TreeModel selectedModel;
			TreeIter selectedIter = new TreeIter();
			
			if (_parent._tvShareBrowser.Selection.GetSelected(out selectedModel, out selectedIter))
				return Convert.ToInt32(selectedModel.GetPath(selectedIter));
			else
				return -1;
		}
			
			
		public void PlaySelectedItem()
		{
			int selectedItem = GetSelectedItem();
			if (selectedItem != -1) _parent.oXbmc.Playlist.PlaySong(GetSelectedItem());
		}
		
		public void RemoveSelectedItem()
		{
			int selectedItem = GetSelectedItem();
			if (selectedItem != -1)
			{
				_parent.oXbmc.Playlist.Remove(GetSelectedItem());
				Populate();
			}
		}
	}
}
