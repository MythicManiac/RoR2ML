using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2ML.Installer {
    public class BackupManager {

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

        public BackupManager(RoR2MLInstaller installer) {
            this.parentInstaller = installer;
        }

        public void RestoreOrCreateBackup(int backupSlot) {
            if( DoesBackupExist( backupSlot ) ) {
                RestoreGameBackups( backupSlot );
            } else {
                CreateBackupFromGameFiles( backupSlot );
            }
        }

        public bool DoesBackupExist(int backupSlot) {
            string backupPath = Path.Combine( currentGamePath, "Risk of Rain 2_Data", "Managed_Backups", "Backup_" + backupSlot.ToString() );
            return Directory.Exists( backupPath );
        }

        public void CreateBackupFromGameFiles(int backupSlot) {
            //TODO - Add log here
            if( currentGamePath == string.Empty ) {
                return;
            }

            string backupPath = Path.Combine( currentGamePath, "Risk of Rain 2_Data", "Managed_Backups", "Backup_" + backupSlot.ToString() );

            if( !Directory.Exists( backupPath ) ) {
                Directory.CreateDirectory( backupPath );
            }

            InstallerUtils.DirectoryCopy( managedPath, backupPath, true );
        }
        public void RestoreGameBackups(int backupSlot) {
            //TODO - Add log here
            if( currentGamePath == string.Empty ) {
                return;
            }

            string backupPath = Path.Combine( currentGamePath, "Risk of Rain 2_Data", "Managed_Backups", "Backup_" + backupSlot.ToString() );

            if( !Directory.Exists( backupPath ) ) {
                return;
            }

            Directory.Delete( managedPath, true );
            InstallerUtils.DirectoryCopy( backupPath, managedPath, true );
        }

    }
}