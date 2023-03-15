namespace ExifReader.Models.Tags;

/// <summary>
/// タグ情報を表します。このクラスは抽象クラスです。
/// </summary>
internal abstract record TagBase
{
    /// <summary>
    /// タグ番号
    /// </summary>
    public TagId TagId { get; protected init; }

    /// <summary>
    /// タグの整形済み情報
    /// </summary>
    public string? FormattedValue { get; protected set; }
}