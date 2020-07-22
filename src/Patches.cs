using Harmony;
using UnityEngine;

namespace AnkleSupport
{
    [HarmonyPatch(typeof(GameManager), "Awake")]
    internal class GameManager_Awake
    {
        private static void Postfix()
        {
            try
            {
                Implementation.Initialize();
            }
            catch (System.Exception e)
            {
                HUDMessage.AddMessage("[" + Implementation.NAME + "] Could not initialize.");
                Debug.Log(e.ToString());
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
                    Implementation.UpdateAnkleSupport();
                }
                catch (System.Exception e)
                {
                    HUDMessage.AddMessage("[" + Implementation.NAME + "] Could not update ankle protection.");
                    Debug.Log(e.ToString());
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
                    Implementation.UpdateAnkleSupport();
                }
                catch (System.Exception e)
                {
                    HUDMessage.AddMessage("[" + Implementation.NAME + "] Could not update ankle protection.");
                    Debug.Log(e.ToString());
                }
            }
        }
    }
}
