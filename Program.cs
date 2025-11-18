// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;




class PatternFinder
{
    private const string PhonePattern = @"\+3\(\d{3}\)-\d{3}-\d{4}";
    private const string DatePattern = @"\b(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}\b";
    private const string IpPattern = @"\b((25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})\b";

    private static readonly Dictionary<string, Regex> patterns = new()
    {
        {"Телефон", new Regex(PhonePattern, RegexOptions.Compiled)},
        {"Дата", new Regex(DatePattern, RegexOptions.Compiled)},
        {"IP-адреса", new Regex(IpPattern, RegexOptions.Compiled)}
    };

    public Dictionary<string, List<string>> FindAll(string text)
    {
        var result = new Dictionary<string, List<string>>();
        foreach (var kv in patterns)
        {
            var matches = kv.Value.Matches(text);
            result[kv.Key] = matches.Select(m => m.Value).ToList();
        }
        return result;
    }
}



class Program
{
    static void Main(string[] args)
    {
        string text = "Заданий текст з номерами телефонів: +3(123)-456-7890, +3(098)-765-4321, і невірний +3(12)-345-6789 або +3(123)-45-6789. Дата: 18/11/2025, ще одна 01/01/2000. IP: 192.168.1.1, 255.255.255.255, 10.0.0.1.";
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Вхідний рядок порожній або не заданий.");
            return;
        }
        var finder = new PatternFinder();
        var found = finder.FindAll(text);
        if (found.All(x => x.Value.Count == 0))
        {
            Console.WriteLine("Збігів не знайдено.");
            return;
        }
        Console.WriteLine("Підсумковий звіт:");
        foreach (var kv in found)
            Console.WriteLine($"{kv.Key}ів: {kv.Value.Count}");
        foreach (var kv in found.Where(x => x.Value.Count > 0))
        {
            Console.WriteLine($"\nЗнайдені {kv.Key.ToLower()}и:");
            kv.Value.ForEach(Console.WriteLine);
        }
    }
}
