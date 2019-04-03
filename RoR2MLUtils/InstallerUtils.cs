using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2ML.Installer {
    public class InstallerUtils {

        public const string GAME_DIR_NAME = "Risk of Rain 2";
        public const string LOADER_ASSEMBLY_NAME = "RoR2ML.dll";

        public const string ASSEMBLY_ENTRY_POINT_TYPE = "RoR2.UI.MainMenu.MainMenuController";
        public const string ASSEMBLY_ENTRY_POINT_METHOD = "Start";

        public const string LOADER_ENTRY_POINT_TYPE = "RoR2ML.Loader";
        public const string LOADER_ENTRY_POINT_METHOD = "Init";

        public static bool canOpenFile(FileInfo file) {
            return canOpenFile( file.FullName );
        }
        public static bool canOpenFile(string filePath) {
            try {
                using( FileStream stream = File.Open( filePath, FileMode.Open, FileAccess.ReadWrite ) ) {
                    return true;
                }
            } catch( Exception ) {
                // TODO: Log exception
                return false;
            }
        }

        public static string getLoaderFilePath() {
            // TODO: Don't assume loader DLL is in same directory as installer
            return AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + LOADER_ASSEMBLY_NAME;
        }

        public static string getUserInput(string query) {
            Console.WriteLine( query );
            return Console.ReadLine();
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs) {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo( sourceDirName );

            if( !dir.Exists ) {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName );
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if( !Directory.Exists( destDirName ) ) {
                Directory.CreateDirectory( destDirName );
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach( FileInfo file in files ) {
                string temppath = Path.Combine( destDirName, file.Name );
                file.CopyTo( temppath, false );
            }

            // If copying subdirectories, copy them and their contents to new location.
            if( copySubDirs ) {
                foreach( DirectoryInfo subdir in dirs ) {
                    string temppath = Path.Combine( destDirName, subdir.Name );
                    DirectoryCopy( subdir.FullName, temppath, copySubDirs );
                }
            }
        }
    }
}
