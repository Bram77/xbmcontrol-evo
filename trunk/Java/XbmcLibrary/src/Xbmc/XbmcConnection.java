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
public class XbmcConnection {

    public XbmcConfiguration Configuration  = null;
    public XbmcRequest Request              = null;
    public XbmcControls Controls            = null;
    public XbmcDatabase Database            = null;
    public XbmcShares Shares                = null;
    public XbmcPlaylist Playlist            = null;
    public XbmcMusic Music                  = null;
    public XbmcVideo Video                  = null;

    private Boolean b_connected             = false;
    private Boolean b_webServerEnabled      = false;

    public XbmcConnection () {
        Configuration   = new XbmcConfiguration ();
        Request         = new XbmcRequest (this);
        Controls        = new XbmcControls (this);
        Database        = new XbmcDatabase(this);
        Shares          = new XbmcShares(this);
        Playlist        = new XbmcPlaylist(this);
        Music           = new XbmcMusic(this);
        Video           = new XbmcVideo(this);
    }

    public Boolean isConnected () { return b_connected; }

    public Boolean setResponseFormat () {
        List<String> l_response = Request.send ("SetResponseFormat");
        b_connected = (l_response == null)? false : Request.getBooleanResponse(l_response.get(0));
        
        return b_connected;
    }

    public Boolean isWebserverEnabled () {
        List<String> l_response = Request.send ("WebServerStatus");
        b_webServerEnabled = (l_response == null)? false : Request.getBooleanResponse(l_response.get(0));
        
        return b_webServerEnabled;
    }

    public Boolean connect () {
        setResponseFormat();
        isWebserverEnabled();

        return (!b_connected || !b_webServerEnabled)? false : true;
    }
}
