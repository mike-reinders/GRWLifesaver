namespace GRWLifesaver
{
    public class Program
    {

        public const System.String Name = "GRW Lifesaver";

        private System.DateTime lastThreadException;
        private System.Int32 threadExceptionsCount;
        private System.Boolean tooMuchErrors;

        public void Run()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.SetUnhandledExceptionMode(System.Windows.Forms.UnhandledExceptionMode.CatchException);
            
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, "GRWLifesaverSingleInstanceMutex");
            try {
                if (mutex.WaitOne(0, false)) {
                    System.Windows.Forms.Application.ThreadException += this.Application_ThreadException;

                    this.GRWLifesaver = new GRWLifesaver.GRWLifesaver(this);
                    this.MainForm = new Form.MainForm(this);
                    System.Windows.Forms.Application.Run(this.MainForm);
                } else {
                    Form.ErrorForm.Show("GRWLifesaver is already running!", true);
                }
            } finally {
                if (mutex != null) {
                    mutex.Close();
                    mutex = null;
                }
            }
        }


        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if ((System.DateTime.Now - this.lastThreadException).TotalMilliseconds < 3000) {
                threadExceptionsCount++;
            } else {
                threadExceptionsCount = 0;
            }

            this.lastThreadException = System.DateTime.Now;
            if (threadExceptionsCount >= 5) {
                if (!tooMuchErrors) {
                    tooMuchErrors = true;
                    Form.ErrorForm.Show("To much errors have been shown recently." + System.Environment.NewLine + System.Environment.NewLine + "Application is closing..", true);
                }
            } else {
                Form.ErrorForm.Show(e.Exception);
            }
        }


        public Form.MainForm MainForm
        {
            get;
            private set;
        }


        public GRWLifesaver.GRWLifesaver GRWLifesaver
        {
            get;
            private set;
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [System.STAThread]
        static void Main()
        {
            new Program().Run();
        }
        
    }
}
