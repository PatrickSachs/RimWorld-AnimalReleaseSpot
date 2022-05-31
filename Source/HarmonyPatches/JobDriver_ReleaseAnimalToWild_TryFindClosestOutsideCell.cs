using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace Lilith.RimWorld.AnimalReleaseSpot.HarmonyPatches {
    /// <summary>
    /// Overrides the position for animal release with our own if the map contains an animal release spot.
    /// </summary>
    [HarmonyPatch(typeof(JobDriver_ReleaseAnimalToWild))]
    [HarmonyPatch(nameof(JobDriver_ReleaseAnimalToWild.TryFindClosestOutsideCell))]
    [UsedImplicitly]
    public static class JobDriver_ReleaseAnimalToWild_TryFindClosestOutsideCell {
        [UsedImplicitly]
        public static bool Prefix(Map map, ref IntVec3 cell, ref bool __result) {
            var spot = AnimalReleaseSpotBuilding.FindInMap(map);
            #if DEBUG
            Log.Message("[JobDriver_ReleaseAnimalToWild_TryFindClosestOutsideCell] Spot is: " + spot);
            #endif
            if (spot == null) {
                return true;
            }
            #if DEBUG
            Log.Message("[JobDriver_ReleaseAnimalToWild_TryFindClosestOutsideCell] Position is: " + spot.Position);
            #endif

            cell = spot.Position;
            //cell.GetDistrict()
            __result = true;
            return false;
        }
    }
}