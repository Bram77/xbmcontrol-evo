
using System;
using Gtk;

namespace xbmcontrolevo
{
	
	
	public class Playlist
	{
		private MainWindow _parent;
		private TreeStore tsPlaylist;
		private int itemCount;
		private TreeViewColumn tvcNumber;
		private TreeViewColumn tvcTitle;
		private TreeViewColumn tvcDuration;
		
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
                for (itemCount = 0; itemCount < aPlaylistEntries.Length; itemCount++)
                {
                    if (aPlaylistEntries[itemCount] != "")
                        tsPlaylist.AppendValues ((itemCount+1).ToString(), aPlaylistEntries[itemCount]);
                }
				
				_parent._tvPlaylist.Model = tsPlaylist;
				_parent._tvPlaylist.ShowAll();
            }
		}
		
		public void HighlightNowPlayingEntry()
        {
			string itemPlaying 			= _parent.oXbmc.NowPlaying.Get("songno");
            TreePath tpItemPlaying  	= new TreePath(itemPlaying);

            if (itemCount > 0 && Convert.ToInt32(itemPlaying) < itemCount)
			{
				TreeIter tiNowPLaying = new TreeIter();
				
             	if (tsPlaylist.GetIterFromString(out tiNowPLaying, itemPlaying))
					_parent._tvPlaylist.Selection.SelectIter(tiNowPLaying);
			}
        }
	}
}
