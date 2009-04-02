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
using System.Windows.Forms;

namespace XBMC
{
    public class XBMC_Playlist
    {
        XBMC_Communicator parent;
        private string[] aCurrentPlaylist = null;

        public XBMC_Playlist(XBMC_Communicator p)
        {
            parent = p;
        }

        public string[] Get(string type, bool parse)
        {
            string[] aPlaylistTemp = parent.Request("GetPlaylistContents(" +type+ ")");

            if (parse)
            {
                if (aPlaylistTemp != null)
                {
                    aCurrentPlaylist = new string[aPlaylistTemp.Length];
                    for (int x = 0; x < aPlaylistTemp.Length; x++)
                    {
                        int i = aPlaylistTemp[x].LastIndexOf(".");
                        if (i > 1)
                        {
                            string extension = aPlaylistTemp[x].Substring(i, aPlaylistTemp[x].Length - i);
                                aPlaylistTemp[x] = aPlaylistTemp[x].Replace("\\", "/");
                            string[] aPlaylistEntry = aPlaylistTemp[x].Split('/');
                            string playlistEntry = aPlaylistEntry[aPlaylistEntry.Length - 1].Replace(extension, "");
                            aCurrentPlaylist[x] = playlistEntry;
                        }
                        else
                            aCurrentPlaylist[x] = "";
					}
					
					return aCurrentPlaylist;
                }
				else
					return null;
            }
            else
                return aPlaylistTemp;
        }

        public bool PlaySong(int position)
        {
            string[] response = parent.Request("SetPlaylistSong(" + position.ToString() + ")");
			return parent.CreateBoolRespose(response);
        }

        public bool Remove(int position)
        {
            string[] response = parent.Request("RemoveFromPlaylist(" + position.ToString() + ")");
			return parent.CreateBoolRespose(response);
        }

        public string GetCurrentPlaylistType()
        {
            string[] response = parent.Request("GetCurrentPlaylist()");
            return (response == null) ? null : response[0];
        }

        public bool Clear()
        {
            string[] response = parent.Request("ClearPlayList()");
			return parent.CreateBoolRespose(response);
        }

        public bool AddDirectoryContent(string folderPath, string mask, bool recursive)
        {
            string p = "";
            string m = "";
            string r = "";
			
            if (mask != null)
            {
				p = ";0";
                m = ";[" + mask + "]";
                r = (recursive) ? ";1" : ";0";
            }

            string[] response = parent.Request("AddToPlayList(" + folderPath + p + m + r + ")");
			return parent.CreateBoolRespose(response);
        }

        public bool AddDirectoryContent(string folderPath, string mask)
        {
            return this.AddDirectoryContent(folderPath, mask, false);
        }

        public bool AddFilesToPlaylist(string filePath)
        {
            return this.AddDirectoryContent(filePath, null);
        }
		
		public int GetLength()
		{
			string[] length = parent.Request("GetPlaylistLength(0)");
			return (length == null)? 0 : Convert.ToInt32(length[0]);
		}

        public bool SetSong(int position)
        {
            string[] response = parent.Request("SetPlaylistSong(" + position.ToString() + ")");
			return parent.CreateBoolRespose(response);
        }

        public bool Set(string type)
        {
            string[] response = parent.Request("SetCurrentPlaylist(" + type + ")");
			return parent.CreateBoolRespose(response);
        }
    }
}
