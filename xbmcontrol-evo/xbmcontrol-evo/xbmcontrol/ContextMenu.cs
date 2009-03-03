
using System;
using Gtk;

namespace xbmcontrolevo
{
	
	
	public class ContextMenu
	{
		private MainWindow _parent;
		
		public ContextMenu(MainWindow parent)
		{
			_parent = parent;
		}
		
		public void Show(string contextMenu, string selectedPath, string mediaType)
		{
			if (contextMenu == "folder")
				CreateDirectoryMenu(selectedPath, mediaType);
			else if (contextMenu == "file")
				CreateFileMenu(selectedPath);
			else
				CreateDefaultMenu();
		}
		
		private void CreateDefaultMenu()
		{
			Menu cmDefault 			= new Menu();
			cmDefault.WidthRequest 	= 150;
			
			cmDefault.Add(_parent.menuItems.Previous());
			cmDefault.Add(_parent.menuItems.Play());
			cmDefault.Add(_parent.menuItems.Stop());
			cmDefault.Add(_parent.menuItems.Next());
			cmDefault.Add(_parent.menuItems.Seperator());
			cmDefault.Add(_parent.menuItems.Configuration());
			cmDefault.Add(_parent.menuItems.Seperator());
			cmDefault.Add(_parent.menuItems.Quit());
			
			cmDefault.ShowAll();
			cmDefault.Popup();
		}
	
		private void CreateDirectoryMenu(string directoryPath, string mediaType)
		{
			Menu cmMediaDirectory 			= new Menu();
			cmMediaDirectory.WidthRequest 	= 150;
			
			cmMediaDirectory.Add(_parent.menuItems.PlayRecursive(directoryPath, mediaType));
			cmMediaDirectory.Add(_parent.menuItems.Seperator());
			cmMediaDirectory.Add(_parent.menuItems.Quit());
			
			cmMediaDirectory.ShowAll();
			cmMediaDirectory.Popup();
		}
		
		private void CreateFileMenu(string filePath)
		{
			Menu cmMediaFile 			= new Menu();
			cmMediaFile.WidthRequest 	= 150;
			
			cmMediaFile.Add(_parent.menuItems.PlayFile(filePath));
			cmMediaFile.Add(_parent.menuItems.Seperator());
			cmMediaFile.Add(_parent.menuItems.Quit());
			
			cmMediaFile.ShowAll();
			cmMediaFile.Popup();
		}
	}
}
