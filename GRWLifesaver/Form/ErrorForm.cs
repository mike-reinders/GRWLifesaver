namespace GRWLifesaver.Form
{

    public class ErrorForm : System.Windows.Forms.Form
    {

        public static System.Drawing.Size MIN_SIZE = new System.Drawing.Size(400, 150);


        private System.Windows.Forms.Label LB_ErrorMessage;
        private System.Windows.Forms.Button BN_OK;
        private System.Windows.Forms.Button BN_ShowDetails;

        private System.Windows.Forms.TextBox TB_Details;

        public ErrorForm(System.String message, System.Boolean exit)
        {
            if (message is null) throw new System.ArgumentNullException("message");

            this.Message = message;
            this.Exception = null;
            this.ExitOnFormClose = exit;

            this.InitializeComponent();
        }


        public ErrorForm(System.Exception ex, System.Boolean exit)
        {
            if (ex is null) throw new System.ArgumentNullException("ex");

            this.Message = null;
            this.Exception = ex;
            this.ExitOnFormClose = exit;

            this.InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Icon = System.Drawing.SystemIcons.Exclamation;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.Text = "An error occurred";

            this.LB_ErrorMessage = new System.Windows.Forms.Label();
            this.LB_ErrorMessage.Name = "LB_ErrorMessage";
            this.LB_ErrorMessage.Parent = this;

            this.LB_ErrorMessage.AutoSize = true;
            this.LB_ErrorMessage.Font = new System.Drawing.Font(this.LB_ErrorMessage.Font.FontFamily, this.LB_ErrorMessage.Font.Size, System.Drawing.FontStyle.Regular);

            if (this.Message is null) {
                this.LB_ErrorMessage.Text = "An unhandled error occurred." + System.Environment.NewLine + "You can find details about it underneath below.";
            } else {
                this.LB_ErrorMessage.Text = this.Message;
            }

            this.BN_OK = new System.Windows.Forms.Button();
            this.BN_OK.Name = "BN_OK";
            this.BN_OK.Parent = this;

            this.BN_OK.Text = "Ok";
            this.BN_OK.Click += this.BN_OK_Click;

            this.BN_ShowDetails = new System.Windows.Forms.Button();
            this.BN_ShowDetails.Name = "BN_ShowDetails";
            this.BN_ShowDetails.Parent = this;

            this.BN_ShowDetails.Text = "Details";

            this.BN_ShowDetails.Click += this.BN_ShowDetails_Click;

            if (this.Exception is null) {
                this.BN_ShowDetails.Visible = false;
            }

            this.TB_Details = new System.Windows.Forms.TextBox();
            this.TB_Details.Name = "TB_Details";
            this.TB_Details.Parent = this;

            this.TB_Details.Multiline = true;
            this.TB_Details.ReadOnly = true;
            this.TB_Details.BackColor = System.Drawing.Color.White;
            this.TB_Details.WordWrap = false;
            this.TB_Details.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_Details.Visible = false;

            if (!(this.Exception is null)) {
                System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
                System.Exception ex = this.Exception;

                while (!(ex is null)) {
                    strBuilder.Append(ex.GetType().FullName);
                    strBuilder.Append("[Message: \"");
                    strBuilder.Append(ex.Message.Replace("\"", "\\\""));
                    strBuilder.Append("\"");
                    strBuilder.Append("]");
                    strBuilder.Append(System.Environment.NewLine);
                    strBuilder.Append(System.Environment.NewLine);
                    strBuilder.Append(ex.StackTrace);
                    strBuilder.Append(System.Environment.NewLine);
                    strBuilder.Append(System.Environment.NewLine);

                    ex = ex.InnerException;
                }

                this.TB_Details.Text = strBuilder.ToString();
            }

            this.Layout += this.ErrorForm_Layout;
            this.Resize += this.ErrorForm_Resize;
            this.FormClosed += this.ErrorForm_FormClosed;

            this.ResumeLayout(true);
        }


        private void ErrorForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (this.ExitOnFormClose) {
                System.Windows.Forms.Application.Exit();
            }
        }


        public System.Boolean ExitOnFormClose
        {
            get;
            set;
        }


        public System.Exception Exception
        {
            get;
            set;
        }


        public System.String Message
        {
            get;
            set;
        }


        private void BN_OK_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


        private void BN_ShowDetails_Click(object sender, System.EventArgs e)
        {
            if (!(this.Exception is null)) {
                this.TB_Details.Visible = !this.TB_Details.Visible;
            }
        }


        private void ErrorForm_Resize(object sender, System.EventArgs e)
        {
            if (this.WindowState != System.Windows.Forms.FormWindowState.Normal) {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
        }


        private void ErrorForm_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            this.SuspendLayout();

            if ((e.AffectedControl == this || e.AffectedControl == this.LB_ErrorMessage || e.AffectedControl == this.TB_Details) && (e.AffectedProperty == "Visible" || e.AffectedProperty == "Bounds")) {
                this.LB_ErrorMessage.Location = new System.Drawing.Point(12, 12);
                this.BN_ShowDetails.Size = new System.Drawing.Size(75, 23);
                this.BN_OK.Size = new System.Drawing.Size(75, 23);

                if (this.TB_Details.Visible) {
                    this.TB_Details.Location = new System.Drawing.Point(12, this.LB_ErrorMessage.Location.Y + this.LB_ErrorMessage.Size.Height + 12);
                    this.TB_Details.Size = new System.Drawing.Size(500, 200);

                    this.ClientSize = new System.Drawing.Size(this.LB_ErrorMessage.Location.X + this.TB_Details.Size.Width + 12, this.TB_Details.Location.Y + this.TB_Details.Size.Height + 4 + this.BN_ShowDetails.Size.Height + 12);
                } else {
                    this.ClientSize = new System.Drawing.Size(this.LB_ErrorMessage.Location.X + this.LB_ErrorMessage.Size.Width + 12, this.LB_ErrorMessage.Location.Y + this.LB_ErrorMessage.Size.Height + 12);
                }

                // Apply MIN_SIZE
                if (this.Size.Width < ErrorForm.MIN_SIZE.Width) {
                    this.Size = new System.Drawing.Size(ErrorForm.MIN_SIZE.Width, this.Size.Height);
                }
                if (this.Size.Height < ErrorForm.MIN_SIZE.Height) {
                    this.Size = new System.Drawing.Size(this.Size.Width, ErrorForm.MIN_SIZE.Height);
                }
                
                this.BN_ShowDetails.Location = new System.Drawing.Point(this.ClientSize.Width - 12 - this.BN_ShowDetails.Size.Width, this.ClientSize.Height - 12 - this.BN_ShowDetails.Size.Height);
                this.BN_OK.Location = new System.Drawing.Point(this.BN_ShowDetails.Location.X - 12 - this.BN_OK.Size.Width, this.ClientSize.Height - 12 - this.BN_OK.Size.Height);
            }

            this.ResumeLayout(true);
        }


        public static void Show(System.String message) => ErrorForm.Show(message, false);
        public static void Show(System.String message, System.Boolean exit)
        {
            ErrorForm errorForm = new ErrorForm(message, exit);

            if (!System.Windows.Forms.Application.MessageLoop) {
                System.Windows.Forms.Application.Run(errorForm);
            } else {
                errorForm.Show();
            }
        }


        public static void Show(System.Exception ex) => ErrorForm.Show(ex, false);
        public static void Show(System.Exception ex, System.Boolean exit)
        {
            ErrorForm errorForm = new ErrorForm(ex, exit);
            
            if (!System.Windows.Forms.Application.MessageLoop) {
                System.Windows.Forms.Application.Run(errorForm);
            } else {
                errorForm.Show();
            }
        }

    }

}