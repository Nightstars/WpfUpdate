using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Update.Common.zip
{
    public class ZipUtil
    {
        #region initialize
        public delegate void UnZipEventHandler(object sender, UnZipEventArgs e);
        public event UnZipEventHandler UnZipEvent;
        #endregion

        #region Unzip file
        /// <summary>
        /// unzio zip file，and replace to target directory
        /// </summary>
        /// <param name="zipFilePath">zip file path</param>
        /// <param name="targetDir">Tagert path</param>
        /// <returns></returns>
        public bool UnZip(string zipFilePath, string targetDir)
        {
            bool resualt;
            try
            {
                targetDir = targetDir.EndsWith(@"\") ? targetDir : targetDir + @"\";
                var directoryInfo = new DirectoryInfo(targetDir);
                if (!directoryInfo.Exists)
                    directoryInfo.Create();
                var fileInfo = new FileInfo(zipFilePath);
                if (!fileInfo.Exists)
                    return false;
                using (var zipToOpen = new FileStream(zipFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                {
                    using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                    {
                        var count = archive.Entries.Count;
                        for (int i = 0; i < count; i++)
                        {
                            var entries = archive.Entries[i];
                            if (!entries.FullName.EndsWith("/"))
                            {
                                var entryFilePath = Regex.Replace(entries.FullName.Replace("/", @"\"), @"^\\*", "");
                                var filePath = directoryInfo + entryFilePath; //设置解压路径
                                UnZipEvent(this, new UnZipEventArgs { Size = entries.Length, Count = count, Index = i + 1, Path = entries.FullName, Name = entries.Name });
                                var content = new byte[entries.Length];
                                entries.Open().Read(content, 0, content.Length);
                                var greatFolder = Directory.GetParent(filePath);
                                if (!greatFolder.Exists)
                                    greatFolder.Create();
                                File.WriteAllBytes(filePath, content);
                            }
                        }
                    }
                }
                resualt = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resualt = false;
            }
            return resualt;
        }
        #endregion

        #region Unzip file
        /// <summary>
        /// unzio zip file，and replace to target directory
        /// </summary>
        /// <param name="zipFilePath">zip file path</param>
        /// <param name="targetDir">Tagert path</param>
        /// <returns></returns>
        public async Task<bool> UnZipAsync(string zipFilePath, string targetDir)
        {
            return await Task<bool>.Run(() =>
            {
                bool resualt;
                try
                {
                    targetDir = targetDir.EndsWith(@"\") ? targetDir : targetDir + @"\";
                    var directoryInfo = new DirectoryInfo(targetDir);
                    if (!directoryInfo.Exists)
                        directoryInfo.Create();
                    var fileInfo = new FileInfo(zipFilePath);
                    if (!fileInfo.Exists)
                        return false;
                    using (var zipToOpen = new FileStream(zipFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                    {
                        using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                        {
                            var count = archive.Entries.Count;
                            for (int i = 0; i < count; i++)
                            {
                                var entries = archive.Entries[i];
                                if (!entries.FullName.EndsWith("/"))
                                {
                                    var entryFilePath = Regex.Replace(entries.FullName.Replace("/", @"\"), @"^\\*", "");
                                    var filePath = directoryInfo + entryFilePath; //设置解压路径
                                    UnZipEvent(this, new UnZipEventArgs { Size = entries.Length, Count = count, Index = i + 1, Path = entries.FullName, Name = entries.Name });
                                    var content = new byte[entries.Length];
                                    entries.Open().Read(content, 0, content.Length);
                                    var greatFolder = Directory.GetParent(filePath);
                                    if (!greatFolder.Exists)
                                        greatFolder.Create();
                                    File.WriteAllBytes(filePath, content);
                                }
                            }
                        }
                    }
                    resualt = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    resualt = false;
                }
                return resualt;
            });
        }
        #endregion

    }
}
