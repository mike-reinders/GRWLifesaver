namespace GRWLifesaver.GRWLifesaver
{

    public class AccountProfileCollection : System.Collections.Generic.IReadOnlyCollection<AccountProfile>
    {

        private System.Collections.Generic.ICollection<AccountProfile> accountProfileCollection;
        
        public AccountProfileCollection(System.Collections.Generic.ICollection<AccountProfile> accountProfileCollection)
        {
            if (accountProfileCollection is null) throw new System.ArgumentNullException("accountProfileCollection");

            this.accountProfileCollection = accountProfileCollection;
        }


        public AccountProfile this[System.String uuid]
        {
            get
            {
                foreach (AccountProfile accountProfile in this.accountProfileCollection) {
                    if (accountProfile.UUID == uuid) {
                        return accountProfile;
                    }
                }

                return null;
            }
        }


        public int Count
        {
            get
            {
                return this.accountProfileCollection.Count;
            }
        }


        public System.Collections.Generic.IEnumerator<AccountProfile> GetEnumerator()
        {
            return this.accountProfileCollection.GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.accountProfileCollection.GetEnumerator();
        }


        public void Clear()
        {
            this.accountProfileCollection.Clear();
        }


        public System.String[] ToUUIDArray()
        {
            System.Collections.Generic.List<System.String> uuids = new System.Collections.Generic.List<System.String>();
            foreach (AccountProfile accountProfile in this.accountProfileCollection) {
                uuids.Add(accountProfile.UUID);
            }
            return uuids.ToArray();
        }

    }

}