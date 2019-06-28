namespace GRWLifesaver.Form
{

    public class MainForm : System.Windows.Forms.Form
    {

        private System.Windows.Forms.Label LB_MajorMessage;

        private System.Windows.Forms.Label LB_BackupFolder;
        private System.Windows.Forms.TextBox TB_BackupFolder;
        private System.Windows.Forms.FolderBrowserDialog FBD_BackupFolder;

        private System.Windows.Forms.Label LB_AccountUUIDs;
        private System.Windows.Forms.ComboBox CB_AccountUUIDs;
        private System.Windows.Forms.TabControl TC_Backups;
        private System.Windows.Forms.Label LB_SystemTime;
        private System.Windows.Forms.Label LB_BackupAll;
        private System.Windows.Forms.TabPage TP_UplayPCBackups;
        private System.Windows.Forms.TabPage TP_SteamPCBackups;
        private System.Windows.Forms.ListView LV_UplayPCBackups;
        private System.Windows.Forms.ListView LV_SteamPCBackups;

        private System.Windows.Forms.ListViewItem LVI_UplayPCBackups_Slot1;
        private System.Windows.Forms.ListViewItem LVI_UplayPCBackups_Slot2;
        private System.Windows.Forms.ListViewItem LVI_UplayPCBackups_Slot3;
        private System.Windows.Forms.ListViewItem LVI_UplayPCBackups_Slot4;
        private System.Windows.Forms.ListViewItem LVI_UplayPCBackups_Slot5;
        private System.Windows.Forms.ListViewItem LVI_UplayPCBackups_Slot6;
        private System.Windows.Forms.ListViewItem LVI_UplayPCBackups_Slot7;
        private System.Windows.Forms.ListViewItem LVI_UplayPCBackups_Slot8;

        private System.Windows.Forms.ListViewItem LVI_SteamPCBackups_Slot1;
        private System.Windows.Forms.ListViewItem LVI_SteamPCBackups_Slot2;
        private System.Windows.Forms.ListViewItem LVI_SteamPCBackups_Slot3;
        private System.Windows.Forms.ListViewItem LVI_SteamPCBackups_Slot4;
        private System.Windows.Forms.ListViewItem LVI_SteamPCBackups_Slot5;
        private System.Windows.Forms.ListViewItem LVI_SteamPCBackups_Slot6;
        private System.Windows.Forms.ListViewItem LVI_SteamPCBackups_Slot7;
        private System.Windows.Forms.ListViewItem LVI_SteamPCBackups_Slot8;

        private System.Windows.Forms.ContextMenuStrip CM_UplayPCBackups;
        private System.Windows.Forms.ToolStripItem MI_UplayPCBackups_CreateBackup;
        private System.Windows.Forms.ToolStripItem MI_UplayPCBackups_Backups;
        private System.Windows.Forms.ToolStripItem MI_UplayPCBackups_EnableBackup;

        private System.Windows.Forms.ContextMenuStrip CM_SteamPCBackups;
        private System.Windows.Forms.ToolStripItem MI_SteamPCBackups_CreateBackup;
        private System.Windows.Forms.ToolStripItem MI_SteamPCBackups_Backups;
        private System.Windows.Forms.ToolStripItem MI_SteamPCBackups_EnableBackup;

        private System.Windows.Forms.NotifyIcon NI_MainFormNotifyIcon;

        private BackupForm BF_BackupForm;

        private System.Windows.Forms.Timer Tmr_Updater;


        public MainForm(Program program)
        {
            if (program is null) throw new System.ArgumentNullException("program");

            this.Program = program;
            this.InitializeComponent();

            if (this.Program.GRWLifesaver.UplayFolder is null) {
                this.DisplayMajorMessage("UPlay seems to be not installed on this System.", true, System.Drawing.Color.Red);
            } else {
                this.SetDisplayEnabled(true);

                this.TB_BackupFolder.Text = this.Program.GRWLifesaver.BackupFolder;
                this.TB_BackupFolder.Select(0, 0);

                this.CB_AccountUUIDs.Items.AddRange(this.Program.GRWLifesaver.AccountProfiles.ToUUIDArray());
                if (this.CB_AccountUUIDs.Items.Count > 0) {
                    this.CB_AccountUUIDs.SelectedIndex = 0;
                }
            }
        }


        public GRWLifesaver.AccountProfile AccountProfile
        {
            get;
            set;
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // this properties
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(590, 325);

            this.Icon = Properties.Resources.GRW_101;
            this.Text = Program.Name;

            // this events
            this.FormClosing += this.MainForm_FormClosing;
            this.FormClosed += this.MainForm_FormClosed;
            this.Resize += this.MainForm_Resize;

            // NI_MainFormNotifyIcon
            this.NI_MainFormNotifyIcon = new System.Windows.Forms.NotifyIcon();
            this.NI_MainFormNotifyIcon.Icon = Properties.Resources.GRW_101;
            this.NI_MainFormNotifyIcon.Visible = true;
            this.NI_MainFormNotifyIcon.Text = "Ghost Recon Wildlands: Lifesaver";

            this.NI_MainFormNotifyIcon.DoubleClick += this.NI_MainFormNotifyIcon_DoubleClick;

            this.ResumeLayout(true);
        }


        private void HideToTray()
        {
            this.Hide();
        }


        private void ShowFromTray()
        {
            this.Show();
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized) {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
            this.Activate();
            this.BringToFront();
        }


        private void NI_MainFormNotifyIcon_DoubleClick(object sender, System.EventArgs e)
        {
            this.ShowFromTray();
        }


        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized) {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            } else if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized) {
                this.HideToTray();
            }
        }


        private void MainForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            this.NI_MainFormNotifyIcon.Dispose();
            this.NI_MainFormNotifyIcon = null;
        }


        private void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (GRWLifesaver.SaveGame.BackupService.IsEnabled) {
                GRWLifesaver.SaveGame.BackupService.Disable(200);

                if (GRWLifesaver.SaveGame.BackupService.IsEnabled) {
                    e.Cancel = true;
                }
            }
        }


        private void SetDisplayEnabled(System.Boolean enabled)
        {
            this.SuspendLayout();

            if (enabled) {
                // LB_BackupFolder
                if (this.LB_BackupFolder is null) {
                    this.LB_BackupFolder = new System.Windows.Forms.Label();
                    this.LB_BackupFolder.Name = "LB_BackupFolder";
                    this.LB_BackupFolder.Parent = this;

                    this.LB_BackupFolder.AutoSize = true;
                    this.LB_BackupFolder.Text = "Backup Folder";
                }

                // TB_BackupFolder
                if (this.TB_BackupFolder is null) {
                    this.TB_BackupFolder = new System.Windows.Forms.TextBox();
                    this.TB_BackupFolder.Name = "TB_BackupFolder";
                    this.TB_BackupFolder.Parent = this;

                    this.TB_BackupFolder.ReadOnly = true;

                    this.TB_BackupFolder.Click += this.TB_BackupFolder_Click;
                }

                // LB_AccountUUIDs
                if (this.LB_AccountUUIDs is null) {
                    this.LB_AccountUUIDs = new System.Windows.Forms.Label();
                    this.LB_AccountUUIDs.Name = "LB_AccountUUIDs";
                    this.LB_AccountUUIDs.Parent = this;

                    this.LB_AccountUUIDs.AutoSize = true;
                    this.LB_AccountUUIDs.Text = "Account UUID";
                }

                // CB_AccountUUIDs
                if (this.CB_AccountUUIDs is null) {
                    this.CB_AccountUUIDs = new System.Windows.Forms.ComboBox();
                    this.CB_AccountUUIDs.Name = "CB_AccountUUIDs";
                    this.CB_AccountUUIDs.Parent = this;

                    this.CB_AccountUUIDs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    this.CB_AccountUUIDs.SelectedValueChanged += this.CB_AccountUUIDs_SelectedValueChanged;
                }

                // FBD_BackupFolder
                if (this.FBD_BackupFolder is null) {
                    this.FBD_BackupFolder = new System.Windows.Forms.FolderBrowserDialog();
                    this.FBD_BackupFolder.Description = "Please select the Folder where you want to save the backups";
                }

                // this events
                this.Layout += this.FormLayout;
            } else {
                this.Layout -= this.FormLayout;

                // Tmr_Updater
                if (!(this.Tmr_Updater is null)) {
                    this.Tmr_Updater.Enabled = false;
                    this.Tmr_Updater.Dispose();
                    this.Tmr_Updater = null;
                }

                // LB_BackupFolder
                if (!(this.LB_BackupFolder is null)) {
                    this.LB_BackupFolder.Parent = null;
                    this.LB_BackupFolder.Dispose();
                    this.LB_BackupFolder = null;
                }

                // TB_BackupFolder
                if (!(this.TB_BackupFolder is null)) {
                    this.TB_BackupFolder.Parent = null;
                    this.TB_BackupFolder.Dispose();
                    this.TB_BackupFolder = null;
                }

                // LB_AccountUUIDs
                if (!(this.LB_AccountUUIDs is null)) {
                    this.LB_AccountUUIDs.Parent = null;
                    this.LB_AccountUUIDs.Dispose();
                    this.LB_AccountUUIDs = null;
                }

                // CB_AccountUUIDs
                if (!(this.CB_AccountUUIDs is null)) {
                    this.CB_AccountUUIDs.Parent = null;
                    this.CB_AccountUUIDs.Dispose();
                    this.CB_AccountUUIDs = null;
                }

                // FBD_BackupFolder
                if (!(this.FBD_BackupFolder is null)) {
                    this.FBD_BackupFolder.Dispose();
                    this.FBD_BackupFolder = null;
                }
            }

            this.ResumeLayout(true);
        }


        private void SetDisplaySaveGames() => this.SetDisplaySaveGames(null);


        private void SetDisplaySaveGames(GRWLifesaver.AccountProfile accountProfile)
        {
            this.SuspendLayout();

            this.AccountProfile = accountProfile;

            if (!(this.AccountProfile is null)) {
                // LB_SystemTime
                if (this.LB_SystemTime is null) {
                    this.LB_SystemTime = new System.Windows.Forms.Label();
                    this.LB_SystemTime.Name = "LB_SystemTime";
                    this.LB_SystemTime.Parent = this;

                    this.LB_SystemTime.AutoSize = true;
                    this._updateSystemTime();
                }

                // LB_BackupAll
                if (this.LB_BackupAll is null) {
                    this.LB_BackupAll = new System.Windows.Forms.Label();
                    this.LB_BackupAll.Name = "LB_BackupAll";
                    this.LB_BackupAll.Parent = this;

                    this.LB_BackupAll.AutoSize = true;
                    this.LB_BackupAll.Text = "Enable: Auto-Backup All";
                    this.LB_BackupAll.MouseEnter += this.LB_BackupAll_MouseEnter;
                    this.LB_BackupAll.MouseLeave += this.LB_BackupAll_MouseLeave;
                    this.LB_BackupAll.Click += this.LB_BackupAll_Click;
                }

                // TC_Backups
                if (this.TC_Backups is null) {
                    this.TC_Backups = new System.Windows.Forms.TabControl();
                    this.TC_Backups.Name = "TC_Backups";
                    this.TC_Backups.Parent = this;
                }

                // TP_UplayPCBackups
                if (this.TP_UplayPCBackups is null) {
                    this.TP_UplayPCBackups = new System.Windows.Forms.TabPage();
                    this.TP_UplayPCBackups.Name = "TP_UplayPCBackups";
                    this.TP_UplayPCBackups.Parent = this.TC_Backups;

                    this.TP_UplayPCBackups.Text = "Uplay PC";
                }

                // TP_SteamPCBackups
                if (this.TP_SteamPCBackups is null) {
                    this.TP_SteamPCBackups = new System.Windows.Forms.TabPage();
                    this.TP_SteamPCBackups.Name = "TP_SteamPCBackups";
                    this.TP_SteamPCBackups.Parent = this.TC_Backups;

                    this.TP_SteamPCBackups.Text = "Steam PC";
                }

                // LVI_UplayPCBackups_Slot1
                if (this.LVI_UplayPCBackups_Slot1 is null) {
                    this.LVI_UplayPCBackups_Slot1 = new System.Windows.Forms.ListViewItem();
                    this.LVI_UplayPCBackups_Slot1.Text = "1";
                }

                // LVI_UplayPCBackups_Slot2
                if (this.LVI_UplayPCBackups_Slot2 is null) {
                    this.LVI_UplayPCBackups_Slot2 = new System.Windows.Forms.ListViewItem();
                    this.LVI_UplayPCBackups_Slot2.Text = "2";
                }

                // LVI_UplayPCBackups_Slot3
                if (this.LVI_UplayPCBackups_Slot3 is null) {
                    this.LVI_UplayPCBackups_Slot3 = new System.Windows.Forms.ListViewItem();
                    this.LVI_UplayPCBackups_Slot3.Text = "3";
                }

                // LVI_UplayPCBackups_Slot4
                if (this.LVI_UplayPCBackups_Slot4 is null) {
                    this.LVI_UplayPCBackups_Slot4 = new System.Windows.Forms.ListViewItem();
                    this.LVI_UplayPCBackups_Slot4.Text = "4";
                }

                // LVI_UplayPCBackups_Slot5
                if (this.LVI_UplayPCBackups_Slot5 is null) {
                    this.LVI_UplayPCBackups_Slot5 = new System.Windows.Forms.ListViewItem();
                    this.LVI_UplayPCBackups_Slot5.Text = "5";
                }

                // LVI_UplayPCBackups_Slot6
                if (this.LVI_UplayPCBackups_Slot6 is null) {
                    this.LVI_UplayPCBackups_Slot6 = new System.Windows.Forms.ListViewItem();
                    this.LVI_UplayPCBackups_Slot6.Text = "6";
                }

                // LVI_UplayPCBackups_Slot7
                if (this.LVI_UplayPCBackups_Slot7 is null) {
                    this.LVI_UplayPCBackups_Slot7 = new System.Windows.Forms.ListViewItem();
                    this.LVI_UplayPCBackups_Slot7.Text = "7";
                }

                // LVI_UplayPCBackups_Slot8
                if (this.LVI_UplayPCBackups_Slot8 is null) {
                    this.LVI_UplayPCBackups_Slot8 = new System.Windows.Forms.ListViewItem();
                    this.LVI_UplayPCBackups_Slot8.Text = "8";
                }

                // LV_UplayPCBackups
                if (this.LV_UplayPCBackups is null) {
                    this.LV_UplayPCBackups = new System.Windows.Forms.ListView();
                    this.LV_UplayPCBackups.Name = "LV_UplayPCBackups";
                    this.LV_UplayPCBackups.Parent = this.TP_UplayPCBackups;

                    this.LV_UplayPCBackups.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.LV_UplayPCBackups.BorderStyle = System.Windows.Forms.BorderStyle.None;

                    this.LV_UplayPCBackups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
                    this.LV_UplayPCBackups.View = System.Windows.Forms.View.Details;
                    this.LV_UplayPCBackups.FullRowSelect = true;

                    this.LV_UplayPCBackups.Columns.Add("Slot", 40);
                    this.LV_UplayPCBackups.Columns.Add("Savegame", 200);
                    this.LV_UplayPCBackups.Columns.Add("Latest Backup", 200);
                    this.LV_UplayPCBackups.Columns.Add("Backup Enabled", 100);

                    this.LV_UplayPCBackups.ColumnWidthChanging += this.LV_UplayPCBackups_ColumnWidthChanging;

                    this.LV_UplayPCBackups.Items.Add(this.LVI_UplayPCBackups_Slot1);
                    this.LVI_UplayPCBackups_Slot1.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_UplayPCBackups.Items.Add(this.LVI_UplayPCBackups_Slot2);
                    this.LVI_UplayPCBackups_Slot2.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_UplayPCBackups.Items.Add(this.LVI_UplayPCBackups_Slot3);
                    this.LVI_UplayPCBackups_Slot3.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_UplayPCBackups.Items.Add(this.LVI_UplayPCBackups_Slot4);
                    this.LVI_UplayPCBackups_Slot4.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_UplayPCBackups.Items.Add(this.LVI_UplayPCBackups_Slot5);
                    this.LVI_UplayPCBackups_Slot5.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_UplayPCBackups.Items.Add(this.LVI_UplayPCBackups_Slot6);
                    this.LVI_UplayPCBackups_Slot6.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_UplayPCBackups.Items.Add(this.LVI_UplayPCBackups_Slot7);
                    this.LVI_UplayPCBackups_Slot7.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_UplayPCBackups.Items.Add(this.LVI_UplayPCBackups_Slot8);
                    this.LVI_UplayPCBackups_Slot8.SubItems.AddRange(new System.String[] { "", "", "" });


                    this.LV_UplayPCBackups.MouseUp += this.LV_UplayPCBackups_MouseUp;
                }

                // CM_UplayPCBackups
                if (this.CM_UplayPCBackups is null) {
                    this.CM_UplayPCBackups = new System.Windows.Forms.ContextMenuStrip();
                    this.CM_UplayPCBackups.Name = "CM_UplayPCBackups";

                    this.MI_UplayPCBackups_CreateBackup = this.CM_UplayPCBackups.Items.Add("Create backup now");
                    this.MI_UplayPCBackups_CreateBackup.MouseUp += this.CM_UplayPCBackups_CreateBackup_MouseUp;

                    this.MI_UplayPCBackups_Backups = this.CM_UplayPCBackups.Items.Add("Backups");
                    this.MI_UplayPCBackups_Backups.MouseUp += this.MI_UplayPCBackups_Backups_MouseUp;

                    this.MI_UplayPCBackups_EnableBackup = this.CM_UplayPCBackups.Items.Add("Enable: Auto-Backup");
                    this.MI_UplayPCBackups_EnableBackup.MouseUp += this.MI_UplayPCBackups_EnableBackup_MouseUp;
                }

                // LVI_SteamPCBackups_Slot1
                if (this.LVI_SteamPCBackups_Slot1 is null) {
                    this.LVI_SteamPCBackups_Slot1 = new System.Windows.Forms.ListViewItem();
                    this.LVI_SteamPCBackups_Slot1.Text = "1";
                }

                // LVI_SteamPCBackups_Slot2
                if (this.LVI_SteamPCBackups_Slot2 is null) {
                    this.LVI_SteamPCBackups_Slot2 = new System.Windows.Forms.ListViewItem();
                    this.LVI_SteamPCBackups_Slot2.Text = "2";
                }

                // LVI_SteamPCBackups_Slot3
                if (this.LVI_SteamPCBackups_Slot3 is null) {
                    this.LVI_SteamPCBackups_Slot3 = new System.Windows.Forms.ListViewItem();
                    this.LVI_SteamPCBackups_Slot3.Text = "3";
                }

                // LVI_SteamPCBackups_Slot4
                if (this.LVI_SteamPCBackups_Slot4 is null) {
                    this.LVI_SteamPCBackups_Slot4 = new System.Windows.Forms.ListViewItem();
                    this.LVI_SteamPCBackups_Slot4.Text = "4";
                }

                // LVI_SteamPCBackups_Slot5
                if (this.LVI_SteamPCBackups_Slot5 is null) {
                    this.LVI_SteamPCBackups_Slot5 = new System.Windows.Forms.ListViewItem();
                    this.LVI_SteamPCBackups_Slot5.Text = "5";
                }

                // LVI_SteamPCBackups_Slot6
                if (this.LVI_SteamPCBackups_Slot6 is null) {
                    this.LVI_SteamPCBackups_Slot6 = new System.Windows.Forms.ListViewItem();
                    this.LVI_SteamPCBackups_Slot6.Text = "6";
                }

                // LVI_SteamPCBackups_Slot7
                if (this.LVI_SteamPCBackups_Slot7 is null) {
                    this.LVI_SteamPCBackups_Slot7 = new System.Windows.Forms.ListViewItem();
                    this.LVI_SteamPCBackups_Slot7.Text = "7";
                }

                // LVI_SteamPCBackups_Slot8
                if (this.LVI_SteamPCBackups_Slot8 is null) {
                    this.LVI_SteamPCBackups_Slot8 = new System.Windows.Forms.ListViewItem();
                    this.LVI_SteamPCBackups_Slot8.Text = "8";
                }

                // LV_SteamPCBackups
                if (this.LV_SteamPCBackups is null) {
                    this.LV_SteamPCBackups = new System.Windows.Forms.ListView();
                    this.LV_SteamPCBackups.Name = "LV_SteamPCBackups";
                    this.LV_SteamPCBackups.Parent = this.TP_SteamPCBackups;

                    this.LV_SteamPCBackups.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.LV_SteamPCBackups.BorderStyle = System.Windows.Forms.BorderStyle.None;

                    this.LV_SteamPCBackups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
                    this.LV_SteamPCBackups.View = System.Windows.Forms.View.Details;
                    this.LV_SteamPCBackups.FullRowSelect = true;

                    this.LV_SteamPCBackups.Columns.Add("Slot", 40);
                    this.LV_SteamPCBackups.Columns.Add("Savegame", 200);
                    this.LV_SteamPCBackups.Columns.Add("Latest Backup", 200);
                    this.LV_SteamPCBackups.Columns.Add("Backup Enabled", 100);
                
                    this.LV_SteamPCBackups.ColumnWidthChanging += this.LV_SteamPCBackups_ColumnWidthChanging;

                    this.LV_SteamPCBackups.Items.Add(this.LVI_SteamPCBackups_Slot1);
                    this.LVI_SteamPCBackups_Slot1.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_SteamPCBackups.Items.Add(this.LVI_SteamPCBackups_Slot2);
                    this.LVI_SteamPCBackups_Slot2.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_SteamPCBackups.Items.Add(this.LVI_SteamPCBackups_Slot3);
                    this.LVI_SteamPCBackups_Slot3.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_SteamPCBackups.Items.Add(this.LVI_SteamPCBackups_Slot4);
                    this.LVI_SteamPCBackups_Slot4.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_SteamPCBackups.Items.Add(this.LVI_SteamPCBackups_Slot5);
                    this.LVI_SteamPCBackups_Slot5.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_SteamPCBackups.Items.Add(this.LVI_SteamPCBackups_Slot6);
                    this.LVI_SteamPCBackups_Slot6.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_SteamPCBackups.Items.Add(this.LVI_SteamPCBackups_Slot7);
                    this.LVI_SteamPCBackups_Slot7.SubItems.AddRange(new System.String[] { "", "", "" });

                    this.LV_SteamPCBackups.Items.Add(this.LVI_SteamPCBackups_Slot8);
                    this.LVI_SteamPCBackups_Slot8.SubItems.AddRange(new System.String[] { "", "", "" });


                    this.LV_SteamPCBackups.MouseUp += this.LV_SteamPCBackups_MouseUp;
                }

                // BF_BackupForm
                if (this.BF_BackupForm is null) {
                    this.BF_BackupForm = new BackupForm();
                }

                // CM_SteamPCBackups
                if (this.CM_SteamPCBackups is null) {
                    this.CM_SteamPCBackups = new System.Windows.Forms.ContextMenuStrip();
                    this.CM_SteamPCBackups.Name = "CM_SteamPCBackups";

                    this.MI_SteamPCBackups_CreateBackup = this.CM_SteamPCBackups.Items.Add("Create backup now");
                    this.MI_SteamPCBackups_CreateBackup.MouseUp += this.MI_SteamPCBackups_CreateBackup_MouseUp;

                    this.MI_SteamPCBackups_Backups = this.CM_SteamPCBackups.Items.Add("Backups");
                    this.MI_SteamPCBackups_Backups.MouseUp += this.MI_SteamPCBackups_Backups_MouseUp;

                    this.MI_SteamPCBackups_EnableBackup = this.CM_SteamPCBackups.Items.Add("Enable: Auto-Backup");
                    this.MI_SteamPCBackups_EnableBackup.MouseUp += this.MI_SteamPCBackups_EnableBackup_MouseUp;
                }

                // Tmr_Updater
                if (this.Tmr_Updater is null) {
                    this.Tmr_Updater = new System.Windows.Forms.Timer();
                    this.Tmr_Updater.Tick += this.Tmr_Updater_Tick;
                    this.Tmr_Updater.Interval = 1000;
                    this.Tmr_Updater.Enabled = true;
                }
            } else {
                // LB_SystemTime
                if (this.LB_SystemTime is null) {
                    this.LB_SystemTime.Parent = null;
                    this.LB_SystemTime.Dispose();
                    this.LB_SystemTime = null;
                }

                // LB_BackupAll
                if (!(this.LB_BackupAll is null)) {
                    this.LB_BackupAll.Parent = null;
                    this.LB_BackupAll.Dispose();
                    this.LB_BackupAll = null;
                }

                // TC_Backups
                if (!(this.TC_Backups is null)) {
                    this.TC_Backups.Parent = null;
                    this.TC_Backups.Dispose();
                    this.TC_Backups = null;
                }

                // LVI_UplayPCBackups_Slot1
                if (!(this.LVI_UplayPCBackups_Slot1 is null)) {
                    this.LVI_UplayPCBackups_Slot1.Remove();
                    this.LVI_UplayPCBackups_Slot1 = null;
                }

                // LVI_UplayPCBackups_Slot2
                if (!(this.LVI_UplayPCBackups_Slot2 is null)) {
                    this.LVI_UplayPCBackups_Slot2.Remove();
                    this.LVI_UplayPCBackups_Slot2 = null;
                }

                // LVI_UplayPCBackups_Slot3
                if (!(this.LVI_UplayPCBackups_Slot3 is null)) {
                    this.LVI_UplayPCBackups_Slot3.Remove();
                    this.LVI_UplayPCBackups_Slot3 = null;
                }

                // LVI_UplayPCBackups_Slot4
                if (!(this.LVI_UplayPCBackups_Slot4 is null)) {
                    this.LVI_UplayPCBackups_Slot4.Remove();
                    this.LVI_UplayPCBackups_Slot4 = null;
                }

                // LVI_UplayPCBackups_Slot5
                if (!(this.LVI_UplayPCBackups_Slot5 is null)) {
                    this.LVI_UplayPCBackups_Slot5.Remove();
                    this.LVI_UplayPCBackups_Slot5 = null;
                }

                // LVI_UplayPCBackups_Slot6
                if (!(this.LVI_UplayPCBackups_Slot6 is null)) {
                    this.LVI_UplayPCBackups_Slot6.Remove();
                    this.LVI_UplayPCBackups_Slot6 = null;
                }

                // LVI_UplayPCBackups_Slot7
                if (!(this.LVI_UplayPCBackups_Slot7 is null)) {
                    this.LVI_UplayPCBackups_Slot7.Remove();
                    this.LVI_UplayPCBackups_Slot7 = null;
                }

                // LVI_UplayPCBackups_Slot8
                if (!(this.LVI_UplayPCBackups_Slot8 is null)) {
                    this.LVI_UplayPCBackups_Slot8.Remove();
                    this.LVI_UplayPCBackups_Slot8 = null;
                }

                // LV_UplayPCBackups
                if (!(this.LV_UplayPCBackups is null)) {
                    this.LV_UplayPCBackups.Parent = null;
                    this.LV_UplayPCBackups.Dispose();
                    this.LV_UplayPCBackups = null;
                }

                // CM_UplayPCBackups
                if (!(this.CM_UplayPCBackups is null)) {
                    this.CM_UplayPCBackups.Dispose();
                    this.CM_UplayPCBackups = null;

                    this.MI_UplayPCBackups_CreateBackup.Dispose();
                    this.MI_UplayPCBackups_CreateBackup = null;

                    this.MI_UplayPCBackups_Backups.Dispose();
                    this.MI_UplayPCBackups_Backups = null;

                    this.MI_UplayPCBackups_EnableBackup.Dispose();
                    this.MI_UplayPCBackups_EnableBackup = null;
                }

                // LVI_SteamPCBackups_Slot1
                if (!(this.LVI_SteamPCBackups_Slot1 is null)) {
                    this.LVI_SteamPCBackups_Slot1.Remove();
                    this.LVI_SteamPCBackups_Slot1 = null;
                }

                // LVI_SteamPCBackups_Slot2
                if (!(this.LVI_SteamPCBackups_Slot2 is null)) {
                    this.LVI_SteamPCBackups_Slot2.Remove();
                    this.LVI_SteamPCBackups_Slot2 = null;
                }

                // LVI_SteamPCBackups_Slot3
                if (!(this.LVI_SteamPCBackups_Slot3 is null)) {
                    this.LVI_SteamPCBackups_Slot3.Remove();
                    this.LVI_SteamPCBackups_Slot3 = null;
                }

                // LVI_SteamPCBackups_Slot4
                if (!(this.LVI_SteamPCBackups_Slot4 is null)) {
                    this.LVI_SteamPCBackups_Slot4.Remove();
                    this.LVI_SteamPCBackups_Slot4 = null;
                }

                // LVI_SteamPCBackups_Slot5
                if (!(this.LVI_SteamPCBackups_Slot5 is null)) {
                    this.LVI_SteamPCBackups_Slot5.Remove();
                    this.LVI_SteamPCBackups_Slot5 = null;
                }

                // LVI_SteamPCBackups_Slot6
                if (!(this.LVI_SteamPCBackups_Slot6 is null)) {
                    this.LVI_SteamPCBackups_Slot6.Remove();
                    this.LVI_SteamPCBackups_Slot6 = null;
                }

                // LVI_SteamPCBackups_Slot7
                if (!(this.LVI_SteamPCBackups_Slot7 is null)) {
                    this.LVI_SteamPCBackups_Slot7.Remove();
                    this.LVI_SteamPCBackups_Slot7 = null;
                }

                // LVI_SteamPCBackups_Slot8
                if (!(this.LVI_SteamPCBackups_Slot8 is null)) {
                    this.LVI_SteamPCBackups_Slot8.Remove();
                    this.LVI_SteamPCBackups_Slot8 = null;
                }

                // LV_SteamPCBackups
                if (!(this.LV_SteamPCBackups is null)) {
                    this.LV_SteamPCBackups.Parent = null;
                    this.LV_SteamPCBackups.Dispose();
                    this.LV_SteamPCBackups = null;
                }
                
                // CM_SteamPCBackups
                if (!(this.CM_SteamPCBackups is null)) {
                    this.CM_SteamPCBackups.Dispose();
                    this.CM_SteamPCBackups = null;

                    this.MI_SteamPCBackups_CreateBackup.Dispose();
                    this.MI_SteamPCBackups_CreateBackup = null;

                    this.MI_SteamPCBackups_Backups.Dispose();
                    this.MI_SteamPCBackups_Backups = null;

                    this.MI_SteamPCBackups_EnableBackup.Dispose();
                    this.MI_SteamPCBackups_EnableBackup = null;
                }

                // BF_BackupForm
                if (!(this.BF_BackupForm is null)) {
                    this.BF_BackupForm.Dispose();
                    this.BF_BackupForm = null;
                }
            }

            this.ResumeLayout(true);
        }


        private void MI_SteamPCBackups_EnableBackup_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.LV_SteamPCBackups.SelectedIndices.Count == 1) {
                switch (this.LV_SteamPCBackups.SelectedIndices[0]) {
                    case 0:
                        this.AccountProfile.SteamPC_SaveGame1.BackupEnabled = !this.AccountProfile.SteamPC_SaveGame1.BackupEnabled;
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot1, this.AccountProfile.SteamPC_SaveGame1);
                        break;
                    case 1:
                        this.AccountProfile.SteamPC_SaveGame2.BackupEnabled = !this.AccountProfile.SteamPC_SaveGame2.BackupEnabled;
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot2, this.AccountProfile.SteamPC_SaveGame2);
                        break;
                    case 2:
                        this.AccountProfile.SteamPC_SaveGame3.BackupEnabled = !this.AccountProfile.SteamPC_SaveGame3.BackupEnabled;
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot3, this.AccountProfile.SteamPC_SaveGame3);
                        break;
                    case 3:
                        this.AccountProfile.SteamPC_SaveGame4.BackupEnabled = !this.AccountProfile.SteamPC_SaveGame4.BackupEnabled;
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot4, this.AccountProfile.SteamPC_SaveGame4);
                        break;
                    case 4:
                        this.AccountProfile.SteamPC_SaveGame5.BackupEnabled = !this.AccountProfile.SteamPC_SaveGame5.BackupEnabled;
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot5, this.AccountProfile.SteamPC_SaveGame5);
                        break;
                    case 5:
                        this.AccountProfile.SteamPC_SaveGame6.BackupEnabled = !this.AccountProfile.SteamPC_SaveGame6.BackupEnabled;
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot6, this.AccountProfile.SteamPC_SaveGame6);
                        break;
                    case 6:
                        this.AccountProfile.SteamPC_SaveGame7.BackupEnabled = !this.AccountProfile.SteamPC_SaveGame7.BackupEnabled;
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot7, this.AccountProfile.SteamPC_SaveGame7);
                        break;
                    case 7:
                        this.AccountProfile.SteamPC_SaveGame8.BackupEnabled = !this.AccountProfile.SteamPC_SaveGame8.BackupEnabled;
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot8, this.AccountProfile.SteamPC_SaveGame8);
                        break;
                }
            }
        }


        private void MI_UplayPCBackups_EnableBackup_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.LV_UplayPCBackups.SelectedIndices.Count == 1) {
                switch (this.LV_UplayPCBackups.SelectedIndices[0]) {
                    case 0:
                        this.AccountProfile.UplayPC_SaveGame1.BackupEnabled = !this.AccountProfile.UplayPC_SaveGame1.BackupEnabled;
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot1, this.AccountProfile.UplayPC_SaveGame1);
                        break;
                    case 1:
                        this.AccountProfile.UplayPC_SaveGame2.BackupEnabled = !this.AccountProfile.UplayPC_SaveGame2.BackupEnabled;
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot2, this.AccountProfile.UplayPC_SaveGame2);
                        break;
                    case 2:
                        this.AccountProfile.UplayPC_SaveGame3.BackupEnabled = !this.AccountProfile.UplayPC_SaveGame3.BackupEnabled;
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot3, this.AccountProfile.UplayPC_SaveGame3);
                        break;
                    case 3:
                        this.AccountProfile.UplayPC_SaveGame4.BackupEnabled = !this.AccountProfile.UplayPC_SaveGame4.BackupEnabled;
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot4, this.AccountProfile.UplayPC_SaveGame4);
                        break;
                    case 4:
                        this.AccountProfile.UplayPC_SaveGame5.BackupEnabled = !this.AccountProfile.UplayPC_SaveGame5.BackupEnabled;
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot5, this.AccountProfile.UplayPC_SaveGame5);
                        break;
                    case 5:
                        this.AccountProfile.UplayPC_SaveGame6.BackupEnabled = !this.AccountProfile.UplayPC_SaveGame6.BackupEnabled;
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot6, this.AccountProfile.UplayPC_SaveGame6);
                        break;
                    case 6:
                        this.AccountProfile.UplayPC_SaveGame7.BackupEnabled = !this.AccountProfile.UplayPC_SaveGame7.BackupEnabled;
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot7, this.AccountProfile.UplayPC_SaveGame7);
                        break;
                    case 7:
                        this.AccountProfile.UplayPC_SaveGame8.BackupEnabled = !this.AccountProfile.UplayPC_SaveGame8.BackupEnabled;
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot8, this.AccountProfile.UplayPC_SaveGame8);
                        break;
                }
            }
        }


        private void MI_SteamPCBackups_CreateBackup_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.LV_SteamPCBackups.SelectedIndices.Count == 1) {
                switch (this.LV_SteamPCBackups.SelectedIndices[0]) {
                    case 0:
                        this.AccountProfile.SteamPC_SaveGame1.EnsureBackup();
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot1, this.AccountProfile.SteamPC_SaveGame1);
                        break;
                    case 1:
                        this.AccountProfile.SteamPC_SaveGame2.EnsureBackup();
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot2, this.AccountProfile.SteamPC_SaveGame2);
                        break;
                    case 2:
                        this.AccountProfile.SteamPC_SaveGame3.EnsureBackup();
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot3, this.AccountProfile.SteamPC_SaveGame3);
                        break;
                    case 3:
                        this.AccountProfile.SteamPC_SaveGame4.EnsureBackup();
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot4, this.AccountProfile.SteamPC_SaveGame4);
                        break;
                    case 4:
                        this.AccountProfile.SteamPC_SaveGame5.EnsureBackup();
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot5, this.AccountProfile.SteamPC_SaveGame5);
                        break;
                    case 5:
                        this.AccountProfile.SteamPC_SaveGame6.EnsureBackup();
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot6, this.AccountProfile.SteamPC_SaveGame6);
                        break;
                    case 6:
                        this.AccountProfile.SteamPC_SaveGame7.EnsureBackup();
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot7, this.AccountProfile.SteamPC_SaveGame7);
                        break;
                    case 7:
                        this.AccountProfile.SteamPC_SaveGame8.EnsureBackup();
                        this._updateSavegameChart(this.LVI_SteamPCBackups_Slot8, this.AccountProfile.SteamPC_SaveGame8);
                        break;
                }
            }
        }


        private void CM_UplayPCBackups_CreateBackup_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.LV_UplayPCBackups.SelectedIndices.Count == 1) {
                switch (this.LV_UplayPCBackups.SelectedIndices[0]) {
                    case 0:
                        this.AccountProfile.UplayPC_SaveGame1.EnsureBackup();
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot1, this.AccountProfile.UplayPC_SaveGame1);
                        break;
                    case 1:
                        this.AccountProfile.UplayPC_SaveGame2.EnsureBackup();
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot2, this.AccountProfile.UplayPC_SaveGame2);
                        break;
                    case 2:
                        this.AccountProfile.UplayPC_SaveGame3.EnsureBackup();
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot3, this.AccountProfile.UplayPC_SaveGame3);
                        break;
                    case 3:
                        this.AccountProfile.UplayPC_SaveGame4.EnsureBackup();
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot4, this.AccountProfile.UplayPC_SaveGame4);
                        break;
                    case 4:
                        this.AccountProfile.UplayPC_SaveGame5.EnsureBackup();
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot5, this.AccountProfile.UplayPC_SaveGame5);
                        break;
                    case 5:
                        this.AccountProfile.UplayPC_SaveGame6.EnsureBackup();
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot6, this.AccountProfile.UplayPC_SaveGame6);
                        break;
                    case 6:
                        this.AccountProfile.UplayPC_SaveGame7.EnsureBackup();
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot7, this.AccountProfile.UplayPC_SaveGame7);
                        break;
                    case 7:
                        this.AccountProfile.UplayPC_SaveGame8.EnsureBackup();
                        this._updateSavegameChart(this.LVI_UplayPCBackups_Slot8, this.AccountProfile.UplayPC_SaveGame8);
                        break;
                }
            }
        }


        private void LV_UplayPCBackups_ColumnWidthChanging(object sender, System.Windows.Forms.ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = this.LV_UplayPCBackups.Columns[e.ColumnIndex].Width;
        }


        private void LV_SteamPCBackups_ColumnWidthChanging(object sender, System.Windows.Forms.ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = this.LV_SteamPCBackups.Columns[e.ColumnIndex].Width;
        }


        private void LV_UplayPCBackups_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                System.Windows.Forms.ListViewItem listViewItem = this.LV_UplayPCBackups.GetItemAt(e.X, e.Y);

                if (!(listViewItem is null)) {
                    System.Drawing.Point loc = this.LV_UplayPCBackups.PointToScreen(e.Location);
                    loc = new System.Drawing.Point(loc.X + 12, loc.Y + 12);

                    System.Boolean saveGameExist = false;
                    System.Boolean backupExist = false;
                    System.Boolean showEnableBackup = true;

                    if (this.LV_UplayPCBackups.SelectedIndices.Count == 1) {
                        switch (this.LV_UplayPCBackups.SelectedIndices[0]) {
                            case 0:
                                saveGameExist = this.AccountProfile.UplayPC_SaveGame1.Exists;
                                backupExist = this.AccountProfile.UplayPC_SaveGame1.BackupExists;
                                showEnableBackup = !this.AccountProfile.UplayPC_SaveGame1.BackupEnabled;
                                break;
                            case 1:
                                saveGameExist = this.AccountProfile.UplayPC_SaveGame2.Exists;
                                backupExist = this.AccountProfile.UplayPC_SaveGame2.BackupExists;
                                showEnableBackup = !this.AccountProfile.UplayPC_SaveGame2.BackupEnabled;
                                break;
                            case 2:
                                saveGameExist = this.AccountProfile.UplayPC_SaveGame3.Exists;
                                backupExist = this.AccountProfile.UplayPC_SaveGame3.BackupExists;
                                showEnableBackup = !this.AccountProfile.UplayPC_SaveGame3.BackupEnabled;
                                break;
                            case 3:
                                saveGameExist = this.AccountProfile.UplayPC_SaveGame4.Exists;
                                backupExist = this.AccountProfile.UplayPC_SaveGame4.BackupExists;
                                showEnableBackup = !this.AccountProfile.UplayPC_SaveGame4.BackupEnabled;
                                break;
                            case 4:
                                saveGameExist = this.AccountProfile.UplayPC_SaveGame5.Exists;
                                backupExist = this.AccountProfile.UplayPC_SaveGame5.BackupExists;
                                showEnableBackup = !this.AccountProfile.UplayPC_SaveGame5.BackupEnabled;
                                break;
                            case 5:
                                saveGameExist = this.AccountProfile.UplayPC_SaveGame6.Exists;
                                backupExist = this.AccountProfile.UplayPC_SaveGame6.BackupExists;
                                showEnableBackup = !this.AccountProfile.UplayPC_SaveGame6.BackupEnabled;
                                break;
                            case 6:
                                saveGameExist = this.AccountProfile.UplayPC_SaveGame7.Exists;
                                backupExist = this.AccountProfile.UplayPC_SaveGame7.BackupExists;
                                showEnableBackup = !this.AccountProfile.UplayPC_SaveGame7.BackupEnabled;
                                break;
                            case 7:
                                saveGameExist = this.AccountProfile.UplayPC_SaveGame8.Exists;
                                backupExist = this.AccountProfile.UplayPC_SaveGame8.BackupExists;
                                showEnableBackup = !this.AccountProfile.UplayPC_SaveGame8.BackupEnabled;
                                break;
                        }
                    }

                    this.MI_UplayPCBackups_CreateBackup.Enabled = saveGameExist;
                    this.MI_UplayPCBackups_Backups.Enabled = backupExist;

                    if (showEnableBackup) {
                        this.MI_UplayPCBackups_EnableBackup.Text = "Enable: Auto-Backup";
                    } else {
                        this.MI_UplayPCBackups_EnableBackup.Text = "Disable: Auto-Backup";
                    }

                    this.CM_UplayPCBackups.Show(loc);
                }
            }
        }


        private void LV_SteamPCBackups_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                System.Windows.Forms.ListViewItem listViewItem = this.LV_SteamPCBackups.GetItemAt(e.X, e.Y);

                if (!(listViewItem is null)) {
                    System.Drawing.Point loc = this.LV_SteamPCBackups.PointToScreen(e.Location);
                    loc = new System.Drawing.Point(loc.X + 6, loc.Y + 6);

                    System.Boolean saveGameExist = false;
                    System.Boolean backupExist = false;
                    System.Boolean showEnableBackup = true;

                    if (this.LV_SteamPCBackups.SelectedIndices.Count == 1) {
                        switch (this.LV_SteamPCBackups.SelectedIndices[0]) {
                            case 0:
                                saveGameExist = this.AccountProfile.SteamPC_SaveGame1.Exists;
                                backupExist = this.AccountProfile.SteamPC_SaveGame1.BackupExists;
                                showEnableBackup = !this.AccountProfile.SteamPC_SaveGame1.BackupEnabled;
                                break;
                            case 1:
                                saveGameExist = this.AccountProfile.SteamPC_SaveGame2.Exists;
                                backupExist = this.AccountProfile.SteamPC_SaveGame2.BackupExists;
                                showEnableBackup = !this.AccountProfile.SteamPC_SaveGame2.BackupEnabled;
                                break;
                            case 2:
                                saveGameExist = this.AccountProfile.SteamPC_SaveGame3.Exists;
                                backupExist = this.AccountProfile.SteamPC_SaveGame3.BackupExists;
                                showEnableBackup = !this.AccountProfile.SteamPC_SaveGame3.BackupEnabled;
                                break;
                            case 3:
                                saveGameExist = this.AccountProfile.SteamPC_SaveGame4.Exists;
                                backupExist = this.AccountProfile.SteamPC_SaveGame4.BackupExists;
                                showEnableBackup = !this.AccountProfile.SteamPC_SaveGame4.BackupEnabled;
                                break;
                            case 4:
                                saveGameExist = this.AccountProfile.SteamPC_SaveGame5.Exists;
                                backupExist = this.AccountProfile.SteamPC_SaveGame5.BackupExists;
                                showEnableBackup = !this.AccountProfile.SteamPC_SaveGame5.BackupEnabled;
                                break;
                            case 5:
                                saveGameExist = this.AccountProfile.SteamPC_SaveGame6.Exists;
                                backupExist = this.AccountProfile.SteamPC_SaveGame6.BackupExists;
                                showEnableBackup = !this.AccountProfile.SteamPC_SaveGame6.BackupEnabled;
                                break;
                            case 6:
                                saveGameExist = this.AccountProfile.SteamPC_SaveGame7.Exists;
                                backupExist = this.AccountProfile.SteamPC_SaveGame7.BackupExists;
                                showEnableBackup = !this.AccountProfile.SteamPC_SaveGame7.BackupEnabled;
                                break;
                            case 7:
                                saveGameExist = this.AccountProfile.SteamPC_SaveGame8.Exists;
                                backupExist = this.AccountProfile.SteamPC_SaveGame8.BackupExists;
                                showEnableBackup = !this.AccountProfile.SteamPC_SaveGame8.BackupEnabled;
                                break;
                        }
                    }

                    this.MI_SteamPCBackups_CreateBackup.Enabled = saveGameExist;
                    this.MI_SteamPCBackups_Backups.Enabled = backupExist;

                    if (showEnableBackup) {
                        this.MI_SteamPCBackups_EnableBackup.Text = "Enable: Auto-Backup";
                    } else {
                        this.MI_SteamPCBackups_EnableBackup.Text = "Disable: Auto-Backup";
                    }

                    this.CM_SteamPCBackups.Show(loc);
                }
            }
        }


        private void MI_UplayPCBackups_Backups_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.LV_UplayPCBackups.SelectedIndices.Count == 1) {
                switch (this.LV_UplayPCBackups.SelectedIndices[0]) {
                    case 0:
                        this.showBackupForm(this.AccountProfile.UplayPC_SaveGame1);
                        break;
                    case 1:
                        this.showBackupForm(this.AccountProfile.UplayPC_SaveGame2);
                        break;
                    case 2:
                        this.showBackupForm(this.AccountProfile.UplayPC_SaveGame3);
                        break;
                    case 3:
                        this.showBackupForm(this.AccountProfile.UplayPC_SaveGame4);
                        break;
                    case 4:
                        this.showBackupForm(this.AccountProfile.UplayPC_SaveGame5);
                        break;
                    case 5:
                        this.showBackupForm(this.AccountProfile.UplayPC_SaveGame6);
                        break;
                    case 6:
                        this.showBackupForm(this.AccountProfile.UplayPC_SaveGame7);
                        break;
                    case 7:
                        this.showBackupForm(this.AccountProfile.UplayPC_SaveGame8);
                        break;
                }
            }
        }


        private void MI_SteamPCBackups_Backups_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.LV_SteamPCBackups.SelectedIndices.Count == 1) {
                switch (this.LV_SteamPCBackups.SelectedIndices[0]) {
                    case 0:
                        this.showBackupForm(this.AccountProfile.SteamPC_SaveGame1);
                        break;
                    case 1:
                        this.showBackupForm(this.AccountProfile.SteamPC_SaveGame2);
                        break;
                    case 2:
                        this.showBackupForm(this.AccountProfile.SteamPC_SaveGame3);
                        break;
                    case 3:
                        this.showBackupForm(this.AccountProfile.SteamPC_SaveGame4);
                        break;
                    case 4:
                        this.showBackupForm(this.AccountProfile.SteamPC_SaveGame5);
                        break;
                    case 5:
                        this.showBackupForm(this.AccountProfile.SteamPC_SaveGame6);
                        break;
                    case 6:
                        this.showBackupForm(this.AccountProfile.SteamPC_SaveGame7);
                        break;
                    case 7:
                        this.showBackupForm(this.AccountProfile.SteamPC_SaveGame8);
                        break;
                }
            }
        }

        
        private void showBackupForm(GRWLifesaver.SaveGame saveGame)
        {
            if (!(saveGame is null)) {
                this.BF_BackupForm.SaveGame = saveGame;
                this.BF_BackupForm.ShowDialog();
            }
        }


        private void LB_BackupAll_Click(object sender, System.EventArgs e)
        {
            System.Boolean anyBackupEnabled = false;

            if (this.AccountProfile.UplayPC_SaveGame1.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.UplayPC_SaveGame2.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.UplayPC_SaveGame3.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.UplayPC_SaveGame4.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.UplayPC_SaveGame5.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.UplayPC_SaveGame6.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.UplayPC_SaveGame7.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.UplayPC_SaveGame8.BackupEnabled) {
                anyBackupEnabled = true;
            }

            if (this.AccountProfile.SteamPC_SaveGame1.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.SteamPC_SaveGame2.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.SteamPC_SaveGame3.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.SteamPC_SaveGame4.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.SteamPC_SaveGame5.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.SteamPC_SaveGame6.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.SteamPC_SaveGame7.BackupEnabled) {
                anyBackupEnabled = true;
            }
            if (this.AccountProfile.SteamPC_SaveGame8.BackupEnabled) {
                anyBackupEnabled = true;
            }

            if (anyBackupEnabled) {
                // Disable Backup All

                this.AccountProfile.UplayPC_SaveGame1.BackupEnabled = false;
                this.AccountProfile.UplayPC_SaveGame2.BackupEnabled = false;
                this.AccountProfile.UplayPC_SaveGame3.BackupEnabled = false;
                this.AccountProfile.UplayPC_SaveGame4.BackupEnabled = false;
                this.AccountProfile.UplayPC_SaveGame5.BackupEnabled = false;
                this.AccountProfile.UplayPC_SaveGame6.BackupEnabled = false;
                this.AccountProfile.UplayPC_SaveGame7.BackupEnabled = false;
                this.AccountProfile.UplayPC_SaveGame8.BackupEnabled = false;

                this.AccountProfile.SteamPC_SaveGame1.BackupEnabled = false;
                this.AccountProfile.SteamPC_SaveGame2.BackupEnabled = false;
                this.AccountProfile.SteamPC_SaveGame3.BackupEnabled = false;
                this.AccountProfile.SteamPC_SaveGame4.BackupEnabled = false;
                this.AccountProfile.SteamPC_SaveGame5.BackupEnabled = false;
                this.AccountProfile.SteamPC_SaveGame6.BackupEnabled = false;
                this.AccountProfile.SteamPC_SaveGame7.BackupEnabled = false;
                this.AccountProfile.SteamPC_SaveGame8.BackupEnabled = false;

                this.LB_BackupAll.Text = "Enable: Auto-Backup All";
            } else {
                // Enable Backup All

                this.AccountProfile.UplayPC_SaveGame1.BackupEnabled = true;
                this.AccountProfile.UplayPC_SaveGame2.BackupEnabled = true;
                this.AccountProfile.UplayPC_SaveGame3.BackupEnabled = true;
                this.AccountProfile.UplayPC_SaveGame4.BackupEnabled = true;
                this.AccountProfile.UplayPC_SaveGame5.BackupEnabled = true;
                this.AccountProfile.UplayPC_SaveGame6.BackupEnabled = true;
                this.AccountProfile.UplayPC_SaveGame7.BackupEnabled = true;
                this.AccountProfile.UplayPC_SaveGame8.BackupEnabled = true;

                this.AccountProfile.SteamPC_SaveGame1.BackupEnabled = true;
                this.AccountProfile.SteamPC_SaveGame2.BackupEnabled = true;
                this.AccountProfile.SteamPC_SaveGame3.BackupEnabled = true;
                this.AccountProfile.SteamPC_SaveGame4.BackupEnabled = true;
                this.AccountProfile.SteamPC_SaveGame5.BackupEnabled = true;
                this.AccountProfile.SteamPC_SaveGame6.BackupEnabled = true;
                this.AccountProfile.SteamPC_SaveGame7.BackupEnabled = true;
                this.AccountProfile.SteamPC_SaveGame8.BackupEnabled = true;

                this.LB_BackupAll.Text = "Disable: Auto-Backup All";
            }

            this._updateStats();
        }


        private void LB_BackupAll_MouseEnter(object sender, System.EventArgs e)
        {
            this.LB_BackupAll.Font = new System.Drawing.Font(this.LB_BackupAll.Font, System.Drawing.FontStyle.Underline);
        }


        private void LB_BackupAll_MouseLeave(object sender, System.EventArgs e)
        {
            this.LB_BackupAll.Font = new System.Drawing.Font(this.LB_BackupAll.Font, System.Drawing.FontStyle.Regular);
        }


        private void CB_AccountUUIDs_SelectedValueChanged(object sender, System.EventArgs e)
        {
            this.SetDisplaySaveGames(this.Program.GRWLifesaver.AccountProfiles[this.CB_AccountUUIDs.Text]);
            if (!this.AccountProfile.UplayPC_SaveGameExist) {
                this.TC_Backups.SelectTab(this.TP_SteamPCBackups);
            }
            this._updateStats();
        }


        private void Tmr_Updater_Tick(object sender, System.EventArgs e)
        {
            this._updateStats();
            this._updateSystemTime();
        }



        private void _updateSystemTime()
        {
            this.LB_SystemTime.Text = "System Time: " + System.DateTime.Now.ToString("HH:mm:ss");
        }


        private void _updateStats()
        {
            this._updateSavegameChart(this.LVI_UplayPCBackups_Slot1, this.AccountProfile.UplayPC_SaveGame1);
            this._updateSavegameChart(this.LVI_UplayPCBackups_Slot2, this.AccountProfile.UplayPC_SaveGame2);
            this._updateSavegameChart(this.LVI_UplayPCBackups_Slot3, this.AccountProfile.UplayPC_SaveGame3);
            this._updateSavegameChart(this.LVI_UplayPCBackups_Slot4, this.AccountProfile.UplayPC_SaveGame4);
            this._updateSavegameChart(this.LVI_UplayPCBackups_Slot5, this.AccountProfile.UplayPC_SaveGame5);
            this._updateSavegameChart(this.LVI_UplayPCBackups_Slot6, this.AccountProfile.UplayPC_SaveGame6);
            this._updateSavegameChart(this.LVI_UplayPCBackups_Slot7, this.AccountProfile.UplayPC_SaveGame7);
            this._updateSavegameChart(this.LVI_UplayPCBackups_Slot8, this.AccountProfile.UplayPC_SaveGame8);

            this._updateSavegameChart(this.LVI_SteamPCBackups_Slot1, this.AccountProfile.SteamPC_SaveGame1);
            this._updateSavegameChart(this.LVI_SteamPCBackups_Slot2, this.AccountProfile.SteamPC_SaveGame2);
            this._updateSavegameChart(this.LVI_SteamPCBackups_Slot3, this.AccountProfile.SteamPC_SaveGame3);
            this._updateSavegameChart(this.LVI_SteamPCBackups_Slot4, this.AccountProfile.SteamPC_SaveGame4);
            this._updateSavegameChart(this.LVI_SteamPCBackups_Slot5, this.AccountProfile.SteamPC_SaveGame5);
            this._updateSavegameChart(this.LVI_SteamPCBackups_Slot6, this.AccountProfile.SteamPC_SaveGame6);
            this._updateSavegameChart(this.LVI_SteamPCBackups_Slot7, this.AccountProfile.SteamPC_SaveGame7);
            this._updateSavegameChart(this.LVI_SteamPCBackups_Slot8, this.AccountProfile.SteamPC_SaveGame8);
        }


        private void _updateSavegameChart(System.Windows.Forms.ListViewItem listViewItem, GRWLifesaver.SaveGame saveGame)
        {
            System.String itemText;

            if (saveGame.Exists) {
                if (saveGame.LastChanged == System.DateTime.MinValue) {
                    itemText = "";
                    saveGame.ClearCache();
                } else {
                    itemText = saveGame.LastChanged.ToString("dd.MM.yyyy  HH:mm:ss") + "  /" + MainForm._dateToShort(saveGame.LastChanged);
                }
            } else {
                itemText = "missing savegame";
            }
            if (listViewItem.SubItems[1].Text != itemText) {
                listViewItem.SubItems[1].Text = itemText;
            }
            
            if (saveGame.BackupExists) {
                if (saveGame.LastBackup == System.DateTime.MinValue) {
                    itemText = "";
                    saveGame.ClearCache();
                } else {
                    itemText = saveGame.LastBackup.ToString("dd.MM.yyyy  HH:mm:ss") + "  /" + MainForm._dateToShort(saveGame.LastBackup);
                }
            } else {
                itemText = "no backup yet";
            }
            if (listViewItem.SubItems[2].Text != itemText) {
                listViewItem.SubItems[2].Text = itemText;
            }
            if (saveGame.BackupEnabled) {
                itemText = "yes";
            } else {
                itemText = "";
            }
            if (listViewItem.SubItems[3].Text != itemText) {
                listViewItem.SubItems[3].Text = itemText;
            }
        }


        internal static System.String _dateToShort(System.DateTime date)
        {
            System.TimeSpan span = (System.DateTime.Now - date);

            System.Int64 totalDays = (System.Int64)span.TotalDays;
            System.Int64 totalHours = (System.Int64)span.TotalHours;
            System.Int64 totalMinutes = (System.Int64)span.TotalMinutes;

            if (totalDays == 1) {
                return totalDays + " day";
            } else if (totalDays > 0) {
                return totalDays + " days";
            } else if (totalHours == 1) {
                return totalHours + " hour";
            } else if (totalHours > 0) {
                return totalHours + " hours";
            } else if (totalMinutes == 1) {
                return totalMinutes + " minute";
            } else if (totalMinutes > 0) {
                return totalMinutes + " minutes";
            } else if (span.Ticks >= 0) {
                return "now";
            } else {
                return "invalid";
            }
        }


        private void DisplayMajorMessage(System.String message) => this.DisplayMajorMessage(message, false);
        private void DisplayMajorMessage(System.String message, System.Boolean bold) => this.DisplayMajorMessage(message, bold, System.Drawing.Color.Black);
        private void DisplayMajorMessage(System.String message, System.Boolean bold, System.Drawing.Color color)
        {
            this.SetDisplayEnabled(false);

            this.SuspendLayout();

            if (this.LB_MajorMessage is null || this.LB_MajorMessage.IsDisposed) {
                this.LB_MajorMessage = new System.Windows.Forms.Label();
                this.LB_MajorMessage.Name = "LB_MajorMessage";
                this.LB_MajorMessage.Parent = this;
            }

            this.LB_MajorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB_MajorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            if (bold) {
                this.LB_MajorMessage.Font = new System.Drawing.Font(this.LB_MajorMessage.Font, System.Drawing.FontStyle.Bold);
            }

            this.LB_MajorMessage.Text = message;
            this.LB_MajorMessage.ForeColor = color;

            this.ResumeLayout(true);
        }


        private void HideMajorMessage()
        {
            if (!(this.LB_MajorMessage is null)) {
                this.LB_MajorMessage.Parent = null;
                this.LB_MajorMessage.Dispose();
                this.LB_MajorMessage = null;
            }
        }


        private void TB_BackupFolder_Click(object sender, System.EventArgs e)
        {
            if (this.FBD_BackupFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                this.Program.GRWLifesaver.BackupFolder = this.FBD_BackupFolder.SelectedPath;
                this.TB_BackupFolder.Text = this.Program.GRWLifesaver.BackupFolder;
                this._updateStats();
            }
        }


        private void FormLayout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            if (e.AffectedControl == this && (e.AffectedProperty == "Visible" || e.AffectedProperty == "Bounds")) {
                this.SuspendLayout();

                // BackupFolder
                this.LB_BackupFolder.Location = new System.Drawing.Point(12, 6);

                this.TB_BackupFolder.Location = new System.Drawing.Point(12, this.LB_BackupFolder.Location.Y + this.LB_BackupFolder.Size.Height + 4);
                this.TB_BackupFolder.Size = new System.Drawing.Size(this.ClientSize.Width - 24, this.ClientSize.Height - 24);

                // AccountUUIDs
                this.LB_AccountUUIDs.Location = new System.Drawing.Point(12, this.TB_BackupFolder.Location.Y + this.TB_BackupFolder.Size.Height + 4);

                this.CB_AccountUUIDs.Location = new System.Drawing.Point(12, this.LB_AccountUUIDs.Location.Y + this.LB_AccountUUIDs.Size.Height + 4);
                this.CB_AccountUUIDs.Size = new System.Drawing.Size(this.ClientSize.Width - 24, 0);

                if (!(this.TC_Backups is null)) {
                    // TC_Backups
                    this.TC_Backups.Location = new System.Drawing.Point(12, this.CB_AccountUUIDs.Location.Y + this.CB_AccountUUIDs.Size.Height + 4);
                    this.TC_Backups.Size = new System.Drawing.Size(this.ClientSize.Width - 24, this.ClientSize.Height - this.TC_Backups.Location.Y - 12);
                }

                this.ResumeLayout(true);
            }

            if ((e.AffectedControl == this || e.AffectedControl == this.LB_BackupAll || e.AffectedControl == this.TC_Backups) && (e.AffectedProperty == "Visible" || e.AffectedProperty == "Bounds")) {
                if (!(this.LB_BackupAll is null)) {
                    this.LB_BackupAll.Location = new System.Drawing.Point(this.TC_Backups.Location.X + this.TC_Backups.Size.Width - this.LB_BackupAll.Size.Width - 12, this.TC_Backups.Location.Y + 4);

                    if (!(this.LB_SystemTime is null)) {
                        this.LB_SystemTime.Location = new System.Drawing.Point(this.LB_BackupAll.Location.X - 50 - this.LB_SystemTime.Size.Width, this.LB_BackupAll.Location.Y);
                    }
                }
            }
        }


        public Program Program
        {
            get;
            private set;
        }

    }

}