using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MonoMod;

namespace RoR2ML.Installer {
    public class RoR2MLInstaller {

        #region Components
        public MonoModHelper monomodHelper;
        public FileSwapManager swapManager;
        #endregion

        #region Paths
        public int currentBackupSlot = 0;

        public string currentGamePath;
        public string managedPath;
        #endregion


        public RoR2MLInstaller() {
            monomodHelper = new MonoModHelper( this );
            swapManager = new FileSwapManager( this );
        }

        //Sets the path for the game
        public void SetGamePath(string path) {
            currentGamePath = path;

            managedPath = Path.Combine( currentGamePath, "Risk of Rain 2_Data", "Managed" );
        }

        public void InstallDefaultPatch() {
            string localDirectory = Directory.GetParent( Assembly.GetEntryAssembly().FullName ).FullName;
            string pathToPatch = Path.Combine( localDirectory, "RoR2MLEntryPointPatch.dll" );
            InstallPatch( pathToPatch, new string[] { Path.Combine( localDirectory, "Thundercell.dll" ) } , "UnityEngine.CoreModule.dll");
        }

        public void InstallPatch(string pathToPatch, string assemblyToPatch = "Assembly-CSharp.dll", bool restoreOrMakeBackup = true) {
            if( currentGamePath == string.Empty )
                return;

            monomodHelper.PatchAssembly( pathToPatch, assemblyToPatch );
        }
        public void InstallPatch(string pathToPatch, string[] dependencies, string assemblyToPatch = "Assembly-CSharp.dll", bool restoreOrMakeBackup = true) {
            if( currentGamePath == string.Empty )
                return;

            swapManager.SwapFiles(true);
            monomodHelper.PatchAssembly( pathToPatch, assemblyToPatch, dependencies );
        }

        public void RunHookGen(string assemblyToGenerateFor = "Assembly-CSharp.dll", bool restoreOrMakeBackup = true) {
            if( currentGamePath == string.Empty )
                return;

            monomodHelper.RunHookGen( assemblyToGenerateFor );
        }

    }
}