
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
			siTray.Activate 	+= delegate { _parent.Visible = !_parent.Visible; };
			siTray.PopupMenu 	+= OnTrayIconPopup;
			siTray.Tooltip 		= "XBMControl Evo";
		}
		
		private void OnTrayIconPopup (object o, EventArgs args) 
		{
			_parent.contextMenu.Show("default", null, null);
		}
		
		
	}
}
