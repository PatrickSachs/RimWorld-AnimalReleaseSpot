using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Lilith.RimWorld.AnimalReleaseSpot.HarmonyPatches {
    /// <summary>
    /// The job checks if our district touches the map edge. However, if we have a release spot the district needs to be
    /// the same as the release spot.
    /// </summary>
    public static class JobDriver_ReleaseAnimalToWild_MakeNewToils {
        public static void Harmony(Harmony harm) {
            var method = typeof(JobDriver_ReleaseAnimalToWild)
                .GetMethod("<MakeNewToils>b__6_4", BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) {
                Log.Error("[JobDriver_ReleaseAnimalToWild_MakeNewToils] Could not find toil method, releasing " +
                          "animals in districts not touching the map edge will NOT WORK.");
            }
            #if DEBUG
            Log.Message("[JobDriver_ReleaseAnimalToWild_MakeNewToils] Found method: " + method + 
                        " - instance: " + method?.DeclaringType + 
                        " - parameters: " + method?.GetParameters().Select(p => p.ToString()).Join() + 
                        " - return: " + method?.ReturnType);
            #endif
            // ReSharper disable once ArgumentsStyleOther
            harm.Patch(method, prefix: new HarmonyMethod(typeof(JobDriver_ReleaseAnimalToWild_MakeNewToils), nameof(Prefix)));
        }
        
        public static bool Prefix(JobDriver_ReleaseAnimalToWild __instance, ref bool __result) {
            #if DEBUG
            Log.Message("[JobDriver_ReleaseAnimalToWild_MakeNewToils] Instance: " + __instance);
            #endif
            var animal = __instance.job.targetA.Thing;
            var spot = AnimalReleaseSpotBuilding.FindInMap(animal?.Map);
            
            #if DEBUG
            Log.Message("[JobDriver_ReleaseAnimalToWild_MakeNewToils] Spot is: " + spot);
            #endif
            if (spot == null) {
                return true;
            }

            var district = spot.GetDistrict();
            var animalDistrict = animal.GetDistrict();
            #if DEBUG
            Log.Message("[JobDriver_ReleaseAnimalToWild_MakeNewToils] District is: " + spot.GetDistrict() + " - animal district is: " + animalDistrict);
            #endif

            __result = district == animalDistrict;
            return false;
        }
    }
}