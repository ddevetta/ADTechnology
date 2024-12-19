// Decompiled with JetBrains decompiler
// Type: ADTechnology.Classes.FtpShell
// Assembly: FtpShell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3691c68ab77a0ae2
// MVID: 27A79587-E493-4379-9483-0CE7010BF7FB
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\FtpShell.dll

using System;
using System.Collections;
using System.IO;
using System.Threading;

using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace ADTechnology.Classes
{
    public class FtpShell
    {
        private FtpShell.OSType ostype = FtpShell.OSType.Unknown;
        private PasswordConnectionInfo conn;
        private SftpClient client;
        private SftpDownloadAsyncResult async = null;
        private MemoryStream dataStream;
        public int port = 22;

        //public ConnectionInfo Connection
        //{
        //    get
        //    {
        //        return this.conn;
        //    }
        //}

        public bool IsConnected
        {
            get
            {
                return (this.client == null) ? false : this.client.IsConnected;
            }
        }

        public TimeSpan timeout
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

        public FtpShell.OSType osType
        {
            get
            {
                return this.ostype;
            }
        }

        public string ServerVersion
        {
            get
            {
                return this.conn.ServerVersion;
            }
        }

        public AuthenticationMethod AuthenticationMethod
        {
            get
            {
                return this.conn.AuthenticationMethods[0];
            }
        }

        public MemoryStream DataStream
        {
            get
            {
                return this.dataStream;
            }
        }

        public SftpDownloadAsyncResult AsyncResult
        {
            get { return async; }
        }

        public FtpShell()
        {
        }

        public void Connect(string server, string user, string pass)
        {
            this.conn = new PasswordConnectionInfo(server, this.port, user, pass);
            this.client = new SftpClient(conn);
            this.client.Connect();
        }

        public void Disconnect()
        {
            if (this.IsConnected)
            {
                this.client.Disconnect();
            }
        }

        public long OpenStream(string file)
        {
            if (!this.IsConnected)
                return 0;
            this.dataStream = new MemoryStream();
            this.async = (SftpDownloadAsyncResult)this.client.BeginDownloadFile(file, (Stream)this.dataStream);
            while (!async.IsCompleted)
            {
                System.Console.WriteLine(async.DownloadedBytes.ToString());
                Thread.Sleep(200);
            }
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
            foreach (SftpFile fileInfo in this.client.ListDirectory("."))
            {
                string[] arr = new string[5]
                {
                      fileInfo.Name,
                      fileInfo.Length.ToString(),
                      fileInfo.LastWriteTimeUtc.ToString("d") + fileInfo.LastWriteTimeUtc.ToString(" HH:mm"), 
                      fileInfo.IsRegularFile ? "F" : fileInfo.IsDirectory ? "D" : !fileInfo.IsSymbolicLink ? "L" : "?", 
                      fileInfo.LastWriteTimeUtc.ToString("o")
                };
                arrayList.Add((object)arr);
            }
            return arrayList;
        }

        public string GetWorkingDirectory()
        {
            return this.client.WorkingDirectory;
        }

        public void ChangeDir(string directory)
        {
            this.client.ChangeDirectory(directory);
        }

        public void CdUp()
        {
            this.client.ChangeDirectory("..");
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
