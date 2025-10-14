namespace ShareMITApps.Helpers;

[AttributeUsage(AttributeTargets.Field)]
public class StringValueAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}
