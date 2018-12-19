using System.Reflection;
using UnityEngine;

namespace AnkleSupport
{
    public class Implementation
    {
        public const string NAME = "Ankle-Support";

        private const float TOUGHNESS_FACTOR = 2f;

        private static float defaultFallChance;
        private static float defaultMoveChance;

        public static void OnLoad()
        {
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
            Log("Version " + assemblyName.Version);
        }

        internal static void Initialize()
        {
            SprainedAnkle sprainedAnkle = GameManager.GetSprainedAnkleComponent();

            defaultMoveChance = sprainedAnkle.m_BaseChanceWhenMovingOnSlope;
            defaultFallChance = sprainedAnkle.m_ChanceSprainAfterFall;
        }

        internal static void Log(string message)
        {
            Debug.LogFormat("[" + NAME + "] {0}", message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format("[" + NAME + "] {0}", message);
            Debug.LogFormat(preformattedMessage, parameters);
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