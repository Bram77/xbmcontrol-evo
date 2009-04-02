
using System;
using Gtk;
using Gdk;
using Notifications;

namespace xbmcontrolevo
{
	
	
	public class SysTrayIcon
	{
		private StatusIcon siTray;
		private XbmControlEvo _parent;
		private Notification balloonTip;
		
		public SysTrayIcon(XbmControlEvo parent)
		{
			_parent 			= parent;
			
			siTray 				= new StatusIcon(_parent.oImages.menu.icon);
			siTray.Visible 		= true;
			siTray.Activate 	+= OnActivate;
			siTray.PopupMenu 	+= OnTrayIconPopup;
			siTray.Tooltip 		= "XBMControl Evo";
			balloonTip			= new Notification();
			
			ShowBallonTip();
		}
		
		private void OnTrayIconPopup (object o, EventArgs args) 
		{
			_parent.oContextMenu.Show("default", null);
		}
		
		private void OnActivate(object o, EventArgs args)
		{
			_parent.MainWindow.Visible = !_parent.MainWindow.Visible;
		}
		
		public void Show ()
		{
			siTray.Visible = true;
		}
		
		public void Hide ()
		{
			siTray.Visible = false;
		}
		
		public void ShowBallonTip ()
		{
			balloonTip.Icon		= _parent.oImages.button.drive;
			balloonTip.Summary	= "XBMControl Evo - Now Playing";
			balloonTip.Body		= "Test";
			balloonTip.Urgency	= Urgency.Critical;
			balloonTip.Show();
		}
	}
}
