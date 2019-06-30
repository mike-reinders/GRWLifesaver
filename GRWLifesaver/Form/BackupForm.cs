namespace GRWLifesaver.Form
{

    public class BackupForm : System.Windows.Forms.Form
    {

        private System.Windows.Forms.Label LB_NoSaveGameLoaded;
        private System.Windows.Forms.ListView LV_BackupsList;

        private System.Windows.Forms.Button BN_Delete;
        private System.Windows.Forms.Button BN_Cancel;
        private System.Windows.Forms.Button BN_Restore;

        private Form.DeletionPromptForm DF_DeletionPromptForm;

        public BackupForm()
        {
            this.InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Icon = Properties.Resources.GRW_101;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            
            // LB_NoSaveGameLoaded
            this.LB_NoSaveGameLoaded = new System.Windows.Forms.Label();
            this.LB_NoSaveGameLoaded.Name = "LB_NoSaveGameLoaded";
            this.LB_NoSaveGameLoaded.Parent = this;
            
            this.LB_NoSaveGameLoaded.Visible = false;
            this.LB_NoSaveGameLoaded.Font = new System.Drawing.Font(this.LB_NoSaveGameLoaded.Font, System.Drawing.FontStyle.Bold);
            this.LB_NoSaveGameLoaded.ForeColor = System.Drawing.Color.Red;
            this.LB_NoSaveGameLoaded.Text = "No Savegame were attached to this Dialog";
            this.LB_NoSaveGameLoaded.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // LV_BackupsList
            this.LV_BackupsList = new System.Windows.Forms.ListView();
            this.LV_BackupsList.Name = "LV_BackupsList";
            this.LV_BackupsList.Parent = this;

            this.LV_BackupsList.Visible = false;
            this.LV_BackupsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_BackupsList.View = System.Windows.Forms.View.Details;
            this.LV_BackupsList.FullRowSelect = true;
            this.LV_BackupsList.MultiSelect = true;

            this.LV_BackupsList.Columns.Add("Savegame / Time", 255);

            this.LV_BackupsList.ColumnWidthChanging += this.LV_BackupsList_ColumnWidthChanging;
            this.LV_BackupsList.SelectedIndexChanged += this.LV_BackupsList_SelectedIndexChanged;
            this.LV_BackupsList.MouseDoubleClick += this.LV_BackupsList_MouseDoubleClick;

            // BN_Delete
            this.BN_Delete = new System.Windows.Forms.Button();
            this.BN_Delete.Name = "BN_Delete";
            this.BN_Delete.Parent = this;

            this.BN_Delete.Text = "Delete";
            this.BN_Delete.Enabled = false;
            this.BN_Delete.Visible = false;

            this.BN_Delete.Click += this.BN_Delete_Click;

            // BN_Cancel
            this.BN_Cancel = new System.Windows.Forms.Button();
            this.BN_Cancel.Name = "BN_Cancel";
            this.BN_Cancel.Parent = this;

            this.BN_Cancel.Text = "Cancel";

            this.BN_Cancel.Click += this.BN_Cancel_Click;

            // BN_Restore
            this.BN_Restore = new System.Windows.Forms.Button();
            this.BN_Restore.Name = "BN_Restore";
            this.BN_Restore.Parent = this;

            this.BN_Restore.Text = "Restore";
            this.BN_Restore.Enabled = false;
            this.BN_Restore.Visible = false;

            this.BN_Restore.Click += this.BN_Restore_Click;

            this.DF_DeletionPromptForm = new DeletionPromptForm();

            // this events
            this.FormClosed += this.BackupForm_FormClosed;
            this.Load += this.BackupForm_Load;
            this.Layout += this.BackupForm_Layout;

            this.ResumeLayout(true);
        }


        private void BN_Delete_Click(object sender, System.EventArgs e)
        {
            if (this.LV_BackupsList.SelectedIndices.Count > 0) {
                System.String[] backups = new System.String[this.LV_BackupsList.SelectedIndices.Count];

                for (System.Int32 i = 0; i < backups.Length; i++) {
                    backups[i] = this.SaveGame.GetBackupFromDate(this._getDate(this.LV_BackupsList.SelectedItems[i].Text));
                }

                if (this.DF_DeletionPromptForm.ShowDeleteDialog(this.SaveGame, backups) == System.Windows.Forms.DialogResult.Yes) {
                    this.RefreshBackupsList();
                }
            }
        }


        private void LV_BackupsList_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.LV_BackupsList.SelectedIndices.Count == 1) {
                this.RestoreSaveGame(this._getDate(this.LV_BackupsList.Items[this.LV_BackupsList.SelectedIndices[0]].Text));
            }
        }


        private void LV_BackupsList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this._updateButtonsEnabled();
        }


        private void _updateButtonsEnabled()
        {
            this.BN_Restore.Enabled = this.LV_BackupsList.SelectedIndices.Count == 1;
            this.BN_Delete.Enabled = this.LV_BackupsList.SelectedIndices.Count > 0;
        }


        private void BN_Restore_Click(object sender, System.EventArgs e)
        {
            if (this.LV_BackupsList.SelectedIndices.Count == 1) {
                this.RestoreSaveGame(this._getDate(this.LV_BackupsList.Items[this.LV_BackupsList.SelectedIndices[0]].Text));
            }
        }


        private void BN_Cancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


        private void RefreshBackupsList()
        {
            this.LV_BackupsList.Items.Clear();

            System.DateTime date;
            foreach (System.String backup in this.SaveGame.Backups) {
                date = this.SaveGame.GetBackupTime(backup);
                this.LV_BackupsList.Items.Add(date.ToString("dd.MM.yyyy HH:mm:ss") + "  /" + MainForm._dateToShort(date));
            }

            this._updateButtonsEnabled();
        }


        private System.DateTime _getDate(System.String str)
        {
            System.DateTime date = System.DateTime.MinValue;

            try {
                date = System.DateTime.ParseExact(str.Split('/')[0].Trim(), "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            } catch (System.Exception) {
                // Suppress Exceptions
            }

            return date;
        }


        private void RestoreSaveGame(System.DateTime date)
        {
            System.String backup = this.SaveGame.GetBackupFromDate(date);

            if (date == System.DateTime.MinValue || backup is null) {
                System.Windows.Forms.MessageBox.Show("Failed to restore Backup by given Time." + System.Environment.NewLine + "This might happen, when you (or something else) deleted a backup after opening the restore dialog." + System.Environment.NewLine + System.Environment.NewLine + "We'll refresh the backup list for you. Maybe this helps.", "Error Restoring Backup", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                this.SaveGame.ClearCache();
                this.RefreshBackupsList();
            } else {
                try {
                    this.SaveGame.RestoreBackup(backup);
                    this.Close();
                } catch (System.Exception) {
                    System.Windows.Forms.MessageBox.Show("Failed to restore Backup." + System.Environment.NewLine + "Maybe try to relaunch your GRWLifesaver.", "Error Restoring Backup", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }
        }


        private void LV_BackupsList_ColumnWidthChanging(object sender, System.Windows.Forms.ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = this.LV_BackupsList.Columns[e.ColumnIndex].Width;
        }


        private void BackupForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            this.SaveGame = null;
            this.Text = "";
            this.LV_BackupsList.Items.Clear();
        }


        private void BackupForm_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            if ((e.AffectedControl == this || e.AffectedControl == this.BN_Restore) && (e.AffectedProperty == "Visible" || e.AffectedProperty == "Bounds")) {
                this.BN_Cancel.Size = new System.Drawing.Size(70, 23);
                
                if (this.BN_Restore.Visible) {
                    this.BN_Restore.Size = new System.Drawing.Size(70, 23);
                    this.BN_Restore.Location = new System.Drawing.Point(this.ClientSize.Width - 12 - this.BN_Restore.Size.Width, this.ClientSize.Height - 12 - this.BN_Restore.Size.Height);
                    
                    this.BN_Cancel.Location = new System.Drawing.Point(this.BN_Restore.Location.X - 12 - this.BN_Cancel.Size.Width, this.ClientSize.Height - 12 - this.BN_Cancel.Size.Height);
                } else {
                    this.BN_Cancel.Location = new System.Drawing.Point(this.ClientSize.Width - 12 - this.BN_Cancel.Size.Width, this.ClientSize.Height - 12 - this.BN_Cancel.Size.Height);
                }

                if (this.BN_Delete.Visible) {
                    this.BN_Delete.Size = new System.Drawing.Size(70, 23);
                    this.BN_Delete.Location = new System.Drawing.Point(12, this.ClientSize.Height - 12 - this.BN_Delete.Size.Height);
                }

                if (this.LB_NoSaveGameLoaded.Visible) {
                    this.LB_NoSaveGameLoaded.Location = new System.Drawing.Point(12, 12);
                    this.LB_NoSaveGameLoaded.Size = new System.Drawing.Size(this.ClientSize.Width - 24, this.BN_Cancel.Location.Y - 12 - 6);
                }

                if (this.LV_BackupsList.Visible) {
                    this.LV_BackupsList.Location = new System.Drawing.Point(12, 12);
                    this.LV_BackupsList.Size = new System.Drawing.Size(this.ClientSize.Width - 24, this.BN_Cancel.Location.Y - 12 - 6);
                }
            }
        }


        private void BackupForm_Load(object sender, System.EventArgs e)
        {
            this.Text = "Backups";
            if (this.SaveGame is null) {
                this.LB_NoSaveGameLoaded.Visible = true;
                this.LV_BackupsList.Visible = false;
                this.BN_Delete.Visible = false;
                this.BN_Restore.Visible = false;

                this.Size = new System.Drawing.Size(300, 150);
            } else {
                this.LB_NoSaveGameLoaded.Visible = false;
                this.LV_BackupsList.Visible = true;
                this.BN_Delete.Visible = true;
                this.BN_Restore.Visible = true;

                this.Size = new System.Drawing.Size(300, 500);

                this.Text += " - " + this.SaveGame.GameName + " SaveGame Slot " + this.SaveGame.ID;

                this.RefreshBackupsList();
            }
        }


        public GRWLifesaver.SaveGame SaveGame
        {
            get;
            set;
        }



    }

}