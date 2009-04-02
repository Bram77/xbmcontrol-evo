using System;
using System.IO;
using System.Drawing.Imaging;
using Gtk;

namespace xbmcontrolevo
{
	
	
	public class HelperFunctions
	{
		private XbmControlEvo _parent;
		
		public HelperFunctions(XbmControlEvo parent)
		{
			_parent = parent;
		}
		
		public void Messagebox (string message)
		{
			MessageDialog md = new MessageDialog(_parent.MainWindow, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, message);
			md.Modal = true;
			ResponseType result = (ResponseType)md.Run();                 
	        if (result == ResponseType.Ok) md.Destroy();
		}
		
		public bool Confirm (string message)
		{
			MessageDialog md = new MessageDialog(_parent.MainWindow, DialogFlags.DestroyWithParent, MessageType.Question, ButtonsType.YesNo, message);
			md.Modal = true;
			ResponseType result = (ResponseType)md.Run(); 
			
	        if (result == ResponseType.Yes)
			{
				md.Destroy();
				return true;
		 	}
		 	else
			{
				md.Destroy();
				return false; 
			}
		}
		
		public string SecondsToHumanTime(string seconds)
		{
			double totalSeconds = Convert.ToDouble(seconds);
			double iHours		= Math.Floor(((double) totalSeconds/60) /60);
			double iMinutes 	= Math.Floor((double) totalSeconds/60);
			double iSeconds 	= totalSeconds-(Math.Floor((double) totalSeconds/60)*60);
			
			string sHours		= (iHours < 10)? "0" +iHours.ToString() : iHours.ToString();
			string sMinutes 	= (iMinutes < 10)? "0" +iMinutes.ToString() : iMinutes.ToString();
			string sSeconds 	= (iSeconds < 10)? "0" +iSeconds.ToString() : iSeconds.ToString(); 
			string sDuration 	= sHours + ":" + sMinutes + ":" + sSeconds;
	
			return sDuration;
		}
		
		public Gdk.Pixbuf ImageToPixbuf(System.Drawing.Image image)
		{
			using (MemoryStream stream = new MemoryStream()) 
			{
				image.Save(stream, ImageFormat.Bmp);
				stream.Position = 0;
				Gdk.Pixbuf pixbuf = new Gdk.Pixbuf(stream);
				
				return pixbuf;
			}
		}
	
		
		public byte[] Base64DecodeString(string inputStr) 
	    {
	      byte[] encodedByteArray = Convert.FromBase64CharArray(inputStr.ToCharArray(), 0, inputStr.Length);
	      return encodedByteArray;
	    } 
	}
}
