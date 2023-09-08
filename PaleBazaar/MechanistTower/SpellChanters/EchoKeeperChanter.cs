using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components.Forms;
using PaleBazaar.MechanistTower.Configuration;
using PaleBazaar.MechanistTower.Entities;
using PaleBazaar.MechanistTower.Manipulators;

namespace PaleBazaar.MechanistTower.SpellChanters
{
    public class EchoKeeperChanter : IEchoKeeperChanter
    {
        private readonly IConfigurationSigils _configurationSigils;
        private readonly IEchoShaper _echoShaper;

        public EchoKeeperChanter(
            IConfigurationSigils configurationSigils,
            IEchoShaper echoShaper)
        {
            _configurationSigils = configurationSigils;
            _echoShaper = echoShaper;
        }

        public async Task BanishEcho(string fileName)
        {
            var blobContainerClient = new BlobContainerClient(
                _configurationSigils.BlobConnectionString,
                _configurationSigils.BlobContainerName);

            var blob = blobContainerClient.GetBlobClient(fileName);

            await blob.DeleteIfExistsAsync(
                snapshotsOption: DeleteSnapshotsOption.IncludeSnapshots,
                conditions: null,
                cancellationToken: default);
        }

        public async Task InscribeEcho(IBrowserFile file, Echo oculusEcho)
        {
            var echoFileName = _echoShaper.AugmentRunicNaming(file.Name);
            var reshapedEcho = _echoShaper.ShapeEcho(file, 1920);
            var contentType = file.ContentType;

            var echo = await InscribeEcho(reshapedEcho, echoFileName, contentType);

            var faintFileName = $"thumb_{echoFileName}";
            var faintReshapedEcho = _echoShaper.ShapeEcho(file, 177, 100);

            var faintEcho = await InscribeEcho(faintReshapedEcho, faintFileName, contentType);

            oculusEcho.ImageUrl = echo.Uri.ToString();
            oculusEcho.ThumbnailUrl = faintEcho.Uri.ToString();
            oculusEcho.FileName = echoFileName;
            oculusEcho.ThumbnailFileName = faintFileName;
        }

        private async Task<BlobClient> InscribeEcho(Stream stream, string fileName, string contentType)
        {
            var blobContainerClient = new BlobContainerClient(
                _configurationSigils.BlobConnectionString,
                _configurationSigils.BlobContainerName);

            if (blobContainerClient == null)
            {
                await blobContainerClient.CreateIfNotExistsAsync();
            }

            var blob = blobContainerClient.GetBlobClient(fileName);

            var options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders()
                {
                    ContentType = contentType
                }
            };

            await blob.UploadAsync(stream, options);
            return blob;
        }
    }
}