
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
			this.selectedIter 	= new TreeIter();
			this._parent 		= parent;
			this.tsShares 		= new TreeStore (typeof (Pixbuf), typeof (string), typeof (string), typeof (string), typeof (string));
			this.tvcShareIcons 	= this._parent.tvShares.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 0);
			this.tvcShareNames 	= this._parent.tvShares.AppendColumn ("", new CellRendererText (), "text", 1);
			this.tvcSharePaths	= this._parent.tvShares.AppendColumn ("", new CellRendererText (), "text", 2);
			this.tvcShareTypes	= this._parent.tvShares.AppendColumn ("", new CellRendererText (), "text", 3);
			
			this.tvcSharePaths.Visible 	= false;
			this.tvcShareTypes.Visible 	= false;
			this.tvcShareIcons.Sizing 	= TreeViewColumnSizing.Autosize;
			this.tvcShareNames.Sizing	= TreeViewColumnSizing.Autosize;
			this._parent.tvShares.ColumnsAutosize();

			this.SetShareTypes();
			this.SetCurrentShareType(0);
		}
		
		private void SetShareTypes()
		{
			this.aShareTypes = new string[4];
			this.aShareTypes[0] = "music";
			this.aShareTypes[1] = "video";
			this.aShareTypes[2] = "pictures";
			this.aShareTypes[3] = "files";
		}
		
		public string GetCurrentShareType()
		{
			return currentShareType;
		}
		
		public void SetCurrentShareType(int selectedShareType)
		{
			this.currentShareType = this.aShareTypes[selectedShareType];
		}
		
		internal void Populate()
		{
			string[] aShares 		= this._parent.oXbmc.Media.GetShares(currentShareType);
	        string[] aSharesPaths  	= this._parent.oXbmc.Media.GetShares(currentShareType, true);
			
			if (aShares != null)
			{
				this.tsShares.Clear();
				
				for (int x = 0; x < aShares.Length; x++)
				{
					Pixbuf icon;
					string[] aPrefix = aSharesPaths[x].Split(':');
					
					if (aShares[x] == "DVD-ROM Drive")
						icon = this._parent.oImages.button.dvd;
					else if (aSharesPaths[x] == "special://musicplaylists/")
						icon = this._parent.oImages.button.playlist;
					else if (aPrefix[0] == "lastfm")
						icon = this._parent.oImages.button.lastfm;
					else if (aPrefix[0] == "shout")
						icon = this._parent.oImages.button.shoutcast;
					else if (aPrefix[0] == "smb" || aPrefix[0] == "ftp")
						icon = this._parent.oImages.button.drive_network;
					else
						icon = this._parent.oImages.button.drive;
					
					this.tsShares.AppendValues (icon, aShares[x], aSharesPaths[x], "share");
				}
				
				this._parent.tvShares.Model = tsShares;
				this._parent.tvShares.ShowAll();
			}
		}
		
		public void CollapseAll()
		{
			this._parent.tvShares.CollapseAll();
		}
		
		private TreeStore GetDirectories(string startPath)
		{
			string[] aDirectoryContentPath 	= this._parent.oXbmc.Media.GetDirectoryContentPaths(startPath, "/");
			string[] aDirectoryNames 		= this._parent.oXbmc.Media.GetDirectoryNames(aDirectoryContentPath);
		    
			if (aDirectoryContentPath != null)
            {
                for (int x = 0; x < aDirectoryContentPath.Length; x++)
                {
                    if (aDirectoryContentPath[x] != null && aDirectoryContentPath[x] != "")
                    	this.tsShares.AppendValues(this.selectedIter, this._parent.oImages.menu.folder_closed, aDirectoryNames[x].Normalize(), aDirectoryContentPath[x].Normalize(), "folder");
					//System.Windows.Forms.MessageBox.Show(aDirectoryContentPath[x]);
				}
            }
			
			return this.tsShares;
		}
		
		public void ExpandSelectedDirectory ()
		{
			if (this._parent.tvShares.Selection.GetSelected(out this.selectedModel, out this.selectedIter))
			{
				string selectedType = this.selectedModel.GetValue(selectedIter, 3).ToString();
				
				if (!this._parent.tvShares.GetRowExpanded(selectedModel.GetPath(this.selectedIter)))
				{
					this.selectedPath = this.selectedModel.GetValue(this.selectedIter, 2).ToString();

					if (!this.tsShares.IterHasChild(this.selectedIter))
					{
						this._parent.tvShares.Model = this.GetDirectories(this.selectedPath);
						this._parent.tvShares.ShowAll();		
					}
					
					if (selectedType == "folder" || selectedType == "share")
					{
						if (selectedType != "share")
							this.tsShares.SetValue(this.selectedIter, 0, this._parent.oImages.menu.folder_open);
						this._parent.tvShares.ExpandRow(selectedModel.GetPath(this.selectedIter), false);
					}
				}
				else
				{
					if (selectedType == "folder" || selectedType == "share")
					{
						if (selectedType != "share")
							tsShares.SetValue(selectedIter, 0, this._parent.oImages.menu.folder_closed);
						this._parent.tvShares.CollapseRow(selectedModel.GetPath(selectedIter));
					}
				}
				
				this._parent.oFileBrowser.ShowFiles(selectedType, selectedPath);
			}
		}
		
		public void ShowContextMenu () 
		{
			string selectedType = null;
			string selectedPath	= null;
			
			if (this._parent.tvShares.Selection.GetSelected(out selectedModel, out selectedIter))
			{
				selectedPath = selectedModel.GetValue(selectedIter, 2).ToString();
				selectedType = selectedModel.GetValue(selectedIter, 3).ToString();
			}
			
			if (selectedType == "share" || selectedType == "folder" || selectedType == "file")
					this._parent.oContextMenu.Show(selectedType, selectedPath);
			else
				this._parent.oContextMenu.Show("default", null);
		}
		
		public void ShowSongInfoPopup()
		{
			//Model selectedModel;
			//TreeIter selectedIter = new TreeIter();
			
			//if (this._parent.tvShares.Selection.GetSelected(out selectedModel, out selectedIter))
				//this._parent.oMediaInfo.ShowSongInfoPopup(selectedModel.GetValue(selectedIter, 2).ToString());
		}
	}
}
