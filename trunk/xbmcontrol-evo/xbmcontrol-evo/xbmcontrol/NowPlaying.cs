
using System;
using System.IO;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	
	public class NowPlaying
	{
		private MainWindow _parent;
		private string fileInfoShowing;
		
		public NowPlaying(MainWindow parent)
		{
			_parent = parent;
		}
		
		public void ShowData()
		{
			bool show = (_parent.oXbmc.Status.IsNotPlaying())? false : true;
			
			this.ShowArtist(show);
			this.ShowSong(show);
			this.ShowAlbum(show);
			this.ShowYear(show);
			this.ShowGenre(show);
			this.ShowDuration(show);
			this.ShowCoverArt(show);
			
			this.fileInfoShowing = _parent.oXbmc.NowPlaying.Get("filename");
		}
		
		public void ShowCoverArt(bool show)
		{
			if (show)
			{
				MemoryStream msCoverArt = _parent.oXbmc.NowPlaying.GetCoverArt();
				
				if (msCoverArt == null)
					_parent.Messagebox("Failed to load over art");
				else
				{
					Gdk.Pixbuf pbCoverArt = new Gdk.Pixbuf(msCoverArt);
					int caHeight 	= pbCoverArt.Height;
					int caWidth 	= pbCoverArt.Width;
					
					Gdk.Pixbuf pbCoverartResized;
					
					if (caHeight >= caWidth)
					{
						float dim 		= caWidth / caHeight;
						int newHeight	= _parent._imgNowPlaying.HeightRequest;
           				int newWidth 	= (int)((float)(newHeight) * dim);

						pbCoverartResized = pbCoverArt.ScaleSimple(newWidth, newHeight, InterpType.Bilinear);
					}
					else
					{
						float dim 		= caHeight / caWidth;
						int newWidth	= _parent._imgNowPlaying.HeightRequest;
           				int newHeight 	= (int)((float)(newWidth) * dim);

						pbCoverartResized = pbCoverArt.ScaleSimple(newWidth, newHeight, InterpType.Hyper);
					}
					
					_parent._imgNowPlaying.Pixbuf = pbCoverartResized;
					_parent._imgNowPlaying.Show();
				}
			}
			else
				_parent._imgNowPlaying.Pixbuf = new Pixbuf("images/icon_large.png");
		}
		
		public void ShowArtist(bool show)
		{
			_parent._lArtist.Text = (!show)? "Nothing Playing" : _parent.oXbmc.NowPlaying.Get("artist");
		}
		
		public void ShowSong(bool show)
		{
			_parent._lSong.Text = (!show)? "" : _parent.oXbmc.NowPlaying.Get("title");
		}
		
		public void ShowAlbum(bool show)
		{
			_parent._lAlbum.Text = (!show)? "" : _parent.oXbmc.NowPlaying.Get("album");
		}
		
		public void ShowYear(bool show)
		{
			_parent._lYear.Text = (!show)? "" : _parent.oXbmc.NowPlaying.Get("year");;
		}
		
		public void ShowGenre(bool show)
		{
			_parent._lGenre.Text = (!show)? "" : _parent.oXbmc.NowPlaying.Get("genre");
		}
		
		public void ShowProgress(bool show)
		{
			_parent._lProgress.Text = (!show)? "" : _parent.oXbmc.NowPlaying.Get("time");
		}
		
		public void ShowDuration(bool show)
		{
			_parent._lDuration.Text = (!show)? "" : _parent.oXbmc.NowPlaying.Get("duration");
		}
		
		public string GetInfoNowShowing()
		{
			return this.fileInfoShowing;
		}
	}
}
