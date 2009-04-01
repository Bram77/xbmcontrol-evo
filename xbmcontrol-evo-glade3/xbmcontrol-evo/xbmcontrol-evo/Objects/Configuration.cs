
using System;
using System.IO;
using System.Security.AccessControl;
using System.Xml;

namespace xbmcontrolevo
{
	
	public struct sFieldName
	{
		public string startElement, ipAddress, username, password, showInTaskbar, showInSystemTray, updateInterval, connectionTimeout;
		
		public sFieldName (string init)
		{
			startElement		= "configuration";
			ipAddress 			= "ip_address";
			username			= "username";
			password			= "password";
			showInTaskbar 		= "show_in_taskbar";
			showInSystemTray	= "show_in_systemtray";
			updateInterval 		= "update_interval";
			connectionTimeout	= "connection_timeout";
		}
	}
	
	public struct sValues
	{
		public string ipAddress, username, password;
		public bool showInTaskbar, showInSystemTray;
		public double updateInterval, connectionTimeout;
		
		public sValues (string init)
		{
			ipAddress 			= "";
			username 			= "xbmc";
			password			= "xbmc";
			showInTaskbar		= true;
			showInSystemTray	= true;
			updateInterval		= 1.0;
			connectionTimeout	= 6.0;
		}
	}
	
	public class Configuration
	{
		private XbmControlEvo _parent;
		private string configFile;
		private XmlDocument xmlDoc;
		private XmlTextWriter XmlConfigWriter;
		private FileInfo xmlConfigFile;
		private FileStream sXmlConfigFile;
		
		public sFieldName fieldName;
		public sValues values;
		
		
		
		public Configuration(XbmControlEvo parent)
		{
			fieldName		= new sFieldName(null);
			values			= new sValues(null);
			_parent 		= parent;
			configFile		= _parent.appDir + "/config.xml";
			xmlDoc			= new XmlDocument();
			xmlConfigFile 	= new FileInfo(configFile);
			
			Load();
		}
		
		private void Load()
		{
			try 
			{
				xmlDoc.Load(configFile);
				values.ipAddress 			= GetStringValue(fieldName.ipAddress);
				values.username				= GetStringValue(fieldName.username);
				values.password				= GetStringValue(fieldName.password);
				values.updateInterval		= GetDoubleValue(fieldName.updateInterval);
				values.connectionTimeout	= GetDoubleValue(fieldName.connectionTimeout);
				values.showInTaskbar		= GetBoolValue(fieldName.showInTaskbar);
				values.showInSystemTray		= GetBoolValue(fieldName.showInSystemTray);
			}
			catch (Exception e)
			{
				Save(true);
				if (_parent.DEBUG) _parent.oHelper.Messagebox(e.Message);
			}
			
			ShowConfig();
		}
		
		public void Save(bool newConfigFile)
		{
			if (!newConfigFile)
			{
				values.ipAddress			= _parent._eIpAddress.Text;
				values.username				= _parent._eUsername.Text;
				values.password				= _parent._ePassword.Text;
				values.updateInterval		= _parent._sbUpdateInterval.Value;
				values.connectionTimeout	= _parent._sbConnectionTimeout.Value;
				values.showInTaskbar		= _parent._chbShowInTaskbar.Active; 
				values.showInSystemTray		= _parent._chbShowInSystemTray.Active;
			}
			
			try
			{
				sXmlConfigFile	= xmlConfigFile.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				XmlConfigWriter	= new XmlTextWriter(sXmlConfigFile, null);
				XmlConfigWriter.Formatting 	= Formatting.Indented;
				XmlConfigWriter.Indentation = 2;
				
			 	XmlConfigWriter.WriteStartDocument(true);
					XmlConfigWriter.WriteStartElement(fieldName.startElement);
						XmlConfigWriter.WriteElementString(fieldName.ipAddress, values.ipAddress);
						XmlConfigWriter.WriteElementString(fieldName.username, values.username);
						XmlConfigWriter.WriteElementString(fieldName.password, values.password);
						XmlConfigWriter.WriteElementString(fieldName.updateInterval, values.updateInterval.ToString());
						XmlConfigWriter.WriteElementString(fieldName.connectionTimeout, values.connectionTimeout.ToString());
						XmlConfigWriter.WriteElementString(fieldName.showInTaskbar, values.showInTaskbar.ToString());
						XmlConfigWriter.WriteElementString(fieldName.showInSystemTray, values.showInSystemTray.ToString());
					XmlConfigWriter.WriteEndElement();
				XmlConfigWriter.WriteEndDocument();
				
				XmlConfigWriter.Flush();
				XmlConfigWriter.Close();
				sXmlConfigFile.Close();
			}
			catch (Exception e)
			{
				if (_parent.DEBUG) _parent.oHelper.Messagebox(e.Message);
			}
			
			ShowConfig();
		}
		
		public void Save ()
		{
			Save(false);
		}
		
		public void ShowConfig()
		{
			_parent._eIpAddress.Text 			= values.ipAddress;
			_parent._eUsername.Text				= values.username;
			_parent._ePassword.Text				= values.password;
			_parent._sbUpdateInterval.Value		= values.updateInterval;
			_parent._sbConnectionTimeout.Value	= values.connectionTimeout;
			_parent._chbShowInTaskbar.Active	= Convert.ToBoolean(values.showInTaskbar);
			_parent._chbShowInSystemTray.Active	= Convert.ToBoolean(values.showInSystemTray);
		}
		
		public string GetStringValue(string node)
		{
			string nodeValue = "";
			
			try
			{
				nodeValue = xmlDoc.SelectSingleNode(fieldName.startElement + "/" + node).InnerText;
			}
			catch (Exception e)
			{
				if (_parent.DEBUG) _parent.oHelper.Messagebox(e.Message);	
			}
			
			return nodeValue;
		}
		
		public bool GetBoolValue(string node)
		{
			bool nodeValue = true;
			
			try
			{
				nodeValue = (xmlDoc.SelectSingleNode(fieldName.startElement + "/" + node).InnerText == "1")? true : false;
			}
			catch (Exception e)
			{
				if (_parent.DEBUG) _parent.oHelper.Messagebox(e.Message);	
			}
			
			return nodeValue;	
		}
		
		public int GetIntValue(string node)
		{
			int nodeValue = 0;
			
			try
			{
				nodeValue = Convert.ToInt32(xmlDoc.SelectSingleNode(fieldName.startElement + "/" + node).InnerText);
			}
			catch (Exception e)
			{
				if (_parent.DEBUG) _parent.oHelper.Messagebox(e.Message);	
			}
			
			return nodeValue;
		}
		
		public double GetDoubleValue(string node)
		{
			double nodeValue = 1.0;
			
			try
			{
				nodeValue = Convert.ToDouble(Convert.ToInt32(xmlDoc.SelectSingleNode(fieldName.startElement + "/" + node).InnerText));
			}
			catch (Exception e)
			{
				if (_parent.DEBUG) _parent.oHelper.Messagebox(e.Message);	
			}
			
			return nodeValue;
		}
	}
}
