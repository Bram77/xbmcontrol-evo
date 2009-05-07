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

    public XbmcDatabase (XbmcConnection o_connection) {
        o_Xbmc = o_connection;
    }

    public List<String> getArtists(String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE strArtist LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strArtist FROM artist" + s_condition + " ORDER BY strArtist");
    }

    public List<String> getArtists() { return getArtists(null); }

    public List<String> getArtistIds(String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE strArtist LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idArtist FROM artist" + s_condition + " ORDER BY strArtist");
    }

    public List<String> getArtistIds() { return getArtistIds(null); }

    public List<String> getAlbums(String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE strAlbum LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strAlbum FROM album" + s_condition + " ORDER BY strAlbum");
    }

    public List<String> getAlbums() { return getAlbums(null); }

    public List<String> getAlbumIds(String s_searchString) {
        String s_condition = (s_searchString == null) ? "" : " WHERE strAlbum LIKE '%%" + s_searchString + "%%'";
        return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idAlbum FROM album" + s_condition + " ORDER BY strAlbum");
    }

    public List<String> getAlbumIds() { return getAlbumIds(null); }

    public String getArtistId(String s_artist) {
        List<String> l_artistId = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idArtist FROM artist WHERE strArtist='" + s_artist + "'");
        return (l_artistId != null) ? l_artistId.get(0) : null;
    }

    public String getAlbumId(String s_artist, String s_album) {
        String s_conditions = " WHERE strAlbum='" + s_album + "'";
        if (s_artist != null) s_conditions += " AND idArtist='" + getArtistId(s_artist) + "'";
        List<String> l_artistId = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idAlbum FROM album" + s_conditions);
        return (l_artistId != null) ? l_artistId.get(0) : null;
    }

    public String getAlbumId(String s_album) { return getAlbumId(null, s_album); }

    public String getPathById(String s_pathId) {
        List<String> l_path = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strPath FROM path WHERE idPath='" + s_pathId + "'");
        return (l_path != null) ? l_path.get(0) : null;
    }
    
    public String getAlbumPath(String s_albumId) {
        List<String> l_pathId = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE idAlbum='" + s_albumId + "'");
        return (l_pathId != null) ? getPathById(l_pathId.get(0)) : null;
    }

    public String getSongPath(String s_songId) {
        List<String> l_pathId = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE idSong='" + s_songId + "'");
        return (l_pathId != null) ? getPathById(l_pathId.get(0)) : null;
    }
    
    public List<String> getAlbumsByArtist(String s_artist) { return getAlbumsByArtistId(getArtistId(s_artist)); }
    public List<String> getAlbumsByArtistId(String s_artistId) { return (s_artistId == null) ? null : o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strAlbum FROM album WHERE idArtist='" + s_artistId + "' ORDER BY strAlbum"); }
    public List<String> getAlbumIdsByArtist(String s_artist) { return getAlbumIdsByArtistId(getArtistId(s_artist)); }
    public List<String> getAlbumIdsByArtistId(String s_artistId) { return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idAlbum FROM album WHERE idArtist='" + s_artistId + "' ORDER BY strAlbum"); }
    public List<String> getTitlesByAlbum(String s_artist, String s_album) { return getTitlesByAlbumId(getAlbumId(s_artist, s_album)); }
    public List<String> getTitlesByAlbum(String s_album) { return getTitlesByAlbum(null, s_album); }
    public List<String> getTitlesByAlbumId(String s_albumId) { return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strTitle FROM song WHERE idAlbum='" + s_albumId + "' ORDER BY iTrack"); }
    public List<String> getPathsByAlbum(String s_artist, String s_album) { return getPathsByAlbumId(getAlbumId(s_artist, s_album)); }
    public List<String> getPathsByAlbum(String s_album) { return getPathsByAlbum(null, s_album); }

    public List<String> getPathsByAlbumId(String s_albumId)
    {
        List<String> l_path         = new ArrayList<String> ();
        List<String> l_fileName     = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strFileName FROM song WHERE idAlbum='" + s_albumId + "' ORDER BY iTrack");

        for (int x = 0; x < l_fileName.size(); x++)
            l_path.add(getAlbumPath(s_albumId) + l_fileName.get(x));

        return l_path;
    }

    public List<String> getSearchSongTitles(String s_searchString) { return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strTitle FROM song WHERE strTitle LIKE '%%" + s_searchString+ "%%' ORDER BY strTitle"); }

    public List<String> getSearchSongPaths(String s_searchString)
    {
        List<String> l_songPaths     = new ArrayList<String> ();
        List<String> l_songPathIds   = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE strTitle LIKE '%%" + s_searchString + "%%' ORDER BY strTitle");
        List<String> l_fileNames     = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strFileName FROM song WHERE strTitle LIKE '%%" + s_searchString + "%%' ORDER BY strTitle");

        for (int x = 0; x < l_songPathIds.size(); x++)
            l_songPaths.set(x, o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strPath FROM path WHERE idPath='" + l_songPathIds.get(x) + "'") + l_fileNames.get(x));

        return l_songPaths;
    }

    public List<String> getTitlesByArtist(String s_artist) { return getTitlesByArtistId(getArtistId(s_artist)); }
    public List<String> getTitlesByArtistId(String s_artistId) { return o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strTitle FROM song WHERE idArtist='" + s_artistId + "' ORDER BY iTrack"); }
    public List<String> getPathsByArtist(String s_artist) { return getPathsByArtistId(getArtistId(s_artist)); }

    public List<String> getPathsByArtistId(String s_artistId)
    {
        List<String> l_paths         = new ArrayList<String> ();
        List<String> l_songPathIds   = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE idArtist='" + s_artistId + "'");
        List<String> l_fileNames     = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strFileName FROM song WHERE idArtist='" + s_artistId + "'");
        
        for (int x=0; x<l_fileNames.size(); x++)
            l_paths.set(x, getPathById(l_songPathIds.get(x)) + l_fileNames.get(x));

        return l_paths;
    }

    public String getPathBySongTitle(String s_artistAlbumId, String s_songTitle, Boolean b_artist)
    {
        String s_songPath           = null;
        String s_idField            = (b_artist) ? "idArtist" : "idAlbum";
        List<String> l_songPathIds   = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT idPath FROM song WHERE " + s_idField + "='" + s_artistAlbumId + "' AND strTitle='" + s_songTitle + "'");
        List<String> l_songFileNames = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strFileName FROM song WHERE " + s_idField + "='" + s_artistAlbumId + "' AND strTitle='" + s_songTitle + "'");
        List<String> l_songPaths     = o_Xbmc.Request.send("QueryMusicDatabase", "SELECT strPath FROM path WHERE idPath='" + l_songPathIds.get(0) + "'");

        if (l_songPaths != null)
            s_songPath = l_songPaths.get(0) + l_songFileNames.get(0);

        return s_songPath;
    }

    public String getPathBySongTitle(String s_artistAlbumId, String s_songTitle) { return getPathBySongTitle(s_artistAlbumId, s_songTitle, false); }
}
