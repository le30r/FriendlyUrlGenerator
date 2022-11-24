namespace FriendlyUrlGenerator.Parser;

public class WordParser
{
    public Language Parse(Locale locale)
    {
        List<string> adjects = new();
        var adjectsThread = new Thread(() =>
        {
            var fa = new StreamReader($@".\Resources\{locale}\adjects.txt");
            var line = fa.ReadLine();
            while (line != null)
            {
                line = fa.ReadLine();
                adjects.Add((line ?? "").ToLower().Trim());
            }

            fa.Dispose();
        });

        List<string> nouns = new();
        var nounsThread = new Thread(() =>
        {
            var fn = new StreamReader($@".\Resources\{locale}\nouns.txt");
            var line = fn.ReadLine();
            while (line != null)
            {
                line = fn.ReadLine();
                nouns.Add((line ?? "").ToLower().Trim());
            }

            fn.Dispose();
        });

        List<string> verbs = new();
        var verbsThread = new Thread(() =>
        {
            var fv = new StreamReader($@".\Resources\{locale}\verbs.txt");
            var line = fv.ReadLine();
            while (line != null)
            {
                line = fv.ReadLine();
                verbs.Add((line ?? "").ToLower().Trim());
            }

            fv.Dispose();
        });

        verbsThread.Start();
        nounsThread.Start();
        adjectsThread.Start();


        verbsThread.Join();
        nounsThread.Join();
        adjectsThread.Join();


        return new Language(nouns, verbs, adjects, locale);
    }
}