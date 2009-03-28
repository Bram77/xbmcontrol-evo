
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class SysTrayIcon
	{
		private StatusIcon siTray;
		private XbmControlEvo _parent;
		
		public SysTrayIcon(XbmControlEvo parent)
		{
			_parent 			= parent;
			
			siTray 				= new StatusIcon(new Pixbuf (_parent.appDir + "/Interface/" + _parent.theme + "/icons/icon.png"));
			siTray.Visible 		= true;
			siTray.Activate 	+= OnActivate;
			siTray.PopupMenu 	+= OnTrayIconPopup;
			siTray.Tooltip 		= "XBMControl Evo";
		}
		
		private void OnTrayIconPopup (object o, EventArgs args) 
		{
			_parent.oContextMenu.Show("default", null);
		}
		
		private void OnActivate(object o, EventArgs args)
		{
			_parent._MainWindow.Visible = !_parent._MainWindow.Visible;
		}
		
		public void Show ()
		{
			siTray.Visible = true;
		}
		
		public void Hide ()
		{
			siTray.Visible = false;
		}
	}
}
