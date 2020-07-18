// Decompiled with JetBrains decompiler
// Type: ADTechnology.Classes.FtpShell
// Assembly: FtpShell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3691c68ab77a0ae2
// MVID: 27A79587-E493-4379-9483-0CE7010BF7FB
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\FtpShell.dll

using System.Collections;
using System.IO;

using EnterpriseDT.Net.Ftp;

namespace ADTechnology.Classes
{
    public class FtpShellEDT
    {
        private FtpShellEDT.OSType ostype = FtpShellEDT.OSType.Unknown;
        private SecureFTPConnection conn;
        private MemoryStream dataStream;
        public int port;

        public string server
        {
            get
            {
                return this.conn.ServerAddress;
            }
            set
            {
                this.conn.ServerAddress = value;
            }
        }

        public string user
        {
            get
            {
                return this.conn.UserName;
            }
            set
            {
                this.conn.UserName = value;
            }
        }

        public string pass
        {
            get
            {
                return this.conn.Password;
            }
            set
            {
                this.conn.Password = value;
            }
        }

        public bool IsConnected
        {
            get
            {
                return this.conn.IsConnected;
            }
        }

        public int timeout
        {
            get
            {
                return this.conn.Timeout;
            }
            set
            {
                this.conn.Timeout = value;
            }
        }

        public FtpShellEDT.OSType osType
        {
            get
            {
                return this.ostype;
            }
        }

        public FtpShellEDT.FtpProtocol Protocol
        {
            get
            {
                return (FtpShellEDT.FtpProtocol)this.conn.Protocol;
            }
            set
            {
                this.conn.Protocol = (FileTransferProtocol)value;
            }
        }

        public FtpShellEDT.AuthenticationType AuthenticationMethod
        {
            get
            {
                return (FtpShellEDT.AuthenticationType)this.conn.AuthenticationMethod;
            }
            set
            {
                this.conn.AuthenticationMethod = (EnterpriseDT.Net.Ssh.AuthenticationType)value;
            }
        }

        public FtpShellEDT.ServerValidationType ServerValidation
        {
            get
            {
                return (FtpShellEDT.ServerValidationType)this.conn.ServerValidation;
            }
            set
            {
                this.conn.ServerValidation = (SecureFTPServerValidationType)value;
            }
        }

        public string KnownHostsFile
        {
            get
            {
                return this.conn.KnownHosts.KnownHostsFile;
            }
            set
            {
                this.conn.KnownHosts.KnownHostsFile = value;
            }
        }

        public MemoryStream DataStream
        {
            get
            {
                return this.dataStream;
            }
        }

        public FtpShellEDT()
        {
            this.conn = new SecureFTPConnection();
            this.conn.LicenseOwner = "AdvancedDataTech";
            this.conn.LicenseKey = "020-9954-3873-3000";
            this.conn.Protocol = FileTransferProtocol.SFTP;
            this.conn.AuthenticationMethod = EnterpriseDT.Net.Ssh.AuthenticationType.KeyboardInteractive;
            this.conn.ServerValidation = SecureFTPServerValidationType.None;
            this.conn.PreferredKeyExchangeMethods = EnterpriseDT.Net.Ssh.SSHKeyExchangeMethod.DiffieHellmanGroupExchangeSha1;
        }

        public void Connect(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.conn.Connect();
        }

        public void Disconnect()
        {
            if (!this.conn.IsConnected)
                return;
            this.conn.Close();
        }

        public long OpenStream(string file)
        {
            if (!this.conn.IsConnected)
                return 0;
            this.dataStream = new MemoryStream();
            this.conn.CloseStreamsAfterTransfer = false;
            this.conn.DownloadStream((Stream)this.dataStream, file);
            this.dataStream.Seek(0L, SeekOrigin.Begin);
            return this.dataStream.Length;
        }

        public void CloseStream()
        {
            if (this.dataStream == null)
                return;
            this.dataStream.Close();
            this.dataStream.Dispose();
        }

        public ArrayList List()
        {
            ArrayList arrayList = new ArrayList();
            foreach (FTPFile fileInfo in this.conn.GetFileInfos())
            {
                string[] strArray1 = new string[4]
                {
          fileInfo.Name,
          fileInfo.Size.ToString(),
          fileInfo.LastModified.ToString("d"),
          null
                };
                if (fileInfo.LastModified.Hour > 0 || fileInfo.LastModified.Minute > 0)
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[2] = strArray2[2] + " " + fileInfo.LastModified.ToString("HH:mm");
                }
                strArray1[3] = !fileInfo.Dir ? (!fileInfo.Link ? "F" : "L") : "D";
                arrayList.Add((object)strArray1);
                if (this.ostype < FtpShellEDT.OSType.Windows)
                    this.ostype = (FtpShellEDT.OSType)fileInfo.Type;
            }
            return arrayList;
        }

        public string GetWorkingDirectory()
        {
            return this.conn.ServerDirectory;
        }

        public void ChangeDir(string directory)
        {
            this.conn.ChangeWorkingDirectory(directory);
        }

        public void CdUp()
        {
            this.conn.ChangeWorkingDirectoryUp();
        }

        public enum OSType
        {
            Unknown = -1,
            Windows = 0,
            Unix = 1,
        }

        public enum FtpProtocol
        {
            FTP = 0,
            SFTP = 3,
        }

        public enum AuthenticationType
        {
            PublicKey = 2,
            Password = 3,
            KeyboardInteractive = 4,
            PublicKeyAndPassword = 5,
        }

        public enum ServerValidationType
        {
            None,
            Automatic,
            AutomaticNoNameCheck,
        }
    }
}
