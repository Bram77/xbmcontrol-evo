// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------



public partial class MainWindow {
    
    private Gtk.UIManager UIManager;
    
    private Gtk.Action XBMControlAction;
    
    private Gtk.Action QuitAction;
    
    private Gtk.Action XBMCAction;
    
    private Gtk.Action PlaylistAction;
    
    private Gtk.Action HelpAction;
    
    private Gtk.Action UpdateMusicLibraryAction;
    
    private Gtk.Action UpdateVideoLibraryAction;
    
    private Gtk.Action UpdateLibraryAction;
    
    private Gtk.Action MusicAction;
    
    private Gtk.Action VideoAction;
    
    private Gtk.Action RestartAction;
    
    private Gtk.Action RebootAction;
    
    private Gtk.Action ShutdownAction;
    
    private Gtk.Action ConfigurationAction;
    
    private Gtk.Action clearAction;
    
    private Gtk.Action openAction;
    
    private Gtk.Action saveAction;
    
    private Gtk.Action saveAsAction;
    
    private Gtk.Action aRefreshPlaylist;
    
    private Gtk.Action aRemoveSelected;
    
    private Gtk.Action aPlaySelected;
    
    private Gtk.VBox vbox1;
    
    private Gtk.MenuBar menubar1;
    
    private Gtk.HPaned hpaned1;
    
    private Gtk.Notebook nbBrowser;
    
    private Gtk.VBox vbox3;
    
    private Gtk.ComboBox cbShareType;
    
    private Gtk.ScrolledWindow GtkScrolledWindow;
    
    private Gtk.TreeView tvShareBrowser;
    
    private Gtk.Label label2;
    
    private Gtk.Notebook nbDataContainer;
    
    private Gtk.ScrolledWindow GtkScrolledWindow1;
    
    private Gtk.Fixed fixedNowPlaying;
    
    private Gtk.Image imgNowPlaying;
    
    private Gtk.Label lArtist;
    
    private Gtk.Label lSong;
    
    private Gtk.Label lAlbum;
    
    private Gtk.Label lGenre;
    
    private Gtk.Label lProgress;
    
    private Gtk.Label lDuration;
    
    private Gtk.Label lYear;
    
    private Gtk.Label labelMediaInfo;
    
    private Gtk.VBox vbox2;
    
    private Gtk.ComboBox cbPlaylistType;
    
    private Gtk.ScrolledWindow GtkScrolledWindow2;
    
    private Gtk.TreeView tvPlaylist;
    
    private Gtk.Toolbar toolbar1;
    
    private Gtk.Label label3;
    
    private Gtk.Label label4;
    
    private Gtk.Fixed fixed1;
    
    private Gtk.HScale hsVolume;
    
    private Gtk.HScale hsProgress;
    
    private Gtk.ToggleButton tbMute;
    
    private Gtk.Button bPrevious;
    
    private Gtk.ToggleButton tbPlay;
    
    private Gtk.ToggleButton tbStop;
    
    private Gtk.Button bNext;
    
    private Gtk.Button bRepeat;
    
    private Gtk.Button bShuffle;
    
    private Gtk.Button bPartyMode;
    
    private Gtk.Image imgLoading;
    
