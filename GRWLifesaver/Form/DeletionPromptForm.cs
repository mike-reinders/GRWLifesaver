namespace GRWLifesaver.Form
{

    public class DeletionPromptForm : System.Windows.Forms.Form
    {

        private System.Windows.Forms.Label LB_Message;
        private System.Windows.Forms.Button BN_Yes;
        private System.Windows.Forms.Button BN_No;

        public DeletionPromptForm()
        {
            this.InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Size = new System.Drawing.Size(280, 110);
            this.Text = "Delete Backup?";


            this.LB_Message = new System.Windows.Forms.Label();
            this.LB_Message.Name = "LB_Message";
            this.LB_Message.Parent = this;

            this.LB_Message.AutoSize = true;

            this.BN_Yes = new System.Windows.Forms.Button();
            this.BN_Yes.Name = "BN_Yes";
            this.BN_Yes.Parent = this;

            this.BN_Yes.Text = "Yes";
            this.BN_Yes.Click += this.BN_Yes_Click;
            
            this.BN_No = new System.Windows.Forms.Button();
            this.BN_No.Name = "BN_No";
            this.BN_No.Parent = this;

            this.BN_No.Text = "No";
            this.BN_No.Click += this.BN_No_Click;

            // this events
            this.Layout += this.DeletionPromptForm_Layout;
            this.FormClosed += this.DeletionPromptForm_FormClosed;

            this.ResumeLayout(true);
        }


        private void DeletionPromptForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            this.SaveGame = null;
            this.Backups = null;
        }


        public GRWLifesaver.SaveGame SaveGame
        {
            get;
            set;
        }


        public System.String[] Backups
        {
            get;
            set;
        }


        public System.Windows.Forms.DialogResult ShowDeleteDialog(GRWLifesaver.SaveGame saveGame, System.String[] backups)
        {
            if (saveGame is null) {
                throw new System.ArgumentNullException("saveGame");
            }
            if (backups is null) {
                throw new System.ArgumentNullException("backups");
            }
            if (backups.Length <= 0) {
                throw new System.ArgumentException("backups must contain anything");
            } else if (backups.Length == 1) {
                this.LB_Message.Text = "Are you sure, you want to delete this backup?";
            } else {
                this.LB_Message.Text = "Are you sure, you want to delete these backups?";
            }

            this.SaveGame = saveGame;
            this.Backups = backups;
            return this.ShowDialog();
        }


        private void BN_No_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }


        private void BN_Yes_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;

            foreach (System.String backup in this.Backups) {
                this.SaveGame.DeleteBackup(backup);
            }
        }


        private void DeletionPromptForm_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            if (e.AffectedControl == this && (e.AffectedProperty == "Visible" || e.AffectedProperty == "Bounds")) {
                this.LB_Message.Location = new System.Drawing.Point(12, 12);

                this.BN_No.Size = new System.Drawing.Size(75, 23);
                this.BN_No.Location = new System.Drawing.Point(this.ClientSize.Width - 12 - this.BN_No.Size.Width, this.ClientSize.Height - 12 - this.BN_No.Size.Height);
                this.BN_Yes.Size = new System.Drawing.Size(75, 23);
                this.BN_Yes.Location = new System.Drawing.Point(this.BN_No.Location.X - 12 - this.BN_Yes.Size.Width, this.ClientSize.Height - 12 - this.BN_Yes.Size.Height);
            }
        }

    }

}