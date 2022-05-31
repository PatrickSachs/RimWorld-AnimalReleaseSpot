using HarmonyLib;
using JetBrains.Annotations;
using Lilith.RimWorld.AnimalReleaseSpot.HarmonyPatches;
using Verse;

namespace Lilith.RimWorld.AnimalReleaseSpot {
    [UsedImplicitly]
    public class AnimalReleaseSpotMod : Mod {
        public static Harmony Harm;
        
        public AnimalReleaseSpotMod(ModContentPack content) : base(content) {
            Harm = new Harmony("lilith.animalreleasespot");
            #if DEBUG
            Harmony.DEBUG = true;
            #endif
            Harm.PatchAll();
            JobDriver_ReleaseAnimalToWild_MakeNewToils.Harmony(Harm);
        }
    }
}