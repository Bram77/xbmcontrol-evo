/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Xbmc;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLEncoder;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.util.List;
import java.util.ArrayList;
import java.net.Authenticator;
import java.net.MalformedURLException;
import java.net.PasswordAuthentication;
import java.net.ProtocolException;
import java.util.Properties;

/**
 *
 * @author Bram van Oploo
 */
public class XbmcRequest {

    private String s_requestUrl                 = null;
    private XbmcConnection o_Xbmc               = null;
    private List<String> l_requestResult        = null;
    private URL u_requestUrl                    = null;
    private HttpURLConnection http_connection   = null;
    private BufferedReader br_responseReader    = null;
    private String s_response                   = null;
    private String[] sa_response                = null;

    public XbmcRequest (XbmcConnection o_connection) {
        o_Xbmc = o_connection;
    }

    private String getUrl (String s_command, String s_parameter) {
        String s_encodedCommand     = "";
        String s_encodedParameter   = "";
        String s_port               = (o_Xbmc.Configuration.getPort() == "")? "" : ":" + o_Xbmc.Configuration.getPort();

        try {
            s_encodedCommand    = "?command=" + URLEncoder.encode(s_command, "UTF-8");
            s_encodedParameter  = (s_parameter == null)? "" : "&parameter=" + URLEncoder.encode(s_parameter, "UTF-8");
        } catch (UnsupportedEncodingException e) {
            throw new RuntimeException ("UTF-8 url encoding not supported", e);
        }

        s_requestUrl = "http://" + o_Xbmc.Configuration.getIp() + s_port + o_Xbmc.Configuration.getApiPath() + s_encodedCommand + s_encodedParameter;

        return s_requestUrl;
    }

    public List<String> send (String s_command, String s_parameter) {
        l_requestResult         = null;
        final String s_username = o_Xbmc.Configuration.getUsername();
        final String s_password = o_Xbmc.Configuration.getPassword();
        
        try {
            Authenticator.setDefault (
                new Authenticator() {
                    @Override
                    protected PasswordAuthentication getPasswordAuthentication() {
                    return new PasswordAuthentication (s_username, s_password.toCharArray());
                }
            });

            Properties p_systemProperties = System.getProperties();
            p_systemProperties.setProperty("http.proxyHost", o_Xbmc.Configuration.getProxyServer());
            p_systemProperties.setProperty("http.proxyPort", o_Xbmc.Configuration.getProxyPort());

            u_requestUrl      = new URL(this.getUrl(s_command, s_parameter));
            http_connection   = (HttpURLConnection)u_requestUrl.openConnection();
            http_connection.setRequestMethod("GET");
            http_connection.setDoOutput(true);
            http_connection.setReadTimeout(o_Xbmc.Configuration.getConnectionTimeout());
            http_connection.connect();
            
            br_responseReader    = new BufferedReader(new InputStreamReader(http_connection.getInputStream()));
            s_response           = br_responseReader.toString().replace("\n", "").replace("<html>", "").replace("</html>", "").replace("</field>", "");
            sa_response          = (s_response.lastIndexOf("<field>") != -1)? s_response.split("<field>") : s_response.split("<li>");
            l_requestResult      = new ArrayList<String>();

            for (int x=0; x<sa_response.length; x++ ) l_requestResult.add(sa_response[x]);
            if (l_requestResult.size() > 0) l_requestResult.remove(0);
            br_responseReader.close();
        } catch (MalformedURLException e) {
            throw new RuntimeException ("Invalid URL", e);
        } catch (ProtocolException e) {
            throw new RuntimeException ("Protocol exception", e);
        } catch (IOException e) {
            throw new RuntimeException ("Could not read response", e);
        } finally {
            http_connection.disconnect();
            u_requestUrl        = null;
            br_responseReader   = null;
            s_response          = null;
            sa_response         = null;
            http_connection     = null;
        }

        return l_requestResult;
    }

    public List<String> send (String command) {
       return send (command, null);
    }

    public String getLastRequestUrl () {
        return s_requestUrl;
    }

    public Boolean getBooleanResponse(String s_response)
    {
        if (s_response == null || s_response == "")
            return false;
        else
            return (s_response == "OK" || s_response == "True" || s_response == "On")? true : false ;
    }
}
