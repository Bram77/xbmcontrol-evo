
using System;
using System.IO;
using System.Security.AccessControl;
using System.Xml;

namespace xbmcontrolevo
{
	
	public struct sFieldNames
	{
		public string startElement, ipAddress, username, password, showInTaskbar, showInSystemTray, updateInterval, connectionTimeout;
		
		public sFieldNames (string init)
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
		
		public sFieldNames fieldNames;
		public sValues values;
		
		
		
		public Configuration(XbmControlEvo parent)
		{
			fieldNames		= new sFieldNames(null);
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
				values.ipAddress 			= GetStringValue(fieldNames.ipAddress);
				values.username				= GetStringValue(fieldNames.username);
				values.password				= GetStringValue(fieldNames.password);
				values.updateInterval		= GetDoubleValue(fieldNames.updateInterval);
				values.connectionTimeout	= GetDoubleValue(fieldNames.connectionTimeout);
				values.showInTaskbar		= GetBoolValue(fieldNames.showInTaskbar);
				values.showInSystemTray		= GetBoolValue(fieldNames.showInSystemTray);
			}
			catch
			{
				Save(true);
			}
			
			ShowConfig();
		}
		
		public void Save(bool newConfigFile)
		{
			if (!newConfigFile)
			{
				values.ipAddress			= _parent.eIpAddress.Text;
				values.username				= _parent.eUsername.Text;
				values.password				= _parent.ePassword.Text;
				values.updateInterval		= _parent.sbUpdateInterval.Value;
				values.connectionTimeout	= _parent.sbConnectionTimeout.Value;
				values.showInTaskbar		= _parent.chbShowInTaskbar.Active; 
				values.showInSystemTray		= _parent.chbShowInSystemTray.Active;
			}
			
			try
			{
				sXmlConfigFile	= xmlConfigFile.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
				XmlConfigWriter	= new XmlTextWriter(sXmlConfigFile, null);
				XmlConfigWriter.Formatting 	= Formatting.Indented;
				XmlConfigWriter.Indentation = 2;
				
			 	XmlConfigWriter.WriteStartDocument(true);
					XmlConfigWriter.WriteStartElement(fieldNames.startElement);
						XmlConfigWriter.WriteElementString(fieldNames.ipAddress, values.ipAddress);
						XmlConfigWriter.WriteElementString(fieldNames.username, values.username);
						XmlConfigWriter.WriteElementString(fieldNames.password, values.password);
						XmlConfigWriter.WriteElementString(fieldNames.updateInterval, values.updateInterval.ToString());
						XmlConfigWriter.WriteElementString(fieldNames.connectionTimeout, values.connectionTimeout.ToString());
						XmlConfigWriter.WriteElementString(fieldNames.showInTaskbar, values.showInTaskbar.ToString());
						XmlConfigWriter.WriteElementString(fieldNames.showInSystemTray, values.showInSystemTray.ToString());
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
			_parent.eIpAddress.Text 			= values.ipAddress;
			_parent.eUsername.Text				= values.username;
			_parent.ePassword.Text				= values.password;
			_parent.sbUpdateInterval.Value		= values.updateInterval;
			_parent.sbConnectionTimeout.Value	= values.connectionTimeout;
			_parent.chbShowInTaskbar.Active		= Convert.ToBoolean(values.showInTaskbar);
			_parent.chbShowInSystemTray.Active	= Convert.ToBoolean(values.showInSystemTray);
		}
		
		public string GetStringValue(string node)
		{
			string nodeValue = "";
			
			try
			{
				nodeValue = xmlDoc.SelectSingleNode(fieldNames.startElement + "/" + node).InnerText;
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
				nodeValue = Convert.ToBoolean(xmlDoc.SelectSingleNode(fieldNames.startElement + "/" + node).InnerText);
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
				nodeValue = Convert.ToInt32(xmlDoc.SelectSingleNode(fieldNames.startElement + "/" + node).InnerText);
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
				nodeValue = Convert.ToDouble(Convert.ToInt32(xmlDoc.SelectSingleNode(fieldNames.startElement + "/" + node).InnerText));
			}
			catch (Exception e)
			{
				if (_parent.DEBUG) _parent.oHelper.Messagebox(e.Message);	
			}
			
			return nodeValue;
		}
	}
}
