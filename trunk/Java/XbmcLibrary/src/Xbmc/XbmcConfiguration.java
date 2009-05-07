/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Xbmc;

/**
 *
 * @author Bram van Oploo
 */
public class XbmcConfiguration {

    private String s_ipAddress              = "127.0.0.1";
    private String s_port                   = "80";
    private String s_username               = "xbmc";
    private String s_password               = "xbmc";
    private Integer i_connectionTimeout     = 5000;
    private String s_apiPath                = "/xbmcCmds/xbmcHttp";
    private String s_proxyServer            = null;
    private String s_proxyPort              = null;

    public XbmcConfiguration () {
    }

    public void setIp (String s_xbmc_ip)                    { s_ipAddress = s_xbmc_ip; }
    public String getIp ()                                  { return s_ipAddress; }
    public void setPort (String s_xbmc_port)                { s_port = s_xbmc_port; }
    public String getPort ()                                { return s_port; }
    public void setUsername (String s_user)                 { s_username = s_user; }
    public String getUsername ()                            { return s_username; }
    public void setPassword (String s_pass)                 { s_password = s_pass; }
    public String getPassword ()                            { return s_password; }
    public void setConnectionTimeout (Integer i_timeout)    { i_connectionTimeout = i_timeout; }
    public Integer getConnectionTimeout ()                  { return i_connectionTimeout; }
    public void setApiPath (String s_path)                  { s_apiPath = s_path; }
    public String getApiPath ()                             { return s_apiPath; }
    public void setProxyServer (String s_proxy_server)      { s_proxyServer = s_proxy_server; }
    public String getProxyServer ()                         { return s_proxyServer; }
    public void setProxyPort (String s_proxy_port)          { s_proxyPort = s_proxy_port; }
    public String getProxyPort ()                           { return s_proxyPort; }
}
