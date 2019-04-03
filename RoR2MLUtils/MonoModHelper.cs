using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MonoMod;
using Mono.Cecil;
using MonoMod.RuntimeDetour.HookGen;
using System.Reflection;

namespace RoR2ML.Installer {

    //Helps us do some MonoMod stuff easier.
    public class MonoModHelper {

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

        public MonoModHelper(RoR2MLInstaller installer) {
            this.parentInstaller = installer;
        }

        /// <summary>
        /// Patches an assembly located in Managed.
        /// </summary>
        /// <param name="pathToPatch"></param>
        /// <param name="assemblyName"></param>
        public void PatchAssembly(string pathToPatch, string assemblyName) {

            string assemblyPath = FindAssemblyByName( assemblyName );
            if( assemblyPath == string.Empty )
                return;

            string outputPath = Path.Combine( Directory.GetParent( assemblyPath ).FullName, Path.GetFileNameWithoutExtension( assemblyPath ) + "-NEW.dll" );

            Console.WriteLine( assemblyPath );
            Console.WriteLine( outputPath );

            using( MonoModder mm = new MonoModder() {
                InputPath = assemblyPath,
                OutputPath = outputPath
            } ) {
                mm.LogVerboseEnabled = true;
                mm.Read();
                //Force everything to be public
                mm.PublicEverything = true;

                //Read in the patch
                mm.ReadMod( pathToPatch );

                mm.MapDependencies();
                mm.DependencyDirs.Add( Directory.GetParent( Assembly.GetExecutingAssembly().FullName ).FullName );

                mm.AutoPatch();

                mm.Write();
            }

            File.Delete( assemblyPath );
            File.Copy( outputPath, assemblyPath );
            File.Delete( outputPath );

        }

        /// <summary>
        /// Runs HookGen on an assembly located in Managed
        /// </summary>
        /// <param name="assemblyName"></param>
        public void RunHookGen(string assemblyName) {

            string assemblyPath = FindAssemblyByName( assemblyName );
            if( assemblyPath == string.Empty )
                return;

            string hooksPath = Path.Combine( Directory.GetParent( assemblyPath ).FullName, Path.GetFileNameWithoutExtension( assemblyPath ) + "-HOOKS.dll" );


            using( MonoModder mm = new MonoModder() {
                InputPath = assemblyPath,
                OutputPath = hooksPath,
                ReadingMode = ReadingMode.Deferred
            } ) {
                mm.Read();

                mm.MapDependencies();

                if( File.Exists( hooksPath ) ) {
                    File.Delete( hooksPath );
                }

                HookGenerator gen = new HookGenerator( mm, Path.GetFileName( hooksPath ) );
#if !CECIL0_9
                using( ModuleDefinition mOut = gen.OutputModule ) {
#else
                ModuleDefinition mOut = gen.OutputModule;
                {
#endif

                    gen.Generate();
                    mOut.Write( hooksPath );
                }
            }

        }

        private string FindAssemblyByName(string name) {
            string[] files = Directory.GetFiles( managedPath, name );

            if( files.Length > 1 ) {
                //LOG SOMETHING
            }

            if( files.Length == 0 )
                return string.Empty;

            return files[0];
        }

    }
}