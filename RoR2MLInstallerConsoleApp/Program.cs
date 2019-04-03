using System;

namespace RoR2MLInstallerConsoleApp {
    class Program {
        static void Main(string[] args) {
            RoR2ML.Installer.RoR2MLInstaller installer = new RoR2ML.Installer.RoR2MLInstaller();


            installer.SetGamePath(Console.ReadLine());
            installer.InstallPatch(Console.ReadLine());
            installer.RunHookGen();
        }
    }
}
