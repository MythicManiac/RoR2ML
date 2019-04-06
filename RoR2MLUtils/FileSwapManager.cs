using RoR2ML.Installer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RoR2ML.Installer {
    public class FileSwapManager {

        public static readonly string SECONDARY_SET_NAME = "secondaryFileset.txt";

        public RoR2MLInstaller parentInstaller;

        //Location to the folder the .exe is located in.
        public string currentGamePath {
            get {
                return parentInstaller.currentGamePath;
            }
        }
        //Location to the Managed folder
        public string managedPath {
            get {
                return parentInstaller.managedPath;
            }
        }

        public FileSwapManager(RoR2MLInstaller installer) {
            this.parentInstaller = installer;
        }

        /// <summary>
        /// This swaps the game files out for the modded files, or vice,versa. Uses a single empty file as a "flag" for when the game has crashed, too.
        /// </summary>
        public void SwapFiles(bool swapIn) {
            string swapFolder = managedPath + "_SWAP";


            //If there is no directory to swap from, create it and give it the flag file
            if( !Directory.Exists( swapFolder ) ) {
                InstallerUtils.DirectoryCopy( managedPath, swapFolder, true );
                File.Create( Path.Combine( swapFolder, SECONDARY_SET_NAME ) ).Close();
            }

            //If we're swapping the mod files into the game but the SECONDARY_SET_NAME already exists, we skip this.
            //If we're swapping out the mod files and SECONDARY_SET_NAME already exists, we skip this.
            if( swapIn == File.Exists( Path.Combine( managedPath, SECONDARY_SET_NAME ) ) ) {
                return;
            }

            //If we're swapping and the flag file doesn't exist, swap the folders.
            Directory.Move( swapFolder, swapFolder + "_TMP" );
            Directory.Move( managedPath, swapFolder );
            Directory.Move( swapFolder + "_TMP", managedPath );
        }

    }
}