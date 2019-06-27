namespace GRWLifesaver
{

    public class RegistryPointer
    {

        private static RegistryAPI RegistryAPI = new RegistryAPI();

        public RegistryPointer(Microsoft.Win32.RegistryHive registryHive, System.String subKey, System.String name)
        {
            this.RegistryHive = registryHive;
            this.SubKey = subKey;
            this.Name = name;
        }


        public Microsoft.Win32.RegistryHive RegistryHive
        {
            get;
            set;
        }


        public System.String SubKey
        {
            get;
            set;
        }


        public System.String Name
        {
            get;
            set;
        }


        public System.String GetString()
        {
            return RegistryPointer.RegistryAPI.GetString(this);
        }


        public System.Object GetValue()
        {
            return RegistryPointer.RegistryAPI.GetValue(this);
        }


        public void SetString(System.String value)
        {
            RegistryPointer.RegistryAPI.SetString(this, value);
        }


        public void SetValue(System.Object value)
        {
            RegistryPointer.RegistryAPI.SetValue(this, value);
        }


        public void ClearCache()
        {
            RegistryAPI.ClearCache(this);
        }


    }

}