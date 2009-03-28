
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
			
			tvcShareIcons 	= _parent._tvShares.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 0);
			tvcShareNames 	= _parent._tvShares.AppendColumn ("", new CellRendererText (), "text", 1);
			tvcSharePaths	= _parent._tvShares.AppendColumn ("", new CellRendererText (), "text", 2);
			tvcShareTypes	= _parent._tvShares.AppendColumn ("", new CellRendererText (), "text", 3);
			
			tvcSharePaths.Visible 	= false;
			tvcShareTypes.Visible 	= false;
			tvcShareIcons.Sizing 	= TreeViewColumnSizing.Autosize;
			tvcShareNames.Sizing	= TreeViewColumnSizing.Autosize;
			_parent._tvShares.ColumnsAutosize();

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
					
					tsShares.AppendValues (new Pixbuf(_parent.appDir + "/Interface/" + _parent.theme + "/icons/" +icon+ ".png"), aShares[x], aSharesPaths[x], "share");
				}
				
				_parent._tvShares.Model = tsShares;
				_parent._tvShares.ShowAll();
			}
		}
		
		public void CollapseAll()
		{
			_parent._tvShares.CollapseAll();
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
                    	tsShares.AppendValues(selectedIter, new Pixbuf (_parent.appDir + "/Interface/" + _parent.theme + "/icons/folder_closed.png"), aDirectoryContent[x], aDirectoryContentPath[x], "folder");
				}
            }
			
			return tsShares;
		}
		
		public void ExpandSelectedDirectory ()
		{
			if (_parent._tvShares.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				string selectedType = selectedModel.GetValue(selectedIter, 3).ToString();
				
				if (!_parent._tvShares.GetRowExpanded(selectedModel.GetPath(selectedIter)))
				{
					selectedPath = selectedModel.GetValue(selectedIter, 2).ToString();
					
					if (!tsShares.IterHasChild(selectedIter))
					{
						_parent._tvShares.Model = GetDirectories(selectedPath);
						_parent._tvShares.ShowAll();
					}
					
					if (selectedType == "folder" || selectedType == "share")
					{
						if (selectedType != "share")
							tsShares.SetValue(selectedIter, 0, new Pixbuf (_parent.appDir + "/Interface/" + _parent.theme + "/icons/folder_open.png"));
						_parent._tvShares.ExpandRow(selectedModel.GetPath(selectedIter), false);
					}
				}
				else
				{
					if (selectedType == "folder" || selectedType == "share")
					{
						if (selectedType != "share")
							tsShares.SetValue(selectedIter, 0, new Pixbuf (_parent.appDir + "/Interface/" + _parent.theme + "/icons/folder_closed.png"));
						_parent._tvShares.CollapseRow(selectedModel.GetPath(selectedIter));
					}
				}
				
				_parent.oFileBrowser.ShowFiles (selectedType, selectedPath);
			}
		}
		
		public void ShowContextMenu () 
		{
			string selectedType = null;
			string selectedPath	= null;
			
			if (_parent._tvShares.Selection.GetSelected(out selectedModel, out selectedIter))
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
			
			//if (_parent._tvShares.Selection.GetSelected(out selectedModel, out selectedIter))
				//_parent.oMediaInfo.ShowSongInfoPopup(selectedModel.GetValue(selectedIter, 2).ToString());
		}
	}
}
