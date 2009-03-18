
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
		private TreeViewColumn tvcFileIcons;
		private TreeViewColumn tvcFileNames;
		private TreeViewColumn tvcFilePaths;
		private TreeViewColumn tvcFileTypes;
		
		public FileBrowser(XbmControlEvo parent)
		{
			_parent = parent;
			
			tsFiles			= new TreeStore (typeof (Pixbuf), typeof (string), typeof (string), typeof (string), typeof (string));
			
			tvcFileIcons 	= _parent._tvFiles.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 0);
			tvcFileNames 	= _parent._tvFiles.AppendColumn ("", new CellRendererText (), "text", 1);
			tvcFilePaths	= _parent._tvFiles.AppendColumn ("", new CellRendererText (), "text", 2);
			tvcFileTypes	= _parent._tvFiles.AppendColumn ("", new CellRendererText (), "text", 3);
			
			tvcFilePaths.Visible  	= false;
			tvcFileTypes.Visible  	= false;
			tvcFileIcons.Sizing		= TreeViewColumnSizing.Autosize;
			tvcFileNames.Sizing		= TreeViewColumnSizing.Autosize;
			_parent._tvFiles.ColumnsAutosize();
		}
		
		public void ShowContextMenu ()
		{
			if (_parent._tvFiles.Selection.GetSelected(out selectedModel, out selectedIter))
				_parent.oContextMenu.Show("file", selectedModel.GetValue(selectedIter, 2).ToString(), null);
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
						string mediaType = (aFilesPathParts[0] == "lastfm")? "lastfm" : "file" ;
						tsFiles.AppendValues(new Pixbuf ("Images/file_" + _parent.oShareBrowser.GetCurrentShareType() + ".png"), aFiles[y], aFilesPath[y], mediaType);
					}
				}
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
						tsFiles.AppendValues(new Pixbuf ("Images/file_music.png"), aSongs[y], aSongsPath[y], "file");
				}
			}
			
			return tsFiles;
		}
		
		public void ShowFiles (string caller, string arg)
		{
			tsFiles.Clear();
			_parent._nbRight.CurrentPage = 1;
			
			if (caller == "share" || caller == "folder")
				_parent._tvFiles.Model = this.GetFiles(arg);
			else if (caller == "artist" || caller == "album")
				_parent._tvFiles.Model = this.GetSongs(caller, arg);

			_parent._tvFiles.ShowAll();
		}
		
	}
}