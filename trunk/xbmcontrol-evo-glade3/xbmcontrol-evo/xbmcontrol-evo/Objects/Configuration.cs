
using System;
using System.Xml;

namespace xbmcontrolevo
{
	
	
	public class Configuration
	{
		private XbmControlEvo _parent;
		private string ipAddress;
		private string username;
		private string password;
		
		public Configuration(XbmControlEvo parent)
		{
			_parent 	= parent;
			ipAddress 	= null;
			username 	= null;
			password 	= null;
		}
		
		private bool Load()
		{
			XmlTextReader xmlReader = new XmlTextReader("config.xml");
			
			if (xmlReader.Read())
				_parent.oHelper.Messagebox("Configfile exists");
			
			return true;
		}
		
		public bool Save()
		{
			XmlTextWriter xmlWriter = new XmlTextWriter("config.xml", null);
			
			return true;
		}
	}
}
