﻿
using Com.Ing.DiBa.NotfallExporterLib.Event;
using System;
using System.Drawing;
using System.Windows.Forms;
using Com.Ing.DiBa.NotfallExporterLib.Api;

namespace NotfallExporterUI
{
    public partial class Form1 : Form
    {

        private NotfallExportJob _importJob;
        private OutPutManager _outPutManager;

        public Form1()
        {
            InitializeComponent();
            button_stopImport.Enabled = false;

            textBoxImport.Text = Properties.Settings.Default.ImportDirectory;
            textBoxBackup.Text = Properties.Settings.Default.BackupDirectory;
            textBoxAccountConfig.Text = Properties.Settings.Default.AccountConfig;
            textBoxIndexSpezifikation.Text = Properties.Settings.Default.IdxIndexSpecification;
            textBoxError.Text = Properties.Settings.Default.ErrorDirectory;

            _outPutManager = new OutPutManager(textBoxOutPut);
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
            _importJob.StopJob();

            button_startImport.Enabled = true;
            button_stopImport.Enabled = false;
            buttonStartDirectoryImport.Enabled = true;

            SetExportGuiEnabled(true);
        }

        private void button_startImport_Click(object sender, EventArgs e)
        {
            if (checkBoxOverrideLog.Checked)
                _outPutManager.Clear();

            _outPutManager.PrintText("Starting Directory-Export-Job...\n", Color.Blue);

            ExportModel model = createExportModel();

            DirectoryExporter importer = new DirectoryExporter(model);

            importer.FileExportEvent += (object eventSender, FileExportEventArgs args) => _outPutManager.PrintFileExport(args);
            importer.WarnEvent += (object eventSender, WarnEventArgs args) => _outPutManager.PrintWarn(args);
            importer.ErrorEvent += (object eventSender, ErrorEventArgs args) => _outPutManager.PrintError(args);

            _importJob = new NotfallExportJob(importer);
            _importJob.StartJob();

            button_stopImport.Enabled = true;
            button_startImport.Enabled = false;
            buttonStartDirectoryImport.Enabled = false;

            SetExportGuiEnabled(false);
        }

        private ExportModel createExportModel()
        {
            return new ExportModel()
            {
                ErrorDirectory = textBoxError.Text,
                ImportDirectory = textBoxImport.Text,
                BackupDirectory = textBoxBackup.Text,
                IdxIndexSpecification = textBoxIndexSpezifikation.Text,
                AccountConfig = textBoxAccountConfig.Text
            };
        }

        private void SetExportGuiEnabled(bool enabled)
        {
            textBoxAccountConfig.Enabled = enabled;
            textBoxIndexSpezifikation.Enabled = enabled;
            textBoxError.Enabled = enabled;
            textBoxImport.Enabled = enabled;
            textBoxBackup.Enabled = enabled;
            checkBoxOverrideLog.Enabled = enabled;
        }




      

        private void fillTextBoxWithFolderDialog(TextBox box)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
                box.Text = folderBrowserDialog.SelectedPath;
        }

        private void FillTextBoxWithFileDialog(TextBox box)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "xml files (*.xml)|*.xml";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    box.Text = openFileDialog.FileName;
                    
                }
            }
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

        private void buttonIdexSpezifikationDurchsuchen_Click(object sender, EventArgs e)
        {
            FillTextBoxWithFileDialog(textBoxIndexSpezifikation);
        }

        private void buttonAccountConfigDurchsuchen_Click(object sender, EventArgs e)
        {
            FillTextBoxWithFileDialog(textBoxAccountConfig);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBoxOverrideLog.Checked)
                _outPutManager.Clear();

            _outPutManager.PrintText("Starting Directory-Export...\n", Color.Blue);

            ExportModel model = createExportModel();

            DirectoryExporter importer = new DirectoryExporter(model);

            importer.DirectoryExportEvent += (object eventSender, DirectoryExportEventArgs args) => _outPutManager.PrintDirectoryExport(args);
            importer.FileExportEvent += (object eventSender, FileExportEventArgs args) => _outPutManager.PrintFileExport(args);
            importer.WarnEvent += (object eventSender, WarnEventArgs args) => _outPutManager.PrintWarn(args);
            importer.ErrorEvent += (object eventSender, ErrorEventArgs args) => _outPutManager.PrintError(args);

            importer.Start();
        }
    }
}
