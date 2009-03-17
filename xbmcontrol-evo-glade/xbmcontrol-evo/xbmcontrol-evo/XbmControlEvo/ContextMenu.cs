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
			else if (contextMenu == "playlist")
				CreatePlaylistMenu(selectedPath);
			else
				CreateDefaultMenu();
		}
		
		private void CreateDefaultMenu()
		{
			Menu cmDefault 			= new Menu();
			cmDefault.WidthRequest 	= 200;
			
			cmDefault.Add(_parent.oMenuItems.Previous());
			cmDefault.Add(_parent.oMenuItems.PlayPause());
			cmDefault.Add(_parent.oMenuItems.Stop());
			cmDefault.Add(_parent.oMenuItems.Next());
			cmDefault.Add(_parent.oMenuItems.Seperator());
			cmDefault.Add(_parent.oMenuItems.VolumeUp());
			cmDefault.Add(_parent.oMenuItems.VolumeDown());
			cmDefault.Add(_parent.oMenuItems.Mute());
			cmDefault.Add(_parent.oMenuItems.Seperator());
			cmDefault.Add(_parent.oMenuItems.Configuration());
			cmDefault.Add(_parent.oMenuItems.Seperator());
			cmDefault.Add(_parent.oMenuItems.Quit());
			
			cmDefault.ShowAll();
			cmDefault.Popup();
		}
	
		private void CreateDirectoryMenu(string directoryPath, string mediaType)
		{
			Menu cmMediaDirectory 			= new Menu();
			cmMediaDirectory.WidthRequest 	= 150;
			
			cmMediaDirectory.Add(_parent.oMenuItems.PlayDirectory(directoryPath, mediaType));
			cmMediaDirectory.Add(_parent.oMenuItems.EnqueDirectory(directoryPath, mediaType));
			cmMediaDirectory.Add(_parent.oMenuItems.Seperator());
			cmMediaDirectory.Add(_parent.oMenuItems.CollapseAll());
			
			cmMediaDirectory.ShowAll();
			cmMediaDirectory.Popup();
		}
		
		private void CreateFileMenu(string filePath)
		{
			Menu cmMediaFile 			= new Menu();
			cmMediaFile.WidthRequest 	= 150;
			
			cmMediaFile.Add(_parent.oMenuItems.PlayFile(filePath));
			cmMediaFile.Add(_parent.oMenuItems.EnqueFile(filePath));
			cmMediaFile.Add(_parent.oMenuItems.Seperator());
			cmMediaFile.Add(_parent.oMenuItems.ShowSongInfo("sharebrowser"));
			//cmMediaFile.Add(_parent.oMenuItems.SaveSelectedFile());
			cmMediaFile.Add(_parent.oMenuItems.Seperator());
			cmMediaFile.Add(_parent.oMenuItems.CollapseAll());
			
			cmMediaFile.ShowAll();
			cmMediaFile.Popup();
		}
		
		private void CreatePlaylistMenu(string selectedPath)
		{
			Menu cmPlaylistEntry		= new Menu();
			cmPlaylistEntry.WidthRequest 	= 150;
			
			cmPlaylistEntry.Add(_parent.oMenuItems.PlayPlaylistEntry());
			cmPlaylistEntry.Add(_parent.oMenuItems.RemovePlaylistEntry());
			cmPlaylistEntry.Add(_parent.oMenuItems.Seperator());
			cmPlaylistEntry.Add(_parent.oMenuItems.ShowSongInfo("playlist"));
			cmPlaylistEntry.Add(_parent.oMenuItems.Seperator());
			cmPlaylistEntry.Add(_parent.oMenuItems.RefreshPlaylist());
			cmPlaylistEntry.Add(_parent.oMenuItems.ClearPlaylist());
			
			cmPlaylistEntry.ShowAll();
			cmPlaylistEntry.Popup();
		}
	}
}
