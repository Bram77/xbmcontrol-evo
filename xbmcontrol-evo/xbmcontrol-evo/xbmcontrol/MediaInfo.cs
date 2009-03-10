
using System;
using System.IO;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class MediaInfo
	{
		private MainWindow _parent;
		private Gdk.Color white = new Gdk.Color();
		
		public MediaInfo(MainWindow parent)
		{
			_parent = parent;
		}
		
		public void ShowCoverArt()
		{
			/*
			Bitmap bitmap = new Bitmap("images/dvd_32.png");
			Gtk.Window window = new Gtk.Window("ImageTest");
			Gtk.Image testImg = new Gtk.Image();
			testImg.Pixbuf = new Pixbuf("images/dvd_32.png");
			_parent._fInfoContainer.Add(testImg);
			
			
			//_parent._fInfoContainer.Add(
    		//_parent._imgCoverArt.Pixbuf = new Pixbuf(ImageToPixbuf());
    		*/
		}
		
		private void ShowNowPlayingArtist()
		{
			//_parent._lArtist.LabelProp = _parent.oXbmc.NowPlaying.Get("artist");
		}
		
		private void ShowNowPlayingSong()
		{
			//_parent._lSong.LabelProp = _parent.oXbmc.NowPlaying.Get("title");
		}
		
		public void ShowSongInfoPopup(string filepath)
		{
			string sDuration 	= _parent.SecondsToHumanTime(_parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "duration"));
			string songInfo		= "\n\nartist: " + _parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "artist");
			songInfo 			+= "\ntitle: " + _parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "title");
			songInfo 			+= "\nalbum: " + _parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "album");
			songInfo 			+= "\ngenre: " + _parent.oXbmc.Media.GetMusicTagByFilepath(filepath, "genre");
			songInfo 			+= "\nduration: " + sDuration;
			
			Gtk.Window wSongInfo = new Gtk.Window("Song Info");
			wSongInfo.SetPosition(WindowPosition.CenterAlways);
			wSongInfo.SetIconFromFile("images/icon.png");
			
			Pango.FontDescription fd = Pango.FontDescription.FromString("Verdana Bold 9");
			
			/*
			string thumbLocation = _parent.oXbmc.Media.GetFileThumbnailLocation(filepath);

			if (thumbLocation != null)
			{
				MemoryStream msThumbnail = _parent.oXbmc.Media.FileDownload(thumbLocation);
				
				if (msThumbnail != null)
					wSongInfo.Add(new Gtk.Image(new Gdk.Pixbuf(msThumbnail)));
			}
			*/
			
			
			Label lArtist = new Label(songInfo);
			lArtist.ModifyFont(fd);
			lArtist.Xpad = 20;
			lArtist.Ypad = 20;
			
			wSongInfo.Add(lArtist);
			
			//Gtk.Image iCoverart = new Gtk.Image(new Gdk.Pixbuf(_parent.oXbmc.Media.GetFileThumbnailLocation(filepath)));
			//wSongInfo.Add(iCoverart);
			
			wSongInfo.ShowAll();
		}
	}
}
