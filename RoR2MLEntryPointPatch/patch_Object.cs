using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine {
    public class patch_Object {

        public static extern void orig_cctor();

        [MonoMod.MonoModConstructor]
        public static void cctor() {
            orig_cctor();

            //Entry point
            Thundercell.ThunderCell.Initialize();
        }

    }
}
