namespace GRWLifesaver.GRWLifesaver
{

    public class BackupService
    {

        private System.Collections.Generic.HashSet<SaveGame> saveGames = new System.Collections.Generic.HashSet<SaveGame>();
        private System.Threading.Thread thread;
        private System.Boolean closeThread;


        public BackupService()
        {

        }


        public System.Boolean IsEnabled
        {
            get
            {
                lock (this.saveGames) {
                    return !(this.thread is null) && this.thread.IsAlive;
                }
            }
        }


        public void AddSaveGame(SaveGame saveGame)
        {
            if (saveGame is null) return;

            lock (this.saveGames) {
                if (!this.saveGames.Contains(saveGame)) {
                    saveGames.Add(saveGame);

                    if (thread is null || !thread.IsAlive) {
                        thread = new System.Threading.Thread(this.mainThread);
                        thread.Start();
                    }
                }
            }
        }


        public void RemoveSaveGame(SaveGame saveGame)
        {
            if (saveGame is null) return;

            lock (this.saveGames) {
                if (this.saveGames.Contains(saveGame)) {
                    saveGames.Remove(saveGame);
                }
            }
        }


        public System.Boolean HasSaveGame(SaveGame saveGame)
        {
            lock (this.saveGames) {
                return !(saveGame is null) && this.saveGames.Contains(saveGame);
            }
        }


        public void Disable() => this.Disable(-1);


        public void Disable(System.Int32 millisecondsTimeout)
        {
            lock (this.saveGames) {
                this.closeThread = true;
                this.saveGames.Clear();

                if (millisecondsTimeout >= 0) {
                    this.thread.Join(millisecondsTimeout);
                }
            }
        }


        private void mainThread()
        {
            // init values
            this.closeThread = false;
            System.DateTime lastProcedualBackup = System.DateTime.MinValue;
            System.Boolean didBackupOnce = false;
            
            while (this.saveGames.Count > 0 && !this.closeThread) {
                if ((System.DateTime.Now - lastProcedualBackup).TotalSeconds > 120) {
                    lock (this.saveGames) {
                        SaveGame[] saveGames = new SaveGame[this.saveGames.Count];
                        this.saveGames.CopyTo(saveGames);
                    }
                    foreach (SaveGame saveGame in saveGames) {
                        if (saveGame.LastChanged > saveGame.LastBackup) {
                            saveGame.EnsureBackup();
                            didBackupOnce = true;
                        }

                        if (this.closeThread) {
                            break;
                        }
                    }

                    if (didBackupOnce) {
                        didBackupOnce = false;
                        lastProcedualBackup = System.DateTime.Now;
                    }
                }

                // sleep
                System.Threading.Thread.Sleep(100);
            }
        }

    }

}