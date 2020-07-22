using MelonLoader;
using UnityEngine;

namespace AnkleSupport
{
    public class Implementation : MelonMod
    {
        public const string NAME = "Ankle-Support";

        private const float TOUGHNESS_FACTOR = 2f;

        private static float defaultFallChance;
        private static float defaultMoveChance;

        public override void OnApplicationStart()
        {
            Debug.Log($"[{InfoAttribute.Name}] version {InfoAttribute.Version} loaded!");
        }

        internal static void Initialize()
        {
            SprainedAnkle sprainedAnkle = GameManager.GetSprainedAnkleComponent();
            Sprains sprains = GameManager.GetSprainsComponent();

            defaultMoveChance = sprains.m_BaseChanceWhenMovingOnSlope;
            defaultFallChance = sprainedAnkle.m_ChanceSprainAfterFall;
        }

        internal static void UpdateAnkleSupport()
        {
            float toughness = GetShoesToughness();
            float moveChance = defaultMoveChance - toughness * TOUGHNESS_FACTOR;
            float fallChance = defaultFallChance - toughness * TOUGHNESS_FACTOR;

            Sprains sprains = GameManager.GetSprainsComponent();
            SprainedAnkle sprainedAnkle = GameManager.GetSprainedAnkleComponent();

            sprains.m_BaseChanceWhenMovingOnSlope = moveChance;
            sprainedAnkle.m_ChanceSprainAfterFall = fallChance;
        }

        private static float GetShoesToughness()
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();

            GearItem gearItem = playerManager.GetClothingInSlot(ClothingRegion.Feet, ClothingLayer.Top);
            if (gearItem == null || gearItem.m_ClothingItem == null)
            {
                return 0;
            }

            return gearItem.m_ClothingItem.m_Toughness;
        }
    }
}
