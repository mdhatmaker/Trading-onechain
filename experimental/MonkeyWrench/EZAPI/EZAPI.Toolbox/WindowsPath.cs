using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EZAPI.Toolbox
{
    public class WindowsPath
    {
        public WindowsPath()
        {
            string path = Application.ExecutablePath;
            string st;
            st = Path.GetFileName(path);
            st = Path.GetExtension(path);
            st = Path.GetFileNameWithoutExtension(path);
            st = Path.GetPathRoot(path);
            st = Path.GetDirectoryName(path);
            st = Path.GetTempPath();
            FileInfo exeFile = new FileInfo(path);
            string exeFileParent = exeFile.Directory.Parent.FullName;
            st = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

        }

        /// <summary>
        /// Locate an executable file anywhere in the path and execute it.
        /// </summary>
        /// <param name="executableFile">name of executable file (i.e. "mongo.exe")</param>
        public void ExecuteInPath(string executableFile)
        {
            var environmentPath = System.Environment.GetEnvironmentVariable("PATH");
            Trace.WriteLine(environmentPath);
            var paths = environmentPath.Split(';');
            var exePath = paths.Select(x => Path.Combine(x, executableFile))
                               .Where(x => File.Exists(x))
                               .FirstOrDefault();

            Console.WriteLine(exePath);

            if (string.IsNullOrWhiteSpace(exePath) == false)
            {
                Process.Start(exePath);
            }
        }


        /// <summary>
        /// Allows you to set the current directory to:
        /// firstChoice (if it exists)
        /// secondChoice (if it exists)
        /// tempPath (if neither exists and bool argument is set to true) 
        /// </summary>
        /// <param name="firstChoice">attempt to set this current directory first</param>
        /// <param name="secondChoice">attempt to set this current directory as second choice</param>
        /// <param name="setTempDirIfNoneExist">if neither first nor second choice exists, should we choose temp path?</param>
        /// <returns>string of directory used (should be set as current directory at this point)</returns>
        public string SetCurrentDirectory(string firstChoice, string secondChoice, bool setTempDirIfNoneExist)
        {
            string dir = null;
            
            if (Directory.Exists(firstChoice))
            {
                dir = firstChoice;
                Directory.SetCurrentDirectory(firstChoice);
            }
            else if (Directory.Exists(secondChoice))
            {
                dir = secondChoice;
                Directory.SetCurrentDirectory(secondChoice);
            }
            else
            {
                if (setTempDirIfNoneExist == true)
                {
                    dir = Path.GetTempPath();
                    Directory.SetCurrentDirectory(dir);
                }
            }

            return dir;
        }

        /// <summary>
        /// Ensure that the passed path arg exists (create it if it does not).
        /// </summary>
        /// <param name="path">path to check for existence/create</param>
        public void GetOrCreatePath(string path)
        {
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception)
            {
                // Fail silently
            }

        }

    } // class
} // namespace
