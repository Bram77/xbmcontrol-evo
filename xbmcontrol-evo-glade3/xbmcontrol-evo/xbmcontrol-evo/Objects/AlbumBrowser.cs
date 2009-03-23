
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class AlbumBrowser
	{
		private XbmControlEvo _parent;
		private TreeIter selectedIter;
		private TreeModel selectedModel;
		private TreeStore tsAlbums;
		private TreeViewColumn tvcAlbumIcons;
		private TreeViewColumn tvcAlbumNames;
		private TreeViewColumn tvcAlbumIds;
		
		public AlbumBrowser(XbmControlEvo parent)
		{
			selectedIter 	= new TreeIter();
			_parent 		= parent;
			
			tsAlbums	 	= new TreeStore (typeof (Pixbuf), typeof (string), typeof (string));
			
			tvcAlbumIcons 	= _parent._tvAlbums.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 0);
			tvcAlbumNames 	= _parent._tvAlbums.AppendColumn ("", new CellRendererText (), "text", 1);
			tvcAlbumIds 	= _parent._tvAlbums.AppendColumn ("", new CellRendererText (), "text", 2);
			
			tvcAlbumIds.Visible 	= false;
			tvcAlbumIcons.Sizing 	= TreeViewColumnSizing.Autosize;
			tvcAlbumNames.Sizing	= TreeViewColumnSizing.Autosize;
			_parent._tvAlbums.ColumnsAutosize();
		}
		
		internal void Populate(string searchString)
		{
			if (tsAlbums.IterNChildren() == 0 || searchString != null)
			{
				string[] aAlbums 	= _parent.oXbmc.Database.GetAlbums(searchString);
	            string[] aAlbumIds = _parent.oXbmc.Database.GetAlbumIds(searchString);
				
				if (aAlbums != null)
				{
					tsAlbums.Clear();
				
					for (int x = 0; x < aAlbums.Length; x++)
					{
						if (aAlbums[x] != "" && aAlbumIds[x] != "")
							tsAlbums.AppendValues (new Pixbuf ("Interface/" + _parent.theme + "/icons/cd_16.png"), aAlbums[x], aAlbumIds[x]);
					}
					
					_parent._tvAlbums.Model = tsAlbums;
					_parent._tvAlbums.ShowAll();
				}
			}
		}
		
		internal void Populate ()
		{
			this.Populate(null);
		}
		
		internal void GetAlbumSongs()
		{
			if (_parent._tvAlbums.Selection.GetSelected(out selectedModel, out selectedIter))
				_parent.oFileBrowser.ShowFiles("album", selectedModel.GetValue(selectedIter, 2).ToString());
		}
		
		public void ShowContextMenu () 
		{
			if (_parent._tvAlbums.Selection.GetSelected(out selectedModel, out selectedIter))
				_parent.oContextMenu.Show("album", selectedModel.GetValue(selectedIter, 2).ToString());
			else
				_parent.oContextMenu.Show("default", null);
		}
	}
}
