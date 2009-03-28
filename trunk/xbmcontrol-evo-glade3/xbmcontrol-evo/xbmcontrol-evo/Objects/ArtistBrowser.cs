
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class ArtistBrowser
	{
		private XbmControlEvo _parent;
		private TreeIter selectedIter;
		private TreeModel selectedModel;
		private TreeStore tsArtists;
		private TreeViewColumn tvcArtistIcons;
		private TreeViewColumn tvcArtistNames;
		private TreeViewColumn tvcArtistIds;
		private TreeViewColumn tvcArtistTypes;
		private int albumCount;
		
		public ArtistBrowser(XbmControlEvo parent)
		{
			selectedIter 	= new TreeIter();
			_parent 		= parent;
			
			tsArtists	 	= new TreeStore (typeof (Pixbuf), typeof (string), typeof (string), typeof (string));
			
			tvcArtistIcons 	= _parent._tvArtists.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 0);
			tvcArtistNames 	= _parent._tvArtists.AppendColumn ("", new CellRendererText (), "text", 1);
			tvcArtistIds 	= _parent._tvArtists.AppendColumn ("", new CellRendererText (), "text", 2);
			tvcArtistTypes 	= _parent._tvArtists.AppendColumn ("", new CellRendererText (), "text", 3);
			
			tvcArtistIds.Visible 	= false;
			tvcArtistTypes.Visible 	= false;
			tvcArtistIcons.Sizing 	= TreeViewColumnSizing.Autosize;
			tvcArtistNames.Sizing	= TreeViewColumnSizing.Autosize;
			_parent._tvArtists.ColumnsAutosize();
		}
		
		internal void Populate(string searchString)
		{
			if (tsArtists.IterNChildren() == 0 || searchString != null)
			{
				string[] aArtists 	= _parent.oXbmc.Database.GetArtists(searchString);
	            string[] aArtistIds = _parent.oXbmc.Database.GetArtistIds(searchString);
				
				if (aArtists != null)
				{
					tsArtists.Clear();
				
					for (int x = 0; x < aArtists.Length; x++)
					{
						if (aArtists[x] != "" && aArtistIds[x] != "")
							tsArtists.AppendValues (new Pixbuf (_parent.appDir + "/Interface/" + _parent.theme + "/icons/mic_16.png"), aArtists[x], aArtistIds[x], "artist");
					}
					
					_parent._tvArtists.Model = tsArtists;
					_parent._tvArtists.ShowAll();
				}
			}
		}
		
		internal void Populate ()
		{
			this.Populate(null);
		}
		
		internal void ExpandeSelectedItem ()
		{
			if (_parent._tvArtists.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				string selectedType	= selectedModel.GetValue(selectedIter, 3).ToString();
				string selectedId	= selectedModel.GetValue(selectedIter, 2).ToString();	
				
				if (selectedType == "artist")
				{
					if (!_parent._tvArtists.GetRowExpanded(selectedModel.GetPath(selectedIter)))
					{
						if (!selectedModel.IterHasChild(selectedIter))
						{
							_parent._tvArtists.Model = this.GetArtistAlbums(tsArtists.GetValue(selectedIter, 2).ToString());
							_parent._tvArtists.ShowAll();
							
							if (albumCount == 0)
								_parent.oFileBrowser.ShowFiles(selectedType, selectedId);
							else
								_parent.oFileBrowser.Clear();
							
							albumCount = 0;
						}

						_parent._tvArtists.ExpandRow(selectedModel.GetPath(selectedIter), false);
					}
					else
						_parent._tvArtists.CollapseRow(selectedModel.GetPath(selectedIter));
				}
				else if (selectedType == "album")
					_parent.oFileBrowser.ShowFiles(selectedType, selectedId);
			}
		}
			
		private TreeStore GetArtistAlbums (string artistId)
		{
			string[] aAlbums 	= _parent.oXbmc.Database.GetAlbumsByArtistId(artistId);
            string[] aAlbumIds 	= _parent.oXbmc.Database.GetAlbumIdsByArtistId(artistId);
			
			if (aAlbums != null)
            {
                for (int x = 0; x < aAlbums.Length; x++)
				{
					if (aAlbums[x] != "")
					{
                    	tsArtists.AppendValues (selectedIter, new Pixbuf (_parent.appDir + "/Interface/" + _parent.theme + "/icons/cd_16.png"), aAlbums[x], aAlbumIds[x], "album");
						albumCount++;
					}
				}
            }
			
			return tsArtists;
		}
		
		public void ShowContextMenu () 
		{
			string selectedType = null;
			string selectedId	= null;
			
			if (_parent._tvArtists.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				selectedType = selectedModel.GetValue(selectedIter, 3).ToString();
				selectedId	 = selectedModel.GetValue(selectedIter, 2).ToString();
			}
			
			if (selectedType == "artist" || selectedType == "album")
					_parent.oContextMenu.Show(selectedType, selectedId);
			else
				_parent.oContextMenu.Show("default", null);
		}
	}
}
