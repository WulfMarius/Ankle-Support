using Harmony;

namespace AnkleSupport
{
    [HarmonyPatch(typeof(GameManager), "Awake")]
    internal class GameManager_Awake
    {
        private static void Postfix()
        {
            try
            {
                AnkleSupport.Initialize();
            }
            catch (System.Exception e)
            {
                HUDMessage.AddMessage("[Ankle-Support]: Could not initialize.");
                UnityEngine.Debug.LogException(e);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "PutOnClothingItem")]
    internal class PlayerManager_PutOnClothingItem
    {
        private static void Postfix(GearItem gi)
        {
            if (gi.m_ClothingItem != null && gi.m_ClothingItem.m_Region == ClothingRegion.Feet)
            {
                try
                {
                    AnkleSupport.UpdateAnkleSupport();
                }
                catch (System.Exception e)
                {
                    HUDMessage.AddMessage("[Ankle-Support]: Could not update ankle protection.");
                    UnityEngine.Debug.LogException(e);
                }
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "TakeOffClothingItem")]
    internal class PlayerManager_TakeOffClothingItem
    {
        private static void Postfix(GearItem gi)
        {
            if (gi.m_ClothingItem != null && gi.m_ClothingItem.m_Region == ClothingRegion.Feet)
            {
                try
                {
                    AnkleSupport.UpdateAnkleSupport();
                }
                catch (System.Exception e)
                {
                    HUDMessage.AddMessage("[Ankle-Support]: Could not update ankle protection.");
                    UnityEngine.Debug.LogException(e);
                }
            }
        }
    }
}