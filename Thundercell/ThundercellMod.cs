using System;
using System.Collections.Generic;
using System.Text;

namespace Thundercell {
    public class ThundercellMod {

        public string modID = "MODID";
        public string version = "0.0.0.0";

        public int loadPriority = 0;
        public int hookPriority = 0;

        /// <summary>
        /// Set the mod ID and version here
        /// </summary>
        public virtual void InitializeMod() {}

        /// <summary>
        /// Loads extra mod data from outside the .dll file.
        /// </summary>
        public virtual void LoadExtraModData() {}

        public virtual void EnableHooks() {}
        public virtual void DisableHooks() { }
    }
}