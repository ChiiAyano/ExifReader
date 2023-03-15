namespace ExifReader.Models.Tags;

/// <summary>
/// 日付形式を表すタグ
/// </summary>
internal record DateTimeTag : TagBase
{
    /// <summary>
    /// 整形した日付情報
    /// </summary>
    public DateTime DateTime { get; }

    public DateTimeTag(TagId tagId, string? data)
    {
        this.TagId = tagId;

        if (string.IsNullOrWhiteSpace(data))
        {
            this.FormattedValue = string.Empty;
            return;
        }

        // 日付は yyyy:MM:dd HH:mm:ss がくるか、文字分すべてが空白文字、または ":" だけのこしてあと空白文字になるはず
        var yearStr = data[..4];
        var monthStr = data[5..7];
        var dayStr = data[8..10];
        var hourStr = data[11..13];
        var minuteStr = data[14..16];
        var secondStr = data[17..19];

        if (string.IsNullOrWhiteSpace(yearStr) || string.IsNullOrWhiteSpace(monthStr) || string.IsNullOrWhiteSpace(dayStr) ||
            string.IsNullOrWhiteSpace(hourStr) || string.IsNullOrWhiteSpace(minuteStr) || string.IsNullOrWhiteSpace(secondStr))
        {
            this.FormattedValue = string.Empty;
            return;
        }

        if (!int.TryParse(yearStr, out var year) || !int.TryParse(monthStr, out var month) || !int.TryParse(dayStr, out var day) ||
            !int.TryParse(hourStr, out var hour) || !int.TryParse(minuteStr, out var minute) || !int.TryParse(secondStr, out var second))
        {
            this.FormattedValue = string.Empty;
            return;
        }

        this.DateTime = new DateTime(year, month, day, hour, minute, second);
        this.FormattedValue = this.DateTime.ToString("yyyy/MM/dd HH:mm:ss");
    }
}