namespace ExifReader.Models.Tags;

internal record ExposureBiasValueTag : TagBase
{
    /// <summary>
    /// 露出補正値
    /// </summary>
    public double ExposureBias { get; }

    public ExposureBiasValueTag(TagId tagId, int a, int b)
    {
        this.TagId = tagId;
        this.ExposureBias = (double)a / b;

        if (this.ExposureBias > 0)
        {
            this.FormattedValue = $"+{this.ExposureBias:0.#} EV";
        }
        else if (this.ExposureBias < 0)
        {
            this.FormattedValue = $"{this.ExposureBias:0.#} EV";
        }
        else
        {
            this.FormattedValue = "±0 EV";
        }
    }
}