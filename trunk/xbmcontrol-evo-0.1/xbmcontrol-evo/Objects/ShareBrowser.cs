
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class ShareBrowser
	{
		
		private XbmControlEvo _parent;
		private string[] aShareTypes;
		private string currentShareType;
		private string selectedPath;
		
		private TreeIter selectedIter;
		private TreeModel selectedModel;
		
		private TreeStore tsShares;
		private TreeViewColumn tvcShareIcons;
		private TreeViewColumn tvcShareNames;
		private TreeViewColumn tvcSharePaths;
		private TreeViewColumn tvcShareTypes;
		
		public ShareBrowser(XbmControlEvo parent)
		{
			selectedIter 	= new TreeIter();
			_parent 		= parent;
			
			tsShares 	 	= new TreeStore (typeof (Pixbuf), typeof (string), typeof (string), typeof (string), typeof (string));
			
			tvcShareIcons 	= _parent.tvShares.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 0);
			tvcShareNames 	= _parent.tvShares.AppendColumn ("", new CellRendererText (), "text", 1);
			tvcSharePaths	= _parent.tvShares.AppendColumn ("", new CellRendererText (), "text", 2);
			tvcShareTypes	= _parent.tvShares.AppendColumn ("", new CellRendererText (), "text", 3);
			
			tvcSharePaths.Visible 	= false;
			tvcShareTypes.Visible 	= false;
			tvcShareIcons.Sizing 	= TreeViewColumnSizing.Autosize;
			tvcShareNames.Sizing	= TreeViewColumnSizing.Autosize;
			_parent.tvShares.ColumnsAutosize();

			this.SetShareTypes();
			this.SetCurrentShareType(0);
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
		
		internal void Populate()
		{
			string[] aShares 		= _parent.oXbmc.Media.GetShares(currentShareType);
	        string[] aSharesPaths  	= _parent.oXbmc.Media.GetShares(currentShareType, true);
			
			if (aShares != null)
			{
				tsShares.Clear();
				
				for (int x = 0; x < aShares.Length; x++)
				{
					Pixbuf icon;
					string[] aPrefix = aSharesPaths[x].Split(':');
					
					if (aShares[x] == "DVD-ROM Drive")
						icon = _parent.oImages.button.dvd;
					else if (aSharesPaths[x] == "special://musicplaylists/")
						icon = _parent.oImages.button.playlist;
					else if (aPrefix[0] == "lastfm")
						icon = _parent.oImages.button.lastfm;
					else if (aPrefix[0] == "shout")
						icon = _parent.oImages.button.shoutcast;
					else if (aPrefix[0] == "smb" || aPrefix[0] == "ftp")
						icon = _parent.oImages.button.drive_network;
					else
						icon = _parent.oImages.button.drive;
					
					tsShares.AppendValues (icon, aShares[x], aSharesPaths[x], "share");
				}
				
				_parent.tvShares.Model = tsShares;
				_parent.tvShares.ShowAll();
			}
		}
		
		public void CollapseAll()
		{
			_parent.tvShares.CollapseAll();
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
                    	tsShares.AppendValues(selectedIter, _parent.oImages.menu.folder_closed, aDirectoryContent[x], aDirectoryContentPath[x], "folder");
				}
            }
			
			return tsShares;
		}
		
		public void ExpandSelectedDirectory ()
		{
			if (_parent.tvShares.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				string selectedType = selectedModel.GetValue(selectedIter, 3).ToString();
				
				if (!_parent.tvShares.GetRowExpanded(selectedModel.GetPath(selectedIter)))
				{
					selectedPath = selectedModel.GetValue(selectedIter, 2).ToString();
					
					if (!tsShares.IterHasChild(selectedIter))
					{
						_parent.tvShares.Model = GetDirectories(selectedPath);
						_parent.tvShares.ShowAll();
					}
					
					if (selectedType == "folder" || selectedType == "share")
					{
						if (selectedType != "share")
							tsShares.SetValue(selectedIter, 0, _parent.oImages.menu.folder_open);
						_parent.tvShares.ExpandRow(selectedModel.GetPath(selectedIter), false);
					}
				}
				else
				{
					if (selectedType == "folder" || selectedType == "share")
					{
						if (selectedType != "share")
							tsShares.SetValue(selectedIter, 0, _parent.oImages.menu.folder_closed);
						_parent.tvShares.CollapseRow(selectedModel.GetPath(selectedIter));
					}
				}
				
				_parent.oFileBrowser.ShowFiles (selectedType, selectedPath);
			}
		}
		
		public void ShowContextMenu () 
		{
			string selectedType = null;
			string selectedPath	= null;
			
			if (_parent.tvShares.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				selectedPath = selectedModel.GetValue(selectedIter, 2).ToString();
				selectedType = selectedModel.GetValue(selectedIter, 3).ToString();
			}
			
			if (selectedType == "share" || selectedType == "folder" || selectedType == "file")
					_parent.oContextMenu.Show(selectedType, selectedPath);
			else
				_parent.oContextMenu.Show("default", null);
		}
		
		public void ShowSongInfoPopup()
		{
			//Model selectedModel;
			//TreeIter selectedIter = new TreeIter();
			
			//if (_parent.tvShares.Selection.GetSelected(out selectedModel, out selectedIter))
				//_parent.oMediaInfo.ShowSongInfoPopup(selectedModel.GetValue(selectedIter, 2).ToString());
		}
	}
}
