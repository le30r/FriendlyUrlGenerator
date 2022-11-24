namespace FriendlyUrlGenerator.Parser;

public class Language
{
    public List<string> Nouns { get; }
    public List<string> Verbs { get; }
    public List<string> Adjects { get; }
    public Locale Locale;

    public Language(List<string> nouns, List<string> verbs, List<string> adjects, Locale locale)
    {
        Nouns = nouns;
        Verbs = verbs;
        Adjects = adjects;
        Locale = locale;
    }
}