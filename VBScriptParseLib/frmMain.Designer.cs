namespace TARLABS.VBScriptFormatter
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.formatCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rTFToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tEXTToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iMAGEToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rTFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tEXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iMAGEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblConstants = new System.Windows.Forms.Label();
            this.lblVariables = new System.Windows.Forms.Label();
            this.lblClasses = new System.Windows.Forms.Label();
            this.lblBlankLines = new System.Windows.Forms.Label();
            this.lblComments = new System.Windows.Forms.Label();
            this.lblFunctions = new System.Windows.Forms.Label();
            this.chkShowLineNumbers = new System.Windows.Forms.CheckBox();
            this.lnkTARLABS = new System.Windows.Forms.LinkLabel();
            this.btnInstallUFTPlugin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.chkRemoveComments = new System.Windows.Forms.CheckBox();
            this.cmbIndetationCharacter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSourceCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceCode)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1473, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem3,
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem4,
            this.formatCodeToolStripMenuItem,
            this.toolStripMenuItem5});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + A";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + C";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + V";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + A";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(177, 6);
            // 
            // formatCodeToolStripMenuItem
            // 
            this.formatCodeToolStripMenuItem.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.Actions_format_indent_more;
            this.formatCodeToolStripMenuItem.Name = "formatCodeToolStripMenuItem";
            this.formatCodeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.formatCodeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.formatCodeToolStripMenuItem.Text = "Format Code";
            this.formatCodeToolStripMenuItem.Click += new System.EventHandler(this.formatCodeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(177, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asFileToolStripMenuItem,
            this.toClipboardToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // asFileToolStripMenuItem
            // 
            this.asFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hTMLToolStripMenuItem1,
            this.rTFToolStripMenuItem1,
            this.tEXTToolStripMenuItem1,
            this.iMAGEToolStripMenuItem1});
            this.asFileToolStripMenuItem.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.file;
            this.asFileToolStripMenuItem.Name = "asFileToolStripMenuItem";
            this.asFileToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.asFileToolStripMenuItem.Text = "To &File...";
            // 
            // hTMLToolStripMenuItem1
            // 
            this.hTMLToolStripMenuItem1.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.html;
            this.hTMLToolStripMenuItem1.Name = "hTMLToolStripMenuItem1";
            this.hTMLToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.H)));
            this.hTMLToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.hTMLToolStripMenuItem1.Text = "HTML";
            this.hTMLToolStripMenuItem1.Click += new System.EventHandler(this.hTMLToolStripMenuItem1_Click);
            // 
            // rTFToolStripMenuItem1
            // 
            this.rTFToolStripMenuItem1.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.doc_rtf;
            this.rTFToolStripMenuItem1.Name = "rTFToolStripMenuItem1";
            this.rTFToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.rTFToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.rTFToolStripMenuItem1.Text = "RTF";
            this.rTFToolStripMenuItem1.Click += new System.EventHandler(this.rTFToolStripMenuItem1_Click);
            // 
            // tEXTToolStripMenuItem1
            // 
            this.tEXTToolStripMenuItem1.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.Txt;
            this.tEXTToolStripMenuItem1.Name = "tEXTToolStripMenuItem1";
            this.tEXTToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.tEXTToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.tEXTToolStripMenuItem1.Text = "TEXT";
            this.tEXTToolStripMenuItem1.Click += new System.EventHandler(this.tEXTToolStripMenuItem1_Click);
            // 
            // iMAGEToolStripMenuItem1
            // 
            this.iMAGEToolStripMenuItem1.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.image;
            this.iMAGEToolStripMenuItem1.Name = "iMAGEToolStripMenuItem1";
            this.iMAGEToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.iMAGEToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.iMAGEToolStripMenuItem1.Text = "IMAGE";
            this.iMAGEToolStripMenuItem1.Click += new System.EventHandler(this.iMAGEToolStripMenuItem1_Click);
            // 
            // toClipboardToolStripMenuItem
            // 
            this.toClipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hTMLToolStripMenuItem,
            this.rTFToolStripMenuItem,
            this.tEXTToolStripMenuItem,
            this.iMAGEToolStripMenuItem});
            this.toClipboardToolStripMenuItem.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.clipboard;
            this.toClipboardToolStripMenuItem.Name = "toClipboardToolStripMenuItem";
            this.toClipboardToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.toClipboardToolStripMenuItem.Text = "To Clipboard";
            // 
            // hTMLToolStripMenuItem
            // 
            this.hTMLToolStripMenuItem.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.html;
            this.hTMLToolStripMenuItem.Name = "hTMLToolStripMenuItem";
            this.hTMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.H)));
            this.hTMLToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.hTMLToolStripMenuItem.Text = "HTML";
            this.hTMLToolStripMenuItem.Click += new System.EventHandler(this.hTMLToolStripMenuItem_Click);
            // 
            // rTFToolStripMenuItem
            // 
            this.rTFToolStripMenuItem.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.doc_rtf;
            this.rTFToolStripMenuItem.Name = "rTFToolStripMenuItem";
            this.rTFToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.R)));
            this.rTFToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.rTFToolStripMenuItem.Text = "RTF";
            this.rTFToolStripMenuItem.Click += new System.EventHandler(this.rTFToolStripMenuItem_Click);
            // 
            // tEXTToolStripMenuItem
            // 
            this.tEXTToolStripMenuItem.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.Txt;
            this.tEXTToolStripMenuItem.Name = "tEXTToolStripMenuItem";
            this.tEXTToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.R)));
            this.tEXTToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.tEXTToolStripMenuItem.Text = "TEXT";
            this.tEXTToolStripMenuItem.Click += new System.EventHandler(this.tEXTToolStripMenuItem_Click);
            // 
            // iMAGEToolStripMenuItem
            // 
            this.iMAGEToolStripMenuItem.Image = global::TARLABS.VBScriptFormatter.Properties.Resources.image;
            this.iMAGEToolStripMenuItem.Name = "iMAGEToolStripMenuItem";
            this.iMAGEToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.I)));
            this.iMAGEToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.iMAGEToolStripMenuItem.Text = "IMAGE";
            this.iMAGEToolStripMenuItem.Click += new System.EventHandler(this.iMAGEToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visitWebsiteToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // visitWebsiteToolStripMenuItem
            // 
            this.visitWebsiteToolStripMenuItem.Name = "visitWebsiteToolStripMenuItem";
            this.visitWebsiteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.visitWebsiteToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.visitWebsiteToolStripMenuItem.Text = "Visit Website";
            this.visitWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visitWebsiteToolStripMenuItem_Click);
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.chkShowLineNumbers);
            this.groupBox1.Controls.Add(this.lnkTARLABS);
            this.groupBox1.Controls.Add(this.btnInstallUFTPlugin);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.chkRemoveComments);
            this.groupBox1.Controls.Add(this.cmbIndetationCharacter);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(1177, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(281, 618);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblConstants);
            this.groupBox2.Controls.Add(this.lblVariables);
            this.groupBox2.Controls.Add(this.lblClasses);
            this.groupBox2.Controls.Add(this.lblBlankLines);
            this.groupBox2.Controls.Add(this.lblComments);
            this.groupBox2.Controls.Add(this.lblFunctions);
            this.groupBox2.Location = new System.Drawing.Point(20, 359);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(237, 234);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stats";
            // 
            // lblConstants
            // 
            this.lblConstants.AutoSize = true;
            this.lblConstants.Location = new System.Drawing.Point(8, 178);
            this.lblConstants.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConstants.Name = "lblConstants";
            this.lblConstants.Size = new System.Drawing.Size(67, 16);
            this.lblConstants.TabIndex = 29;
            this.lblConstants.Text = "Constants";
            // 
            // lblVariables
            // 
            this.lblVariables.AutoSize = true;
            this.lblVariables.Location = new System.Drawing.Point(8, 146);
            this.lblVariables.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVariables.Name = "lblVariables";
            this.lblVariables.Size = new System.Drawing.Size(66, 16);
            this.lblVariables.TabIndex = 28;
            this.lblVariables.Text = "Variables";
            // 
            // lblClasses
            // 
            this.lblClasses.AutoSize = true;
            this.lblClasses.Location = new System.Drawing.Point(8, 118);
            this.lblClasses.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClasses.Name = "lblClasses";
            this.lblClasses.Size = new System.Drawing.Size(57, 16);
            this.lblClasses.TabIndex = 27;
            this.lblClasses.Text = "Classes";
            // 
            // lblBlankLines
            // 
            this.lblBlankLines.AutoSize = true;
            this.lblBlankLines.Location = new System.Drawing.Point(8, 89);
            this.lblBlankLines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlankLines.Name = "lblBlankLines";
            this.lblBlankLines.Size = new System.Drawing.Size(77, 16);
            this.lblBlankLines.TabIndex = 26;
            this.lblBlankLines.Text = "Blank Lines";
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(8, 62);
            this.lblComments.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(72, 16);
            this.lblComments.TabIndex = 25;
            this.lblComments.Text = "Comments";
            // 
            // lblFunctions
            // 
            this.lblFunctions.AutoSize = true;
            this.lblFunctions.Location = new System.Drawing.Point(8, 34);
            this.lblFunctions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFunctions.Name = "lblFunctions";
            this.lblFunctions.Size = new System.Drawing.Size(65, 16);
            this.lblFunctions.TabIndex = 24;
            this.lblFunctions.Text = "Functions";
            // 
            // chkShowLineNumbers
            // 
            this.chkShowLineNumbers.AutoSize = true;
            this.chkShowLineNumbers.BackColor = System.Drawing.SystemColors.Control;
            this.chkShowLineNumbers.Checked = global::TARLABS.VBScriptFormatter.Properties.Settings.Default.ShowLineNumbers;
            this.chkShowLineNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLineNumbers.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TARLABS.VBScriptFormatter.Properties.Settings.Default, "ShowLineNumbers", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkShowLineNumbers.Location = new System.Drawing.Point(20, 127);
            this.chkShowLineNumbers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkShowLineNumbers.Name = "chkShowLineNumbers";
            this.chkShowLineNumbers.Size = new System.Drawing.Size(105, 20);
            this.chkShowLineNumbers.TabIndex = 31;
            this.chkShowLineNumbers.Text = "Show Line #s";
            this.chkShowLineNumbers.UseVisualStyleBackColor = false;
            this.chkShowLineNumbers.CheckedChanged += new System.EventHandler(this.chkShowLineNumbers_CheckedChanged);
            // 
            // lnkTARLABS
            // 
            this.lnkTARLABS.AutoSize = true;
            this.lnkTARLABS.Location = new System.Drawing.Point(20, 326);
            this.lnkTARLABS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkTARLABS.Name = "lnkTARLABS";
            this.lnkTARLABS.Size = new System.Drawing.Size(140, 16);
            this.lnkTARLABS.TabIndex = 30;
            this.lnkTARLABS.TabStop = true;
            this.lnkTARLABS.Text = "http://www.tarlabs.com";
            this.lnkTARLABS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTARLABS_LinkClicked);
            // 
            // btnInstallUFTPlugin
            // 
            this.btnInstallUFTPlugin.BackColor = System.Drawing.SystemColors.Control;
            this.btnInstallUFTPlugin.Location = new System.Drawing.Point(20, 231);
            this.btnInstallUFTPlugin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnInstallUFTPlugin.Name = "btnInstallUFTPlugin";
            this.btnInstallUFTPlugin.Size = new System.Drawing.Size(237, 26);
            this.btnInstallUFTPlugin.TabIndex = 29;
            this.btnInstallUFTPlugin.Text = "Install HP UFT Plugin";
            this.btnInstallUFTPlugin.UseVisualStyleBackColor = false;
            this.btnInstallUFTPlugin.Click += new System.EventHandler(this.btnInstallUFTPlugin_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(20, 270);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(237, 26);
            this.btnExit.TabIndex = 28;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // chkRemoveComments
            // 
            this.chkRemoveComments.AutoSize = true;
            this.chkRemoveComments.BackColor = System.Drawing.SystemColors.Control;
            this.chkRemoveComments.Checked = global::TARLABS.VBScriptFormatter.Properties.Settings.Default.RemoveComments;
            this.chkRemoveComments.Location = new System.Drawing.Point(20, 94);
            this.chkRemoveComments.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkRemoveComments.Name = "chkRemoveComments";
            this.chkRemoveComments.Size = new System.Drawing.Size(146, 20);
            this.chkRemoveComments.TabIndex = 27;
            this.chkRemoveComments.Text = "Remove Comments";
            this.chkRemoveComments.UseVisualStyleBackColor = false;
            this.chkRemoveComments.CheckedChanged += new System.EventHandler(this.chkRemoveComments_CheckedChanged);
            // 
            // cmbIndetationCharacter
            // 
            this.cmbIndetationCharacter.BackColor = System.Drawing.SystemColors.Control;
            this.cmbIndetationCharacter.DisplayMember = "1";
            this.cmbIndetationCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIndetationCharacter.FormattingEnabled = true;
            this.cmbIndetationCharacter.Items.AddRange(new object[] {
            "1 Tab",
            "2 Tabs",
            "1 Space",
            "2 Spaces",
            "3 Spaces",
            "4 Spaces"});
            this.cmbIndetationCharacter.Location = new System.Drawing.Point(20, 55);
            this.cmbIndetationCharacter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbIndetationCharacter.Name = "cmbIndetationCharacter";
            this.cmbIndetationCharacter.Size = new System.Drawing.Size(236, 24);
            this.cmbIndetationCharacter.TabIndex = 26;
            this.cmbIndetationCharacter.Text = global::TARLABS.VBScriptFormatter.Properties.Settings.Default.IdentationCharacter;
            this.cmbIndetationCharacter.SelectedIndexChanged += new System.EventHandler(this.cmbIndetationCharacter_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(20, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "Indetation Character";
            // 
            // txtSourceCode
            // 
            this.txtSourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceCode.AutoScrollMinSize = new System.Drawing.Size(459, 375);
            this.txtSourceCode.BackBrush = null;
            this.txtSourceCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSourceCode.CharHeight = 15;
            this.txtSourceCode.CharWidth = 8;
            this.txtSourceCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSourceCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtSourceCode.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtSourceCode.IsReplaceMode = false;
            this.txtSourceCode.Location = new System.Drawing.Point(16, 33);
            this.txtSourceCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSourceCode.Name = "txtSourceCode";
            this.txtSourceCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtSourceCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtSourceCode.Size = new System.Drawing.Size(1145, 621);
            this.txtSourceCode.TabIndex = 7;
            this.txtSourceCode.Text = resources.GetString("txtSourceCode.Text");
            this.txtSourceCode.Zoom = 100;
            this.txtSourceCode.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtSourceCode_TextChangedDelayed);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 673);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1473, 22);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBarInfo
            // 
            this.statusBarInfo.AutoToolTip = true;
            this.statusBarInfo.Name = "statusBarInfo";
            this.statusBarInfo.Size = new System.Drawing.Size(169, 17);
            this.statusBarInfo.Text = "                                                      ";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1473, 695);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSourceCode);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VBScript Source Code Formatter - by http://www.tarlabs.com";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceCode)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox txtSourceCode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rTFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tEXTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iMAGEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visitWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rTFToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tEXTToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iMAGEToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem formatCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ErrorProvider errProvider;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblConstants;
        private System.Windows.Forms.Label lblVariables;
        private System.Windows.Forms.Label lblClasses;
        private System.Windows.Forms.Label lblBlankLines;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.Label lblFunctions;
        private System.Windows.Forms.CheckBox chkShowLineNumbers;
        private System.Windows.Forms.LinkLabel lnkTARLABS;
        private System.Windows.Forms.Button btnInstallUFTPlugin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox chkRemoveComments;
        private System.Windows.Forms.ComboBox cmbIndetationCharacter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarInfo;
    }
}