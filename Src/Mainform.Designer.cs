namespace ExpertSokoban
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlLevelList = new System.Windows.Forms.Panel();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuContextPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextHighscores = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextNewLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextNewComment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContextHide = new System.Windows.Forms.ToolStripMenuItem();
            this.toolEditLevel = new System.Windows.Forms.ToolStrip();
            this.btnEditLevelWall = new System.Windows.Forms.ToolStripButton();
            this.btnEditLevelPiece = new System.Windows.Forms.ToolStripButton();
            this.btnEditLevelTarget = new System.Windows.Forms.ToolStripButton();
            this.btnEditLevelSokoban = new System.Windows.Forms.ToolStripButton();
            this.btnEditLevelSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditLevelOK = new System.Windows.Forms.ToolStripButton();
            this.btnEditLevelCancel = new System.Windows.Forms.ToolStripButton();
            this.toolFileEdit = new System.Windows.Forms.ToolStrip();
            this.btnFileEditNewLevel = new System.Windows.Forms.ToolStripButton();
            this.btnFileEditEditLevel = new System.Windows.Forms.ToolStripButton();
            this.btnFileEditAddComment = new System.Windows.Forms.ToolStripButton();
            this.btnFileEditDeleteLevel = new System.Windows.Forms.ToolStripButton();
            this.toolFile = new System.Windows.Forms.ToolStrip();
            this.btnFileNew = new System.Windows.Forms.ToolStripButton();
            this.btnFileOpen = new System.Windows.Forms.ToolStripButton();
            this.btnFileSave = new System.Windows.Forms.ToolStripButton();
            this.sepToolFile = new System.Windows.Forms.ToolStripSeparator();
            this.btnFileCut = new System.Windows.Forms.ToolStripButton();
            this.btnFileCopy = new System.Windows.Forms.ToolStripButton();
            this.btnFilePaste = new System.Windows.Forms.ToolStripButton();
            this.toolPlay = new System.Windows.Forms.ToolStrip();
            this.btnPlayOpenLevel = new System.Windows.Forms.ToolStripButton();
            this.sepPlay1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlayPrevLevel = new System.Windows.Forms.ToolStripButton();
            this.btnPlayNextLevel = new System.Windows.Forms.ToolStripButton();
            this.sepPlay2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlayPrevUnsolvedLevel = new System.Windows.Forms.ToolStripButton();
            this.btnPlayNextUnsolvedLevel = new System.Windows.Forms.ToolStripButton();
            this.pnlCloseLevelList = new RT.Util.Controls.NiceClosePanel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLevelUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelRetry = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelHighscores = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLevelPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelNext = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelPreviousUnsolved = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelNextUnsolved = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLevelChangePlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLevelExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevelUnusedHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUnusedCTRLShortcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCreateLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditEditLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditAddComment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditFinish = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditUnusedHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsLevelList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsPlayingToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsFileToolbars = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsEditLevelToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOptionsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOptionsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOptionsEndPos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsAreaSokoban = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsAreaPiece = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOptionsLetterControl = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsLetterControlNext = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsSound = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsAnimation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsChangeLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.dummyItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsUnusedHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ctMainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.ctStatusBar = new System.Windows.Forms.StatusStrip();
            this.lblStatusMoves = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusPushes = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusPieces = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusEdit = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusSolved = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusNull = new System.Windows.Forms.ToolStripStatusLabel();
            this.ctLevelListSplitter = new System.Windows.Forms.Splitter();
            this.tmrBugWorkaround = new System.Windows.Forms.Timer(this.components);
            this.tmrUpdateControls = new System.Windows.Forms.Timer(this.components);
            this.ctMainArea = new ExpertSokoban.MainArea();
            this.mnuEditWall = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.grpEditTool = new ExpertSokoban.MenuRadioGroupMainAreaTool();
            this.mnuEditPiece = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.mnuEditTarget = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.mnuEditSokoban = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.mnuOptionsMoveNo = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.grpMovePathOptions = new ExpertSokoban.MenuRadioGroupPathDrawMode();
            this.mnuOptionsMoveLine = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.mnuOptionsMoveDots = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.mnuOptionsMoveArrows = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.mnuOptionsPushNo = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.grpPushPathOptions = new ExpertSokoban.MenuRadioGroupPathDrawMode();
            this.mnuOptionsPushLine = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.mnuOptionsPushDots = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.mnuOptionsPushArrows = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.lstLevels = new ExpertSokoban.LevelListBox();
            this.pnlLevelList.SuspendLayout();
            this.mnuContext.SuspendLayout();
            this.toolEditLevel.SuspendLayout();
            this.toolFileEdit.SuspendLayout();
            this.toolFile.SuspendLayout();
            this.toolPlay.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.ctMainToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.ctMainToolStripContainer.ContentPanel.SuspendLayout();
            this.ctMainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.ctMainToolStripContainer.SuspendLayout();
            this.ctStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLevelList
            // 
            this.pnlLevelList.BackColor = System.Drawing.SystemColors.Control;
            this.pnlLevelList.Controls.Add(this.lstLevels);
            this.pnlLevelList.Controls.Add(this.toolEditLevel);
            this.pnlLevelList.Controls.Add(this.toolFileEdit);
            this.pnlLevelList.Controls.Add(this.toolFile);
            this.pnlLevelList.Controls.Add(this.toolPlay);
            this.pnlLevelList.Controls.Add(this.pnlCloseLevelList);
            this.pnlLevelList.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLevelList.Location = new System.Drawing.Point(645, 0);
            this.pnlLevelList.Name = "pnlLevelList";
            this.pnlLevelList.Size = new System.Drawing.Size(147, 573);
            this.pnlLevelList.TabIndex = 6;
            this.pnlLevelList.Visible = false;
            this.pnlLevelList.Resize += new System.EventHandler(this.levelListPanelResize);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextPlay,
            this.mnuContextEdit,
            this.mnuContextHighscores,
            this.mnuContextSep1,
            this.mnuContextNewLevel,
            this.mnuContextNewComment,
            this.mnuContextSep2,
            this.mnuContextCut,
            this.mnuContextCopy,
            this.mnuContextPaste,
            this.mnuContextDelete,
            this.mnuContextSep3,
            this.mnuContextHide});
            this.mnuContext.Name = "LevelContextMenu";
            this.mnuContext.Size = new System.Drawing.Size(239, 242);
            // 
            // mnuContextPlay
            // 
            this.mnuContextPlay.Name = "mnuContextPlay";
            this.mnuContextPlay.ShortcutKeyDisplayString = "Enter";
            this.mnuContextPlay.Size = new System.Drawing.Size(238, 22);
            this.mnuContextPlay.Text = "Pl&ay this level";
            this.mnuContextPlay.Click += new System.EventHandler(this.playLevel);
            // 
            // mnuContextEdit
            // 
            this.mnuContextEdit.Name = "mnuContextEdit";
            this.mnuContextEdit.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuContextEdit.Size = new System.Drawing.Size(238, 22);
            this.mnuContextEdit.Text = "&Edit this level";
            this.mnuContextEdit.Click += new System.EventHandler(this.editLevel);
            // 
            // mnuContextHighscores
            // 
            this.mnuContextHighscores.Name = "mnuContextHighscores";
            this.mnuContextHighscores.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.mnuContextHighscores.Size = new System.Drawing.Size(238, 22);
            this.mnuContextHighscores.Text = "Show &highscores";
            this.mnuContextHighscores.Click += new System.EventHandler(this.showHighscores);
            // 
            // mnuContextSep1
            // 
            this.mnuContextSep1.Name = "mnuContextSep1";
            this.mnuContextSep1.Size = new System.Drawing.Size(235, 6);
            // 
            // mnuContextNewLevel
            // 
            this.mnuContextNewLevel.Name = "mnuContextNewLevel";
            this.mnuContextNewLevel.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.mnuContextNewLevel.Size = new System.Drawing.Size(238, 22);
            this.mnuContextNewLevel.Text = "C&reate a new level here";
            this.mnuContextNewLevel.Click += new System.EventHandler(this.createLevel);
            // 
            // mnuContextNewComment
            // 
            this.mnuContextNewComment.Name = "mnuContextNewComment";
            this.mnuContextNewComment.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mnuContextNewComment.Size = new System.Drawing.Size(238, 22);
            this.mnuContextNewComment.Text = "&Insert a comment here";
            this.mnuContextNewComment.Click += new System.EventHandler(this.addComment);
            // 
            // mnuContextSep2
            // 
            this.mnuContextSep2.Name = "mnuContextSep2";
            this.mnuContextSep2.Size = new System.Drawing.Size(235, 6);
            // 
            // mnuContextCut
            // 
            this.mnuContextCut.Name = "mnuContextCut";
            this.mnuContextCut.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuContextCut.Size = new System.Drawing.Size(238, 22);
            this.mnuContextCut.Text = "C&ut";
            this.mnuContextCut.Click += new System.EventHandler(this.cut);
            // 
            // mnuContextCopy
            // 
            this.mnuContextCopy.Name = "mnuContextCopy";
            this.mnuContextCopy.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuContextCopy.Size = new System.Drawing.Size(238, 22);
            this.mnuContextCopy.Text = "&Copy";
            this.mnuContextCopy.Click += new System.EventHandler(this.copy);
            // 
            // mnuContextPaste
            // 
            this.mnuContextPaste.Name = "mnuContextPaste";
            this.mnuContextPaste.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuContextPaste.Size = new System.Drawing.Size(238, 22);
            this.mnuContextPaste.Text = "&Paste";
            this.mnuContextPaste.Click += new System.EventHandler(this.paste);
            // 
            // mnuContextDelete
            // 
            this.mnuContextDelete.Name = "mnuContextDelete";
            this.mnuContextDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuContextDelete.Size = new System.Drawing.Size(238, 22);
            this.mnuContextDelete.Text = "&Delete";
            this.mnuContextDelete.Click += new System.EventHandler(this.deleteLevelOrComment);
            // 
            // mnuContextSep3
            // 
            this.mnuContextSep3.Name = "mnuContextSep3";
            this.mnuContextSep3.Size = new System.Drawing.Size(235, 6);
            // 
            // mnuContextHide
            // 
            this.mnuContextHide.Name = "mnuContextHide";
            this.mnuContextHide.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.mnuContextHide.Size = new System.Drawing.Size(238, 22);
            this.mnuContextHide.Text = "Hide &level list";
            this.mnuContextHide.Click += new System.EventHandler(this.toggleLevelList);
            // 
            // toolEditLevel
            // 
            this.toolEditLevel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolEditLevel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEditLevelWall,
            this.btnEditLevelPiece,
            this.btnEditLevelTarget,
            this.btnEditLevelSokoban,
            this.btnEditLevelSep,
            this.btnEditLevelOK,
            this.btnEditLevelCancel});
            this.toolEditLevel.Location = new System.Drawing.Point(0, 85);
            this.toolEditLevel.Name = "toolEditLevel";
            this.toolEditLevel.Size = new System.Drawing.Size(147, 25);
            this.toolEditLevel.TabIndex = 3;
            this.toolEditLevel.Text = "Level edit toolbar";
            // 
            // btnEditLevelWall
            // 
            this.btnEditLevelWall.Checked = true;
            this.btnEditLevelWall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnEditLevelWall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditLevelWall.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolBrick;
            this.btnEditLevelWall.Name = "btnEditLevelWall";
            this.btnEditLevelWall.Size = new System.Drawing.Size(23, 22);
            this.btnEditLevelWall.Text = "Wall tool";
            this.btnEditLevelWall.Click += new System.EventHandler(this.changeEditingTool);
            // 
            // btnEditLevelPiece
            // 
            this.btnEditLevelPiece.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditLevelPiece.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolPiece;
            this.btnEditLevelPiece.Name = "btnEditLevelPiece";
            this.btnEditLevelPiece.Size = new System.Drawing.Size(23, 22);
            this.btnEditLevelPiece.Text = "Piece tool";
            this.btnEditLevelPiece.Click += new System.EventHandler(this.changeEditingTool);
            // 
            // btnEditLevelTarget
            // 
            this.btnEditLevelTarget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditLevelTarget.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolTarget;
            this.btnEditLevelTarget.Name = "btnEditLevelTarget";
            this.btnEditLevelTarget.Size = new System.Drawing.Size(23, 22);
            this.btnEditLevelTarget.Text = "Target tool";
            this.btnEditLevelTarget.Click += new System.EventHandler(this.changeEditingTool);
            // 
            // btnEditLevelSokoban
            // 
            this.btnEditLevelSokoban.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditLevelSokoban.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolSokoban;
            this.btnEditLevelSokoban.Name = "btnEditLevelSokoban";
            this.btnEditLevelSokoban.Size = new System.Drawing.Size(23, 22);
            this.btnEditLevelSokoban.Text = "Sokoban tool";
            this.btnEditLevelSokoban.Click += new System.EventHandler(this.changeEditingTool);
            // 
            // btnEditLevelSep
            // 
            this.btnEditLevelSep.Name = "btnEditLevelSep";
            this.btnEditLevelSep.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEditLevelOK
            // 
            this.btnEditLevelOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditLevelOK.Image = global::ExpertSokoban.Properties.Resources.ok;
            this.btnEditLevelOK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditLevelOK.Name = "btnEditLevelOK";
            this.btnEditLevelOK.Size = new System.Drawing.Size(23, 22);
            this.btnEditLevelOK.Text = "Finish editing";
            this.btnEditLevelOK.Click += new System.EventHandler(this.finishEditingLevel);
            // 
            // btnEditLevelCancel
            // 
            this.btnEditLevelCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditLevelCancel.Image = global::ExpertSokoban.Properties.Resources.cancel;
            this.btnEditLevelCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditLevelCancel.Name = "btnEditLevelCancel";
            this.btnEditLevelCancel.Size = new System.Drawing.Size(23, 22);
            this.btnEditLevelCancel.Text = "Cancel editing";
            this.btnEditLevelCancel.Click += new System.EventHandler(this.cancelEditingLevel);
            // 
            // toolFileEdit
            // 
            this.toolFileEdit.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolFileEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFileEditNewLevel,
            this.btnFileEditEditLevel,
            this.btnFileEditAddComment,
            this.btnFileEditDeleteLevel});
            this.toolFileEdit.Location = new System.Drawing.Point(0, 60);
            this.toolFileEdit.Name = "toolFileEdit";
            this.toolFileEdit.Size = new System.Drawing.Size(147, 25);
            this.toolFileEdit.TabIndex = 4;
            this.toolFileEdit.Text = "File edit toolbar";
            // 
            // btnFileEditNewLevel
            // 
            this.btnFileEditNewLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileEditNewLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_add;
            this.btnFileEditNewLevel.Name = "btnFileEditNewLevel";
            this.btnFileEditNewLevel.Size = new System.Drawing.Size(23, 22);
            this.btnFileEditNewLevel.Text = "Create new level";
            this.btnFileEditNewLevel.Click += new System.EventHandler(this.createLevel);
            // 
            // btnFileEditEditLevel
            // 
            this.btnFileEditEditLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileEditEditLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_edit;
            this.btnFileEditEditLevel.Name = "btnFileEditEditLevel";
            this.btnFileEditEditLevel.Size = new System.Drawing.Size(23, 22);
            this.btnFileEditEditLevel.Text = "Edit level";
            this.btnFileEditEditLevel.Click += new System.EventHandler(this.editLevel);
            // 
            // btnFileEditAddComment
            // 
            this.btnFileEditAddComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileEditAddComment.Image = global::ExpertSokoban.Properties.Resources.comment;
            this.btnFileEditAddComment.Name = "btnFileEditAddComment";
            this.btnFileEditAddComment.Size = new System.Drawing.Size(23, 22);
            this.btnFileEditAddComment.Text = "Add a comment";
            this.btnFileEditAddComment.Click += new System.EventHandler(this.addComment);
            // 
            // btnFileEditDeleteLevel
            // 
            this.btnFileEditDeleteLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileEditDeleteLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_del;
            this.btnFileEditDeleteLevel.Name = "btnFileEditDeleteLevel";
            this.btnFileEditDeleteLevel.Size = new System.Drawing.Size(23, 22);
            this.btnFileEditDeleteLevel.Text = "Delete selected level or comment";
            this.btnFileEditDeleteLevel.Click += new System.EventHandler(this.deleteLevelOrComment);
            // 
            // toolFile
            // 
            this.toolFile.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFileNew,
            this.btnFileOpen,
            this.btnFileSave,
            this.sepToolFile,
            this.btnFileCut,
            this.btnFileCopy,
            this.btnFilePaste});
            this.toolFile.Location = new System.Drawing.Point(0, 35);
            this.toolFile.Name = "toolFile";
            this.toolFile.Size = new System.Drawing.Size(147, 25);
            this.toolFile.TabIndex = 0;
            this.toolFile.Text = "File toolbar";
            // 
            // btnFileNew
            // 
            this.btnFileNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileNew.Image = global::ExpertSokoban.Properties.Resources.new_;
            this.btnFileNew.Name = "btnFileNew";
            this.btnFileNew.Size = new System.Drawing.Size(23, 22);
            this.btnFileNew.Text = "New level file";
            this.btnFileNew.Click += new System.EventHandler(this.newLevelFile);
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileOpen.Image = global::ExpertSokoban.Properties.Resources.open;
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(23, 22);
            this.btnFileOpen.Text = "Open level file";
            this.btnFileOpen.Click += new System.EventHandler(this.openLevelFile);
            // 
            // btnFileSave
            // 
            this.btnFileSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileSave.Image = global::ExpertSokoban.Properties.Resources.save;
            this.btnFileSave.Name = "btnFileSave";
            this.btnFileSave.Size = new System.Drawing.Size(23, 22);
            this.btnFileSave.Text = "Save level file";
            this.btnFileSave.Click += new System.EventHandler(this.saveLevelFile);
            // 
            // sepToolFile
            // 
            this.sepToolFile.Name = "sepToolFile";
            this.sepToolFile.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFileCut
            // 
            this.btnFileCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileCut.Image = global::ExpertSokoban.Properties.Resources.cut;
            this.btnFileCut.Name = "btnFileCut";
            this.btnFileCut.Size = new System.Drawing.Size(23, 22);
            this.btnFileCut.Text = "Cut";
            this.btnFileCut.Click += new System.EventHandler(this.cut);
            // 
            // btnFileCopy
            // 
            this.btnFileCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFileCopy.Image = global::ExpertSokoban.Properties.Resources.copy;
            this.btnFileCopy.Name = "btnFileCopy";
            this.btnFileCopy.Size = new System.Drawing.Size(23, 22);
            this.btnFileCopy.Text = "Copy";
            this.btnFileCopy.Click += new System.EventHandler(this.copy);
            // 
            // btnFilePaste
            // 
            this.btnFilePaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilePaste.Image = global::ExpertSokoban.Properties.Resources.paste;
            this.btnFilePaste.Name = "btnFilePaste";
            this.btnFilePaste.Size = new System.Drawing.Size(23, 22);
            this.btnFilePaste.Text = "Paste";
            this.btnFilePaste.Click += new System.EventHandler(this.paste);
            // 
            // toolPlay
            // 
            this.toolPlay.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolPlay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlayOpenLevel,
            this.sepPlay1,
            this.btnPlayPrevLevel,
            this.btnPlayNextLevel,
            this.sepPlay2,
            this.btnPlayPrevUnsolvedLevel,
            this.btnPlayNextUnsolvedLevel});
            this.toolPlay.Location = new System.Drawing.Point(0, 10);
            this.toolPlay.Name = "toolPlay";
            this.toolPlay.Size = new System.Drawing.Size(147, 25);
            this.toolPlay.TabIndex = 6;
            this.toolPlay.Text = "Playing toolbar";
            // 
            // btnPlayOpenLevel
            // 
            this.btnPlayOpenLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlayOpenLevel.Image = global::ExpertSokoban.Properties.Resources.open;
            this.btnPlayOpenLevel.Name = "btnPlayOpenLevel";
            this.btnPlayOpenLevel.Size = new System.Drawing.Size(23, 22);
            this.btnPlayOpenLevel.Text = "Open level file";
            this.btnPlayOpenLevel.Click += new System.EventHandler(this.openLevelFile);
            // 
            // sepPlay1
            // 
            this.sepPlay1.Name = "sepPlay1";
            this.sepPlay1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPlayPrevLevel
            // 
            this.btnPlayPrevLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlayPrevLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_prev;
            this.btnPlayPrevLevel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPlayPrevLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayPrevLevel.Name = "btnPlayPrevLevel";
            this.btnPlayPrevLevel.Size = new System.Drawing.Size(23, 22);
            this.btnPlayPrevLevel.Text = "Previous level";
            this.btnPlayPrevLevel.Click += new System.EventHandler(this.prevLevel);
            // 
            // btnPlayNextLevel
            // 
            this.btnPlayNextLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlayNextLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_next;
            this.btnPlayNextLevel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPlayNextLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayNextLevel.Name = "btnPlayNextLevel";
            this.btnPlayNextLevel.Size = new System.Drawing.Size(23, 22);
            this.btnPlayNextLevel.Text = "Next level";
            this.btnPlayNextLevel.Click += new System.EventHandler(this.nextLevel);
            // 
            // sepPlay2
            // 
            this.sepPlay2.Name = "sepPlay2";
            this.sepPlay2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPlayPrevUnsolvedLevel
            // 
            this.btnPlayPrevUnsolvedLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlayPrevUnsolvedLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_prev_unsolved;
            this.btnPlayPrevUnsolvedLevel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPlayPrevUnsolvedLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayPrevUnsolvedLevel.Name = "btnPlayPrevUnsolvedLevel";
            this.btnPlayPrevUnsolvedLevel.Size = new System.Drawing.Size(23, 22);
            this.btnPlayPrevUnsolvedLevel.Text = "Previous unsolved level";
            this.btnPlayPrevUnsolvedLevel.Click += new System.EventHandler(this.prevUnsolvedLevel);
            // 
            // btnPlayNextUnsolvedLevel
            // 
            this.btnPlayNextUnsolvedLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlayNextUnsolvedLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_next_unsolved;
            this.btnPlayNextUnsolvedLevel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPlayNextUnsolvedLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayNextUnsolvedLevel.Name = "btnPlayNextUnsolvedLevel";
            this.btnPlayNextUnsolvedLevel.Size = new System.Drawing.Size(23, 22);
            this.btnPlayNextUnsolvedLevel.Text = "Next unsolved level";
            this.btnPlayNextUnsolvedLevel.Click += new System.EventHandler(this.nextUnsolvedLevel);
            // 
            // pnlCloseLevelList
            // 
            this.pnlCloseLevelList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCloseLevelList.Location = new System.Drawing.Point(0, 0);
            this.pnlCloseLevelList.Name = "pnlCloseLevelList";
            this.pnlCloseLevelList.Size = new System.Drawing.Size(147, 10);
            this.pnlCloseLevelList.TabIndex = 5;
            this.pnlCloseLevelList.Tag = "notranslate";
            this.pnlCloseLevelList.CloseClicked += new System.EventHandler(this.toggleLevelList);
            // 
            // mnuMain
            // 
            this.mnuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLevel,
            this.mnuEdit,
            this.mnuOptions,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(645, 24);
            this.mnuMain.TabIndex = 8;
            this.mnuMain.Text = "Main menu";
            // 
            // mnuLevel
            // 
            this.mnuLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLevelNew,
            this.mnuLevelOpen,
            this.mnuLevelSave,
            this.mnuLevelSaveAs,
            this.mnuLevelSep1,
            this.mnuLevelUndo,
            this.mnuLevelRedo,
            this.mnuLevelRetry,
            this.mnuLevelHighscores,
            this.mnuLevelSep2,
            this.mnuLevelPrevious,
            this.mnuLevelNext,
            this.mnuLevelPreviousUnsolved,
            this.mnuLevelNextUnsolved,
            this.mnuLevelSep3,
            this.mnuLevelChangePlayer,
            this.mnuLevelSep4,
            this.mnuLevelExit,
            this.mnuLevelUnusedHotkeys,
            this.mnuUnusedCTRLShortcuts});
            this.mnuLevel.Name = "mnuLevel";
            this.mnuLevel.Size = new System.Drawing.Size(46, 20);
            this.mnuLevel.Text = "&Level";
            // 
            // mnuLevelNew
            // 
            this.mnuLevelNew.Image = global::ExpertSokoban.Properties.Resources.new_;
            this.mnuLevelNew.Name = "mnuLevelNew";
            this.mnuLevelNew.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuLevelNew.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelNew.Text = "&New level file";
            this.mnuLevelNew.Click += new System.EventHandler(this.newLevelFile);
            // 
            // mnuLevelOpen
            // 
            this.mnuLevelOpen.Image = global::ExpertSokoban.Properties.Resources.open;
            this.mnuLevelOpen.Name = "mnuLevelOpen";
            this.mnuLevelOpen.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuLevelOpen.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelOpen.Text = "&Open level file...";
            this.mnuLevelOpen.Click += new System.EventHandler(this.openLevelFile);
            // 
            // mnuLevelSave
            // 
            this.mnuLevelSave.Image = global::ExpertSokoban.Properties.Resources.save;
            this.mnuLevelSave.Name = "mnuLevelSave";
            this.mnuLevelSave.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuLevelSave.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelSave.Text = "&Save level file";
            this.mnuLevelSave.Click += new System.EventHandler(this.saveLevelFile);
            // 
            // mnuLevelSaveAs
            // 
            this.mnuLevelSaveAs.Name = "mnuLevelSaveAs";
            this.mnuLevelSaveAs.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelSaveAs.Text = "Save level file &as...";
            this.mnuLevelSaveAs.Click += new System.EventHandler(this.saveLevelFileAs);
            // 
            // mnuLevelSep1
            // 
            this.mnuLevelSep1.Name = "mnuLevelSep1";
            this.mnuLevelSep1.Size = new System.Drawing.Size(236, 6);
            // 
            // mnuLevelUndo
            // 
            this.mnuLevelUndo.Image = global::ExpertSokoban.Properties.Resources.undo;
            this.mnuLevelUndo.Name = "mnuLevelUndo";
            this.mnuLevelUndo.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.mnuLevelUndo.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelUndo.Text = "&Undo move";
            this.mnuLevelUndo.Click += new System.EventHandler(this.undo);
            // 
            // mnuLevelRedo
            // 
            this.mnuLevelRedo.Image = global::ExpertSokoban.Properties.Resources.redo;
            this.mnuLevelRedo.Name = "mnuLevelRedo";
            this.mnuLevelRedo.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.mnuLevelRedo.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelRedo.Text = "Redo &move";
            this.mnuLevelRedo.Click += new System.EventHandler(this.redo);
            // 
            // mnuLevelRetry
            // 
            this.mnuLevelRetry.Image = global::ExpertSokoban.Properties.Resources.restart;
            this.mnuLevelRetry.Name = "mnuLevelRetry";
            this.mnuLevelRetry.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnuLevelRetry.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelRetry.Text = "&Retry level";
            this.mnuLevelRetry.Click += new System.EventHandler(this.retryLevel);
            // 
            // mnuLevelHighscores
            // 
            this.mnuLevelHighscores.Name = "mnuLevelHighscores";
            this.mnuLevelHighscores.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.mnuLevelHighscores.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelHighscores.Text = "Show &highscores";
            this.mnuLevelHighscores.Click += new System.EventHandler(this.showHighscores);
            // 
            // mnuLevelSep2
            // 
            this.mnuLevelSep2.Name = "mnuLevelSep2";
            this.mnuLevelSep2.Size = new System.Drawing.Size(236, 6);
            // 
            // mnuLevelPrevious
            // 
            this.mnuLevelPrevious.Image = global::ExpertSokoban.Properties.Resources.lvl_prev;
            this.mnuLevelPrevious.Name = "mnuLevelPrevious";
            this.mnuLevelPrevious.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.mnuLevelPrevious.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelPrevious.Text = "&Previous level";
            this.mnuLevelPrevious.Click += new System.EventHandler(this.prevLevel);
            // 
            // mnuLevelNext
            // 
            this.mnuLevelNext.Image = global::ExpertSokoban.Properties.Resources.lvl_next;
            this.mnuLevelNext.Name = "mnuLevelNext";
            this.mnuLevelNext.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.mnuLevelNext.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelNext.Text = "N&ext level";
            this.mnuLevelNext.Click += new System.EventHandler(this.nextLevel);
            // 
            // mnuLevelPreviousUnsolved
            // 
            this.mnuLevelPreviousUnsolved.Image = global::ExpertSokoban.Properties.Resources.lvl_prev_unsolved;
            this.mnuLevelPreviousUnsolved.Name = "mnuLevelPreviousUnsolved";
            this.mnuLevelPreviousUnsolved.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mnuLevelPreviousUnsolved.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelPreviousUnsolved.Text = "Pre&vious unsolved level";
            this.mnuLevelPreviousUnsolved.Click += new System.EventHandler(this.prevUnsolvedLevel);
            // 
            // mnuLevelNextUnsolved
            // 
            this.mnuLevelNextUnsolved.Image = global::ExpertSokoban.Properties.Resources.lvl_next_unsolved;
            this.mnuLevelNextUnsolved.Name = "mnuLevelNextUnsolved";
            this.mnuLevelNextUnsolved.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuLevelNextUnsolved.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelNextUnsolved.Text = "Next unsolve&d level";
            this.mnuLevelNextUnsolved.Click += new System.EventHandler(this.nextUnsolvedLevel);
            // 
            // mnuLevelSep3
            // 
            this.mnuLevelSep3.Name = "mnuLevelSep3";
            this.mnuLevelSep3.Size = new System.Drawing.Size(236, 6);
            // 
            // mnuLevelChangePlayer
            // 
            this.mnuLevelChangePlayer.Name = "mnuLevelChangePlayer";
            this.mnuLevelChangePlayer.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.mnuLevelChangePlayer.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelChangePlayer.Text = "&Change player name...";
            this.mnuLevelChangePlayer.Click += new System.EventHandler(this.changePlayer);
            // 
            // mnuLevelSep4
            // 
            this.mnuLevelSep4.Name = "mnuLevelSep4";
            this.mnuLevelSep4.Size = new System.Drawing.Size(236, 6);
            // 
            // mnuLevelExit
            // 
            this.mnuLevelExit.Name = "mnuLevelExit";
            this.mnuLevelExit.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelExit.Text = "E&xit";
            this.mnuLevelExit.Click += new System.EventHandler(this.exit);
            // 
            // mnuLevelUnusedHotkeys
            // 
            this.mnuLevelUnusedHotkeys.Enabled = false;
            this.mnuLevelUnusedHotkeys.Name = "mnuLevelUnusedHotkeys";
            this.mnuLevelUnusedHotkeys.Size = new System.Drawing.Size(239, 22);
            this.mnuLevelUnusedHotkeys.Tag = "notranslate";
            this.mnuLevelUnusedHotkeys.Text = "Unused hotkeys: bfgijklqtwyz";
            this.mnuLevelUnusedHotkeys.Visible = false;
            // 
            // mnuUnusedCTRLShortcuts
            // 
            this.mnuUnusedCTRLShortcuts.Enabled = false;
            this.mnuUnusedCTRLShortcuts.Name = "mnuUnusedCTRLShortcuts";
            this.mnuUnusedCTRLShortcuts.Size = new System.Drawing.Size(239, 22);
            this.mnuUnusedCTRLShortcuts.Tag = "notranslate";
            this.mnuUnusedCTRLShortcuts.Text = "Unused CTRL shortcuts: aq";
            this.mnuUnusedCTRLShortcuts.Visible = false;
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCreateLevel,
            this.mnuEditEditLevel,
            this.mnuEditAddComment,
            this.mnuEditDelete,
            this.mnuEditSep1,
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.mnuEditSep2,
            this.mnuEditFinish,
            this.mnuEditCancel,
            this.mnuEditSep3,
            this.mnuEditWall,
            this.mnuEditPiece,
            this.mnuEditTarget,
            this.mnuEditSokoban,
            this.mnuEditUnusedHotkeys});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCreateLevel
            // 
            this.mnuEditCreateLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_add;
            this.mnuEditCreateLevel.Name = "mnuEditCreateLevel";
            this.mnuEditCreateLevel.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.mnuEditCreateLevel.Size = new System.Drawing.Size(237, 22);
            this.mnuEditCreateLevel.Text = "Create &new level";
            this.mnuEditCreateLevel.Click += new System.EventHandler(this.createLevel);
            // 
            // mnuEditEditLevel
            // 
            this.mnuEditEditLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_edit;
            this.mnuEditEditLevel.Name = "mnuEditEditLevel";
            this.mnuEditEditLevel.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuEditEditLevel.Size = new System.Drawing.Size(237, 22);
            this.mnuEditEditLevel.Text = "&Edit level";
            this.mnuEditEditLevel.Click += new System.EventHandler(this.editLevel);
            // 
            // mnuEditAddComment
            // 
            this.mnuEditAddComment.Image = global::ExpertSokoban.Properties.Resources.comment;
            this.mnuEditAddComment.Name = "mnuEditAddComment";
            this.mnuEditAddComment.ShortcutKeyDisplayString = "";
            this.mnuEditAddComment.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mnuEditAddComment.Size = new System.Drawing.Size(237, 22);
            this.mnuEditAddComment.Text = "Add a co&mment...";
            this.mnuEditAddComment.Click += new System.EventHandler(this.addComment);
            // 
            // mnuEditDelete
            // 
            this.mnuEditDelete.Image = global::ExpertSokoban.Properties.Resources.lvl_del;
            this.mnuEditDelete.Name = "mnuEditDelete";
            this.mnuEditDelete.ShortcutKeyDisplayString = "";
            this.mnuEditDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuEditDelete.Size = new System.Drawing.Size(237, 22);
            this.mnuEditDelete.Text = "&Delete level/comment";
            this.mnuEditDelete.Click += new System.EventHandler(this.deleteLevelOrComment);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(234, 6);
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::ExpertSokoban.Properties.Resources.cut;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuEditCut.Size = new System.Drawing.Size(237, 22);
            this.mnuEditCut.Text = "C&ut";
            this.mnuEditCut.Click += new System.EventHandler(this.cut);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::ExpertSokoban.Properties.Resources.copy;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuEditCopy.Size = new System.Drawing.Size(237, 22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.copy);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::ExpertSokoban.Properties.Resources.paste;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuEditPaste.Size = new System.Drawing.Size(237, 22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.paste);
            // 
            // mnuEditSep2
            // 
            this.mnuEditSep2.Name = "mnuEditSep2";
            this.mnuEditSep2.Size = new System.Drawing.Size(234, 6);
            // 
            // mnuEditFinish
            // 
            this.mnuEditFinish.Image = global::ExpertSokoban.Properties.Resources.ok;
            this.mnuEditFinish.Name = "mnuEditFinish";
            this.mnuEditFinish.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)));
            this.mnuEditFinish.Size = new System.Drawing.Size(237, 22);
            this.mnuEditFinish.Text = "&Finish editing";
            this.mnuEditFinish.Click += new System.EventHandler(this.finishEditingLevel);
            // 
            // mnuEditCancel
            // 
            this.mnuEditCancel.Image = global::ExpertSokoban.Properties.Resources.cancel;
            this.mnuEditCancel.Name = "mnuEditCancel";
            this.mnuEditCancel.ShortcutKeyDisplayString = "";
            this.mnuEditCancel.Size = new System.Drawing.Size(237, 22);
            this.mnuEditCancel.Text = "C&ancel editing";
            this.mnuEditCancel.Click += new System.EventHandler(this.cancelEditingLevel);
            // 
            // mnuEditSep3
            // 
            this.mnuEditSep3.Name = "mnuEditSep3";
            this.mnuEditSep3.Size = new System.Drawing.Size(234, 6);
            // 
            // mnuEditUnusedHotkeys
            // 
            this.mnuEditUnusedHotkeys.Enabled = false;
            this.mnuEditUnusedHotkeys.Name = "mnuEditUnusedHotkeys";
            this.mnuEditUnusedHotkeys.Size = new System.Drawing.Size(237, 22);
            this.mnuEditUnusedHotkeys.Tag = "notranslate";
            this.mnuEditUnusedHotkeys.Text = "Unused hotkeys: bghjkloqrvxyz";
            this.mnuEditUnusedHotkeys.Visible = false;
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptionsLevelList,
            this.mnuOptionsPlayingToolbar,
            this.mnuOptionsFileToolbars,
            this.mnuOptionsEditLevelToolbar,
            this.mnuOptionsStatusBar,
            this.mnuOptionsSep1,
            this.mnuOptionsMoveNo,
            this.mnuOptionsMoveLine,
            this.mnuOptionsMoveDots,
            this.mnuOptionsMoveArrows,
            this.mnuOptionsSep2,
            this.mnuOptionsPushNo,
            this.mnuOptionsPushLine,
            this.mnuOptionsPushDots,
            this.mnuOptionsPushArrows,
            this.mnuOptionsSep3,
            this.mnuOptionsEndPos,
            this.mnuOptionsAreaSokoban,
            this.mnuOptionsAreaPiece,
            this.mnuOptionsSep4,
            this.mnuOptionsLetterControl,
            this.mnuOptionsLetterControlNext,
            this.mnuOptionsSound,
            this.mnuOptionsAnimation,
            this.mnuOptionsChangeLanguage,
            this.mnuOptionsUnusedHotkeys});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "&Options";
            // 
            // mnuOptionsLevelList
            // 
            this.mnuOptionsLevelList.Name = "mnuOptionsLevelList";
            this.mnuOptionsLevelList.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.mnuOptionsLevelList.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsLevelList.Text = "Display &level list";
            this.mnuOptionsLevelList.Click += new System.EventHandler(this.toggleLevelList);
            // 
            // mnuOptionsPlayingToolbar
            // 
            this.mnuOptionsPlayingToolbar.Name = "mnuOptionsPlayingToolbar";
            this.mnuOptionsPlayingToolbar.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsPlayingToolbar.Text = "Display pla&ying toolbar";
            this.mnuOptionsPlayingToolbar.Click += new System.EventHandler(this.togglePlayingToolbar);
            // 
            // mnuOptionsFileToolbars
            // 
            this.mnuOptionsFileToolbars.Name = "mnuOptionsFileToolbars";
            this.mnuOptionsFileToolbars.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsFileToolbars.Text = "Display &editing toolbars (level file)";
            this.mnuOptionsFileToolbars.Click += new System.EventHandler(this.toggleFileEditToolbar);
            // 
            // mnuOptionsEditLevelToolbar
            // 
            this.mnuOptionsEditLevelToolbar.Name = "mnuOptionsEditLevelToolbar";
            this.mnuOptionsEditLevelToolbar.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsEditLevelToolbar.Text = "Display editin&g toolbar (level)";
            this.mnuOptionsEditLevelToolbar.Click += new System.EventHandler(this.toggleEditLevelToolbar);
            // 
            // mnuOptionsStatusBar
            // 
            this.mnuOptionsStatusBar.Name = "mnuOptionsStatusBar";
            this.mnuOptionsStatusBar.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.mnuOptionsStatusBar.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsStatusBar.Text = "Display stat&us bar";
            this.mnuOptionsStatusBar.Click += new System.EventHandler(this.toggleStatusBar);
            // 
            // mnuOptionsSep1
            // 
            this.mnuOptionsSep1.Name = "mnuOptionsSep1";
            this.mnuOptionsSep1.Size = new System.Drawing.Size(295, 6);
            // 
            // mnuOptionsSep2
            // 
            this.mnuOptionsSep2.Name = "mnuOptionsSep2";
            this.mnuOptionsSep2.Size = new System.Drawing.Size(295, 6);
            // 
            // mnuOptionsSep3
            // 
            this.mnuOptionsSep3.Name = "mnuOptionsSep3";
            this.mnuOptionsSep3.Size = new System.Drawing.Size(295, 6);
            // 
            // mnuOptionsEndPos
            // 
            this.mnuOptionsEndPos.Name = "mnuOptionsEndPos";
            this.mnuOptionsEndPos.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsEndPos.Text = "Display end p&osition of Sokoban and piece";
            this.mnuOptionsEndPos.Click += new System.EventHandler(this.changeEndPosOption);
            // 
            // mnuOptionsAreaSokoban
            // 
            this.mnuOptionsAreaSokoban.Name = "mnuOptionsAreaSokoban";
            this.mnuOptionsAreaSokoban.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsAreaSokoban.Text = "Display reac&hable area for Sokoban";
            this.mnuOptionsAreaSokoban.Click += new System.EventHandler(this.toggleReachableAreaSokoban);
            // 
            // mnuOptionsAreaPiece
            // 
            this.mnuOptionsAreaPiece.Name = "mnuOptionsAreaPiece";
            this.mnuOptionsAreaPiece.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsAreaPiece.Text = "Display reachable area &for piece";
            this.mnuOptionsAreaPiece.Click += new System.EventHandler(this.toggleReachableAreaPiece);
            // 
            // mnuOptionsSep4
            // 
            this.mnuOptionsSep4.Name = "mnuOptionsSep4";
            this.mnuOptionsSep4.Size = new System.Drawing.Size(295, 6);
            // 
            // mnuOptionsLetterControl
            // 
            this.mnuOptionsLetterControl.Name = "mnuOptionsLetterControl";
            this.mnuOptionsLetterControl.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.mnuOptionsLetterControl.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsLetterControl.Text = "Enable lette&r control";
            this.mnuOptionsLetterControl.Click += new System.EventHandler(this.toggleLettering);
            // 
            // mnuOptionsLetterControlNext
            // 
            this.mnuOptionsLetterControlNext.Name = "mnuOptionsLetterControlNext";
            this.mnuOptionsLetterControlNext.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuOptionsLetterControlNext.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsLetterControlNext.Text = "Show ne&xt letter control set";
            this.mnuOptionsLetterControlNext.Click += new System.EventHandler(this.showNextLetterControlSet);
            // 
            // mnuOptionsSound
            // 
            this.mnuOptionsSound.Name = "mnuOptionsSound";
            this.mnuOptionsSound.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsSound.Text = "Enable &sound";
            this.mnuOptionsSound.Click += new System.EventHandler(this.toggleSound);
            // 
            // mnuOptionsAnimation
            // 
            this.mnuOptionsAnimation.Name = "mnuOptionsAnimation";
            this.mnuOptionsAnimation.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsAnimation.Text = "Enable &animations";
            this.mnuOptionsAnimation.Click += new System.EventHandler(this.toggleAnimation);
            // 
            // mnuOptionsChangeLanguage
            // 
            this.mnuOptionsChangeLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyItemToolStripMenuItem});
            this.mnuOptionsChangeLanguage.Name = "mnuOptionsChangeLanguage";
            this.mnuOptionsChangeLanguage.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsChangeLanguage.Text = "&Change language";
            // 
            // dummyItemToolStripMenuItem
            // 
            this.dummyItemToolStripMenuItem.Name = "dummyItemToolStripMenuItem";
            this.dummyItemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dummyItemToolStripMenuItem.Tag = "notranslate";
            this.dummyItemToolStripMenuItem.Text = "dummy item";
            // 
            // mnuOptionsUnusedHotkeys
            // 
            this.mnuOptionsUnusedHotkeys.Enabled = false;
            this.mnuOptionsUnusedHotkeys.Name = "mnuOptionsUnusedHotkeys";
            this.mnuOptionsUnusedHotkeys.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsUnusedHotkeys.Tag = "notranslate";
            this.mnuOptionsUnusedHotkeys.Text = "Unused hotkeys: bjkqz";
            this.mnuOptionsUnusedHotkeys.Visible = false;
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpHelp,
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpHelp
            // 
            this.mnuHelpHelp.Image = global::ExpertSokoban.Properties.Resources.help;
            this.mnuHelpHelp.Name = "mnuHelpHelp";
            this.mnuHelpHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnuHelpHelp.Size = new System.Drawing.Size(127, 22);
            this.mnuHelpHelp.Text = "&Help...";
            this.mnuHelpHelp.Click += new System.EventHandler(this.help);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(127, 22);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.helpAbout);
            // 
            // ctMainToolStripContainer
            // 
            // 
            // ctMainToolStripContainer.BottomToolStripPanel
            // 
            this.ctMainToolStripContainer.BottomToolStripPanel.Controls.Add(this.ctStatusBar);
            // 
            // ctMainToolStripContainer.ContentPanel
            // 
            this.ctMainToolStripContainer.ContentPanel.AutoScroll = true;
            this.ctMainToolStripContainer.ContentPanel.Controls.Add(this.ctMainArea);
            this.ctMainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(645, 527);
            this.ctMainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctMainToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.ctMainToolStripContainer.Name = "ctMainToolStripContainer";
            this.ctMainToolStripContainer.Size = new System.Drawing.Size(645, 573);
            this.ctMainToolStripContainer.TabIndex = 9;
            // 
            // ctMainToolStripContainer.TopToolStripPanel
            // 
            this.ctMainToolStripContainer.TopToolStripPanel.Controls.Add(this.mnuMain);
            // 
            // ctStatusBar
            // 
            this.ctStatusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.ctStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusMoves,
            this.lblStatusPushes,
            this.lblStatusPieces,
            this.lblStatusEdit,
            this.lblStatusSolved,
            this.lblStatusNull});
            this.ctStatusBar.Location = new System.Drawing.Point(0, 0);
            this.ctStatusBar.Name = "ctStatusBar";
            this.ctStatusBar.Size = new System.Drawing.Size(645, 22);
            this.ctStatusBar.SizingGrip = false;
            this.ctStatusBar.TabIndex = 0;
            // 
            // lblStatusMoves
            // 
            this.lblStatusMoves.Name = "lblStatusMoves";
            this.lblStatusMoves.Size = new System.Drawing.Size(54, 17);
            this.lblStatusMoves.Tag = "notranslate";
            this.lblStatusMoves.Text = "Moves: 0";
            this.lblStatusMoves.Visible = false;
            // 
            // lblStatusPushes
            // 
            this.lblStatusPushes.Name = "lblStatusPushes";
            this.lblStatusPushes.Size = new System.Drawing.Size(56, 17);
            this.lblStatusPushes.Tag = "notranslate";
            this.lblStatusPushes.Text = "Pushes: 0";
            this.lblStatusPushes.Visible = false;
            // 
            // lblStatusPieces
            // 
            this.lblStatusPieces.Name = "lblStatusPieces";
            this.lblStatusPieces.Size = new System.Drawing.Size(112, 17);
            this.lblStatusPieces.Tag = "notranslate";
            this.lblStatusPieces.Text = "Remaining pieces: 0";
            this.lblStatusPieces.Visible = false;
            // 
            // lblStatusEdit
            // 
            this.lblStatusEdit.Name = "lblStatusEdit";
            this.lblStatusEdit.Size = new System.Drawing.Size(189, 17);
            this.lblStatusEdit.Tag = "notranslate";
            this.lblStatusEdit.Text = "You are currently editing this level.";
            this.lblStatusEdit.Visible = false;
            // 
            // lblStatusSolved
            // 
            this.lblStatusSolved.Name = "lblStatusSolved";
            this.lblStatusSolved.Size = new System.Drawing.Size(234, 17);
            this.lblStatusSolved.Text = "You have solved the level. Congratulations!";
            this.lblStatusSolved.Visible = false;
            // 
            // lblStatusNull
            // 
            this.lblStatusNull.Name = "lblStatusNull";
            this.lblStatusNull.Size = new System.Drawing.Size(360, 17);
            this.lblStatusNull.Text = "No levels currently selected. Select a level from the level list to play.";
            this.lblStatusNull.Visible = false;
            // 
            // ctLevelListSplitter
            // 
            this.ctLevelListSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctLevelListSplitter.Location = new System.Drawing.Point(642, 0);
            this.ctLevelListSplitter.MinSize = 50;
            this.ctLevelListSplitter.Name = "ctLevelListSplitter";
            this.ctLevelListSplitter.Size = new System.Drawing.Size(3, 573);
            this.ctLevelListSplitter.TabIndex = 10;
            this.ctLevelListSplitter.TabStop = false;
            this.ctLevelListSplitter.Visible = false;
            // 
            // tmrBugWorkaround
            // 
            this.tmrBugWorkaround.Enabled = true;
            this.tmrBugWorkaround.Tick += new System.EventHandler(this.bugWorkaround);
            // 
            // tmrUpdateControls
            // 
            this.tmrUpdateControls.Enabled = true;
            this.tmrUpdateControls.Interval = 10;
            this.tmrUpdateControls.Tick += new System.EventHandler(this.updateControls);
            // 
            // ctMainArea
            // 
            this.ctMainArea.AnimationEnabled = false;
            this.ctMainArea.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (255)))), ((int) (((byte) (255)))), ((int) (((byte) (206)))));
            this.ctMainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctMainArea.LetteringEnabled = false;
            this.ctMainArea.Location = new System.Drawing.Point(0, 0);
            this.ctMainArea.Modified = false;
            this.ctMainArea.MoveDrawMode = ExpertSokoban.PathDrawMode.Line;
            this.ctMainArea.Name = "ctMainArea";
            this.ctMainArea.PushDrawMode = ExpertSokoban.PathDrawMode.Arrows;
            this.ctMainArea.RefreshOnResize = true;
            this.ctMainArea.ShowAreaPiece = false;
            this.ctMainArea.ShowAreaSokoban = false;
            this.ctMainArea.ShowEndPos = false;
            this.ctMainArea.Size = new System.Drawing.Size(645, 527);
            this.ctMainArea.SoundEnabled = false;
            this.ctMainArea.TabIndex = 1;
            this.ctMainArea.TabStop = true;
            this.ctMainArea.Tool = ExpertSokoban.MainAreaTool.Wall;
            this.ctMainArea.Click += new System.EventHandler(this.mainAreaClick);
            this.ctMainArea.MustSaveLevel += new System.EventHandler(this.saveLevel);
            this.ctMainArea.LevelSolved += new System.EventHandler(this.levelSolved);
            this.ctMainArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainAreaKeyDown);
            // 
            // mnuEditWall
            // 
            this.mnuEditWall.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolBrick;
            this.mnuEditWall.Name = "mnuEditWall";
            this.mnuEditWall.ParentGroup = this.grpEditTool;
            this.mnuEditWall.ShortcutKeyDisplayString = "";
            this.mnuEditWall.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.mnuEditWall.Size = new System.Drawing.Size(237, 22);
            this.mnuEditWall.Text = "&Wall tool";
            this.mnuEditWall.Value = ExpertSokoban.MainAreaTool.Wall;
            // 
            // grpEditTool
            // 
            this.grpEditTool.ValueChanged += new System.EventHandler(this.changeEditTool);
            // 
            // mnuEditPiece
            // 
            this.mnuEditPiece.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolPiece;
            this.mnuEditPiece.Name = "mnuEditPiece";
            this.mnuEditPiece.ParentGroup = this.grpEditTool;
            this.mnuEditPiece.ShortcutKeyDisplayString = "";
            this.mnuEditPiece.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuEditPiece.Size = new System.Drawing.Size(237, 22);
            this.mnuEditPiece.Text = "P&iece tool";
            this.mnuEditPiece.Value = ExpertSokoban.MainAreaTool.Piece;
            // 
            // mnuEditTarget
            // 
            this.mnuEditTarget.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolTarget;
            this.mnuEditTarget.Name = "mnuEditTarget";
            this.mnuEditTarget.ParentGroup = this.grpEditTool;
            this.mnuEditTarget.ShortcutKeyDisplayString = "";
            this.mnuEditTarget.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mnuEditTarget.Size = new System.Drawing.Size(237, 22);
            this.mnuEditTarget.Text = "&Target tool";
            this.mnuEditTarget.Value = ExpertSokoban.MainAreaTool.Target;
            // 
            // mnuEditSokoban
            // 
            this.mnuEditSokoban.Checked = true;
            this.mnuEditSokoban.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuEditSokoban.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolSokoban;
            this.mnuEditSokoban.Name = "mnuEditSokoban";
            this.mnuEditSokoban.ParentGroup = this.grpEditTool;
            this.mnuEditSokoban.ShortcutKeyDisplayString = "";
            this.mnuEditSokoban.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.mnuEditSokoban.Size = new System.Drawing.Size(237, 22);
            this.mnuEditSokoban.Text = "&Sokoban tool";
            this.mnuEditSokoban.Value = ExpertSokoban.MainAreaTool.Sokoban;
            // 
            // mnuOptionsMoveNo
            // 
            this.mnuOptionsMoveNo.Name = "mnuOptionsMoveNo";
            this.mnuOptionsMoveNo.ParentGroup = this.grpMovePathOptions;
            this.mnuOptionsMoveNo.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsMoveNo.Text = "Don\'t display &move path";
            this.mnuOptionsMoveNo.Value = ExpertSokoban.PathDrawMode.None;
            // 
            // grpMovePathOptions
            // 
            this.grpMovePathOptions.ValueChanged += new System.EventHandler(this.changeMovePathOption);
            // 
            // mnuOptionsMoveLine
            // 
            this.mnuOptionsMoveLine.Name = "mnuOptionsMoveLine";
            this.mnuOptionsMoveLine.ParentGroup = this.grpMovePathOptions;
            this.mnuOptionsMoveLine.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsMoveLine.Text = "Display move path as li&ne";
            this.mnuOptionsMoveLine.Value = ExpertSokoban.PathDrawMode.Line;
            // 
            // mnuOptionsMoveDots
            // 
            this.mnuOptionsMoveDots.Name = "mnuOptionsMoveDots";
            this.mnuOptionsMoveDots.ParentGroup = this.grpMovePathOptions;
            this.mnuOptionsMoveDots.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsMoveDots.Text = "Display move path as &dots";
            this.mnuOptionsMoveDots.Value = ExpertSokoban.PathDrawMode.Dots;
            // 
            // mnuOptionsMoveArrows
            // 
            this.mnuOptionsMoveArrows.Name = "mnuOptionsMoveArrows";
            this.mnuOptionsMoveArrows.ParentGroup = this.grpMovePathOptions;
            this.mnuOptionsMoveArrows.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsMoveArrows.Text = "Display mo&ve path as arrows";
            this.mnuOptionsMoveArrows.Value = ExpertSokoban.PathDrawMode.Arrows;
            // 
            // mnuOptionsPushNo
            // 
            this.mnuOptionsPushNo.Name = "mnuOptionsPushNo";
            this.mnuOptionsPushNo.ParentGroup = this.grpPushPathOptions;
            this.mnuOptionsPushNo.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsPushNo.Text = "Don\'t display &push path";
            this.mnuOptionsPushNo.Value = ExpertSokoban.PathDrawMode.None;
            // 
            // grpPushPathOptions
            // 
            this.grpPushPathOptions.ValueChanged += new System.EventHandler(this.changePushPathOption);
            // 
            // mnuOptionsPushLine
            // 
            this.mnuOptionsPushLine.Name = "mnuOptionsPushLine";
            this.mnuOptionsPushLine.ParentGroup = this.grpPushPathOptions;
            this.mnuOptionsPushLine.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsPushLine.Text = "Display push path as l&ine";
            this.mnuOptionsPushLine.Value = ExpertSokoban.PathDrawMode.Line;
            // 
            // mnuOptionsPushDots
            // 
            this.mnuOptionsPushDots.Name = "mnuOptionsPushDots";
            this.mnuOptionsPushDots.ParentGroup = this.grpPushPathOptions;
            this.mnuOptionsPushDots.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsPushDots.Text = "Display push path as do&ts";
            this.mnuOptionsPushDots.Value = ExpertSokoban.PathDrawMode.Dots;
            // 
            // mnuOptionsPushArrows
            // 
            this.mnuOptionsPushArrows.Name = "mnuOptionsPushArrows";
            this.mnuOptionsPushArrows.ParentGroup = this.grpPushPathOptions;
            this.mnuOptionsPushArrows.Size = new System.Drawing.Size(298, 22);
            this.mnuOptionsPushArrows.Text = "Display push path as arro&ws";
            this.mnuOptionsPushArrows.Value = ExpertSokoban.PathDrawMode.Arrows;
            // 
            // lstLevels
            // 
            this.lstLevels.ContextMenuStrip = this.mnuContext;
            this.lstLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLevels.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstLevels.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.lstLevels.IntegralHeight = false;
            this.lstLevels.Location = new System.Drawing.Point(0, 110);
            this.lstLevels.Modified = false;
            this.lstLevels.Name = "lstLevels";
            this.lstLevels.ScrollAlwaysVisible = true;
            this.lstLevels.Size = new System.Drawing.Size(147, 463);
            this.lstLevels.TabIndex = 2;
            this.lstLevels.Tag = "notranslate";
            this.lstLevels.LevelActivating += new ExpertSokoban.ConfirmEventHandler(this.levelActivating);
            this.lstLevels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.levelListKeyDown);
            this.lstLevels.LevelActivated += new System.EventHandler(this.levelActivated);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.ctLevelListSplitter);
            this.Controls.Add(this.ctMainToolStripContainer);
            this.Controls.Add(this.pnlLevelList);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.Icon = global::ExpertSokoban.Properties.Resources.ExpertSokoban;
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Expert Sokoban";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing);
            this.pnlLevelList.ResumeLayout(false);
            this.pnlLevelList.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            this.toolEditLevel.ResumeLayout(false);
            this.toolEditLevel.PerformLayout();
            this.toolFileEdit.ResumeLayout(false);
            this.toolFileEdit.PerformLayout();
            this.toolFile.ResumeLayout(false);
            this.toolFile.PerformLayout();
            this.toolPlay.ResumeLayout(false);
            this.toolPlay.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ctMainToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.ctMainToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.ctMainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.ctMainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.ctMainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.ctMainToolStripContainer.ResumeLayout(false);
            this.ctMainToolStripContainer.PerformLayout();
            this.ctStatusBar.ResumeLayout(false);
            this.ctStatusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolPlay;
        private System.Windows.Forms.ToolStrip toolFile;
        private System.Windows.Forms.ToolStrip toolFileEdit;
        private System.Windows.Forms.ToolStrip toolEditLevel;

        private RT.Util.Controls.NiceClosePanel pnlCloseLevelList;
        private ExpertSokoban.MainArea ctMainArea;
        private System.Windows.Forms.Panel pnlLevelList;
        private ExpertSokoban.LevelListBox lstLevels;
        private System.Windows.Forms.ToolStripButton btnFileOpen;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsLevelList;
        private System.Windows.Forms.ToolStripButton btnFileNew;
        private System.Windows.Forms.ToolStripSeparator sepToolFile;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelNew;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private MenuRadioItemPathDrawMode mnuOptionsMoveNo;
        private MenuRadioItemPathDrawMode mnuOptionsMoveDots;
        private MenuRadioItemPathDrawMode mnuOptionsMoveLine;
        private MenuRadioItemPathDrawMode mnuOptionsMoveArrows;
        private System.Windows.Forms.ToolStripSeparator mnuOptionsSep2;
        private MenuRadioItemPathDrawMode mnuOptionsPushNo;
        private MenuRadioItemPathDrawMode mnuOptionsPushDots;
        private MenuRadioItemPathDrawMode mnuOptionsPushLine;
        private MenuRadioItemPathDrawMode mnuOptionsPushArrows;
        private System.Windows.Forms.ToolStripSeparator mnuOptionsSep3;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsEndPos;
        private System.Windows.Forms.ToolStripMenuItem mnuLevel;
        private System.Windows.Forms.ToolStripSeparator mnuOptionsSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripSeparator mnuLevelSep1;
        private System.Windows.Forms.ToolStripButton btnFileSave;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelSave;
        private System.Windows.Forms.ToolStripContainer ctMainToolStripContainer;
        private System.Windows.Forms.Splitter ctLevelListSplitter;
        private System.Windows.Forms.ToolStripButton btnEditLevelWall;
        private System.Windows.Forms.ToolStripButton btnEditLevelPiece;
        private System.Windows.Forms.ToolStripButton btnEditLevelTarget;
        private System.Windows.Forms.ToolStripButton btnEditLevelSokoban;
        private MenuRadioItemMainAreaTool mnuEditWall;
        private MenuRadioItemMainAreaTool mnuEditPiece;
        private MenuRadioItemMainAreaTool mnuEditTarget;
        private MenuRadioItemMainAreaTool mnuEditSokoban;
        private System.Windows.Forms.ToolStripButton btnEditLevelOK;
        private System.Windows.Forms.ToolStripSeparator btnEditLevelSep;
        private System.Windows.Forms.ToolStripMenuItem mnuEditFinish;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCancel;
        private System.Windows.Forms.ToolStripSeparator mnuEditSep3;
        private System.Windows.Forms.ToolStripButton btnEditLevelCancel;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsPlayingToolbar;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsEditLevelToolbar;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsFileToolbars;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsUnusedHotkeys;
        private System.Windows.Forms.ToolStripButton btnFileEditDeleteLevel;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelExit;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelUndo;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelRetry;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCreateLevel;
        private System.Windows.Forms.ToolStripMenuItem mnuEditEditLevel;
        private System.Windows.Forms.ToolStripMenuItem mnuEditAddComment;
        private System.Windows.Forms.ToolStripSeparator mnuEditSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCut;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPaste;
        private System.Windows.Forms.ToolStripMenuItem mnuEditDelete;
        private System.Windows.Forms.ToolStripSeparator mnuEditSep2;
        private System.Windows.Forms.ToolStripMenuItem mnuEditUnusedHotkeys;
        private System.Windows.Forms.Timer tmrBugWorkaround;
        private ExpertSokoban.MenuRadioGroupPathDrawMode grpMovePathOptions;
        private ExpertSokoban.MenuRadioGroupPathDrawMode grpPushPathOptions;
        private ExpertSokoban.MenuRadioGroupMainAreaTool grpEditTool;
        private System.Windows.Forms.ToolStripSeparator mnuLevelSep2;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelNext;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelNextUnsolved;
        private System.Windows.Forms.ToolStripSeparator mnuLevelSep3;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelPrevious;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelPreviousUnsolved;
        private System.Windows.Forms.ToolStripMenuItem mnuUnusedCTRLShortcuts;
        private System.Windows.Forms.StatusStrip ctStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMoves;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusPushes;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusPieces;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusNull;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusEdit;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusSolved;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsStatusBar;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelChangePlayer;
        private System.Windows.Forms.ToolStripSeparator mnuLevelSep4;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelUnusedHotkeys;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuContextPlay;
        private System.Windows.Forms.ToolStripMenuItem mnuContextEdit;
        private System.Windows.Forms.ToolStripSeparator mnuContextSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuContextNewLevel;
        private System.Windows.Forms.ToolStripMenuItem mnuContextNewComment;
        private System.Windows.Forms.ToolStripSeparator mnuContextSep2;
        private System.Windows.Forms.ToolStripMenuItem mnuContextCut;
        private System.Windows.Forms.ToolStripMenuItem mnuContextCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuContextPaste;
        private System.Windows.Forms.ToolStripMenuItem mnuContextDelete;
        private System.Windows.Forms.ToolStripSeparator mnuContextSep3;
        private System.Windows.Forms.ToolStripMenuItem mnuContextHide;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelRedo;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelSaveAs;
        private System.Windows.Forms.Timer tmrUpdateControls;
        private System.Windows.Forms.ToolStripMenuItem mnuLevelHighscores;
        private System.Windows.Forms.ToolStripMenuItem mnuContextHighscores;
        private System.Windows.Forms.ToolStripButton btnFileEditNewLevel;
        private System.Windows.Forms.ToolStripButton btnFileEditEditLevel;
        private System.Windows.Forms.ToolStripButton btnFileEditAddComment;
        private System.Windows.Forms.ToolStripButton btnFileCut;
        private System.Windows.Forms.ToolStripButton btnFileCopy;
        private System.Windows.Forms.ToolStripButton btnFilePaste;
        private System.Windows.Forms.ToolStripButton btnPlayOpenLevel;
        private System.Windows.Forms.ToolStripButton btnPlayPrevLevel;
        private System.Windows.Forms.ToolStripButton btnPlayNextLevel;
        private System.Windows.Forms.ToolStripButton btnPlayPrevUnsolvedLevel;
        private System.Windows.Forms.ToolStripButton btnPlayNextUnsolvedLevel;
        private System.Windows.Forms.ToolStripSeparator sepPlay1;
        private System.Windows.Forms.ToolStripSeparator sepPlay2;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsAreaSokoban;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsAreaPiece;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsSound;
        private System.Windows.Forms.ToolStripSeparator mnuOptionsSep4;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsChangeLanguage;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsLetterControl;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsLetterControlNext;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsAnimation;
        private System.Windows.Forms.ToolStripMenuItem dummyItemToolStripMenuItem;
    }
}

