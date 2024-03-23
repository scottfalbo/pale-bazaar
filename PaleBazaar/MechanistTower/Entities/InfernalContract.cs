// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PaleBazaar.MechanistTower.Entities;

public class InfernalContract
{
    [Required]
    [JsonProperty(PropertyName = "altText")]
    public string AltText { get; set; }

    [JsonProperty(PropertyName = "createdDateTime")]
    public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

    [Required]
    [JsonProperty(PropertyName = "display")]
    public bool Display { get; set; }

    [Required]
    [JsonProperty(PropertyName = "eternalSymbol")]
    public string EternalSymbol { get; set; }

    [JsonProperty(PropertyName = "FileName")]
    public string FileName { get; set; }

    [Required]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "imageUrl")]
    public string ImageUrl { get; set; }

    [JsonProperty(PropertyName = "isForSale")]
    public bool IsForSale { get; set; }

    [JsonProperty(PropertyName = "medium")]
    public string Medium { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [Required]
    [JsonProperty(PropertyName = "partitionKey")]
    public string PartitionKey { get; set; }

    [JsonProperty(PropertyName = "price")]
    public decimal Price { get; set; }

    [JsonProperty(PropertyName = "size")]
    public string Size { get; set; }

    [JsonProperty(PropertyName = "thumbnailFileName")]
    public string ThumbnailFileName { get; set; }

    [JsonProperty(PropertyName = "thumbnailUrl")]
    public string ThumbnailUrl { get; set; }
}