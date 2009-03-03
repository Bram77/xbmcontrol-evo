
using System;
using Gtk;
using Gdk;
using XBMC;

namespace xbmcontrolevo
{
	
	
	public class ShareBrowser
	{
		private string currentShareView;
		private TreeView _tvParent;
		public XBMC_Communicator _Xbmc;
		public MainWindow _parent;
		private TreeStore tsShares;
		private TreeIter shareIter;
		private TreeIter selectedIter;
		private TreeModel selectedModel;
		private TreeViewColumn tvcIcons;
		private TreeViewColumn tvcShares;
		private TreeViewColumn tvcPaths;
		private TreeViewColumn tvcType;
		
		public ShareBrowser(TreeView tvParent, MainWindow parent)
		{
			tsShares = new TreeStore (typeof (Pixbuf), typeof (string), typeof (string), typeof (string), typeof (string));
			
			_parent = parent;
			_tvParent = tvParent;
			_Xbmc = parent.Xbmc;
			selectedIter = new TreeIter();
		}
		
		public void Populate (string shareType)
		{
			currentShareView = shareType;
			
			foreach (TreeViewColumn col in _tvParent.Columns) 
	        	_tvParent.RemoveColumn(col);
			
			tsShares.Clear();
	 		
			tvcIcons 	= _tvParent.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 0);
			tvcShares 	= _tvParent.AppendColumn ("", new CellRendererText (), "text", 1);
			tvcPaths	= _tvParent.AppendColumn ("", new CellRendererText (), "text", 2);
			tvcType		= _tvParent.AppendColumn ("", new CellRendererText (), "text", 3);
			
			tvcPaths.Visible 		 = false;
			tvcType.Visible 	 	 = false;
			tvcIcons.Sizing 		 = TreeViewColumnSizing.Autosize;
			tvcShares.Sizing 		 = TreeViewColumnSizing.Autosize;
			_tvParent.HeadersVisible = false;
			
			_tvParent.ColumnsAutosize();
			_tvParent.EnableGridLines = TreeViewGridLines.Horizontal;

			this.GetShares();
		}
		
		internal void GetShares()
		{
			string[] aShares 		= null;
	        string[] aSharesPaths 	= null;
			
			aShares 	 = _Xbmc.Media.GetShares(currentShareView);
	        aSharesPaths = _Xbmc.Media.GetShares(currentShareView, true);
			
			if (aShares != null)
			{
				for (int x = 0; x < aShares.Length; x++)
				{
					string icon;
					string[] aPrefix = aSharesPaths[x].Split(':');
					
					if (aShares[x] == "DVD-ROM Drive")
						icon = "dvd_32";
					else if (aSharesPaths[x] == "special://musicplaylists/")
						icon = "playlist_32";
					else if (aPrefix[0] == "lastfm")
						icon = "lastfm_32";
					else if (aPrefix[0] == "shout")
						icon = "shoutcast_32";
					else if (aPrefix[0] == "smb" || aPrefix[0] == "ftp")
						icon = "share_network_32";
					else
						icon = "share_32";
					
					tsShares.AppendValues (new Pixbuf ("images/" + icon + ".png"), aShares[x], aSharesPaths[x], "share");
				}
				
				_tvParent.Model = tsShares;
				_tvParent.Show();
			}
		}
		
		internal TreeStore GetDirectoryContent(string startPath)
		{
			tsShares = GetDirectories(startPath);
			tsShares = GetFiles(startPath);
			
			return tsShares;
		}
		
		internal TreeStore GetDirectories(string startPath)
		{
			string[] aDirectoryContent = _Xbmc.Media.GetDirectoryContentNames(startPath, "/");
			string[] aDirectoryContentPath = _Xbmc.Media.GetDirectoryContentPaths(startPath, "/");
		    
			if (aDirectoryContentPath != null)
            {
                for (int x = 0; x < aDirectoryContentPath.Length; x++)
                {
                    if (aDirectoryContentPath[x] != null && aDirectoryContentPath[x] != "" && aDirectoryContentPath[x].IndexOf(".") < 1)
                    	tsShares.AppendValues(selectedIter, new Pixbuf ("images/folder_closed.png"), aDirectoryContent[x], aDirectoryContentPath[x], "folder");
				}
            }
			
			return tsShares;
		}
		
		internal TreeStore GetFiles(string startPath)
		{
			string[] aFiles		 	= _Xbmc.Media.GetDirectoryContentNames(startPath, "[" + currentShareView + "]");
		    string[] aFilesPath		= _Xbmc.Media.GetDirectoryContentPaths(startPath, "[" + currentShareView + "]");
			
			if (aFilesPath != null)
			{
				for (int y = 0; y < aFilesPath.Length; y++)
				{
					if (aFilesPath[y] != null && aFilesPath[y] != "")
						tsShares.AppendValues(selectedIter, new Pixbuf ("images/file_" + currentShareView + ".png"), aFiles[y], aFilesPath[y], "file");
				}
			}
			
			return tsShares;
		}
		
		public void ExpandSelectedDirectory ()
		{
			if (_tvParent.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				string selectedType = selectedModel.GetValue(selectedIter, 3).ToString();
				
				if (!_tvParent.GetRowExpanded(selectedModel.GetPath(selectedIter)))
				{
					if (!tsShares.IterHasChild(selectedIter))
					{
			    		string startPath = tsShares.GetValue(selectedIter, 2).ToString();
	
						_tvParent.Model = GetDirectoryContent(startPath);
						_tvParent.ShowAll();
					}
					
					if (selectedType == "folder")
						tsShares.SetValue(selectedIter, 0, new Pixbuf ("images/folder_open.png"));
					_tvParent.ExpandRow(selectedModel.GetPath(selectedIter), false);
				}
				else
				{
					if (selectedType == "folder")
						tsShares.SetValue(selectedIter, 0, new Pixbuf ("images/folder_closed.png"));
					_tvParent.CollapseRow(selectedModel.GetPath(selectedIter));
				}
			}
		}
		
		public void ShowContextMenu () 
		{
			if (_tvParent.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				string selectedPath = selectedModel.GetValue(selectedIter, 2).ToString();
				string selectedType = selectedModel.GetValue(selectedIter, 3).ToString();
				
				if (selectedType == "folder" || selectedType == "share")
					_parent.contextMenu.Show("folder", selectedPath, currentShareView);
				else if (selectedType == "file")
					_parent.contextMenu.Show("file", selectedPath, null);
				else
					_parent.contextMenu.Show("default", null, null);
			}
		}
	}
}
