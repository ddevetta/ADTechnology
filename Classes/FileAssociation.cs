using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTechnology.Classes
{
    /// <summary>
    /// Class for retrieving file association information for a file extension, from the machine registry
    /// File associations are stored in HKEY_CLASSES_ROOT.
    /// </summary>
    public class FileAssociation
    {
        public FileAssociation()
        {
        }

        /// <summary>
        /// Finds the file association data for a specified extension.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <returns>FileAssociationType containing registry data for the extension</returns>
        public FileAssociationType Find(string extension)
        {
            FileAssociationType ret = new FileAssociationType(extension);
            ret.Fill();

            return ret;
        }

        /// <summary>
        /// Gets all file associations from the registry.
        /// </summary>
        /// <returns>A list of all File Associations in the registry</returns>
        public List<FileAssociationType> GetAssociations()
        {
            List<FileAssociationType> ret = new List<FileAssociationType>();

            RegistryKey classes = Registry.ClassesRoot;
            string[] extensions = classes.GetSubKeyNames();
            
            foreach (string extension in extensions)
            {
                if (String.IsNullOrEmpty(extension))
                    continue;
                if (!extension.StartsWith("."))
                    continue;

                FileAssociationType fa = new FileAssociationType(extension);
                fa.Fill();

                if (fa.Extension == null)
                    continue;

                ret.Add(fa);
            }

            classes.Close();

            return ret;
        }
    }
}
