namespace Lab9.Green;

public class Task2 : Green
{
    private char[] _output;

    public char[] Output => _output.ToArray();

    public Task2(string text) : base(text)
    {
        _output = [];
    }

    public override void Review()
    {
        _output = [];

        if (Input == null || Input.Length == 0)
        {
            return;
        }

        List<char> firstLetters = [];
        foreach (string word in ExtractWords(Input))
        {
            if (word.Length > 0)
            {
                char firstLetter = char.ToLower(word[0]);
                if (char.IsLetter(firstLetter))
                {
                    firstLetters.Add(firstLetter);
                }
            }
        }

        if (firstLetters.Count == 0)
        {
            return;
        }

        Dictionary<char, int> counts = [];
        foreach (char letter in firstLetters)
        {
            if (counts.ContainsKey(letter))
            {
                counts[letter]++;
            }
            else
            {
                counts[letter] = 1;
            }
        }

        char[] letters = counts.Keys.ToArray();
        for (int i = 0; i < letters.Length - 1; i++)
        {
            for (int j = 0; j < letters.Length - i - 1; j++)
            {
                bool shouldSwap = counts[letters[j]] < counts[letters[j + 1]]
                    || counts[letters[j]] == counts[letters[j + 1]] && letters[j] > letters[j + 1];

                if (shouldSwap)
                {
                    char temp = letters[j];
                    letters[j] = letters[j + 1];
                    letters[j + 1] = temp;
                }
            }
        }

        _output = letters;
    }

    public override string ToString()
    {
        if (_output == null || _output.Length == 0)
        {
            return string.Empty;
        }

        return string.Join(", ", _output);
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
