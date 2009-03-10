
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class Playlist
	{
		private MainWindow _parent;
		private TreeStore tsPlaylist;
		
		public Playlist(MainWindow parent)
		{
			tsPlaylist	= new TreeStore (typeof (Pixbuf), typeof (string), typeof (string), typeof (string));
			_parent 	= parent;

			TreeViewColumn tvcPlaying 	= _parent._tvPlaylist.AppendColumn ("", new CellRendererPixbuf(), "pixbuf", 0);
			TreeViewColumn tvcNumber 	= _parent._tvPlaylist.AppendColumn ("", new Gtk.CellRendererText (), "text", 1);
			TreeViewColumn tvcTitle 	= _parent._tvPlaylist.AppendColumn ("", new Gtk.CellRendererText (), "text", 2);
			TreeViewColumn tvcPath	 	= _parent._tvPlaylist.AppendColumn ("", new Gtk.CellRendererText(), "text", 3);
			
			tvcPath.Visible 					= false;
			tvcTitle.Sizing 					= TreeViewColumnSizing.Autosize;
			tvcNumber.Sizing 					= TreeViewColumnSizing.Autosize;
			tvcPlaying.Sizing 					= TreeViewColumnSizing.Autosize;
			_parent._tvPlaylist.HeadersVisible 	= false;
			_parent._tvPlaylist.EnableGridLines = TreeViewGridLines.Vertical;
			
			SetCurrentPlaylistType("0");
			
			Populate();
		}
		
		public void SetCurrentPlaylistType(string selectedType)
		{
			_parent.oXbmc.Playlist.Set(selectedType);
			Populate();
		}
		
		public string GetCurrentPlaylistType()
		{
			return _parent.oXbmc.Playlist.GetCurrentPlaylistType();
		}
		
		public void Populate()
		{
			tsPlaylist.Clear();
			string[] aPlaylistPaths = _parent.oXbmc.Playlist.Get(GetCurrentPlaylistType(), false);
			
			if (aPlaylistPaths != null)
            {	
                for (int j = 0; j < aPlaylistPaths.Length; j++)
                {
                    int i = aPlaylistPaths[j].LastIndexOf(".");
                    if (i > 1)
                    {
                        string extension = aPlaylistPaths[j].Substring(i, aPlaylistPaths[j].Length - i);
                        aPlaylistPaths[j] = aPlaylistPaths[j].Replace("\\", "/");
                        string[] aPlaylistEntry = aPlaylistPaths[j].Split('/');
                        string playlistEntry = aPlaylistEntry[aPlaylistEntry.Length - 1].Replace(extension, "");
						
						tsPlaylist.AppendValues (null, (j+1).ToString(), playlistEntry, aPlaylistPaths[j]);
                    }
				}
				
				_parent._tvPlaylist.Model = tsPlaylist;
				_parent._tvPlaylist.ShowAll();
            }

			MarkNowPlayingEntry();
		}
		
		public void MarkNowPlayingEntry()
        {
			int itemCount		= _parent.oXbmc.Playlist.GetLength();
			string itemPlaying 	= _parent.oXbmc.NowPlaying.Get("songno", true);
			
            if (itemCount > 0 && Convert.ToInt32(itemPlaying) < itemCount)
			{
				Gtk.Image nowPlayingImage 	= new Gtk.Image();
				Pixbuf nowPlayingIcon 		= nowPlayingImage.RenderIcon(Stock.MediaPlay, IconSize.Menu, "");
				Pixbuf emptyIcon			= new Pixbuf("images/pixel.gif");
				
				TreeIter tiNowPLaying 		= new TreeIter();
				TreeIter tiPlaylistItem 	= new TreeIter();
				
				tsPlaylist.GetIterFirst(out tiPlaylistItem);
				
				while (tsPlaylist.IterNext(ref tiPlaylistItem))
					_parent._tvPlaylist.Model.SetValue(tiPlaylistItem, 0, emptyIcon);
				
             	if (tsPlaylist.GetIter(out tiNowPLaying, new TreePath(itemPlaying)) && !_parent.oXbmc.Status.IsNotPlaying())
					_parent._tvPlaylist.Model.SetValue(tiNowPLaying, 0, nowPlayingIcon);
			}
        }
		
		private int GetSelectedItem()
		{
			TreeModel selectedModel;
			TreeIter selectedIter = new TreeIter();
			
			if (_parent._tvPlaylist.Selection.GetSelected(out selectedModel, out selectedIter))
				return Convert.ToInt32(selectedModel.GetPath(selectedIter).ToString());
			else
				return -1;
		}
			
			
		public void PlaySelectedItem()
		{
			int selectedItem = GetSelectedItem();
			if (selectedItem != -1) _parent.oXbmc.Playlist.PlaySong(selectedItem);
		}
		
		public void RemoveSelectedItem()
		{
			int selectedItem = GetSelectedItem();
			
			if (selectedItem != -1)
			{
				_parent.oXbmc.Playlist.Remove(selectedItem);
				Populate();
			}
		}
		
		public void Clear()
		{
			_parent.oXbmc.Playlist.Clear();
			Populate();
		}
		
		public void ShowSongInfoPopup()
		{
			TreeModel selectedModel;
			TreeIter selectedIter = new TreeIter();
			
			if (_parent._tvPlaylist.Selection.GetSelected(out selectedModel, out selectedIter))
				_parent.oMediaInfo.ShowSongInfoPopup(selectedModel.GetValue(selectedIter, 3).ToString());
		}
	}
}
