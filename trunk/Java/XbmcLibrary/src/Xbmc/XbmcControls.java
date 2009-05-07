/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Xbmc;

import java.util.List;

/**
 *
 * @author Bram van Oploo
 */
public class XbmcControls {
    
    private XbmcConnection o_Xbmc = null;

    public XbmcControls (XbmcConnection o_connection) {
        o_Xbmc = o_connection;
    }

    public Boolean play() {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "PlayerControl(Play)");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean playFile(String s_filename)
    {
        List<String> l_response = o_Xbmc.Request.send ("PlayFile(" + s_filename + ")");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean playMedia(String s_media)
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "PlayMedia(" + s_media + ")");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean stop()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "PlayerControl(Stop)");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean next()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "PlayerControl(Next)");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean playListNext()
    {
        List<String> l_response = o_Xbmc.Request.send ("PlayListNext");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean previous()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "PlayerControl(Previous)");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean toggleShuffle()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "PlayerControl(Random)");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean togglePartymode()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "PlayerControl(Partymode(music))");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean repeat(Boolean b_enable)
    {
        String s_mode = (b_enable) ? "RepeatAll" : "RepeatOff";
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "PlayerControl(" + s_mode + ")");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean toggleRepeatModes()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "PlayerControl(Repeat)");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean lastFmLove()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "LastFM.Love(false)");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean lastFmHate()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "LastFM.Ban(false)");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean toggleMute()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "Mute");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean setVolume(int i_percentage)
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "SetVolume(" + Integer.toString(i_percentage) + ")");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean seekPercentage(int i_percentage)
    {
        List<String> l_response = o_Xbmc.Request.send ("SeekPercentage", Integer.toString(i_percentage));
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean reboot()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "Reboot");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean shutdown()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "Shutdown");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }

    public Boolean restart()
    {
        List<String> l_response = o_Xbmc.Request.send ("ExecBuiltIn", "RestartApp");
        return o_Xbmc.Request.getBooleanResponse (l_response.get(0));
    }
}
