// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

class PhoneFinder
{
    public List<string> FindPhones(string text)
    {
        string pattern = @"\+3\(\d{3}\)-\d{3}-\d{4}";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(text);
        List<string> phones = new List<string>();
        foreach (Match match in matches)
        {
            phones.Add(match.Value);
        }
        return phones;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string text = "Заданий текст з номерами телефонів: +3(123)-456-7890, +3(098)-765-4321, і невірний +3(12)-345-6789 або +3(123)-45-6789.";
        PhoneFinder finder = new PhoneFinder();
        var phones = finder.FindPhones(text);
        Console.WriteLine("Знайдені номери телефонів:");
        foreach (var phone in phones)
        {
            Console.WriteLine(phone);
        }
    }
}
