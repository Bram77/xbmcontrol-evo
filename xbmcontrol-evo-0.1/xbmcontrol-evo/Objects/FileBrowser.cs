
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	public class FileBrowser
	{
		private XbmControlEvo _parent;
		private TreeModel selectedModel;
		private TreeIter selectedIter;
		private TreeStore tsFiles;
		private Pixbuf mediaIcon;
		
		public FileBrowser(XbmControlEvo parent)
		{
			_parent 		= parent;
			selectedIter 	= new TreeIter();
			tsFiles			= new TreeStore (typeof (string), typeof (Pixbuf), typeof (string), typeof (string), typeof (string), typeof (string));

			_parent.tvFiles.AppendColumn ("", new CellRendererText (), "text", 0);
			TreeViewColumn tvcFileIcons 	= _parent.tvFiles.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 1);
			TreeViewColumn tvcFileNames 	= _parent.tvFiles.AppendColumn ("", new CellRendererText (), "text", 2);
			TreeViewColumn tvcFilePaths		= _parent.tvFiles.AppendColumn ("", new CellRendererText (), "text", 3);
			TreeViewColumn tvcFileTypes		= _parent.tvFiles.AppendColumn ("", new CellRendererText (), "text", 4);
			
			tvcFilePaths.Visible  	= false;
			tvcFileTypes.Visible  	= false;
			tvcFileIcons.Sizing		= TreeViewColumnSizing.Autosize;
			tvcFileNames.Sizing		= TreeViewColumnSizing.Autosize;
			
			_parent.tvFiles.ColumnsAutosize();
			_parent.tvFiles.Selection.Mode = SelectionMode.Multiple;
		}
		
		internal void Clear()
		{
			this.tsFiles.Clear();
		}
		
		public void ShowContextMenu ()
		{
			TreePath[] aSelectedPaths = _parent.tvFiles.Selection.GetSelectedRows();
			
			if (aSelectedPaths.Length > 0)
			{
				_parent.tvFiles.Model.GetIter(out selectedIter, aSelectedPaths[0]);
				_parent.oContextMenu.Show("file", _parent.tvFiles.Model.GetValue(selectedIter, 3).ToString());
			}
		}
		
		public TreeStore GetFiles (string startPath)
		{
			string[] aFiles		 	= _parent.oXbmc.Media.GetDirectoryContentNames(startPath, "[" + _parent.oShareBrowser.GetCurrentShareType() + "]");
		    string[] aFilesPath		= _parent.oXbmc.Media.GetDirectoryContentPaths(startPath, "[" + _parent.oShareBrowser.GetCurrentShareType() + "]");
			
			if (aFilesPath != null)
			{
				for (int y = 0; y < aFilesPath.Length; y++)
				{
					if (aFilesPath[y] != null && aFilesPath[y] != "")
					{
						string[] aFilesPathParts = aFilesPath[y].Split(':');
						string mediaType 		 = (aFilesPathParts[0] == "lastfm")? "lastfm" : "file" ;
						
						if (_parent.oShareBrowser.GetCurrentShareType() == "video")
							mediaIcon = _parent.oImages.menu.file_video;
						else if (_parent.oShareBrowser.GetCurrentShareType() == "music")
							mediaIcon = _parent.oImages.menu.file_music;
						else if (_parent.oShareBrowser.GetCurrentShareType() == "pictures")
							mediaIcon = _parent.oImages.menu.file_picture;
						else
							mediaIcon = _parent.oImages.menu.file;
						
						this.tsFiles.AppendValues((y+1).ToString()+ ".", mediaIcon, aFiles[y], aFilesPath[y], mediaType);
					}
				}
				
				if ((aFilesPath[0] != null && aFilesPath[0] != "") || aFilesPath.Length > 1)
					_parent.nbRight.CurrentPage = 1;
			}
			
			return tsFiles;
		}
		
		internal TreeStore GetSongs (string caller, string id)
		{
			string[] aSongs 	= null;
			string[] aSongsPath = null;
			
			if (caller == "artist")
			{
				aSongs 		= _parent.oXbmc.Database.GetTitlesByArtistId(id);	
				aSongsPath 	= _parent.oXbmc.Database.GetPathsByArtistId(id);
			}
			else if (caller == "album")
			{
				aSongs 		= _parent.oXbmc.Database.GetTitlesByAlbumId(id);
				aSongsPath 	= _parent.oXbmc.Database.GetPathsByAlbumId(id);
			}
			
			if (aSongsPath != null)
			{
				for (int y = 0; y < aSongsPath.Length; y++)
				{
					if (aSongsPath[y] != null && aSongsPath[y] != "")
						tsFiles.AppendValues((y+1).ToString()+ ".", _parent.oImages.menu.file_music, aSongs[y], aSongsPath[y], "file");
				}
				
				_parent.nbRight.CurrentPage = 1;
			}
			
			return tsFiles;
		}
		
		public void ShowFiles (string caller, string arg)
		{
			tsFiles.Clear();
			
			if (caller == "share" || caller == "folder")
				_parent.tvFiles.Model = GetFiles(arg);
			else if (caller == "artist" || caller == "album")
				_parent.tvFiles.Model = GetSongs(caller, arg);

			_parent.tvFiles.ShowAll();
		}
		
	}
}