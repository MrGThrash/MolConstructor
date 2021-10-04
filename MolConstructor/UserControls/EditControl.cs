using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MolConstructor
{
    public partial class EditControl : UserControl
    {
        // Tab1 - General edit
        private List<double[]> InputCut = new List<double[]>();
        private List<int[]> InitCutBonds = new List<int[]>();
        private List<int[]> InitCutAngles = new List<int[]>();
        private List<MolData> EditedComposition = new List<MolData>();
        private double[] CenterPoint_Edit = new double[3];

        // Tab 2 - recolor
        private List<double[]> InputColorData = new List<double[]>();
        private List<int[]> InputColorBonds = new List<int[]>();
        private List<int[]> InputColorAngles = new List<int[]>();
        private List<MolData> RecoloredComposition = new List<MolData>();

        public EditControl()
        {
            InitializeComponent();
        }

        #region Page 1 - General edit
        private void btnChooseInitFile_Click(object sender, EventArgs e)
        {
            InputCut = LoadFile_Edit(InputCut, true, tbInitPath_Page1, tbX_Page1, tbY_Page1, tbZ_Page1, btnChooseInitBonds, InitCutBonds, InitCutAngles);
            CenterPoint_Edit = MolData.GetCenterPoint(new double[] {replaceValue(tbX_Page1.Text),
                                                                    replaceValue(tbY_Page1.Text),
                                                                    replaceValue(tbZ_Page1.Text)}, InputCut);
            if (InputCut.Count == 0)
            {
                return;
            }
            else
            {
                label10.Visible = (InitCutBonds.Count == 0);
                tbBondsPath_Page1.Visible = (InitCutBonds.Count == 0);
                btnChooseInitAngles_Page1.Visible = (InitCutAngles.Count == 0);
                label106.Visible = (InitCutAngles.Count == 0);
                tbAnglesPath_Page1.Visible = (InitCutAngles.Count == 0);
                btnClearEdit.Visible = true;
            }

            gbShift.Enabled = true;
        }

        private void btnChooseInitBonds_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "DAT-файлы (*.dat)|*.dat";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            tbBondsPath_Page1.Text = openFileDialog.FileName;
            InitCutBonds = FileWorker.LoadBonds(tbBondsPath_Page1.Text, 0);
        }

        private void btnChooseInitAngles_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "DAT-файлы (*.dat)|*.dat";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            tbAnglesPath_Page1.Text = openFileDialog.FileName;
            InitCutAngles = FileWorker.LoadAngles(File.ReadAllLines(tbAnglesPath_Page1.Text));
        }

        private void chbCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (!chbCenter.Checked)
                return;
            double[] centeredStrct = Methods.CenterStructure(CenterPoint_Edit, InputCut);
            tbShiftX_Page1.Text = centeredStrct[0].ToString();
            tbShiftY_Page1.Text = centeredStrct[1].ToString();
            tbShiftZ_Page1.Text = centeredStrct[2].ToString();
        }

        private void chbHasWalls_Page1_CheckedChanged(object sender, EventArgs e)
        {
            cmbWallsType_Page1.Enabled = chbHasWalls_Page1.Checked;
        }

        private void btnCutSlice_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCutAxis.SelectedIndex == -1)
                {
                    MessageBox.Show(null, "Вы не выбрали плоскость среза!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (tbCutCoord.Text == "")
                {
                    MessageBox.Show(null, "Вы не выбрали координату среза!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    double coord = replaceValue(tbCutCoord.Text);
                    if (cmbCutAxis.SelectedIndex == 0)
                    {
                        double sizeZ =replaceValue(tbZ_Page1.Text);
                        if (Math.Abs(coord) > sizeZ / 2.0)
                        {
                            MessageBox.Show(null, "Координата среза больше размеров ящика!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    if (cmbCutAxis.SelectedIndex == 1)
                    {
                        double sizeY =replaceValue(tbY_Page1.Text);
                        if (Math.Abs(coord) > sizeY / 2.0)
                        {
                            MessageBox.Show(null, "Координата среза больше размеров ящика!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    if (cmbCutAxis.SelectedIndex == 2)
                    {
                        double sizeX = replaceValue(tbX_Page1.Text);
                        if (Math.Abs(coord) > sizeX / 2.0)
                        {
                            MessageBox.Show(null, "Координата среза больше размеров ящика!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    double xSize= replaceValue(tbX_Page1.Text);
                    double ySize = replaceValue(tbY_Page1.Text);
                    double zSize = replaceValue(tbZ_Page1.Text);
                    double density = replaceValue(tbDensity_Page1.Text);
                    if (chbBySphere.Checked)
                    {
                        coord = replaceValue(tbSphereRad.Text);
                    }

                    EditedComposition = StructFormer.MakeSlice(chbCutOnlyPolymer.Checked, chbBySphere.Checked, density, 
                                                               xSize, ySize, zSize, cmbCutAxis.SelectedIndex, coord, InputCut);
                    if (btnSaveStruct_Page1.Enabled)
                    {
                        return;
                    }

                    btnSaveStruct_Page1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }

        private void btnShiftStructure_Click(object sender, EventArgs e)
        {
            EditedComposition = MolData.ShiftAll((chbHasWalls_Page1.Checked ? 1 : 0) != 0, cmbWallsType_Page1.SelectedIndex,
                                                  (chbShiftOnlyPolymer.Checked ? 1 : 0) != 0, replaceValue(tbDensity_Page1.Text),
                                                  new double[] {replaceValue(tbX_Page1.Text),
                                                                replaceValue(tbY_Page1.Text),
                                                                replaceValue(tbZ_Page1.Text)},
                                                  new double[3] {replaceValue(tbShiftX_Page1.Text),
                                                                 replaceValue(tbShiftY_Page1.Text),
                                                                 replaceValue(tbShiftZ_Page1.Text)},
                                                  CenterPoint_Edit, InputCut);



            //for (int i = 0; i < EditedComposition.Count; i++)
            //{
            //    if (i >= 1700)
            //    {
            //        if (EditedComposition[i].AtomType == 1.00)
            //        {
            //            EditedComposition[i].AtomType = 1.01;
            //        }
            //    }
            //}

                // For Robin
                //    {
                //        double xSize = replaceValue(tbX_Page1.Text);
                //        double ySize = replaceValue(tbY_Page1.Text);
                //        double zSize = replaceValue(tbZ_Page1.Text);

                //        int maxNum = (int)xSize * (int)ySize * (int)zSize * 3;

                //        int watercount = (maxNum-EditedComposition.Count)/ 2;
                //        int molInd = EditedComposition.Max(x => x.MolIndex);

                //        Random rnd = new Random();
                //        Random rndDec = new Random();
                //        int counter = 0;
                //        do
                //        {

                //            double xCoord = (double)rnd.Next(0, (int)xSize - 1) + rndDec.NextDouble();
                //            double yCoord = (double)rnd.Next(0, (int)ySize - 1) + rndDec.NextDouble();
                //            double zCoord = (double)rnd.Next(0, 75) + rndDec.NextDouble();

                //                molInd++;
                //                counter++;
                //                EditedComposition.Add(new MolData(1.03, EditedComposition.Count + 1, molInd, xCoord, yCoord, zCoord));

                //        }
                //        while (counter < watercount);


                //    counter = 0;
                //    do
                //    {
                //            double xCoord = (double)rnd.Next(1, (int)xSize - 1) + rndDec.NextDouble();
                //            double yCoord = (double)rnd.Next(1, (int)ySize - 1) + rndDec.NextDouble();
                //            double zCoord = (double)rnd.Next(105, 179) + rndDec.NextDouble();


                //            if (true)
                //        {
                //            molInd++;
                //            counter++;
                //            EditedComposition.Add(new MolData(1.02, EditedComposition.Count + 1, molInd, xCoord , yCoord , zCoord ));
                //        }

                //    }
                //    while (counter < watercount);
                //}

                //Gavrilov-Rudyak
                // 1,4- regular monomers, 3 - initiator, the rest are the crosslinkers
                //foreach (var c in EditedComposition)
                //{
                //    if (c.AtomType == 1.02)
                //    {
                //        c.AtomType = 1.00;
                //    }
                //    if (c.AtomType == 1.03)
                //    {
                //        c.AtomType = 1.00;
                //    }
                //    if (c.AtomType == 1.04)
                //    {
                //        c.AtomType = 1.01;
                //    }
                //    if (c.AtomType == 1.06)
                //    {
                //        c.AtomType = 1.01;
                //    }
                //}

                if (chbHighlightCrossLinks.Checked)
            {
                var type = FileWorker.AtomTypes[Convert.ToInt32(tbHighlightType.Text)];

                foreach (var c in EditedComposition)
                {
                    var beadBonds = new List<int>();
                    foreach (var p in InitCutBonds)
                    {
                        if (p[0] == c.Index)
                        {
                            beadBonds.Add(p[1]);
                        }
                        if (p[1] == c.Index)
                        {
                            beadBonds.Add(p[0]);
                        }
                    }

                    c.Bonds = beadBonds;
                }

                foreach (var c in EditedComposition)
                {
                    //if (c.Bonds.Count == 2 && c.Index <= 833)
                    //{
                    //c.AtomType = 1.00;
                    //}

                    if (c.Bonds.Count > 2)
                    {
                        c.AtomType = type;
                    }
                }

                //Random rand = new Random();

                //int frac = (int)(EditedComposition.Count * 0.2);

                //int counter = 0;

                //do
                //{
                //    int ind = rand.Next(0, EditedComposition.Count-1);

                //    if (EditedComposition[ind].AtomType == 1.00 && EditedComposition[ind].Bonds.Count == 2)
                //    {    
                //        var indTwo = EditedComposition[ind].Bonds[0] - 1;
                //        var indThree = EditedComposition[ind].Bonds[1] - 1;
                //        if (EditedComposition[indTwo].AtomType == 1.00)
                //        {
                //            if (EditedComposition[indTwo].Bonds.Count == 2)
                //            {
                //               if (EditedComposition[EditedComposition[indTwo].Bonds[0]].AtomType == 1.01)
                //                {
                //                    EditedComposition[indTwo].AtomType = 1.01;
                //                }
                //               else
                //                {
                //                    EditedComposition[indThree].AtomType = 1.01;
                //                }         
                //            }
                //            else
                //            {
                //                continue;
                //            }
                //        }
                //        else
                //        {
                //            if (EditedComposition[indThree].AtomType == 1.00)
                //            {
                //                EditedComposition[indThree].AtomType = 1.01;
                //            }
                //            else
                //            {
                //                continue;
                //            }
                //        }

                //        EditedComposition[ind].AtomType = 1.01;

                //        counter += 2;
                //    }
                //} while (counter < frac);


            }
            if (chbCreateAngles.Checked)
            {
                var type = FileWorker.AtomTypes[Convert.ToInt32(tbAngleType.Text)];
                InitCutAngles = MolData.CreateAngles(type, EditedComposition, InitCutBonds);
            }

            if (chbMur.Checked)
            {
                foreach (MolData molData in EditedComposition)
                {
                    if (molData.AtomType.Equals(1.03) && molData.ZCoord > 40.0)
                        molData.AtomType = 1.02;
                    if (molData.AtomType.Equals(1.02) && molData.ZCoord <= 40.0)
                        molData.AtomType = 1.03;
                }
            }
            if (!btnSaveStruct_Page1.Enabled)
            {
                btnSaveStruct_Page1.Enabled = true;
            }
            if (!chbShrink.Checked)
            {
                return;
            }
            double num =replaceValue(tbShrinkRate.Text);
            foreach (MolData molData in EditedComposition)
            {
                molData.XCoord /= num;
                molData.YCoord /= num;
                molData.ZCoord /= num;
            }
        }

        private void btnSaveStruct_Page1_Click(object sender, EventArgs e)
        {
            SaveStruct(EditedComposition, InitCutBonds, InitCutAngles, tbX_Page1, tbY_Page1, tbZ_Page1,
                       tbDensity_Page1, 1, chbHasWalls_Page1.Checked, false);
        }

        private void chbBySphere_CheckedChanged(object sender, EventArgs e)
        {
            lbl112.Enabled = chbBySphere.Checked;
            tbSphereRad.Enabled = chbBySphere.Checked;
        }

        private void btnClearEdit_Click(object sender, EventArgs e)
        {
            InputCut.Clear();
            InitCutBonds.Clear();
            InitCutAngles.Clear();
            tbInitPath_Page1.Text = "";
            tbBondsPath_Page1.Text = "";
            tbAnglesPath_Page1.Text = "";
            label10.Visible = (InitCutBonds.Count == 0);
            tbBondsPath_Page1.Visible = (InitCutBonds.Count == 0);
            btnChooseInitAngles_Page1.Visible = (InitCutAngles.Count == 0);
            label106.Visible = (InitCutAngles.Count == 0);
            tbAnglesPath_Page1.Visible = (InitCutAngles.Count == 0);
            btnClearEdit.Visible = false;
        }

        private void chbShrink_CheckedChanged(object sender, EventArgs e)
        {
            tbShrinkRate.Visible = chbShrink.Checked;
            label105.Visible = chbShrink.Checked;
        }

        private void chbShiftOnlyPolymer_CheckedChanged(object sender, EventArgs e)
        {
            chbShrink.Enabled = chbShiftOnlyPolymer.Checked;
            chbHighlightCrossLinks.Enabled = chbShiftOnlyPolymer.Checked;
            chbCreateAngles.Enabled = chbShiftOnlyPolymer.Checked;
        }

        private void chbHighlightCrossLinks_CheckedChanged(object sender, EventArgs e)
        {
            tbHighlightType.Visible = chbHighlightCrossLinks.Checked;
            label1.Visible = chbHighlightCrossLinks.Checked;
        }

        private void chbCreateAngles_CheckedChanged(object sender, EventArgs e)
        {
            tbAngleType.Visible = chbCreateAngles.Checked;
            label13.Visible = chbCreateAngles.Checked;

        }

        #endregion

        #region Page 2 - Recolor
        private void btnChoosePath_Page2_Click(object sender, EventArgs e)
        {
            InputColorData = LoadFile_Edit(InputColorData, false, tbInitPath_Page2, tbX_Page2, tbY_Page2, tbZ_Page2, btnChooseBonds_Page2, InputColorBonds, InputColorAngles);
                if (InputColorData.Count == 0)
                {
                    return;
                }
                else
                {
                    label91.Visible = (InputColorBonds.Count == 0);
                    tbBondsPath_Page2.Visible = (InputColorBonds.Count == 0);
                    btnChooseInitAngles_Page2.Visible = (InputColorAngles.Count == 0);
                    label8.Visible = (InputColorAngles.Count == 0);
                    tbAnglesPath_Page2.Visible = (InputColorAngles.Count == 0);
                }
            btnRecolor.Enabled = true;
        }

        private void btnChooseBonds_2_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "DAT-файлы (*.dat)|*.dat";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            tbBondsPath_Page2.Text = openFileDialog.FileName;
            InputColorBonds = FileWorker.LoadBonds(tbBondsPath_Page2.Text, 0);
        }

        private void btnChooseInitAngles_2_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "DAT-файлы (*.dat)|*.dat";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            tbAnglesPath_Page2.Text = openFileDialog.FileName;
            InputColorAngles = FileWorker.LoadAngles(File.ReadAllLines(tbAnglesPath_Page2.Text));
        }

        private void btnClearRecolor_Click(object sender, EventArgs e)
        {
            InputColorData.Clear();
            InputColorBonds.Clear();
            InputColorAngles.Clear();
            tbInitPath_Page2.Text = "";
            tbBondsPath_Page2.Text = "";
            tbAnglesPath_Page2.Text = "";
            label91.Visible = (InputColorBonds.Count == 0);
            tbBondsPath_Page2.Visible = (InputColorBonds.Count == 0);
            btnChooseInitAngles_Page2.Visible = (InputColorAngles.Count == 0);
            label8.Visible = (InputColorAngles.Count == 0);
            tbAnglesPath_Page2.Visible = (InputColorAngles.Count == 0);
        }

        private void btnRecolor_Click(object sender, EventArgs e)
        {
            int molOneAmount = Convert.ToInt32(tbNonLinMolAmount.Text);
            int nonLinLength = Convert.ToInt32(tbNonLinLength.Text);
            int linLength = Convert.ToInt32(tbLinMolLenght_Page8_2.Text);
            int molTwoCount = 0;
            if (linLength > 0)
            {
                molTwoCount = (InputColorData.Where<double[]>((x => x[3] <= 1.01)).Count() - molOneAmount * nonLinLength) / linLength;
            }

            RecoloredComposition = StructFormer.Recolor(chbLeftOnlyPolymer_2.Checked, true, chbAllDiff.Checked, chbIsDb.Checked, 
                                                        cmbNonLinPlace.SelectedIndex, nonLinLength, molOneAmount, linLength, molTwoCount, InputColorData);
            btnSaveStruct_Page2.Enabled = true;
        }

        private void btnSaveStruct_Page2_Click(object sender, EventArgs e)
        {
            SaveStruct(RecoloredComposition, InputColorBonds, InputColorAngles, tbX_Page2, tbY_Page2, tbZ_Page2,
                          tbDensity_Page2, 1, false, false);
        }
        #endregion

        #region Infrastructure
        private List<double[]> LoadFile_Edit(List<double[]> data, bool forEdit, TextBox tbPath,
                              TextBox tbX, TextBox tbY, TextBox tbZ, Button btn,
                              List<int[]> bonds, List<int[]> angles)
        {
            openFileDialog.Filter = "Lammps-файлы траекторные (*.lammpstrj)|*.lammpstrj|Конфиг. файлы Lammps (*.*)|*.*|Файлы Mat.Studio (*.mol)|*.mol|Текстовые файлы XYZR (*.xyzr)|*.xyzr";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openFileDialog.InitialDirectory = openFileDialog.FileName;
                    tbPath.Text = openFileDialog.FileName;
                    if (openFileDialog.FilterIndex == 1)
                    {
                        int snapnum = 0;
                        double[] sizes = new double[3];
                        
                        var lmpsdata = FileWorker.LoadLammpstrjLines(openFileDialog.FileName, out snapnum, out sizes);
                        
                        // Assign only coordinats is the system is edited
                        if(data.Count != lmpsdata.Count)
                        {
                            data = lmpsdata;
                        }
                        else
                        {
                            for (int i = 0; i< data.Count; i++)
                            {
                                data[i][0] = lmpsdata[i][0];
                                data[i][1] = lmpsdata[i][1];
                                data[i][2] = lmpsdata[i][2];
                                data[i][3] = lmpsdata[i][3];
                            }
                        }
                        if (tbX != null && tbY != null && tbZ != null)
                        {
                            tbX.Text = sizes[0].ToString();
                            tbY.Text = sizes[1].ToString();
                            tbZ.Text = sizes[2].ToString();
                        }
                        btn.Enabled = true;
                        return data;
                    }
                    else if (openFileDialog.FilterIndex == 2)
                    {
                        data = new List<double[]>();
                        double xSize;
                        double ySize;
                        double zSize;
                        FileWorker.LoadConfLines(out xSize, out ySize, out zSize, openFileDialog.FileName, data, bonds, angles);

                        //if (!forEdit && (xSize > 250 || ySize > 250 || zSize > 250))
                        //{
                        //    double multiplier = Math.Max((double)xSize / 100.0, Math.Max((double)ySize / 100.0, (double)zSize / 100.0));

                        //    var diam = StructFormer.GetDiameter(data)[0];

                        //    for (int i = 0; i < data.Count; i++)
                        //    {
                        //        data[i][0] /= 3.0;
                        //        data[i][1] /= 3.0;
                        //        data[i][2] /= 3.0;
                        //    }


                        //    xSize = xSize / (int)multiplier;
                        //    ySize = ySize / (int)multiplier;
                        //    zSize = zSize / (int)multiplier;
                        //}

                        if (tbX != null && tbY != null && tbZ != null)
                        {
                            tbX.Text = xSize.ToString();
                            tbY.Text = ySize.ToString();
                            tbZ.Text = zSize.ToString();
                        }
                        btn.Visible = false;
                        return data;
                    }
                    else if (openFileDialog.FilterIndex == 3)
                    {
                        data = FileWorker.LoadMolLines(openFileDialog.FileName, bonds);
                        btn.Visible = false;

                        tbX.Text = "100";
                        tbY.Text = "100";
                        tbZ.Text = "100";
                        return data;
                    }
                    else
                    {
                        data = FileWorker.LoadXyzrLines(openFileDialog.FileName);
                        if (tbX != null && tbY != null && tbZ != null)
                        {
                            tbX.Text = Math.Abs(data[data.Count - 1][0] - data[data.Count - 8][0]).ToString();
                            tbY.Text = Math.Abs(data[data.Count - 1][1] - data[data.Count - 8][1]).ToString();
                            tbZ.Text = Math.Abs(data[data.Count - 1][2]).ToString();
                        }
                        btn.Visible = true;
                        return data;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                   //MessageBox.Show("Произошла ошибка при чтении!\nУбедитесь, что выбранный файл имеет нужный формат!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return new List<double[]>();
                }
            }
            else
            {
                return new List<double[]>();
            }
        }
        private void LoadBonds(out List<int[]> bonds, TextBox tbPath)
        {
            openFileDialog.Filter = "Конфиг. файлы Lammps (*.txt)|*.txt|DAT-файлы (*.dat)|*.dat";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openFileDialog.InitialDirectory = openFileDialog.FileName;
                    tbPath.Text = openFileDialog.FileName;
                    if (openFileDialog.FilterIndex == 1)
                    {
                        bonds = FileWorker.LoadBonds(tbPath.Text, 0);
                    }
                    else
                    {
                        bonds = new List<int[]>();
                        double xSize;
                        double ySize;
                        double zSize;
                        FileWorker.LoadConfLines(out xSize, out ySize, out zSize, openFileDialog.FileName, null, bonds, null);
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при чтении!\nУбедитесь, что выбранный файл имеет нужный формат!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    bonds = new List<int[]>();
                }
            }
            else
                bonds = new List<int[]>();
        }
        private void SaveStruct(List<MolData> system, List<int[]> bonds, List<int[]> angles,
                                TextBox tbX, TextBox tbY, TextBox tbZ, TextBox tbDenstiy,
                                int molCount, bool hasWalls, bool addBonds)
        {
            saveFileDialog.Filter = "Файлы RasMol (*.ent)|*.ent|Lammps-файлы траекторные (*.lammpstrj)|*.lammpstrj|Конфиг. файлы Lammps (*.txt)|*.txt|Restart-файлы (*.dat)|*.dat|Текстовые файлы XYZR (*.xyzr)|*.xyzr";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            int boundaryCond = 3;
            if (hasWalls)
                boundaryCond = 6;
            double density = replaceValue(tbDenstiy.Text);
            double[] sizes = new double[] { replaceValue(tbX.Text),
                                            replaceValue(tbY.Text),
                                            replaceValue(tbZ.Text)};

            switch (saveFileDialog.FilterIndex)
            {
                case 1:
                    FileWorker.Save_MOL(saveFileDialog.FileName, bonds, system);
                    break;
                case 2:
                    FileWorker.SaveLammpstrj(false, saveFileDialog.FileName, 0, sizes, density, system);
                    break;
                case 3:
                    int atomTypes = MolData.CalcTypes(system);
                    int bondTypes = MolData.CalcBonds(bonds, system);
                    if (addBonds)
                    {
                        
                        bondTypes++;
                    }
                    int angleTypes = 0;
                    //if ((uint)angles.Count > 0U)
                    //{
                    //    var anglesTwo = MolData.CreateAngles(1.04, system, bonds);
                    //    foreach (var c in anglesTwo)
                    //    {
                    //        angles.Add(c);
                    //    }

                    //    angles = angles.Distinct().ToList();

                    //    angleTypes = MolData.CalcAngles(angles, system);
                    //}
                    //else
                    //{
                    //    angles = MolData.CreateAngles(1.04, system, bonds);
                    //    angleTypes = MolData.CalcAngles(angles, system);
                    //}
                    FileWorker.Save_Conf(saveFileDialog.FileName, sizes, density, atomTypes, bondTypes, angleTypes, bonds, angles, system);
                    break;
                case 4:
                    FileWorker.Save_DAT(saveFileDialog.FileName, boundaryCond, 0, 1, sizes, bonds, angles, system);
                    break;
                case 5:
                    FileWorker.Save_XYZ(saveFileDialog.FileName, false, sizes, system);
                    break;
            }
            StartProcces(saveFileDialog.FileName);
        }

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
