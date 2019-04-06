using System;
using System.Collections.Generic;
using System.Text;

namespace Thundercell {
    public class ThunderCell {

        public static ThunderCell instance;

        public string gamePath;

        public ThundercellModManager modManager;


        public static void Initialize() {
            instance = new ThunderCell();
        }

        public ThunderCell() {
            //Create mod manager
            modManager = new ThundercellModManager();


        }

    }
}