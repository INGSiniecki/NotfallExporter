
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NotfallExporterUI
{
    /// <summary>
    /// Manager-Class for Export-Output
    /// </summary>
    class OutPutManager
    {
        private readonly RichTextBox _richTextBox;
        private delegate void SafeCallDelegate(string text, Color color);

        /// <summary>
        /// instantiates an instance of OutPutManager
        /// </summary>
        /// <param name="richTextBox">TextBox object to display the output</param>
        public OutPutManager(RichTextBox richTextBox)
        {
            _richTextBox = richTextBox;
        }

        /// <summary>
        /// clears the output
        /// </summary>
        public void Clear()
        {
            _richTextBox.Clear();
        }


        /// <summary>
        /// prints a colored Text
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="color">color</param>
        public void PrintText(string text, Color color)
        {
            if (_richTextBox.InvokeRequired)
            {
                _richTextBox.Invoke(new SafeCallDelegate(PrintText), new object[] { text, color });
            }
            else
            {
                _richTextBox.SelectionStart = _richTextBox.TextLength;
                _richTextBox.SelectionLength = 0;

                _richTextBox.SelectionColor = color;
                _richTextBox.AppendText(text);
                _richTextBox.SelectionColor = _richTextBox.ForeColor;
            }
        }

        public void PrintMessage(string message)
        {
            PrintText($"{message}\n", Color.Black);
        }

        /// <summary>
        /// prints ErrorEvents
        /// </summary>
        /// <param name="args"></param>
        public void PrintError(Exception e)
        {
            PrintText($"{e.Message}\n", Color.Red);
        }
    }
}
