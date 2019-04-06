using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;

namespace Thundercell {
    public class ThundercellModManager {

        public List<ThundercellMod> allMods = new List<ThundercellMod>();

        Type thundercellModTypeRef;

        public void LoadMods() {

            if( thundercellModTypeRef == null ) {
                Type t = typeof( ThundercellMod );
            }

            string pathToMainFolder = Directory.GetParent( UnityEngine.Application.dataPath ).FullName;

            string[] dlls = Directory.GetFiles( "*.dll" );


            foreach( string s in dlls ) {
                Assembly assembly = Assembly.LoadFile( s );

                foreach( Module md in assembly.Modules ) {
                    foreach( Type td in md.GetTypes() ) {
                        CheckAndCreateThundercellModInstance( td );
                    }
                }
            }

            allMods.Add( new TestMod() );

            foreach( ThundercellMod mod in allMods ) {
                mod.LoadExtraModData();
            }

            foreach( ThundercellMod mod in allMods ) {
                mod.EnableHooks();
            }

        }

        public void CheckAndCreateThundercellModInstance(Type td) {

            if( td.BaseType == thundercellModTypeRef ) {
                ThundercellMod newMod = Activator.CreateInstance( td, false ) as ThundercellMod;

                newMod.InitializeMod();
                allMods.Add( newMod );
            }

        }

    }
}