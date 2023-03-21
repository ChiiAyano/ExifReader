using ExifReader.Models.Enums;

namespace ExifReader.Models.Tags;

internal record GpsAltitudeRefTag : TagBase
{
    public GpsAltitudeRef? Value { get; }

    public GpsAltitudeRefTag(TagId tagId, int[]? data)
    {
        this.TagId = tagId;

        if (data is null || !data.Any())
        {
            this.Value = null;
        }
        else
        {
            this.Value = (GpsAltitudeRef)data[0];
        }

        this.FormattedValue = this.Value?.ToString();
    }
}