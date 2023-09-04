using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.Entities.EternalSymbols;
using PaleBazaar.MechanistTower.Tomes;

namespace PaleBazaar.MechanistTower.SpellChanters
{
    public class IllustrationChanters : IIllustrationChanters
    {
        private readonly IEchoesTome _echoesTome;
        private readonly IEchoKeeperChanter _echoKeeperChanter;

        public IllustrationChanters(IEchoesTome echoesTome, IEchoKeeperChanter echoKeeperChanter)
        {
            _echoesTome = echoesTome;
            _echoKeeperChanter = echoKeeperChanter;
        }

        public async Task<List<Echo>> GetIllustrations()
        {
            var illustrations = await _echoesTome.GetEchoesAsync(OculusEchoCyphers.Illustration);

            return illustrations.ToList();
        }

        public async Task ImbueEcho(IFormFile[] files, string name, string altText)
        {
            foreach (var file in files)
            {
                var illustration = new Echo(OculusEchoCyphers.Illustration)
                {
                    Name = name,
                    AltText = altText,
                };

                await _echoKeeperChanter.InscribeEcho(file, illustration);

                await _echoesTome.ImbueEchoAsync(illustration);
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