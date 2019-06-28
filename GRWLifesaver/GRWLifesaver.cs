namespace GRWLifesaver
{

    public class GRWLifesaver
    {

        public const System.String UplayDefaultInstallPath = "C:\\Program Files (x86)\\Ubisoft\\Ubisoft Game Launcher\\";

        private RegistryPointer RP_UplayInstallDirUbisoft = new RegistryPointer(Microsoft.Win32.RegistryHive.LocalMachine, @"SOFTWARE\Wow6432Node\Ubisoft\Launcher", "InstallDir");
        private RegistryPointer RP_UplayInstallDirWindows = new RegistryPointer(Microsoft.Win32.RegistryHive.LocalMachine, @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Uplay", "InstallLocation");
        private RegistryPointer RP_BackupFolder = new RegistryPointer(Microsoft.Win32.RegistryHive.LocalMachine, @"SOFTWARE\Wow6432Node\Ubisoft\Lifesaver", "BackupFolder");
        private RegistryPointer RP_UserBackupFolder = new RegistryPointer(Microsoft.Win32.RegistryHive.CurrentUser, @"Software\Ubisoft\Lifesaver", "BackupFolder");

        private System.String backupFolder;
        private System.String uplayFolder;

        private System.Collections.Generic.List<AccountProfile> accountProfiles = new System.Collections.Generic.List<AccountProfile>();
        private AccountProfileCollection accountProfilesReadOnly;


        public GRWLifesaver(Program program)
        {
            if (program is null) throw new System.ArgumentNullException("program");

            this.Program = program;
            this.accountProfilesReadOnly = new AccountProfileCollection(this.accountProfiles);
        }


        public Program Program
        {
            get;
            private set;
        }


        public System.String BackupFolder
        {
            get
            {
                if (this.backupFolder is null) {
                    try {
                        this.backupFolder = this.RP_BackupFolder.GetString();
                    } catch (System.Exception ex) when (ex is System.Security.SecurityException || ex is System.UnauthorizedAccessException) {
                        this.backupFolder = this.RP_UserBackupFolder.GetString();
                    }

                    if (!(this.backupFolder is null) && !System.IO.Directory.Exists(this.backupFolder)) {
                        this.BackupFolder = null;
                    }
                }

                return this.backupFolder;
            }
            set
            {
                try {
                    this.RP_BackupFolder.SetValue(value);
                } catch (System.Exception ex) when (ex is System.Security.SecurityException || ex is System.UnauthorizedAccessException) {
                    this.RP_UserBackupFolder.SetValue(value);
                }
                this.backupFolder = null;
                foreach (AccountProfile accountProfile in this.AccountProfiles) {
                    accountProfile.ClearCache();
                }
            }
        }


        public System.String UplayFolder
        {
            get
            {
                if (this.uplayFolder is null) {
                    if (GRWLifesaver.IsUplayInstallationDirectory(this.RP_UplayInstallDirUbisoft.GetString())) {
                        this.uplayFolder = this.RP_UplayInstallDirUbisoft.GetString();
                    } else if (GRWLifesaver.IsUplayInstallationDirectory(this.RP_UplayInstallDirWindows.GetString())) {
                        this.uplayFolder = this.RP_UplayInstallDirWindows.GetString();
                    } else if (GRWLifesaver.IsUplayInstallationDirectory(GRWLifesaver.UplayDefaultInstallPath)) {
                        this.uplayFolder = GRWLifesaver.UplayDefaultInstallPath;
                    }
                }

                return this.uplayFolder;
            }
        }


        public AccountProfileCollection AccountProfiles
        {
            get
            {
                if (this.accountProfiles.Count <= 0) {
                    System.String basename;
                    foreach (System.String dir in System.IO.Directory.EnumerateDirectories(this.UplayFolder + @"\savegames\")) {
                        basename = new System.IO.DirectoryInfo(dir).Name;
                        if (AccountProfile.REGEX_ACCOUNTUUID.IsMatch(basename)) {
                            this.accountProfiles.Add(new AccountProfile(this, basename));
                        }
                    }
                }

                return this.accountProfilesReadOnly;
            }
        }


        /************************************************/
        /***************  STATIC METHODS  ***************/
        /************************************************/

        private static System.Boolean IsUplayInstallationDirectory(System.String path)
        {
            // prevent NullPointerException
            if (path is null) {
                return false;
            }

            // normalize 'path'
            path = GRWLifesaver.NormalizePath(path);

            // the directory has to exist
            if (!System.IO.Directory.Exists(path)) {
                return false;
            }

            // require a few files to exist
            if (!System.IO.File.Exists(path + "\\" + "Uplay.exe")) {
                return false;
            }
            if (!System.IO.File.Exists(path + "\\" + "upc.exe")) {
                return false;
            }
            if (!System.IO.File.Exists(path + "\\" + "UbisoftGameLauncher.exe")) {
                return false;
            }
            if (!System.IO.File.Exists(path + "\\" + "UbisoftGameLauncher64.exe")) {
                return false;
            }
            if (!System.IO.File.Exists(path + "\\" + "Uninstall.exe")) {
                return false;
            }

            return true;
        }


        private static string NormalizePath(string path)
        {
            return System.IO.Path.GetFullPath(new System.Uri(path).LocalPath)
                       .TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }


    }

}