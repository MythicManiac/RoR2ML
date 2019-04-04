using System;

namespace RoR2MLInstallerConsoleApp {
    class Program {

        static void Main(string[] args) {
            RoR2ML.Installer.RoR2MLInstaller installer = new RoR2ML.Installer.RoR2MLInstaller();


            Console.WriteLine( "Please enter the path to the game's main folder (the one with the .exe)" );
            installer.SetGamePath(Console.ReadLine());
            Console.WriteLine( "Please enter the path to the patch you want to apply (enter nothing for default patch)" );

            string input = Console.ReadLine();

            if( input.Length == 0 ) {
                installer.InstallDefaultPatch();
            } else {
                installer.InstallPatch(input);
            }
            installer.RunHookGen();
        }
    }
}
