// ------------------------------------------------------------------------
//    XBMControl - A compact remote controller for XBMC (.NET 3.5)
//    Copyright (C) 2008  Bram van Oploo (bramvano@gmail.com)
//                        Mike Thiels (Mike.Thiels@gmail.com)
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace XBMC
{
    public class XBMC_Media
    {
        XBMC_Communicator parent = null;

        public XBMC_Media(XBMC_Communicator p)
        {
            parent = p;
        }

        public string[] GetShares(string type, bool path)
        {
            string[] aMediaShares = parent.Request("GetShares(" + type + ")");

            if (aMediaShares != null)
            {
                string[] aShareNames = new string[aMediaShares.Length];
                string[] aSharePaths = new string[aMediaShares.Length];

                for (int x = 0; x < aMediaShares.Length; x++)
                {
                    string[] aTmpShare = aMediaShares[x].Split(';');

                    if (aTmpShare != null)
                    {
                        aShareNames[x] = aTmpShare[0];
                        aSharePaths[x] = aTmpShare[1];
                    }
                }

                return (path) ? aSharePaths : aShareNames;
            }
            else
                return null;
        }

        public string[] GetShares(string type)
        {
            return GetShares(type, false);
        }

        public string[] GetDirectoryContentPaths(string directory, string mask)
        {
            mask = (mask == null) ? "" : ";" + mask;

            string[] aDirectoryContent = parent.Request("GetDirectory(" + directory + mask + ")");

            if (aDirectoryContent != null)
            {
                string[] aContentPaths = new string[aDirectoryContent.Length];

                for (int x = 0; x < aDirectoryContent.Length; x++)
                    aContentPaths[x] = (aDirectoryContent[x] == "Error:Not folder" || aDirectoryContent[x] == "Error") ? null : aDirectoryContent[x];

                return aContentPaths;
            }
            else
                return null;
        }

        public string[] GetDirectoryContentPaths(string directory)
        {
            return GetDirectoryContentPaths(directory, null);
        }

        public string[] GetDirectoryNames(string[] aContentPaths)
        {
            if (aContentPaths != null)
            {
                string[] aContentNames = new string[aContentPaths.Length];

                for (int x = 0; x < aContentPaths.Length; x++)
                {
                    if (aContentPaths[x] == null || aContentPaths[x] == "")
                        aContentNames[x] = null;
                    else
                    {
                        aContentPaths[x] 		= aContentPaths[x].Replace("\\", "/");
                        string[] aTmpContent 	= aContentPaths[x].Split('/');
	                    aContentNames[x] 		= (aTmpContent[aTmpContent.Length - 1] == "") ? aTmpContent[aTmpContent.Length - 2] : aTmpContent[aTmpContent.Length - 1];
                    }
                }

                return aContentNames;
            }
            else
                return null;
        }
		
		public string GetMusicTagByFilepath(string filepath, string field)
		{
			string[] aMusicTagTemp = parent.Request("GetTagFromFilename(" + filepath + ")");

            if (aMusicTagTemp != null)
            {
                string[,] aMusicTag = new string[aMusicTagTemp.Length, 2];
				
                for (int x = 0; x < aMusicTagTemp.Length; x++)
                {
                    int splitIndex = aMusicTagTemp[x].IndexOf(':') + 1;

                    if (splitIndex > 2)
                    {
                        aMusicTag[x, 0] = aMusicTagTemp[x].Substring(0, splitIndex - 1).Replace(" ", "").ToLower();
                        aMusicTag[x, 1] = aMusicTagTemp[x].Substring(splitIndex, aMusicTagTemp[x].Length - splitIndex);
                        
                        if (aMusicTag[x, 0] == field)
                            return aMusicTag[x, 1];
                    }
                }
            }

			return "No data found";
		}
		
		public string GetFileThumbnailLocation(string filePath)
		{
			string[] aFilePathparts = filePath.Split('/');
			string fileName 		= aFilePathparts[aFilePathparts.Length-1];
			string fileDir 			= filePath.Replace(fileName, "");
				
			string[] thumbPath = parent.Request("GetThumbFilename(" +fileName+ ";" +fileDir+ ")");
			
			if (thumbPath != null)
			{
				string thumbPathFormatted = thumbPath[0].Replace("special://xbmc", "Q:\\").Replace("/", "\\");
				MessageBox.Show(thumbPathFormatted);
				return thumbPathFormatted;
			}
			else
				return null;
		}
		
		public bool FileExists(string filePath)
		{
			string[] response =	parent.Request("FileExists", filePath);
			return parent.CreateBoolRespose(response);
		}
		
		public MemoryStream FileDownload(string filePath)
		{
			MemoryStream msFile;
			string[] aDownloadData;
			byte[] aFileBytes;
			
			if (this.FileExists(filePath))
			{
                try
                {
					aDownloadData = parent.Request("FileDownload(" +filePath+ ";[bare])");
					
					if (aDownloadData != null)
					{
	                    aFileBytes 	= Convert.FromBase64String(aDownloadData[0]);
	                    msFile		= new MemoryStream(aFileBytes, 0, aFileBytes.Length);
	                    //msFile.Write(aFileBytes, 0, aFileBytes.Length);
					}
					else
						msFile = null;
                }
                catch (Exception e)
                {
                    parent.WriteToLog(DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss") + " - DEBUG: " + e.Message + " || " + filePath);
                	msFile = null;
				}
			}
			else
			{
				MessageBox.Show("File does not exist");
				msFile = null;
			}
			
			return msFile;
		}
    }
}
