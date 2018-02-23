using UnityEngine;

namespace AnkleSupport
{
    internal class AnkleSupport
    {
        private static readonly float TOUGHNESS_FACTOR = 2f;

        private static float defaultFallChance;
        private static float defaultMoveChance;

        public static void OnLoad()
        {
            Debug.Log("[Ankle-Support]: Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
        }

        internal static void Initialize()
        {
            SprainedAnkle sprainedAnkle = GameManager.GetSprainedAnkleComponent();

            defaultMoveChance = sprainedAnkle.m_BaseChanceWhenMovingOnSlope;
            defaultFallChance = sprainedAnkle.m_ChanceSprainAfterFall;
        }

        internal static void UpdateAnkleSupport()
        {
            float toughness = getShoesToughness();

            SprainedAnkle sprainedAnkle = GameManager.GetSprainedAnkleComponent();

            sprainedAnkle.m_BaseChanceWhenMovingOnSlope = defaultMoveChance - toughness * TOUGHNESS_FACTOR;
            sprainedAnkle.m_ChanceSprainAfterFall = defaultFallChance - toughness * TOUGHNESS_FACTOR;
        }

        private static float getShoesToughness()
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