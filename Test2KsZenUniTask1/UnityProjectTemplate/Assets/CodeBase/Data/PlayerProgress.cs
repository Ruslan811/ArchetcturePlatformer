using CodeBase.Services.WalletService;
using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public bool PrivatePolicyAccepted;
        public bool GDPRPolicyAccepted;
        public WalletsData WalletsData;
        public WorldData WorldData;
        public State HeroState;
        public KillData KillData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new State();
            KillData = new KillData();
        }
    }
}
