namespace GRWLifesaver
{

    public class AccountProfile
    {

        public static System.Text.RegularExpressions.Regex REGEX_ACCOUNTUUID = new System.Text.RegularExpressions.Regex("^[a-z0-9]{8}\\-[a-z0-9]{4}\\-[a-z0-9]{4}\\-[a-z0-9]{4}\\-[a-z0-9]{12}$");
        public const System.Int32 UplayPCGameID = 1771;
        public const System.Int32 SteamPCGameID = 3559;


        public AccountProfile(GRWLifesaver grwLifesaver, System.String uuid)
        {
            if (grwLifesaver is null) throw new System.ArgumentNullException("grwLifesaver");
            if (!AccountProfile.REGEX_ACCOUNTUUID.IsMatch(uuid)) throw new System.ArgumentException("Invalid uuid");

            this.GRWLifesaver = grwLifesaver;
            this.UUID = uuid;

            this.UplayPC_SaveGame1 = new SaveGame(this, AccountProfile.UplayPCGameID, 1);
            this.UplayPC_SaveGame2 = new SaveGame(this, AccountProfile.UplayPCGameID, 2);
            this.UplayPC_SaveGame3 = new SaveGame(this, AccountProfile.UplayPCGameID, 3);
            this.UplayPC_SaveGame4 = new SaveGame(this, AccountProfile.UplayPCGameID, 4);
            this.UplayPC_SaveGame5 = new SaveGame(this, AccountProfile.UplayPCGameID, 5);
            this.UplayPC_SaveGame6 = new SaveGame(this, AccountProfile.UplayPCGameID, 6);
            this.UplayPC_SaveGame7 = new SaveGame(this, AccountProfile.UplayPCGameID, 7);
            this.UplayPC_SaveGame8 = new SaveGame(this, AccountProfile.UplayPCGameID, 8);

            this.SteamPC_SaveGame1 = new SaveGame(this, AccountProfile.SteamPCGameID, 1);
            this.SteamPC_SaveGame2 = new SaveGame(this, AccountProfile.SteamPCGameID, 2);
            this.SteamPC_SaveGame3 = new SaveGame(this, AccountProfile.SteamPCGameID, 3);
            this.SteamPC_SaveGame4 = new SaveGame(this, AccountProfile.SteamPCGameID, 4);
            this.SteamPC_SaveGame5 = new SaveGame(this, AccountProfile.SteamPCGameID, 5);
            this.SteamPC_SaveGame6 = new SaveGame(this, AccountProfile.SteamPCGameID, 6);
            this.SteamPC_SaveGame7 = new SaveGame(this, AccountProfile.SteamPCGameID, 7);
            this.SteamPC_SaveGame8 = new SaveGame(this, AccountProfile.SteamPCGameID, 8);
        }


        public GRWLifesaver GRWLifesaver
        {
            get;
            private set;
        }


        public System.String UUID
        {
            get;
            private set;
        }


        public System.Boolean UplayPC_SaveGameExist
        {
            get
            {
                if (this.UplayPC_SaveGame1.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame2.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame3.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame4.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame5.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame6.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame7.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame8.Exists) {
                    return true;
                }

                return false;
            }
        }


        public System.Boolean SteamPC_SaveGameExist
        {
            get
            {
                if (this.SteamPC_SaveGame1.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame2.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame3.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame4.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame5.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame6.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame7.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame8.Exists) {
                    return true;
                }

                return false;
            }
        }


        public SaveGame UplayPC_SaveGame1
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame2
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame3
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame4
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame5
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame6
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame7
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame8
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame1
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame2
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame3
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame4
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame5
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame6
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame7
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame8
        {
            get;
            private set;
        }


        public void ClearCache()
        {
            this.UplayPC_SaveGame1.ClearCache();
            this.UplayPC_SaveGame2.ClearCache();
            this.UplayPC_SaveGame3.ClearCache();
            this.UplayPC_SaveGame4.ClearCache();
            this.UplayPC_SaveGame5.ClearCache();
            this.UplayPC_SaveGame6.ClearCache();
            this.UplayPC_SaveGame7.ClearCache();
            this.UplayPC_SaveGame8.ClearCache();

            this.SteamPC_SaveGame1.ClearCache();
            this.SteamPC_SaveGame2.ClearCache();
            this.SteamPC_SaveGame3.ClearCache();
            this.SteamPC_SaveGame4.ClearCache();
            this.SteamPC_SaveGame5.ClearCache();
            this.SteamPC_SaveGame6.ClearCache();
            this.SteamPC_SaveGame7.ClearCache();
            this.SteamPC_SaveGame8.ClearCache();
        }


    }

}