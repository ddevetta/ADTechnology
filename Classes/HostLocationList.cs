// Decompiled with JetBrains decompiler
// Type: ADTechnology.Classes.HostLocationList
// Assembly: Classes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3691c68ab77a0ae2
// MVID: CE1BA639-CFBF-46AF-9C7E-ED92C98B418E
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\Classes.dll

using System;
using System.Collections;

namespace ADTechnology.Classes
{
    [Serializable]
    public class HostLocationList : CollectionBase
    {
        public HostLocation this[int index]
        {
            get
            {
                return (HostLocation)this.List[index];
            }
            set
            {
                this.List[index] = (object)value;
            }
        }

        public int Add(HostLocation HostLocation)
        {
            return this.List.Add((object)HostLocation);
        }

        public int AddOrUpdate(HostLocation HostLocation)
        {
            int index = this.Find(HostLocation);
            if  (index == -1)  
                return this.List.Add(HostLocation);
            else
            {
                HostLocation hl = this[index];
                hl.Host = HostLocation.Host;
                hl.User = HostLocation.User;
                hl.Password = HostLocation.Password;
                hl.EncryptedPassword = HostLocation.EncryptedPassword;
                hl.Path = HostLocation.Path;
                return index;
            }
        }

        public int Find(HostLocation HostLocation)
        {
            int index = 0;
            for (; index < this.List.Count; ++index)
            {
                HostLocation hl = (HostLocation)this.List[index];
                if (hl.Name == HostLocation.Name)
                    return index;
            }
            return -1;
        }

        public void Remove(HostLocation HostLocation)
        {
            this.List.Remove((object)HostLocation);
        }
    }
}
