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
            this.LevelListPanel = new System.Windows.Forms.Panel();
            this.LevelList = new ExpertSokoban.LevelListBox();
            this.LevelContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextHighscores = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextNewLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextNewComment = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextCopyItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextHide = new System.Windows.Forms.ToolStripMenuItem();
            this.EditLevelToolStrip = new System.Windows.Forms.ToolStrip();
            this.EditToolWall = new System.Windows.Forms.ToolStripButton();
            this.EditToolPiece = new System.Windows.Forms.ToolStripButton();
            this.EditToolTarget = new System.Windows.Forms.ToolStripButton();
            this.EditToolSokoban = new System.Windows.Forms.ToolStripButton();
            this.EditToolSep = new System.Windows.Forms.ToolStripSeparator();
            this.EditToolOK = new System.Windows.Forms.ToolStripButton();
            this.EditToolCancel = new System.Windows.Forms.ToolStripButton();
            this.Edit2ToolStrip = new System.Windows.Forms.ToolStrip();
            this.LevelToolNewLevel = new System.Windows.Forms.ToolStripButton();
            this.LevelToolDelete = new System.Windows.Forms.ToolStripButton();
            this.LevelToolEdit = new System.Windows.Forms.ToolStripButton();
            this.LevelToolComment = new System.Windows.Forms.ToolStripButton();
            this.Edit1ToolStrip = new System.Windows.Forms.ToolStrip();
            this.LevelToolNew = new System.Windows.Forms.ToolStripButton();
            this.LevelToolOpen = new System.Windows.Forms.ToolStripButton();
            this.LevelToolSave = new System.Windows.Forms.ToolStripButton();
            this.LevelToolSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelToolCut = new System.Windows.Forms.ToolStripButton();
            this.LevelToolCopy = new System.Windows.Forms.ToolStripButton();
            this.LevelToolPaste = new System.Windows.Forms.ToolStripButton();
            this.PlayToolStrip = new System.Windows.Forms.ToolStrip();
            this.LevelToolOpen2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelToolPrev = new System.Windows.Forms.ToolStripButton();
            this.LevelToolNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelToolPrevUnsolved = new System.Windows.Forms.ToolStripButton();
            this.LevelToolNextUnsolved = new System.Windows.Forms.ToolStripButton();
            this.LevelListClosePanel = new RT.Util.Controls.NiceClosePanel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.LevelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelNew = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSave = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelRetry = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelHighscores = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelNext = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelPreviousUnsolved = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelNextUnsolved = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelChangePlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.LevelExit = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelUnusedHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.UnusedCTRLShortcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCreateLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.EditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.EditEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.EditAddComment = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.EditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.EditFinish = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.EditWall = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.EditToolOptions = new ExpertSokoban.MenuRadioGroupMainAreaTool();
            this.EditPiece = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.EditTarget = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.EditSokoban = new ExpertSokoban.MenuRadioItemMainAreaTool();
            this.EditUnusedHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsLevelList = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsPlayToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsEditToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsEditLevelToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.OptionsMoveNo = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.MovePathOptions = new ExpertSokoban.MenuRadioGroupPathDrawMode();
            this.OptionsMoveLine = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.OptionsMoveDots = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.OptionsMoveArrows = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.OptionsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.OptionsPushNo = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.PushPathOptions = new ExpertSokoban.MenuRadioGroupPathDrawMode();
            this.OptionsPushLine = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.OptionsPushDots = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.OptionsPushArrows = new ExpertSokoban.MenuRadioItemPathDrawMode();
            this.OptionsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.OptionsEndPos = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsAreaSokoban = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsAreaPiece = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.OptionsSound = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsUnusedHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpKeyboard = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusMoves = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusPushes = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusPieces = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusEdit = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusSolved = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusNull = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainArea = new ExpertSokoban.MainArea();
            this.LevelListSplitter = new System.Windows.Forms.Splitter();
            this.BugWorkaroundTimer = new System.Windows.Forms.Timer(this.components);
            this.UpdateControlsTimer = new System.Windows.Forms.Timer(this.components);
            this.LevelListPanel.SuspendLayout();
            this.LevelContextMenu.SuspendLayout();
            this.EditLevelToolStrip.SuspendLayout();
            this.Edit2ToolStrip.SuspendLayout();
            this.Edit1ToolStrip.SuspendLayout();
            this.PlayToolStrip.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.MainToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.ContentPanel.SuspendLayout();
            this.MainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // LevelListPanel
            // 
            this.LevelListPanel.BackColor = System.Drawing.SystemColors.Control;
            this.LevelListPanel.Controls.Add(this.LevelList);
            this.LevelListPanel.Controls.Add(this.EditLevelToolStrip);
            this.LevelListPanel.Controls.Add(this.Edit2ToolStrip);
            this.LevelListPanel.Controls.Add(this.Edit1ToolStrip);
            this.LevelListPanel.Controls.Add(this.PlayToolStrip);
            this.LevelListPanel.Controls.Add(this.LevelListClosePanel);
            this.LevelListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListPanel.Location = new System.Drawing.Point(485, 0);
            this.LevelListPanel.Name = "LevelListPanel";
            this.LevelListPanel.Size = new System.Drawing.Size(147, 366);
            this.LevelListPanel.TabIndex = 6;
            this.LevelListPanel.Visible = false;
            this.LevelListPanel.Resize += new System.EventHandler(this.LevelListPanel_Resize);
            // 
            // LevelList
            // 
            this.LevelList.ContextMenuStrip = this.LevelContextMenu;
            this.LevelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LevelList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LevelList.IntegralHeight = false;
            this.LevelList.Location = new System.Drawing.Point(0, 110);
            this.LevelList.Modified = false;
            this.LevelList.Name = "LevelList";
            this.LevelList.ScrollAlwaysVisible = true;
            this.LevelList.Size = new System.Drawing.Size(147, 256);
            this.LevelList.TabIndex = 2;
            this.LevelList.LevelActivating += new RT.Util.ConfirmEventHandler(this.LevelList_LevelActivating);
            this.LevelList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LevelList_KeyDown);
            this.LevelList.LevelActivated += new System.EventHandler(this.LevelList_LevelActivated);
            // 
            // LevelContextMenu
            // 
            this.LevelContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextPlay,
            this.ContextEdit,
            this.ContextHighscores,
            this.ContextSep1,
            this.ContextNewLevel,
            this.ContextNewComment,
            this.ContextSep2,
            this.ContextCut,
            this.ContextCopyItem,
            this.ContextPaste,
            this.ContextDelete,
            this.ContextSep3,
            this.ContextHide});
            this.LevelContextMenu.Name = "LevelContextMenu";
            this.LevelContextMenu.Size = new System.Drawing.Size(270, 242);
            // 
            // ContextPlay
            // 
            this.ContextPlay.Name = "ContextPlay";
            this.ContextPlay.ShortcutKeyDisplayString = "Enter";
            this.ContextPlay.Size = new System.Drawing.Size(269, 22);
            this.ContextPlay.Text = "Pl&ay this level";
            this.ContextPlay.Click += new System.EventHandler(this.ContextPlay_Click);
            // 
            // ContextEdit
            // 
            this.ContextEdit.Name = "ContextEdit";
            this.ContextEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ContextEdit.Size = new System.Drawing.Size(269, 22);
            this.ContextEdit.Text = "&Edit this level";
            this.ContextEdit.Click += new System.EventHandler(this.EditEdit_Click);
            // 
            // ContextHighscores
            // 
            this.ContextHighscores.Name = "ContextHighscores";
            this.ContextHighscores.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.ContextHighscores.Size = new System.Drawing.Size(269, 22);
            this.ContextHighscores.Text = "Show &highscores";
            this.ContextHighscores.Click += new System.EventHandler(this.LevelHighscores_Click);
            // 
            // ContextSep1
            // 
            this.ContextSep1.Name = "ContextSep1";
            this.ContextSep1.Size = new System.Drawing.Size(266, 6);
            // 
            // ContextNewLevel
            // 
            this.ContextNewLevel.Name = "ContextNewLevel";
            this.ContextNewLevel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.ContextNewLevel.Size = new System.Drawing.Size(269, 22);
            this.ContextNewLevel.Text = "C&reate a new level here";
            this.ContextNewLevel.Click += new System.EventHandler(this.EditCreateLevel_Click);
            // 
            // ContextNewComment
            // 
            this.ContextNewComment.Name = "ContextNewComment";
            this.ContextNewComment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.ContextNewComment.Size = new System.Drawing.Size(269, 22);
            this.ContextNewComment.Text = "&Insert a comment here";
            this.ContextNewComment.Click += new System.EventHandler(this.EditAddComment_Click);
            // 
            // ContextSep2
            // 
            this.ContextSep2.Name = "ContextSep2";
            this.ContextSep2.Size = new System.Drawing.Size(266, 6);
            // 
            // ContextCut
            // 
            this.ContextCut.Name = "ContextCut";
            this.ContextCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ContextCut.Size = new System.Drawing.Size(269, 22);
            this.ContextCut.Text = "C&ut";
            this.ContextCut.Click += new System.EventHandler(this.EditCut_Click);
            // 
            // ContextCopyItem
            // 
            this.ContextCopyItem.Name = "ContextCopyItem";
            this.ContextCopyItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.ContextCopyItem.Size = new System.Drawing.Size(269, 22);
            this.ContextCopyItem.Text = "&Copy";
            this.ContextCopyItem.Click += new System.EventHandler(this.EditCopy_Click);
            // 
            // ContextPaste
            // 
            this.ContextPaste.Name = "ContextPaste";
            this.ContextPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.ContextPaste.Size = new System.Drawing.Size(269, 22);
            this.ContextPaste.Text = "&Paste";
            this.ContextPaste.Click += new System.EventHandler(this.EditPaste_Click);
            // 
            // ContextDelete
            // 
            this.ContextDelete.Name = "ContextDelete";
            this.ContextDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ContextDelete.Size = new System.Drawing.Size(269, 22);
            this.ContextDelete.Text = "&Delete";
            this.ContextDelete.Click += new System.EventHandler(this.EditDelete_Click);
            // 
            // ContextSep3
            // 
            this.ContextSep3.Name = "ContextSep3";
            this.ContextSep3.Size = new System.Drawing.Size(266, 6);
            // 
            // ContextHide
            // 
            this.ContextHide.Name = "ContextHide";
            this.ContextHide.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ContextHide.Size = new System.Drawing.Size(269, 22);
            this.ContextHide.Text = "Hide &level list";
            this.ContextHide.Click += new System.EventHandler(this.OptionsLevelList_Click);
            // 
            // EditLevelToolStrip
            // 
            this.EditLevelToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.EditLevelToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditToolWall,
            this.EditToolPiece,
            this.EditToolTarget,
            this.EditToolSokoban,
            this.EditToolSep,
            this.EditToolOK,
            this.EditToolCancel});
            this.EditLevelToolStrip.Location = new System.Drawing.Point(0, 85);
            this.EditLevelToolStrip.Name = "EditLevelToolStrip";
            this.EditLevelToolStrip.Size = new System.Drawing.Size(147, 25);
            this.EditLevelToolStrip.TabIndex = 3;
            this.EditLevelToolStrip.Text = "Edit toolbar";
            // 
            // EditToolWall
            // 
            this.EditToolWall.Checked = true;
            this.EditToolWall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EditToolWall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolWall.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolBrick;
            this.EditToolWall.Name = "EditToolWall";
            this.EditToolWall.Size = new System.Drawing.Size(23, 22);
            this.EditToolWall.Text = "Wall tool";
            this.EditToolWall.Click += new System.EventHandler(this.EditTool_Click);
            // 
            // EditToolPiece
            // 
            this.EditToolPiece.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolPiece.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolPiece;
            this.EditToolPiece.Name = "EditToolPiece";
            this.EditToolPiece.Size = new System.Drawing.Size(23, 22);
            this.EditToolPiece.Text = "Piece tool";
            this.EditToolPiece.Click += new System.EventHandler(this.EditTool_Click);
            // 
            // EditToolTarget
            // 
            this.EditToolTarget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolTarget.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolTarget;
            this.EditToolTarget.Name = "EditToolTarget";
            this.EditToolTarget.Size = new System.Drawing.Size(23, 22);
            this.EditToolTarget.Text = "Target tool";
            this.EditToolTarget.Click += new System.EventHandler(this.EditTool_Click);
            // 
            // EditToolSokoban
            // 
            this.EditToolSokoban.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolSokoban.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolSokoban;
            this.EditToolSokoban.Name = "EditToolSokoban";
            this.EditToolSokoban.Size = new System.Drawing.Size(23, 22);
            this.EditToolSokoban.Text = "Sokoban tool";
            this.EditToolSokoban.Click += new System.EventHandler(this.EditTool_Click);
            // 
            // EditToolSep
            // 
            this.EditToolSep.Name = "EditToolSep";
            this.EditToolSep.Size = new System.Drawing.Size(6, 25);
            // 
            // EditToolOK
            // 
            this.EditToolOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolOK.Image = global::ExpertSokoban.Properties.Resources.ok;
            this.EditToolOK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.EditToolOK.Name = "EditToolOK";
            this.EditToolOK.Size = new System.Drawing.Size(23, 22);
            this.EditToolOK.Text = "Finish editing";
            this.EditToolOK.Click += new System.EventHandler(this.EditFinish_Click);
            // 
            // EditToolCancel
            // 
            this.EditToolCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditToolCancel.Image = global::ExpertSokoban.Properties.Resources.cancel;
            this.EditToolCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.EditToolCancel.Name = "EditToolCancel";
            this.EditToolCancel.Size = new System.Drawing.Size(23, 22);
            this.EditToolCancel.Text = "Cancel editing";
            this.EditToolCancel.Click += new System.EventHandler(this.EditCancel_Click);
            // 
            // Edit2ToolStrip
            // 
            this.Edit2ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Edit2ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelToolNewLevel,
            this.LevelToolDelete,
            this.LevelToolEdit,
            this.LevelToolComment});
            this.Edit2ToolStrip.Location = new System.Drawing.Point(0, 60);
            this.Edit2ToolStrip.Name = "Edit2ToolStrip";
            this.Edit2ToolStrip.Size = new System.Drawing.Size(147, 25);
            this.Edit2ToolStrip.TabIndex = 4;
            this.Edit2ToolStrip.Text = "toolStrip1";
            // 
            // LevelToolNewLevel
            // 
            this.LevelToolNewLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNewLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_add;
            this.LevelToolNewLevel.Name = "LevelToolNewLevel";
            this.LevelToolNewLevel.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNewLevel.Text = "Create new level";
            this.LevelToolNewLevel.Click += new System.EventHandler(this.EditCreateLevel_Click);
            // 
            // LevelToolDelete
            // 
            this.LevelToolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolDelete.Image = global::ExpertSokoban.Properties.Resources.lvl_del;
            this.LevelToolDelete.Name = "LevelToolDelete";
            this.LevelToolDelete.Size = new System.Drawing.Size(23, 22);
            this.LevelToolDelete.Text = "Delete selected item";
            this.LevelToolDelete.ToolTipText = "Delete selected level";
            this.LevelToolDelete.Click += new System.EventHandler(this.EditDelete_Click);
            // 
            // LevelToolEdit
            // 
            this.LevelToolEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolEdit.Image = global::ExpertSokoban.Properties.Resources.lvl_edit;
            this.LevelToolEdit.Name = "LevelToolEdit";
            this.LevelToolEdit.Size = new System.Drawing.Size(23, 22);
            this.LevelToolEdit.Text = "Edit level";
            this.LevelToolEdit.Click += new System.EventHandler(this.EditEdit_Click);
            // 
            // LevelToolComment
            // 
            this.LevelToolComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolComment.Image = global::ExpertSokoban.Properties.Resources.comment;
            this.LevelToolComment.Name = "LevelToolComment";
            this.LevelToolComment.Size = new System.Drawing.Size(23, 22);
            this.LevelToolComment.Text = "Add a comment";
            this.LevelToolComment.Click += new System.EventHandler(this.EditAddComment_Click);
            // 
            // Edit1ToolStrip
            // 
            this.Edit1ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Edit1ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelToolNew,
            this.LevelToolOpen,
            this.LevelToolSave,
            this.LevelToolSep1,
            this.LevelToolCut,
            this.LevelToolCopy,
            this.LevelToolPaste});
            this.Edit1ToolStrip.Location = new System.Drawing.Point(0, 35);
            this.Edit1ToolStrip.Name = "Edit1ToolStrip";
            this.Edit1ToolStrip.Size = new System.Drawing.Size(147, 25);
            this.Edit1ToolStrip.TabIndex = 0;
            this.Edit1ToolStrip.Text = "Levels toolbar";
            // 
            // LevelToolNew
            // 
            this.LevelToolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNew.Image = global::ExpertSokoban.Properties.Resources.new_;
            this.LevelToolNew.Name = "LevelToolNew";
            this.LevelToolNew.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNew.Text = "New level file";
            this.LevelToolNew.Click += new System.EventHandler(this.LevelNew_Click);
            // 
            // LevelToolOpen
            // 
            this.LevelToolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolOpen.Image = global::ExpertSokoban.Properties.Resources.open;
            this.LevelToolOpen.Name = "LevelToolOpen";
            this.LevelToolOpen.Size = new System.Drawing.Size(23, 22);
            this.LevelToolOpen.Text = "Open level file";
            this.LevelToolOpen.Click += new System.EventHandler(this.LevelOpen_Click);
            // 
            // LevelToolSave
            // 
            this.LevelToolSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolSave.Image = global::ExpertSokoban.Properties.Resources.save;
            this.LevelToolSave.Name = "LevelToolSave";
            this.LevelToolSave.Size = new System.Drawing.Size(23, 22);
            this.LevelToolSave.Text = "Save level file";
            this.LevelToolSave.Click += new System.EventHandler(this.LevelSave_Click);
            // 
            // LevelToolSep1
            // 
            this.LevelToolSep1.Name = "LevelToolSep1";
            this.LevelToolSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // LevelToolCut
            // 
            this.LevelToolCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolCut.Image = global::ExpertSokoban.Properties.Resources.cut;
            this.LevelToolCut.Name = "LevelToolCut";
            this.LevelToolCut.Size = new System.Drawing.Size(23, 22);
            this.LevelToolCut.Text = "Cut";
            this.LevelToolCut.Click += new System.EventHandler(this.EditCut_Click);
            // 
            // LevelToolCopy
            // 
            this.LevelToolCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolCopy.Image = global::ExpertSokoban.Properties.Resources.copy;
            this.LevelToolCopy.Name = "LevelToolCopy";
            this.LevelToolCopy.Size = new System.Drawing.Size(23, 22);
            this.LevelToolCopy.Text = "Copy";
            this.LevelToolCopy.Click += new System.EventHandler(this.EditCopy_Click);
            // 
            // LevelToolPaste
            // 
            this.LevelToolPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolPaste.Image = global::ExpertSokoban.Properties.Resources.paste;
            this.LevelToolPaste.Name = "LevelToolPaste";
            this.LevelToolPaste.Size = new System.Drawing.Size(23, 22);
            this.LevelToolPaste.Text = "Paste";
            this.LevelToolPaste.Click += new System.EventHandler(this.EditPaste_Click);
            // 
            // PlayToolStrip
            // 
            this.PlayToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.PlayToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelToolOpen2,
            this.toolStripSeparator1,
            this.LevelToolPrev,
            this.LevelToolNext,
            this.toolStripSeparator2,
            this.LevelToolPrevUnsolved,
            this.LevelToolNextUnsolved});
            this.PlayToolStrip.Location = new System.Drawing.Point(0, 10);
            this.PlayToolStrip.Name = "PlayToolStrip";
            this.PlayToolStrip.Size = new System.Drawing.Size(147, 25);
            this.PlayToolStrip.TabIndex = 6;
            this.PlayToolStrip.Text = "Levels toolbar";
            // 
            // LevelToolOpen2
            // 
            this.LevelToolOpen2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolOpen2.Image = global::ExpertSokoban.Properties.Resources.open;
            this.LevelToolOpen2.Name = "LevelToolOpen2";
            this.LevelToolOpen2.Size = new System.Drawing.Size(23, 22);
            this.LevelToolOpen2.Text = "Open level file";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // LevelToolPrev
            // 
            this.LevelToolPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolPrev.Image = global::ExpertSokoban.Properties.Resources.lvl_prev;
            this.LevelToolPrev.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LevelToolPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolPrev.Name = "LevelToolPrev";
            this.LevelToolPrev.Size = new System.Drawing.Size(23, 22);
            this.LevelToolPrev.Text = "Next level";
            this.LevelToolPrev.ToolTipText = "Previous level";
            this.LevelToolPrev.Click += new System.EventHandler(this.LevelPrevious_Click);
            // 
            // LevelToolNext
            // 
            this.LevelToolNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNext.Image = global::ExpertSokoban.Properties.Resources.lvl_next;
            this.LevelToolNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LevelToolNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolNext.Name = "LevelToolNext";
            this.LevelToolNext.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNext.Text = "Next level";
            this.LevelToolNext.Click += new System.EventHandler(this.LevelNext_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // LevelToolPrevUnsolved
            // 
            this.LevelToolPrevUnsolved.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolPrevUnsolved.Image = global::ExpertSokoban.Properties.Resources.lvl_prev_unsolved;
            this.LevelToolPrevUnsolved.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LevelToolPrevUnsolved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolPrevUnsolved.Name = "LevelToolPrevUnsolved";
            this.LevelToolPrevUnsolved.Size = new System.Drawing.Size(23, 22);
            this.LevelToolPrevUnsolved.Text = "Next level";
            this.LevelToolPrevUnsolved.ToolTipText = "Previous unsolved level";
            this.LevelToolPrevUnsolved.Click += new System.EventHandler(this.LevelPreviousUnsolved_Click);
            // 
            // LevelToolNextUnsolved
            // 
            this.LevelToolNextUnsolved.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LevelToolNextUnsolved.Image = global::ExpertSokoban.Properties.Resources.lvl_next_unsolved;
            this.LevelToolNextUnsolved.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LevelToolNextUnsolved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LevelToolNextUnsolved.Name = "LevelToolNextUnsolved";
            this.LevelToolNextUnsolved.Size = new System.Drawing.Size(23, 22);
            this.LevelToolNextUnsolved.Text = "Next unsolved level";
            this.LevelToolNextUnsolved.Click += new System.EventHandler(this.LevelNextUnsolved_Click);
            // 
            // LevelListClosePanel
            // 
            this.LevelListClosePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LevelListClosePanel.Location = new System.Drawing.Point(0, 0);
            this.LevelListClosePanel.Name = "LevelListClosePanel";
            this.LevelListClosePanel.Size = new System.Drawing.Size(147, 10);
            this.LevelListClosePanel.TabIndex = 5;
            this.LevelListClosePanel.CloseClicked += new System.EventHandler(this.OptionsLevelList_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelMenu,
            this.EditMenu,
            this.OptionsMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(485, 24);
            this.MainMenu.TabIndex = 8;
            this.MainMenu.Text = "menuStrip1";
            // 
            // LevelMenu
            // 
            this.LevelMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LevelNew,
            this.LevelOpen,
            this.LevelSave,
            this.LevelSaveAs,
            this.LevelSep1,
            this.LevelUndo,
            this.LevelRedo,
            this.LevelRetry,
            this.LevelHighscores,
            this.LevelSep2,
            this.LevelPrevious,
            this.LevelNext,
            this.LevelPreviousUnsolved,
            this.LevelNextUnsolved,
            this.LevelSep3,
            this.LevelChangePlayer,
            this.LevelSep4,
            this.LevelExit,
            this.LevelUnusedHotkeys,
            this.UnusedCTRLShortcuts});
            this.LevelMenu.Name = "LevelMenu";
            this.LevelMenu.Size = new System.Drawing.Size(49, 20);
            this.LevelMenu.Text = "&Level";
            // 
            // LevelNew
            // 
            this.LevelNew.Image = global::ExpertSokoban.Properties.Resources.new_;
            this.LevelNew.Name = "LevelNew";
            this.LevelNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.LevelNew.Size = new System.Drawing.Size(265, 22);
            this.LevelNew.Text = "&New level file";
            this.LevelNew.Click += new System.EventHandler(this.LevelNew_Click);
            // 
            // LevelOpen
            // 
            this.LevelOpen.Image = global::ExpertSokoban.Properties.Resources.open;
            this.LevelOpen.Name = "LevelOpen";
            this.LevelOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.LevelOpen.Size = new System.Drawing.Size(265, 22);
            this.LevelOpen.Text = "&Open level file...";
            this.LevelOpen.Click += new System.EventHandler(this.LevelOpen_Click);
            // 
            // LevelSave
            // 
            this.LevelSave.Image = global::ExpertSokoban.Properties.Resources.save;
            this.LevelSave.Name = "LevelSave";
            this.LevelSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.LevelSave.Size = new System.Drawing.Size(265, 22);
            this.LevelSave.Text = "&Save level file";
            this.LevelSave.Click += new System.EventHandler(this.LevelSave_Click);
            // 
            // LevelSaveAs
            // 
            this.LevelSaveAs.Name = "LevelSaveAs";
            this.LevelSaveAs.Size = new System.Drawing.Size(265, 22);
            this.LevelSaveAs.Text = "Save level file &as...";
            this.LevelSaveAs.Click += new System.EventHandler(this.LevelSaveAs_Click);
            // 
            // LevelSep1
            // 
            this.LevelSep1.Name = "LevelSep1";
            this.LevelSep1.Size = new System.Drawing.Size(262, 6);
            // 
            // LevelUndo
            // 
            this.LevelUndo.Image = global::ExpertSokoban.Properties.Resources.undo;
            this.LevelUndo.Name = "LevelUndo";
            this.LevelUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.LevelUndo.Size = new System.Drawing.Size(265, 22);
            this.LevelUndo.Text = "&Undo move";
            this.LevelUndo.Click += new System.EventHandler(this.LevelUndo_Click);
            // 
            // LevelRedo
            // 
            this.LevelRedo.Image = global::ExpertSokoban.Properties.Resources.redo;
            this.LevelRedo.Name = "LevelRedo";
            this.LevelRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.LevelRedo.Size = new System.Drawing.Size(265, 22);
            this.LevelRedo.Text = "Redo &move";
            this.LevelRedo.Click += new System.EventHandler(this.LevelRedo_Click);
            // 
            // LevelRetry
            // 
            this.LevelRetry.Image = global::ExpertSokoban.Properties.Resources.restart;
            this.LevelRetry.Name = "LevelRetry";
            this.LevelRetry.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.LevelRetry.Size = new System.Drawing.Size(265, 22);
            this.LevelRetry.Text = "&Retry level";
            this.LevelRetry.Click += new System.EventHandler(this.LevelRetry_Click);
            // 
            // LevelHighscores
            // 
            this.LevelHighscores.Name = "LevelHighscores";
            this.LevelHighscores.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.LevelHighscores.Size = new System.Drawing.Size(265, 22);
            this.LevelHighscores.Text = "Show &highscores";
            this.LevelHighscores.Click += new System.EventHandler(this.LevelHighscores_Click);
            // 
            // LevelSep2
            // 
            this.LevelSep2.Name = "LevelSep2";
            this.LevelSep2.Size = new System.Drawing.Size(262, 6);
            // 
            // LevelPrevious
            // 
            this.LevelPrevious.Image = global::ExpertSokoban.Properties.Resources.lvl_prev;
            this.LevelPrevious.Name = "LevelPrevious";
            this.LevelPrevious.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.LevelPrevious.Size = new System.Drawing.Size(265, 22);
            this.LevelPrevious.Text = "&Previous level";
            this.LevelPrevious.Click += new System.EventHandler(this.LevelPrevious_Click);
            // 
            // LevelNext
            // 
            this.LevelNext.Image = global::ExpertSokoban.Properties.Resources.lvl_next;
            this.LevelNext.Name = "LevelNext";
            this.LevelNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.LevelNext.Size = new System.Drawing.Size(265, 22);
            this.LevelNext.Text = "N&ext level";
            this.LevelNext.Click += new System.EventHandler(this.LevelNext_Click);
            // 
            // LevelPreviousUnsolved
            // 
            this.LevelPreviousUnsolved.Image = global::ExpertSokoban.Properties.Resources.lvl_prev_unsolved;
            this.LevelPreviousUnsolved.Name = "LevelPreviousUnsolved";
            this.LevelPreviousUnsolved.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.LevelPreviousUnsolved.Size = new System.Drawing.Size(265, 22);
            this.LevelPreviousUnsolved.Text = "Pre&vious unsolved level";
            this.LevelPreviousUnsolved.Click += new System.EventHandler(this.LevelPreviousUnsolved_Click);
            // 
            // LevelNextUnsolved
            // 
            this.LevelNextUnsolved.Image = global::ExpertSokoban.Properties.Resources.lvl_next_unsolved;
            this.LevelNextUnsolved.Name = "LevelNextUnsolved";
            this.LevelNextUnsolved.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.LevelNextUnsolved.Size = new System.Drawing.Size(265, 22);
            this.LevelNextUnsolved.Text = "Next unsolve&d level";
            this.LevelNextUnsolved.Click += new System.EventHandler(this.LevelNextUnsolved_Click);
            // 
            // LevelSep3
            // 
            this.LevelSep3.Name = "LevelSep3";
            this.LevelSep3.Size = new System.Drawing.Size(262, 6);
            // 
            // LevelChangePlayer
            // 
            this.LevelChangePlayer.Name = "LevelChangePlayer";
            this.LevelChangePlayer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.LevelChangePlayer.Size = new System.Drawing.Size(265, 22);
            this.LevelChangePlayer.Text = "&Change player name...";
            this.LevelChangePlayer.Click += new System.EventHandler(this.LevelChangePlayer_Click);
            // 
            // LevelSep4
            // 
            this.LevelSep4.Name = "LevelSep4";
            this.LevelSep4.Size = new System.Drawing.Size(262, 6);
            // 
            // LevelExit
            // 
            this.LevelExit.Name = "LevelExit";
            this.LevelExit.Size = new System.Drawing.Size(265, 22);
            this.LevelExit.Text = "E&xit";
            this.LevelExit.Click += new System.EventHandler(this.LevelExit_Click);
            // 
            // LevelUnusedHotkeys
            // 
            this.LevelUnusedHotkeys.Enabled = false;
            this.LevelUnusedHotkeys.Name = "LevelUnusedHotkeys";
            this.LevelUnusedHotkeys.Size = new System.Drawing.Size(265, 22);
            this.LevelUnusedHotkeys.Text = "Unused hotkeys: bfgijklqtwyz";
            this.LevelUnusedHotkeys.Visible = false;
            // 
            // UnusedCTRLShortcuts
            // 
            this.UnusedCTRLShortcuts.Enabled = false;
            this.UnusedCTRLShortcuts.Name = "UnusedCTRLShortcuts";
            this.UnusedCTRLShortcuts.Size = new System.Drawing.Size(265, 22);
            this.UnusedCTRLShortcuts.Text = "Unused CTRL shortcuts: agiq";
            this.UnusedCTRLShortcuts.Visible = false;
            // 
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditCreateLevel,
            this.EditDelete,
            this.EditEdit,
            this.EditAddComment,
            this.EditSep1,
            this.EditCut,
            this.EditCopy,
            this.EditPaste,
            this.EditSep2,
            this.EditFinish,
            this.EditCancel,
            this.EditSep3,
            this.EditWall,
            this.EditPiece,
            this.EditTarget,
            this.EditSokoban,
            this.EditUnusedHotkeys});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(41, 20);
            this.EditMenu.Text = "&Edit";
            // 
            // EditCreateLevel
            // 
            this.EditCreateLevel.Image = global::ExpertSokoban.Properties.Resources.lvl_add;
            this.EditCreateLevel.Name = "EditCreateLevel";
            this.EditCreateLevel.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.EditCreateLevel.Size = new System.Drawing.Size(264, 22);
            this.EditCreateLevel.Text = "Create &new level";
            this.EditCreateLevel.Click += new System.EventHandler(this.EditCreateLevel_Click);
            // 
            // EditDelete
            // 
            this.EditDelete.Image = global::ExpertSokoban.Properties.Resources.lvl_del;
            this.EditDelete.Name = "EditDelete";
            this.EditDelete.ShortcutKeyDisplayString = "";
            this.EditDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.EditDelete.Size = new System.Drawing.Size(264, 22);
            this.EditDelete.Text = "&Delete level";
            this.EditDelete.Click += new System.EventHandler(this.EditDelete_Click);
            // 
            // EditEdit
            // 
            this.EditEdit.Image = global::ExpertSokoban.Properties.Resources.lvl_edit;
            this.EditEdit.Name = "EditEdit";
            this.EditEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.EditEdit.Size = new System.Drawing.Size(264, 22);
            this.EditEdit.Text = "&Edit level";
            this.EditEdit.Click += new System.EventHandler(this.EditEdit_Click);
            // 
            // EditAddComment
            // 
            this.EditAddComment.Image = global::ExpertSokoban.Properties.Resources.comment;
            this.EditAddComment.Name = "EditAddComment";
            this.EditAddComment.ShortcutKeyDisplayString = "";
            this.EditAddComment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.EditAddComment.Size = new System.Drawing.Size(264, 22);
            this.EditAddComment.Text = "Add a co&mment...";
            this.EditAddComment.Click += new System.EventHandler(this.EditAddComment_Click);
            // 
            // EditSep1
            // 
            this.EditSep1.Name = "EditSep1";
            this.EditSep1.Size = new System.Drawing.Size(261, 6);
            // 
            // EditCut
            // 
            this.EditCut.Image = global::ExpertSokoban.Properties.Resources.cut;
            this.EditCut.Name = "EditCut";
            this.EditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.EditCut.Size = new System.Drawing.Size(264, 22);
            this.EditCut.Text = "C&ut";
            this.EditCut.Click += new System.EventHandler(this.EditCut_Click);
            // 
            // EditCopy
            // 
            this.EditCopy.Image = global::ExpertSokoban.Properties.Resources.copy;
            this.EditCopy.Name = "EditCopy";
            this.EditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.EditCopy.Size = new System.Drawing.Size(264, 22);
            this.EditCopy.Text = "&Copy";
            this.EditCopy.Click += new System.EventHandler(this.EditCopy_Click);
            // 
            // EditPaste
            // 
            this.EditPaste.Image = global::ExpertSokoban.Properties.Resources.paste;
            this.EditPaste.Name = "EditPaste";
            this.EditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.EditPaste.Size = new System.Drawing.Size(264, 22);
            this.EditPaste.Text = "&Paste";
            this.EditPaste.Click += new System.EventHandler(this.EditPaste_Click);
            // 
            // EditSep2
            // 
            this.EditSep2.Name = "EditSep2";
            this.EditSep2.Size = new System.Drawing.Size(261, 6);
            // 
            // EditFinish
            // 
            this.EditFinish.Image = global::ExpertSokoban.Properties.Resources.ok;
            this.EditFinish.Name = "EditFinish";
            this.EditFinish.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)));
            this.EditFinish.Size = new System.Drawing.Size(264, 22);
            this.EditFinish.Text = "&Finish editing";
            this.EditFinish.Click += new System.EventHandler(this.EditFinish_Click);
            // 
            // EditCancel
            // 
            this.EditCancel.Image = global::ExpertSokoban.Properties.Resources.cancel;
            this.EditCancel.Name = "EditCancel";
            this.EditCancel.ShortcutKeyDisplayString = "";
            this.EditCancel.Size = new System.Drawing.Size(264, 22);
            this.EditCancel.Text = "C&ancel editing";
            this.EditCancel.Click += new System.EventHandler(this.EditCancel_Click);
            // 
            // EditSep3
            // 
            this.EditSep3.Name = "EditSep3";
            this.EditSep3.Size = new System.Drawing.Size(261, 6);
            // 
            // EditWall
            // 
            this.EditWall.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolBrick;
            this.EditWall.Name = "EditWall";
            this.EditWall.ParentGroup = this.EditToolOptions;
            this.EditWall.ShortcutKeyDisplayString = "";
            this.EditWall.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.EditWall.Size = new System.Drawing.Size(264, 22);
            this.EditWall.Text = "&Wall tool";
            this.EditWall.Value = ExpertSokoban.MainAreaTool.Wall;
            // 
            // EditToolOptions
            // 
            this.EditToolOptions.ValueChanged += new System.EventHandler(this.EditToolOptions_ValueChanged);
            // 
            // EditPiece
            // 
            this.EditPiece.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolPiece;
            this.EditPiece.Name = "EditPiece";
            this.EditPiece.ParentGroup = this.EditToolOptions;
            this.EditPiece.ShortcutKeyDisplayString = "";
            this.EditPiece.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.EditPiece.Size = new System.Drawing.Size(264, 22);
            this.EditPiece.Text = "P&iece tool";
            this.EditPiece.Value = ExpertSokoban.MainAreaTool.Piece;
            // 
            // EditTarget
            // 
            this.EditTarget.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolTarget;
            this.EditTarget.Name = "EditTarget";
            this.EditTarget.ParentGroup = this.EditToolOptions;
            this.EditTarget.ShortcutKeyDisplayString = "";
            this.EditTarget.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.EditTarget.Size = new System.Drawing.Size(264, 22);
            this.EditTarget.Text = "&Target tool";
            this.EditTarget.Value = ExpertSokoban.MainAreaTool.Target;
            // 
            // EditSokoban
            // 
            this.EditSokoban.Image = global::ExpertSokoban.Properties.Resources.Skin_ToolSokoban;
            this.EditSokoban.Name = "EditSokoban";
            this.EditSokoban.ParentGroup = this.EditToolOptions;
            this.EditSokoban.ShortcutKeyDisplayString = "";
            this.EditSokoban.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.EditSokoban.Size = new System.Drawing.Size(264, 22);
            this.EditSokoban.Text = "&Sokoban tool";
            this.EditSokoban.Value = ExpertSokoban.MainAreaTool.Sokoban;
            // 
            // EditUnusedHotkeys
            // 
            this.EditUnusedHotkeys.Enabled = false;
            this.EditUnusedHotkeys.Name = "EditUnusedHotkeys";
            this.EditUnusedHotkeys.Size = new System.Drawing.Size(264, 22);
            this.EditUnusedHotkeys.Text = "Unused hotkeys: bghjkloqrvxyz";
            this.EditUnusedHotkeys.Visible = false;
            // 
            // OptionsMenu
            // 
            this.OptionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsLevelList,
            this.OptionsPlayToolStrip,
            this.OptionsEditToolStrip,
            this.OptionsEditLevelToolStrip,
            this.OptionsStatusBar,
            this.OptionsSep1,
            this.OptionsMoveNo,
            this.OptionsMoveLine,
            this.OptionsMoveDots,
            this.OptionsMoveArrows,
            this.OptionsSep2,
            this.OptionsPushNo,
            this.OptionsPushLine,
            this.OptionsPushDots,
            this.OptionsPushArrows,
            this.OptionsSep3,
            this.OptionsEndPos,
            this.OptionsAreaSokoban,
            this.OptionsAreaPiece,
            this.OptionsSep4,
            this.OptionsSound,
            this.OptionsUnusedHotkeys});
            this.OptionsMenu.Name = "OptionsMenu";
            this.OptionsMenu.Size = new System.Drawing.Size(63, 20);
            this.OptionsMenu.Text = "&Options";
            // 
            // OptionsLevelList
            // 
            this.OptionsLevelList.Name = "OptionsLevelList";
            this.OptionsLevelList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.OptionsLevelList.Size = new System.Drawing.Size(329, 22);
            this.OptionsLevelList.Text = "Display &level list";
            this.OptionsLevelList.Click += new System.EventHandler(this.OptionsLevelList_Click);
            // 
            // OptionsPlayToolStrip
            // 
            this.OptionsPlayToolStrip.Name = "OptionsPlayToolStrip";
            this.OptionsPlayToolStrip.Size = new System.Drawing.Size(329, 22);
            this.OptionsPlayToolStrip.Text = "Display pla&ying toolbar";
            this.OptionsPlayToolStrip.Click += new System.EventHandler(this.OptionsPlayToolStrip_Click);
            // 
            // OptionsEditToolStrip
            // 
            this.OptionsEditToolStrip.Name = "OptionsEditToolStrip";
            this.OptionsEditToolStrip.Size = new System.Drawing.Size(329, 22);
            this.OptionsEditToolStrip.Text = "Display &editing toolbars (level pack)";
            this.OptionsEditToolStrip.Click += new System.EventHandler(this.OptionsEditToolStrip_Click);
            // 
            // OptionsEditLevelToolStrip
            // 
            this.OptionsEditLevelToolStrip.Name = "OptionsEditLevelToolStrip";
            this.OptionsEditLevelToolStrip.Size = new System.Drawing.Size(329, 22);
            this.OptionsEditLevelToolStrip.Text = "Display editin&g toolbar (level)";
            this.OptionsEditLevelToolStrip.Click += new System.EventHandler(this.OptionsEditLevelToolStrip_Click);
            // 
            // OptionsStatusBar
            // 
            this.OptionsStatusBar.Name = "OptionsStatusBar";
            this.OptionsStatusBar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.OptionsStatusBar.Size = new System.Drawing.Size(329, 22);
            this.OptionsStatusBar.Text = "Display stat&us bar";
            this.OptionsStatusBar.Click += new System.EventHandler(this.OptionsStatusBar_Click);
            // 
            // OptionsSep1
            // 
            this.OptionsSep1.Name = "OptionsSep1";
            this.OptionsSep1.Size = new System.Drawing.Size(326, 6);
            // 
            // OptionsMoveNo
            // 
            this.OptionsMoveNo.Name = "OptionsMoveNo";
            this.OptionsMoveNo.ParentGroup = this.MovePathOptions;
            this.OptionsMoveNo.Size = new System.Drawing.Size(329, 22);
            this.OptionsMoveNo.Text = "Don\'t display &move path";
            this.OptionsMoveNo.Value = ExpertSokoban.PathDrawMode.None;
            // 
            // MovePathOptions
            // 
            this.MovePathOptions.ValueChanged += new System.EventHandler(this.MovePathOptions_ValueChanged);
            // 
            // OptionsMoveLine
            // 
            this.OptionsMoveLine.Name = "OptionsMoveLine";
            this.OptionsMoveLine.ParentGroup = this.MovePathOptions;
            this.OptionsMoveLine.Size = new System.Drawing.Size(329, 22);
            this.OptionsMoveLine.Text = "Display move path as li&ne";
            this.OptionsMoveLine.Value = ExpertSokoban.PathDrawMode.Line;
            // 
            // OptionsMoveDots
            // 
            this.OptionsMoveDots.Name = "OptionsMoveDots";
            this.OptionsMoveDots.ParentGroup = this.MovePathOptions;
            this.OptionsMoveDots.Size = new System.Drawing.Size(329, 22);
            this.OptionsMoveDots.Text = "Display move path as &dots";
            this.OptionsMoveDots.Value = ExpertSokoban.PathDrawMode.Dots;
            // 
            // OptionsMoveArrows
            // 
            this.OptionsMoveArrows.Name = "OptionsMoveArrows";
            this.OptionsMoveArrows.ParentGroup = this.MovePathOptions;
            this.OptionsMoveArrows.Size = new System.Drawing.Size(329, 22);
            this.OptionsMoveArrows.Text = "Display move path as &arrows";
            this.OptionsMoveArrows.Value = ExpertSokoban.PathDrawMode.Arrows;
            // 
            // OptionsSep2
            // 
            this.OptionsSep2.Name = "OptionsSep2";
            this.OptionsSep2.Size = new System.Drawing.Size(326, 6);
            // 
            // OptionsPushNo
            // 
            this.OptionsPushNo.Name = "OptionsPushNo";
            this.OptionsPushNo.ParentGroup = this.PushPathOptions;
            this.OptionsPushNo.Size = new System.Drawing.Size(329, 22);
            this.OptionsPushNo.Text = "Don\'t display &push path";
            this.OptionsPushNo.Value = ExpertSokoban.PathDrawMode.None;
            // 
            // PushPathOptions
            // 
            this.PushPathOptions.ValueChanged += new System.EventHandler(this.PushPathOptions_ValueChanged);
            // 
            // OptionsPushLine
            // 
            this.OptionsPushLine.Name = "OptionsPushLine";
            this.OptionsPushLine.ParentGroup = this.PushPathOptions;
            this.OptionsPushLine.Size = new System.Drawing.Size(329, 22);
            this.OptionsPushLine.Text = "Display push path as l&ine";
            this.OptionsPushLine.Value = ExpertSokoban.PathDrawMode.Line;
            // 
            // OptionsPushDots
            // 
            this.OptionsPushDots.Name = "OptionsPushDots";
            this.OptionsPushDots.ParentGroup = this.PushPathOptions;
            this.OptionsPushDots.Size = new System.Drawing.Size(329, 22);
            this.OptionsPushDots.Text = "Display push path as do&ts";
            this.OptionsPushDots.Value = ExpertSokoban.PathDrawMode.Dots;
            // 
            // OptionsPushArrows
            // 
            this.OptionsPushArrows.Name = "OptionsPushArrows";
            this.OptionsPushArrows.ParentGroup = this.PushPathOptions;
            this.OptionsPushArrows.Size = new System.Drawing.Size(329, 22);
            this.OptionsPushArrows.Text = "Display push path as a&rrows";
            this.OptionsPushArrows.Value = ExpertSokoban.PathDrawMode.Arrows;
            // 
            // OptionsSep3
            // 
            this.OptionsSep3.Name = "OptionsSep3";
            this.OptionsSep3.Size = new System.Drawing.Size(326, 6);
            // 
            // OptionsEndPos
            // 
            this.OptionsEndPos.Name = "OptionsEndPos";
            this.OptionsEndPos.Size = new System.Drawing.Size(329, 22);
            this.OptionsEndPos.Text = "Display end p&osition of Sokoban and piece";
            this.OptionsEndPos.Click += new System.EventHandler(this.OptionsEndPos_Click);
            // 
            // OptionsAreaSokoban
            // 
            this.OptionsAreaSokoban.Name = "OptionsAreaSokoban";
            this.OptionsAreaSokoban.Size = new System.Drawing.Size(329, 22);
            this.OptionsAreaSokoban.Text = "Display rea&chable area for Sokoban";
            this.OptionsAreaSokoban.Click += new System.EventHandler(this.OptionsAreaSokoban_Click);
            // 
            // OptionsAreaPiece
            // 
            this.OptionsAreaPiece.Name = "OptionsAreaPiece";
            this.OptionsAreaPiece.Size = new System.Drawing.Size(329, 22);
            this.OptionsAreaPiece.Text = "Display reac&hable area for piece";
            this.OptionsAreaPiece.Click += new System.EventHandler(this.OptionsAreaPiece_Click);
            // 
            // OptionsSep4
            // 
            this.OptionsSep4.Name = "OptionsSep4";
            this.OptionsSep4.Size = new System.Drawing.Size(326, 6);
            // 
            // OptionsSound
            // 
            this.OptionsSound.Name = "OptionsSound";
            this.OptionsSound.Size = new System.Drawing.Size(329, 22);
            this.OptionsSound.Text = "Enable &sound";
            this.OptionsSound.Click += new System.EventHandler(this.OptionsSound_Click);
            // 
            // OptionsUnusedHotkeys
            // 
            this.OptionsUnusedHotkeys.Enabled = false;
            this.OptionsUnusedHotkeys.Name = "OptionsUnusedHotkeys";
            this.OptionsUnusedHotkeys.Size = new System.Drawing.Size(329, 22);
            this.OptionsUnusedHotkeys.Text = "Unused hotkeys: bfjkqvwxz";
            this.OptionsUnusedHotkeys.Visible = false;
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpHelp,
            this.HelpKeyboard,
            this.HelpAbout});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(45, 20);
            this.HelpMenu.Text = "&Help";
            // 
            // HelpHelp
            // 
            this.HelpHelp.Image = global::ExpertSokoban.Properties.Resources.help;
            this.HelpHelp.Name = "HelpHelp";
            this.HelpHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.HelpHelp.Size = new System.Drawing.Size(210, 22);
            this.HelpHelp.Text = "&Help";
            this.HelpHelp.Click += new System.EventHandler(this.HelpHelp_Click);
            // 
            // HelpKeyboard
            // 
            this.HelpKeyboard.Name = "HelpKeyboard";
            this.HelpKeyboard.Size = new System.Drawing.Size(210, 22);
            this.HelpKeyboard.Text = "&Keyboard shortcuts...";
            this.HelpKeyboard.Click += new System.EventHandler(this.HelpKeyboard_Click);
            // 
            // HelpAbout
            // 
            this.HelpAbout.Name = "HelpAbout";
            this.HelpAbout.Size = new System.Drawing.Size(210, 22);
            this.HelpAbout.Text = "&About";
            this.HelpAbout.Click += new System.EventHandler(this.HelpAbout_Click);
            // 
            // MainToolStripContainer
            // 
            // 
            // MainToolStripContainer.BottomToolStripPanel
            // 
            this.MainToolStripContainer.BottomToolStripPanel.Controls.Add(this.StatusBar);
            // 
            // MainToolStripContainer.ContentPanel
            // 
            this.MainToolStripContainer.ContentPanel.AutoScroll = true;
            this.MainToolStripContainer.ContentPanel.Controls.Add(this.MainArea);
            this.MainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(485, 320);
            this.MainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.MainToolStripContainer.Name = "MainToolStripContainer";
            this.MainToolStripContainer.Size = new System.Drawing.Size(485, 366);
            this.MainToolStripContainer.TabIndex = 9;
            this.MainToolStripContainer.Text = "toolStripContainer1";
            // 
            // MainToolStripContainer.TopToolStripPanel
            // 
            this.MainToolStripContainer.TopToolStripPanel.Controls.Add(this.MainMenu);
            // 
            // StatusBar
            // 
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusMoves,
            this.StatusPushes,
            this.StatusPieces,
            this.StatusEdit,
            this.StatusSolved,
            this.StatusNull});
            this.StatusBar.Location = new System.Drawing.Point(0, 0);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(485, 22);
            this.StatusBar.SizingGrip = false;
            this.StatusBar.TabIndex = 0;
            // 
            // StatusMoves
            // 
            this.StatusMoves.Name = "StatusMoves";
            this.StatusMoves.Size = new System.Drawing.Size(60, 17);
            this.StatusMoves.Text = "Moves: 0";
            this.StatusMoves.Visible = false;
            // 
            // StatusPushes
            // 
            this.StatusPushes.Name = "StatusPushes";
            this.StatusPushes.Size = new System.Drawing.Size(64, 17);
            this.StatusPushes.Text = "Pushes: 0";
            this.StatusPushes.Visible = false;
            // 
            // StatusPieces
            // 
            this.StatusPieces.Name = "StatusPieces";
            this.StatusPieces.Size = new System.Drawing.Size(124, 17);
            this.StatusPieces.Text = "Remaining pieces: 0";
            this.StatusPieces.Visible = false;
            // 
            // StatusEdit
            // 
            this.StatusEdit.Name = "StatusEdit";
            this.StatusEdit.Size = new System.Drawing.Size(206, 17);
            this.StatusEdit.Text = "You are currently editing this level.";
            this.StatusEdit.Visible = false;
            // 
            // StatusSolved
            // 
            this.StatusSolved.Name = "StatusSolved";
            this.StatusSolved.Size = new System.Drawing.Size(253, 17);
            this.StatusSolved.Text = "You have solved the level. Congratulations!";
            this.StatusSolved.Visible = false;
            // 
            // StatusNull
            // 
            this.StatusNull.Name = "StatusNull";
            this.StatusNull.Size = new System.Drawing.Size(401, 17);
            this.StatusNull.Text = "No level s currently selected. Select a level from the level list to play.";
            this.StatusNull.Visible = false;
            // 
            // MainArea
            // 
            this.MainArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(206)))));
            this.MainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainArea.Location = new System.Drawing.Point(0, 0);
            this.MainArea.Modified = false;
            this.MainArea.MoveDrawMode = ExpertSokoban.PathDrawMode.Line;
            this.MainArea.Name = "MainArea";
            this.MainArea.PushDrawMode = ExpertSokoban.PathDrawMode.Arrows;
            this.MainArea.ShowAreaPiece = false;
            this.MainArea.ShowAreaSokoban = false;
            this.MainArea.ShowEndPos = false;
            this.MainArea.Size = new System.Drawing.Size(485, 320);
            this.MainArea.SoundEnabled = false;
            this.MainArea.TabIndex = 1;
            this.MainArea.TabStop = true;
            this.MainArea.Tool = ExpertSokoban.MainAreaTool.Wall;
            this.MainArea.Click += new System.EventHandler(this.MainArea_Click);
            this.MainArea.LevelSolved += new System.EventHandler(this.MainArea_LevelSolved);
            this.MainArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainArea_KeyDown);
            // 
            // LevelListSplitter
            // 
            this.LevelListSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.LevelListSplitter.Location = new System.Drawing.Point(482, 0);
            this.LevelListSplitter.MinSize = 50;
            this.LevelListSplitter.Name = "LevelListSplitter";
            this.LevelListSplitter.Size = new System.Drawing.Size(3, 366);
            this.LevelListSplitter.TabIndex = 10;
            this.LevelListSplitter.TabStop = false;
            this.LevelListSplitter.Visible = false;
            // 
            // BugWorkaroundTimer
            // 
            this.BugWorkaroundTimer.Enabled = true;
            this.BugWorkaroundTimer.Tick += new System.EventHandler(this.BugWorkaroundTimer_Tick);
            // 
            // UpdateControlsTimer
            // 
            this.UpdateControlsTimer.Enabled = true;
            this.UpdateControlsTimer.Interval = 10;
            this.UpdateControlsTimer.Tick += new System.EventHandler(this.UpdateControlsTimer_Tick);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 366);
            this.Controls.Add(this.LevelListSplitter);
            this.Controls.Add(this.MainToolStripContainer);
            this.Controls.Add(this.LevelListPanel);
            this.Icon = global::ExpertSokoban.Properties.Resources.ExpertSokoban;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Expert Sokoban";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mainform_FormClosing);
            this.LevelListPanel.ResumeLayout(false);
            this.LevelListPanel.PerformLayout();
            this.LevelContextMenu.ResumeLayout(false);
            this.EditLevelToolStrip.ResumeLayout(false);
            this.EditLevelToolStrip.PerformLayout();
            this.Edit2ToolStrip.ResumeLayout(false);
            this.Edit2ToolStrip.PerformLayout();
            this.Edit1ToolStrip.ResumeLayout(false);
            this.Edit1ToolStrip.PerformLayout();
            this.PlayToolStrip.ResumeLayout(false);
            this.PlayToolStrip.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.MainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.MainToolStripContainer.ResumeLayout(false);
            this.MainToolStripContainer.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RT.Util.Controls.NiceClosePanel LevelListClosePanel;
        private ExpertSokoban.MainArea MainArea;
        private System.Windows.Forms.Panel LevelListPanel;
        private ExpertSokoban.LevelListBox LevelList;
        private System.Windows.Forms.ToolStrip Edit1ToolStrip;
        private System.Windows.Forms.ToolStripButton LevelToolOpen;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem LevelOpen;
        private System.Windows.Forms.ToolStripMenuItem OptionsLevelList;
        private System.Windows.Forms.ToolStripButton LevelToolNew;
        private System.Windows.Forms.ToolStripSeparator LevelToolSep1;
        private System.Windows.Forms.ToolStripMenuItem LevelNew;
        private System.Windows.Forms.ToolStripMenuItem OptionsMenu;
        private MenuRadioItemPathDrawMode OptionsMoveNo;
        private MenuRadioItemPathDrawMode OptionsMoveDots;
        private MenuRadioItemPathDrawMode OptionsMoveLine;
        private MenuRadioItemPathDrawMode OptionsMoveArrows;
        private System.Windows.Forms.ToolStripSeparator OptionsSep2;
        private MenuRadioItemPathDrawMode OptionsPushNo;
        private MenuRadioItemPathDrawMode OptionsPushDots;
        private MenuRadioItemPathDrawMode OptionsPushLine;
        private MenuRadioItemPathDrawMode OptionsPushArrows;
        private System.Windows.Forms.ToolStripSeparator OptionsSep3;
        private System.Windows.Forms.ToolStripMenuItem OptionsEndPos;
        private System.Windows.Forms.ToolStripMenuItem LevelMenu;
        private System.Windows.Forms.ToolStripSeparator OptionsSep1;
        private System.Windows.Forms.ToolStripMenuItem EditMenu;
        private System.Windows.Forms.ToolStripSeparator LevelSep1;
        private System.Windows.Forms.ToolStripButton LevelToolSave;
        private System.Windows.Forms.ToolStripMenuItem LevelSave;
        private System.Windows.Forms.ToolStripContainer MainToolStripContainer;
        private System.Windows.Forms.Splitter LevelListSplitter;
        private System.Windows.Forms.ToolStrip EditLevelToolStrip;
        private System.Windows.Forms.ToolStripButton EditToolWall;
        private System.Windows.Forms.ToolStripButton EditToolPiece;
        private System.Windows.Forms.ToolStripButton EditToolTarget;
        private System.Windows.Forms.ToolStripButton EditToolSokoban;
        private MenuRadioItemMainAreaTool EditWall;
        private MenuRadioItemMainAreaTool EditPiece;
        private MenuRadioItemMainAreaTool EditTarget;
        private MenuRadioItemMainAreaTool EditSokoban;
        private System.Windows.Forms.ToolStripButton EditToolOK;
        private System.Windows.Forms.ToolStripSeparator EditToolSep;
        private System.Windows.Forms.ToolStripMenuItem EditFinish;
        private System.Windows.Forms.ToolStripMenuItem EditCancel;
        private System.Windows.Forms.ToolStripSeparator EditSep3;
        private System.Windows.Forms.ToolStripButton EditToolCancel;
        private System.Windows.Forms.ToolStrip Edit2ToolStrip;
        private System.Windows.Forms.ToolStripMenuItem OptionsPlayToolStrip;
        private System.Windows.Forms.ToolStripMenuItem OptionsEditLevelToolStrip;
        private System.Windows.Forms.ToolStripMenuItem OptionsEditToolStrip;
        private System.Windows.Forms.ToolStripMenuItem OptionsUnusedHotkeys;
        private System.Windows.Forms.ToolStripButton LevelToolDelete;
        private System.Windows.Forms.ToolStripMenuItem LevelExit;
        private System.Windows.Forms.ToolStripMenuItem LevelUndo;
        private System.Windows.Forms.ToolStripMenuItem LevelRetry;
        private System.Windows.Forms.ToolStripMenuItem EditCreateLevel;
        private System.Windows.Forms.ToolStripMenuItem EditEdit;
        private System.Windows.Forms.ToolStripMenuItem EditAddComment;
        private System.Windows.Forms.ToolStripSeparator EditSep1;
        private System.Windows.Forms.ToolStripMenuItem EditCut;
        private System.Windows.Forms.ToolStripMenuItem EditCopy;
        private System.Windows.Forms.ToolStripMenuItem EditPaste;
        private System.Windows.Forms.ToolStripMenuItem EditDelete;
        private System.Windows.Forms.ToolStripSeparator EditSep2;
        private System.Windows.Forms.ToolStripMenuItem EditUnusedHotkeys;
        private System.Windows.Forms.Timer BugWorkaroundTimer;
        private ExpertSokoban.MenuRadioGroupPathDrawMode MovePathOptions;
        private ExpertSokoban.MenuRadioGroupPathDrawMode PushPathOptions;
        private ExpertSokoban.MenuRadioGroupMainAreaTool EditToolOptions;
        private System.Windows.Forms.ToolStripSeparator LevelSep2;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpAbout;
        private System.Windows.Forms.ToolStripMenuItem HelpHelp;
        private System.Windows.Forms.ToolStripMenuItem LevelNext;
        private System.Windows.Forms.ToolStripMenuItem LevelNextUnsolved;
        private System.Windows.Forms.ToolStripSeparator LevelSep3;
        private System.Windows.Forms.ToolStripMenuItem LevelPrevious;
        private System.Windows.Forms.ToolStripMenuItem LevelPreviousUnsolved;
        private System.Windows.Forms.ToolStripMenuItem UnusedCTRLShortcuts;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusMoves;
        private System.Windows.Forms.ToolStripStatusLabel StatusPushes;
        private System.Windows.Forms.ToolStripStatusLabel StatusPieces;
        private System.Windows.Forms.ToolStripStatusLabel StatusNull;
        private System.Windows.Forms.ToolStripStatusLabel StatusEdit;
        private System.Windows.Forms.ToolStripStatusLabel StatusSolved;
        private System.Windows.Forms.ToolStripMenuItem OptionsStatusBar;
        private System.Windows.Forms.ToolStripMenuItem LevelChangePlayer;
        private System.Windows.Forms.ToolStripSeparator LevelSep4;
        private System.Windows.Forms.ToolStripMenuItem LevelUnusedHotkeys;
        private System.Windows.Forms.ContextMenuStrip LevelContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextPlay;
        private System.Windows.Forms.ToolStripMenuItem ContextEdit;
        private System.Windows.Forms.ToolStripSeparator ContextSep1;
        private System.Windows.Forms.ToolStripMenuItem ContextNewLevel;
        private System.Windows.Forms.ToolStripMenuItem ContextNewComment;
        private System.Windows.Forms.ToolStripSeparator ContextSep2;
        private System.Windows.Forms.ToolStripMenuItem ContextCut;
        private System.Windows.Forms.ToolStripMenuItem ContextCopyItem;
        private System.Windows.Forms.ToolStripMenuItem ContextPaste;
        private System.Windows.Forms.ToolStripMenuItem ContextDelete;
        private System.Windows.Forms.ToolStripSeparator ContextSep3;
        private System.Windows.Forms.ToolStripMenuItem ContextHide;
        private System.Windows.Forms.ToolStripMenuItem LevelRedo;
        private System.Windows.Forms.ToolStripMenuItem HelpKeyboard;
        private System.Windows.Forms.ToolStripMenuItem LevelSaveAs;
        private System.Windows.Forms.Timer UpdateControlsTimer;
        private System.Windows.Forms.ToolStripMenuItem LevelHighscores;
        private System.Windows.Forms.ToolStripMenuItem ContextHighscores;
        private System.Windows.Forms.ToolStripButton LevelToolNewLevel;
        private System.Windows.Forms.ToolStripButton LevelToolEdit;
        private System.Windows.Forms.ToolStripButton LevelToolComment;
        private System.Windows.Forms.ToolStripButton LevelToolCut;
        private System.Windows.Forms.ToolStripButton LevelToolCopy;
        private System.Windows.Forms.ToolStripButton LevelToolPaste;
        private System.Windows.Forms.ToolStrip PlayToolStrip;
        private System.Windows.Forms.ToolStripButton LevelToolOpen2;
        private System.Windows.Forms.ToolStripButton LevelToolPrev;
        private System.Windows.Forms.ToolStripButton LevelToolNext;
        private System.Windows.Forms.ToolStripButton LevelToolPrevUnsolved;
        private System.Windows.Forms.ToolStripButton LevelToolNextUnsolved;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem OptionsAreaSokoban;
        private System.Windows.Forms.ToolStripMenuItem OptionsAreaPiece;
        private System.Windows.Forms.ToolStripMenuItem OptionsSound;
        private System.Windows.Forms.ToolStripSeparator OptionsSep4;
    }
}

