namespace Lab9.Green;

public class Task3 : Green
{
    private string _pattern;
    private string[] _output;

    public string[] Output => _output.ToArray();

    public Task3(string text, string pattern) : base(text)
    {
        _pattern = pattern ?? string.Empty;
        _output = [];
    }

    public override void Review()
    {
        _output = [];

        if (Input == null || Input.Length == 0 || _pattern == null || _pattern.Length == 0)
        {
            return;
        }

        string loweredPattern = _pattern.ToLower();
        List<string> result = [];
        HashSet<string> usedWords = new(StringComparer.OrdinalIgnoreCase);

        foreach (string word in ExtractWords(Input))
        {
            if (word.ToLower().Contains(loweredPattern) && usedWords.Add(word))
            {
                result.Add(word);
            }
        }

        _output = result.ToArray();
    }

    public override string ToString()
    {
        if (_output == null || _output.Length == 0)
        {
            return string.Empty;
        }

        return string.Join(Environment.NewLine, _output);
    }

    private static List<string> ExtractWords(string text)
    {
        List<string> words = [];
        int index = 0;

        while (index < text.Length)
        {
            if (!IsWordSymbol(text[index]))
            {
                index++;
                continue;
            }

            int start = index;
            string word = string.Empty;

            while (index < text.Length && IsWordSymbol(text[index]))
            {
                word += text[index];
                index++;
            }

            int end = index - 1;
            bool touchesDigit = start > 0 && char.IsDigit(text[start - 1])
                || end + 1 < text.Length && char.IsDigit(text[end + 1]);

            if (!touchesDigit && word.Any(char.IsLetter))
            {
                words.Add(word);
            }
        }

        return words;
    }

    private static bool IsWordSymbol(char symbol)
    {
        return char.IsLetter(symbol) || symbol == '-' || symbol == '\'';
    }
}
