
using System;
using Gtk;

namespace xbmcontrolevo
{
	
	
	public class ContextMenu
	{
		private XbmControlEvo _parent;
		
		public ContextMenu(XbmControlEvo parent)
		{
			_parent = parent;
		}
		
		public void Show(string caller, string identifier)
		{
			if (caller == "playlist")
				CreatePlaylistMenu(identifier);
			else if (caller == "default")
				CreateDefaultMenu();
			else
				CreateMediaMenu(caller, identifier);
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
	
		private void CreateMediaMenu(string caller, string identifier)
		{
			Menu cmMedia			= new Menu();
			cmMedia.WidthRequest 	= 150;
			
			cmMedia.Add(_parent.oMenuItems.Play(caller, identifier));
			cmMedia.Add(_parent.oMenuItems.Enque(caller, identifier));
			
			if (caller != "album")
			{
				cmMedia.Add(_parent.oMenuItems.Seperator());
				cmMedia.Add(_parent.oMenuItems.CollapseAll());
			}
			
			if (caller == "file")
			{
				cmMedia.Add(_parent.oMenuItems.Seperator());
				cmMedia.Add(_parent.oMenuItems.ShowSongInfo("sharebrowser"));
			}
			
			cmMedia.ShowAll();
			cmMedia.Popup();
		}
		
		private void CreatePlaylistMenu(string selectedPath)
		{
			Menu cmPlaylist			= new Menu();
			cmPlaylist.WidthRequest = 150;
			
			cmPlaylist.Add(_parent.oMenuItems.PlayPlaylistEntry());
			cmPlaylist.Add(_parent.oMenuItems.RemovePlaylistEntry());
			cmPlaylist.Add(_parent.oMenuItems.Seperator());
			cmPlaylist.Add(_parent.oMenuItems.ShowSongInfo("playlist"));
			cmPlaylist.Add(_parent.oMenuItems.Seperator());
			cmPlaylist.Add(_parent.oMenuItems.RefreshPlaylist());
			cmPlaylist.Add(_parent.oMenuItems.ClearPlaylist());
			
			cmPlaylist.ShowAll();
			cmPlaylist.Popup();
		}
	}
}
