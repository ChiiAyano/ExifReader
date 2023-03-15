namespace ExifReader.Models.Tags;

/// <summary>
/// 焦点距離を表すタグ
/// </summary>
internal record FocalLengthTag : TagBase
{
    /// <summary>
    /// 焦点距離
    /// </summary>
    public double Focal { get; }

    public FocalLengthTag(TagId tagId, int a, int b)
    {
        this.TagId = tagId;
        this.Focal = (double)a / b;
        this.FormattedValue = $"{this.Focal:0.#} mm";
    }
}