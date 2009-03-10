
using System;
using Gtk;

namespace xbmcontrolevo
{
	
	
	public class GuiConfig
	{
		private MainWindow _parent;
		
		public GuiConfig(MainWindow parent)
		{
			_parent = parent;
			_parent.SetIconFromFile("images/icon.png");
			
			this.SetLabelFonts();
		}
		
		private Pango.FontDescription GetBold(int size)
		{
			return Pango.FontDescription.FromString("Tahoma Bold " + size.ToString());
		}
		
		private void SetLabelFonts()
		{
			_parent._lArtist.ModifyFont(this.GetBold(18));
			_parent._lArtist.SetAlignment(1f, 0f);
			_parent._lArtist.ModifyFg(StateType.Normal, new Gdk.Color(178, 34, 34));
			
			_parent._lSong.ModifyFont(this.GetBold(15));
			_parent._lSong.SetAlignment(1f, 0f);
			_parent._lSong.ModifyFg(StateType.Normal, new Gdk.Color(105, 105, 105));
			
			//_parent._lAlbum.ModifyFont(this.GetBold(9));
			_parent._lAlbum.SetAlignment(1f, 0f);
			_parent._lAlbum.ModifyFg(StateType.Normal, new Gdk.Color(105, 105, 105));
			//_parent._lYear.ModifyFont(this.GetBold(9));
			_parent._lYear.SetAlignment(1f, 0f);
			_parent._lYear.ModifyFg(StateType.Normal, new Gdk.Color(105, 105, 105));
			//_parent._lGenre.ModifyFont(this.GetBold(9));
			_parent._lGenre.SetAlignment(1f, 0f);
			_parent._lGenre.ModifyFg(StateType.Normal, new Gdk.Color(105, 105, 105));
			
			_parent._lProgress.ModifyFont(this.GetBold(12));
			_parent._lProgress.SetAlignment(1f, 0f);
			_parent._lProgress.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
			
			_parent._lDuration.ModifyFont(this.GetBold(12));
			_parent._lDuration.ModifyFg(StateType.Normal, new Gdk.Color(193, 205, 193));
		}
	}
}
