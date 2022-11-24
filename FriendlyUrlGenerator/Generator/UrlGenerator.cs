using System.Text;
using FriendlyUrlGenerator.Parser;

namespace FriendlyUrlGenerator.Generator;

public class UrlGenerator
{
    private readonly Locale _locale;

    private readonly Language _language;
    public UrlGenerator(Locale locale)
    {
        _locale = locale;
        WordParser parser = new();
        _language = parser.Parse(_locale);
    }

    public string Generate()
    {
        var random = Random.Shared;
        var firstWord = _language.Verbs[random.Next(_language.Verbs.Count)];
        var secondWord = _language.Adjects[random.Next(_language.Adjects.Count)];
        var thirdWord = _language.Nouns[random.Next(_language.Nouns.Count)];
        return $"{firstWord}-{secondWord}-{thirdWord}";
    }

    public string Generate(string pattern)
    {
        var random = Random.Shared;
        var content = ParsePattern(pattern);
        var result = new StringBuilder();
        foreach (var item in content.Select((lexem, i) => (lexem, i)))
            switch (item.lexem.Item1)
            {
                case Lexem.NOUN:
                    result.Append(item.lexem.Item2
                        ? Capitalize(_language.Nouns[random.Next(_language.Nouns.Count)])
                        : _language.Nouns[random.Next(_language.Nouns.Count)]);
                    break;
                case Lexem.VERB:
                    result.Append(item.lexem.Item2
                        ? Capitalize(_language.Verbs[random.Next(_language.Verbs.Count)])
                        : _language.Verbs[random.Next(_language.Verbs.Count)]);
                    break;
                case Lexem.ADJECT:
                    result.Append(item.lexem.Item2
                        ? Capitalize(_language.Adjects[random.Next(_language.Adjects.Count)])
                        : _language.Adjects[random.Next(_language.Adjects.Count)]
                    );
                    break;
                case Lexem.SYMBOL:
                    result.Append(pattern[item.i]);
                    break;
            }

        return result.ToString();
    }

    public List<(Lexem, bool)> ParsePattern(string pattern)
    {
        List<(Lexem, bool)> result = new();
        var i = 0;
        while (i < pattern.Length)
        {
            if (pattern[i] == '^')
            {
                result.Add((Lexem.CARET, false));
                switch (pattern[i + 1])
                {
                    case 'v':
                        result.Add((Lexem.VERB, false));
                        break;
                    case 'n':
                        result.Add((Lexem.NOUN, false));
                        break;
                    case 'a':
                        result.Add((Lexem.ADJECT, false));
                        break;
                    case 'V':
                        result.Add((Lexem.VERB, true));
                        break;
                    case 'N':
                        result.Add((Lexem.NOUN, true));
                        break;
                    case 'A':
                        result.Add((Lexem.ADJECT, true));
                        break;
                    default:
                        throw new ArgumentException("Wrong pattern syntax");
                }

                i++;
            }
            else
            {
                result.Add((Lexem.SYMBOL, false));
            }

            i++;
        }

        return result;
    }

    public static string Capitalize(string input)
    {
        return input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };
    }
}