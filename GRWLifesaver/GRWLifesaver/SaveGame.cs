namespace GRWLifesaver
{

    public class SaveGame
    {

        public static System.Text.RegularExpressions.Regex REGEX_BACKUPSAVE = new System.Text.RegularExpressions.Regex(@"^([1-8])_([0-9]{4}\-[0-9]{2}\-[0-9]{2}_[0-9]{2}\-[0-9]{2}\-[0-9]{2})\.save$");
        public static System.Text.RegularExpressions.Regex REGEX_SAVEGAME_INTERNALIDENTIFIER = new System.Text.RegularExpressions.Regex(@"^([0-9A-Z]+)\[([0-9]+)\]\-tm_([0-9]{4}_[0-9]{2}_[0-9]{2}_[0-9]{2}_[0-9]{2}_[0-9]{2})$");
        
        private System.Collections.Generic.List<System.String> backups = new System.Collections.Generic.List<string>();
        private System.DateTime lastBackup = System.DateTime.MinValue;

        private System.Object sync_lastChanged = new System.Object();
        private System.Object sync_backupTime = new System.Object();


        static SaveGame()
        {
            SaveGame.BackupService = new BackupService();
        }


        public SaveGame(AccountProfile accountProfile, System.Int32 gameID, System.Int32 id)
        {
            if (accountProfile is null) throw new System.ArgumentNullException("accountProfile");
            if (gameID < 0) throw new System.ArgumentException("GameID must be positive value");
            if (id < 1 || id > 8) throw new System.ArgumentException("id must be in range between 1 and 8");
            
            this.AccountProfile = accountProfile;
            this.GameID = gameID;
            this.ID = id;
            this.FileInfo = new System.IO.FileInfo(this.AccountProfile.GRWLifesaver.UplayFolder + @"\savegames\" + this.AccountProfile.UUID + @"\" + this.GameID.ToString() + @"\" + this.ID + @".save");
        }


        public static BackupService BackupService
        {
            get;
            private set;
        }


        public AccountProfile AccountProfile
        {
            get;
            private set;
        }


        public System.Int32 GameID
        {
            get;
            private set;
        }


        public System.String GameName
        {
            get
            {
                switch (this.GameID) {
                    case AccountProfile.UplayPCGameID:
                        return "Uplay PC";
                    case AccountProfile.SteamPCGameID:
                        return "Steam PC";
                    default:
                        return "Game";
                }
            }
        }


        public System.Int32 ID
        {
            get;
            private set;
        }


        protected System.IO.FileInfo FileInfo
        {
            get;
            set;
        }


        public System.Boolean Exists
        {
            get
            {
                return this.FileInfo.Exists;
            }
        }


        public System.Boolean BackupExists
        {
            get
            {
                lock (this.backups) {
                    return this.Backups.Length > 0;
                }
            }
        }


        public System.String[] Backups
        {
            get
            {
                System.Int32 backupsCount;
                lock (this.backups) {
                    backupsCount = this.backups.Count;
                }
                
                if (backupsCount <= 0) {
                    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(this.AccountProfile.GRWLifesaver.BackupFolder + @"\" + this.AccountProfile.UUID + @"\" + this.GameID + @"\");

                    if (dirInfo.Exists) {
                        foreach (System.IO.FileInfo subDirInfo in dirInfo.GetFiles()) {
                            if (SaveGame.REGEX_BACKUPSAVE.IsMatch(subDirInfo.Name)) {
                                if (System.Int32.Parse(SaveGame.REGEX_BACKUPSAVE.Replace(subDirInfo.Name, "$1")) == this.ID) {
                                    lock (this.backups) {
                                        this.backups.Add(subDirInfo.Name);
                                    }
                                }
                            }
                        }
                    }
                }

                lock (this.backups) {
                    return this.backups.ToArray();
                }
            }
        }


        public System.DateTime LastBackup
        {
            get
            {
                if (this.lastBackup == System.DateTime.MinValue) {
                    System.String[] backups;
                    lock (this.backups) {
                        backups = this.backups.ToArray();
                    }

                    System.DateTime lastBackup = System.DateTime.MinValue;
                    
                    System.DateTime cacheBackupTime;
                    foreach (System.String backup in backups) {
                        cacheBackupTime = this.GetBackupTime(backup);

                        if (cacheBackupTime > lastBackup) {
                            lastBackup = cacheBackupTime;
                        }
                    }

                    this.LastBackup = lastBackup;
                }

                return this.lastBackup;
            }
            private set
            {
                this.lastBackup = value;
            }
        }


        public System.DateTime GetBackupTime(System.String backup)
        {
            if (!SaveGame.REGEX_BACKUPSAVE.IsMatch(backup)) {
                throw new System.ArgumentException("backup name is invalid");
            }

            lock (this.sync_backupTime) {
                return SaveGame.GetSaveGameSaveTime(this.AccountProfile.GRWLifesaver.BackupFolder + @"\" + this.AccountProfile.UUID + @"\" + this.GameID + @"\" + backup);
            }
        }


        public System.String GetBackupFromDate(System.DateTime date)
        {
            foreach (System.String backup in this.Backups) {
                if (this.GetBackupTime(backup) == date) {
                    return backup;
                }
            }

            return null;
        }


        public void EnsureBackup()
        {
            if (this.Exists) {
                System.String newBackupName = this.ID + "_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".save";

                lock (this.backups) {
                    System.DateTime date;
                    foreach (System.String backup in this.Backups) {
                        date = this.GetBackupTime(backup);

                        if (date == this.LastChanged) {
                            return;
                        }
                    }

                    // Ensure Directory exists
                    System.IO.Directory.CreateDirectory(this.AccountProfile.GRWLifesaver.BackupFolder + @"\" + this.AccountProfile.UUID + @"\" + this.GameID + @"\");

                    // Ensure Backup
                    this.FileInfo.CopyTo(this.AccountProfile.GRWLifesaver.BackupFolder + @"\" + this.AccountProfile.UUID + @"\" + this.GameID + @"\" + newBackupName, true);
                    this.LastBackup = System.DateTime.MinValue;

                    if (this.backups.Find(item => item == newBackupName) is null) {
                        this.backups.Add(newBackupName);
                    }
                }
            }
        }


        public void DeleteBackup(System.String backup)
        {
            if (!SaveGame.REGEX_BACKUPSAVE.IsMatch(backup)) {
                throw new System.ArgumentException("backup name is invalid");
            }
            
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(this.AccountProfile.GRWLifesaver.BackupFolder + @"\" + this.AccountProfile.UUID + @"\" + this.GameID + @"\" + backup);

            if (fileInfo.Exists) {
                lock (this.backups) {
                    System.Int32 foundIndex = this.backups.FindIndex(item => item == backup);
                    if (foundIndex > -1) {
                        this.backups.RemoveAt(foundIndex);
                    }
                }
                this.LastBackup = System.DateTime.MinValue;

                fileInfo.Delete();
            }
        }


        public void RestoreBackup(System.String backup)
        {
            if (!SaveGame.REGEX_BACKUPSAVE.IsMatch(backup)) {
                throw new System.ArgumentException("backup name is invalid");
            }

            System.IO.File.Copy(this.AccountProfile.GRWLifesaver.BackupFolder + @"\" + this.AccountProfile.UUID + @"\" + this.GameID + @"\" + backup, this.FileInfo.FullName, true);
        }


        public void EnsureProceduralBackup()
        {
            if ((this.LastChanged - this.LastBackup).TotalSeconds >= 120) {
                this.EnsureBackup();
            }
        }


        public System.DateTime LastChanged
        {
            get
            {
                lock (this.sync_lastChanged) {
                    return SaveGame.GetSaveGameSaveTime(this.FileInfo.FullName);
                }
            }
        }


        public System.Boolean BackupEnabled
        {
            get
            {
                return SaveGame.BackupService.HasSaveGame(this);
            }
            set
            {
                if (value) {
                    SaveGame.BackupService.AddSaveGame(this);
                } else {
                    SaveGame.BackupService.RemoveSaveGame(this);
                }
            }
        }


        public void ClearCache()
        {
            this.backups.Clear();
            this.lastBackup = System.DateTime.MinValue;
        }


        internal static System.DateTime GetSaveGameSaveTime(System.String file)
        {
            return SaveGame.GetSaveGameSaveTime(file, 0);
        }


        private static System.DateTime GetSaveGameSaveTime(System.String file, System.Int32 tryCount)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

            try {
                if (fileInfo.Exists) {
                    System.IO.FileStream fileStream = System.IO.File.OpenRead(fileInfo.FullName);

                    if (fileStream.Length >= 552) {
                        System.Byte[] data = new System.Byte[512];
                        fileStream.Seek(40, System.IO.SeekOrigin.Begin);
                        System.Int32 read = fileStream.Read(data, 0, 512);
                        fileStream.Close();

                        System.String dataStr = System.Text.Encoding.Unicode.GetString(data, 0, read);

                        System.Int32 firstNull = dataStr.IndexOf('\x0');
                        if (firstNull > 0) {
                            dataStr = dataStr.Substring(0, firstNull);
                        }

                        if (SaveGame.REGEX_SAVEGAME_INTERNALIDENTIFIER.IsMatch(dataStr)) {
                            System.DateTime date;
                            if (System.DateTime.TryParseExact(SaveGame.REGEX_SAVEGAME_INTERNALIDENTIFIER.Replace(dataStr, "$3"), "yyyy_MM_dd_HH_mm_ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date)) {
                                return date;
                            }
                        }
                    }
                }
            } catch (System.IO.FileNotFoundException) {
                // Suppress Exception
            } catch (System.Exception ex) {
                if (tryCount < 3) {
                    System.Threading.Thread.Sleep(60);
                    return SaveGame.GetSaveGameSaveTime(file, tryCount++);
                } else {
                    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return System.DateTime.MinValue;
        }

    }

}