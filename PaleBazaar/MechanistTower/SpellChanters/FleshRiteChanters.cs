using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.Entities.EternalSymbols;
using PaleBazaar.MechanistTower.Tomes;

namespace PaleBazaar.MechanistTower.SpellChanters
{
    public class FleshRiteChanters : IFleshRiteChanters
    {
        private readonly IEchoesTome _echoesTome;
        private readonly IEchoKeeperChanter _echoKeeperChanter;

        public FleshRiteChanters(IEchoesTome echoesTome, IEchoKeeperChanter echoKeeperChanter)
        {
            _echoesTome = echoesTome;
            _echoKeeperChanter = echoKeeperChanter;
        }

        public async Task<List<Echo>> GetFleshRites()
        {
            var fleshRites = await _echoesTome.GetEchoesAsync(OculusEchoCyphers.FleshRite);

            return fleshRites.ToList();
        }

        public async Task ImbueEcho(IFormFile[] files, string name, string altText)
        {
            foreach (var file in files)
            {
                var fleshRite = new Echo(OculusEchoCyphers.FleshRite)
                {
                    Name = name,
                    AltText = altText,
                };

                await _echoKeeperChanter.InscribeEcho(file, fleshRite);

                await _echoesTome.ImbueEchoAsync(fleshRite);
            }
        }

        public async Task ShatterEcho(string id, string partitionKey, string fileName, string thumbnailFileName)
        {
            await _echoKeeperChanter.BanishEcho(fileName);
            await _echoKeeperChanter.BanishEcho(thumbnailFileName);

            await _echoesTome.ShatterEchoAsync(id, partitionKey);
        }
    }
}