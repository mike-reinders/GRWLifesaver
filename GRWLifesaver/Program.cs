namespace GRWLifesaver
{
    public class Program
    {

        public const System.String Name = "GRW Lifesaver";

        public void Run()
        {
            this.GRWLifesaver = new GRWLifesaver.GRWLifesaver(this);

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form.MainForm(this));
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
