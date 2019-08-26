using System.Reflection;
using UnityEngine;

namespace AnkleSupport
{
    public class Implementation
    {
        public const string NAME = "Ankle-Support";

        private const float TOUGHNESS_FACTOR = 2f;

        private static readonly FieldInfo SPRAINS_MOVE_CHANCE = Harmony.AccessTools.Field(typeof(Sprains), "m_BaseChanceWhenMovingOnSlope");

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

            Sprains sprains = GameManager.GetSprainsComponent();
            defaultMoveChance = (float) SPRAINS_MOVE_CHANCE.GetValue(sprains);
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
            float toughness = GetShoesToughness();
            float moveChance = defaultMoveChance - toughness * TOUGHNESS_FACTOR;
            float fallChance = defaultFallChance - toughness * TOUGHNESS_FACTOR;

            Sprains sprains = GameManager.GetSprainsComponent();
            SprainedAnkle sprainedAnkle = GameManager.GetSprainedAnkleComponent();

            SPRAINS_MOVE_CHANCE.SetValue(sprains, moveChance);
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