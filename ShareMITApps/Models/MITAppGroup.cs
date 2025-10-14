namespace ShareMITApps.Models;

public class MITAppGroup(Category category, IEnumerable<MITApp> apps) : ObservableCollection<MITApp>(apps)
{
    public Category Category { get; } = category;

    public string CategoryName => GetStringValue(Category);

    private static string GetStringValue(Category category)
    {
        var type = typeof(Category);
        var memberInfo = type.GetMember($"{category}");
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(StringValueAttribute), false);
            if (attrs.Length > 0)
            {
                return ((StringValueAttribute)attrs[0]).Value;
            }
        }
        return $"{category}";
    }
}
