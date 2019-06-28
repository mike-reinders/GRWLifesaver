namespace GRWLifesaver
{

    public class RegistryAPI
    {
        
        private System.Collections.Generic.Dictionary<RegistryPointer, System.Object> cache = new System.Collections.Generic.Dictionary<RegistryPointer, System.Object>();


        public RegistryAPI()
        {
            // Empty Constructor
        }


        public void ClearCache()
        {
            lock (this.cache) {
                this.cache.Clear();
            }
        }


        internal void ClearCache(RegistryPointer registryPointer)
        {
            lock (this.cache) {
                if (!(registryPointer is null)) {
                    this.cache.Remove(registryPointer);
                }
            }
        }


        public System.String GetString(RegistryPointer registryPointer)
        {
            return (System.String)this.GetValue(registryPointer, typeof(System.String));
        }


        public System.Object GetValue(RegistryPointer registryPointer)
        {
            return this.GetValue(registryPointer, null);
        }


        private System.Object GetValue(RegistryPointer registryPointer, System.Type type)
        {
            lock (this.cache) {
                if (this.cache.ContainsKey(registryPointer)) {
                    return this.cache[registryPointer];
                }

                using (Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.RegistryKey.OpenBaseKey(registryPointer.RegistryHive, Microsoft.Win32.RegistryView.Registry32).OpenSubKey(registryPointer.SubKey)) {
                    if (!(regKey is null)) {
                        System.Object obj = regKey.GetValue(registryPointer.Name);

                        if (type is null || obj.GetType() == type) {
                            this.cache.Add(registryPointer, obj);
                            return obj;
                        }
                    }
                }

                return null;
            }
        }


        public void SetString(RegistryPointer registryPointer, System.String value)
        {
            this.SetValue(registryPointer, value);
        }


        public void SetValue(RegistryPointer registryPointer, System.Object value)
        {
            if (registryPointer is null) throw new System.ArgumentNullException("registryPointer");

            using (Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.RegistryKey.OpenBaseKey(registryPointer.RegistryHive, Microsoft.Win32.RegistryView.Registry32).CreateSubKey(registryPointer.SubKey)) {
                lock (this.cache) {
                    if (value is null) {
                        regKey.DeleteValue(registryPointer.SubKey);
                    } else {
                        Microsoft.Win32.RegistryValueKind registryValueKind;

                        if (value.GetType() == typeof(System.String)) {
                            registryValueKind = Microsoft.Win32.RegistryValueKind.String;
                        } else {
                            throw new System.NotSupportedException("unsupported value type");
                        }

                        regKey.SetValue(registryPointer.Name, value, registryValueKind);
                    }

                    this.cache.Remove(registryPointer);
                }
            }
        }

    }

}