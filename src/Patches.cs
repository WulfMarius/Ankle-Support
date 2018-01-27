using Harmony;

namespace AnkleSupport
{
    [HarmonyPatch(typeof(PlayerManager), "PutOnClothingItem")]
    internal class PlayerManager_PutOnClothingItem
    {
        private static void Postfix(GearItem gi)
        {
            if (gi.m_ClothingItem != null && gi.m_ClothingItem.m_Region == ClothingRegion.Feet)
            {
                AnkleSupport.UpdateAnkleSupport();
            }
        }
    }

    [HarmonyPatch(typeof(GameManager), "Awake")]
    internal class GameManager_Awake
    {
        private static void Postfix()
        {
            AnkleSupport.Initialize();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "TakeOffClothingItem")]
    internal class PlayerManager_TakeOffClothingItem
    {
        private static void Postfix(GearItem gi)
        {
            if (gi.m_ClothingItem != null && gi.m_ClothingItem.m_Region == ClothingRegion.Feet)
            {
                AnkleSupport.UpdateAnkleSupport();
            }
        }
    }
}