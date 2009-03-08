// ------------------------------------------------------------------------
//    XBMControl - A compact remote controller for XBMC (.NET 3.5)
//    Copyright (C) 2008  Bram van Oploo (bramvano@gmail.com)
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
using System.Drawing;
using System.Net;
//using System.Windows.Forms;

namespace XBMC
{
    public class XBMC_NowPlaying
    {
        XBMC_Communicator parent = null;
        private string[,] maNowPlayingInfo = null;
        //private string error = null;

        public XBMC_NowPlaying(XBMC_Communicator p)
        {
            parent = p;
        }

        public string Get(string field, bool refresh)
        {
            string returnValue = null;

            if (refresh)
            {
                string[] aNowPlayingTemp = parent.Request("GetCurrentlyPlaying");

                if (aNowPlayingTemp != null)
                {
                    maNowPlayingInfo = new string[aNowPlayingTemp.Length, 2];
                    for (int x = 0; x < aNowPlayingTemp.Length; x++)
                    {
                        int splitIndex = aNowPlayingTemp[x].IndexOf(':') + 1;

                        if (splitIndex > 2)
                        {
                            maNowPlayingInfo[x, 0] = aNowPlayingTemp[x].Substring(0, splitIndex - 1).Replace(" ", "").ToLower();
                            maNowPlayingInfo[x, 1] = aNowPlayingTemp[x].Substring(splitIndex, aNowPlayingTemp[x].Length - splitIndex);
                            
                            if (maNowPlayingInfo[x, 0] == field)
                                returnValue = this.maNowPlayingInfo[x, 1];
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < this.maNowPlayingInfo.GetLength(0); x++)
                {
                    if (this.maNowPlayingInfo[x, 0] == field)
                        returnValue = this.maNowPlayingInfo[x, 1];
                }
            }

            return returnValue;
        }

        public string Get(string field)
        {
            return this.Get(field, false);
        }

        public Image GetCoverArt()
        {
            MemoryStream stream = null;
            Image thumbnail = null;
            WebClient client = new WebClient();
            Uri xbmcThumbUri = new Uri("http://" + parent.GetIp() + "/thumb.jpg");
            parent.Request("GetCurrentlyPlaying", "q:\\web\\thumb.jpg");

            try
            {
                stream = new MemoryStream(client.DownloadData(xbmcThumbUri));
                thumbnail = new Bitmap(Image.FromStream(stream));
            }
            catch //(Exception e)
            {
                //string error = e.Message;
            }
            finally
            {
                client.Dispose();
            }

            return thumbnail;
        }

        public string GetMediaType()
        {
            return this.Get("type", true);
        }
    }
}
