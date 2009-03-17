
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class SysTrayIcon
	{
		private StatusIcon siTray;
		private MainWindow _parent;
		
		public SysTrayIcon(MainWindow parent)
		{
			_parent 			= parent;
			
			siTray 				= new StatusIcon(new Pixbuf ("images/icon.png"));
			siTray.Visible 		= true;
			siTray.Activate 	+= OnActivate;
			siTray.PopupMenu 	+= OnTrayIconPopup;
			siTray.Tooltip 		= "XBMControl Evo";
		}
		
		private void OnTrayIconPopup (object o, EventArgs args) 
		{
			_parent.oContextMenu.Show("default", null, null);
		}
		
		private void OnActivate(object o, EventArgs args)
		{
			_parent.Visible = !_parent.Visible;
		}
	}
}
