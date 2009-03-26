
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class Playlist
	{
		private XbmControlEvo _parent;
		private TreeStore tsPlaylist;
		private TreeIter tiNowPlaying;
		
		
		public Playlist(XbmControlEvo parent)
		{
			tsPlaylist	 = new TreeStore (typeof (Pixbuf), typeof (string), typeof (Pixbuf), typeof (string), typeof (string));
			tiNowPlaying = new TreeIter();
			_parent 	 = parent;

			_parent._tvPlaylist.AppendColumn ("", new CellRendererPixbuf(), "pixbuf", 0);
			_parent._tvPlaylist.AppendColumn ("", new Gtk.CellRendererText (), "text", 1);
			_parent._tvPlaylist.AppendColumn ("", new CellRendererPixbuf(), "pixbuf", 2);
			_parent._tvPlaylist.AppendColumn ("", new Gtk.CellRendererText (), "text", 3);
			
			TreeViewColumn tvcPath	 			= _parent._tvPlaylist.AppendColumn ("", new Gtk.CellRendererText(), "text", 4);
			tvcPath.Visible 					= false;
			_parent._tvPlaylist.Selection.Mode 	= SelectionMode.Multiple;
			
			if (_parent.IsConnected())
				SetCurrentPlaylistType("0");
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
		
		public void ShowContextMenu ()
		{
			_parent.oContextMenu.Show("playlist", null);
		}
		
		public void Populate()
		{
			tsPlaylist.Clear();
			string[] aPlaylistPaths = _parent.oXbmc.Playlist.Get(GetCurrentPlaylistType(), false);
			int curPlaylistType 	= Convert.ToInt32(this.GetCurrentPlaylistType());
			string playlistType 	= null;
			
			if (curPlaylistType == 0)
				playlistType = "music";
			else if (curPlaylistType == 1)
				playlistType = "video";
			
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
						
						tsPlaylist.AppendValues (new Pixbuf("Interface/" + _parent.theme + "/icons/pixel.gif"), (j+1).ToString() +".", new Pixbuf ("Interface/" + _parent.theme + "/icons/file_" +playlistType+ ".png"), playlistEntry, aPlaylistPaths[j]);
                    }
				}
				
				_parent._tvPlaylist.Model = tsPlaylist;
				_parent._tvPlaylist.ShowAll();
            }

			MarkNowPlayingEntry();
		}
		
		public void MarkNowPlayingEntry()
        {
			int itemCount 		= _parent.oXbmc.Playlist.GetLength();
			string itemPlaying  = _parent.oXbmc.NowPlaying.Get("songno", true);
			
            if (itemCount > 0 && Convert.ToInt32(itemPlaying) < itemCount)
			{
				Gtk.Image nowPlayingImage 	= new Gtk.Image();
				Pixbuf nowPlayingIcon 		= nowPlayingImage.RenderIcon(Stock.MediaPlay, IconSize.Menu, "");
				TreeIter tiPlaylistItem 	= new TreeIter();
				tsPlaylist.GetIterFirst(out tiPlaylistItem);
				
				while (tsPlaylist.IterNext(ref tiPlaylistItem))
					_parent._tvPlaylist.Model.SetValue(tiPlaylistItem, 0, new Pixbuf("Interface/" + _parent.theme + "/icons/pixel.gif"));
				
             	if (tsPlaylist.GetIter(out tiNowPlaying, new TreePath(itemPlaying)) && !_parent.oXbmc.Status.IsNotPlaying())
					_parent._tvPlaylist.Model.SetValue(tiNowPlaying, 0, nowPlayingIcon);
			}
        }
		
		internal void SelectNowPlayingEntry ()
		{
			if (!_parent.oXbmc.Status.IsNotPlaying())
			{
				_parent._tvPlaylist.Selection.SelectIter(tiNowPlaying);
				_parent._tvPlaylist.ScrollToCell(new TreePath(_parent.oXbmc.NowPlaying.Get("songno", false)), null, false, 0, 0);
			}
		}
		
		private int[] GetSelectedItem()
		{
			TreePath[] atpSelectedRows 	= _parent._tvPlaylist.Selection.GetSelectedRows();
			int[] aiSelectedRows 		= null;
			
			if (atpSelectedRows.Length > 0)
			{
				aiSelectedRows = new int[atpSelectedRows.Length];	
				for (int x=0; x<atpSelectedRows.Length; x++)
					aiSelectedRows[x] = Convert.ToInt32(atpSelectedRows[x].ToString());
			}
				
			return aiSelectedRows;
		}
			
		public void PlaySelectedItem()
		{
			int[] aSelectedItem = this.GetSelectedItem();
			if (aSelectedItem != null) _parent.oXbmc.Playlist.PlaySong(aSelectedItem[0]);
		}
		
		public void RemoveSelectedItems()
		{
			int[] aSelectedItem = this.GetSelectedItem();
			
			if (aSelectedItem != null)
			{
				for (int x=0; x<aSelectedItem.Length; x++)
					_parent.oXbmc.Playlist.Remove(aSelectedItem[x]);
				this.Populate();
			}
		}
		
		public void Clear()
		{
			_parent.oXbmc.Playlist.Clear();
			this.Populate();
		}
		
		public void Refresh()
		{
			this.Populate();
		}
		
		public void ShowSongInfoPopup()
		{
			//TreeModel selectedModel;
			//TreeIter selectedIter = new TreeIter();
			
			//if (_parent._tvPlaylist.Selection.GetSelected(out selectedModel, out selectedIter))
				//_parent.oMediaInfo.ShowSongInfoPopup(selectedModel.GetValue(selectedIter, 3).ToString());
		}
	}
}
