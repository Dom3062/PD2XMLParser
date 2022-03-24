using System.Windows.Forms;
namespace XMLParser
{
    static class Debug
    {
        public static void ToMessageBox<T>(this T text, string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            MessageBox.Show(text.ToString(), caption, buttons, icon);
        }

        public static void ToMessageBox<T>(this T[] text, string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            string text_ = "";
            foreach (T i in text)
            {
                if (text_ != "") text_ += ", ";
                text_ += i;
            }
            MessageBox.Show(text_, caption, buttons, icon);
        }
    }
}