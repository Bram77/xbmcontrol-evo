
using System;
using System.IO;
using System.Xml;
using Gtk;

namespace xbmcontrolevo
{
	
	public struct sFieldNames
	{
		public string startElement, identifier, ipAddress, username, password, showInTaskbar, showInSystemTray, closeToSystemTray, updateInterval, connectionTimeout, theme;
		
		public sFieldNames (string init)
		{
			identifier			= "identifier";
			startElement		= "configuration";
			theme				= "theme";
			ipAddress 			= "ip_address";
			username			= "username";
			password			= "password";
			showInTaskbar 		= "show_in_taskbar";
			showInSystemTray	= "show_in_systemtray";
			closeToSystemTray	= "close_to_systemtray";
			updateInterval 		= "update_interval";
			connectionTimeout	= "connection_timeout";
		}
	}
	
	public struct sValues
	{
		public string identifier, ipAddress, username, password, theme;
		public bool showInTaskbar, showInSystemTray, closeToSystemTray;
		public double updateInterval, connectionTimeout;
		
		public sValues (string init)
		{
			identifier			= "XBMC";
			theme				= "default";
			ipAddress 			= "";
			username 			= "xbmc";
			password			= "xbmc";
			showInTaskbar		= true;
			showInSystemTray	= true;
			closeToSystemTray	= true;
			updateInterval		= 1.0;
			connectionTimeout	= 6.0;
		}
	}
	
	public class Configuration
	{
		private XbmControlEvo _parent;
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
			xmlDoc			= new XmlDocument();
			xmlConfigFile 	= new FileInfo(@_parent.configFile);
			
			Load();
		}
		
		private void Load()
		{
			try 
			{
				xmlDoc.Load(_parent.configFile);
				values.identifier			= GetStringValue(fieldNames.identifier);
				values.theme				= GetStringValue(fieldNames.theme);
				values.ipAddress 			= GetStringValue(fieldNames.ipAddress);
				values.username				= GetStringValue(fieldNames.username);
				values.password				= GetStringValue(fieldNames.password);
				values.updateInterval		= GetDoubleValue(fieldNames.updateInterval);
				values.connectionTimeout	= GetDoubleValue(fieldNames.connectionTimeout);
				values.showInTaskbar		= GetBoolValue(fieldNames.showInTaskbar);
				values.showInSystemTray		= GetBoolValue(fieldNames.showInSystemTray);
				values.closeToSystemTray	= GetBoolValue(fieldNames.closeToSystemTray);
				
				if (values.closeToSystemTray)
				{
					_parent.chbShowInSystemTray.Active		= true;
					_parent.chbShowInSystemTray.Sensitive 	= false;
				}
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
				values.identifier			= _parent.cbeIdentifier.ActiveText;
				values.theme				= _parent.cbTheme.ActiveText;
				values.ipAddress			= _parent.eIpAddress.Text;
				values.username				= _parent.eUsername.Text;
				values.password				= _parent.ePassword.Text;
				values.updateInterval		= _parent.sbUpdateInterval.Value;
				values.connectionTimeout	= _parent.sbConnectionTimeout.Value;
				values.showInTaskbar		= _parent.chbShowInTaskbar.Active; 
				values.showInSystemTray		= _parent.chbShowInSystemTray.Active;
				values.closeToSystemTray	= _parent.chbCloseToSystemTray.Active;
			}
			
			try
			{
				sXmlConfigFile				= xmlConfigFile.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
				XmlConfigWriter				= new XmlTextWriter(sXmlConfigFile, null);
				XmlConfigWriter.Formatting 	= Formatting.Indented;
				XmlConfigWriter.Indentation = 2;
				
			 	XmlConfigWriter.WriteStartDocument(true);
					XmlConfigWriter.WriteStartElement(fieldNames.startElement);
						XmlConfigWriter.WriteElementString(fieldNames.identifier, values.identifier);
						XmlConfigWriter.WriteElementString(fieldNames.theme, values.theme);
						XmlConfigWriter.WriteElementString(fieldNames.ipAddress, values.ipAddress);
						XmlConfigWriter.WriteElementString(fieldNames.username, values.username);
						XmlConfigWriter.WriteElementString(fieldNames.password, values.password);
						XmlConfigWriter.WriteElementString(fieldNames.updateInterval, values.updateInterval.ToString());
						XmlConfigWriter.WriteElementString(fieldNames.connectionTimeout, values.connectionTimeout.ToString());
						XmlConfigWriter.WriteElementString(fieldNames.showInTaskbar, values.showInTaskbar.ToString());
						XmlConfigWriter.WriteElementString(fieldNames.showInSystemTray, values.showInSystemTray.ToString());
						XmlConfigWriter.WriteElementString(fieldNames.closeToSystemTray, values.closeToSystemTray.ToString());
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
			ShowThemeNames(GetThemeNames());
			ShowIdentifier(values.identifier);
			_parent.eIpAddress.Text 			= values.ipAddress;
			_parent.eUsername.Text				= values.username;
			_parent.ePassword.Text				= values.password;
			_parent.sbUpdateInterval.Value		= values.updateInterval;
			_parent.sbConnectionTimeout.Value	= values.connectionTimeout;
			_parent.chbShowInTaskbar.Active		= Convert.ToBoolean(values.showInTaskbar);
			_parent.chbShowInSystemTray.Active	= Convert.ToBoolean(values.showInSystemTray);
			_parent.chbCloseToSystemTray.Active	= Convert.ToBoolean(values.closeToSystemTray);
		}
		
		private string[] GetThemeNames()
		{
			DirectoryInfo diInterfaceDir 	= new DirectoryInfo(_parent.appUserDir + "/Interface");
			DirectoryInfo[] aThemeDir		= diInterfaceDir.GetDirectories();
			string[] aThemeNames			= new string[aThemeDir.Length];
			
			for (int x=0; x<aThemeDir.Length; x++)
				aThemeNames[x] = aThemeDir[x].Name;
			
			return aThemeNames;
		}
		
		private void ShowThemeNames(string[] names)
		{
			_parent.cbTheme.Clear();
			CellRendererText cell 	= new CellRendererText();
	        _parent.cbTheme.PackStart(cell, false);
	        _parent.cbTheme.AddAttribute(cell, "text", 0);
	        ListStore store 		= new ListStore(typeof (string));
	        _parent.cbTheme.Model 	= store;
			
			for (int x=0; x<names.Length; x++)
			{
				if (File.Exists(_parent.appUserDir + "/Interface/" + names[x] + ".glade")) store.AppendValues(names[x]);
				if (values.theme == names[x]) _parent.cbTheme.Active = x;
			}
			
			_parent.cbTheme.ShowAll();
		}
		
		private void ShowIdentifier (string identifier)
		{
			_parent.cbeIdentifier.Clear();
			CellRendererText cell 		= new CellRendererText();
	        _parent.cbeIdentifier.PackStart(cell, false);
	        _parent.cbeIdentifier.AddAttribute(cell, "text", 0);
	        ListStore store 			= new ListStore(typeof (string));
	        _parent.cbeIdentifier.Model = store;
			store.AppendValues(identifier);
			_parent.cbeIdentifier.TextColumn = 0;
			_parent.cbeIdentifier.Active = 0;
			_parent.cbeIdentifier.ShowAll();
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
