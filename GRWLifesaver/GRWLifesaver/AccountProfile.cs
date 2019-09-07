namespace GRWLifesaver.GRWLifesaver
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
            this.UplayPC_SaveGame9 = new SaveGame(this, AccountProfile.UplayPCGameID, 9);
            this.UplayPC_SaveGame10 = new SaveGame(this, AccountProfile.UplayPCGameID, 10);
            this.UplayPC_SaveGame11 = new SaveGame(this, AccountProfile.UplayPCGameID, 11);
            this.UplayPC_SaveGame12 = new SaveGame(this, AccountProfile.UplayPCGameID, 12);
            this.UplayPC_SaveGame13 = new SaveGame(this, AccountProfile.UplayPCGameID, 13);
            this.UplayPC_SaveGame14 = new SaveGame(this, AccountProfile.UplayPCGameID, 14);
            this.UplayPC_SaveGame15 = new SaveGame(this, AccountProfile.UplayPCGameID, 15);
            this.UplayPC_SaveGame16 = new SaveGame(this, AccountProfile.UplayPCGameID, 16);
            this.UplayPC_SaveGame17 = new SaveGame(this, AccountProfile.UplayPCGameID, 17);
            this.UplayPC_SaveGame18 = new SaveGame(this, AccountProfile.UplayPCGameID, 18);
            this.UplayPC_SaveGame19 = new SaveGame(this, AccountProfile.UplayPCGameID, 19);
            this.UplayPC_SaveGame20 = new SaveGame(this, AccountProfile.UplayPCGameID, 20);

            this.SteamPC_SaveGame1 = new SaveGame(this, AccountProfile.SteamPCGameID, 1);
            this.SteamPC_SaveGame2 = new SaveGame(this, AccountProfile.SteamPCGameID, 2);
            this.SteamPC_SaveGame3 = new SaveGame(this, AccountProfile.SteamPCGameID, 3);
            this.SteamPC_SaveGame4 = new SaveGame(this, AccountProfile.SteamPCGameID, 4);
            this.SteamPC_SaveGame5 = new SaveGame(this, AccountProfile.SteamPCGameID, 5);
            this.SteamPC_SaveGame6 = new SaveGame(this, AccountProfile.SteamPCGameID, 6);
            this.SteamPC_SaveGame7 = new SaveGame(this, AccountProfile.SteamPCGameID, 7);
            this.SteamPC_SaveGame8 = new SaveGame(this, AccountProfile.SteamPCGameID, 8);
            this.SteamPC_SaveGame9 = new SaveGame(this, AccountProfile.SteamPCGameID, 9);
            this.SteamPC_SaveGame10 = new SaveGame(this, AccountProfile.SteamPCGameID, 10);
            this.SteamPC_SaveGame11 = new SaveGame(this, AccountProfile.SteamPCGameID, 11);
            this.SteamPC_SaveGame12 = new SaveGame(this, AccountProfile.SteamPCGameID, 12);
            this.SteamPC_SaveGame13 = new SaveGame(this, AccountProfile.SteamPCGameID, 13);
            this.SteamPC_SaveGame14 = new SaveGame(this, AccountProfile.SteamPCGameID, 14);
            this.SteamPC_SaveGame15 = new SaveGame(this, AccountProfile.SteamPCGameID, 15);
            this.SteamPC_SaveGame16 = new SaveGame(this, AccountProfile.SteamPCGameID, 16);
            this.SteamPC_SaveGame17 = new SaveGame(this, AccountProfile.SteamPCGameID, 17);
            this.SteamPC_SaveGame18 = new SaveGame(this, AccountProfile.SteamPCGameID, 18);
            this.SteamPC_SaveGame19 = new SaveGame(this, AccountProfile.SteamPCGameID, 19);
            this.SteamPC_SaveGame20 = new SaveGame(this, AccountProfile.SteamPCGameID, 20);
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
                if (this.UplayPC_SaveGame9.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame10.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame11.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame12.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame13.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame14.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame15.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame16.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame17.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame18.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame19.Exists) {
                    return true;
                }
                if (this.UplayPC_SaveGame20.Exists) {
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
                if (this.SteamPC_SaveGame9.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame10.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame11.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame12.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame13.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame14.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame15.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame16.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame17.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame18.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame19.Exists) {
                    return true;
                }
                if (this.SteamPC_SaveGame20.Exists) {
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


        public SaveGame UplayPC_SaveGame9
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame10
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame11
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame12
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame13
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame14
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame15
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame16
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame17
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame18
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame19
        {
            get;
            private set;
        }


        public SaveGame UplayPC_SaveGame20
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


        public SaveGame SteamPC_SaveGame9
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame10
        {
            get;
            private set;
        }

    
        public SaveGame SteamPC_SaveGame11
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame12
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame13
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame14
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame15
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame16
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame17
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame18
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame19
        {
            get;
            private set;
        }


        public SaveGame SteamPC_SaveGame20
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
            this.UplayPC_SaveGame9.ClearCache();
            this.UplayPC_SaveGame10.ClearCache();
            this.UplayPC_SaveGame11.ClearCache();
            this.UplayPC_SaveGame12.ClearCache();
            this.UplayPC_SaveGame13.ClearCache();
            this.UplayPC_SaveGame14.ClearCache();
            this.UplayPC_SaveGame15.ClearCache();
            this.UplayPC_SaveGame16.ClearCache();
            this.UplayPC_SaveGame17.ClearCache();
            this.UplayPC_SaveGame18.ClearCache();
            this.UplayPC_SaveGame19.ClearCache();
            this.UplayPC_SaveGame20.ClearCache();

            this.SteamPC_SaveGame1.ClearCache();
            this.SteamPC_SaveGame2.ClearCache();
            this.SteamPC_SaveGame3.ClearCache();
            this.SteamPC_SaveGame4.ClearCache();
            this.SteamPC_SaveGame5.ClearCache();
            this.SteamPC_SaveGame6.ClearCache();
            this.SteamPC_SaveGame7.ClearCache();
            this.SteamPC_SaveGame8.ClearCache();
            this.SteamPC_SaveGame9.ClearCache();
            this.SteamPC_SaveGame10.ClearCache();
            this.SteamPC_SaveGame11.ClearCache();
            this.SteamPC_SaveGame12.ClearCache();
            this.SteamPC_SaveGame13.ClearCache();
            this.SteamPC_SaveGame14.ClearCache();
            this.SteamPC_SaveGame15.ClearCache();
            this.SteamPC_SaveGame16.ClearCache();
            this.SteamPC_SaveGame17.ClearCache();
            this.SteamPC_SaveGame18.ClearCache();
            this.SteamPC_SaveGame19.ClearCache();
            this.SteamPC_SaveGame20.ClearCache();
        }


    }

}