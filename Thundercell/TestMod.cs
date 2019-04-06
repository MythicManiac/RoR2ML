using System;
using System.Collections.Generic;
using System.Text;

namespace Thundercell {
    class TestMod : ThundercellMod{

        public override void InitializeMod() {
            base.InitializeMod();

            modID = "TEST";
        }

        public override void EnableHooks() {
            base.EnableHooks();

            On.RoR2.RoR2Application.Awake += RoR2Application_Awake;
        }

        private void RoR2Application_Awake(On.RoR2.RoR2Application.orig_Awake orig, RoR2.RoR2Application self) {
            orig( self );
            UnityEngine.Debug.Log("STUFF IS HAPPENING");
        }
    }
}