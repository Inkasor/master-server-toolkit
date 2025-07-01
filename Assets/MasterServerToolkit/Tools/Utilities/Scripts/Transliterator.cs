using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterServerToolkit.MasterServer
{
    public class Transliterator
    {
        private static readonly Dictionary<string, string> CyrToLat = new Dictionary<string, string>
        {
            {"�", "a"}, {"�", "b"}, {"�", "v"}, {"�", "g"}, {"�", "d"},
            {"�", "ye"}, {"�", "yo"}, {"�", "zh"}, {"�", "z"}, {"�", "i"},
            {"�", "j"}, {"�", "k"}, {"�", "l"}, {"�", "m"}, {"�", "n"},
            {"�", "o"}, {"�", "p"}, {"�", "r"}, {"�", "s"}, {"�", "t"},
            {"�", "u"}, {"�", "f"}, {"�", "kh"}, {"�", "ts"}, {"�", "ch"},
            {"�", "sh"}, {"�", "shch"}, {"�", "'"}, {"�", "y"}, {"�", "`"},
            {"�", "e"}, {"�", "yu"}, {"�", "ya"},
            // ��������� �����
            {"�", "A"}, {"�", "B"}, {"�", "V"}, {"�", "G"}, {"�", "D"},
            {"�", "Ye"}, {"�", "Yo"}, {"�", "Zh"}, {"�", "Z"}, {"�", "I"},
            {"�", "J"}, {"�", "K"}, {"�", "L"}, {"�", "M"}, {"�", "N"},
            {"�", "O"}, {"�", "P"}, {"�", "R"}, {"�", "S"}, {"�", "T"},
            {"�", "U"}, {"�", "F"}, {"�", "Kh"}, {"�", "Ts"}, {"�", "Ch"},
            {"�", "Sh"}, {"�", "Shch"}, {"�", "'"}, {"�", "Y"}, {"�", "`"},
            {"�", "E"}, {"�", "Yu"}, {"�", "Ya"}
        };

        private static readonly Dictionary<string, string> LatToCyr = new Dictionary<string, string>
        {
            {"zh", "�"}, {"kh", "�"}, {"ts", "�"}, {"ch", "�"}, {"sh", "�"},
            {"shch", "�"}, {"yu", "�"}, {"ya", "�"}, {"yo", "�"}, {"j", "�"},
            {"`", "�"}, {"'", "�"}, {"y", "�"}, {"a", "�"}, {"b", "�"}, {"v", "�"},
            {"g", "�"}, {"d", "�"}, {"z", "�"}, {"i", "�"}, {"k", "�"},
            {"l", "�"}, {"m", "�"}, {"n", "�"}, {"o", "�"}, {"p", "�"}, {"r", "�"},
            {"s", "�"}, {"t", "�"}, {"u", "�"}, {"f", "�"},
            // ��� "�" � "�"
            {"ye", "�"}, {"Ye", "�"}, {"YE", "�"}, {"e", "�"}, {"E", "�"},
            // ��������� ����������
            {"Zh", "�"}, {"Kh", "�"}, {"Ts", "�"}, {"Ch", "�"}, {"Sh", "�"},
            {"Shch", "�"}, {"Yu", "�"}, {"Ya", "�"}, {"Yo", "�"}, {"J", "�"},
            {"Y", "�"}, {"A", "�"}, {"B", "�"}, {"V", "�"}, {"G", "�"}, {"D", "�"},
            {"Z", "�"}, {"I", "�"}, {"K", "�"}, {"L", "�"}, {"M", "�"},
            {"N", "�"}, {"O", "�"}, {"P", "�"}, {"R", "�"}, {"S", "�"}, {"T", "�"},
            {"U", "�"}, {"F", "�"}
        };

        public static string CyrillicToLatin(string input)
        {
            if (string.IsNullOrEmpty(input)) 
                return input;

            var sb = new StringBuilder();

            foreach (char c in input)
            {
                string key = c.ToString();
                sb.Append(CyrToLat.TryGetValue(key, out var value) ? value : c);
            }

            return sb.ToString();
        }

        public static string LatinToCyrillic(string input)
        {
            if (string.IsNullOrEmpty(input)) 
                return input;

            var sb = new StringBuilder();
            int pos = 0;

            while (pos < input.Length)
            {
                bool replaced = false;

                foreach (var key in LatToCyr.Keys.OrderByDescending(k => k.Length))
                {
                    if (pos + key.Length <= input.Length)
                    {
                        string substr = input.Substring(pos, key.Length);

                        if (LatToCyr.TryGetValue(substr, out var value))
                        {
                            sb.Append(value);
                            pos += key.Length;
                            replaced = true;
                            break;
                        }
                    }
                }

                if (!replaced)
                {
                    sb.Append(input[pos++]);
                }
            }

            return sb.ToString();
        }
    }
}