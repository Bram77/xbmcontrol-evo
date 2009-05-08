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
public class XbmcMusic {

    private XbmcConnection o_Xbmc;

    public XbmcMusic (XbmcConnection o_connection)                                                  { o_Xbmc = o_connection; }
    public List<String> getShareNames ()                                                            { return o_Xbmc.Shares.getShares("music", false); }
    public List<String> getSharePaths ()                                                            { return o_Xbmc.Shares.getShares("music", true); }
    public List<String> getArtists (String s_searchString)                                          { return o_Xbmc.Database.getArtists(s_searchString); }
    public List<String> getArtists ()                                                               { return getArtists(null); }
    public List<String> getArtistIds (String s_searchString)                                        { return o_Xbmc.Database.getArtistIds(s_searchString); }
    public List<String> getArtistIds ()                                                             { return getArtistIds(null); }
    public List<String> getAlbums (String s_searchString)                                           { return o_Xbmc.Database.getAlbums(s_searchString); }
    public List<String> getAlbums ()                                                                { return getAlbums(null); }
    public List<String> getAlbumIds (String s_searchString)                                         { return o_Xbmc.Database.getAlbumIds(s_searchString); }
    public List<String> getAlbumIds ()                                                              { return getAlbumIds(null); }
    public String getArtistId (String s_artist)                                                     { return o_Xbmc.Database.getArtistId(s_artist); }
    public String getAlbumId (String s_artist, String s_album)                                      { return o_Xbmc.Database.getAlbumId(s_artist, s_album); }
    public String getAlbumId (String s_album)                                                       { return getAlbumId(null, s_album); }
    public String getPathById (String s_pathId)                                                     { return o_Xbmc.Database.getPathById(s_pathId); }
    public String getAlbumPath (String s_albumId)                                                   { return o_Xbmc.Database.getAlbumPath(s_albumId); }
    public String getSongPath (String s_songId)                                                     { return o_Xbmc.Database.getSongPath(s_songId); }
    public List<String> getAlbumsByArtist (String s_artist)                                         { return o_Xbmc.Database.getAlbumsByArtistId(o_Xbmc.Database.getArtistId(s_artist)); }
    public List<String> getAlbumsByArtistId (String s_artistId)                                     { return o_Xbmc.Database.getAlbumsByArtistId(s_artistId); }
    public List<String> getAlbumIdsByArtist (String s_artist)                                       { return o_Xbmc.Database.getAlbumIdsByArtistId(getArtistId(s_artist)); }
    public List<String> getAlbumIdsByArtistId (String s_artistId)                                   { return o_Xbmc.Database.getAlbumIdsByArtistId(s_artistId); }
    public List<String> getTitlesByAlbum (String s_artist, String s_album)                          { return o_Xbmc.Database.getTitlesByAlbumId(o_Xbmc.Database.getAlbumId(s_artist, s_album)); }
    public List<String> getTitlesByAlbum (String s_album)                                           { return getTitlesByAlbum(null, s_album); }
    public List<String> getTitlesByAlbumId (String s_albumId)                                       { return o_Xbmc.Database.getTitlesByAlbumId(s_albumId); }
    public List<String> getPathsByAlbum (String s_artist, String s_album)                           { return o_Xbmc.Database.getPathsByAlbumId(o_Xbmc.Database.getAlbumId(s_artist, s_album)); }
    public List<String> getPathsByAlbum (String s_album)                                            { return getPathsByAlbum(null, s_album); }
    public List<String> getPathsByAlbumId (String s_albumId)                                        { return o_Xbmc.Database.getPathsByAlbumId(s_albumId); }
    public List<String> getSearchSongTitles (String s_searchString)                                 { return o_Xbmc.Database.getSearchSongTitles(s_searchString); }
    public List<String> getSearchSongPaths (String s_searchString)                                  { return o_Xbmc.Database.getSearchSongPaths(s_searchString); }
    public List<String> getTitlesByArtist (String s_artist)                                         { return o_Xbmc.Database.getTitlesByArtistId(o_Xbmc.Database.getArtistId(s_artist)); }
    public List<String> getTitlesByArtistId (String s_artistId)                                     { return o_Xbmc.Database.getTitlesByArtistId(s_artistId); }
    public List<String> getPathsByArtist (String s_artist)                                          { return o_Xbmc.Database.getPathsByArtistId(o_Xbmc.Database.getArtistId(s_artist)); }
    public List<String> getPathsByArtistId (String s_artistId)                                      { return o_Xbmc.Database.getPathsByArtistId(s_artistId); }
    public String getPathBySongTitle (String s_artistAlbumId, String s_songTitle, Boolean b_artist) { return o_Xbmc.Database.getPathBySongTitle(s_artistAlbumId, s_songTitle, b_artist); }
    public String getPathBySongTitle (String s_artistAlbumId, String s_songTitle)                   { return getPathBySongTitle(s_artistAlbumId, s_songTitle, false); }
}
