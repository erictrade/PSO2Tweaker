﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

namespace ArksLayer.Tweaker.Abstractions
{
    /// <summary>
    /// Implementation of Tweaker Settings interface against Windows Registry.
    /// </summary>
    public class RegistryTweakerSettings : ITweakerSettings
    {
        /// <summary>
        /// Registry key for value containing the version of English Large Patch.
        /// </summary>
        private const string EnglishLargePatchVersionKey = "LargeFilesVersion";

        /// <summary>
        /// Registry key for value containing the version of English Patch.
        /// </summary>
        private const string EnglishPatchVersionKey = "ENPatchVersion";

        /// <summary>
        /// Registry key for value containing directory path to pso2_bin.
        /// </summary>
        private const string GameDirectoryKey = "PSO2Dir";

        /// <summary>
        /// Registry key for value containing the version of Story Patch.
        /// </summary>
        private const string StoryPatchVersionKey = "StoryPatchVersion";

        /// <summary>
        /// Constructs an instance of the class, pointing to parameter registry path for HKEY_CURRENT_USER.
        /// </summary>
        /// <param name="path"></param>
        public RegistryTweakerSettings(string path = @"Software\AIDA")
        {
            this.Root = Registry.CurrentUser.CreateSubKey(path);
        }
        /// <summary>
        /// Sets or gets value of the directory path to the English Large Patch version.
        /// </summary>
        public string EnglishLargePatchVersion
        {
            get
            {
                return (string)Root.GetValue(EnglishLargePatchVersionKey);
            }
            set
            {
                Root.SetValue(EnglishLargePatchVersionKey, value.Trim());
            }
        }

        /// <summary>
        /// Sets or gets value of the directory path to the English Patch version.
        /// </summary>
        public string EnglishPatchVersion
        {
            get
            {
                return (string)Root.GetValue(EnglishPatchVersionKey);
            }
            set
            {
                Root.SetValue(EnglishPatchVersionKey, value.Trim());
            }
        }

        /// <summary>
        /// Sets or gets value of the directory path to pso2_bin.
        /// Expected result example: D:\PSO2\pso2_bin
        /// </summary>
        public string GameDirectory
        {
            get
            {
                var value = (string)Root.GetValue(GameDirectoryKey);
                return value.TrimEnd('\\');
            }
            set
            {
                Root.SetValue(GameDirectoryKey, value.TrimEnd('\\'));
            }
        }
        /// <summary>
        /// Using this property, you can manipulate Windows Registry for Tweaker directly.
        /// </summary>
        public RegistryKey Root { get; private set; }

        /// <summary>
        /// Sets or gets value of the directory path to the Story Patch version.
        /// </summary>
        public string StoryPatchVersion
        {
            get
            {
                return (string)Root.GetValue(StoryPatchVersionKey);
            }
            set
            {
                Root.SetValue(StoryPatchVersionKey, value.Trim());
            }
        }

        /// <summary>
        /// Gets the value of the PSO2 user profile folder.
        /// </summary>
        public string UserFolder
        {
            get
            {
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                return Path.Combine(documents, @"Documents\SEGA\PHANTASYSTARONLINE2");
            }
        }

        /// <summary>
        /// Sets or gets the value of the game version file.
        /// </summary>
        public string GameVersionFile
        {
            get
            {
                return Path.Combine(UserFolder, "version.ver");
            }
        }

        /// <summary>
        /// A lock for thread-safe game version file IO.
        /// </summary>
        private static object GameVersionFileLock = new object();

        /// <summary>
        /// Sets or gets the value of the game client version.
        /// </summary>
        public string GameVersion
        {
            set
            {
                lock(GameVersionFileLock)
                {
                    File.WriteAllText(GameVersionFile, value);
                }
            }
            get
            {
                lock (GameVersionFileLock)
                {
                    return File.ReadAllText(GameVersionFile);
                }
            }
        }
    }
}
