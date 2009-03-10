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
using System.Drawing;
using System.IO;
//using System.Windows.Forms;

namespace XBMC
{
    public class XBMC_Controls
    {
        XBMC_Communicator parent = null;

        public XBMC_Controls(XBMC_Communicator p)
        {
            parent = p;
        }

        public bool Play()
        {
            string[] response = parent.Request("ExecBuiltIn", "PlayerControl(Play)");
			return parent.CreateBoolRespose(response);
        }

        public bool PlayFile(string filename)
        {
            string[] response = parent.Request("PlayFile(" + filename + ")");
			return parent.CreateBoolRespose(response);
        }

        public bool PlayMedia(string media)
        {
            string[] response = parent.Request("ExecBuiltIn", "PlayMedia(" + media + ")");
			return parent.CreateBoolRespose(response);
        }

        public bool Stop()
        {
            string[] response = parent.Request("ExecBuiltIn", "PlayerControl(Stop)");
			return parent.CreateBoolRespose(response);
        }

        public bool Next()
        {
            string[] response = parent.Request("ExecBuiltIn", "PlayerControl(Next)");
			return parent.CreateBoolRespose(response);
        }

        public bool PlayListNext()
        {
            string[] response = parent.Request("PlayListNext");
			return parent.CreateBoolRespose(response);
        }

        public bool Previous()
        {
            string[] response = parent.Request("ExecBuiltIn", "PlayerControl(Previous)");
			return parent.CreateBoolRespose(response);
        }

        public bool ToggleShuffle()
        {
            string[] response = parent.Request("ExecBuiltIn", "PlayerControl(Random)");
			return parent.CreateBoolRespose(response);
        }

        public bool TogglePartymode()
        {
            string[] response = parent.Request("ExecBuiltIn", "PlayerControl(Partymode(music))");
			return parent.CreateBoolRespose(response);
        }

        public bool Repeat(bool enable)
        {
            string mode = (enable) ? "RepeatAll" : "RepeatOff";
            string[] response = parent.Request("ExecBuiltIn", "PlayerControl(" + mode + ")");
			return parent.CreateBoolRespose(response);
        }
		
		public bool ToggleRepeatModes()
		{
			string[] response = parent.Request("ExecBuiltIn", "PlayerControl(Repeat)");
			return parent.CreateBoolRespose(response);
		}

        public bool LastFmLove()
        {
            string[] response = parent.Request("ExecBuiltIn", "LastFM.Love(false)");
			return parent.CreateBoolRespose(response);
        }

        public bool LastFmHate()
        {
            string[] response = parent.Request("ExecBuiltIn", "LastFM.Ban(false)");
			return parent.CreateBoolRespose(response);
        }

        public bool ToggleMute()
        {
            string[] response = parent.Request("ExecBuiltIn", "Mute");
			return parent.CreateBoolRespose(response);
        }

        public bool SetVolume(int percentage)
        {
            string[] response = parent.Request("ExecBuiltIn", "SetVolume(" + Convert.ToString(percentage) + ")");
			return parent.CreateBoolRespose(response);
        }

        public bool SeekPercentage(int percentage)
        {
            string[] response = parent.Request("SeekPercentage", Convert.ToString(percentage));
			return parent.CreateBoolRespose(response);
        }

        public bool Reboot()
        {
            string[] response = parent.Request("ExecBuiltIn", "Reboot");
			return parent.CreateBoolRespose(response);
        }

        public bool Shutdown()
        {
            string[] response = parent.Request("ExecBuiltIn", "Shutdown");
			return parent.CreateBoolRespose(response);
        }

        public bool Restart()
        {
            string[] response = parent.Request("ExecBuiltIn", "RestartApp");
			return parent.CreateBoolRespose(response);
        }

        public string GetGuiDescription(string field)
        {
            string returnValue = null;
            string[] aGuiDescription = parent.Request("GetGUIDescription");

            for (int x = 0; x < aGuiDescription.Length; x++)
            {
                int splitIndex = aGuiDescription[x].IndexOf(':') + 1;
                if (splitIndex > 1)
                {
                    string resultField = aGuiDescription[x].Substring(0, splitIndex - 1).Replace(" ", "").ToLower();
                    if (resultField == field) returnValue = aGuiDescription[x].Substring(splitIndex, aGuiDescription[x].Length - splitIndex);
                }
            }

            return returnValue;
        }

        public string GetScreenshotBase64()
        {
            string[] base64screenshot = parent.Request("takescreenshot", "screenshot.png;false;0;" + this.GetGuiDescription("width") + ";" + this.GetGuiDescription("height") + ";75;true;");
            return (base64screenshot == null) ? null : base64screenshot[0];
        }

        public Image Base64StringToImage(string base64String)
        {
            Bitmap file = null;
            byte[] bytes = Convert.FromBase64String(base64String);
            MemoryStream stream = new MemoryStream(bytes);

            if (base64String != null && base64String != "")
                file = new Bitmap(Image.FromStream(stream));

            return file;
        }

        public Image GetScreenshot()
        {
            Image screenshot = null;
            string base64ImageString = this.GetScreenshotBase64();

            if (base64ImageString != null)
                screenshot = this.Base64StringToImage(base64ImageString);

            return screenshot;
        }

        public void UpdateLibrary(string library)
        {
            if(library == "music" || library == "video")
                parent.Request("ExecBuiltIn", "updatelibrary(" + library + ")");
        }

        public bool SetResponseFormat()
        {
            if (parent.GetIp() != null && parent.GetIp() != "")
			{
                string[] response = parent.Request("SetResponseFormat", null, parent.GetIp());
            	return parent.CreateBoolRespose(response);
			}
			else
				return false;
        }
    }
}
