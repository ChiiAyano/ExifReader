namespace ExifReader.Models.Tags;

/// <summary>
/// F値を表すタグ
/// </summary>
internal record FNumberTag : TagBase
{
    /// <summary>
    /// F値
    /// </summary>
    public double Number { get; }

    public FNumberTag(TagId tagId, int a, int b)
    {
        this.TagId = tagId;
        this.Number = (double)a / b;
        this.FormattedValue = $"f/{this.Number:0.#}";
    }
}