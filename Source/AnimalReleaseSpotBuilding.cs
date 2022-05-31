using System.Linq;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace Lilith.RimWorld.AnimalReleaseSpot {
    public class AnimalReleaseSpotBuilding : Building {
        public AnimalReleaseSpotBuilding() {
            var old = FindInMap(Current.Game.CurrentMap);
            if (old != null) {
                old.Destroy(DestroyMode.Vanish);
                Messages.Message("Lilith_AnimalReleaseSpot_AlreadyOnMap".Translate(), MessageTypeDefOf.NegativeEvent, true);
            }
        }

        [CanBeNull]
        public static Building FindInMap([CanBeNull] Map map) {
            return map?.listerBuildings.allBuildingsColonist
                      .FirstOrDefault(building => building.def.defName.Equals("Lilith_AnimalReleaseSpot_Building"));
        }
    }
}