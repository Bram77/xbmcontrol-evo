/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Xbmc;

import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author Bram van Oploo
 */
public class XbmcDatabase {

    private XbmcConnection o_Xbmc = null;

    public XbmcDatabase (XbmcConnection o_connection) { o_Xbmc = o_connection; }

//MUSIC

    public List<String> getArtists (String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE strArtist LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strArtist FROM artist" + s_condition + " ORDER BY strArtist");
    }

    public List<String> getArtistIds (String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE strArtist LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idArtist FROM artist" + s_condition + " ORDER BY strArtist");
    }

    public List<String> getAlbums (String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE strAlbum LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strAlbum FROM album" + s_condition + " ORDER BY strAlbum");
    }

    public List<String> getAlbumIds (String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE strAlbum LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idAlbum FROM album" + s_condition + " ORDER BY strAlbum");
    }

    public String getArtistId (String s_artist) {
        List<String> l_artistId = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idArtist FROM artist WHERE strArtist='" + s_artist + "'");
        return (l_artistId != null) ? l_artistId.get(0) : null;
    }

    public String getAlbumId (String s_artist, String s_album) {
        String s_conditions = " WHERE strAlbum='" + s_album + "'";
        if (s_artist != null) s_conditions += " AND idArtist='" + getArtistId(s_artist) + "'";
        List<String> l_artistId = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idAlbum FROM album" + s_conditions);
        return (l_artistId != null) ? l_artistId.get(0) : null;
    }

    public String getPathById (String s_pathId) {
        List<String> l_path = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strPath FROM path WHERE idPath='" + s_pathId + "'");
        return (l_path != null) ? l_path.get(0) : null;
    }
    
    public String getAlbumPath (String s_albumId) {
        List<String> l_pathId = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE idAlbum='" + s_albumId + "'");
        return (l_pathId != null) ? getPathById(l_pathId.get(0)) : null;
    }

    public String getSongPath (String s_songId) {
        List<String> l_pathId = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE idSong='" + s_songId + "'");
        return (l_pathId != null) ? getPathById(l_pathId.get(0)) : null;
    }
    
    public List<String> getAlbumsByArtistId (String s_artistId)      { return (s_artistId == null) ? null : o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strAlbum FROM album WHERE idArtist='" + s_artistId + "' ORDER BY strAlbum"); }
    public List<String> getAlbumIdsByArtistId (String s_artistId)    { return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idAlbum FROM album WHERE idArtist='" + s_artistId + "' ORDER BY strAlbum"); }
    public List<String> getTitlesByAlbumId (String s_albumId)        { return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strTitle FROM song WHERE idAlbum='" + s_albumId + "' ORDER BY iTrack"); }

    public List<String> getPathsByAlbumId (String s_albumId) {
        List<String> l_path         = new ArrayList<String> ();
        List<String> l_fileName     = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strFileName FROM song WHERE idAlbum='" + s_albumId + "' ORDER BY iTrack");

        for (int x = 0; x < l_fileName.size(); x++)
            l_path.add(getAlbumPath(s_albumId) + l_fileName.get(x));

        return l_path;
    }

    public List<String> getSearchSongTitles (String s_searchString) { return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strTitle FROM song WHERE strTitle LIKE '%%" + s_searchString+ "%%' ORDER BY strTitle"); }

    public List<String> getSearchSongPaths (String s_searchString) {
        List<String> l_songPaths     = new ArrayList<String> ();
        List<String> l_songPathIds   = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE strTitle LIKE '%%" + s_searchString + "%%' ORDER BY strTitle");
        List<String> l_fileNames     = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strFileName FROM song WHERE strTitle LIKE '%%" + s_searchString + "%%' ORDER BY strTitle");

        for (int x = 0; x < l_songPathIds.size(); x++)
            l_songPaths.set(x, o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strPath FROM path WHERE idPath='" + l_songPathIds.get(x) + "'") + l_fileNames.get(x));

        return l_songPaths;
    }

    public List<String> getTitlesByArtistId (String s_artistId) { return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strTitle FROM song WHERE idArtist='" + s_artistId + "' ORDER BY iTrack"); }

    public List<String> getPathsByArtistId (String s_artistId) {
        List<String> l_paths         = new ArrayList<String> ();
        List<String> l_songPathIds   = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE idArtist='" + s_artistId + "'");
        List<String> l_fileNames     = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strFileName FROM song WHERE idArtist='" + s_artistId + "'");
        
        for (int x=0; x<l_fileNames.size(); x++)
            l_paths.set(x, getPathById(l_songPathIds.get(x)) + l_fileNames.get(x));

        return l_paths;
    }

    public String getPathBySongTitle (String s_artistAlbumId, String s_songTitle, Boolean b_artist) {
        String s_songPath           = null;
        String s_idField            = (b_artist) ? "idArtist" : "idAlbum";
        List<String> l_songPathIds   = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE " + s_idField + "='" + s_artistAlbumId + "' AND strTitle='" + s_songTitle + "'");
        List<String> l_songFileNames = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strFileName FROM song WHERE " + s_idField + "='" + s_artistAlbumId + "' AND strTitle='" + s_songTitle + "'");
        List<String> l_songPaths     = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strPath FROM path WHERE idPath='" + l_songPathIds.get(0) + "'");

        if (l_songPaths != null)
            s_songPath = l_songPaths.get(0) + l_songFileNames.get(0);

        return s_songPath;
    }

//VIDEO

    public String getVideoPath (String s_videoTitle) {
        String s_condition      = " WHERE C00 LIKE '%%" + s_videoTitle + "%%'";
        List<String> l_result   = o_Xbmc.Request.send("QueryVideoDatabase", "SELECT strpath FROM movieview " + s_condition);
        return (l_result == null || s_videoTitle == null)? null : l_result.get(0) + "VIDEO_TS.IFO";
    }

    public List<String> getVideoNames (String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE C00 LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryVideoDatabase", "SELECT C00 FROM movie " + s_condition + " ORDER BY C00");
    }

    public List<String> getVideoIds (String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE C00 LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryVideoDatabase", "SELECT idMovie FROM movie" + s_condition + " ORDER BY C00");
    }

    public List<String> getVideoYears (String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE C00 LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryVideoDatabase", "SELECT C07 FROM movie " + s_condition + " ORDER BY C00");
    }

    public List<String> getVideoInfo (String s_videoId) {
        String s_condition = (s_videoId == null) ? "" : " WHERE C09 LIKE '%%" + s_videoId + "%%'";
        return o_Xbmc.Request.send("QueryVideoDatabase", "SELECT * FROM movie " + s_condition + " ORDER BY C00");
    }

}
