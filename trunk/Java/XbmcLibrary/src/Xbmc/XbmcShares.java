/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Xbmc;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author Bram van Oploo
 */
public class XbmcShares {

    private XbmcConnection o_Xbmc           = null;
    private List<String> l_shareNames       = new ArrayList<String>();
    private List<String> l_sharePaths       = new ArrayList<String>();
    private List<String> l_mediaShares      = null;
    private List<String> l_directoryContent = null;
    private List<String> l_contentNames     = new ArrayList<String>();
    private List<String> l_contentPaths     = new ArrayList<String>();
    private List<String> l_fileTag          = null;
    private List<String> l_thumbPath        = null;

    public XbmcShares (XbmcConnection o_connection) {
        o_Xbmc = o_connection;
    }

    public List<String> getShares (String s_type, Boolean b_path) {
        l_mediaShares   = o_Xbmc.Request.send("GetShares(" + s_type + ")");
        l_shareNames    = null;
        l_sharePaths    = null;

        for (int x=0; x<l_mediaShares.size(); x++) {
            String[] sa_shareParts = l_mediaShares.get(x).split(":");
            
            if (sa_shareParts != null) {
                l_shareNames.add(sa_shareParts[0]);
                l_sharePaths.add(sa_shareParts[1]);
            }
        }

        return (b_path)? l_sharePaths : l_shareNames;
    }

    public List<String> getShares (String s_type) { return getShares(s_type, false); }

    public List<String> getDirectories (String s_directory, String s_mask) {
        s_mask              = (s_mask == null)? "" : ";" + s_mask;
        l_directoryContent  = o_Xbmc.Request.send("GetDirectory(" + s_directory + s_mask + ")");
        l_contentPaths      = null;

        for (int x=0; x<l_directoryContent.size(); x++) {
            if (l_directoryContent.get(x) != "Error:Not folder" && l_directoryContent.get(x) != "Error")
                l_contentPaths.add(l_directoryContent.get(x));
        }

        return l_contentPaths;
    }

    public List<String> getDirectories (String s_directory) { return getDirectories(s_directory, null); }

    public List<String> getDirectoryNames (String s_directory, String s_mask) {
        List<String> l_paths = getDirectories(s_directory, s_mask);
        l_contentNames       = null;

        for (int x=0; x<l_paths.size(); x++) {
            l_paths.set(x, l_paths.get(x).replace("\\", "/"));
            String[] sa_contentParts    = l_paths.get(x).split("/");
            Integer y                   = (sa_contentParts[sa_contentParts.length - 1] == "")? 2 : 1 ;
            l_contentNames.add(sa_contentParts[sa_contentParts.length -y]);
        }

        return l_contentNames;
    }

    public List<String> getDirectoryNames (String s_directory) { return getDirectoryNames(s_directory, null); }

    public String getThumbFilename (String s_filePath) {
        String[] sa_filePathparts   = s_filePath.split("/");
        l_thumbPath                 = o_Xbmc.Request.send("GetThumbFilename(" + sa_filePathparts[sa_filePathparts.length-1] + ";" + s_filePath.replace(sa_filePathparts[sa_filePathparts.length-1], "") + ")");
        return l_thumbPath.get(0).replace("special://xbmc", "Q:\\").replace("/", "\\");
    }

    public Boolean fileExists (String s_filePath) {
        List<String> l_response = o_Xbmc.Request.send("FileExists", s_filePath);
        return o_Xbmc.Request.getBooleanResponse(l_response.get(0));
    }

    public byte[] fileDownload(String s_filePath)
    {
        List<String> l_downloadData = null;
        byte[] ba_fileBytes         = null;

        if (fileExists(s_filePath)) {
            try {
                l_downloadData = o_Xbmc.Request.send("FileDownload(" + s_filePath + ";[bare])");
                if (l_downloadData != null) ba_fileBytes = new sun.misc.BASE64Decoder().decodeBuffer(l_downloadData.get(0));
            } catch (IOException e) {
                throw new RuntimeException("Error downloading file", e);
            }
        }

        return ba_fileBytes;
    }

     /*
    public String getTagFromFilename (String s_filepath, String s_field) {
            l_fileTag = o_Xbmc.Request.send("GetTagFromFilename(" + s_filepath + ")");

            if (l_fileTag != null) {
                string[,] aMusicTag = new string[aMusicTagTemp.Length, 2];

                for (int x = 0; x < aMusicTagTemp.Length; x++)
                {
                    int splitIndex = aMusicTagTemp[x].IndexOf(':') + 1;

                    if (splitIndex > 2)
                    {
                        aMusicTag[x, 0] = aMusicTagTemp[x].Substring(0, splitIndex - 1).Replace(" ", "").ToLower();
                        aMusicTag[x, 1] = aMusicTagTemp[x].Substring(splitIndex, aMusicTagTemp[x].Length - splitIndex);

                        if (aMusicTag[x, 0] == field)
                            return aMusicTag[x, 1];
                    }
                }
            }

            return "No data found";
    }
     */
}
