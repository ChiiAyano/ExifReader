namespace ExifReader.Models.Tags;

internal record GpsAltitudeTag : TagBase
{
    public double Altitude { get; }

    public GpsAltitudeTag(TagId tagId, RationalTag.Rational[] values)
    {
        this.TagId = tagId;

        var data = values[0];
        this.Altitude = (double)data.Numerator / data.Denominator;
        this.FormattedValue = $"{this.Altitude:0.00} m";
    }
}
