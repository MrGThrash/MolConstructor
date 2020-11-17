using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace MolConstructor
{
    public partial class AnalysisControls : UserControl
    {
        public AnalysisControls()
        {
            InitializeComponent();
            cmbFormat.SelectedIndex = 0;
            cmbCoord.SelectedIndex = 2;
        }

        #region Form controls

        private void chbWithShift_CheckedChanged(object sender, EventArgs e)
        {
            if (chbWithShift.Checked)
                gbShiftValues.Enabled = true;
            else
                gbShiftValues.Enabled = false;
        }

        private void cmbTypeOfResults_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cmbTypeOfResults.SelectedIndex)
            {
                case 0:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Число шагов";
                        dgvDataFromFolder.Columns[1].HeaderText = "Размер по X";
                        dgvDataFromFolder.Columns[2].HeaderText = "Размер по Y";
                        dgvDataFromFolder.Columns[4].HeaderText = "Размер по Z";
                        dgvDataFromFolder.Columns[3].HeaderText = "Радиус по XY";
                        dgvDataFromFolder.Columns[5].HeaderText = "Радиус инерции";
                        dgvDataFromFolder.Columns[6].HeaderText = "Асферичность δ";
                        dgvDataFromFolder.Columns[7].HeaderText = "Асферичность S";
                        break;
                    }
                case 1:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Число шагов";
                        dgvDataFromFolder.Columns[1].HeaderText = "Расстояние от пред. шага";
                        dgvDataFromFolder.Columns[2].HeaderText = "Суммарное расстояние";
                        dgvDataFromFolder.Columns[3].HeaderText = "Квадрат от пред. шага";
                        dgvDataFromFolder.Columns[4].HeaderText = "Квадрат сум. пути";
                        dgvDataFromFolder.Columns[5].HeaderText = "Радиус инерции";
                        break;
                    }
                case 2:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Координата";
                        dgvDataFromFolder.Columns[1].HeaderText = "Толщина пленки";
                        dgvDataFromFolder.Columns[2].HeaderText = "Степень набухания";
                        dgvDataFromFolder.Columns[3].HeaderText = "Количество бидов S";
                        dgvDataFromFolder.Columns[4].HeaderText = "Количество бидов CL";
                        dgvDataFromFolder.Columns[5].HeaderText = "Количество бидов W";
                        break;
                    }
                case 3:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Координата";
                        dgvDataFromFolder.Columns[1].HeaderText = "Полимер C";
                        dgvDataFromFolder.Columns[2].HeaderText = "Полимер O";
                        dgvDataFromFolder.Columns[3].HeaderText = "Полимер N";
                        dgvDataFromFolder.Columns[4].HeaderText = "Полимер P";
                        dgvDataFromFolder.Columns[5].HeaderText = "Растворитель S";
                        dgvDataFromFolder.Columns[6].HeaderText = "Растворитель CL";
                        dgvDataFromFolder.Columns[7].HeaderText = "Растворитель W";
                        dgvDataFromFolder.Columns[8].HeaderText = "Стенки";
                        break;
                    }
                case 4:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Число шагов";
                        dgvDataFromFolder.Columns[1].HeaderText = "Координата";
                        dgvDataFromFolder.Columns[2].HeaderText = "Число концов";
                        dgvDataFromFolder.Columns[3].HeaderText = "Доля концов";
                        dgvDataFromFolder.Columns[4].HeaderText = "Пустая колонка";
                        break;
                    }
                case 5:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Координата";
                        dgvDataFromFolder.Columns[1].HeaderText = "Доля растворителя А вне геля";
                        dgvDataFromFolder.Columns[2].HeaderText = "Доля растворителя А в пределах геля";
                        //dgvDataFromFolder.Columns[3].HeaderText = "Доля голов вне геля";
                        //dgvDataFromFolder.Columns[4].HeaderText = "Доля голов пределах геля";
                        dgvDataFromFolder.Columns[3].HeaderText = "Доля растворителя B вне геля";
                        dgvDataFromFolder.Columns[4].HeaderText = "Доля растворителя B пределах геля";
                        //dgvDataFromFolder.Columns[7].HeaderText = "Полимер C";
                        //dgvDataFromFolder.Columns[8].HeaderText = "Полимер O";
                        //dgvDataFromFolder.Columns[5].HeaderText = "Доля хвостов вне геля";
                        //dgvDataFromFolder.Columns[6].HeaderText = "Доля хвостов в пределах геля";
                        dgvDataFromFolder.Columns[5].HeaderText = "Полимер C";
                        dgvDataFromFolder.Columns[6].HeaderText = "Полимер O";
                        dgvDataFromFolder.Columns[7].HeaderText = "Полимер N";
                        dgvDataFromFolder.Columns[8].HeaderText = "Доля блоков A";
                        dgvDataFromFolder.Columns[9].HeaderText = "Доля блоков B";
                        break;
                    }
                case 6:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "R";
                        dgvDataFromFolder.Columns[1].HeaderText = "Полимер C";
                        dgvDataFromFolder.Columns[2].HeaderText = "Погрешность";
                        dgvDataFromFolder.Columns[3].HeaderText = "Полимер O";
                        dgvDataFromFolder.Columns[4].HeaderText = "Погрешность";
                        dgvDataFromFolder.Columns[5].HeaderText = "Полимер N";
                        dgvDataFromFolder.Columns[6].HeaderText = "Погрешность";
                        dgvDataFromFolder.Columns[7].HeaderText = "Полимер P";
                        dgvDataFromFolder.Columns[8].HeaderText = "Растворитель S";
                        dgvDataFromFolder.Columns[9].HeaderText = "Растворитель CL";
                        dgvDataFromFolder.Columns[10].HeaderText = "Растворитель W";
                        dgvDataFromFolder.Columns[11].HeaderText = "Блоки A (для связей)";
                        dgvDataFromFolder.Columns[12].HeaderText = "Блоки B (для связей)";
                        break;
                    }
                case 7:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Полимер С";
                        dgvDataFromFolder.Columns[1].HeaderText = "Полимер O";
                        dgvDataFromFolder.Columns[2].HeaderText = "Полимер N";
                        dgvDataFromFolder.Columns[3].HeaderText = "  ";
                        dgvDataFromFolder.Columns[4].HeaderText = "  ";
                        dgvDataFromFolder.Columns[5].HeaderText = "  ";
                        dgvDataFromFolder.Columns[6].HeaderText = "  ";
                        dgvDataFromFolder.Columns[7].HeaderText = "  ";
                        break;
                    }
                case 8:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Число бидов";
                        dgvDataFromFolder.Columns[1].HeaderText = "Количество агрегатов";
                        dgvDataFromFolder.Columns[2].HeaderText = "Число молекул";
                        dgvDataFromFolder.Columns[3].HeaderText = "Доля мостиков";
                        dgvDataFromFolder.Columns[4].HeaderText = "Агг. параметры";
                        dgvDataFromFolder.Columns[5].HeaderText = "Погрешность";
                        dgvDataFromFolder.Columns[6].HeaderText = "  ";
                        dgvDataFromFolder.Columns[7].HeaderText = "  ";
                        break;
                    }
                case 9:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "R";
                        dgvDataFromFolder.Columns[1].HeaderText = "Полимер C";
                        dgvDataFromFolder.Columns[2].HeaderText = "Погрешность";
                        dgvDataFromFolder.Columns[3].HeaderText = "Полимер O";
                        dgvDataFromFolder.Columns[4].HeaderText = "Полимер N";
                        dgvDataFromFolder.Columns[5].HeaderText = "Растворитель S";
                        dgvDataFromFolder.Columns[6].HeaderText = "Растворитель CL";
                        dgvDataFromFolder.Columns[7].HeaderText = "Растворитель W";
                        break;
                    }
                case 10:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "R";
                        dgvDataFromFolder.Columns[1].HeaderText = "G(r)";
                        dgvDataFromFolder.Columns[2].HeaderText = "RgXY";
                        dgvDataFromFolder.Columns[3].HeaderText = "RgZ";
                        dgvDataFromFolder.Columns[4].HeaderText = "Ψj";
                        dgvDataFromFolder.Columns[5].HeaderText = "Вороной online";
                        dgvDataFromFolder.Columns[6].HeaderText = "  ";
                        dgvDataFromFolder.Columns[7].HeaderText = "  ";
                        break;
                    }
                case 11:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Rg";
                        dgvDataFromFolder.Columns[1].HeaderText = "V геля";
                        dgvDataFromFolder.Columns[2].HeaderText = "Частицы полимера";
                        dgvDataFromFolder.Columns[3].HeaderText = "Плотность полимера";
                        dgvDataFromFolder.Columns[4].HeaderText = "Плотность общая";
                        dgvDataFromFolder.Columns[5].HeaderText = "Доля полимера";
                        break;
                    }
                case 12:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Тип связи";
                        dgvDataFromFolder.Columns[1].HeaderText = "Длина связи";
                        dgvDataFromFolder.Columns[2].HeaderText = "Количество";
                        dgvDataFromFolder.Columns[3].HeaderText = "Доля от общих длин по типу";
                        dgvDataFromFolder.Columns[4].HeaderText = "  ";
                        dgvDataFromFolder.Columns[5].HeaderText = "  ";
                        dgvDataFromFolder.Columns[6].HeaderText = "  ";
                        dgvDataFromFolder.Columns[7].HeaderText = "  ";
                        break;
                    }
                case 13:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Время";
                        dgvDataFromFolder.Columns[1].HeaderText = "Число бидов продукта";
                        dgvDataFromFolder.Columns[2].HeaderText = "Доля прореагировавших бидов";
                        dgvDataFromFolder.Columns[3].HeaderText = "  ";
                        dgvDataFromFolder.Columns[4].HeaderText = "  ";
                        dgvDataFromFolder.Columns[5].HeaderText = "  ";
                        dgvDataFromFolder.Columns[6].HeaderText = "  ";
                        dgvDataFromFolder.Columns[7].HeaderText = "  ";
                        break;
                    }
                case 14:
                    {
                        dgvDataFromFolder.Columns[0].HeaderText = "Расстояние между концами";
                        dgvDataFromFolder.Columns[1].HeaderText = "Квадрат расстояния";
                        dgvDataFromFolder.Columns[2].HeaderText = "Контурная длина";
                        dgvDataFromFolder.Columns[3].HeaderText = "Средние значения";
                        dgvDataFromFolder.Columns[4].HeaderText = "Погрешности";
                        dgvDataFromFolder.Columns[5].HeaderText = "  ";
                        dgvDataFromFolder.Columns[6].HeaderText = "  ";
                        dgvDataFromFolder.Columns[7].HeaderText = "  ";
                        break;
                    }
            }
            label2.Visible = (cmbTypeOfResults.SelectedIndex != 0 && cmbTypeOfResults.SelectedIndex != 1
                               && cmbTypeOfResults.SelectedIndex != 10 && cmbTypeOfResults.SelectedIndex != 13) ? false : true;
            tbTimeStep.Visible = (cmbTypeOfResults.SelectedIndex > 3
                                  && cmbTypeOfResults.SelectedIndex != 5 && cmbTypeOfResults.SelectedIndex != 6
                                  && cmbTypeOfResults.SelectedIndex != 10 && cmbTypeOfResults.SelectedIndex != 11 && cmbTypeOfResults.SelectedIndex != 13) ? false : true;

            label5.Visible = (cmbTypeOfResults.SelectedIndex != 2 && cmbTypeOfResults.SelectedIndex != 4 && cmbTypeOfResults.SelectedIndex != 8 &&
                                    cmbTypeOfResults.SelectedIndex != 10 && cmbTypeOfResults.SelectedIndex != 13) ? false : true;
            tbChainLength.Visible = (cmbTypeOfResults.SelectedIndex != 10 && cmbTypeOfResults.SelectedIndex != 4 && cmbTypeOfResults.SelectedIndex != 2 &&
                                     cmbTypeOfResults.SelectedIndex != 8 && cmbTypeOfResults.SelectedIndex != 13) ? false : true;

            if (cmbTypeOfResults.SelectedIndex != 10 && cmbTypeOfResults.SelectedIndex != 13) { label5.Text = "Длина цепи"; }
            else if (cmbTypeOfResults.SelectedIndex == 10) { label5.Text = "Число молекул"; }
            else { label5.Text = "Начальное число бидов субстр."; }

            if (cmbTypeOfResults.SelectedIndex == 10) 
            { label2.Text = "Тип порядка Ψj";
              tbTimeStep.Text = "6";
            }
            else 
            { 
                label2.Text = "Шаг по времени";
                tbTimeStep.Text = "100";
            }

            label13.Visible = (cmbTypeOfResults.SelectedIndex >= 3 && cmbTypeOfResults.SelectedIndex <= 5 ||
                                cmbTypeOfResults.SelectedIndex == 7) ? true : false;
            cmbCoord.Visible = (cmbTypeOfResults.SelectedIndex >= 3 && cmbTypeOfResults.SelectedIndex <= 5 ||
                                cmbTypeOfResults.SelectedIndex == 7) ? true : false;


            label7.Visible = (cmbTypeOfResults.SelectedIndex != 8 && cmbTypeOfResults.SelectedIndex < 13) ? false : true;
            tbCoreBead.Visible = (cmbTypeOfResults.SelectedIndex != 8 && cmbTypeOfResults.SelectedIndex < 13) ? false : true;
            if (cmbTypeOfResults.SelectedIndex == 8) { label7.Text = "Тип бидов ядер"; }
            if (cmbTypeOfResults.SelectedIndex == 13) { label7.Text = "Тип бидов продукта"; }
            if (cmbTypeOfResults.SelectedIndex == 14) { label7.Text = "Тип бидов каркаса"; }
            if (cmbTypeOfResults.SelectedIndex != 8)  { lblStep.Text = "Шаг по координате";}
            else
            {
                lblStep.Text = "Радиус поиска";
            }
            if (cmbTypeOfResults.SelectedIndex == 10) { chbAccSepMols.Text = "Без усреднения"; }
            else { chbAccSepMols.Text = "Отд. молекулы"; }


            label6.Enabled = (cmbTypeOfResults.SelectedIndex == 2) ? false : true;
            cmbFormat.Enabled = (cmbTypeOfResults.SelectedIndex == 2 && !chbInBulk.Checked) ? false : true;
            chbAllFiles.Enabled = (cmbTypeOfResults.SelectedIndex < 3 || cmbTypeOfResults.SelectedIndex == 7) ? false : true;
            chbInBulk.Enabled = (cmbTypeOfResults.SelectedIndex != 2 && cmbTypeOfResults.SelectedIndex != 3 && cmbTypeOfResults.SelectedIndex != 7) ? false : true;
            chbByVolume.Enabled = (cmbTypeOfResults.SelectedIndex != 3 && cmbTypeOfResults.SelectedIndex != 5 && cmbTypeOfResults.SelectedIndex != 6) ? false : true;
            chbByCylinder.Enabled = (cmbTypeOfResults.SelectedIndex != 6) ? false : true;
            chbTwoPhaseDense.Visible = (cmbTypeOfResults.SelectedIndex != 6) ? false : true;
            cmbHalfes.Visible = (cmbTypeOfResults.SelectedIndex != 6 && cmbTypeOfResults.SelectedIndex != 7) ? false : true;
            chbHasBonds.Enabled = (cmbTypeOfResults.SelectedIndex != 5 && cmbTypeOfResults.SelectedIndex != 6 && cmbTypeOfResults.SelectedIndex != 8) ? false : true;
            chbByProb.Enabled = (cmbTypeOfResults.SelectedIndex != 8) ? false : true;
            chbAccSepMols.Enabled = (cmbTypeOfResults.SelectedIndex != 8 && cmbTypeOfResults.SelectedIndex != 10) ? false : true;
            label4.Visible = (cmbTypeOfResults.SelectedIndex != 7) ? false : true;
            tbMGArea.Visible = (cmbTypeOfResults.SelectedIndex != 7) ? false : true;
        }

        private void chbWithAutoCenter_CheckedChanged(object sender, EventArgs e)
        {
            tbShiftX.Enabled = !chbWithAutoCenter.Checked;
            tbShiftY.Enabled = !chbWithAutoCenter.Checked;
            label61.Enabled = !chbWithAutoCenter.Checked;
            label59.Enabled = !chbWithAutoCenter.Checked;
            chbAutoCenterZ.Enabled = chbWithAutoCenter.Checked;
        }

        private void chbAutoCenterZ_CheckedChanged(object sender, EventArgs e)
        {
            label58.Enabled = !chbAutoCenterZ.Checked;
            tbShiftZ.Enabled = !chbAutoCenterZ.Checked;
        }

        private void chbTwoPhaseDense_CheckedChanged(object sender, EventArgs e)
        {
            cmbHalfes.Enabled = chbTwoPhaseDense.Checked;
        }

        #endregion

        #region Buttons

        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbDirectoryPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            if (tbDirectoryPath.Text == "")
            {
                MessageBox.Show(null,
                                "Не указан путь к папке с файлами!",
                                "Внимание!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            dgvDataFromFolder.Rows.Clear();

            int step = Convert.ToInt32(tbTimeStep.Text);
            int coord = cmbCoord.SelectedIndex;
            double epsilon = replaceValue(tbEpsilon.Text) / 2.0;

            string format = "*.lammpstrj";
            if (cmbFormat.SelectedIndex == 1)
            {
                format = "*.mol2";
            }
            if (cmbTypeOfResults.SelectedIndex == 2)
            {
                if (!chbInBulk.Checked)
                {
                    format = "*.dat";
                }
                else
                {
                    format = "*.xyzr";
                }
            }

            var files = Directory.GetFiles(tbDirectoryPath.Text, format).OrderBy(f => new FileInfo(f).LastWriteTime).ToList();

            if (chbAllFolders.Checked)
            {
                var folders = Directory.GetDirectories(tbDirectoryPath.Text);

                foreach (var c in folders)
                {
                    files.AddRange(Directory.GetFiles(c, format).OrderBy(f => f));
                }
            }

            if (files.Count() == 0)
            {
                MessageBox.Show(null,
                                "В папке нет файлов указанного формата!",
                                "Внимание!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }


            var fileNames = new string[files.Count()];

            var index = cmbTypeOfResults.SelectedIndex;

            for (int i = 0; i < fileNames.Length; i++)
            {
                if (index == 2 && !chbInBulk.Checked)
                {
                    if (files.ElementAt(i).EndsWith("cheS.dat"))
                        fileNames[i] = files.ElementAt(i);
                }
                else
                    fileNames[i] = files.ElementAt(i);
            }

            if (index < 3 || index == 11)
            {

                double[] shifts = new double[3];

                if (chbWithShift.Checked)
                {
                    shifts[0] = replaceValue(tbShiftX.Text);
                    shifts[1] = replaceValue(tbShiftY.Text);
                    shifts[2] = replaceValue(tbShiftZ.Text);
                }

                btnCancel.Visible = true;
                pBar.Value = 0;
                pBar.Visible = true;
                bgWorker.RunWorkerAsync(new object[] { cmbFormat.SelectedIndex, epsilon,
                                                       step, shifts, chbWithAutoCenter.Checked,
                                                       chbAutoCenterZ.Checked, Convert.ToInt32(tbTimeStep.Text),
                                                       fileNames, cmbTypeOfResults.SelectedIndex });
            }
            else
            {
                var neededFiles = files.ToArray();
                var beadType = FileWorker.AtomTypes[(int)replaceValue(tbCoreBead.Text)];

                if (!chbAllFiles.Checked) { neededFiles = new string[] { files.ElementAt(files.Count() - 1) }; }

                btnCancel.Visible = true;
                pBar.Value = 0;
                pBar.Visible = true;

                if (index == 3)
                {
                    bgWorkerFilmProfile.RunWorkerAsync(new object[] { cmbFormat.SelectedIndex,
                                                                      epsilon, neededFiles,chbAllFiles.Checked,
                                                                      coord, chbByVolume.Checked,
                                                                      chbInBulk.Checked, cmbTypeOfResults.SelectedIndex});
                }
                else if (index == 4)
                {
                    CalcEndsDestribution(cmbFormat.SelectedIndex, neededFiles, Convert.ToInt32(tbChainLength.Text), epsilon, coord);
                    btnCancel.Visible = false;
                    pBar.Visible = false;
                }
                else if (index == 5)
                {
                    bgWorkerSolv.RunWorkerAsync(new object[] { cmbFormat.SelectedIndex,
                                                               epsilon, neededFiles, coord, chbByVolume.Checked,
                                                               chbWithAutoCenter.Checked,chbAutoCenterZ.Checked,
                                                               Convert.ToInt32(tbTimeStep.Text), chbHasBonds.Checked, cmbTypeOfResults.SelectedIndex});
                }
                else if (index == 6)
                {
                    bgWorkerRadialDensity.RunWorkerAsync(new object[] { cmbFormat.SelectedIndex,
                                                                        epsilon, neededFiles, chbByVolume.Checked,
                                                                        chbByCylinder.Checked,
                                                                        chbWithAutoCenter.Checked,
                                                                        chbAutoCenterZ.Checked, Convert.ToInt32(tbTimeStep.Text),
                                                                        chbTwoPhaseDense.Checked, cmbHalfes.SelectedIndex,
                                                                        chbHasBonds.Checked, cmbTypeOfResults.SelectedIndex });
                }
                else if (index == 7)
                {
                    bgWorker2DHeatMap.RunWorkerAsync(new object[] {cmbFormat.SelectedIndex,
                                                                   epsilon, neededFiles,coord,
                                                                   replaceValue(tbMGArea.Text),
                                                                   chbInBulk.Checked, cmbHalfes.SelectedIndex,
                                                                   cmbTypeOfResults.SelectedIndex});
                }

                else if (index == 10)
                {
                    bgWorker2Dorder.RunWorkerAsync(new object[] { cmbFormat.SelectedIndex, neededFiles, epsilon, Convert.ToInt32(tbTimeStep.Text),
                                                                  Convert.ToInt32(tbChainLength.Text), chbAccSepMols.Checked,
                                                                  cmbTypeOfResults.SelectedIndex });
                }

                else if (index == 12)
                {
                    bgWorkerBondLength.RunWorkerAsync(new object[] { cmbFormat.SelectedIndex, tbDirectoryPath.Text, neededFiles, cmbTypeOfResults.SelectedIndex });
                }

                else if (index == 13)
                {
                    bgWorkerCatalRate.RunWorkerAsync(new object[] { cmbFormat.SelectedIndex, neededFiles, step, Convert.ToInt32(tbChainLength.Text),
                                                                    beadType, cmbTypeOfResults.SelectedIndex });
                }

                else if (index == 14)
                {
                    bgWorkerLarina.RunWorkerAsync(new object[] { cmbFormat.SelectedIndex, neededFiles,chbWithAutoCenter.Checked,chbAutoCenterZ.Checked,
                                                                 beadType, cmbTypeOfResults.SelectedIndex });
                }

                else
                {
                    bool hasBonds = chbHasBonds.Checked;
                                       
                    //if (hasBonds)
                    //{
                    //    if (!File.Exists(bondsPath))
                    //    {
                    //        MessageBox.Show("Вы указали расчет с учетом связей,\n + но в указанной папке нет файла bonds.dat!");
                    //        return;
                    //    }
                    //}


                    bgWorkerAggNumber.RunWorkerAsync(new object[] { cmbFormat.SelectedIndex,beadType,
                                                                    epsilon,hasBonds, tbDirectoryPath.Text, chbByProb.Checked,
                                                                    chbAccSepMols.Checked, Convert.ToInt32(tbChainLength.Text),
                                                                    neededFiles, cmbTypeOfResults.SelectedIndex });
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
            }
            else if (bgWorkerSolv.IsBusy)
            {
                bgWorkerSolv.CancelAsync();
            }
            else if (bgWorkerFilmProfile.IsBusy)
            {
                bgWorkerFilmProfile.CancelAsync();
            }
            else if (bgWorkerRadialDensity.IsBusy)
            {
                bgWorkerRadialDensity.CancelAsync();
            }
            else if (bgWorker2Dorder.IsBusy)
            {
                bgWorker2Dorder.CancelAsync();
            }
            else if (bgWorkerAggNumber.IsBusy)
            {
                bgWorkerAggNumber.CancelAsync();
            }
            else if (bgWorkerBondLength.IsBusy)
            {
                bgWorkerBondLength.CancelAsync();
            }
            else if (bgWorkerCatalRate.IsBusy)
            {
                bgWorkerCatalRate.CancelAsync();
            }
            else
            {
                bgWorker2DHeatMap.CancelAsync();
            }
        }

        #endregion

        private void CalcEndsDestribution(int format, string[] filenames, int chainLength, double epsilon, int coord)
        {
            var obtainedData = new List<double[]>();

            for (int k = 0; k < filenames.Length; k++)
            {
                var file = new List<double[]>();
                double[] sizes = new double[3];

                if (format == 0)
                {
                    file = FileWorker.LoadXyzrLines(filenames[k]);

                    sizes[0] = Math.Abs(file[file.Count - 1][0] - file[file.Count - 8][0]);
                    sizes[1] = Math.Abs(file[file.Count - 1][1] - file[file.Count - 8][1]);
                    sizes[2] = Math.Abs(file[file.Count - 1][2]);
                }
                else
                {
                    int snapnum = 0;
                    file = FileWorker.LoadLammpstrjLines(filenames[k], out snapnum, out sizes);
                }

                var centerPoint = MolData.GetCenterPoint(sizes, file);

                double size = sizes[coord];
                if (centerPoint[coord] == 0.0)
                {
                    foreach (var c in file)
                    {
                        c[coord] += sizes[coord] / 2.0;
                    }
                }

                int steps = (int)(size / (2.0 * epsilon));

                var polymer = file.Where(x => x[3] == 1.00 ||
                                              x[3] == 1.01 ||
                                              x[3] == 1.04).ToList();

                int chainCount = polymer.Count / chainLength;

                for (int i = 0; i <= steps; i++)
                {
                    int endsCount = 0;

                    for (int j = chainLength - 1; j < polymer.Count; j += chainLength)
                    {
                        if (polymer[j][coord] > (i * 2 - 1) * epsilon &&
                            polymer[j][coord] <= (i * 2 + 1) * epsilon)
                            endsCount++;
                    }

                    double[] row = new double[]
               {
                   0.0,
                   i*2*epsilon,
                   endsCount,
                   Math.Round((double)endsCount/(double)chainCount,3),
                   0.0               };


                    if (obtainedData.Count <= steps)
                    {
                        obtainedData.Add(row);
                    }
                    else
                    {
                        for (int j = 1; j < row.Length; j++)
                        {
                            obtainedData[i][j] += row[j];
                        }
                    }
                }
            }

            foreach (var c in obtainedData)
            {
                for (int i = 1; i < c.Length - 1; i++)
                {
                    c[i] /= (double)filenames.Length;
                }
            }

            bgWorker_RunWorkerCompleted(new object[] { }, new RunWorkerCompletedEventArgs(new object[] { obtainedData,
                                                                                                          cmbTypeOfResults.SelectedIndex }, null, false));
        }

       

        #region BackgroundWorkers

        #region Basic BG
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;

            var format = (int)args[0];
            var epsilon = (double)args[1];
            var step = (int)args[2];
            var shifts = (double[])args[3];
            var autoCenter = (bool)args[4];
            var centerZ = (bool)args[5];
            var autoCenterIter = (int)args[6];
            var files = (string[])args[7];
            var calcType = (int)args[8];

            var obtainedData = new List<double[]>();
            if (calcType != 3)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    if (!bgWorker.CancellationPending)
                    {
                        if (calcType != 2)
                        {
                            var file = new List<double[]>();
                            double[] sizes = new double[3];

                            readTableFile(format, files[i], out file, out sizes);

                            var centerPoint = MolData.GetCenterPoint(sizes, file);

                            if (shifts[0] != 0 || shifts[1] != 0 || shifts[2] != 0 ||
                                autoCenter)
                            {

                                if (autoCenter && calcType != 1)
                                {
                                    doAutoCenter(centerZ, autoCenterIter, sizes, centerPoint, file);
                                }
                                else
                                {
                                    MolData.ShiftAllDouble(3, sizes, shifts, centerPoint, file);
                                }
                            }

                            if (calcType != 1)
                            {

                                double[] dataRow = new double[dgvDataFromFolder.Columns.Count];

                                if (calcType == 11)
                                {
                                    dataRow[0] = Methods.GetHydroDiameter(file);

                                    //var realHydroDiam = Math.Sqrt(5.0 * Math.Pow(dataRow[0], 2) / 3.0);
                                    var realHydroDiam =  dataRow[0]/(2.0 * epsilon);

                                    dataRow[1] = 4.0 * Math.PI * Math.Pow(realHydroDiam, 3) / 3.0;

                                    var cm = Methods.GetCenterMass(file);

                                    int gelRadCount = 0;
                                    int particleCount = 0;

                                    for (int f = 0; f < file.Count; f++)
                                    {
                                        if (Methods.GetDistance(file[f][0], file[f][1], file[f][2], cm[0], cm[1], cm[2]) <= realHydroDiam)
                                        {
                                            particleCount++;
                                            if (file[f][3].Equals(1.00) || file[f][3].Equals(1.01) || file[f][3].Equals(1.04))
                                            {
                                                gelRadCount++;
                                            }
                                        }
                                    }

                                    dataRow[1] = Math.Round(dataRow[1], 3);
                                    dataRow[2] = gelRadCount;
                                    dataRow[3] = Math.Round(gelRadCount / dataRow[1], 3);
                                    dataRow[4] = Math.Round(particleCount / dataRow[1], 3);
                                    dataRow[5] = Math.Round(dataRow[3] / dataRow[4], 3);

                                }
                                else
                                {
                                    dataRow[0] = step * i;
                                    dataRow[1] = Math.Round(Math.Sqrt(Methods.GetAxInertSquareRadius(file, 0)), 3);
                                    dataRow[2] = Math.Round(Math.Sqrt(Methods.GetAxInertSquareRadius(file, 1)), 3);
                                    dataRow[3] = Methods.GetHydroRadius2D(file);
                                    dataRow[4] = Math.Round(Math.Sqrt(Methods.GetAxInertSquareRadius(file, 2)), 3);
                                    dataRow[5] = Methods.GetHydroDiameter(file);

                                    var shapes = Methods.GetShapeCharacteristics(file);
                                    
                                    dataRow[6] = shapes[0];
                                    dataRow[7] = shapes[1];
                                }

                                obtainedData.Add(dataRow);
                            }
                            else
                            {
                                double[] dataRow = new double[dgvDataFromFolder.Columns.Count + 4];

                                dataRow[0] = step * i;
                                dataRow[5] = Methods.GetHydroDiameter(file);

                                if (i != 0)
                                {
                                    var centerMass = Methods.GetCenterMassWithPBC(sizes, file);

                                    dataRow[1] = Methods.GetDistance(obtainedData[i - 1][6], obtainedData[i - 1][7],
                                                                          obtainedData[i - 1][8], centerMass[0],
                                                                         centerMass[1], centerMass[2]);
                                    dataRow[2] = dataRow[1] + obtainedData[i - 1][2];

                                    dataRow[3] = Math.Pow(dataRow[1], 2);
                                    dataRow[4] = Math.Pow(dataRow[2], 2);
                                    dataRow[6] = centerMass[0];
                                    dataRow[7] = centerMass[1];
                                    dataRow[8] = centerMass[2];
                                }
                                else
                                {
                                    dataRow[1] = 0.0;
                                    dataRow[2] = 0.0;

                                    var centerMass = Methods.GetCenterMassWithPBC(sizes, file);

                                    dataRow[6] = centerMass[0];
                                    dataRow[7] = centerMass[1];
                                    dataRow[8] = centerMass[2];

                                }

                                obtainedData.Add(dataRow);
                            }
                        }
                        else
                        {
                            if (files[i] != null)
                            {
                                double height = 0;
                                double swellRate = 1.0;
                                double solvACount = 0;
                                double solvBCount = 0;
                                double waterCount = 0;
                                if (files[i].EndsWith("cheS.dat"))
                                {
                                    var snapshot = FileWorker.LoadXyzrLines(files[i]);

                                    for (int j = 1; j < snapshot.Count; j++)
                                    {
                                        if ((snapshot[j][1] + snapshot[j][4]) >= 500.0)
                                        {
                                            solvACount += snapshot[j][3];
                                            solvACount += snapshot[j][7];
                                            waterCount += snapshot[j][8];
                                        }
                                        else
                                        {
                                            height = snapshot[j][0];
                                            break;
                                        }
                                    }
                                    if (obtainedData.Count != 0)
                                    {
                                        swellRate = height / obtainedData[0][1];
                                    }
                                }
                                else
                                {
                                    var file = new List<double[]>();
                                    double[] sizes = new double[3];

                                    readTableFile(format, files[i], out file, out sizes);

                                    double size = sizes[2];

                                    int steps = (int)(size / (2.0 * epsilon));
                                    var layerVol = sizes[0] * sizes[1] * 2.0 * epsilon;

                                    var heightData = new List<double[]>();

                                    for (int k = 0; k <= steps; k++)
                                    {
                                        int polOneCount = 0;
                                        int polTwoCount = 0;
                                        int polThreeCount = 0;
                                        int totalcount = 0;

                                        var layer = file.Where(x => x[2] > (k * 2 - 1) * epsilon &&
                                                                    x[2] <= (k * 2 + 1) * epsilon &&
                                                                    x[3] != 1.080).ToList();

                                        totalcount += layer.Count;
                                        if (totalcount == 0)
                                        {
                                            totalcount++;
                                        }

                                        foreach (var c in layer)
                                        {
                                            if (c[3] == 1.000)
                                            {
                                                polOneCount++;
                                            }
                                            if (c[3] == 1.010)
                                            {
                                                polTwoCount++;
                                            }
                                            if (c[3] == 1.040)
                                            {
                                                polThreeCount++;
                                            }
                                        }

                                        if ((polOneCount + polTwoCount + polThreeCount) == 0)
                                        {
                                            if (totalcount == 1)
                                            {
                                                polOneCount = 1;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            heightData.Add(new double[] { k*2*epsilon,
                                                             (double)polOneCount/layerVol,
                                                             (double)polTwoCount/layerVol,
                                                             (double)polThreeCount/layerVol,
                                                             (totalcount)});
                                        }
                                    }

                                    height = Methods.GetIntegralHeight(heightData);


                                    //swellRate = StructFormer.GetDifferentialHeight(heightData);

                                    // height by concentration
                                    foreach (var c in heightData)
                                    {
                                        c[1] = ((c[1] + c[2] + c[3]) * layerVol) / c[4];
                                    }

                                    var swellheight = heightData.Where(x => x[1] <= 0.2 && x[1] >= 0.1).ToList();
                                    if (swellheight.Count != 0)
                                    {
                                        swellRate = swellheight[swellheight.Count - 1][0];
                                    }

                                    obtainedData.Add(new double[] { step * i, height, swellRate, solvACount, solvBCount, waterCount });
                                }
                            }
                        }

                        int barStep = (int)(files.Length / 100.0);
                        if (barStep == 0)
                        {
                            barStep++;
                        }

                        if (i % barStep == 0)
                        {
                            ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)i) / ((double)files.Length)));
                        }
                    }
                    else
                    {
                        e.Result = new object[] { };
                    }
                }
            }

            e.Result = new object[] { obtainedData, calcType };
        }
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }
        #endregion

        #region BG Film profile
        private void bgWorkerFilmProfile_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;
            var format = (int)args[0];
            var epsilon = (double)args[1];
            var filenames = (string[])args[2];
            var forAllfiles = (bool)args[3];
            var coord = (int)args[4];
            var byvolume = (bool)args[5];
            var inbulk = (bool)args[6];
            var index = (int)args[7];

            var obtainedData = new List<double[]>();

            double[] sizes = new double[3];
            double[] centerPoint = new double[3];

            int fileBegin = 0;

            if (!forAllfiles)
            {
                fileBegin = filenames.Length - 1;
            }

            for (int k = fileBegin; k < filenames.Length; k++)
            {
                if (!bgWorkerFilmProfile.CancellationPending)
                {
                    var file = new List<double[]>();

                    readTableFile(format, filenames[k], out file, out sizes);

                    if (k == fileBegin)
                    {
                        centerPoint = MolData.GetCenterPoint(sizes, file);
                    }

                    double size = sizes[coord];
                    double area = 1.0;
                    for (int i = 0; i <= 2; i++)
                    {
                        if (i != coord)
                        {
                            area *= sizes[i];
                        }
                    }

                    if (centerPoint[coord] == 0.0)
                    {
                        foreach (var c in file)
                        {
                            c[coord] += sizes[coord] / 2.0;
                        }
                    }

                    int steps = (int)(size / (2.0 * epsilon));

                    for (int i = 0; i <= steps; i++)
                    {
                        int polOneCount = 0;
                        int polTwoCount = 0;
                        int polThreeCount = 0;
                        int polFourCount = 0;
                        int solvAcount = 0;
                        int solvBcount = 0;
                        int watercount = 0;
                        int wallcount = 0;
                        int totalcount = 0;

                        var layer = file.Where(x => x[coord] > (i * 2 - 1) * epsilon &&
                                                    x[coord] <= (i * 2 + 1) * epsilon).ToList();


                        totalcount += layer.Count;
                        if (totalcount == 0)
                        {
                            totalcount++;
                        }

                        foreach (var c in layer)
                        {
                            if (c[3] == 1.000)
                            {
                                polOneCount++;
                            }
                            if (c[3] == 1.010)
                            {
                                polTwoCount++;
                            }
                            if (c[3] == 1.040)
                            {
                                polThreeCount++;
                            }
                            if (c[3] == 1.050)
                            {
                                polFourCount++;
                            }
                            if (c[3] == 1.020)
                            {
                                solvAcount++;
                            }
                            if (c[3] == 1.070)
                            {
                                solvBcount++;
                            }
                            if (c[3] == 1.030)
                            {
                                watercount++;
                            }
                            if (c[3] == 1.080 || c[3] == 1.090)
                            {
                                wallcount++;
                            }
                        }

                        double[] row = new double[]
               {
                   i*2*epsilon,
                   (double)polOneCount/(double)totalcount,
                   (double)polTwoCount/(double)totalcount,
                   (double)polThreeCount/(double)totalcount,
                   (double)polFourCount/(double)totalcount,
                   (double)solvAcount/(double)totalcount,
                   (double)solvBcount/(double)totalcount,
                   (double)watercount/(double)totalcount,
                   (double)wallcount/(double)totalcount
               };
                        if (byvolume)
                        {
                            row = new double[]
               {
                   i*2*epsilon,
                   (double)polOneCount/(area*2*epsilon),
                   (double)polTwoCount/(area*2*epsilon),
                   (double)polThreeCount/(area*2*epsilon),
                   (double)polFourCount/(area*2*epsilon),
                   (double)solvAcount/(area*2*epsilon),
                   (double)solvBcount/(area*2*epsilon),
                   (double)watercount/(area*2*epsilon),
                   (double)wallcount/(area*2*epsilon)
               };
                        }

                        if (obtainedData.Count <= steps)
                        {
                            obtainedData.Add(row);
                        }
                        else
                        {
                            for (int j = 1; j < row.Length; j++)
                            {
                                obtainedData[i][j] += row[j];
                            }
                        }
                    }

                    int barStep = (int)(filenames.Length / 100.0);
                    if (barStep == 0)
                    {
                        barStep++;
                    }

                    if (k % barStep == 0)
                    {
                        ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)k) / ((double)filenames.Length)));
                    }
                }
                else
                {
                    e.Result = new object[] { };
                }
            }

            double mult = (double)filenames.Length;

            if (!forAllfiles)
            {
                mult = 1.0;
            }



            foreach (var c in obtainedData)
            {
                for (int i = 1; i < c.Length; i++)
                {
                    c[i] = Math.Round(c[i] / mult, 3);
                }
            }

            if (inbulk)
            {
                double height = Methods.GetIntegralHeight(obtainedData);


                foreach (var c in obtainedData)
                {
                    c[0] /= height;
                }

                obtainedData.Add(new double[]
               {
                   height,
                   0.0,
                   0.0,
                   0.0,
                   0.0,
                   0.0,
                   0.0
               });
                //layer = layer.Where(x => x[2] <= height).ToList();
            }

            e.Result = new object[] { obtainedData, index };
        }
        private void bgWorkerFilmProfile_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }
        private void bgWorkeFilmProfile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }
        #endregion

        #region BG Solv
        private void bgWorkerSolv_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;

            var format = (int)args[0];
            var epsilon = (double)args[1];
            var filenames = (string[])args[2];
            var coord = (int)args[3];
            var byVolume = (bool)args[4];
            var withAutoCenter = (bool)args[5];
            var withZCenter = (bool)args[6];
            var autoCenterIters = (int)args[7];
            var hasBonds = (bool)args[8];
            var index = (int)args[9];

            var obtainedData = new List<double[]>();

            //double molDiam = 0.0;

            var centerPoint = new double[3];

            var bondsAndSegments = new List<MolData>();
            var segmentLength = 0;

            for (int k = 0, len = filenames.Length; k < len; k++)
            {
                try
                {
                    if (!bgWorkerSolv.CancellationPending)
                    {
                        var file = new List<double[]>();
                        double[] sizes = new double[3];

                        // reading the file
                        readTableFile(format, filenames[k], out file, out sizes);

                        if (k == 0)
                        {
                            centerPoint = MolData.GetCenterPoint(sizes, file);
                        }

                        if (hasBonds)
                        {
                            if (k == 0 || !Path.GetDirectoryName(filenames[k - 1]).Equals(Path.GetDirectoryName(filenames[k])))
                            {
                                defineSegments(format, filenames[k], bondsAndSegments);

                                if (k == 0)
                                {
                                    segmentLength = bondsAndSegments.Max(x => x.MolIndex);
                                }
                            }
                        }

                        // center structure if needed
                        if (withAutoCenter)
                        {
                            doAutoCenter(withZCenter, autoCenterIters, sizes, centerPoint, file);
                        }

                        double size = sizes[coord];

                        if (centerPoint[coord] == 0.0)
                        {
                            foreach (var c in file)
                            {
                                c[coord] += sizes[coord] / 2.0;
                            }
                        }

                        // determing the number of steps according to the epsilon
                        int steps = (int)(size / (2.0 * epsilon));

                        double[] centerMass = Methods.GetCenterMass(file);
                        double diameter = Methods.GetDiameter(file).Max();

                        double radius = diameter / 6.0;
                        int coord2 = Math.Abs(coord - 2);

                        if (coord < 2)
                        {
                            coord2 = 2;
                            radius = Methods.GetDiameter(file)[2] / 4.0;
                        }
                        else
                        {
                            radius = Methods.GetHydroRadius2D(file) / 3.0;
                        }

                        int polCcount, polOcount, polNcount, solvARoundCount, solvAcount, solvBRoundCount, solvBcount;
                        //int solvCRoundCount, solvCcount;
                        int totalRoundCount, totalCount;
                        double[] segmentsA = new double[1];
                        double[] segmentsB = new double[1];

                        if (hasBonds)
                        {
                            segmentsA = new double[segmentLength];
                            segmentsB = new double[segmentLength];
                        }

                        for (int i = 0; i <= steps; i++)
                        {
                            // clear all values before the next step
                            polCcount = 0;
                            polOcount = 0;
                            polNcount = 0;
                            solvARoundCount = 0;
                            solvAcount = 0;
                            solvBRoundCount = 0;
                            solvBcount = 0;
                            //solvCRoundCount = 0;
                            //solvCcount = 0;

                            totalRoundCount = 0;
                            totalCount = 0;

                            for (int p = 0; p < segmentsA.Length; p++)
                            {
                                segmentsA[p] = 0;
                                segmentsB[p] = 0;
                            }

                            // slice within the fixed variable 'coord' 
                            var layer = file.Where(x => x[coord] > (i * 2 - 1) * epsilon &&
                                                        x[coord] <= (i * 2 + 1) * epsilon &&
                                                        x[3] != 1.080).ToList();

                            if (byVolume)
                            {
                                int[] sideCoords = new int[] { 0, 1 };

                                if (coord <= 1)
                                {
                                    sideCoords[coord] = 2;
                                }

                                radius = Methods.GetMolSliceRadius(layer, sideCoords[0], sideCoords[1], centerMass);

                            }

                            foreach (var c in layer)
                            {

                                double distance = Methods.GetDistance(c[Math.Abs(coord - 1)], c[coord2], 0.0,
                                                                           centerMass[Math.Abs(coord - 1)],
                                                                           centerMass[coord2], 0.0);

                                if (distance <= radius)
                                {
                                    totalRoundCount++;
                                    if (c[3] == 1.000)
                                    {
                                        polCcount++;
                                        if (hasBonds)
                                        {
                                            var ind = bondsAndSegments[(int)c[4]].MolIndex;

                                            if (ind <= segmentLength)
                                            {
                                                segmentsA[ind - 1]++;
                                            }
                                        }
                                    }
                                    if (c[3] == 1.010 /*c[3] == 1.05*/)
                                    {
                                        polOcount++;
                                        if (hasBonds)
                                        {
                                            var ind = bondsAndSegments[(int)c[4]].MolIndex;

                                            if (ind <= segmentLength)
                                            {
                                                segmentsB[ind - 1]++;
                                            }
                                        }
                                    }
                                    if (c[3] == 1.040 /*c[3] == 1.05*/)
                                    {
                                        polNcount++;
                                    }
                                    if (c[3] == 1.020) { solvBRoundCount++; }
                                    //if (c[3] == 1.040) { solvCRoundCount++; }
                                    if (c[3] == 1.030) { solvARoundCount++; }
                                }
                                else
                                {
                                    if (distance > diameter / 2.0)
                                    {
                                        totalCount++;
                                        if (c[3] == 1.020) { solvBcount++; }
                                        //if (c[3] == 1.040) { solvCcount++; }
                                        if (c[3] == 1.030) { solvAcount++; }
                                    }
                                }
                            }

                            if (totalCount == 0) { totalCount = 1; }
                            if (totalRoundCount == 0) { totalRoundCount = 1; }

                            if (radius == 0)
                            {
                                solvARoundCount = solvAcount;
                                solvBRoundCount = solvBcount;
                                totalRoundCount = totalCount;
                            }

                            double[] row = new double[8 + segmentLength * 2];

                            row[0] = i * 2 * epsilon;
                            row[1] = (double)solvAcount / (double)totalCount;
                            row[2] = (double)solvARoundCount / (double)totalRoundCount;
                            row[3] = (double)solvBcount / (double)totalCount;
                            row[4] = (double)solvBRoundCount / (double)totalRoundCount;
                            //row[5] = (double)solvCcount / (double)totalCount;
                            //row[6] = (double)solvCRoundCount / (double)totalRoundCount;            
                            row[5] = (double)polCcount / (double)totalRoundCount;
                            row[6] = (double)polOcount / (double)totalRoundCount;
                            row[7] = (double)polNcount / (double)totalRoundCount;


                            if (hasBonds)
                            {
                                for (int p = 0; p < segmentLength; p++)
                                {
                                    if (polCcount > 0)
                                    {
                                        row[8 + p] = segmentsA[p] / (double)totalRoundCount;
                                    }
                                    if (polOcount > 0)
                                    {
                                        row[8 + p + segmentLength] = segmentsB[p] / (double)totalRoundCount;
                                    }
                                }
                            }
                            //if (byVolume && radius != 0)
                            //{
                            //    double beadVol = 1.0 / 3.0;
                            //    double sliceVol = 2.0 * Math.PI * Math.Pow(radius, 2) * epsilon;
                            //    double fullVol = 2.0 * xSize * ySize * epsilon;

                            //    row[1] = (double)solvAcount * beadVol / (fullVol - sliceVol);
                            //    row[2] = (double)solvARoundCount * beadVol / sliceVol;
                            //    row[3] = (double)solvBcount * beadVol / (fullVol - sliceVol);
                            //    row[4] = (double)solvBRoundCount * beadVol / sliceVol;
                            //    row[5] = (double)polCcount * beadVol / sliceVol;
                            //    row[6] = (double)polOcount * beadVol / sliceVol;
                            //}

                            if (obtainedData.Count <= steps)
                            {
                                obtainedData.Add(row);
                            }
                            else
                            {
                                for (int j = 1; j < row.Length; j++)
                                {
                                    obtainedData[i][j] += row[j];
                                }
                            }
                        }

                        int barStep = (int)(filenames.Length / 100.0);
                        if (barStep == 0)
                        {
                            barStep++;
                        }

                        if (k % barStep == 0)
                        {
                            ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)k) / ((double)filenames.Length)));
                        }
                    }
                    else
                    {
                        e.Result = new object[] { };
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка в файле №" + filenames[k] + ". Причина ошибки:\n" + ex.ToString(),
                                    "Ошибка!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    e.Result = null;
                }
            }

            foreach (var c in obtainedData)
            {
                for (int i = 1; i < c.Length; i++)
                {
                    c[i] = Math.Round(c[i] / (double)filenames.Length, 3);
                }
            }
            e.Result = new object[] { obtainedData, index };
        }
        private void bgWorkerSolv_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }
        private void bgWorkerSolv_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }

        private void defineSegments(int format, string fileName, List<MolData> bondsAndSegments)
        {
            //if (!Path.GetDirectoryName(fileName).Equals(Path.GetDirectoryName(bondsPath)) || bondsAndSegments.Count == 0)
            //{

            // do it once for all cases 'cause the only random thing is the distribution of blocks but no the bonds themselves
            //if (bondsAndSegments.Count == 0)
            //{
            bondsAndSegments.Clear();

            var bondsPath = Path.GetDirectoryName(fileName);

            var file = new List<double[]>();
            double[] sizes = new double[3];

            readTableFile(format, fileName, out file, out sizes);

            var initBonds = FileWorker.LoadBonds(bondsPath, format);
            // extracting only A and B polymer types
            for (int i = 0; i < file.Count; i++)
            {
                if (file[i][3] == 1.00 || file[i][3] == 1.01)
                {
                    bondsAndSegments.Add(new MolData(file[i][3], i + 1, 0, 0, 0, true));
                }
            }

            // adding bonds
            var atoms = bondsAndSegments.Count;
            foreach (var b in initBonds)
            {
                if (b[0] <= atoms)
                {
                    bondsAndSegments[b[0] - 1].Bonds.Add(b[1]);
                    bondsAndSegments[b[1] - 1].Bonds.Add(b[0]);
                }
            }

            // now after we have all the bonds we construct the segments lengths and write them to the MolIndex property since
            // we have only 1 mG molecule
            var ccounter = 0;

            for (int i = 0; i < bondsAndSegments.Count; i++)
            {
                if (bondsAndSegments[i].Bonds.Count > 2)
                {
                    bondsAndSegments[i].MolIndex = 1;
                    ccounter++;
                }
                else
                {
                    if (bondsAndSegments[i].MolIndex == 0)
                    {
                        var segment = new List<MolData>();

                        segment.Add(bondsAndSegments[i]);

                        var segmentLength = 0;

                        do
                        {
                            segmentLength = segment.Count;

                            for (int k = 0; k < segment.Count; k++)
                            {
                                for (int f = 0; f <= 1; f++)
                                {
                                    if (bondsAndSegments[segment[k].Bonds[f] - 1].Bonds.Count <= 2 &&
                                        bondsAndSegments[segment[k].Bonds[f] - 1].AtomType.Equals(segment[k].AtomType) &&
                                        segment.Where(x => x.Index == segment[k].Bonds[f]).ToList().Count() == 0)
                                    {
                                        segment.Add(bondsAndSegments[segment[k].Bonds[f] - 1]);
                                    }

                                    // in case of beads with only one bond
                                    if (segment[k].Bonds.Count == 1)
                                    {
                                        break;
                                    }
                                }
                            }

                        } while (segmentLength < segment.Count);

                        foreach (var c in segment)
                        {
                            c.MolIndex = segmentLength;
                        }
                    }
                }
            }

            MessageBox.Show(ccounter.ToString());
            //}
            //else
            //{
            //    return;
            //}
       
        }
        #endregion

        #region BG Rad. density
        private void bgWorkerRadialDensity_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;

            var format = (int)args[0];
            var epsilon = (double)args[1];
            var filenames = (string[])args[2];
            var byVolume = (bool)args[3];
            var byCylinder = (bool)args[4];
            var withAutoCenter = (bool)args[5];
            var withZCenter = (bool)args[6];
            var autoCenterIter = (int)args[7];
            var twoPhases = (bool)args[8];
            var half = (int)args[9];
            var hasBonds = (bool)args[10];
            var index = (int)args[11];

            var obtainedData = new List<double[]>();

            var bondsAndSegments = new List<MolData>();
            var segmentLength = 0;


            double[] sizes = new double[3];
            double[] centerPoint = new double[3];

            for (int k = 0, len = filenames.Length; k < len; k++)
            {
                try
                {
                    if (!bgWorkerRadialDensity.CancellationPending)
                    {
                        var file = new List<double[]>();

                        readTableFile(format, filenames[k], out file, out sizes);

                        int steps = (int)(Math.Min(sizes[0], sizes[1]) / (4.0 * epsilon));

                        if (k == 0)
                        {
                            centerPoint = MolData.GetCenterPoint(sizes, file);
                        }

                        if (hasBonds)
                        {
                            if (k == 0 || !Path.GetDirectoryName(filenames[k - 1]).Equals(Path.GetDirectoryName(filenames[k])))
                            {
                                defineSegments(format, filenames[k], bondsAndSegments);

                                if (k == 0)
                                {
                                    segmentLength = bondsAndSegments.Max(x => x.MolIndex);
                                }
                                //for (int i=0; i< segmentLength; i++)
                                //{
                                //    var segACount = bondsAndSegments.Where(x => x.MolIndex == (i + 1) && x.AtomType == 1.000).ToList().Count/(i+1);
                                //    var segBCount = bondsAndSegments.Where(x => x.MolIndex == (i + 1) && x.AtomType == 1.01).ToList().Count/(i+1);

                                //    MessageBox.Show("Сегментов длиной " + (i + 1).ToString() + " для А = " + segACount.ToString() + " для B = " + segBCount.ToString());

                                //}

                            }
                        }

                        if (withAutoCenter)
                        {
                            doAutoCenter(withZCenter, autoCenterIter, sizes, centerPoint, file);
                        }

                        double[] centerMass = Methods.GetCenterMass(file);

                        double[] heights = Methods.GetHeights(file);

                        var diameter = Methods.GetDiameter(file);

                        double lowLim = 0.0;
                        double upperLim = 0.0;

                        if (byCylinder)
                        {
                            lowLim = heights[1] - diameter[2] - 2.0 * epsilon;
                            upperLim = (heights[1] + 2.0 * epsilon);

                            if (twoPhases)
                            {
                                var solvA = file.Where(x => x[3] == 1.03).ToList().Count();
                                var solvB = file.Where(x => x[3] == 1.02).ToList().Count();

                                if (Math.Abs(solvA - solvB) > 100)
                                {
                                    double centercoord = solvA / (solvA + solvB) * sizes[2];

                                    if (solvA < solvB)
                                    {
                                        centercoord = (double)solvB / (double)(solvA + solvB) * sizes[2];
                                    }

                                    if (half <= 0)
                                    {
                                        lowLim = heights[0] + (centercoord - heights[0]) / 2.0;
                                        upperLim = centercoord;
                                    }
                                    else
                                    {
                                        lowLim = centercoord;
                                        upperLim = heights[1] + (centercoord - heights[1]) / 2.0;
                                    }
                                }
                                else
                                {
                                    if (half <= 0)
                                    {
                                        lowLim = centerMass[2] - diameter[2] / 4.0;
                                        upperLim = centerMass[2];


                                        //lowLim = Math.Max(((sizes[2] / 2.0) - diameter[2] / 4.0), heights[0]);
                                        //upperLim = (sizes[2]/2.0);
                                    }
                                    else
                                    {


                                        lowLim = centerMass[2];
                                        upperLim = centerMass[2] + diameter[2] / 4.0;

                                        //lowLim = (sizes[2] / 2.0)+2;
                                        //upperLim = Math.Min(((sizes[2] / 2.0) + diameter[2] / 4.0), heights[1]);
                                    }
                                }
                            }
                        }

                        double[] segmentsA = new double[1];
                        double[] segmentsB = new double[1];

                        if (hasBonds)
                        {
                            segmentsA = new double[segmentLength];
                            segmentsB = new double[segmentLength];
                        }

                        for (int i = 0; i <= steps; i++)
                        {
                            int polCcount = 0;
                            int polOcount = 0;
                            int polNcount = 0;
                            int polPcount = 0;
                            int solvACount = 0;
                            int solvBCount = 0;
                            int waterCount = 0;

                            // either the total number of beads in the layer or the volume of the layer if byVolume is selected
                            double divider = 0.0;

                            for (int p = 0; p < segmentsA.Length; p++)
                            {
                                segmentsA[p] = 0;
                                segmentsB[p] = 0;
                            }

                            var layer = new List<double[]>();

                            if (byCylinder)
                            {
                                layer = file.Where(x => Methods.GetDistance(x[0], x[1], 0.0,
                                                        centerMass[0], centerMass[1], 0.0) > (i * 2 - 1) * epsilon &&
                                                        Methods.GetDistance(x[0], x[1], 0.0,
                                                        centerMass[0], centerMass[1], 0.0) <= (i * 2 + 1) * epsilon &&
                                                        x[2] >= lowLim && x[2] <= upperLim
                                                        && x[3] != 1.080).ToList();

                                if (byVolume)
                                {
                                    var area = Math.PI * Math.Pow(epsilon, 2) * (Math.Pow((i * 2 + 1), 2) - Math.Pow((i * 2 - 1), 2));
                                    if (i == 0)
                                    {
                                        area = Math.PI * Math.Pow(epsilon, 2);
                                    }

                                    divider = (upperLim - lowLim) * area;
                                }
                            }
                            else
                            {

                                lowLim = (i * 2 - 1) * epsilon;

                                if (i == 0)
                                {
                                    lowLim = 0;
                                }

                                upperLim = (i * 2 + 1) * epsilon;

                                layer = file.Where(x => Math.Sqrt(Math.Pow(x[0] - centerMass[0], 2) + Math.Pow(x[1] - centerMass[1], 2) +
                                                                  Math.Pow(x[2] - centerMass[2], 2)) > lowLim &&
                                                                  Math.Sqrt(Math.Pow(x[0] - centerMass[0], 2) + Math.Pow(x[1] - centerMass[1], 2) +
                                                                  Math.Pow(x[2] - centerMass[2], 2)) <= upperLim).ToList();

                                if (byVolume)
                                {
                                    divider = (4.0 / 3.0) * Math.PI * (Math.Pow(upperLim, 3) - Math.Pow(lowLim, 3));
                                }
                            }

                            if (!byVolume)
                            {
                                divider = layer.Count;
                            }

                            foreach (var c in layer)
                            {
                                if (c[3] == 1.000)
                                {
                                    polCcount++;

                                    if (hasBonds)
                                    {
                                        var ind = bondsAndSegments[(int)c[4]].MolIndex;

                                        if (ind <= segmentLength)
                                        {
                                            segmentsA[ind - 1]++;
                                        }
                                    }
                                }
                                if (c[3] == 1.010)
                                {
                                    polOcount++;
                                    if (hasBonds)
                                    {
                                        var ind = bondsAndSegments[(int)c[4]].MolIndex;

                                        if (ind <= segmentLength)
                                        {
                                            segmentsB[ind - 1]++;
                                        }
                                    }
                                }
                                if (c[3] == 1.05)
                                {
                                    polPcount++;
                                }
                                if (c[3] == 1.02)
                                {
                                    solvACount++;
                                }
                                if (c[3] == 1.06)
                                {
                                    solvBCount++;
                                }
                                if (c[3] == 1.03)
                                {
                                    waterCount++;
                                }
                                if (c[3] == 1.04)
                                {
                                    polNcount++;
                                }
                            }

                            if (divider == 0)
                            {
                                divider = 1;
                            }


                            double[] row = new double[11 + filenames.Length * 3];

                            if (hasBonds)
                            {
                                row = new double[8 + segmentLength * 2];
                            }

                            row[0] = i * 2 * epsilon;
                            row[1] = polCcount / divider;
                            row[2] = polOcount / divider;
                            row[3] = polNcount / divider;
                            row[4] = polPcount / divider;
                            row[5] = solvACount / divider;
                            row[6] = solvBCount / divider;
                            row[7] = waterCount / divider;

                            if (!hasBonds)
                            {
                                row[8] = row[1];
                                row[9] = row[2];
                                row[10] = row[3];
                            }
                            else
                            {
                                for (int p = 0; p < segmentLength; p++)
                                {
                                    if (polCcount > 0)
                                    {
                                        row[8 + p] = segmentsA[p] / divider;
                                    }
                                    if (polOcount > 0)
                                    {
                                        row[8 + p + segmentLength] = segmentsB[p] / divider;
                                    }
                                }
                            }

                            if (obtainedData.Count <= steps)
                            {
                                obtainedData.Add(row);
                            }
                            else
                            {
                                for (int j = 1; j <= 7; j++)
                                {
                                    obtainedData[i][j] += row[j];
                                }

                                if (hasBonds)
                                {
                                    for (int j = 8; j < row.Length; j++)
                                    {
                                        obtainedData[i][j] += row[j];
                                    }
                                }
                                else
                                {
                                    obtainedData[i][8 + k] = row[1];
                                    obtainedData[i][8 + k + filenames.Length] = row[2];
                                    obtainedData[i][8 + k + filenames.Length * 2] = row[3];
                                }

                            }
                        }

                        file.Clear();

                        int barStep = (int)(filenames.Length / 100.0);
                        if (barStep == 0)
                        {
                            barStep++;
                        }

                        if (k % barStep == 0)
                        {
                            ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)k) / ((double)filenames.Length)));
                        }
                    }
                    else
                    {
                        e.Result = new object[] { };
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка в файле №" + filenames[k] + ". Причина ошибки:\n" + ex.ToString(),
                                    "Ошибка!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    e.Result = null;
                }
            }

            foreach (var c in obtainedData)
            {
                for (int i = 1; i <= 7; i++)
                {
                    c[i] = Math.Round(c[i] / (double)filenames.Length, 3);
                }

                if (hasBonds)
                {
                    for (int i = 8; i < c.Length; i++)
                    {
                        c[i] = Math.Round(c[i] / (double)filenames.Length, 3);
                    }
                }
                else
                {
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        for (int f = 1; f <= 3; f++)
                        {
                            c[c.Length - (4 - f)] += Math.Pow(c[f] - c[8 + i + (filenames.Length) * (f - 1)], 2);
                        }
                    }

                    for (int f = 1; f <= 3; f++)
                    {
                        c[c.Length - f] = Math.Round(Math.Sqrt(c[c.Length - f] / ((double)filenames.Length - 1)), 3);
                    }
                }
            }
            e.Result = new object[] { obtainedData, index,  };
        }

        private void bgWorkerRadialDensity_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void bgWorkerRadialDensity_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }
        #endregion

        #region Bg Surf Coverage
        private void bgWorker2DHeatMap_DoWork(object sender, DoWorkEventArgs e)
        {

            var args = (object[])e.Argument;

            var format = (int)args[0];
            var step = (double)args[1];
            var filenames = (string[])args[2];
            var coord = (int)args[3];
            var coordSlice = (double)args[4];
            var byHeight = (bool)args[5];
            var half = (int)args[6];

            var index = (int)args[7];

            var obtainedData = new List<double[]>();

            var axOne = 0; // axis X
            var axTwo = 1; // axis Y

            if (coord == 0)
            {
                axOne = 1;
                axTwo = 2;
            }
            if (coord == 1)
            {
                axTwo = 2;
            }

            for (int k = 0; k < filenames.Length; k++)
            {
                if (!bgWorker.CancellationPending)
                {
                    var file = new List<double[]>();
                    double[] sizes = new double[3];

                    if (format == 1)
                    {
                        file = FileWorker.LoadXyzrLines(filenames[k]);

                        sizes[0] = Math.Abs(file[file.Count - 1][0] - file[file.Count - 8][0]);
                        sizes[1] = Math.Abs(file[file.Count - 1][1] - file[file.Count - 8][1]);
                        sizes[2] = Math.Abs(file[file.Count - 1][2]);
                    }
                    else
                    {
                        int snapnum = 0;
                        file = FileWorker.LoadLammpstrjLines(filenames[k], out snapnum, out sizes);
                    }

                    var centerPoint = MolData.GetCenterPoint(sizes, file);

                    int pointsAxOne = (int)(sizes[axOne]);
                    int pointsAxTwo = (int)(sizes[axTwo]);

                    var slice = new List<double[]>();

                    // Lattice calc
                    if (!byHeight)
                    {
                        slice = file.Where(x => Math.Abs(x[coord] - coordSlice) <= step).ToList();
                    }
                    else
                    {
                        if (half <= 0)
                        {
                            // crop at the interface
                            slice = file.Where(x => x[coord] <= coordSlice).ToList();
                        }
                        else
                        {
                            // crop at the interface
                            slice = file.Where(x => x[coord] >= coordSlice).ToList();
                        }
                        // leave only polymers
                        slice = slice.Where(x => x[3].Equals(1.00) || x[3].Equals(1.01) ||
                                                        x[3].Equals(1.04) || x[3].Equals(1.05)).ToList();
                    }

                    // Columns - by Y
                    for (int i = 1; i <= pointsAxTwo; i++)
                    {
                        var line = new double[pointsAxOne];

                        // Rows = by X
                        for (int j = 1; j <= pointsAxOne; j++)
                        {
                            double coef1 = centerPoint[axOne] - sizes[axOne] / 2.0, coef2 = centerPoint[axTwo] - sizes[axTwo] / 2.0;

                            //var pol = file.Where(x => x[3].Equals(1.00) || x[3].Equals(1.01)
                            //                           || x[3].Equals(1.04) || x[3].Equals(1.05)).ToList();

                            var cell = slice.Where(x =>  x[axOne] >= (j - 1 - coef1) && x[axOne] <= (j - coef1)
                                                      && x[axTwo] >= (i - 1 - coef2) && x[axTwo] <= (i - coef2)).ToList();
                            if (!byHeight)
                            {
                                int unitPolC = cell.Where(x => x[3].Equals(1.00)).ToList().Count;
                                int unitPolO = cell.Where(x => x[3].Equals(1.01)).ToList().Count;
                                int unitPolN = cell.Where(x => x[3].Equals(1.04)).ToList().Count;

                                double cellFracC = (double)unitPolC / (double)cell.Count();
                                double cellFracO = (double)unitPolO / (double)cell.Count();
                                double cellFracN = (double)unitPolN / (double)cell.Count();
                                line[j - 1] = cellFracC;
                            }
                            else
                            {
                                var val = 0.0;

                                if (cell.Count != 0)
                                {
                                    if (half <= 0)
                                    {
                                        val = coordSlice - cell.Min(x => x[coord]);
                                    }
                                    else
                                    {
                                        val = cell.Max(x => x[coord])-coordSlice;
                                    }
                                }

                                //if (half <=0)
                                //{
                                //    line[pointsAxTwo - j] = val;
                                //}
                                //else
                                //{
                                    line[j - 1] = val;
                                //}
                            }               
                        }

                        obtainedData.Add(line);
                    }


               //   obtainedData.Add(new double[] { step * (k + 1), surfFrac });

                    int barStep = (int)(filenames.Length / 100.0);
                    if (barStep == 0)
                    {
                        barStep++;
                    }

                    if (k % barStep == 0)
                    {
                        ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)k) / ((double)filenames.Length)));
                    }
                }
                else
                {
                    e.Result = new object[] { };
                }
            }

            if (half <= 0)
            {
                obtainedData.Reverse();
            }

            e.Result = new object[] { obtainedData, index };

        }

        private void bgWorker2DHeatMap_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void bgWorker2DHeatMap_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }
        #endregion

        #region 2D orientational order
        private void bgWorker2Dorder_DoWork(object sender, DoWorkEventArgs e)
        {

           var args = (object[])e.Argument;

            var format = (int)args[0];
            var filenames = (string[])args[1];
            var epsilon = (double)args[2];
            var orderType = (int)args[3];
            var molNum = (int)args[4];
            var isDyn = (bool)args[5];
            var index = (int)args[6];

            var obtainedData = new List<double[]>();
            var psis = new List<double>();
            var Rgs = new List<double[]>();
            var RgXY = 0.0;
            var RgZ = 0.0;
            var RgXYErr = 0.0;
            var RgZErr = 0.0;
            var psi6 = 0.0;
            var psiErr = 0.0;

            var voronSites = "";
            

            for (int k = 0; k < filenames.Length; k++)
            {
                if (!bgWorker.CancellationPending)
                {
                    var file = new List<double[]>();
                    double[] sizes = new double[3];

                    readTableFile(format, filenames[k], out file, out sizes);

                    var molSizes = new double [2];

                    var molCenters = getVoronoiBase(molNum, sizes, file, out molSizes, out voronSites);  

                    //var filnmae = filenames[k].TrimEnd(".lammpstrj".ToCharArray());
                    //filnmae += "-cms.lammpstrj";
                    //FileWorker.SaveLammpstrj(false, filnmae, 1, sizes, 3, molCenters);

                    //Add images
                    add2DPeriodImages(sizes, molCenters);

                    psis.Add(Methods.GetPsiXParam(orderType, molNum, Math.Min(sizes[0], sizes[1]), molCenters));
                    Rgs.Add(molSizes);

                    if (!isDyn)
                    {
                        double density = molNum / (sizes[0] * sizes[1]);

                        var rdf = Methods.CalcRadialDistFunc(0, molNum, 2 * epsilon, Math.Min(sizes[0], sizes[1]), density, molCenters);

                        if (k == 0)
                        {
                            foreach (var c in rdf)
                            {
                                obtainedData.Add(new double[] { c[0], c[1] });
                            }
                        }
                        else
                        {
                            for (int i = 0; i < obtainedData.Count; i++)
                            {
                                obtainedData[i][1] += rdf[i][1];
                            }
                        }
                    }

                    int barStep = (int)(filenames.Length / 100.0);
                    if (barStep == 0)
                    {
                        barStep++;
                    }

                    if (k % barStep == 0)
                    {
                        ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)k) / ((double)filenames.Length)));
                    }
                }
                else
                {
                    e.Result = new object[] { };
                }
            }
            if (isDyn)
            {
                for (int i = 0; i < psis.Count; i++)
                {
                    obtainedData.Add(new double[] { Math.Round(Rgs[i][0],3), Math.Round(Rgs[i][1], 3), Math.Round(psis[i],3)});
                }
            }
            else
            {

                foreach (var c in obtainedData)
                {
                    c[1] /= filenames.Length;
                }

                for (int i=0; i< psis.Count; i++)
                {
                    psi6 += psis[i];
                    RgXY += Rgs[i][0];
                    RgZ += Rgs[i][1];

                }
                psi6 /= filenames.Length;
                RgXY /= filenames.Length;
                RgZ /= filenames.Length;

                for (int i = 0; i < filenames.Length; i++)
                {
                    psiErr += Math.Pow(psis[i] - psi6, 2);
                    RgXYErr += Math.Pow(Rgs[i][0] - RgXY, 2);
                    RgZErr += Math.Pow(Rgs[i][1] - RgZ, 2);
                }

                psiErr = Math.Sqrt(psiErr / (filenames.Length - 1));
                RgXYErr = Math.Sqrt(RgXYErr / (filenames.Length - 1));
                RgZErr = Math.Sqrt(RgZErr / (filenames.Length - 1));
            }

                e.Result = new object[] { obtainedData, index, Math.Round(RgXY, 3), Math.Round(RgXYErr, 3), Math.Round(RgZ, 3), Math.Round(RgZErr, 3),
                                         Math.Round(psi6,3), Math.Round(psiErr, 3), voronSites, isDyn };
        }

        private void bgWorker2Dorder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void bgWorker2Dorder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }

        private List<MolData> getVoronoiBase(int molNum, double[] sizes, List<double[]> data, out double[] molSizes, out string sites)
        {
            var voron = new List<MolData>();

            var polMass = (data.Where(x => x[3] == 1.00 || x[3] == 1.01 || x[3] == 1.04).ToList().Count()) / molNum;
            //if (actualPol != molCount)
            //{
            //    MessageBox.Show("Ошибка: число молекул не совпадает с числом введенным. Всего молекул: " + ((int)actualPol).ToString(),
            //                        "Ошибка!",
            //                        MessageBoxButtons.OK,
            //                        MessageBoxIcon.Error);
            //    return;
            //}

            var points = new List<double[]>();

            var molDiam = 0.0;
            molSizes = new double[2] { 0.0, 0.0 };

            int counter = 0;

            var bordermols = new List<int[]>(); // list of molecules on the periodic border

            // get search radius
            for (int i = 0; i < molNum; i++)
            {
                var mol = data.Skip(i * polMass).Take(polMass).ToList();
                var diam = Methods.GetDiameter(mol);

                molSizes[1] += Math.Sqrt(Methods.GetAxInertSquareRadius(mol, 2));

                if (diam[0] < 0.85 *sizes[0] && diam[1] < 0.85*sizes[1])
                {
                    molDiam += diam.Max();
                    molSizes[0] += Methods.GetHydroRadius2D(mol);
                    counter++;
                    var cm = Methods.GetCenterMass(mol);
                    points.Add(new double[] { cm[0], cm[1] });
                }
                else
                {
                    int sign = 0; // 0 means that the molecule has periodicity by X axis
                    if (diam[1] > 0.85 * sizes[1])
                    {
                        if (diam[0] > 0.85 * sizes[0])
                        {
                            sign = 2; // the molecule in the corner
                        }
                        else
                        {
                            sign = 1;
                        }
                    }

                    bordermols.Add(new int[] { i, sign });
                }
            }
            molSizes[1] /= molNum;
           
            if (molDiam == 0.0)
            {
                molDiam = molSizes[1];
            }
            else
            {
                molDiam /= counter;
                molSizes[0] /= counter;
            }

            // Get rid of periodicity
            foreach (var c in bordermols)
            {
                var mol = data.Skip(c[0] * polMass).Take(polMass).ToList();
                var normMol = new List<double[]>();

                if (c[1] < 2)
                {
                    // All beads will be rewtiten to left upper corner
                    var minCoord = mol.Min(x => x[c[1]]);
                    foreach (var d in mol)
                    {
                        if (Math.Abs(minCoord - d[c[1]]) < molDiam * 1.1)
                        {
                            normMol.Add(d);
                        }
                        else
                        {
                            if (c[1] == 0)
                                normMol.Add(new double[] { d[0] - sizes[0], d[1], d[2], d[3] });
                            else
                                normMol.Add(new double[] { d[0], d[1] - sizes[1], d[2], d[3] });
                        }
                    }
                }
                else
                {
                    var minCoord = mol.Min(x => x[0]);

                    var intermol = new List<double[]>();

                    // shift by x
                    foreach (var d in mol)
                    {
                        if (Math.Abs(minCoord - d[0]) < molDiam * 1.1)
                        {
                            intermol.Add(d);
                        }
                        else
                        {
                            intermol.Add(new double[] { d[0] - sizes[0], d[1], d[2], d[3] });
                        }
                    }

                    foreach (var d in intermol)
                    {
                        if (Math.Abs(minCoord - d[1]) < molDiam * 1.1)
                        {
                            normMol.Add(d);
                        }
                        else
                        {
                            normMol.Add(new double[] { d[0], d[1] - sizes[1], d[2], d[3] });
                        }
                    }
                }
                
                var cm = Methods.GetCenterMass(normMol);

                if (molSizes[0] == 0)
                {
                    molSizes[0] += Methods.GetHydroRadius2D(normMol);
                }

                points.Add(new double[] { cm[0], cm[1] });
            }

            // XY Images - explicit account of periodic conditions
            for (int i = 0; i < molNum; i++)
            {
                points.Add(new double[] { points[i][0] - sizes[0], points[i][1] });
                points.Add(new double[] { points[i][0] + sizes[0], points[i][1] });
                points.Add(new double[] { points[i][0], points[i][1] - sizes[1] });
                points.Add(new double[] { points[i][0], points[i][1] + sizes[1] });

                points.Add(new double[] { points[i][0] - sizes[0], points[i][1] - sizes[1] });
                points.Add(new double[] { points[i][0] + sizes[0], points[i][1] + sizes[1] });
                points.Add(new double[] { points[i][0] + sizes[0], points[i][1] - sizes[1] });
                points.Add(new double[] { points[i][0] - sizes[0], points[i][1] + sizes[1] });
            }
            for (int i=0; i< molNum; i++)
            {
                voron.Add(new MolData(1.00, i + 1, points[i][0], points[i][1], sizes[2]/3.0));
            }


            foreach (var c in voron)
            {
                if (c.XCoord<0) { c.XCoord += sizes[0]; }
                if (c.YCoord < 0) { c.YCoord += sizes[1]; }
            }

            sites = "{\"sites\":[";

            foreach (var c in points)
            {
                // We orienting on alex beutel's coord system
                var beut = (c[1] - sizes[1] / 2.0)*10;
                sites += (int)(c[0]*10) + "," + (int)beut + ",";
            }

            sites = sites.Remove(sites.Length - 1);

            sites += "],\"queries\":[]}";

            return voron;
        }
        private void add2DPeriodImages(double[] sizes, List<MolData> points)
        {
           int cnt = points.Count;
           for (int i=0; i< cnt; i++)
            {
                points.Add(new MolData(1.01, points.Count + 1, points[i].XCoord + sizes[0], points[i].XCoord, 0.0));
                points.Add(new MolData(1.02, points.Count + 1, points[i].XCoord - sizes[0], points[i].YCoord, 0.0));
                points.Add(new MolData(1.03, points.Count + 1, points[i].XCoord, points[i].YCoord + sizes[1], 0.0));
                points.Add(new MolData(1.04, points.Count + 1, points[i].XCoord, points[i].YCoord - sizes[1], 0.0));
                points.Add(new MolData(1.05, points.Count + 1, points[i].XCoord + sizes[0], points[i].YCoord + sizes[1], 0.0));
                points.Add(new MolData(1.06, points.Count + 1, points[i].XCoord + sizes[0], points[i].YCoord - sizes[1], 0.0));
                points.Add(new MolData(1.07, points.Count + 1, points[i].XCoord - sizes[0], points[i].YCoord + sizes[1], 0.0));
                points.Add(new MolData(1.08, points.Count + 1, points[i].XCoord - sizes[0], points[i].YCoord - sizes[1], 0.0));
            }
        }

   
        #endregion

        #region Agg. number

        // main method for calculating the aggregation number 
        private void bgWorkerAggNumber_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;

            var format = (int)args[0];
            var beadType = (double)args[1];
            var epsilon = (double)args[2];
            var hasBonds = (bool)args[3];
            var bondsPath = (string)args[4];
            var byProbs = (bool)args[5];
            var byMols = (bool)args[6];
            var molLength = (int)args[7];
            var filenames = (string[])args[8];
            var index = (int)args[9];

            double radius = epsilon * 2.0;

            var obtainedData = new List<double[]>();
            var initCoreBeads = new List<MolData>();
            var finalBeads = new List<MolData>();

            double[] sizes = new double[3];
            double[] centerPoint = new double[3];


            var meanN = new List<int>(); // mean Agg num
            var meanW = new List<int>(); // mean weight num
            var meanPeak = new List<int>();
            var allBridges = new List<int>();
            var snapData = new List<int[]>();
            int blockBead = 0;

            double meanClusterNumber = 0.0;


            var bonds = new List<Bond>();

            if (hasBonds)
            {
                var initBonds = FileWorker.LoadBonds(bondsPath, format);

                bonds = Bond.OrderBonds(initBonds);
            }

            for (int k = 0; k < filenames.Length; k++)
            {
                try
                {
                    snapData.Clear();

                    if (!bgWorkerAggNumber.CancellationPending)
                    {
                        var file = new List<double[]>();


                        readTableFile(format, filenames[k], out file, out sizes);

                        // Define the length of blocks for each molecule (usually only when only single-type mol) 
                        //if (byMols && k == 0)
                        //{
                        //    int molCount = file.Where(x => x[3].Equals(1.00) || x[3].Equals(1.01) || x[3].Equals(1.04)).ToList().Count / molLength;

                        //    blockBead = file.Where(x => x[3].Equals(beadType)).ToList().Count / molCount;
                        //}

                        // retriveing all the beads from the final state to initial List 
                        if (finalBeads.Count != 0)
                        {
                            for (int i = finalBeads.Count - 1; i >= 0; i--)
                            {
                                finalBeads[i].AtomType = beadType;
                                initCoreBeads.Add(finalBeads[i]);
                                finalBeads.RemoveAt(i);
                            }
                        }

                        // first snapshot
                        if (k == 0)
                        {
                            centerPoint = MolData.GetCenterPoint(sizes, file);

                            for (int i = 0; i < file.Count; i++)
                            {
                                if (file[i][3].Equals(beadType))
                                {
                                    // We do it only once since the algorithm is aimed to work with one configuartion 
                                    // (even if we deal with several  randomly generated ones the bonds molecules order will be the same
                                    // since the generation procedure is done by us also) 

                                    initCoreBeads.Add(new MolData(beadType, i + 1, file[i][0], file[i][1], file[i][2]));
                                    if (hasBonds)
                                    {
                                        initCoreBeads.Last().Bonds = bonds.First(x => x.Index == i + 1).Neighbors;
                                    }
                                    //if (byMols)
                                    //{
                                    //    initCoreBeads.Last().MolIndex = (i / molLength + 1);
                                    //}

                                }
                            }
                        }
                        else
                        {
                            // set the coordinates of core beads to their values in the actual snapshot
                            foreach (var c in initCoreBeads)
                            {
                                c.XCoord = file[c.Index - 1][0];
                                c.YCoord = file[c.Index - 1][1];
                                c.ZCoord = file[c.Index - 1][2];
                            }
                        }

                        int totalCounter = 0;

                        var core = new List<MolData>();

                        do
                        {
                            
                            // initial agg determination
                            //var currbead = initCoreBeads[0];
                            //    Methods.GetOneAggregate_Recursion(hasBonds,beadType,radius,sizes,centerPoint,currbead,core, initCoreBeads);

                            Methods.GetOneAggregate(core, hasBonds, beadType, radius, sizes, centerPoint, initCoreBeads);
                           
                            totalCounter++;

                            // after the initial agg is determined it then transfers to the finaBeads List
                            // its atomType is defined as the number of the aggregate
                            for (int i = core.Count - 1; i >= 0; i--)
                            {
                                core[i].AtomType = totalCounter;
                                finalBeads.Add(core[i]);
                                core.RemoveAt(i);
                            }

                        } while (initCoreBeads.Count > 0);

                        // the procedure to merge the aggregates with the nearby small ones. I'll do it later...
                        //do
                        //{

                        //}

                        meanClusterNumber += totalCounter;

                        for (int i = 1; i <= totalCounter; i++)
                        {
                            core = finalBeads.Where(x => x.AtomType == i).ToList();

                            if (obtainedData.Count == 0)
                            {
                                obtainedData.Add(new double[] { core.Count, 0, 1, 0.0, 0.0, 0.0, 0.0, 0.0 });
                            }
                            else
                            {
                                int matchInd = obtainedData.FindIndex(x => x[0] == core.Count);

                                if (matchInd != -1)
                                {
                                    obtainedData[matchInd][2]++;
                                }
                                else
                                {
                                    obtainedData.Add(new double[] { core.Count, 0, 1, 0.0, 0.0, 0.0, 0.0, 0.0 });
                                }
                            }

                            // Count the bridges between the cores
                            //if (byMols)
                            //{
                            //    double molS = 0.0;
                            //    double bridges = 0.0;

                            //    var fullmols = 0;

                            //    var molsInCore = core.Select(x => x.MolIndex).Distinct().ToList();

                            //    foreach (var c in molsInCore)
                            //    {
                            //        // if the whole molecule is in the agg the skip it and count as the 'fullmol' 
                            //        var fullmol = core.Where(x => x.MolIndex == c).ToList();
                            //        if (fullmol.Count == blockBead)
                            //        {
                            //            fullmols++;
                            //        }
                            //        else
                            //        {
                            //            // search for the other parts of mols in the other aggs
                            //            if (fullmol[0].Charge == 0)
                            //            {
                            //                var bridgesBeads = finalBeads.Where(x => x.MolIndex == c && x.AtomType != core[0].AtomType).Distinct().ToList();

                            //                int molbridges = bridgesBeads.Select(x => x.MolIndex).Distinct().ToList().Count;

                            //                foreach (var p in fullmol)
                            //                {
                            //                    p.Charge = molbridges;
                            //                }
                            //            }
                            //        }
                            //    }

                            //    molS = (double)core.Count / (double)blockBead;

                            //    if (molS == molsInCore.Count())
                            //    {
                            //        bridges = 0.0;
                            //    }
                            //    else if (core.Count < molLength)
                            //    {
                            //        if (molsInCore.Count == 1)
                            //        {
                            //            bridges = 0.0;
                            //        }
                            //        else
                            //        {
                            //            bridges = 1.0;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        bridges = 1.0 - ((double)fullmols / (double)molsInCore.Count);
                            //    }

                            //    int matchInd = obtainedData.FindIndex(x => x[0] == core.Count);
                            //    obtainedData[matchInd][5] += molS;
                            //    obtainedData[matchInd][6] += bridges;
                            //}
                        }

                        // Order the number of aggregates by ascend
                        obtainedData = obtainedData.OrderBy(x => x[0]).ToList();

                        int barStep = (int)(filenames.Length / 100.0);
                        if (barStep == 0)
                        {
                            barStep++;
                        }

                        if ((k + 1) % barStep == 0)
                        {
                            ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)(k + 1)) / ((double)filenames.Length)));
                        }

                        // Probability calculation
                        double sum = 1.0;

                        if (byProbs)
                        {
                            sum = 0.0;
                            foreach (var c in obtainedData)
                            {
                                sum += c[2];
                            }

                            var mN = 0.0;
                            var mW = 0.0;

                            foreach (var c in obtainedData)
                            {
                                mN += (c[0] * c[2] / sum);
                                mW += (Math.Pow(c[0], 2) * c[2] / sum);
                            }

                            meanN.Add((int)mN);
                            meanW.Add((int)(mW / mN));
                        }

                        //if (byMols)
                        //{
                        //    var snapBridges = finalBeads.Where(x => x.Charge != 0).Distinct().ToList();
                        //    var brg = 0.0;

                        //    foreach (var c in snapBridges)
                        //    {
                        //        brg += c.Charge;
                        //    }

                        //    allBridges.Add((int)brg);
                        //}

                        obtainedData = obtainedData.OrderBy(x => x[2]).ToList();
                        meanPeak.Add((int)obtainedData.Last()[0]);

                        foreach (var c in obtainedData)
                        {
                            c[1] += c[2] / sum;

                            //if (byMols)
                            //{
                            //    if (c[2] != 0)
                            //    {
                            //        c[3] += c[5] / c[2];
                            //        c[4] += c[6] / c[2];
                            //        c[5] = 0;
                            //        c[6] = 0;
                            //        c[7]++;
                            //    }
                            //}

                            c[2] = 0;
                        }
                    }
                    else
                    {
                        e.Result = new object[] { };
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка в файле №" + filenames[k] + ". Причина ошибки:\n" + ex.ToString(),
                                    "Ошибка!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    e.Result = null;
                }
            }

            // Order the number of aggregates by ascend
            obtainedData = obtainedData.OrderBy(x => x[0]).ToList();

            meanClusterNumber /= filenames.Length;

            foreach (var c in obtainedData)
            {

                c[1] = Math.Round(c[1] / (double)meanPeak.Count, 3);

                if (byMols)
                {
                    c[0] /= molLength; 
                }

                //if (byMols)
                //{
                //    c[2] = Math.Round(c[3] / c[7], 3);
                //    c[3] = Math.Round(c[4] / c[7], 3);
                //    c[4] = 0;
                //}
            }

            // final procedure for the statistics
            if (obtainedData.Count < 4)
            {
                do
                {
                    obtainedData.Add(new double[] { 0.0, 0, 0.0, 0.0, 0.0, 0.0, 0.0 });
                } while (obtainedData.Count < 4);
            }

            var mNSum = 0.0;
            var mWSum = 0.0;
            var mPeakSum = 0.0;
            var mNError = 0.0;
            var mWError = 0.0;
            var mPeakError = 0.0;

            for (int i = 0; i < meanN.Count; i++)
            {
                mNSum += meanN[i];
                mWSum += meanW[i];
                mPeakSum += meanPeak[i];
            }

            mNSum /= meanN.Count;
            mWSum /= meanN.Count;
            mPeakSum /= meanN.Count;

            for (int i = 0; i < meanN.Count; i++)
            {
                mNError += Math.Pow(mNSum - meanN[i], 2);
                mWError += Math.Pow(mWSum - meanW[i], 2);
                mPeakError += Math.Pow(mPeakSum - meanPeak[i], 2);
            }

            mNError = Math.Sqrt(mNError / (meanN.Count - 1));
            mWError = Math.Sqrt(mWError / (meanN.Count - 1));
            mPeakError = Math.Sqrt(mPeakError / (meanN.Count - 1));

            obtainedData[0][4] = (int)mNSum;
            obtainedData[1][4] = (int)mWSum;
            obtainedData[2][4] = (int)mPeakSum;
            obtainedData[3][4] = Math.Round(meanClusterNumber,1);
            obtainedData[0][5] = (int)mNError;
            obtainedData[1][5] = (int)mWError;
            obtainedData[2][5] = (int)mPeakError;

            if (byMols)
            {

                obtainedData[0][4] /= molLength;
                obtainedData[1][4] /= molLength;
                obtainedData[2][4] /= molLength;
                obtainedData[0][5] /= molLength;
                obtainedData[1][5] /= molLength;
                obtainedData[2][5] /= molLength;
            }

            //if (byMols)
            //{
            //    var allBridgesSum = 0.0;
            //    var bridgesError = 0.0;

            //    foreach (var c in allBridges)
            //    {
            //        allBridgesSum += c;
            //    }

            //    allBridgesSum /= allBridges.Count;

            //    for (int i = 0; i < allBridges.Count; i++)
            //    {
            //        bridgesError += Math.Pow(allBridgesSum - allBridges[i], 2);

            //        if (byMols)
            //    }

            //    bridgesError = Math.Sqrt(bridgesError / (allBridges.Count - 1));

            //    if (obtainedData.Count < 4)
            //    {
            //        obtainedData.Add(new double[] { 0.0, 0, 0.0, 0.0, 0.0, 0.0, 0.0 });
            //    }

            //    obtainedData[3][4] = (int)allBridgesSum;
            //    obtainedData[3][5] = (int)bridgesError;
            //}

            e.Result = new object[] { obtainedData, index };
        }
        private void bgWorkerAggNumber_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }
        private void bgWorkerAggNumber_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }
        #endregion

        #region Bond length
        private void bgWorkerBondLength_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;

            var format = (int)args[0];
            var bondsPath = (string)args[1];
            var filenames = (string[])args[2];
            var index = (int)args[3];

            double[] centerPoint = new double[3];

            var initBonds = FileWorker.LoadBonds(bondsPath, format);

            var obtainedData = new List<double[]>();

            int barStep = (int)(filenames.Length / 100.0);
            if (barStep == 0)
            {
                barStep++;
            }

            for (int k = 0; k < filenames.Length; k++)
            {
                try
                {
                    if (!bgWorkerBondLength.CancellationPending)
                    {
                        var file = new List<double[]>();
                        double[] sizes = new double[3];

                        readTableFile(format, filenames[k], out file, out sizes);

                        if (k == 0)
                        {
                            centerPoint = MolData.GetCenterPoint(sizes, file);
                        }

                        foreach (var c in initBonds)
                        {
                            var partOne = file[c[0] - 1];
                            var partTwo = file[c[1] - 1];
                            var bondPair = new double[] { Math.Min(partOne[3], partTwo[3]), Math.Max(partOne[3], partTwo[3]) };
                            var bondType = FileWorker.GetBondType(bondPair);

                            if (partOne[3] != 1.08)
                            {
                                var dist = Math.Round(Methods.GetDistance(partOne[0], partOne[1], partOne[2], partTwo[0], partTwo[1], partTwo[2]), 2);

                                if (dist > 2.5)
                                {
                                    do
                                    {
                                        MolData.ShiftAllDouble(3, sizes, new double[] { centerPoint[0] - partOne[0], centerPoint[1] - partOne[1], centerPoint[2] - partOne[2] },
                                                               centerPoint, file);
                                        partOne = file[c[0] - 1];
                                        partTwo = file[c[1] - 1];
                                        dist = Math.Round(Methods.GetDistance(partOne[0], partOne[1], partOne[2], partTwo[0], partTwo[1], partTwo[2]), 2);
                                    } while (dist > 2.5);
                                }
                                {
                                    if (obtainedData.Count == 0)
                                    {
                                        obtainedData.Add(new double[] { bondType, dist, 1, 0.0 });
                                    }
                                    else
                                    {
                                        int matchInd = obtainedData.FindIndex(x => x[0] == bondType && x[1] == dist);

                                        if (matchInd != -1)
                                        {
                                            obtainedData[matchInd][2]++;
                                        }
                                        else
                                        {
                                            obtainedData.Add(new double[] { bondType, dist, 1, 0.0 });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка в файле №" + filenames[k] + ". Причина ошибки:\n" + ex.ToString(),
                                    "Ошибка!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    e.Result = null;
                }

                if ((k + 1) % barStep == 0)
                {
                    ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)(k + 1)) / ((double)filenames.Length)));
                }
            }

            // Order the number of aggregates by ascend
            obtainedData = obtainedData.OrderBy(x => x[1]).OrderBy(x => x[0]).ToList();

            foreach (var c in FileWorker.BondTypes.Keys)
            {
                var group = obtainedData.Where(x => x[0] == c).ToList();

                if (group.Count > 0)
                {
                    var sum = 0.0;
                    foreach (var b in group)
                    {
                        sum += b[2];
                    }

                    foreach (var b in group)
                    {
                        b[3] = b[2] / sum;
                    }
                }
            }

            e.Result = new object[] { obtainedData, index };
        }
        private void bgWorkerBondLength_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void bgWorkerBondLength_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }
        #endregion

        #region Catalysis rate
        private void bgWorkerCatalRate_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;

            var format = (int)args[0];
            var filenames = (string[])args[1];
            var timestep = (int)args[2];
            var initSubstr = (int)args[3];
            var prdType = (double)args[4];
            var index = (int)args[5];

            var obtainedData = new List<double[]>();

            int barStep = (int)(filenames.Length / 100.0);
            if (barStep == 0)
            {
                barStep++;
            }

            for (int k = 0; k < filenames.Length; k++)
            {
                try
                {
                    if (!bgWorkerAggNumber.CancellationPending)
                    {
                        var file = new List<double[]>();
                        double[] sizes = new double[3];

                        readTableFile(format, filenames[k], out file, out sizes);

                        var prdCount = file.Where(x => x[3] == prdType).ToList().Count;

                        var frac = 0.0;

                        if (initSubstr > 0)
                        {
                            frac = Math.Round((double)prdCount / initSubstr, 3);
                        }

                        obtainedData.Add(new double[] { k * timestep, prdCount, frac });
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка в файле №" + filenames[k] + ". Причина ошибки:\n" + ex.ToString(),
                                    "Ошибка!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    e.Result = null;
                }

                if ((k + 1) % barStep == 0)
                {
                    ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)(k + 1)) / ((double)filenames.Length)));
                }
            }

            e.Result = new object[] { obtainedData, index };
        }

        private void bgWorkerCatalRate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void bgWorkerCatalRate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }
        #endregion

        #region LARINA
        private void bgWorkerLarina_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;

            var format = (int)args[0];
            var filenames = (string[])args[1];
            var withAutoCenter = (bool)args[2];
            var withZCenter = (bool)args[3];
            var beadType = (double)args[4];
            var index = (int)args[5];

            var obtainedData = new List<double[]>();

            double[] sizes = new double[3];
            double[] centerPoint = new double[3];

            for (int k = 0, len = filenames.Length; k < len; k++)
            {
                try
                {
                    if (!bgWorkerLarina.CancellationPending)
                    {
                        var file = new List<double[]>();

                        readTableFile(format, filenames[k], out file, out sizes);

                        if (k == 0)
                        {
                            centerPoint = MolData.GetCenterPoint(sizes, file);
                        }

                        if (withAutoCenter)
                        {
                            doAutoCenter(withZCenter, 4, sizes, centerPoint, file);
                        }

                        var backbone = file.Where(x => x[3] == beadType || x[3] == 1.04).ToList();

                        var ends = file.Where(x => x[3] == 1.04).OrderBy(x => x[4]).ToList();

                        var endToEnd = 0.0;

                        if (ends.Count == 2)
                        {
                            endToEnd = Methods.GetDistance(ends[0][0], ends[0][1], ends[0][2],
                                                                 ends[1][0], ends[1][1], ends[1][2]);
                        }

                        var sum = 0.0;

                        var pairs = new List<double[]>();
                        var dists = new List<double[]>();


                        for (int j = 0; j < backbone.Count - 1; j++)
                        {
                            dists.Clear();

                            for (int p = 0; p < backbone.Count -1; p++)
                            {
                                if (p != j)
                                    dists.Add(new double[] { Methods.GetDistance(backbone[j][0], backbone[j][1], backbone[j][2],
                                                          backbone[p][0], backbone[p][1], backbone[p][2]), j, p });
                            }
                            dists = dists.OrderBy(x => x[0]).ToList();

                            if (pairs.Count ==0)
                            {
                                pairs.Add(dists[0]);
                            }
                            else
                            {
                                bool added = false;
                                foreach (var c in pairs)
                                {
                                    if (c[0].Equals(dists[0][0]) && c[2].Equals(dists[0][1]))
                                    {
                                        pairs.Add(dists[1]);
                                        added = true;
                                        break;
                                    }
                                }
                                if (added)
                                {
                                    continue;
                                }
                                else
                                {
                                    pairs.Add(dists[0]);
                                }
                            
                            }

                                //sum += StructFormer.GetDistance(backbone[j][0], backbone[j][1], backbone[j][2],
                                //                                backbone[j+1][0], backbone[j+1][1], backbone[j+1][2]);
                            }

                        foreach (var c in pairs)
                        {
                            sum += c[0];
                        }

                        double[] row = new double[5];

                        row[0] = Math.Round(endToEnd,3);
                        row[1] = Math.Round(Math.Pow(endToEnd, 2), 3);
                        row[2] = Math.Round(sum, 3);

                        obtainedData.Add(row);

                        int barStep = (int)(filenames.Length / 100.0);
                        if (barStep == 0)
                        {
                            barStep++;
                        }

                        if (k % barStep == 0)
                        {
                            ((BackgroundWorker)sender).ReportProgress((int)(100.0 * ((double)k) / ((double)filenames.Length)));
                        }
                    }
                    else
                    {
                        e.Result = new object[] { };
                    }
                }
                catch(Exception ex)
            {
                MessageBox.Show("Ошибка в файле №" + filenames[k] + ". Причина ошибки:\n" + ex.ToString(),
                                "Ошибка!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                e.Result = null;
            }
}
            if (filenames.Length > 2)
            {
                // mean value and square deviations
                var endSum = 0.0;
                var endSqSum = 0.0;
                var cntrSum = 0.0;
                var endError = 0.0;
                var endSqError = 0.0;
                var cntrError = 0.0;

                for (int i = 0; i < obtainedData.Count; i++)
                {
                    endSum += obtainedData[i][0];
                    endSqSum += obtainedData[i][1];
                    cntrSum += obtainedData[i][2];
                }

                endSum /= obtainedData.Count;
                endSqSum /= obtainedData.Count;
                cntrSum /= obtainedData.Count;

                for (int i = 0; i < obtainedData.Count; i++)
                {
                    endError += Math.Pow(endSum - obtainedData[i][0], 2);
                    endSqError += Math.Pow(endSqSum - obtainedData[i][1], 2);
                    cntrError += Math.Pow(cntrSum - obtainedData[i][2], 2);
                }

                endError = Math.Sqrt(endError / (obtainedData.Count - 1));
                endSqError = Math.Sqrt(endSqError / (obtainedData.Count - 1));
                cntrError = Math.Sqrt(cntrError / (obtainedData.Count - 1));

                obtainedData[0][3] = Math.Round(endSum, 3);
                obtainedData[1][3] = Math.Round(endSqSum, 3);
                obtainedData[2][3] = Math.Round(cntrSum, 3);
                obtainedData[0][4] = Math.Round(endError, 3);
                obtainedData[1][4] = Math.Round(endSqError, 3);
                obtainedData[2][4] = Math.Round(cntrError, 3);
            }

            e.Result = new object[] { obtainedData, index };
        }

        private void bgWorkerLarina_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void bgWorkerLarina_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted(sender, e);
        }
        #endregion

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e != null)
            {
                var args = (object[])e.Result;
                var index = (int)args[1];

                var obtainedData = (List<double[]>)args[0];

                    foreach (var c in obtainedData)
                    {
                        if (index == 0)
                    {
                        dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                      c[2].ToString(), c[3].ToString(), 
                                                      c[4].ToString(), c[5].ToString(),
                                                      c[6].ToString(), c[7].ToString());
                    }

                        else if (index == 3)
                        {
                            dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                     c[2].ToString(), c[3].ToString(),
                                                     c[4].ToString(), c[5].ToString(),
                                                     c[6].ToString(), c[7].ToString(),
                                                     c[8].ToString());
                        }
                        else if (index == 5)
                        {
                            string blocksA = "";
                            string blocksB = "";

                            if (c.Length > 8)
                            {
                                var segments = (c.Length - 8) / 2;

                                for (int i = 0; i < segments; i++)
                                {

                                    if (i > 0)
                                    {
                                        blocksA += "-";
                                        blocksB += "-";
                                    }
                                    blocksA += c[8 + i].ToString();
                                    blocksB += c[8 + i + segments].ToString();
                                }

                                dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                          c[2].ToString(), c[3].ToString(),
                                                          c[4].ToString(), c[5].ToString(),
                                                          c[6].ToString(), c[7].ToString(),
                                                          blocksA, blocksB);
                            }
                            else
                            {
                                dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                       c[2].ToString(), c[3].ToString(),
                                                       c[4].ToString(), c[5].ToString(),
                                                       c[6].ToString(), c[7].ToString() /*c[8].ToString()*/);
                            }
                        }
                        else if (index == 6)
                        {
                            if (chbHasBonds.Checked)
                            {
                                string blocksA = "";
                                string blocksB = "";

                                var segments = (c.Length - 7) / 2;

                                for (int i = 0; i < segments; i++)
                                {

                                    if (i > 0)
                                    {
                                        blocksA += "-";
                                        blocksB += "-";
                                    }
                                    blocksA += c[7 + i].ToString();
                                    blocksB += c[7 + i + segments].ToString();
                                }


                                dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                           0, c[2].ToString(), 0,
                                                          c[3].ToString(), 0,
                                                          c[4].ToString(), c[5].ToString(),
                                                          c[6].ToString(), blocksA, blocksB);

                            }
                            else
                            {
                                dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                     c[c.Length - 3].ToString(),
                                                     c[2].ToString(), c[c.Length - 2].ToString(),
                                                     c[3].ToString(), c[c.Length - 1].ToString(),
                                                     c[4].ToString(), c[5].ToString(),
                                                     c[6].ToString(), c[7].ToString());
                            }
                        }
                        else if (index == 7)
                    {
                            string LineAx = "";

                        for (int i = 0; i < c.Length; i++)
                        {
                            if (i > 0)
                            {
                                LineAx += "-";
                            }
                            LineAx += c[i].ToString();
                        }

                        dgvDataFromFolder.Rows.Add(LineAx);
                        }
                        else if (index == 8 || index == 11)
                    {
                        dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                   c[2].ToString(),
                                                   c[3].ToString(), c[4].ToString(),
                                                   c[5].ToString());
                    }
                        else if (index == 10)
                        {
                          var isDyn = (bool)args[9];
                        if (isDyn)
                        {
                            dgvDataFromFolder.Rows.Add("", "", c[0].ToString(), c[1].ToString(), c[2].ToString());
                        }
                        else
                        {
                            dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString());
                        }
                        }
                        else if (index == 12)
                        {
                            dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                       c[2].ToString(), c[3].ToString());
                        }
                        else if (index == 13)
                        {
                            dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                       c[2].ToString());
                        }                      
                        else
                        {
                            dgvDataFromFolder.Rows.Add(c[0].ToString(), c[1].ToString(),
                                                       c[2].ToString(), c[3].ToString(),
                                                       c[4].ToString());
                        }
                    }

                if (index == 10)
                {
                    var isDyn = (bool)args[9];

                    if (!isDyn)
                    {
                        dgvDataFromFolder.Rows[0].Cells[2].Value = args[2];
                        dgvDataFromFolder.Rows[1].Cells[2].Value = args[3];
                        dgvDataFromFolder.Rows[0].Cells[3].Value = args[4];
                        dgvDataFromFolder.Rows[1].Cells[3].Value = args[5];
                        dgvDataFromFolder.Rows[0].Cells[4].Value = args[6];
                        dgvDataFromFolder.Rows[1].Cells[4].Value = args[7];
                        dgvDataFromFolder.Rows[0].Cells[5].Value = (string)args[8];
                    }
                }
            }
            
            pBar.Value = 0;
            pBar.Visible = false;
            btnCancel.Visible = false;

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


        #endregion

        #region FileRead 

        private void readTableFile(int format, string fileName, out List<double[]> file, out double[] sizes)
        {
            sizes = new double[3];
            if (format >= 1)
            {
                if (format == 1)
                {
                    file = FileWorker.LoadMol2Lines(fileName, new List<int[]>());
                }
                else
                {
                    file = FileWorker.LoadXyzrLines(fileName);
                }

                sizes[0] = Math.Abs(file[file.Count - 1][0] - file[file.Count - 8][0]);
                sizes[1] = Math.Abs(file[file.Count - 1][1] - file[file.Count - 8][1]);
                sizes[2] = Math.Abs(file[file.Count - 1][2]);

                // remove box coords
                file.RemoveRange(file.Count - 9, 8);
            }   
            else
            {
                int snapnum = 0;
                file = FileWorker.LoadLammpstrjLines(fileName, out snapnum, out sizes);
            }
        }

        #endregion

        #region Text read/check

        public static double replaceValue(string str)
        {
            string symbol = "";
            double retValue = 0;
            symbol = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

            if (symbol == ".")
            {
                str = str.Replace(",", ".");
            }
            else
            {
                str = str.Replace(".", ",");
            }

            if (str == "")
            {
                throw new ApplicationException("Имеются незаполненные поля! Убедитесь,что заданы все параметры расчета!");
            }

            retValue = Convert.ToDouble(str);
            return retValue;
        }
        private void TextBox_TextChangedInt(object sender, EventArgs e)
        {
            if (isValidInt(((TextBox)sender).Text))
                ep.SetError(((TextBox)sender), "");
            else
                ep.SetError(((TextBox)sender), "Неправильно введено число!");
        }
        private void TextBox_TextChangedFloat(object sender, EventArgs e)
        {

        }

        private bool isValidFloat(string s)
        {
            string symbol = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

            if (symbol == ".")
            {
                s = s.Replace(",", ".");
            }
            else
            {
                s = s.Replace(".", ",");
            }

            double f;
            return double.TryParse(s, out f);
        }

        private bool isValidInt(string s)
        {
            int f;
            return int.TryParse(s, out f);
        }


        #endregion

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            var typForm = new TypesForm();
            typForm.Show();
        }

        private void AnalysisControl_Load(object sender, EventArgs e)
        {

        }

      
    }
}
