using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormClient
{
    using System.Drawing;
    using System.Windows.Forms;

    static class RichTextEx
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.ScrollToCaret();
            box.ResumeLayout();
        }

        public static void AppendText(this RichTextBox box, string baslik, string mesaj, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = Color.DarkRed;
            box.AppendText(baslik + ":\t");
            box.SelectionColor = box.ForeColor;

            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(mesaj + "\n");
            box.SelectionColor = box.ForeColor;
            box.ScrollToCaret();
            box.ResumeLayout();
        }
    }
}
