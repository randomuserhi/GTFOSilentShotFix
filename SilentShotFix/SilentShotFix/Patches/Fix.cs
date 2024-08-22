using Gear;
using HarmonyLib;
using SNetwork;

namespace BioScannerFix {
    [HarmonyPatch]
    internal static class Fix {
        private static ItemEquippable? oldItem;

        [HarmonyPatch(typeof(PlayerInventorySynced), nameof(PlayerInventorySynced.GetSync))]
        [HarmonyPrefix]
        private static void Prefix_ShotSync(PlayerInventorySynced __instance) {
            if (!SNet.IsMaster || __instance.Owner.Owner.IsBot) return;

            if (__instance.m_queuedEquipItem != null) {
                ItemEquippable item = __instance.m_queuedEquipItem;

                BulletWeaponSynced? bulletWeapon = item.TryCast<BulletWeaponSynced>();
                if (bulletWeapon != null) {
                    bulletWeapon.Fire();
                }
            }
        }
    }
}
