
using System;
using Gtk;
using Gdk;


namespace xbmcontrolevo
{
	
	
	public class ShareBrowser
	{
		private MainWindow _parent;
		private string[] aShareTypes;
		private string currentShareType;
		private TreeStore tsShares;
		private TreeIter selectedIter;
		private TreeModel selectedModel;
		private TreeViewColumn tvcIcons;
		private TreeViewColumn tvcShares;
		private TreeViewColumn tvcPaths;
		private TreeViewColumn tvcType;
		
		public ShareBrowser(MainWindow parent)
		{
			tsShares 	 	= new TreeStore (typeof (Pixbuf), typeof (string), typeof (string), typeof (string), typeof (string));
			selectedIter 	= new TreeIter();
			_parent 		= parent;
			
			SetShareTypes();
			SetCurrentShareType(0);
			Populate();
		}
		
		private void SetShareTypes()
		{
			aShareTypes = new string[4];
			aShareTypes[0] = "music";
			aShareTypes[1] = "video";
			aShareTypes[2] = "pictures";
			aShareTypes[3] = "files";
		}
		
		public string GetCurrentShareType()
		{
			return currentShareType;
		}
		
		public void SetCurrentShareType(int selectedShareType)
		{
			currentShareType = aShareTypes[selectedShareType];
		}
		
		public void Populate()
		{
			foreach (TreeViewColumn col in _parent._tvShareBrowser.Columns) 
	        	_parent._tvShareBrowser.RemoveColumn(col);
			
			tsShares.Clear();
	 		
			tvcIcons 	= _parent._tvShareBrowser.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 0);
			tvcShares 	= _parent._tvShareBrowser.AppendColumn ("", new CellRendererText (), "text", 1);
			tvcPaths	= _parent._tvShareBrowser.AppendColumn ("", new CellRendererText (), "text", 2);
			tvcType		= _parent._tvShareBrowser.AppendColumn ("", new CellRendererText (), "text", 3);
			
			tvcPaths.Visible 		 = false;
			tvcType.Visible 	 	 = false;
			tvcIcons.Sizing 		 = TreeViewColumnSizing.Autosize;
			tvcShares.Sizing 		 = TreeViewColumnSizing.Autosize;
			_parent._tvShareBrowser.HeadersVisible = false;
			
			_parent._tvShareBrowser.ColumnsAutosize();
			//_parent._tvShareBrowser.EnableGridLines = TreeViewGridLines.Horizontal;

			this.GetShares();
		}
		
		internal void GetShares()
		{
			string[] aShares 		= null;
	        string[] aSharesPaths 	= null;
			
			aShares 	 = _parent.oXbmc.Media.GetShares(currentShareType);
	        aSharesPaths = _parent.oXbmc.Media.GetShares(currentShareType, true);
			
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
				
				_parent._tvShareBrowser.Model = tsShares;
				_parent._tvShareBrowser.ShowAll();
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
			string[] aDirectoryContent = _parent.oXbmc.Media.GetDirectoryContentNames(startPath, "/");
			string[] aDirectoryContentPath = _parent.oXbmc.Media.GetDirectoryContentPaths(startPath, "/");
		    
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
			string[] aFiles		 	= _parent.oXbmc.Media.GetDirectoryContentNames(startPath, "[" + currentShareType + "]");
		    string[] aFilesPath		= _parent.oXbmc.Media.GetDirectoryContentPaths(startPath, "[" + currentShareType + "]");
			
			if (aFilesPath != null)
			{
				for (int y = 0; y < aFilesPath.Length; y++)
				{
					if (aFilesPath[y] != null && aFilesPath[y] != "")
						tsShares.AppendValues(selectedIter, new Pixbuf ("images/file_" + currentShareType + ".png"), aFiles[y], aFilesPath[y], "file");
				}
			}
			
			return tsShares;
		}
		
		public void ExpandSelectedDirectory ()
		{
			if (_parent._tvShareBrowser.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				string selectedType = selectedModel.GetValue(selectedIter, 3).ToString();
				
				if (!_parent._tvShareBrowser.GetRowExpanded(selectedModel.GetPath(selectedIter)))
				{
					if (!tsShares.IterHasChild(selectedIter))
					{
			    		string startPath = tsShares.GetValue(selectedIter, 2).ToString();
	
						_parent._tvShareBrowser.Model = GetDirectoryContent(startPath);
						_parent._tvShareBrowser.ShowAll();
					}
					
					if (selectedType == "folder" || selectedType == "share")
					{
						if (selectedType != "share")
							tsShares.SetValue(selectedIter, 0, new Pixbuf ("images/folder_open.png"));
						_parent._tvShareBrowser.ExpandRow(selectedModel.GetPath(selectedIter), false);
					}
				}
				else
				{
					if (selectedType == "folder" || selectedType == "share")
					{
						if (selectedType != "share")
							tsShares.SetValue(selectedIter, 0, new Pixbuf ("images/folder_closed.png"));
						_parent._tvShareBrowser.CollapseRow(selectedModel.GetPath(selectedIter));
					}
				}
			}
		}
		
		public void ShowContextMenu () 
		{
			if (_parent._tvShareBrowser.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				string selectedPath = selectedModel.GetValue(selectedIter, 2).ToString();
				string selectedType = selectedModel.GetValue(selectedIter, 3).ToString();
				
				if (selectedType == "folder" || selectedType == "share")
					_parent.oContextMenu.Show("folder", selectedPath, currentShareType);
				else if (selectedType == "file")
					_parent.oContextMenu.Show("file", selectedPath, null);
				else
					_parent.oContextMenu.Show("default", null, null);
			}
		}
	}
}
