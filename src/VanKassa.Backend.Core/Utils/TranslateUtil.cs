using System;
namespace VanKassa.Backend.Core.Utils
{
    public static class TranslateUtil
    {
        private static readonly IDictionary<char, string> RussianEnglishDictionary = new Dictionary<char, string>
        {
            {'а', "a" },
            {'б', "b" },
            {'в', "v" },
            {'г', "g" },
            {'д', "d" },
            {'е', "e" },
            {'ё', "e" },
            {'ж', "zh" },
            {'з', "z" },
            {'и', "i" },
            {'й', "j" },
            {'к', "k"},
            {'л', "l" },
            {'м', "m" },
            {'н', "n" },
            {'о', "o" },
            {'п', "p" },
            {'р', "r" },
            {'с', "c" },
            {'т', "t" },
            {'у', "u" },
            {'ф', "f" },
            {'х', "h" },
            {'ц', "c" },
            {'ч', "ch" },
            {'ш', "sh" },
            {'щ', "sch" },
            {'ь', "'" },
            {'ы', "y" },
            {'ъ', "`" },
            {'э', "\"" },
            {'ю', "ju" },
            {'я', "ja" },

            {'А', "A" },
            {'Б', "B" },
            {'В', "V" },
            {'Г', "G" },
            {'Д', "D" },
            {'Е', "E" },
            {'Ё', "E" },
            {'Ж', "ZH" },
            {'З', "Z" },
            {'И', "I" },
            {'Й', "J" },
            {'К', "K"},
            {'Л', "L" },
            {'М', "M" },
            {'Н', "N" },
            {'О', "O" },
            {'П', "P" },
            {'Р', "R" },
            {'С', "C" },
            {'Т', "T" },
            {'У', "U" },
            {'Ф', "F" },
            {'Х', "H" },
            {'Ц', "C" },
            {'Ч', "CH" },
            {'Ш', "SH" },
            {'Щ', "SCH" },
            {'Ь', "'" },
            {'Ы', "Y" },
            {'Ъ', "`" },
            {'Э', "\"" },
            {'Ю', "JU" },
            {'Я', "JA" },
        };


        public static string TransplitRussianToEnglish(string russianText)
        {
            try
            {
                return string.Join("", russianText.Select(letter => RussianEnglishDictionary[letter]));
            }
            catch (KeyNotFoundException)
            {
                return russianText;
            }
        }
    }
}

