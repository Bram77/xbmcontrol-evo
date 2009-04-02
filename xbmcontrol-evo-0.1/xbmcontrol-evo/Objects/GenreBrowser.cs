
using System;
using Gtk;
using Gdk;

namespace xbmcontrolevo
{
	
	public class GenreBrowser
	{
		private XbmControlEvo _parent;
		private TreeIter selectedIter;
		//private TreeModel selectedModel;
		private TreeStore tsGenres;
		private TreeViewColumn tvcGenreIcons;
		private TreeViewColumn tvcGenreNames;
		
		public GenreBrowser(XbmControlEvo parent)
		{
			selectedIter 	= new TreeIter();
			_parent 		= parent;
			
			tsGenres	 	= new TreeStore (typeof (Pixbuf), typeof (string), typeof (string));
			
			tvcGenreIcons 	= _parent.tvGenres.AppendColumn ("", new CellRendererPixbuf (), "pixbuf", 0);
			tvcGenreNames 	= _parent.tvGenres.AppendColumn ("", new CellRendererText (), "text", 1);
			
			tvcGenreIcons.Sizing 	= TreeViewColumnSizing.Autosize;
			tvcGenreNames.Sizing	= TreeViewColumnSizing.Autosize;
			_parent.tvGenres.ColumnsAutosize();
		}
		
		internal void Populate(string searchString)
		{
			if (tsGenres.IterNChildren() == 0 || searchString != null)
			{
				
			}
		}
		
		internal void Populate()
		{
			this.Populate(null);
		}
	}
}
