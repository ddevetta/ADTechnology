using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTechnology.Classes
{
    /// <summary>
    /// Describes a single FileAssociation retrieved from the registry.
    /// </summary>
    public class FileAssociationType
    {
        /// <summary>
        /// Gets the file extension.
        /// 'HKEY_CLASSES_ROOT'
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string Extension { get; private set; }

        /// <summary>
        /// Gets the extension identifier.
        /// 'HKEY_CLASSES_ROOT\{extension}'
        /// </summary>
        /// <value>
        /// The extension identifier.
        /// </value>
        public string ExtensionId { get; private set; }

        /// <summary>
        /// Gets the type of the extension.
        /// 'HKEY_CLASSES_ROOT\{extension}' (default)
        /// </summary>
        /// <value>
        /// The type of the extension.
        /// </value>
        public string ExtensionType { get; private set; }

        /// <summary>
        /// Gets the MIME type of the content.
        /// 'HKEY_CLASSES_ROOT\{extension}\Content Type' (default)
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; private set; }

        /// <summary>
        /// Gets the percieved type of the file.
        /// 'HKEY_CLASSES_ROOT\{extension}\CPercievedType' (default)
        /// </summary>
        /// <value>
        /// The percieved type.
        /// </value>
        public string PercievedType { get; private set; }

        /// <summary>
        /// Gets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public string ProgId { get; private set; }

        public string Program { get; private set; }

        /// <summary>
        /// Gets the CLSID.
        /// 'HKEY_CLASSES_ROOT\{extensionId}\CLSID' (default)
        /// </summary>
        /// <value>
        /// The CLSID.
        /// </value>
        public string CLSID { get; private set; }

        /// <summary>
        /// Gets the Default Icon location.
        /// 'HKEY_CLASSES_ROOT\{extensionId}\DefaultIcon' (default)
        /// </summary>
        /// <value>
        /// The default icon location.
        /// </value>
        public string DefaultIcon { get; private set; }

        /// <summary>
        /// Gets the default shell action, if specified.
        /// </summary>
        /// <value>
        /// The default shell action.
        /// </value>
        public string DefaultShellAction { get; private set; }

        /// <summary>
        /// Gets the actions defined for this file association, e.g. Open, Edit etc.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        public ClassAction[] Actions { get; private set; }

        public FileAssociationType()
        {
        }

        public FileAssociationType(string extension)
        {
            Extension = extension;
        }

        /// <summary>
        /// Fills this instance with values from the registry for the file extension supplied in the constructor (or set later).
        /// The registry on the machine that this class is instantiated is queried.
        /// </summary>
        /// <exception cref="System.ApplicationException">
        /// Extension is null or empty
        /// or
        /// Extension must start with a full-stop character
        /// or
        /// Extension '{0}' is not found in the registry (HKEY_CLASSES_ROOT)
        /// </exception>
        internal void Fill()
        {
            if (string.IsNullOrEmpty(this.Extension))
                throw new ApplicationException("Extension is null or empty");
            if (!this.Extension.StartsWith("."))
                throw new ApplicationException("Extension must start with a full-stop character");

            RegistryKey classes = Registry.ClassesRoot;
            RegistryKey machine = Registry.LocalMachine;

            RegistryKey regKey = classes.OpenSubKey(this.Extension);
            if (regKey == null)
            {
                classes.Close();
                throw new ApplicationException(string.Format("Extension '{0}' is not found in the registry (HKEY_CLASSES_ROOT)", this.Extension));
            }
            
            object defaultValue;

            defaultValue = regKey.GetValue("Content Type");
            if (defaultValue != null)
                this.ContentType = defaultValue.ToString();

            defaultValue = regKey.GetValue("PerceivedType");
            if (defaultValue != null)
                this.PercievedType = defaultValue.ToString();

            //Get ExtensionId. If null, don't continue filling in the rest
            defaultValue = regKey.GetValue("");
            if (defaultValue == null)
                return;

            this.ExtensionId = defaultValue.ToString();

            regKey = regKey.OpenSubKey("OpenWithProgids");
            if (regKey != null)
            {
                string[] progids = regKey.GetValueNames();
                if (progids.Length > 0)
                {
                    this.ProgId = progids[0];
                    regKey = machine.OpenSubKey("SOFTWARE\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\CurrentVersion\\AppModel\\PackageRepository\\Extensions\\ProgIDs\\" + this.ProgId);
                    if (regKey != null)
                    {
                        string[] values = regKey.GetValueNames();
                        if (values.Length > 0)
                            this.Program = values[0];
                    }
                }
            }

            regKey = classes.OpenSubKey(this.ExtensionId);
            if (regKey != null)
            {
                defaultValue = regKey.GetValue("");
                if (defaultValue != null)
                    this.ExtensionType = defaultValue.ToString();
            }

            regKey = classes.OpenSubKey(this.ExtensionId + "\\CLSID");
            if (regKey != null)
            {
                defaultValue = regKey.GetValue("");
                if (defaultValue != null)
                    this.CLSID = defaultValue.ToString();
            }

            regKey = classes.OpenSubKey(this.ExtensionId + "\\DefaultIcon");
            if (regKey != null)
            {
                defaultValue = regKey.GetValue("");
                if (defaultValue != null)
                    this.DefaultIcon = defaultValue.ToString();
            }

            regKey = classes.OpenSubKey(this.ExtensionId + "\\shell");
            if (regKey != null)
            {
                defaultValue = regKey.GetValue("");
                if (defaultValue != null)
                    this.DefaultShellAction = defaultValue.ToString();

                string[] actions = regKey.GetSubKeyNames();
                this.Actions = new ClassAction[actions.Length];
                RegistryKey rk = null;
                for (int i = 0; i < actions.Length; i++)
                {
                    this.Actions[i] = new ClassAction(actions[i]);
                    rk = regKey.OpenSubKey(actions[i] + "\\command");
                    if (rk != null)
                    {
                        defaultValue = rk.GetValue("");
                        if (defaultValue != null)
                        {
                            this.Actions[i].ParseCommand(defaultValue.ToString());
//                            Console.WriteLine(this.Extension + "\t" + this.Actions[i].Action + "\t" + this.Actions[i].Command + "\t" + this.Actions[i].Parms);
                        }

                    }
                }
                if (rk != null)
                    rk.Close();
            }

            if (regKey != null)
                regKey.Close();

            classes.Close();
            machine.Close();

        }
    }

    /// <summary>
    /// Describes an Action associated with a FileAssociation
    /// Contains a method to split the 'command' value in the registry into the'command' and 'parms' components (for use in 'Process.Start')
    /// </summary>
    public class ClassAction
    {
        /// <summary>
        /// Gets the associated file action (Open, Edit etc.).
        /// </summary>
        /// <value>
        /// The file action.
        /// </value>
        public string Action { get; private set; }

        /// <summary>
        /// Gets the command (executable) portion of the full command held in the registry.
        /// </summary>
        /// <value>
        /// The command (executable).
        /// </value>
        public string Command { get; private set; }
        /// <summary>
        /// Gets the remaining parms after the executable portion held in the registry for this action.
        /// </summary>
        /// <value>
        /// The parms.
        /// </value>
        public string Parms { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassAction"/> class.
        /// </summary>
        /// <param name="action">The file action.</param>
        public ClassAction(string action)
        {
            Action = action;
        }

        /// <summary>
        /// Parses the command registry entry into the executable <see cref="Command"/> and remaining parameters - <see cref="Parms"/> .
        /// </summary>
        /// <param name="command">The command.</param>
        public void ParseCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                this.Command = this.Parms = string.Empty;
                return;
            }
            int sp = -1;
            if (command.StartsWith("\""))
                sp = command.IndexOf('"', 2);
            else
                sp = command.IndexOf(' ');
            if (sp < 0)
            {
                this.Command = command;
                this.Parms = string.Empty;
            }
            else
            {
                this.Command = command.Substring(0, sp + 1);
                this.Parms = command.Substring(sp + 1);
            }
        }
    }
}
