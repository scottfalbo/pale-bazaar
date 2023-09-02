using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.Tomes;

namespace PaleBazaar.MechanistTower.SpellChanters
{
    public class IllustrationChanters : IIllustrationChanters
    {
        private readonly IIllustrationsTome _illustrationsTome;
        private readonly IEchoKeeperChanter _echoKeeperChanter;

        public IllustrationChanters(IIllustrationsTome illustrationsTome, IEchoKeeperChanter echoKeeperChanter)
        {
            _illustrationsTome = illustrationsTome;
            _echoKeeperChanter = echoKeeperChanter;
        }

        public async Task<List<Illustration>> GetIllustrations()
        {
            var illustrations = await _illustrationsTome.GetIllustrationsAsync();

            return illustrations.ToList();
        }

        public async Task ImbueEcho(IFormFile[] files, string name, string altText)
        {
            foreach (var file in files)
            {
                var illustration = new Illustration()
                {
                    Name = name,
                    AltText = altText,
                };

                await _echoKeeperChanter.InscribeEcho(file, illustration);

                await _illustrationsTome.ImbueIllustrationAsync(illustration);
            }
        }

        public async Task ShatterEcho(string id, string partitionKey, string fileName, string thumbnailFileName)
        {
            await _echoKeeperChanter.BanishEcho(fileName);
            await _echoKeeperChanter.BanishEcho(thumbnailFileName);

            await _illustrationsTome.ShatterIllustrationAsync(id, partitionKey);
        }
    }
}