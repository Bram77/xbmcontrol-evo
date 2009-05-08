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
public class XbmcVideo {

    private XbmcConnection o_Xbmc;

    public XbmcVideo (XbmcConnection o_connection)              { o_Xbmc = o_connection; }
    public List<String> getShareNames ()                        { return o_Xbmc.Shares.getShares("video", false); }
    public List<String> getSharePaths ()                        { return o_Xbmc.Shares.getShares("video", true); }
    public String getVideoPath (String s_videoTitle)            { return o_Xbmc.Database.getVideoPath(s_videoTitle); }
    public List<String> getVideoNames (String s_searchString)   { return o_Xbmc.Database.getVideoNames(s_searchString); }
    public List<String> getVideoNames ()                        { return getVideoNames(null); }
    public List<String> getVideoIds (String s_searchString)     { return o_Xbmc.Database.getVideoIds(s_searchString); }
    public List<String> getVideoIds ()                          { return getVideoIds(null); }
    public List<String> getVideoYears (String s_searchString)   { return o_Xbmc.Database.getVideoYears(s_searchString); }
    public List<String> getVideoYears ()                        { return getVideoYears(null); }
    public List<String> getVideoInfo (String s_videoId)         { return o_Xbmc.Database.getVideoInfo(s_videoId); }
    public List<String> getVideoInfo ()                         { return getVideoInfo(null); }
}
