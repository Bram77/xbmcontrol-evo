
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
		public TreeModelFilter tmfFilter;
		
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
			
			//Filter functionality
			tmfFilter	 			= new Gtk.TreeModelFilter(tsFiles, null);
			tmfFilter.VisibleFunc 	= new Gtk.TreeModelFilterVisibleFunc(FilterTree);
			_parent._tvFiles.Model	= tmfFilter;
		}
		
		public void ShowContextMenu ()
		{
			if (_parent._tvFiles.Selection.GetSelected(out selectedModel, out selectedIter))
				_parent.oContextMenu.Show("file", selectedModel.GetValue(selectedIter, 2).ToString(), null);
		}
		
		private bool FilterTree (Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			string fileTitle = model.GetValue(iter, 1).ToString();
			return (fileTitle == _parent._eFilterFiles.Text)? true : false;
		}
		
		public TreeStore GetFiles(string startPath)
		{
			tsFiles.Clear();
			
			string[] aFiles		 	= _parent.oXbmc.Media.GetDirectoryContentNames(startPath, "[" + _parent.oShareBrowser.GetCurrentShareType() + "]");
		    string[] aFilesPath		= _parent.oXbmc.Media.GetDirectoryContentPaths(startPath, "[" + _parent.oShareBrowser.GetCurrentShareType() + "]");
			
			if (aFilesPath != null)
			{
				_parent._nbRight.CurrentPage = 1;

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
		
		public void ShowFiles (string path)
		{
			_parent._tvFiles.Model = GetFiles(path);
			_parent._tvFiles.ShowAll();
			
		}
		
	}
}
