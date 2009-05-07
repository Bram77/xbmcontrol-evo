/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Xbmc;

import java.util.List;

/**
 *
 * @author Bram
 */
public class XbmcPlaylist {

    private XbmcConnection o_Xbmc               = null;
    private List<String> l_currentContents      = null;
    private List<String> l_currentContentNames  = null;
    private int i_contentsRequestCount          = 0;
    private int i_contentnamesRequestCount      = 0;

    public XbmcPlaylist (XbmcConnection o_connection) {
        o_Xbmc = o_connection;
    }

    public List<String> getContents (String s_type) {
        l_currentContents       = o_Xbmc.Request.send("GetPlaylistContents(" + s_type + ")");
        l_currentContentNames   = null;
        i_contentsRequestCount++;
        
        for (int x=0; x<l_currentContents.size(); x++) {
            int i = l_currentContents.get(x).lastIndexOf(".");
            if (i>1) {
                String s_extension          = l_currentContents.get(x).substring(i, (l_currentContents.get(x).length()-i));
                l_currentContents.set(x, l_currentContents.get(x).replace("\\", "/"));
                String[] sa_paylistEntry    = l_currentContents.get(x).split("/");
                String s_playlistEntry      = sa_paylistEntry[sa_paylistEntry.length-1].replace(s_extension, "");
                l_currentContentNames.add(s_playlistEntry);
            } else {
                l_currentContentNames.add("");
            }
        }

        return l_currentContents;
    }

    public List<String> getContentNames (String s_type) {
        i_contentnamesRequestCount++;
        if (i_contentsRequestCount != i_contentnamesRequestCount) getContents(s_type);

        return l_currentContentNames;
    }

    public Boolean playSong (int i_position) {
        List<String> l_response = o_Xbmc.Request.send("SetPlaylistSong(" + Integer.toString(i_position) + ")");
        return o_Xbmc.Request.getBooleanResponse(l_response.get(0));
    }

    public Boolean remove (int i_position) {
        List<String> l_response = o_Xbmc.Request.send("RemoveFromPlaylist(" + Integer.toString(i_position) + ")");
        return o_Xbmc.Request.getBooleanResponse(l_response.get(0));
    }

    public String getCurrentPlaylistType () {
        List<String> l_response = o_Xbmc.Request.send("GetCurrentPlaylist()");
        return (l_response == null)? null : l_response.get(0);
    }

    public Boolean clear () {
        List<String> l_response = o_Xbmc.Request.send("ClearPlayList()");
        return o_Xbmc.Request.getBooleanResponse(l_response.get(0));
    }

    public Boolean addDirectoryContent(String s_directoryPath, String s_mask, Boolean b_recursive) {
        String s_p = "";
        String s_m = "";
        String s_r = "";

        if (s_mask != null) {
            s_p = ";0";
            s_m = ";[" + s_mask + "]";
            s_r = (b_recursive)? ";1" : ";0";
        }

        List<String> l_response = o_Xbmc.Request.send("AddToPlayList(" + s_directoryPath + s_p + s_m + s_r + ")");
        return o_Xbmc.Request.getBooleanResponse(l_response.get(0));
    }

    public Boolean addDirectoryContent(String s_directoryPath, String s_mask)   { return addDirectoryContent(s_directoryPath, s_mask, false); }
    public Boolean addFilesToPlaylist(String s_filePath)                        { return addDirectoryContent(s_filePath, null, false); }

    public int getLength (String s_type) {
        List<String> l_playlistLength = o_Xbmc.Request.send("GetPlaylistLength(" + s_type + ")");
        return (l_playlistLength == null)? 0 : Integer.getInteger(l_playlistLength.get(0));
    }

    public Boolean setSong (int i_position) {
        List<String> l_response = o_Xbmc.Request.send("SetPlaylistSong(" + Integer.toString(i_position) + ")");
        return o_Xbmc.Request.getBooleanResponse(l_response.get(0));
    }

    public Boolean setType (String s_type) {
        List<String> l_response = o_Xbmc.Request.send("SetCurrentPlaylist(" + s_type + ")");
        return o_Xbmc.Request.getBooleanResponse(l_response.get(0));
    }
}
