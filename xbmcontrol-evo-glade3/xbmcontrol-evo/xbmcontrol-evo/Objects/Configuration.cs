
using System;
using System.IO;
using System.Xml;

namespace xbmcontrolevo
{
	
	
	public class Configuration
	{
		private XbmControlEvo _parent;
		
		private string ipAddress;
		private string username;
		private string password;
		private string updateInterval;
		private string connectionTimeout;
		private string showInTaskbar;
		private string showInSystemTray;
		private string configFile;
		
		private XmlDocument XmlConfigFile;
		private XmlTextWriter XmlConfigWriter;
		
		public Configuration(XbmControlEvo parent)
		{
			_parent 		= parent;
			XmlConfigFile	= new XmlDocument();
			configFile		= _parent.appDir + "/config.xml";
			
			//Set default values
			ipAddress 			= "";
			username 			= "";
			password 			= "";
			updateInterval		= "1";
			connectionTimeout	= "6";
			showInTaskbar		= "1";
			showInSystemTray	= "1";
		
			this.Load();
		}
		
		private void Load()
		{
			if (!File.Exists(configFile)) 
				this.WriteConfigFile();
			
			XmlConfigFile.Load(configFile);
			this.ShowConfig();
		}
		
		public void Save()
		{
			ipAddress			= _parent._eIpAddress.Text;
			username			= _parent._eUsername.Text;
			password			= _parent._ePassword.Text;
			updateInterval		= Convert.ToInt32(_parent._sbUpdateInterval.Value).ToString();
			connectionTimeout	= Convert.ToInt32(_parent._sbConnectionTimeout.Value).ToString();
			showInTaskbar		= (_parent._chbShowInTaskbar.Active)? "1" : "0" ; 
			showInSystemTray	= (_parent._chbShowInSystemTray.Active)? "1" : "0" ;
			
			this.WriteConfigFile();
			this.Load();
		}
		
		private void WriteConfigFile()
		{
			//recreate config file if it exists
			if (File.Exists(configFile)) File.Delete(configFile);
			
			//Create new config file
			XmlConfigWriter				= new XmlTextWriter(configFile, null);
			XmlConfigFile				= new XmlDocument();
			XmlConfigWriter.Formatting 	= Formatting.Indented;
			XmlConfigWriter.Indentation = 2;
			
			XmlConfigWriter.WriteStartElement("configuration");
			XmlConfigWriter.WriteElementString("ipAddress", ipAddress);
			XmlConfigWriter.WriteElementString("username", username);
			XmlConfigWriter.WriteElementString("password", password);
			XmlConfigWriter.WriteElementString("updateInterval", updateInterval);
			XmlConfigWriter.WriteElementString("connectionTimeout", connectionTimeout);
			XmlConfigWriter.WriteElementString("showInTaskbar", showInTaskbar);
			XmlConfigWriter.WriteElementString("showInSystemTray", showInSystemTray);
			XmlConfigWriter.WriteEndElement();
			
			XmlConfigFile.Save(XmlConfigWriter);
			XmlConfigWriter.Close();
		}
		
		public void ShowConfig()
		{
			_parent._eIpAddress.Text 			= this.GetIpAddress();
			_parent._eUsername.Text				= this.GetUsername();
			_parent._ePassword.Text				= this.GetPassword();
			_parent._sbUpdateInterval.Value		= Convert.ToDouble(this.GetUpdateInterval());
			_parent._sbConnectionTimeout.Value	= Convert.ToDouble(this.GetConnectionTimeout().ToString());
			_parent._chbShowInTaskbar.Active	= this.GetShowInTaskbar();
			_parent._chbShowInSystemTray.Active	= this.GetShowInSystemTray();
		}
		
		public string GetIpAddress()
		{
			return XmlConfigFile.SelectSingleNode("configuration/ipAddress").InnerText;
		}
		
		public string GetUsername()
		{
			return XmlConfigFile.SelectSingleNode("configuration/username").InnerText;
		}
		
		public string GetPassword()
		{
			return XmlConfigFile.SelectSingleNode("configuration/password").InnerText;
		}
		
		public int GetUpdateInterval()
		{
			return Convert.ToInt32(XmlConfigFile.SelectSingleNode("configuration/updateInterval").InnerText);
		}
		
		public int GetConnectionTimeout()
		{
			return Convert.ToInt32(XmlConfigFile.SelectSingleNode("configuration/connectionTimeout").InnerText);
		}
		
		public bool GetShowInTaskbar()
		{
			return (XmlConfigFile.SelectSingleNode("configuration/showInTaskbar").InnerText == "1")? true : false ;
		}
		
		public bool GetShowInSystemTray()
		{
			return (XmlConfigFile.SelectSingleNode("configuration/showInSystemTray").InnerText == "1")? true : false ;
		}
	}
}
