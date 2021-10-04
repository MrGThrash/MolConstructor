using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MolConstructor.UserControls
{
    public partial class VisualControl : UserControl
    {
        public VisualControl()
        {
            InitializeComponent();
        }

        private void btnChoosePath_Page1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;
            tbDirectoryPath.Text = folderBrowserDialog.SelectedPath;
            tbFileCount.Text = Directory.GetFiles(tbDirectoryPath.Text, "*." + cmbFormat_Page1.SelectedItem).OrderBy((f => f)).Count().ToString();
            tbFilesToWrite.Text = tbFileCount.Text;
            btnWriteMovieFile.Enabled = true;
        }

        private void chbWithShift_CheckedChanged(object sender, EventArgs e)
        {
            if (chbWithShift.Checked)
                gbShiftValues.Enabled = true;
            else
                gbShiftValues.Enabled = false;
        }

        private void cmbFormat_Page1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFormat_Page1.SelectedIndex != -1)
            {
                btnChoosePath_Page1.Enabled = true;
            }
            else
            {
                btnChoosePath_Page1.Enabled = false;
                btnWriteMovieFile.Enabled = false;
            }
        }

        private void chbTrim_CheckedChanged(object sender, EventArgs e)
        {
            label88.Visible = chbTrim.Checked;
            tbTrimPerc.Visible = chbTrim.Checked;
        }

        private void btnWriteMovieFile_Click(object sender, EventArgs e)
        {
            if (tbDirectoryPath.Text == "")
            {
                MessageBox.Show((IWin32Window)null, "Не указан путь к папке с файлами!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                var allfiles = Directory.GetFiles(tbDirectoryPath.Text, "*." + cmbFormat_Page1.SelectedItem).OrderBy((f => new FileInfo(f).LastWriteTime)).ToList<string>();
                int fileCount = Convert.ToInt32(tbFilesToWrite.Text);
                string[] files = new string[fileCount];
                for (int i = 0; i < fileCount; i++)
                {
                    files[i] = allfiles.ElementAt(i);
                }

                btnCancel.Visible = true;
                pBar.Value = 0;
                pBar.Visible = true;


                if (cmbTaskType.SelectedIndex == 0)
                {
                    double trimValue = replaceValue(tbTrimPerc.Text);
                    double[] shifts = new double[3];
                    if (chbWithShift.Checked)
                    {
                        shifts[0] = replaceValue(tbShiftX.Text);
                        shifts[1] = replaceValue(tbShiftY.Text);
                        shifts[2] = replaceValue(tbShiftZ.Text);
                    }

                    bgWorker.RunWorkerAsync(new object[]{chbHasWalls_Page1.Checked,tbDirectoryPath.Text,tbMovieOutName.Text,
                                                     cmbFormat_Page1.SelectedItem,shifts,chbTrim.Checked,trimValue,files});
                }
                else
                {
                    bgWorkerCenter.RunWorkerAsync(new object[] { cmbFormat_Page1.SelectedItem, cmbTaskType.SelectedIndex, files });
                }
            }
        }

        private void btnShowMovie_Click(object sender, EventArgs e)
        {
            var name = tbMovieOutName.Text;
            var moviePath = tbDirectoryPath.Text + "\\" + name + "\\.lammpstrj";
            if (Directory.Exists(moviePath))
            {
                StartProcces(moviePath);
            }
        }


        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] objects = (object[])e.Argument;
            bool hasWalls = (bool)objects[0];
            string fileName = (string)objects[1] + "/" + (string)objects[2] + ".lammpstrj";
            string format = (string)objects[3];
            double[] shifts = (double[])objects[4];
            bool toTrim = (bool)objects[5];
            double percentage = (double)objects[6] / 100.0;
            string[] files = (string[])objects[7];
            for (int i = 0; i < files.Length; i++)
            {
                double[] sizes = new double[3];

                List<double[]> data;
                if (format == "xyzr")
                {
                    data = FileWorker.LoadXyzrLines(files[i]);
                    sizes[0] = Math.Abs(data[data.Count - 1][0] - data[data.Count - 8][0]);
                    sizes[1] = Math.Abs(data[data.Count - 1][1] - data[data.Count - 8][1]);
                    sizes[2] = Math.Abs(data[data.Count - 1][2]);
                }
                else
                {
                    int snapnum = 0;
                    data = FileWorker.LoadLammpstrjLines(files[i], out snapnum, out sizes);
                }
                double[] centerPoint = MolData.GetCenterPoint(sizes, data);

                var system = MolData.ShiftAll(hasWalls, 0, false, 3, sizes, shifts, centerPoint, data);
                if (toTrim)
                {
                    system = MolData.ReduceSystem(system, percentage);
                }

                FileWorker.SaveLammpstrj(true, fileName, i + 1, sizes, 3, system);
                int filesCount = (int)((double)files.Length / 100.0);
                if (filesCount == 0)
                {
                    filesCount++;
                }
                if (i % filesCount == 0)
                {
                    if (((BackgroundWorker)sender).CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                  ((BackgroundWorker)sender).ReportProgress((int)(100.0 * (double)i / (double)files.Length));
                }
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                return;
            }
            MessageBox.Show(null, "Файл сформирован", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            pBar.Value = 0;
            pBar.Visible = false;
            btnCancel.Visible = false;
            btnShowMovie.Enabled = true;

        }

        private void bgWorkerCenter_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] objects = (object[])e.Argument;
            string format = (string)objects[0];
            int centerType = (int)objects[1];
            string[] files = (string[])objects[2];

            bool withZCenter = true;

            if (centerType == 2)
            {
                withZCenter = false;
            }

            double[] sizes = new double[3];
            double[] centerPoint = new double[3];

            for (int k = 0, len = files.Length; k < len; k++)
            {
                if (!bgWorkerCenter.CancellationPending)
                {
                    int snapnum = 0;
                    var file = new List<double[]>();

                    readTableFile(format, files[k], out snapnum, out file, out sizes);

                    if (k == 0)
                    {
                        centerPoint = MolData.GetCenterPoint(sizes, file);
                    }

                    //var bil = file.Where(x => x[3] == 1.01 || x[3] == 1.04).ToList();

                    //bil = file.Where(x => x[0] >= 0 && x[0] <= 30 && x[1] >= 0 && x[1] >= 30).ToList();

                    //var cm = Methods.GetAxCenterMass(bil, 2);
                    var cm = 50.0;

                    doAutoCenter(withZCenter, 5, sizes, centerPoint, file);

                    var strct = MolData.ShiftAll(false, 0, false, 3, sizes, new double[] { 0, 0, 50-cm }, centerPoint, file);

                    if (format == "xyzr")
                    {
                        FileWorker.Save_XYZ(files[k], true, sizes, strct);
                    }
                    else
                    {
                        FileWorker.SaveLammpstrj(false, files[k], snapnum, sizes, 3, strct);
                    }

                    int filesCount = (int)((double)files.Length / 100.0);
                    if (filesCount == 0)
                    {
                        filesCount++;
                    }
                    if (k % filesCount == 0)
                    {

                        ((BackgroundWorker)sender).ReportProgress((int)(100.0 * (double)k / (double)files.Length));
                    }
                }
                else
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        private void bgWorkerCenter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void bgWorkerCenter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                return;
            }
            MessageBox.Show(null, "Центровка файлов завершена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            pBar.Value = 0;
            pBar.Visible = false;
            btnCancel.Visible = false;
            btnShowMovie.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
            }
            if (bgWorkerCenter.IsBusy)
            {
                bgWorkerCenter.CancelAsync();
            }
        }

        private void cmbTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isMovie = (cmbTaskType.SelectedIndex < 1) ? true : false;

            chbWithShift.Enabled = isMovie;
            gbShiftValues.Enabled = isMovie;
            label54.Enabled = isMovie;
            tbMovieOutName.Enabled = isMovie;
            chbTrim.Enabled = isMovie;
            label88.Enabled = isMovie;
            tbTrimPerc.Enabled = isMovie;

            if (isMovie)
            {
                btnWriteMovieFile.Text = "Записать файл";
            }
            else
            {
                btnWriteMovieFile.Text = "Обработать файлы";
            }

        }

        private void readTableFile(string format, string fileName, out int snapnum, out List<double[]> file, out double[] sizes)
        {
            sizes = new double[3];
            if (format == "xyzr")
            {
                file = FileWorker.LoadXyzrLines(fileName);

                sizes[0] = Math.Abs(file[file.Count - 1][0] - file[file.Count - 8][0]);
                sizes[1] = Math.Abs(file[file.Count - 1][1] - file[file.Count - 8][1]);
                sizes[2] = Math.Abs(file[file.Count - 1][2]);

                snapnum = 0;
            }
            else
            {
                file = FileWorker.LoadLammpstrjLines(fileName, out snapnum, out sizes);
            }
        }

        private void doAutoCenter(bool withZCenter, int k, double[] sizes, double[] centerPoint, List<double[]> file)
        {
            if (k == 0)
            {
                return;
            }

            for (int i = 0; i < k; i++)
            {
                double[] centerCoord = Methods.CenterStructure(centerPoint, file);

                if (Math.Abs(centerCoord[0]) < 0.5 && Math.Abs(centerCoord[1]) < 0.5 && Math.Abs(centerCoord[2]) < 0.5)
                {
                    break;
                }

                int breakmark = 0;

                if (!withZCenter)
                {
                    centerCoord[2] = 0.0;
                }

                MolData.ShiftAllDouble(3, sizes, centerCoord, centerPoint, file);

                double[] diam = Methods.GetDiameter(file);

                for (int j = 0; j <= 2; j++)
                {
                    if (Math.Abs(diam[j] - sizes[j]) <= 2)
                    {
                        var shifts = new double[3];
                        shifts[j] = -Methods.CenterAxis_Type2(false, j, sizes[j], centerPoint[j], file);

                        if (j == 2 && !withZCenter)
                        {
                            shifts[j] = 0.0;
                        }
                        MolData.ShiftAllDouble(3, sizes, shifts, centerPoint, file);
                    }
                    else
                    {
                        if (centerCoord[j] < 0.5)
                        {
                            breakmark++;
                        }
                    }
                }

                if ((breakmark == 2 && !withZCenter) || (breakmark == 3 && withZCenter))
                {
                    break;
                }
            }
        }

        #region Infrastructure
        private static void StartProcces(string filename)
        {
            try
            {
                new Process()
                {
                    StartInfo = {
            FileName = filename
          }
                }.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void TextBox_TextChangedInt(object sender, EventArgs e)
        {
            if (isValidInt(((Control)sender).Text))
                ep.SetError((Control)sender, "");
            else
                ep.SetError((Control)sender, "Неправильно введено число!");
        }
        private void TextBox_TextChangedFloat(object sender, EventArgs e)
        {
            if (isValidFloat(((Control)sender).Text))
                ep.SetError((Control)sender, "");
            else
                ep.SetError((Control)sender, "Неправильно введено число!");
        }
        public static double replaceValue(string str)
        {
            str = !(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ".") ? str.Replace(".", ",") : str.Replace(",", ".");
            if (str == "")
                throw new ApplicationException("Имеются незаполненные поля! Убедитесь,что заданы все параметры расчета!");
            return Convert.ToDouble(str);
        }
        private bool isValidFloat(string s)
        {
            s = !(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ".") ? s.Replace(".", ",") : s.Replace(",", ".");
            double result;
            return double.TryParse(s, out result);
        }
        private bool isValidInt(string s)
        {
            int result;
            return int.TryParse(s, out result);
        }


        #endregion

    }
}
