using PaleBazaar.MechanistTower.Entities;

namespace PaleBazaar.MechanistTower.SpellChanters
{
    public interface IEchoKeeperChanter
    {
        public Task InscribeEcho(IFormFile file, OculusEcho echo);

        public Task BanishEcho(string fileName);
    }
}