
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

			this._parent.tvFiles.AppendColumn ("", new CellRendererText (), "text", 0);
			TreeViewColumn tvcFileIcons 	= this._parent.tvFiles.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 1);
			TreeViewColumn tvcFileNames 	= this._parent.tvFiles.AppendColumn ("", new CellRendererText (), "text", 2);
			TreeViewColumn tvcFilePaths		= this._parent.tvFiles.AppendColumn ("", new CellRendererText (), "text", 3);
			TreeViewColumn tvcFileTypes		= this._parent.tvFiles.AppendColumn ("", new CellRendererText (), "text", 4);
			
			tvcFilePaths.Visible  	= false;
			tvcFileTypes.Visible  	= false;
			tvcFileIcons.Sizing		= TreeViewColumnSizing.Autosize;
			tvcFileNames.Sizing		= TreeViewColumnSizing.Autosize;
			
			this._parent.tvFiles.ColumnsAutosize();
			this._parent.tvFiles.Selection.Mode = SelectionMode.Multiple;
		}
		
		internal void Clear()
		{
			this.tsFiles.Clear();
		}
		
		public void ShowContextMenu ()
		{
			TreePath[] aSelectedPaths = this._parent.tvFiles.Selection.GetSelectedRows();
			
			if (aSelectedPaths.Length > 0)
			{
				this._parent.tvFiles.Model.GetIter(out selectedIter, aSelectedPaths[0]);
				this._parent.oContextMenu.Show("file", this._parent.tvFiles.Model.GetValue(selectedIter, 3).ToString());
			}
		}
		
		public TreeStore GetFiles (string startPath)
		{
		    string[] aFilesPath		= this._parent.oXbmc.Media.GetDirectoryContentPaths(startPath, "[" + this._parent.oShareBrowser.GetCurrentShareType() + "]");
			string[] aFiles		 	= this._parent.oXbmc.Media.GetDirectoryNames(aFilesPath);
			
			if (aFilesPath != null)
			{
				for (int y = 0; y < aFilesPath.Length; y++)
				{
					if (aFilesPath[y] != null && aFilesPath[y] != "")
					{
						string[] aFilesPathParts = aFilesPath[y].Split(':');
						string mediaType 		 = (aFilesPathParts[0] == "lastfm")? "lastfm" : "file" ;
						
						if (this._parent.oShareBrowser.GetCurrentShareType() == "video")
							mediaIcon = this._parent.oImages.menu.file_video;
						else if (this._parent.oShareBrowser.GetCurrentShareType() == "music")
							mediaIcon = this._parent.oImages.menu.file_music;
						else if (this._parent.oShareBrowser.GetCurrentShareType() == "pictures")
							mediaIcon = this._parent.oImages.menu.file_picture;
						else
							mediaIcon = this._parent.oImages.menu.file;
						
						this.tsFiles.AppendValues((y+1).ToString()+ ".", mediaIcon, aFiles[y], aFilesPath[y], mediaType);
					}
				}
				
				if ((aFilesPath[0] != null && aFilesPath[0] != "") || aFilesPath.Length > 1)
					this._parent.nbRight.CurrentPage = 1;
			}
			
			return tsFiles;
		}
		
		internal TreeStore GetSongs (string caller, string id)
		{
			string[] aSongs 	= null;
			string[] aSongsPath = null;
			
			if (caller == "artist")
			{
				aSongs 		= this._parent.oXbmc.Database.GetTitlesByArtistId(id);	
				aSongsPath 	= this._parent.oXbmc.Database.GetPathsByArtistId(id);
			}
			else if (caller == "album")
			{
				aSongs 		= this._parent.oXbmc.Database.GetTitlesByAlbumId(id);
				aSongsPath 	= this._parent.oXbmc.Database.GetPathsByAlbumId(id);
			}
			
			if (aSongsPath != null)
			{
				for (int y = 0; y < aSongsPath.Length; y++)
				{
					if (aSongsPath[y] != null && aSongsPath[y] != "")
						tsFiles.AppendValues((y+1).ToString()+ ".", this._parent.oImages.menu.file_music, aSongs[y], aSongsPath[y], "file");
				}
				
				this._parent.nbRight.CurrentPage = 1;
			}
			
			return this.tsFiles;
		}
		
		public void ShowFiles (string caller, string arg)
		{
			this.tsFiles.Clear();
			
			if (caller == "share" || caller == "folder")
				this._parent.tvFiles.Model = GetFiles(arg);
			else if (caller == "artist" || caller == "album")
				this._parent.tvFiles.Model = GetSongs(caller, arg);

			this._parent.tvFiles.ShowAll();
		}
		
	}
}