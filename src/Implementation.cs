using MelonLoader;
using UnityEngine;

namespace AnkleSupport
{
    public class Implementation : MelonMod
    {
        public const string NAME = "Ankle-Support";

        private const float TOUGHNESS_FACTOR = 2f;

        private static float chanceOfWristSprainWhenMoving;
        private static float ankleBaseFallChance;

        private static float ankleMoveChanceReduction;

        public override void OnApplicationStart()
        {
            Debug.Log($"[{InfoAttribute.Name}] version {InfoAttribute.Version} loaded!");
        }

        internal static void Initialize()
        {
            SprainedAnkle sprainedAnkle = GameManager.GetSprainedAnkleComponent();
            Sprains sprains = GameManager.GetSprainsComponent();

            chanceOfWristSprainWhenMoving = sprains.m_ChanceOfWristSprainWhenMoving;
            ankleBaseFallChance = sprainedAnkle.m_ChanceSprainAfterFall;

            ankleMoveChanceReduction = 0;
        }

        internal static void UpdateAnkleSupport()
        {
            float toughness = GetShoesToughness();
            float fallChance = ankleBaseFallChance - toughness * TOUGHNESS_FACTOR;

            ankleMoveChanceReduction = toughness * TOUGHNESS_FACTOR;
            GameManager.GetSprainedAnkleComponent().m_ChanceSprainAfterFall = fallChance;
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

        internal static bool ShouldRollForWristSprain()
        {
            return Utils.RollChance(chanceOfWristSprainWhenMoving);
        }

        internal static void AdjustAnkleSprainMoveChance(ref float sprainChance)
        {
            sprainChance -= ankleMoveChanceReduction;
        }
    }
}