    protected virtual void Build() {
        Stetic.Gui.Initialize(this);
        // Widget MainWindow
        this.UIManager = new Gtk.UIManager();
        Gtk.ActionGroup w1 = new Gtk.ActionGroup("Default");
        this.XBMControlAction = new Gtk.Action("XBMControlAction", Mono.Unix.Catalog.GetString("XBMControl"), null, null);
        this.XBMControlAction.ShortLabel = Mono.Unix.Catalog.GetString("XBMControl");
        w1.Add(this.XBMControlAction, null);
        this.QuitAction = new Gtk.Action("QuitAction", Mono.Unix.Catalog.GetString("_Quit"), null, "gtk-disconnect");
        this.QuitAction.ShortLabel = Mono.Unix.Catalog.GetString("_Quit");
        w1.Add(this.QuitAction, null);
        this.XBMCAction = new Gtk.Action("XBMCAction", Mono.Unix.Catalog.GetString("XBMC"), null, null);
        this.XBMCAction.ShortLabel = Mono.Unix.Catalog.GetString("XBMC");
        w1.Add(this.XBMCAction, null);
        this.PlaylistAction = new Gtk.Action("PlaylistAction", Mono.Unix.Catalog.GetString("Playlist"), null, null);
        this.PlaylistAction.ShortLabel = Mono.Unix.Catalog.GetString("Playlist");
        w1.Add(this.PlaylistAction, null);
        this.HelpAction = new Gtk.Action("HelpAction", Mono.Unix.Catalog.GetString("Help"), null, null);
        this.HelpAction.ShortLabel = Mono.Unix.Catalog.GetString("Help");
        w1.Add(this.HelpAction, null);
        this.UpdateMusicLibraryAction = new Gtk.Action("UpdateMusicLibraryAction", Mono.Unix.Catalog.GetString("Update music library"), null, null);
        this.UpdateMusicLibraryAction.ShortLabel = Mono.Unix.Catalog.GetString("Update music library");
        w1.Add(this.UpdateMusicLibraryAction, null);
        this.UpdateVideoLibraryAction = new Gtk.Action("UpdateVideoLibraryAction", Mono.Unix.Catalog.GetString("Update video library"), null, null);
        this.UpdateVideoLibraryAction.ShortLabel = Mono.Unix.Catalog.GetString("Update video library");
        w1.Add(this.UpdateVideoLibraryAction, null);
        this.UpdateLibraryAction = new Gtk.Action("UpdateLibraryAction", Mono.Unix.Catalog.GetString("Update Library"), null, null);
        this.UpdateLibraryAction.ShortLabel = Mono.Unix.Catalog.GetString("Library");
        w1.Add(this.UpdateLibraryAction, null);
        this.MusicAction = new Gtk.Action("MusicAction", Mono.Unix.Catalog.GetString("Music"), null, "gtk-harddisk");
        this.MusicAction.ShortLabel = Mono.Unix.Catalog.GetString("Update music library");
        w1.Add(this.MusicAction, null);
        this.VideoAction = new Gtk.Action("VideoAction", Mono.Unix.Catalog.GetString("Video"), null, "gtk-harddisk");
        this.VideoAction.ShortLabel = Mono.Unix.Catalog.GetString("Update video library");
        w1.Add(this.VideoAction, null);
        this.RestartAction = new Gtk.Action("RestartAction", Mono.Unix.Catalog.GetString("Restart"), null, "gtk-refresh");
        this.RestartAction.ShortLabel = Mono.Unix.Catalog.GetString("Restart");
        w1.Add(this.RestartAction, null);
        this.RebootAction = new Gtk.Action("RebootAction", Mono.Unix.Catalog.GetString("Reboot"), null, "gtk-refresh");
        this.RebootAction.ShortLabel = Mono.Unix.Catalog.GetString("Reboot");
        w1.Add(this.RebootAction, null);
        this.ShutdownAction = new Gtk.Action("ShutdownAction", Mono.Unix.Catalog.GetString("Shutdown"), null, "gtk-quit");
        this.ShutdownAction.ShortLabel = Mono.Unix.Catalog.GetString("Shutdown");
        w1.Add(this.ShutdownAction, null);
        this.ConfigurationAction = new Gtk.Action("ConfigurationAction", Mono.Unix.Catalog.GetString("Configuration"), null, "gtk-edit");
        this.ConfigurationAction.ShortLabel = Mono.Unix.Catalog.GetString("_Preferences");
        w1.Add(this.ConfigurationAction, null);
        this.clearAction = new Gtk.Action("clearAction", null, Mono.Unix.Catalog.GetString("Clear playlist"), "gtk-clear");
        w1.Add(this.clearAction, null);
        this.openAction = new Gtk.Action("openAction", null, null, "gtk-open");
        w1.Add(this.openAction, null);
        this.saveAction = new Gtk.Action("saveAction", null, null, "gtk-save");
        w1.Add(this.saveAction, null);
        this.saveAsAction = new Gtk.Action("saveAsAction", null, null, "gtk-save-as");
        w1.Add(this.saveAsAction, null);
        this.aRefreshPlaylist = new Gtk.Action("aRefreshPlaylist", null, null, "gtk-refresh");
        w1.Add(this.aRefreshPlaylist, null);
        this.aRemoveSelected = new Gtk.Action("aRemoveSelected", null, Mono.Unix.Catalog.GetString("Remove selected item"), "gtk-remove");
        w1.Add(this.aRemoveSelected, null);
        this.aPlaySelected = new Gtk.Action("aPlaySelected", null, Mono.Unix.Catalog.GetString("Play selected item"), "gtk-media-play");
        w1.Add(this.aPlaySelected, null);
        this.UIManager.InsertActionGroup(w1, 0);
        this.AddAccelGroup(this.UIManager.AccelGroup);
        this.WidthRequest = 900;
        this.HeightRequest = 600;
        this.Name = "MainWindow";
        this.Title = Mono.Unix.Catalog.GetString("XBMControl Evo");
        this.WindowPosition = ((Gtk.WindowPosition)(1));
        this.Resizable = false;
        this.AllowGrow = false;
        this.DefaultWidth = 800;
        this.DefaultHeight = 600;
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.vbox1 = new Gtk.VBox();
        this.vbox1.Name = "vbox1";
        this.vbox1.Spacing = 6;
        // Container child vbox1.Gtk.Box+BoxChild
        this.UIManager.AddUiFromString("<ui><menubar name='menubar1'><menu name='XBMControlAction' action='XBMControlAction'><menuitem name='ConfigurationAction' action='ConfigurationAction'/><menuitem name='QuitAction' action='QuitAction'/></menu><menu name='XBMCAction' action='XBMCAction'><menu name='UpdateLibraryAction' action='UpdateLibraryAction'><menuitem name='MusicAction' action='MusicAction'/><menuitem name='VideoAction' action='VideoAction'/></menu><menuitem name='RestartAction' action='RestartAction'/><menuitem name='RebootAction' action='RebootAction'/><menuitem name='ShutdownAction' action='ShutdownAction'/></menu><menu name='PlaylistAction' action='PlaylistAction'/><menu name='HelpAction' action='HelpAction'/></menubar></ui>");
        this.menubar1 = ((Gtk.MenuBar)(this.UIManager.GetWidget("/menubar1")));
        this.menubar1.Name = "menubar1";
        this.vbox1.Add(this.menubar1);
        Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.vbox1[this.menubar1]));
        w2.Position = 0;
        w2.Expand = false;
        w2.Fill = false;
        // Container child vbox1.Gtk.Box+BoxChild
        this.hpaned1 = new Gtk.HPaned();
        this.hpaned1.CanFocus = true;
        this.hpaned1.Name = "hpaned1";
        this.hpaned1.Position = 300;
        // Container child hpaned1.Gtk.Paned+PanedChild
        this.nbBrowser = new Gtk.Notebook();
        this.nbBrowser.WidthRequest = 300;
        this.nbBrowser.CanFocus = true;
        this.nbBrowser.Name = "nbBrowser";
        this.nbBrowser.CurrentPage = 0;
        // Container child nbBrowser.Gtk.Notebook+NotebookChild
        this.vbox3 = new Gtk.VBox();
        this.vbox3.Name = "vbox3";
        this.vbox3.Spacing = 6;
        // Container child vbox3.Gtk.Box+BoxChild
        this.cbShareType = Gtk.ComboBox.NewText();
        this.cbShareType.AppendText(Mono.Unix.Catalog.GetString("Music"));
        this.cbShareType.AppendText(Mono.Unix.Catalog.GetString("Video"));
        this.cbShareType.AppendText(Mono.Unix.Catalog.GetString("Pictures"));
        this.cbShareType.AppendText(Mono.Unix.Catalog.GetString("Files"));
        this.cbShareType.Name = "cbShareType";
        this.cbShareType.Active = 0;
        this.vbox3.Add(this.cbShareType);
        Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox3[this.cbShareType]));
        w3.Position = 0;
        w3.Expand = false;
        w3.Fill = false;
        // Container child vbox3.Gtk.Box+BoxChild
        this.GtkScrolledWindow = new Gtk.ScrolledWindow();
        this.GtkScrolledWindow.Name = "GtkScrolledWindow";
        this.GtkScrolledWindow.ShadowType = ((Gtk.ShadowType)(1));
        // Container child GtkScrolledWindow.Gtk.Container+ContainerChild
        this.tvShareBrowser = new Gtk.TreeView();
        this.tvShareBrowser.CanFocus = true;
        this.tvShareBrowser.Name = "tvShareBrowser";
        this.GtkScrolledWindow.Add(this.tvShareBrowser);
        this.vbox3.Add(this.GtkScrolledWindow);
        Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox3[this.GtkScrolledWindow]));
        w5.Position = 1;
        this.nbBrowser.Add(this.vbox3);
        // Notebook tab
        this.label2 = new Gtk.Label();
        this.label2.Name = "label2";
        this.label2.LabelProp = Mono.Unix.Catalog.GetString("Shares");
        this.nbBrowser.SetTabLabel(this.vbox3, this.label2);
        this.label2.ShowAll();
        this.hpaned1.Add(this.nbBrowser);
        Gtk.Paned.PanedChild w7 = ((Gtk.Paned.PanedChild)(this.hpaned1[this.nbBrowser]));
        w7.Resize = false;
        w7.Shrink = false;
        // Container child hpaned1.Gtk.Paned+PanedChild
        this.nbDataContainer = new Gtk.Notebook();
        this.nbDataContainer.CanFocus = true;
        this.nbDataContainer.Name = "nbDataContainer";
        this.nbDataContainer.CurrentPage = 0;
        this.nbDataContainer.ShowBorder = false;
        this.nbDataContainer.Scrollable = true;
        // Container child nbDataContainer.Gtk.Notebook+NotebookChild
        this.GtkScrolledWindow1 = new Gtk.ScrolledWindow();
        this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
        this.GtkScrolledWindow1.ShadowType = ((Gtk.ShadowType)(1));
        // Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
        Gtk.Viewport w8 = new Gtk.Viewport();
        w8.ShadowType = ((Gtk.ShadowType)(0));
        // Container child GtkViewport.Gtk.Container+ContainerChild
        this.fixedNowPlaying = new Gtk.Fixed();
        this.fixedNowPlaying.Name = "fixedNowPlaying";
        this.fixedNowPlaying.HasWindow = false;
        // Container child fixedNowPlaying.Gtk.Fixed+FixedChild
        this.imgNowPlaying = new Gtk.Image();
        this.imgNowPlaying.WidthRequest = 300;
        this.imgNowPlaying.HeightRequest = 300;
        this.imgNowPlaying.Name = "imgNowPlaying";
        this.fixedNowPlaying.Add(this.imgNowPlaying);
        Gtk.Fixed.FixedChild w9 = ((Gtk.Fixed.FixedChild)(this.fixedNowPlaying[this.imgNowPlaying]));
        w9.X = 20;
        w9.Y = 145;
        // Container child fixedNowPlaying.Gtk.Fixed+FixedChild
        this.lArtist = new Gtk.Label();
        this.lArtist.WidthRequest = 555;
        this.lArtist.Name = "lArtist";
        this.fixedNowPlaying.Add(this.lArtist);
        Gtk.Fixed.FixedChild w10 = ((Gtk.Fixed.FixedChild)(this.fixedNowPlaying[this.lArtist]));
        w10.X = 10;
        w10.Y = 10;
        // Container child fixedNowPlaying.Gtk.Fixed+FixedChild
        this.lSong = new Gtk.Label();
        this.lSong.WidthRequest = 555;
        this.lSong.Name = "lSong";
        this.fixedNowPlaying.Add(this.lSong);
        Gtk.Fixed.FixedChild w11 = ((Gtk.Fixed.FixedChild)(this.fixedNowPlaying[this.lSong]));
        w11.X = 10;
        w11.Y = 34;
        // Container child fixedNowPlaying.Gtk.Fixed+FixedChild
        this.lAlbum = new Gtk.Label();
        this.lAlbum.WidthRequest = 555;
        this.lAlbum.Name = "lAlbum";
        this.fixedNowPlaying.Add(this.lAlbum);
        Gtk.Fixed.FixedChild w12 = ((Gtk.Fixed.FixedChild)(this.fixedNowPlaying[this.lAlbum]));
        w12.X = 10;
        w12.Y = 70;
        // Container child fixedNowPlaying.Gtk.Fixed+FixedChild
        this.lGenre = new Gtk.Label();
        this.lGenre.WidthRequest = 155;
        this.lGenre.Name = "lGenre";
        this.fixedNowPlaying.Add(this.lGenre);
        Gtk.Fixed.FixedChild w13 = ((Gtk.Fixed.FixedChild)(this.fixedNowPlaying[this.lGenre]));
        w13.X = 410;
        w13.Y = 98;
        // Container child fixedNowPlaying.Gtk.Fixed+FixedChild
        this.lProgress = new Gtk.Label();
        this.lProgress.WidthRequest = 150;
        this.lProgress.Name = "lProgress";
        this.fixedNowPlaying.Add(this.lProgress);
        Gtk.Fixed.FixedChild w14 = ((Gtk.Fixed.FixedChild)(this.fixedNowPlaying[this.lProgress]));
        w14.X = 353;
        w14.Y = 430;
        // Container child fixedNowPlaying.Gtk.Fixed+FixedChild
        this.lDuration = new Gtk.Label();
        this.lDuration.WidthRequest = 60;
        this.lDuration.Name = "lDuration";
        this.fixedNowPlaying.Add(this.lDuration);
        Gtk.Fixed.FixedChild w15 = ((Gtk.Fixed.FixedChild)(this.fixedNowPlaying[this.lDuration]));
        w15.X = 502;
        w15.Y = 430;
        // Container child fixedNowPlaying.Gtk.Fixed+FixedChild
        this.lYear = new Gtk.Label();
        this.lYear.WidthRequest = 155;
        this.lYear.Name = "lYear";
        this.fixedNowPlaying.Add(this.lYear);
        Gtk.Fixed.FixedChild w16 = ((Gtk.Fixed.FixedChild)(this.fixedNowPlaying[this.lYear]));
        w16.X = 410;
        w16.Y = 84;
        w8.Add(this.fixedNowPlaying);
        this.GtkScrolledWindow1.Add(w8);
        this.nbDataContainer.Add(this.GtkScrolledWindow1);
        // Notebook tab
        this.labelMediaInfo = new Gtk.Label();
        this.labelMediaInfo.Name = "labelMediaInfo";
        this.labelMediaInfo.LabelProp = Mono.Unix.Catalog.GetString("Playing Now");
        this.nbDataContainer.SetTabLabel(this.GtkScrolledWindow1, this.labelMediaInfo);
        this.labelMediaInfo.ShowAll();
        // Container child nbDataContainer.Gtk.Notebook+NotebookChild
        this.vbox2 = new Gtk.VBox();
        this.vbox2.Name = "vbox2";
        this.vbox2.Spacing = 6;
        // Container child vbox2.Gtk.Box+BoxChild
        this.cbPlaylistType = Gtk.ComboBox.NewText();
        this.cbPlaylistType.AppendText(Mono.Unix.Catalog.GetString("Music"));
        this.cbPlaylistType.AppendText(Mono.Unix.Catalog.GetString("Video"));
        this.cbPlaylistType.Name = "cbPlaylistType";
        this.cbPlaylistType.Active = 0;
        this.vbox2.Add(this.cbPlaylistType);
        Gtk.Box.BoxChild w20 = ((Gtk.Box.BoxChild)(this.vbox2[this.cbPlaylistType]));
        w20.Position = 0;
        w20.Expand = false;
        w20.Fill = false;
        // Container child vbox2.Gtk.Box+BoxChild
        this.GtkScrolledWindow2 = new Gtk.ScrolledWindow();
        this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
        this.GtkScrolledWindow2.ShadowType = ((Gtk.ShadowType)(1));
        // Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
        this.tvPlaylist = new Gtk.TreeView();
        this.tvPlaylist.CanFocus = true;
        this.tvPlaylist.Name = "tvPlaylist";
        this.GtkScrolledWindow2.Add(this.tvPlaylist);
        this.vbox2.Add(this.GtkScrolledWindow2);
        Gtk.Box.BoxChild w22 = ((Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow2]));
        w22.Position = 1;
        // Container child vbox2.Gtk.Box+BoxChild
        this.UIManager.AddUiFromString("<ui><toolbar name='toolbar1'><toolitem name='clearAction' action='clearAction'/><toolitem name='aRefreshPlaylist' action='aRefreshPlaylist'/><toolitem name='aRemoveSelected' action='aRemoveSelected'/><toolitem name='aPlaySelected' action='aPlaySelected'/></toolbar></ui>");
        this.toolbar1 = ((Gtk.Toolbar)(this.UIManager.GetWidget("/toolbar1")));
        this.toolbar1.TooltipMarkup = "Refresh playlist";
        this.toolbar1.Name = "toolbar1";
        this.toolbar1.ShowArrow = false;
        this.toolbar1.ToolbarStyle = ((Gtk.ToolbarStyle)(0));
        this.toolbar1.IconSize = ((Gtk.IconSize)(3));
        this.vbox2.Add(this.toolbar1);
        Gtk.Box.BoxChild w23 = ((Gtk.Box.BoxChild)(this.vbox2[this.toolbar1]));
        w23.Position = 2;
        w23.Expand = false;
        w23.Fill = false;
        this.nbDataContainer.Add(this.vbox2);
        Gtk.Notebook.NotebookChild w24 = ((Gtk.Notebook.NotebookChild)(this.nbDataContainer[this.vbox2]));
        w24.Position = 1;
        // Notebook tab
        this.label3 = new Gtk.Label();
        this.label3.Name = "label3";
        this.label3.LabelProp = Mono.Unix.Catalog.GetString("Playlist");
        this.nbDataContainer.SetTabLabel(this.vbox2, this.label3);
        this.label3.ShowAll();
        // Notebook tab
        Gtk.Label w25 = new Gtk.Label();
        w25.Visible = true;
        this.nbDataContainer.Add(w25);
        this.label4 = new Gtk.Label();
        this.label4.Name = "label4";
        this.label4.LabelProp = Mono.Unix.Catalog.GetString("Configuration");
        this.nbDataContainer.SetTabLabel(w25, this.label4);
        this.label4.ShowAll();
        this.hpaned1.Add(this.nbDataContainer);
        Gtk.Paned.PanedChild w26 = ((Gtk.Paned.PanedChild)(this.hpaned1[this.nbDataContainer]));
        w26.Resize = false;
        w26.Shrink = false;
        this.vbox1.Add(this.hpaned1);
        Gtk.Box.BoxChild w27 = ((Gtk.Box.BoxChild)(this.vbox1[this.hpaned1]));
        w27.Position = 1;
        // Container child vbox1.Gtk.Box+BoxChild
        this.fixed1 = new Gtk.Fixed();
        this.fixed1.HeightRequest = 60;
        this.fixed1.Name = "fixed1";
        this.fixed1.HasWindow = false;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.hsVolume = new Gtk.HScale(null);
        this.hsVolume.TooltipMarkup = "Volume";
        this.hsVolume.WidthRequest = 120;
        this.hsVolume.CanFocus = true;
        this.hsVolume.Name = "hsVolume";
        this.hsVolume.Adjustment.Upper = 100;
        this.hsVolume.Adjustment.PageIncrement = 10;
        this.hsVolume.Adjustment.StepIncrement = 1;
        this.hsVolume.Adjustment.Value = 51.8987341772152;
        this.hsVolume.DrawValue = false;
        this.hsVolume.Digits = 0;
        this.hsVolume.ValuePos = ((Gtk.PositionType)(2));
        this.fixed1.Add(this.hsVolume);
        Gtk.Fixed.FixedChild w28 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.hsVolume]));
        w28.X = 730;
        w28.Y = 10;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.hsProgress = new Gtk.HScale(null);
        this.hsProgress.TooltipMarkup = "Progress";
        this.hsProgress.WidthRequest = 420;
        this.hsProgress.CanFocus = true;
        this.hsProgress.Name = "hsProgress";
        this.hsProgress.Adjustment.Upper = 100;
        this.hsProgress.Adjustment.PageIncrement = 10;
        this.hsProgress.Adjustment.StepIncrement = 1;
        this.hsProgress.DrawValue = false;
        this.hsProgress.Digits = 0;
        this.hsProgress.ValuePos = ((Gtk.PositionType)(2));
        this.fixed1.Add(this.hsProgress);
        Gtk.Fixed.FixedChild w29 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.hsProgress]));
        w29.X = 300;
        w29.Y = 10;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.tbMute = new Gtk.ToggleButton();
        this.tbMute.TooltipMarkup = "Toggle Mute";
        this.tbMute.WidthRequest = 32;
        this.tbMute.HeightRequest = 32;
        this.tbMute.CanFocus = true;
        this.tbMute.Name = "tbMute";
        this.tbMute.UseUnderline = true;
        // Container child tbMute.Gtk.Container+ContainerChild
        Gtk.Alignment w30 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w31 = new Gtk.HBox();
        w31.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w32 = new Gtk.Image();
        w32.Pixbuf = Stetic.IconLoader.LoadIcon(this, "stock_volume-mute", Gtk.IconSize.Button, 20);
        w31.Add(w32);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w34 = new Gtk.Label();
        w31.Add(w34);
        w30.Add(w31);
        this.tbMute.Add(w30);
        this.fixed1.Add(this.tbMute);
        Gtk.Fixed.FixedChild w38 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.tbMute]));
        w38.X = 850;
        w38.Y = 8;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.bPrevious = new Gtk.Button();
        this.bPrevious.TooltipMarkup = "Previous";
        this.bPrevious.WidthRequest = 35;
        this.bPrevious.HeightRequest = 35;
        this.bPrevious.CanFocus = true;
        this.bPrevious.Name = "bPrevious";
        this.bPrevious.UseUnderline = true;
        // Container child bPrevious.Gtk.Container+ContainerChild
        Gtk.Alignment w39 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w40 = new Gtk.HBox();
        w40.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w41 = new Gtk.Image();
        w41.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-media-previous", Gtk.IconSize.LargeToolbar, 24);
        w40.Add(w41);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w43 = new Gtk.Label();
        w40.Add(w43);
        w39.Add(w40);
        this.bPrevious.Add(w39);
        this.fixed1.Add(this.bPrevious);
        Gtk.Fixed.FixedChild w47 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.bPrevious]));
        w47.X = 14;
        w47.Y = 10;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.tbPlay = new Gtk.ToggleButton();
        this.tbPlay.TooltipMarkup = "Play";
        this.tbPlay.WidthRequest = 35;
        this.tbPlay.HeightRequest = 35;
        this.tbPlay.CanFocus = true;
        this.tbPlay.Name = "tbPlay";
        this.tbPlay.UseUnderline = true;
        // Container child tbPlay.Gtk.Container+ContainerChild
        Gtk.Alignment w48 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w49 = new Gtk.HBox();
        w49.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w50 = new Gtk.Image();
        w50.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-media-play", Gtk.IconSize.LargeToolbar, 24);
        w49.Add(w50);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w52 = new Gtk.Label();
        w49.Add(w52);
        w48.Add(w49);
        this.tbPlay.Add(w48);
        this.fixed1.Add(this.tbPlay);
        Gtk.Fixed.FixedChild w56 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.tbPlay]));
        w56.X = 46;
        w56.Y = 10;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.tbStop = new Gtk.ToggleButton();
        this.tbStop.TooltipMarkup = "Stop";
        this.tbStop.WidthRequest = 35;
        this.tbStop.HeightRequest = 35;
        this.tbStop.CanFocus = true;
        this.tbStop.Name = "tbStop";
        this.tbStop.UseUnderline = true;
        // Container child tbStop.Gtk.Container+ContainerChild
        Gtk.Alignment w57 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w58 = new Gtk.HBox();
        w58.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w59 = new Gtk.Image();
        w59.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-media-stop", Gtk.IconSize.LargeToolbar, 24);
        w58.Add(w59);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w61 = new Gtk.Label();
        w58.Add(w61);
        w57.Add(w58);
        this.tbStop.Add(w57);
        this.fixed1.Add(this.tbStop);
        Gtk.Fixed.FixedChild w65 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.tbStop]));
        w65.X = 78;
        w65.Y = 10;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.bNext = new Gtk.Button();
        this.bNext.TooltipMarkup = "Next";
        this.bNext.WidthRequest = 35;
        this.bNext.HeightRequest = 35;
        this.bNext.CanFocus = true;
        this.bNext.Name = "bNext";
        this.bNext.UseUnderline = true;
        // Container child bNext.Gtk.Container+ContainerChild
        Gtk.Alignment w66 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w67 = new Gtk.HBox();
        w67.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w68 = new Gtk.Image();
        w68.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-media-next", Gtk.IconSize.LargeToolbar, 24);
        w67.Add(w68);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w70 = new Gtk.Label();
        w67.Add(w70);
        w66.Add(w67);
        this.bNext.Add(w66);
        this.fixed1.Add(this.bNext);
        Gtk.Fixed.FixedChild w74 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.bNext]));
        w74.X = 110;
        w74.Y = 10;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.bRepeat = new Gtk.Button();
        this.bRepeat.TooltipMarkup = "Toggle Repeat Modes";
        this.bRepeat.WidthRequest = 28;
        this.bRepeat.HeightRequest = 28;
        this.bRepeat.CanFocus = true;
        this.bRepeat.Name = "bRepeat";
        this.bRepeat.UseUnderline = true;
        // Container child bRepeat.Gtk.Container+ContainerChild
        Gtk.Alignment w75 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w76 = new Gtk.HBox();
        w76.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w77 = new Gtk.Image();
        w77.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-redo", Gtk.IconSize.Menu, 16);
        w76.Add(w77);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w79 = new Gtk.Label();
        w76.Add(w79);
        w75.Add(w76);
        this.bRepeat.Add(w75);
        this.fixed1.Add(this.bRepeat);
        Gtk.Fixed.FixedChild w83 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.bRepeat]));
        w83.X = 160;
        w83.Y = 10;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.bShuffle = new Gtk.Button();
        this.bShuffle.TooltipMarkup = "Toggle Shuffel Mode";
        this.bShuffle.WidthRequest = 28;
        this.bShuffle.HeightRequest = 28;
        this.bShuffle.CanFocus = true;
        this.bShuffle.Name = "bShuffle";
        this.bShuffle.UseUnderline = true;
        // Container child bShuffle.Gtk.Container+ContainerChild
        Gtk.Alignment w84 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w85 = new Gtk.HBox();
        w85.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w86 = new Gtk.Image();
        w86.Pixbuf = Stetic.IconLoader.LoadIcon(this, "stock_chart-toggle-legend", Gtk.IconSize.Menu, 16);
        w85.Add(w86);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w88 = new Gtk.Label();
        w85.Add(w88);
        w84.Add(w85);
        this.bShuffle.Add(w84);
        this.fixed1.Add(this.bShuffle);
        Gtk.Fixed.FixedChild w92 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.bShuffle]));
        w92.X = 185;
        w92.Y = 10;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.bPartyMode = new Gtk.Button();
        this.bPartyMode.TooltipMarkup = "Toggle Party Mode";
        this.bPartyMode.WidthRequest = 28;
        this.bPartyMode.HeightRequest = 28;
        this.bPartyMode.CanFocus = true;
        this.bPartyMode.Name = "bPartyMode";
        this.bPartyMode.UseUnderline = true;
        // Container child bPartyMode.Gtk.Container+ContainerChild
        Gtk.Alignment w93 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w94 = new Gtk.HBox();
        w94.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w95 = new Gtk.Image();
        w95.Pixbuf = Stetic.IconLoader.LoadIcon(this, "stock_filters", Gtk.IconSize.Menu, 16);
        w94.Add(w95);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w97 = new Gtk.Label();
        w94.Add(w97);
        w93.Add(w94);
        this.bPartyMode.Add(w93);
        this.fixed1.Add(this.bPartyMode);
        Gtk.Fixed.FixedChild w101 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.bPartyMode]));
        w101.X = 210;
        w101.Y = 10;
        // Container child fixed1.Gtk.Fixed+FixedChild
        this.imgLoading = new Gtk.Image();
        this.imgLoading.TooltipMarkup = "Loading data...";
        this.imgLoading.Name = "imgLoading";
        this.imgLoading.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-jump-to", Gtk.IconSize.LargeToolbar, 24);
        this.fixed1.Add(this.imgLoading);
        Gtk.Fixed.FixedChild w102 = ((Gtk.Fixed.FixedChild)(this.fixed1[this.imgLoading]));
        w102.X = 255;
        w102.Y = 13;
        this.vbox1.Add(this.fixed1);
        Gtk.Box.BoxChild w103 = ((Gtk.Box.BoxChild)(this.vbox1[this.fixed1]));
        w103.Position = 2;
        w103.Expand = false;
        w103.Fill = false;
        this.Add(this.vbox1);
        if ((this.Child != null)) {
            this.Child.ShowAll();
        }
        this.Show();
        this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
        this.QuitAction.Activated += new System.EventHandler(this.OnExit);
        this.MusicAction.Activated += new System.EventHandler(this.click_UpdateMusicLibrary);
        this.VideoAction.Activated += new System.EventHandler(this.click_UpdateVideoLibrary);
        this.clearAction.Activated += new System.EventHandler(this.aClearPlaylist_click);
        this.aRefreshPlaylist.Activated += new System.EventHandler(this.aRefreshPlaylist_activated);
        this.aRemoveSelected.Activated += new System.EventHandler(this.aRemoveSelected_activated);
        this.aPlaySelected.Activated += new System.EventHandler(this.aPlaySelected_activated);
        this.cbShareType.Changed += new System.EventHandler(this.cbShareBrowser_changed);
        this.tvShareBrowser.ButtonReleaseEvent += new Gtk.ButtonReleaseEventHandler(this.tvShareBrowser_release);
        this.cbPlaylistType.Changed += new System.EventHandler(this.cbPlaylistType_changed);
        this.tvPlaylist.ButtonReleaseEvent += new Gtk.ButtonReleaseEventHandler(this.tvPlaylist_buttonRelease);
        this.hsVolume.ValueChanged += new System.EventHandler(this.hsVolume_valueChanged);
        this.hsProgress.ChangeValue += new Gtk.ChangeValueHandler(this.hsProgress_changeValue);
        this.tbMute.Released += new System.EventHandler(this.tbMute_released);
        this.bPrevious.Released += new System.EventHandler(this.bPrevious_released);
        this.tbPlay.Released += new System.EventHandler(this.tbPlay_released);
        this.tbStop.Released += new System.EventHandler(this.tbStop_released);
        this.bNext.Released += new System.EventHandler(this.bNext_released);
        this.bRepeat.Activated += new System.EventHandler(this.bRepeat_click);
        this.bShuffle.Released += new System.EventHandler(this.bShuffle_release);
        this.bPartyMode.Released += new System.EventHandler(this.bPartyMode_released);
    }
}
