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
    public partial class ConstructControl : UserControl
    {
        // Tab1 - solution and interface
        private List<double[]> InputData = new List<double[]>();
        private List<int[]> InitBonds = new List<int[]>();
        private List<int[]> TotalBonds = new List<int[]>();
        private List<int[]> InitAngles = new List<int[]>();
        private List<int[]> TotalAngles = new List<int[]>();
        private List<MolData> MolComposition = new List<MolData>();

        // Tab2 - films
        private List<double[]> FilmMolOne = new List<double[]>();
        private List<double[]> FilmMolTwo = new List<double[]>();
        private List<int[]> InitFilmBondsOne = new List<int[]>();
        private List<int[]> InitFilmBondsTwo = new List<int[]>();
        private List<int[]> TotalFilmBonds = new List<int[]>();
        private List<int[]> InitFilmAnglesOne = new List<int[]>();
        private List<int[]> InitFilmAnglesTwo = new List<int[]>();
        private List<int[]> TotalFilmAngles = new List<int[]>();
        private List<MolData> FilmComposition = new List<MolData>();

        // Tab4 - dplanar brush
        private List<double[]> InputBrush = new List<double[]>();
        private List<double[]> InputMicrogel_Brush = new List<double[]>();
        private List<int[]> InitBrushBonds = new List<int[]>();
        private List<int[]> MicrogelBonds_Brush = new List<int[]>();
        private List<int[]> MicrogelAngles_Brush = new List<int[]>();
        private List<int[]> TotalBrushBonds = new List<int[]>();
        private List<int[]> InitBrushAngles = new List<int[]>();
        private List<int[]> TotalBrushAngles = new List<int[]>();
        private List<MolData> BrushComposition = new List<MolData>();

        // Tab4 - drop
        private List<double[]> InputDropData = new List<double[]>();
        private List<int[]> InitDropBonds = new List<int[]>();
        private List<int[]> TotalDropBonds = new List<int[]>();
        private List<int[]> InitDropAngles = new List<int[]>();
        private List<int[]> TotalDropAngles = new List<int[]>();
        private List<MolData> DropComposition = new List<MolData>();

        // Tab5 - lipids and microgels
        private List<double[]> InputLipid = new List<double[]>();
        private List<double[]> InputMicrogel_Lipid = new List<double[]>();
        private List<int[]> LipidBonds = new List<int[]>();
        private List<int[]> LipidAngles = new List<int[]>();
        private List<int[]> MicrogelBonds_Lipid = new List<int[]>();
        private List<int[]> MicrogelAngles_Lipid = new List<int[]>();
        private List<MolData> LipidComposition = new List<MolData>();
        private List<int[]> TotalLipidBonds = new List<int[]>();
        private List<int[]> TotalLipidAngles = new List<int[]>();

        // Tab 5 - synth
        private List<int[]> SynthBonds = new List<int[]>();
        private List<int[]> SynthAngles = new List<int[]>();
        private List<MolData> SynthMol = new List<MolData>();
        // Tab 5 - functionalization
        private List<double[]> InputFuncMol = new List<double[]>();
        private List<int[]> InitFuncBonds = new List<int[]>();
        private List<int[]> InitFuncAngles = new List<int[]>();
        private List<MolData> NewFuncMol = new List<MolData>();
        private List<int[]> TotalFuncBonds = new List<int[]>();

        public ConstructControl()
        {
            InitializeComponent();
            // Set initial params;
            cmbWallsType_PagePlanBrush.SelectedIndex = 0;
        }
        #region Page - Solution and Interface
        private void btnChooseMol_Page1_Click(object sender, EventArgs e)
        {
            LoadFile(out InputData, false, tbInitPath_Page1, null, null, null, btnChooseBonds_Page1, InitBonds, InitAngles);
            if (InputData.Count == 0)
            {
                return;
            }
            else
            {
                label7.Visible = (InitBonds.Count == 0);
                tbBondsPath_Page1.Visible = (InitBonds.Count == 0);
            }

            btnComposeStruct_Page1.Enabled = true;
        }

        private void btnChooseBonds_Page1_Click(object sender, EventArgs e)
        {
            LoadBonds(out InitBonds, tbBondsPath_Page1);
        }

        private void chbAutofill_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAutofill.Checked && InputData != null)
                tbMolCount.Text = Methods.GetFlatAmount(Convert.ToInt32(tbXsize_Page1.Text), Convert.ToInt32(tbYsize_Page1.Text), Methods.GetDiameter(InputData)).ToString();
            else
                tbMolCount.Text = "1";
        }

        //private void btnShowMiccelPicker_Click(object sender, EventArgs e)
        //{
        //    new MPForm().Show();
        //}

        private void chbOnlySolv_CheckedChanged(object sender, EventArgs e)
        {
            if (InputData.Count == 0)
            {
                btnComposeStruct_Page1.Enabled = chbOnlySolv.Checked;
            }
        }

        private void chbRandomDistribution_CheckedChanged(object sender, EventArgs e)
        {
            chbTwoOils.Enabled = chbRandomDistribution.Checked;
            if (!chbTwoOils.Enabled)
            {
                chbTwoOils.Checked = false;
            }
            if (!chbSeparatedSolvents.Enabled)
            {
                chbSeparatedSolvents.Enabled = true;
            }
            else
            {
                chbSeparatedSolvents.Checked = false;
                chbSeparatedSolvents.Enabled = false;
            }
        }

        private void chbHasMatrix_CheckedChanged(object sender, EventArgs e)
        {
            tbPolymerPerc.Enabled = chbHasMatrix.Checked;
            lblIsLazy.Enabled = chbHasMatrix.Checked;
            tbMatrixChainLength.Enabled = chbHasMatrix.Checked;
            lblMatrix.Enabled = chbHasMatrix.Checked;
            tbMatrixType.Enabled = chbHasMatrix.Checked;
            lblMatrixType.Enabled = chbHasMatrix.Checked;
            chbDiblockMatrix.Enabled = chbHasMatrix.Checked;
            if (!chbHasMatrix.Checked)
            {
                return;
            }
            chbSeparatedSolvents.Checked = true;
            chbHasCatalysis.Checked = false;
        }

        private void chbDiblockMatrix_CheckedChanged(object sender, EventArgs e)
        {
            if (chbHasMatrix.Checked)
            {
                label100.Enabled = chbDiblockMatrix.Checked;
                tbMatrixBlockA.Enabled = chbDiblockMatrix.Checked;
            }
            else
            {
                label100.Enabled = chbHasMatrix.Checked;
                tbMatrixBlockA.Enabled = chbHasMatrix.Checked;
            }
        }

        private void chbHasCatalysis_CheckedChanged(object sender, EventArgs e)
        {
            label108.Enabled = chbHasCatalysis.Checked;
            label109.Enabled = chbHasCatalysis.Checked;
            label14.Enabled = chbHasCatalysis.Checked;
            tbCatalPerc.Enabled = chbHasCatalysis.Checked;
            tbSubstrPerc.Enabled = chbHasCatalysis.Checked;
            tbCatRes.Enabled = chbHasCatalysis.Checked;
            tbSubstrRes.Enabled = chbHasCatalysis.Checked;
            if (!chbHasCatalysis.Checked)
            {
                return;
            }
            chbSeparatedSolvents.Checked = true;
            chbHasMatrix.Checked = false;
        }

        private void chbHasWalls_CheckedChanged(object sender, EventArgs e)
        {
            cmbWallsType_Page1.Enabled = chbHasWalls_Page1.Checked;
        }

        private void btnComposeStruct_Page1_Click(object sender, EventArgs e)
        {
            try
            {
                double xSize = replaceValue(tbXsize_Page1.Text);
                double ySize = replaceValue(tbYsize_Page1.Text);
                double zSize = replaceValue(tbZsize_Page1.Text);
                double density = replaceValue(tbDensity_Page1.Text);
                int molNumber = Convert.ToInt32(tbMolCount.Text);
                double borderShift = replaceValue(tbBorderShift.Text);
                double percentage = replaceValue(tbSolvPercent_Page1.Text) / 100.0;
                if (chbOnlySolv.Checked)
                {
                    molNumber = 0;
                }
                StructFormer structFormer = new StructFormer(chbIsHomoPol.Checked, chbHasWalls_Page1.Checked, chbAutofill.Checked, chbSeparatedSolvents.Checked,
                                                             chbCrossEnabled.Checked, cmbSolvLocation.SelectedIndex, density, cmbWallsType_Page1.SelectedIndex,
                                                             xSize, ySize, zSize, molNumber, percentage, borderShift, InputData);
                if (!chbHasMatrix.Checked)
                {
                    MolComposition = !chbRandomDistribution.Checked ? structFormer.ComposeStructure(chbDoubleSurf.Checked) : structFormer.ComposeRandomStructure(chbTwoOils.Checked, InitBonds);

                    if (chbHasCatalysis.Checked)
                    {
                        double substrPerc = replaceValue(tbSubstrPerc.Text) / 100.0;
                        double catalPerc = replaceValue(tbCatalPerc.Text) / 100.0;
                        structFormer.PrepareCatalysicSystem(substrPerc, catalPerc, MolComposition);
                        tbCatRes.Text = MolComposition.Where(x => x.AtomType.Equals(1.05)).ToList().Count.ToString();
                        tbSubstrRes.Text = MolComposition.Where(x => x.AtomType.Equals(1.06)).ToList().Count.ToString();
                    }
                }
                else
                {
                    int matrixChain = Convert.ToInt32(tbMatrixChainLength.Text);
                    int blockALength = Convert.ToInt32(tbMatrixBlockA.Text);
                    double polyPerc = replaceValue(tbPolymerPerc.Text) / 100.0;
                    double matrixType = replaceValue(tbMatrixType.Text);
                    MolComposition = structFormer.ComposeDoubleMixture(matrixChain, matrixType, polyPerc, chbDiblockMatrix.Checked, blockALength);
                }

                int molcount = Convert.ToInt32(tbMolCount.Text);
                if (chbDoubleSurf.Checked) { molcount *= 2; }

                TotalBonds = MolData.MultiplyBonds(molcount, InitBonds);

                if ((uint)InitAngles.Count > 0U)
                {
                    TotalAngles = MolData.MultiplyAngles(Convert.ToInt32(tbMolCount.Text), InitAngles);
                }

                if (chbHasMatrix.Checked)
                {
                    TotalBonds = MolData.MultiplyBonds(Convert.ToInt32(tbMatrixChainLength.Text), Convert.ToInt32(tbMolCount.Text), InitBonds, MolComposition);
                    TotalAngles = MolData.MultiplyAngles(Convert.ToInt32(tbMolCount.Text), InitAngles);
                }

                btnSaveStruct_Page1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }

        private void btnSaveStruct_Page1_Click(object sender, EventArgs e)
        {
            SaveStruct(MolComposition, TotalBonds, TotalAngles, tbXsize_Page1, tbYsize_Page1, tbZsize_Page1, tbDensity_Page1,
                       Convert.ToInt32(tbMolCount.Text), chbHasWalls_Page1.Checked, chbHasCatalysis.Checked);
        }
#endregion

        #region Page - Films
        private void btnChooseMol1_Page2_Click(object sender, EventArgs e)
        {
            LoadFile(out FilmMolOne, false, tbInit1Path_PageFilm, null, null, null, btnChooseBonds1_Page2, InitFilmBondsOne, InitFilmAnglesOne);
            if (FilmMolOne.Count == 0)
            {
                return;
            }
            else
            {
                label57.Visible = (InitFilmBondsOne.Count == 0);
                tbBonds2Path_PageFilm.Visible = (InitFilmBondsOne.Count == 0);
            }
            gbFilmParams.Enabled = true;
            btnCompose_PageFilm.Enabled = true;
        }

        private void btnChooseBonds1_Page2_Click(object sender, EventArgs e)
        {
            LoadBonds(out InitFilmBondsOne, tbBonds1Path_PageFilm);
        }

        private void btnChooseMol2_Page2_Click(object sender, EventArgs e)
        {
            LoadFile(out FilmMolTwo, false, tbInit2Path_PageFilm, null, null, null, btnChooseBonds2_Page2, InitFilmBondsTwo, InitFilmAnglesTwo);
            if (FilmMolTwo.Count == 0)
            {
                return;
            }
            else
            {
                label72.Visible = (InitFilmBondsTwo.Count == 0);
                tbBonds2Path_PageFilm.Visible = (InitFilmBondsTwo.Count == 0);
            }

            label75.Enabled = true;
            tbMolTwoPerc_PageFilm.Enabled = true;
            chbMolsUnMixed.Enabled = true;
        }

        private void btnChooseBonds2_Page2_Click(object sender, EventArgs e)
        {
            LoadBonds(out InitFilmBondsTwo, tbBonds2Path_PageFilm);
        }

        private void chbMolsUnMixed_CheckedChanged(object sender, EventArgs e)
        {
            label76.Enabled = chbMolsUnMixed.Checked;
            cmbMolLocation.Enabled = chbMolsUnMixed.Checked;
        }

        private void chbTwoSolvs_PageFilm_CheckedChanged(object sender, EventArgs e)
        {
            label89.Enabled = chbTwoSolvs_PageFilm.Checked;
            tbSecondSolv_PageFilm.Enabled = chbTwoSolvs_PageFilm.Checked;
        }

        private void chbHasWalls_PageFilm_CheckedChanged(object sender, EventArgs e)
        {
            cmbWallsType_PageFilm.Enabled = chbHasWalls_Page2.Checked;
        }

        private void btnComposeFilm_Page2_Click(object sender, EventArgs e)
        {
            try
            {
                double xSize = replaceValue(tbXsize_PageFilm.Text);
                double ySize = replaceValue(tbYsize_PageFilm.Text);
                double zSize = replaceValue(tbZsize_PageFilm.Text);
                double density = replaceValue(tbDensity_Page2.Text);

                double polPerc = replaceValue(tbPolPerc_PageFilm.Text) / 100.0;
                double molTwoPerc = replaceValue(tbMolTwoPerc_PageFilm.Text) / 100.0;
                if (!tbMolTwoPerc_PageFilm.Enabled)
                {
                    molTwoPerc = 0.0;
                }
                double percentage = replaceValue(tbSolvPerc_PageFilm.Text) / 100.0;
                bool areChainsUnmixed = chbMolsUnMixed.Checked;
                bool isAllMixed = chbAllMixed_Page2.Checked;
                bool hasWalls = chbHasWalls_Page2.Checked;
                bool twoSolvs = chbTwoSolvs_PageFilm.Checked;

                StructFormer structFormer = new StructFormer(!isAllMixed, hasWalls, cmbWallsType_PageFilm.SelectedIndex, xSize, ySize, zSize, density, percentage, (double[])null, FilmMolOne);
                double solvTwoPerc = replaceValue(tbSecondSolv_PageFilm.Text) / 100.0;

                int location = Convert.ToInt32(cmbMolLocation.SelectedIndex);
                if (location == -1)
                {
                    location = 0;
                }
                bgWorker_FilmFormer.RunWorkerAsync(new object[8] {polPerc, molTwoPerc, structFormer,areChainsUnmixed,
                                                                 location,twoSolvs,solvTwoPerc,chbNonLinear1_Page2.Checked});
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }

        private void btnSaveFilm_Page2_Click(object sender, EventArgs e)
        {
            SaveStruct(FilmComposition, TotalFilmBonds, TotalFilmAngles, tbXsize_PageFilm, tbYsize_PageFilm, tbZsize_PageFilm, tbDensity_Page2,
                          1, chbHasWalls_Page2.Checked, false);
        }

        private void bgWorker_FilmFormer_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] objects = (object[])e.Argument;
            double polyPerc = (double)objects[0];
            double molTwoPerc = (double)objects[1];
            StructFormer structFormer = (StructFormer)objects[2];
            bool areChainsUnMixed = (bool)objects[3];
            int polLocation = (int)objects[4];
            bool twoSolvs = (bool)objects[5];
            double solvTwoPerc = (double)objects[6];
            bool complexOne = (bool)objects[7];
            List<MolData> system = structFormer.ComposeFilm(out TotalFilmBonds, out TotalFilmAngles, areChainsUnMixed,
                                                            twoSolvs, complexOne, solvTwoPerc, polLocation, polyPerc, molTwoPerc,
                                                            InitFilmBondsOne, InitFilmAnglesOne, FilmMolTwo, InitFilmBondsTwo, InitFilmAnglesTwo);
            e.Result = new object[] { system };
        }

        private void bgWorker_FilmFormer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FilmComposition = (List<MolData>)((object[])e.Result)[0];
            btnSave_PageFilm.Enabled = true;
        }
        #endregion

        #region Page - Drop
        private void btnChooseMol_Page4_Click(object sender, EventArgs e)
        {
            LoadFile(out InputDropData, false, tbInitPath_PageDrop, null, null, null, btnChooseBonds_PageDrop, InitDropBonds, InitDropAngles);
            if (InputDropData.Count == 0)
            {
                return;
            }
            else
            {
                label77.Visible = (InitDropBonds.Count == 0);
                tbBondsPath_PageDrop.Visible = (InitDropBonds.Count == 0);
            }
            btnComposeDrop_Page4.Enabled = true;
        }

        private void btnChooseBonds_Page4_Click(object sender, EventArgs e)
        {
            LoadBonds(out InitDropBonds, tbBondsPath_PageDrop);       
        }

        private void chbFillWithSolvent_CheckedChanged(object sender, EventArgs e)
        {
            gbHasWater_Page3.Enabled = chbFillWithSolvent.Checked;
        }

        private void chbHasWalls_Page4_CheckedChanged(object sender, EventArgs e)
        {
            chbToSubstrate_Page4.Enabled = chbHasWalls_Page4.Checked;
            cmbWallsType_Page4.Enabled = chbHasWalls_Page4.Checked;
        }

        private void chbToSubstrate_CheckedChanged(object sender, EventArgs e)
        {
            label87.Enabled = chbToSubstrate_Page4.Checked;
            tbSubsDist_Page4.Enabled = chbToSubstrate_Page4.Checked;
        }

        private void btnComposeDrop_Page4_Click(object sender, EventArgs e)
        {
            try
            {
                double xSize = replaceValue(tbXsize_Page4.Text);
                double ySize = replaceValue(tbYsize_Page4.Text);
                double zSize = replaceValue(tbZsize_Page4.Text);
                double density = replaceValue(tbDensity_Page4.Text);
                double polPerc = replaceValue(tbPolPerc_Page4.Text) / 100.0;
                double radius = replaceValue(tbDropRadius.Text);
                double solvType = replaceValue(tbSolvType_Page4.Text);
                double dist = !chbToSubstrate_Page4.Checked ? double.NaN : replaceValue(tbSubsDist_Page4.Text);
                if ((int)solvType != 1)
                {
                    MessageBox.Show(null, "Тип молекул должен иметь формат 1.0#0!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    StructFormer structFormer = new StructFormer(chbHasWalls_Page4.Checked, cmbWallsType_Page4.SelectedIndex, xSize, ySize, zSize, density, (double[])null, InputDropData);
                    bgWorker_DropFormer.RunWorkerAsync(new object[] {polPerc,radius,solvType, structFormer, chbFillWithSolvent.Checked, chbOneMolInDrop.Checked,
                                                                     chbInPercents.Checked, chbInUnits.Checked, chbOutsideDrop.Checked,
                                                                     chbNonLinear_Page4.Checked,dist});
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }

        private void btnSaveDrop_Page4_Click(object sender, EventArgs e)
        {
            SaveStruct(DropComposition, TotalDropBonds, TotalDropAngles, tbXsize_Page4, tbYsize_Page4, tbZsize_Page4, tbDensity_Page4,
                        1, chbHasWalls_Page4.Checked, false);
        }

        private void bgWorker_DropFormer_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] objects = (object[])e.Argument;
            double polyPerc = (double)objects[0];
            double radius = (double)objects[1];
            double solvType = (double)objects[2];
            StructFormer structFormer = (StructFormer)objects[3];
            bool inBox = (bool)objects[4];
            bool oneMol = (bool)objects[5];
            bool inPercents = (bool)objects[6];
            bool inUnits = (bool)objects[7];
            bool outsideDrop = (bool)objects[8];
            bool nonLinMol = (bool)objects[9];
            double distFromSubstrate = (double)objects[10];
            var system = structFormer.ComposeDrop(out TotalDropBonds, out TotalDropAngles, inBox, oneMol, radius, polyPerc, solvType,
                                                  inPercents, inUnits, outsideDrop, nonLinMol, distFromSubstrate, InitDropBonds, InitDropAngles);
            e.Result = (object)new object[1] { system };
        }

        private void bgWorker_DropFormer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DropComposition = (List<MolData>)((object[])e.Result)[0];
            btnSaveDrop_Page4.Enabled = true;
        }

        #endregion

        #region Page - Planar Brush
        private void chbTwoSolvs_PagePlanBrush_CheckedChanged(object sender, EventArgs e)
        {
            label35.Enabled = chbTwoSolvs_PagePlanBrush.Checked;
            tbSecondSolv_PagePlanBrush.Enabled = chbTwoSolvs_PagePlanBrush.Checked;
        }

        private void btnChooseMol_PagePlanBrush_Click(object sender, EventArgs e)
        {
            LoadFile(out InputBrush, false, tbInitPath_PagePlanBrush, null, null, null,
                 btnChooseBonds_PagePlanBrush, InitBrushBonds, InitBrushAngles);
            if (InputBrush.Count == 0)
            {
                return;
            }
            else
            {
                label34.Visible = (InitBrushBonds.Count == 0);
                tbBondsPath_PagePlanBrush.Visible = (InitBrushBonds.Count == 0);
            }
            gbPlanBrush.Enabled = true;
            btnCompose_PagePlanBrush.Enabled = true;
        }

        private void btnChooseBonds_PagePlanBrush_Click(object sender, EventArgs e)
        {

            LoadBonds(out InitBrushBonds, tbBondsPath_PagePlanBrush);
        }

        private void btnChooseNP_PagePlanBrush_Click(object sender, EventArgs e)
        {
            LoadFile(out InputMicrogel_Brush, false, tbInitParticlelPath_PagePlanBrush, null, null, null,
                    btnChooseNPBonds_PagePlanBrush, MicrogelBonds_Brush, MicrogelAngles_Brush);
            if (InputMicrogel_Brush.Count == 0)
            {
                return;
            }
            else
            {
                label56.Visible = (MicrogelBonds_Brush.Count == 0);
                tbInitNPbondsPath_PagePlanBrush.Visible = (MicrogelBonds_Brush.Count == 0);
            }
            label36.Enabled = true;
            label37.Enabled = true;
            tbMicrogelCount_PlanBrush.Enabled = true;
            tbMicrogelPosition_PlanBrush.Enabled = true;
        }

        private void btnChooseNPBonds_PagePlanBrush_Click(object sender, EventArgs e)
        {

            LoadBonds(out MicrogelBonds_Brush, tbInitNPbondsPath_PagePlanBrush);
        }

        private void btnCompose_PagePlanBrush_Click(object sender, EventArgs e)
        {
            try
            {
                double xSize = replaceValue(tbXsize_PagePlanBrush.Text);
                double ySize = replaceValue(tbYsize_PagePlanBrush.Text);
                double zSize = replaceValue(tbZsize_PagePlanBrush.Text);
                double density = replaceValue(tbDensity_PagePlanBrush.Text);
                double graftDensity = replaceValue(tbGraftDensity_PagePlanBrush.Text);

                if (graftDensity > 1.0)
                {
                    MessageBox.Show(null, "Плотность пришивки не может быть больще 1!", "Внимание!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double ankerType = FileWorker.AtomTypes[Convert.ToInt32(tbAnkerBead_PagePlanBrush.Text)];

                double percentage = replaceValue(tbSolvPerc_PagePlanBrush.Text) / 100.0;
                bool twoSolvs = chbTwoSolvs_PagePlanBrush.Checked;
                double solvTwoPerc = replaceValue(tbSecondSolv_PagePlanBrush.Text) / 100.0;

                int particleCount = Convert.ToInt32(tbMicrogelCount_PlanBrush.Text);
                if (!tbMicrogelCount_PlanBrush.Enabled)
                {
                    particleCount = 0;
                }

                double mgelPos = replaceValue(tbMicrogelPosition_PlanBrush.Text);

                StructFormer structFormer = new StructFormer(true,true, cmbWallsType_PagePlanBrush.SelectedIndex,
                                                             xSize, ySize, zSize, density, percentage, (double[])null, InputBrush);

                
                bgWorkerBrush.RunWorkerAsync(new object[]{ structFormer, ankerType, graftDensity, twoSolvs,solvTwoPerc,
                                                            particleCount, mgelPos});
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_PagePlanBrush_Click(object sender, EventArgs e)
        {
            SaveStruct(BrushComposition, TotalBrushBonds, TotalBrushAngles, tbXsize_PagePlanBrush, tbYsize_PagePlanBrush,
                       tbZsize_PagePlanBrush, tbDensity_PagePlanBrush, 1, true, false);
        }

        private void bgWorkerBrush_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] objects = (object[])e.Argument;
            StructFormer structFormer = (StructFormer)objects[0];
            double ankerType = (double)objects[1];
            double graftDensity = (double)objects[2];
            bool twoSolvs = (bool)objects[3];
            double solvTwoPerc = (double)objects[4];
            int mgelCount = (int)objects[5];
            double mgelPos = (double)objects[6];

           
            List<MolData> system = structFormer.ComposePlanBrush(out TotalBrushBonds, out TotalBrushAngles, twoSolvs, solvTwoPerc,
                                                                 ankerType, graftDensity,mgelCount,mgelPos,InitBrushBonds,InitBrushAngles,
                                                                 InputMicrogel_Brush,MicrogelBonds_Brush,MicrogelAngles_Brush);
            e.Result = new object[] { system };
        }

        private void bgWorkerBrush_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BrushComposition = (List<MolData>)((object[])e.Result)[0];
            btnSave_PagePlanBrush.Enabled = true;
        }

        #endregion

        #region Page - Lipids

        private void btnChooseLipid_Page5_Click(object sender, EventArgs e)
        {
            LoadFile(out InputLipid, false, tbInitLipidPath_Page5, null, null, null, 
                     btnChooseLipidBonds_Page5, LipidBonds, LipidAngles);
            if (InputLipid.Count == 0)
            {
                return;
            }
            else
            {
                label13.Visible = (LipidBonds.Count == 0);
                tbLipidBondsPath_Page5.Visible = (LipidBonds.Count == 0);
            }
            gbLipid_Page5.Enabled = true;
            btnCreateLipid_Page5.Enabled = true;
        }

        private void btnChooseMicrogel_Page5_Click(object sender, EventArgs e)
        {
            LoadFile(out InputMicrogel_Lipid, false, tbInitMicrogelPath_PageLipid, null, null, null, 
                     btnChooseMGBonds_PageLipid, MicrogelBonds_Lipid, MicrogelAngles_Lipid);
            if (InputMicrogel_Lipid.Count == 0)
            {
                return;
            }
            else
            {
                label12.Visible = (MicrogelBonds_Lipid.Count == 0);
                tbMGBondsPath_Page5.Visible = (MicrogelBonds_Lipid.Count == 0);
            }
            label16.Enabled = true;
            tbMicrogelCount_Lipid.Enabled = true;
            chbMolsUnMixed.Enabled = true;
        }

        private void btnChooseLipidBonds_Page5_Click(object sender, EventArgs e)
        {
            LoadBonds(out LipidBonds, tbLipidBondsPath_Page5);
        }

        private void btnChooseMicrogelBonds_Page5_Click(object sender, EventArgs e)
        {
            LoadBonds(out MicrogelBonds_Lipid, tbMGBondsPath_Page5);
        }

        private void chbHasWalls_Page5_CheckedChanged(object sender, EventArgs e)
        {
            cmbWallsType_Page5.Enabled = chbHasWalls_Page5.Checked;
        }

        private void chbLayerReady_CheckedChanged(object sender, EventArgs e)
        {
            tbLipidCount.Enabled = !chbLayerReady.Checked;
            label112.Enabled = !chbLayerReady.Checked;
            cmbLipidStructType.Enabled = !chbLayerReady.Checked;
            cmbLipidStructType_SelectedIndexChanged(sender, e);
            if (!chbLayerReady.Checked)
                return;
            tbLipidCount.Text = "1";
            cmbLipidStructType.SelectedIndex = 0;
        }

        private void chbHasLigands_CheckedChanged(object sender, EventArgs e)
        {
            lblLigand.Visible = chbHasLigands.Checked;
            tbLigandPerc.Visible = chbHasLigands.Checked;
        }

        private void cmbLipidStructType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLipidStructType.SelectedIndex == 0)
            {
                label20.Enabled = true;
                tbLayerPosition.Enabled = true;
                if ((uint)InputMicrogel_Lipid.Count <= 0U)
                {
                    return;
                }
                label27.Enabled = true;
                tbMicrogelPosition_Lipid.Enabled = true;
            }
            else
            {
                label20.Enabled = false;
                label27.Enabled = false;
                tbLayerPosition.Enabled = false;
                tbMicrogelPosition_Lipid.Enabled = false;
            }
        }

        private void btnCreateLipid_Page5_Click(object sender, EventArgs e)
        {
            try
            {
                double xSize = replaceValue(tbXsize_Page5.Text);
                double ySize = replaceValue(tbYsize_Page5.Text);
                double zSize = replaceValue(tbZsize_Page5.Text);
                double density = replaceValue(tbDensity_Page5.Text);
                double ligandPerc = replaceValue(tbLigandPerc.Text)/100.0;

                if (!chbHasLigands.Checked)
                {
                    ligandPerc = 0.0;
                }
                int lipidNum = Convert.ToInt32(tbLipidCount.Text);
                int mgelCount = Convert.ToInt32(tbMicrogelCount_Lipid.Text);
                if (!tbMicrogelCount_Lipid.Enabled)
                {
                    mgelCount = 0;
                }
                double layerPos = replaceValue(tbLayerPosition.Text);
                double mgelPos = replaceValue(tbMicrogelPosition_Lipid.Text);
                bool structType = Convert.ToBoolean(cmbLipidStructType.SelectedIndex);
                StructFormer structFormer = new StructFormer(chbHasWalls_Page5.Checked, cmbWallsType_Page5.SelectedIndex, 
                                                             xSize, ySize, zSize, density, null, InputLipid);
                
                bgWorker_Lipid.RunWorkerAsync(new object[]{ structType,structFormer, lipidNum, ligandPerc,
                                                            mgelCount,layerPos, mgelPos, chbLipidTwoLiqs.Checked});
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnSaveLipid_Page5_Click(object sender, EventArgs e)
        {
            SaveStruct(LipidComposition, TotalLipidBonds, TotalLipidAngles, tbXsize_Page5, tbYsize_Page5, tbZsize_Page5, 
                       tbDensity_Page5, 1, chbHasWalls_Page5.Checked, false);
        }

        private void bgWorker_Lipid_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] objects = (object[])e.Argument;
            bool isRandom = (bool)objects[0];
            StructFormer structFormer = (StructFormer)objects[1];
            int lipidNum = (int)objects[2];
            double ligandPerc = (double)objects[3];
            int microgelNum = (int)objects[4];
            double layerPosition = (double)objects[5];
            double microgelPosition = (double)objects[6];
            bool twoLiqs = (bool)objects[7];

            List<MolData> system = structFormer.ComposeLipidSystem(out TotalLipidBonds, out TotalLipidAngles, isRandom, lipidNum, ligandPerc, 
                                                                   microgelNum, layerPosition, microgelPosition, twoLiqs, 
                                                                   LipidBonds, LipidAngles, 
                                                                   InputMicrogel_Lipid, MicrogelBonds_Lipid, MicrogelAngles_Lipid);
            e.Result = new object[] { system };
        }

        private void bgWorker_Lipid_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LipidComposition = (List<MolData>)((object[])e.Result)[0];
            btnSaveLipid_Page5.Enabled = true;
        }
        #endregion

        #region Page - Synthesis
        private void btnCalcOptimalAmounts_Click(object sender, EventArgs e)
        {
            try
            {
                double[] arbRes = ArbSynth.CalcOptimalComposition(Convert.ToInt32(tbGraftLength_ZeroGen.Text), Convert.ToInt32(tbGraftLength_FourthGen.Text));
                tbFirstGen.Text = ((int)arbRes[0]).ToString();
                tbSecondGen.Text = ((int)arbRes[1]).ToString();
                tbThirdGen.Text = ((int)arbRes[2]).ToString();
                tbFourthGen.Text = ((int)arbRes[3]).ToString();
                tbTypeBPercentage.Text = arbRes[4].ToString();
            }
            catch
            {
                MessageBox.Show(null, "Введите длину пришвки!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void tbCalcTypeBFraction_Click(object sender, EventArgs e)
        {
            try
            {
                tbTypeBPercentage.Text = ArbSynth.CalcTypeBpercentage(Convert.ToInt32(tbGraftLength_ZeroGen.Text), Convert.ToInt32(tbGraftLength_FourthGen.Text), 
                                                                      Convert.ToInt32(tbFirstGen.Text), Convert.ToInt32(tbSecondGen.Text), 
                                                                      Convert.ToInt32(tbThirdGen.Text), Convert.ToInt32(tbFourthGen.Text)).ToString();
            }
            catch
            {
                MessageBox.Show(null, "Не введено значение для како-либо из генераций!\nПроверьте, заполнены ли все поля!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btnSynth_Click(object sender, EventArgs e)
        {
            int graftAmount = 0;

            string message = "";

            // microgels
            if (tabControl2.SelectedIndex == 1)
            {
            }

            // arbs
            if (tabControl2.SelectedIndex == 1)
            {
                ArbSynth arbSynth = new ArbSynth(Convert.ToInt32(tbGraftLength_Substrate.Text), Convert.ToInt32(tbGraftLength_ZeroGen.Text), 
                                                 Convert.ToInt32(tbGraftLength_FirstGen.Text), Convert.ToInt32(tbGraftLength_SecondGen.Text),
                                                 Convert.ToInt32(tbGraftLength_ThirdGen.Text), Convert.ToInt32(tbGraftLength_FourthGen.Text),
                                                 Convert.ToInt32(tbZeroGen.Text),replaceValue(tbFirstGen.Text),
                                                 replaceValue(tbSecondGen.Text), replaceValue(tbThirdGen.Text), replaceValue(tbFourthGen.Text), 
                                                 chbArbHasAngles.Checked, chbIsDiblock.Checked,chbAllDifferent.Checked, 
                                                 replaceValue(tbGraftFraction.Text) / 100.0,
                                                 new bool[] { chbIrregGraftG1.Checked, chbIrregGraftG2.Checked, chbIrregGraftG3.Checked, chbIrregGraftG4.Checked });
                arbSynth.CreateMolecule();
                graftAmount = arbSynth.totalLastGrafts;
                SynthBonds = arbSynth.bonds;
                SynthMol = arbSynth.molecule;

                message = "Молекула готова! Количество бидов = " + SynthMol.Count.ToString() + "\n," +
                            "Число пришивок в последней генерации = " + graftAmount.ToString();
            }
            else if (tabControl2.SelectedIndex == 2)
            {
                int backBone = Convert.ToInt32(tbBBoneLength.Text);
                int sideChain = Convert.ToInt32(tbSideChainLength.Text);
                int graftDensity = Convert.ToInt32(tbGraftDensity.Text);
                int sideChainType = cmbSideChainsType.SelectedIndex + 1;
                int blockBlength = (int)(replaceValue(tbGraftBlength.Text) * (double)sideChain / 100.0);
                double blockBfrac = replaceValue(tbGraftBfrac.Text) / 100.0;
                double dbFrac = (double)Convert.ToInt32(tbsChainDbFrac.Text) / 100.0;

                int chainNum = 1;
                if (chbGenManyCombs.Checked)
                {
                    chainNum = Convert.ToInt32(tbCombNum.Text);
                }

                CombSynth combSynth = new CombSynth(sideChainType, graftDensity, backBone, sideChain, chbIsDoubleGraft.Checked,
                                                    chbIndBB.Checked, chbIsRandom.Checked, chainNum, dbFrac, blockBlength, blockBfrac,
                                                    replaceValue(tbXsize_PageSynth.Text), replaceValue(tbYsize_PageSynth.Text), replaceValue(tbZsize_PageSynth.Text));
                combSynth.CreateMolecule();
                SynthBonds = combSynth.bonds;
                SynthMol = combSynth.molecule;

                message = "Молекула готова! Количество бидов = " + SynthMol.Count.ToString();
            }
            else
            {
                if (InputFuncMol.Count !=0 && InitFuncBonds.Count !=0)
                {
                    var subsType = FileWorker.AtomTypes[(int)replaceValue(tbBeadFuncSubsType.Text)];
                    var funcType = FileWorker.AtomTypes[(int)replaceValue(tbBeadFuncGraftType.Text)];

                    var funcLength = 1;
                    if (chbFuncWithChains.Checked)
                    {
                        funcLength = Convert.ToInt32(tbFuncChainLength.Text);
                    }
                    


                    var funcSynth = new FuncSynth(chbBoundExisting.Checked,chbFuncWithChains.Checked, funcLength, subsType, funcType,
                                                  replaceValue(tbBeadFuncSubsFrac.Text)/100.0, InputFuncMol, InitFuncBonds);

                    NewFuncMol = funcSynth.FunctionalizeMolecule();
                    TotalFuncBonds = funcSynth.finalBonds;

                    if (chbBoundExisting.Checked)
                    {
                        message = "Функционализация завершена! Количество новых связей = " + (TotalFuncBonds.Count - InitFuncBonds.Count).ToString();
                    }
                    else
                    {
                        message = "Функционализация завершена! Количество новых бидов = " + (NewFuncMol.Count - InputFuncMol.Count).ToString();
                    }
                }

                else
                {
                    message = "Молекула не указана! Функционализация невозможна!";                     
                }
            }

            MessageBox.Show(null, message , "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnSave_PageSynth_Click(object sender, EventArgs e)
        {
            //saveFileDialog.Filter = "Конфиг. файлы Lammps (*.txt)|*.txt|Файлы RasMol (*.ent)|*.ent|Текстовые файлы XYZR (*.xyzr)|*.xyzr";
            //if (saveFileDialog.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}
            double[] scales = new double[3] { replaceValue(tbXsize_PageSynth.Text), replaceValue(tbYsize_PageSynth.Text), replaceValue(tbZsize_PageSynth.Text) };

            var finalstruct = SynthMol;
            var finalbonds = SynthBonds;
            var finalAngles = SynthAngles;

            if (tabControl2.SelectedIndex == 3 && NewFuncMol.Count > 0)
            {
                finalstruct = NewFuncMol;
                finalbonds = TotalFuncBonds;
                finalAngles = InitFuncAngles;
            }

            SaveStruct(finalstruct, finalbonds, finalAngles, scales, 3, 1, false, false);

            //switch (saveFileDialog.FilterIndex)
            //{
            //    case 1:
            //        FileWorker.Save_Conf(saveFileDialog.FileName, scales, 3, atomTypes, bondTypes, angleTypes, finalbonds, finalAngles, finalstruct);
            //        break;
            //    case 2:
            //        FileWorker.Save_MOL(saveFileDialog.FileName, finalbonds, finalstruct);
            //        StartProcces(saveFileDialog.FileName);
            //        break;
            //    case 3:
            //        FileWorker.Save_XYZ(saveFileDialog.FileName, true, scales, finalstruct);
            //        FileWorker.SaveBonds_DAT(new FileInfo(saveFileDialog.FileName).DirectoryName + "\\bonds.dat", finalbonds);
            //        StartProcces(saveFileDialog.FileName);
            //        break;
            //}
        }

        private void btnChooseMol_Page6_Click(object sender, EventArgs e)
        {
            LoadFile(out InputFuncMol, false, tbInitPath_Page5, tbXsize_PageSynth, tbYsize_PageSynth, tbZsize_PageSynth, 
                    btnChooseBonds_Page6, InitFuncBonds, InitFuncAngles);
            if (InputFuncMol.Count == 0)
            {
                return;
            }
            else
            {
                label28.Visible = (InitFuncBonds.Count == 0);
                tbBondsPath_Page6.Visible = (InitFuncBonds.Count == 0);
            }
        }

        private void btnChooseBonds_Page6_Click(object sender, EventArgs e)
        {
            LoadBonds(out InitFuncBonds, tbBondsPath_Page6);
        }
     
        private void chbIsDiblock_CheckedChanged(object sender, EventArgs e)
        {
            label90.Enabled = chbIsDiblock.Checked;
            tbGraftFraction.Enabled = chbIsDiblock.Checked;
        }

        private void cmbSideChainsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSideChainsType.SelectedIndex != 2)
            {
                chbIndBB.Enabled = false;
                lblMatya1.Enabled = false;
                lblMatya2.Enabled = false;
                tbGraftBlength.Enabled = false;
                tbGraftBfrac.Enabled = false;
            }
            else
            {
                chbIndBB.Enabled = true;
                lblMatya1.Enabled = true;
                lblMatya2.Enabled = true;
                tbGraftBlength.Enabled = true;
                tbGraftBfrac.Enabled = true;
            }
            if (cmbSideChainsType.SelectedIndex != 1)
            {
                lblDbFrac.Enabled = false;
                tbsChainDbFrac.Enabled = false;
            }
            else
            {
                lblDbFrac.Enabled = true;
                tbsChainDbFrac.Enabled = true;
            }
        }

        private void chbAllDifferent_CheckedChanged(object sender, EventArgs e)
        {
            chbIsDiblock.Enabled = !chbAllDifferent.Checked;
            if (chbAllDifferent.Checked)
            {
                chbIsDiblock.Checked = false;
            }
        }

        private void chbBoundExisting_CheckedChanged(object sender, EventArgs e)
        {
            label32.Enabled = !chbBoundExisting.Checked;
            tbBeadFuncSubsFrac.Enabled = !chbBoundExisting.Checked;
        }

        private void chbFuncWithChains_CheckedChanged(object sender, EventArgs e)
        {
            label70.Enabled = chbFuncWithChains.Checked;
            tbFuncChainLength.Enabled = chbFuncWithChains.Checked;
        }

        private void chbGenManyCombs_CheckedChanged(object sender, EventArgs e)
        {
            label88.Enabled = chbGenManyCombs.Checked;
            tbCombNum.Enabled = chbGenManyCombs.Checked;

        }

        #endregion

        #region Infrastructure
        private void LoadFile(out List<double[]> data, bool forEdit, TextBox tbPath, 
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

                        data = FileWorker.LoadLammpstrjLines(openFileDialog.FileName, out snapnum, out sizes);
                        if (tbX != null && tbY != null && tbZ != null)
                        {
                            tbX.Text = sizes[0].ToString();
                            tbY.Text = sizes[1].ToString();
                            tbZ.Text = sizes[2].ToString();
                        }
                        btn.Enabled = true;
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
                    }
                    else if (openFileDialog.FilterIndex == 3)
                    {
                        data = FileWorker.LoadMolLines(openFileDialog.FileName, bonds);
                        btn.Visible = false;
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
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Произошла ошибка при чтении!\nУбедитесь, что выбранный файл имеет нужный формат!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    data = new List<double[]>(); ;
                }
            }
            else
            {
                data = new List<double[]>();
                return;
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
                    if (openFileDialog.FilterIndex == 2)
                    {
                        bonds = FileWorker.LoadBonds(tbPath.Text, 2);
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
            double[] sizes = new double[] { replaceValue(tbX.Text),
                                            replaceValue(tbY.Text),
                                            replaceValue(tbZ.Text)};

            double density = replaceValue(tbDenstiy.Text);
            SaveStruct(system, bonds, angles, sizes, density,
                       molCount,hasWalls, addBonds);

        }

        private void SaveStruct(List<MolData> system, List<int[]> bonds, List<int[]> angles, 
                                double[] sizes, double density, 
                                int molCount, bool hasWalls, bool addBonds)
        {
            saveFileDialog.Filter = "Файлы RasMol (*.ent)|*.ent|Lammps-файлы траекторные (*.lammpstrj)|*.lammpstrj|Конфиг. файлы Lammps (*.txt)|*.txt|Restart-файлы (*.dat)|*.dat|Текстовые файлы XYZR (*.xyzr)|*.xyzr";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            int boundaryCond = 3;
            if (hasWalls)
                boundaryCond = 6;
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
                    if ((uint)angles.Count > 0U)
                    {
                        angleTypes = MolData.CalcAngles(angles, system);
                    }
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
