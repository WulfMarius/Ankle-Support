using Harmony;

namespace AnkleSupport
{
    [HarmonyPatch(typeof(GameManager), "Awake")]
    internal static class GameManager_Awake
    {
        private static void Postfix()
        {
            Implementation.Initialize();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "PutOnClothingItem")]
    internal static class PlayerManager_PutOnClothingItem
    {
        private static void Postfix(GearItem gi)
        {
            if (gi.m_ClothingItem != null && gi.m_ClothingItem.m_Region == ClothingRegion.Feet)
            {
                Implementation.UpdateAnkleSupport();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "TakeOffClothingItem")]
    internal static class PlayerManager_TakeOffClothingItem
    {
        private static void Postfix(GearItem gi)
        {
            if (gi.m_ClothingItem != null && gi.m_ClothingItem.m_Region == ClothingRegion.Feet)
            {
                Implementation.UpdateAnkleSupport();
            }
        }
    }
}
