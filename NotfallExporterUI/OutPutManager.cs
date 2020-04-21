
using Com.Ing.DiBa.NotfallExporterLib.Event;
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
        /// prints Directory-Export Events
        /// </summary>
        /// <param name="args"></param>
        public void PrintDirectoryExport(DirectoryExportEventArgs args)
        {
            string output = $"Directory: {args.Directory.Name} exported!\nExport-Duration: {args.durationMillis}ms\nFiles exported: {args.importedFileCount}\n";
            PrintText(output, Color.Blue);
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

        /// <summary>
        /// prints  File-Export-Events
        /// </summary>
        /// <param name="args"></param>
        public void PrintFileExport(FileExportEventArgs args)
        {
            string output = $"{args.SourceFile.Name} exported!\n";
            PrintText(output, Color.Green);
        }
        /// <summary>
        /// Prints WarnEvents
        /// </summary>
        /// <param name="args"></param>
        public void PrintWarn(WarnEventArgs args)
        {
            PrintText($"{args.WarnMessage}\n", Color.Orange);
        }

        /// <summary>
        /// prints ErrorEvents
        /// </summary>
        /// <param name="args"></param>
        public void PrintError(ErrorEventArgs args)
        {
            PrintText($"{args.Exception.Message}\n", Color.Red);
        }
    }
}
