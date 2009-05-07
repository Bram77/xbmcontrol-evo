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
public class XbmcNowPlaying {

    private XbmcConnection o_Xbmc   = null;
    private List<String> l_data     = null;

    public XbmcNowPlaying (XbmcConnection o_connection) {
        o_Xbmc = o_connection;
    }

    private String getData (String s_field) {
        l_data                  = o_Xbmc.Request.send("GetCurrentlyPlaying");
        int i_splitIndex        = 0;
        String s_returnValue    = null;

        for (int x=0; x<l_data.size(); x++) {
            i_splitIndex = l_data.get(x).indexOf(":")+1;

            if (i_splitIndex > 2) {
                if (l_data.get(x).substring(0, i_splitIndex - 1).replace(" ", "").toLowerCase() == s_field)
                    s_returnValue = l_data.get(x).substring(i_splitIndex, (l_data.get(x).length()-i_splitIndex));
            }
        }

        return s_returnValue;
    }

    public byte[] GetCoverArt() {
        List<String> l_result = o_Xbmc.Request.send("GetCurrentlyPlaying", "q:\\web\\thumb.jpg");
        return (!o_Xbmc.Request.getBooleanResponse(l_result.get(0)))? null : o_Xbmc.Shares.fileDownload("q:\\web\\thumb.jpg");
    }

    public String getMediaType ()   { return getData("type"); }
    public String getArtist ()      { return getData("artist"); }
    public String getAlbum ()       { return getData("album"); }
    public String getYear ()        { return getData("year"); }
    public String getGenre ()       { return getData("genre"); }
    public String getBitrate ()     { return getData("bitrate"); }
    public String getDuration ()    { return getData("duration"); }
    public String getTime ()        { return getData("time"); }
    public String getFilename ()    { return getData("filename"); }
}
