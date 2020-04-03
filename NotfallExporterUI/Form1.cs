using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotfallExporterLib;

namespace NotfallExporterUI
{
    public partial class Form1 : Form
    {

        NotfallImportJob importJob;
        public Form1()
        {
            InitializeComponent();
            button_stopImport.Enabled = false;

            textBoxError.Text = @"D:\Programmierung\ING\Files\Error";
            textBoxImport.Text = @"D:\Programmierung\ING\Files\ImportDMS";
            textBoxBackup.Text = @"D:\Programmierung\ING\Files\MovedTo_nti2Doxis";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button_stopImport_Click(object sender, EventArgs e)
        {
            importJob.stopJob();

            button_startImport.Enabled = true;
            button_stopImport.Enabled = false;
        }

        private void button_startImport_Click(object sender, EventArgs e)
        {
            ImportData model = new ImportData();
            model._error_directory = textBoxError.Text;
            model._import_directory = textBoxImport.Text;
            model._backup_directory = textBoxBackup.Text;

            NotfallImporter importer = new NotfallImporter(model);

            importJob = new NotfallImportJob(importer);
            importJob.startJob();


           


            button_stopImport.Enabled = true;
            button_startImport.Enabled = false;
        }

      

        private void fillTextBoxWithFolderDialog(TextBox box)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
                box.Text = folderBrowserDialog.SelectedPath;
        }

        private void buttonImportDurchsuchen_Click(object sender, EventArgs e)
        {
            fillTextBoxWithFolderDialog(textBoxImport);
        }

        private void buttonBackupDurchsuchen_Click(object sender, EventArgs e)
        {
            fillTextBoxWithFolderDialog(textBoxBackup);
        }

        private void buttonErrorDurchsuchen_Click(object sender, EventArgs e)
        {
            fillTextBoxWithFolderDialog(textBoxError);
        }
    }
}
