namespace ExifReader.Models.Tags;

/// <summary>
/// 感度種別を表すタグ
/// </summary>
internal record SensitivityTypeTag : TagBase
{
    public enum Types
    {
        /// <summary>
        /// 不明
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 標準出力感度
        /// </summary>
        StandardOutputSensitivity = 1,
        /// <summary>
        /// 推奨露光指数
        /// </summary>
        RecommendedExposureIndex = 2,
        /// <summary>
        /// ISOスピード
        /// </summary>
        IsoSpeed = 3,
        /// <summary>
        /// 標準出力感度および推奨露光指数
        /// </summary>
        SosAndRei = 4,
        /// <summary>
        /// 標準出力感度およびISOスピード
        /// </summary>
        SosAndIso = 5,
        /// <summary>
        /// 推奨露光指数およびISOスピード
        /// </summary>
        ReiAndIso = 6,
        /// <summary>
        /// すべて
        /// </summary>
        All = 7
    }

    public Types DataType { get; }

    public SensitivityTypeTag(TagId tagId, int data)
    {
        this.DataType = (Types)data;
        this.FormattedValue = this.DataType.ToString();
    }
}
