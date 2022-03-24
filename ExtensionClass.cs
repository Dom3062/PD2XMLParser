static class StringExtensionClass
{
    /// <summary>
    /// Returns True or False if user defined string is found in the array string.
    /// </summary>
    /// <param name="array">Array string</param>
    /// <param name="find">User defined string to find in the array string</param>
    /// <returns>Returns True if user defined string is found in the array string, otherwise returns False</returns>
    public static bool Contains(this string[] array, string find)
    { //Rozšíření původní funkce Contains, která nepracuje s řetězci v poli
        foreach (string i in array)
        {
            if (i.Contains(find))
            {
                return true;
            }
        }
        return false;
    }

    public static bool Contains(this string[] array, string find, out int pos)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Contains(find))
            {
                pos = i;
                return true;
            }
        }
        pos = -1;
        return false;
    }

    public static bool ContainsExact(this string[] array, string find)
    {
        foreach (string i in array)
        {
            if (i.ToLower() == find)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns True, if string contains one or more defined string in string array. Otherwise returns False.
    /// </summary>
    /// <param name="text">String</param>
    /// <param name="texts">String array</param>
    /// <returns>Returns True, if string contains one or more defined string in string array. Otherwise returns False.</returns>
    public static bool ContainsOr(this string text, params string[] texts)
    {
        foreach (string s in texts)
        {
            if (text.Contains(s))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns last character in string
    /// </summary>
    /// <param name="s">String</param>
    /// <returns>Returns last character in string</returns>
    public static char LastCharacter(this string s)
    {
        if (s == null || s.Length == 0)
        {
            return ' ';
        }
        return s[s.Length - 1];
    }

    /// <summary>
    /// Returns True if the string can be parsed to double, otherwise returns False (does NOT return exception)
    /// </summary>
    /// <param name="text">Text string to convert to double</param>
    /// <returns>Returns True if the string can be parsed to double, otherwise returns False</returns>
    public static bool CanParseToDouble(this string text)
    {
        try
        {
            System.Convert.ToDouble(text);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Reverses a string
    /// </summary>
    /// <param name="text">String to reverse</param>
    /// <returns>Returns reversed string</returns>
    public static string Reverse(this string text)
    {
        string a = "";
        for (int i = text.Length - 1; i >= 0; i--)
        {
            a += text[i];
        }
        return a;
    }

    /// <summary>
    /// Returns position on which text is in the array. If not found, it returns -1.
    /// </summary>
    /// <param name="array">Array to search</param>
    /// <param name="text">Text to search</param>
    /// <returns>Returns text position in the array. If not found, it returns -1.</returns>
    public static int GetTextPosition(this string[] array, string text)
    {
        for(int i = 0; i < array.Length; i++)
        {
            if(array[i] == text)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Returns count of user defined char in string literal
    /// </summary>
    /// <param name="text">String literal</param>
    /// <param name="char_to_find">Char to find in string literal</param>
    /// <returns>Returns count of user defined char in string literal</returns>
    public static int Count(this string text, char char_to_find)
    {
        int count = 0;
        foreach (char c in text)
        {
            if (c == char_to_find)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Returns first position of user defined char in string literal
    /// </summary>
    /// <param name="text">String literal</param>
    /// <param name="char_to_return">User defined char to find in string literal</param>
    /// <returns>Returns first position of char_to_return. If not user defined char is found, it returns -1</returns>
    public static int GetCharPos(this string text, char char_to_return)
    {
        return GetCharPos(text, char_to_return, 0);
    }

    /// <summary>
    /// Returns position of user defined char in string literal
    /// </summary>
    /// <param name="text">String literal</param>
    /// <param name="char_to_return">User defined char to find in string literal</param>
    /// <param name="pos">Defines, which position of char_to_return return</param>
    /// <returns>Returns position of char_to_return and pos. If not user defined char is found on pos, it returns -1</returns>
    public static int GetCharPos(this string text, char char_to_return, int pos)
    {
        int p = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == char_to_return)
            {
                if (p == pos)
                {
                    return i;
                }
                else
                {
                    p++;
                }
            }
        }
        return -1;
    }
}

static class CharExtensionClass
{
    /// <summary>
    /// Returns True or False if char is between start and end
    /// </summary>
    /// <param name="number">Number to compare</param>
    /// <param name="start">Start char</param>
    /// <param name="end">End char</param>
    /// <param name="equal">Defines if the char will be checked with >=|=< or >|<</param>
    /// <returns>Returns True or False if char is between start and end</returns>
    public static bool Between(this char letter, char start, char end, bool inclusive = false)
    { //Přidání nové funkce Between k char
        if (inclusive)
        {
            return (letter >= start && letter <= end);
        }
        else
        {
            return (letter > start && letter < end);
        }
    }
}

static class Int32ExtensionClass
{
    /// <summary>
    /// Returns True or False if number is between start and end
    /// </summary>
    /// <param name="number">Number to compare</param>
    /// <param name="start">Start number</param>
    /// <param name="end">End number</param>
    /// <param name="equal">Defines if the number will be checked with >=|=< or >|<</param>
    /// <returns>Returns True or False if number is between start and end</returns>
    public static bool Between(this int number, int start, int end, bool inclusive = false)
    { //Přidání nové funkce Between k int
        if (inclusive)
        {
            return (number >= start && number <= end);
        }
        else
        {
            return (number > start && number < end);
        }
    }
}

static class DoubleExtensionClass
{
    /// <summary>
    /// Returns True or False if number is between start and end
    /// </summary>
    /// <param name="number">Number to compare</param>
    /// <param name="start">Start number</param>
    /// <param name="end">End number</param>
    /// <param name="equal">Defines if the number will be checked with >=|=< or >|<</param>
    /// <returns>Returns True or False if number is between start and end</returns>
    public static bool Between(this double number, double start, double end, bool inclusive = false)
    { //Přidání nové funkce Between k double
        if (inclusive)
        {
            return (number >= start && number <= end);
        }
        else
        {
            return (number > start && number < end);
        }
    }
}

static class FormExtensionClass
{
    /// <summary>
    /// Nulls ( "" ) every textbox on the form
    /// </summary>
    public static void Nulling(this System.Windows.Forms.Form F)
    {
        foreach (System.Windows.Forms.Control C in F.Controls)
        {
            if (C.GetType() == typeof(System.Windows.Forms.TextBox))
            {
                C.Text = "";
            }
        }
    }

    /// <summary>
    /// Shows or hides all same types on the form
    /// </summary>
    /// <param name="type">Type to show or hide</param>
    /// <param name="sh">True to show, False to hide</param>
    public static void ShowHideType(this System.Windows.Forms.Form F, System.Type type, bool sh)
    {
        foreach (System.Windows.Forms.Control C in F.Controls)
        {
            if (C.GetType() == type)
            {
                C.Visible = sh;
            }
        }
    }

    /// <summary>
    /// Sets white color to every textbox on the form
    /// </summary>
    public static void White(this System.Windows.Forms.Form F)
    {
        foreach (System.Windows.Forms.Control C in F.Controls)
        {
            if (C.GetType() == typeof(System.Windows.Forms.TextBox))
            {
                C.BackColor = System.Drawing.Color.White;
            }
        }
    }
}

public static class GenericExtensionClass
{
    /// <summary>
    /// Returns True if T is equal one of the T in array_t, otherwise returns False
    /// </summary>
    /// <typeparam name="T">Type to use. Parameters t and array_t must be the same type as T</typeparam>
    /// <param name="t">T to compare</param>
    /// <param name="array_t">Array of T</param>
    /// <returns>Returns True if T is equal to at least one of the T in array_t, otherwise returns False</returns>
    public static bool IsOr<T>(this T t, params T[] array_t)
    {
        foreach (T _t in array_t)
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(t, _t))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns True if T is equal to one of the T in find array, otherwise returns False
    /// </summary>
    /// <typeparam name="T">Type to use. find array must be the same type as T</typeparam>
    /// <param name="list">List to read</param>
    /// <param name="find">Array of T</param>
    /// <returns>Returns True if T is equal to at least one of the T in find array, otherwise returns False</returns>
    public static bool ContainsOr<T>(this System.Collections.Generic.List<T> list, params T[] find)
    {
        foreach (T t in list)
        {
            foreach (T r in find)
            {
                if (Equals(t, r))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static T Last<T>(this System.Collections.Generic.List<T> list)
    {
        if (list.Count > 0)
        {
            return list[list.Count - 1];
        }
        return default;
    }

    public static void RemoveLast<T>(this System.Collections.Generic.List<T> list)
    {
        if (list.Count > 0)
        {
            list.RemoveAt(list.Count - 1);
        }
    }
}