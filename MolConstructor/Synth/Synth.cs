// -----------------------------------------------------------------------
// <copyright file="ArbSynth.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MolConstructor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// 

    public abstract class Synth
    {
        protected double step;
        protected List<int[]> graftBonds;
        protected List<int[]> graftAngles;
        public int totalLastGrafts;
        public List<int[]> bonds;
        public List<int[]> angles;
        public List<MolData> molecule;
    }

    public class MicrogelSynth : Synth
    {
        private bool isCoreShell;
        private bool isHollow;
        private bool isCopol;
        private int copolType;
        private int subchainLength_main;
        private int subchainLength_other;
        private double polBfraction;
        private double cutoffRad_main;
        private double cutoffRad_second;
        private double cutoffRad_hollow;

        public MicrogelSynth (bool isCoreShell, bool isHollow, bool isCopol, int copolType,
                              int subchainLength_main, int subchainLength_other, double polBfraction,
                              double cutoffRad_main, double cutoffRad_second, double cutoffRad_hollow)
        {
            this.isCoreShell = isCoreShell;
            this.isHollow = isHollow;
            this.isCopol = isCopol;
            this.copolType = copolType;
            this.subchainLength_main = subchainLength_main;
            this.subchainLength_other = subchainLength_other;
            this.polBfraction = polBfraction;
            this.cutoffRad_main = cutoffRad_main;
            this.cutoffRad_second = cutoffRad_second;
            this.cutoffRad_hollow = cutoffRad_hollow;
        }

        public void CreateMolecule(List<double[]> lattice, List<int[]> latticebonds)
        {
            var mesh = new List<double[]>();
            var meshBonds = new List<int[]>();

            //CreateMesh(lattice, latticebonds, mesh, meshBonds);
            //CutTheShit(mesh, meshBonds);
            //ColorIt();
            //FinalizeIt();
        }

        //// creates the initial mesh according to the pattern
        //private void CreateMesh(List<double[]> lattice, List<int[]> latticebonds, List<double[]> mesh, List<int[]> meshBonds)
        //{

        //    if (isCoreShell && subchainLength_main != subchainLength_other)
        //    {

        //    }
        //    else
        //    {
        //        // the code by Sergei Filippov
        //        subchainLength_main += 1;
        //        int o, p, bondsn = 0, nBond = latticebonds.Count, atoms = -1;

        //        var init = new List<int>();
        //        for (int i = 0; i < 2 * nBond; i++)
        //        {
        //            init.Add(-1);
        //        }

            
        //        var NewCoords = new List<string> { "x", "y", "z" }.ToDictionary(el => el, el => new List<double>());
        //        var NewBonds = new List<string> { "i", "j" }.ToDictionary(el => el, el => new List<int>());


        //        for (int i = 0; i < nBond; i++)
        //        {
        //            if (init[bonds["i"][i]] != -1) o = 1; else o = 0;
        //            if (init[bonds["j"][i]] != -1) p = subchainLength_main - 1; else p = subchainLength_main;

        //            int atomso = atoms + 1;

        //            for (int j = o; j <= p; j++)
        //            {
        //                atoms += 1;
        //                if (j == 0) init[bonds["i"][i]] = atoms;
        //                if (j == num) init[bonds["j"][i]] = atoms;
        //                mesh["x"].Add(coords["x"][bonds["i"][i]] +
        //                                  (coords["x"][bonds["j"][i]] - coords["x"][bonds["i"][i]]) * ((double)j) / num);
        //                mesh["y"].Add(coords["y"][bonds["i"][i]] +
        //                                  (coords["y"][bonds["j"][i]] - coords["y"][bonds["i"][i]]) * ((double)j) / num);
        //                mesh["z"].Add(coords["z"][bonds["i"][i]] +
        //                                  (coords["z"][bonds["j"][i]] - coords["z"][bonds["i"][i]]) * ((double)j) / num);
        //            }

        //            int atomsp = atoms, atomi = init[bonds["i"][i]], atomj = init[bonds["j"][i]];
        //            for (int j = 1; j <= subchainLength_main; j++)
        //            {
        //                bondsn += 1;
        //                if (j == 1)
        //                {
        //                    meshBonds["i"].Add(atomi);
        //                    meshBonds["j"].Add(atomso + 1 - o);
        //                }
        //                else if (j == subchainLength_main)
        //                {
        //                    meshBonds["i"].Add(atomsp + subchainLength_main - p - 1);
        //                    meshBonds["j"].Add(atomj);
        //                }
        //                else
        //                {
        //                    meshBonds["i"].Add(atomso + j - o - 1);
        //                    meshBonds["j"].Add(atomso + j - o);
        //                }
        //            }
        //        }

             
        //    }

        //    static void Gel(int num, double tt, double R, double frac, bool LinkingCheck, int blocks, Dictionary<string, List<double>> coords, List<int> types, Dictionary<string, List<int>> bonds,
        //   out Dictionary<string, List<double>> GelCoords, out List<int> GelTypes, out Dictionary<string, List<int>> GelBonds)
        //    {


        //        num += 1;
        //        Random rnd = new Random();
        //        int o, p, bondsn = 0, Nbond = bonds["i"].Count, atoms = -1;
        //        List<int> was = new List<int>(new int[2 * Nbond]);
        //        for (int i = 0; i < 2 * Nbond; i++) was[i] = -1;

        //        GelCoords = new List<string> { "x", "y", "z" }.ToDictionary(el => el, el => new List<double>());
        //        var NewCoords = new List<string> { "x", "y", "z" }.ToDictionary(el => el, el => new List<double>());

        //        GelTypes = new List<int>();
        //        GelBonds = new List<string> { "i", "j" }.ToDictionary(el => el, el => new List<int>());
        //        var NewBonds = new List<string> { "i", "j" }.ToDictionary(el => el, el => new List<int>());


        //        for (int i = 0; i < Nbond; i++)
        //        {
        //            if (was[bonds["i"][i]] != -1) o = 1; else o = 0;
        //            if (was[bonds["j"][i]] != -1) p = num - 1; else p = num;

        //            int atomso = atoms + 1;

        //            for (int j = o; j <= p; j++)
        //            {
        //                atoms += 1;
        //                if (j == 0) was[bonds["i"][i]] = atoms;
        //                if (j == num) was[bonds["j"][i]] = atoms;
        //                NewCoords["x"].Add(coords["x"][bonds["i"][i]] +
        //                                  (coords["x"][bonds["j"][i]] - coords["x"][bonds["i"][i]]) * ((double)j) / num);
        //                NewCoords["y"].Add(coords["y"][bonds["i"][i]] +
        //                                  (coords["y"][bonds["j"][i]] - coords["y"][bonds["i"][i]]) * ((double)j) / num);
        //                NewCoords["z"].Add(coords["z"][bonds["i"][i]] +
        //                                  (coords["z"][bonds["j"][i]] - coords["z"][bonds["i"][i]]) * ((double)j) / num);
        //            }

        //            int atomsp = atoms, atomi = was[bonds["i"][i]], atomj = was[bonds["j"][i]];
        //            for (int j = 1; j <= num; j++)
        //            {
        //                bondsn += 1;
        //                if (j == 1)
        //                {
        //                    NewBonds["i"].Add(atomi);
        //                    NewBonds["j"].Add(atomso + 1 - o);
        //                }
        //                else if (j == num)
        //                {
        //                    NewBonds["i"].Add(atomsp + num - p - 1);
        //                    NewBonds["j"].Add(atomj);
        //                }
        //                else
        //                {
        //                    NewBonds["i"].Add(atomso + j - o - 1);
        //                    NewBonds["j"].Add(atomso + j - o);
        //                }
        //            }
        //        }

        //        int l = 0;
        //        atoms += 1;
        //        var used = new List<int>(new int[atoms + 1]);
        //        for (int i = 0; i < atoms; i++) used[i] = -1;

        //        for (int i = 0; i < atoms; i++)

        //            if ((NewCoords["x"][i] - tt) * (NewCoords["x"][i] - tt) + (NewCoords["y"][i] - tt) * (NewCoords["y"][i] - tt) +
        //                                                                        (NewCoords["z"][i] - tt) * (NewCoords["z"][i] - tt) <= R * R)
        //            {
        //                GelCoords["x"].Add(NewCoords["x"][i]);
        //                GelCoords["y"].Add(NewCoords["y"][i]);
        //                GelCoords["z"].Add(NewCoords["z"][i]);
        //                used[i] = l;
        //                l += 1;

        //                int r = rnd.Next(1000);
        //                if (r < frac * 1000) GelTypes.Add(1); else GelTypes.Add(2);
        //            }

        //        for (int i = 0; i < bondsn; i++)

        //            if ((used[NewBonds["i"][i]] != -1) && (used[NewBonds["j"][i]] != -1))
        //            {
        //                GelBonds["i"].Add(used[NewBonds["i"][i]]);
        //                GelBonds["j"].Add(used[NewBonds["j"][i]]);
        //            }

        //        if (LinkingCheck) // check the presence of standalone beads after cropping
        //        {

        //            var query = new List<int>();
        //            var u = new List<bool>(new bool[GelTypes.Count]);
        //            int q1 = 0, q2 = 0;
        //            query.Add(0); u[0] = true;

        //            int[,] MasBonds = new int[GelTypes.Count, GelTypes.Count];
        //            for (int i = 0; i < GelBonds["i"].Count; i++)
        //            {
        //                MasBonds[GelBonds["i"][i], GelBonds["j"][i]] = 1;
        //                MasBonds[GelBonds["j"][i], GelBonds["i"][i]] = 1;
        //            }

        //            while (q1 <= q2)
        //            {
        //                int t = query[q1];
        //                for (int i = 0; i < GelTypes.Count; i++)
        //                {
        //                    if ((MasBonds[i, t] == 1) && (u[i] == false))
        //                    {
        //                        query.Add(i);
        //                        u[i] = true;
        //                        q2 += 1;
        //                    }
        //                }
        //                q1 += 1;
        //            }

        //            var cumm = new List<int>();
        //            int c = 0;
        //            for (int i = 0; i < GelTypes.Count; i++)
        //            {
        //                if (u[i] == false)
        //                {
        //                    GelCoords["x"].RemoveAt(i - c);
        //                    GelCoords["y"].RemoveAt(i - c);
        //                    GelCoords["z"].RemoveAt(i - c);
        //                    GelTypes.RemoveAt(i - c);
        //                    for (int j = 0; j < GelBonds["i"].Count; j++)
        //                        if ((GelBonds["i"][j] == i) || (GelBonds["j"][j] == i))
        //                        {
        //                            GelBonds["i"].RemoveAt(j);
        //                            GelBonds["j"].RemoveAt(j);
        //                        }
        //                    c += 1;
        //                }
        //                cumm.Add(c);
        //            }
        //            for (int i = 0; i < GelBonds["i"].Count; i++)
        //            {
        //                GelBonds["i"][i] -= cumm[GelBonds["i"][i]];
        //                GelBonds["j"][i] -= cumm[GelBonds["i"][i]];
        //            }
        //        }

        //        if (blocks > -1)
        //        {
        //            int inter = 0;
        //            for (int i = 0; i < GelTypes.Count; i++)
        //            {
        //                int s = 0;
        //                for (int j = 0; j < GelBonds["i"].Count; j++)
        //                    if ((GelBonds["i"][j] == i) || (GelBonds["j"][j] == i))
        //                        s += 1;
        //                if (s > 2)
        //                {
        //                    inter = i;
        //                    break;
        //                }
        //            }

        //            var query = new List<int>();
        //            var colorx = new List<int>();
        //            var u = new List<bool>(new bool[GelTypes.Count]);
        //            int q1 = 0, q2 = 0;
        //            colorx.Add(0);
        //            query.Add(inter); u[inter] = true;
        //            GelTypes[inter] = 1;

        //            int[,] MasBonds = new int[GelTypes.Count, GelTypes.Count];

        //            for (int i = 0; i < GelBonds["i"].Count; i++)
        //            {
        //                MasBonds[GelBonds["i"][i], GelBonds["j"][i]] = 1;
        //                MasBonds[GelBonds["j"][i], GelBonds["i"][i]] = 1;
        //            }

        //            while (q1 <= q2)
        //            {
        //                for (int i = 0; i < GelTypes.Count; i++)
        //                {
        //                    if ((MasBonds[query[q1], i] == 1) && (u[i] == false))
        //                    {
        //                        q2 += 1; query.Add(i); u[i] = true;
        //                        colorx.Add(colorx[q1] + 1);
        //                        if (colorx[q2] == 2 * (num)) colorx[q2] = 0;
        //                        if ((colorx[q2] <= blocks) || (colorx[q2] >= 2 * (num) - blocks))
        //                            GelTypes[query[q2]] = 1;
        //                        else
        //                            GelTypes[query[q2]] = 2;
        //                    }
        //                }
        //                q1 += 1;
        //            }

        //        }

        //    }
        //}

        //private void CutTheShit(List<double[]> mesh, List<int[]> meshBonds)
        //{
        //    int l = 0;
        //    atoms += 1;
        //    var used = new List<int>(new int[atoms + 1]);
        //    for (int i = 0; i < atoms; i++) used[i] = -1;

        //    for (int i = 0; i < atoms; i++)

        //        if ((NewCoords["x"][i] - tt) * (NewCoords["x"][i] - tt) + (NewCoords["y"][i] - tt) * (NewCoords["y"][i] - tt) +
        //                                                                    (NewCoords["z"][i] - tt) * (NewCoords["z"][i] - tt) <= R * R)
        //        {
        //            GelCoords["x"].Add(NewCoords["x"][i]);
        //            GelCoords["y"].Add(NewCoords["y"][i]);
        //            GelCoords["z"].Add(NewCoords["z"][i]);
        //            used[i] = l;
        //            l += 1;
        //        }

        //    for (int i = 0; i < bondsn; i++)

        //        if ((used[NewBonds["i"][i]] != -1) && (used[NewBonds["j"][i]] != -1))
        //        {
        //            GelBonds["i"].Add(used[NewBonds["i"][i]]);
        //            GelBonds["j"].Add(used[NewBonds["j"][i]]);
        //        }

        //}

        //private void ColorIt()
        //{
        //    switch (copolType)
        //    {
        //        case 0:
        //            {
        //                break;
        //            }
        //        case 1:
        //            {
        //                break;
        //            }
        //        case 2:
        //            {
        //                break;
        //            }
        //        case 3:
        //            {
        //                break;
        //            }

        //    }
        //    if (copolType == 0)
        //    {

        //    }

        //}

        //private void FinalizeIt()
        //{

        //}
    }


    public class ArbSynth : Synth
    {
        private const int MAXGRAFTSPERCHAIN = 10;
        private bool allDifferent;
        private bool isDiblock;
        private bool hasAngles;
        private int substrateLength;
        private int typeBlength;
        private int[] genLengths = new int[5];
        private double[] genAmounts = new double[5];
        private bool[] randGrafts = new bool[3];

        public ArbSynth(int substrateLength, int zeroGenLength,
                        int firstGenLength, int secondGenLength,
                        int thirdGenLength, int fourthGenLength, 
                        int zeroGenAmount,
                        double firstGenAmount,
                        double secondGenAmount,
                        double thirdGenAmount, double fourthGenAmount,
                        bool hasAngles, bool isDiblock, bool allDifferent, 
                        double fractionBPerGraft,
                        bool[] randGrafts)
        {
            this.isDiblock = isDiblock;
            this.allDifferent = allDifferent;
            this.hasAngles = hasAngles;
            this.substrateLength = substrateLength;

            genLengths[0] = zeroGenLength;
            genLengths[1] = firstGenLength;
            genLengths[2] = secondGenLength;
            genLengths[3] = thirdGenLength;
            genLengths[4] = fourthGenLength;

            genAmounts[0] = zeroGenAmount;
            genAmounts[1] = firstGenAmount;
            genAmounts[2] = secondGenAmount;
            genAmounts[3] = thirdGenAmount;
            genAmounts[4] = fourthGenAmount;

            this.randGrafts = randGrafts;

            step = 1.0 / Math.Pow(3.0, 1.0 / 3.0);

            for (int i =0; i< genLengths.Length; i++)
            {
                if (genAmounts[i] != 0)
                {
                    typeBlength = (int)(fractionBPerGraft * genLengths[i]);
                }
                else
                    break;
            }

            graftBonds = new List<int[]>();
            bonds = new List<int[]>();
            molecule = new List<MolData>();
        }

        public void CreateMolecule()
        {
             int[] graftsInGen = new int[5];

            List<int[]> graftBonds = new List<int[]>();

            //Linear substrate
            AddOneGraft(0, 0, 0, 0, 45.0, 0, 1.00);

            //For Papadakis
            foreach (var c in molecule)
            {
                c.AtomType = 1.04;
            }

            // Main cycle for 
            for (int i = 0; i <= 4; i++)
            {
                // check if the last generation
                var isLast = ((i < 4 && genAmounts[i + 1] == 0) || i == 4) ? true : false;

                // account all the subchains in <= n-2 generation which are already grafted 
                int prevChains = 0;

                // zero generation
                if (i == 0)
                {
                    AddGens(1, 0, (int)genAmounts[0]);

                    graftsInGen[0] = (int)genAmounts[0];
                }
                else
                {
                    int substrateChains = 1;

                    for (int j = 0; j < i; j++)
                    {
                        prevChains += substrateChains;
                        substrateChains = graftsInGen[j];
                    }

                    if (genAmounts[i] != 0)
                    {
                        totalLastGrafts = (int)(substrateChains * genAmounts[i]);

                        if (genAmounts[i] > (int)genAmounts[i])
                        {
                            // integer number of grafts
                            var fullGraft = (int)genAmounts[i];

                            int totalCounter = 0;

                            // use in case if the number of remained grafts is not 1/n (like 1/2,1/3,1/4 etc.)
                            if (i > 0 && randGrafts[i - 1])
                            {
                                var rand = new Random();

                                var manygrafts = new List<int>();

                                // the number of grafts for random grafting together with the fullGraft;
                                int randGraftsNum = totalLastGrafts - substrateChains * fullGraft;
                                int randCounter = 0;

                                do
                                {
                                    int currNum = rand.Next(1, substrateChains);

                                    if (!manygrafts.Contains(currNum))
                                    {
                                        manygrafts.Add(currNum);
                                        AddGens(prevChains + currNum, i, fullGraft + 1);
                                        totalCounter += fullGraft + 1;
                                        randCounter++;
                                    }

                                } while (randCounter < randGraftsNum);

                                for (int j = 1; j <= substrateChains; j++)
                                {
                                    if (!manygrafts.Contains(j))
                                    {
                                        if (fullGraft != 0)
                                        {
                                            AddGens(prevChains + j, i, fullGraft);
                                        }
                                        totalCounter += fullGraft;
                                    }
                                }
                            }
                            else
                            {
                                int coef = (int)(1 / (genAmounts[i] - fullGraft));

                                if ((genAmounts[i] - fullGraft) > 0.5)
                                {
                                    coef = (int)(1 / (1 - genAmounts[i] + fullGraft));
                                }

                                int cycleCounter = 0;

                                for (int j = 1; j <= substrateChains; j++)
                                {
                                    cycleCounter++;
                                    if ((genAmounts[i] - fullGraft) > 0.5)
                                    {
                                        if (cycleCounter != coef)
                                        {
                                            AddGens(prevChains + j, i, fullGraft + 1);
                                            totalCounter += fullGraft + 1;
                                        }
                                        else
                                        {
                                            if (fullGraft != 0)
                                            {
                                                AddGens(prevChains + j, i, fullGraft);
                                                totalCounter += fullGraft;
                                            }
                                            cycleCounter = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (cycleCounter == coef)
                                        {
                                            AddGens(prevChains + j, i, fullGraft + 1);
                                            totalCounter += fullGraft + 1;
                                            cycleCounter = 0;
                                        }
                                        else
                                        {
                                            if (fullGraft != 0)
                                            {
                                                AddGens(prevChains + j, i, fullGraft);
                                                totalCounter += fullGraft;
                                            }
                                        }
                                    }
                                }
                            }

                            totalLastGrafts = totalCounter;
                        }
                        else
                        {
                            for (int j = 1; j <= substrateChains; j++)
                            {
                                AddGens(prevChains + j, i, (int)genAmounts[i]);
                            }
                        }

                        graftsInGen[i] = totalLastGrafts;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Recolor (robin)
            //{
            //    foreach (var c in molecule)
            //    {
            //        var beadBonds = new List<int>();
            //        foreach (var p in bonds)
            //        {
            //            if (p[0] == c.Index)
            //            {
            //                beadBonds.Add(p[1]);
            //            }
            //            if (p[1] == c.Index)
            //            {
            //                beadBonds.Add(p[0]);
            //            }
            //        }

            //        c.Bonds = beadBonds;
            //    }

            //    foreach (var c in molecule)
            //    {
            //        if (c.Bonds.Count > 2)
            //        {
            //            c.AtomType = 1.01;
            //        }
            //    }
            //}
        }

        private void AddGens(int chainNum, int genNum, int graftAmount)
        {
            var positions = CalcPositions(genNum, graftAmount);

            int beads = molecule.Count;

            var initGraftInd = molecule.Max(x => x.MolIndex);

            var counter = 1;

            var type = 1.00;

            // in case if we want to visualize the distribution of the grafts within each gen
            if (allDifferent)
            {
               type = 1.00 + (genNum+1)/100.0;

               if (genNum >= 2)
                {
                    type += 0.01;
                }
            }

            foreach (var c in positions)
            {
                if (c[1] < 0)
                {
                    AddOneGraft(genNum, (int)c[0], chainNum, initGraftInd + counter, (c[1] +
                               genNum * 5.0) * Math.Pow(-1, chainNum - 1), c[2] + genNum * 70.0, type);
                }
                else
                {
                    AddOneGraft(genNum,(int)c[0], chainNum, initGraftInd + counter, (c[1] -
                               genNum * 5.0) * Math.Pow(-1, chainNum - 1), c[2] - genNum  * 70.0, type);
                }
                counter++;
            }
        }

        private List<double[]> CalcPositions(int generation, int graftAmount)
        {
            List<double[]> positions = new List<double[]>();

            var chainLength = substrateLength;

            if (generation > 0)
            {
                chainLength = genLengths[generation-1];
            }

            // For Papadakis

            //positions = new List<double[]>();
            //for (int i = 0; i < graftAmount; i++)
            //{
            //    if (graftAmount == 8)
            //    {
            //        positions.Add(new double[] { 5 + 16 * i, 90.0, 0.0 });
            //    }
            //    if (graftAmount == 10)
            //    {
            //        positions.Add(new double[] { 2 + 2 * i, 90.0, 0.0 });
            //    }
            //    if (graftAmount == 12)
            //    {
            //        positions.Add(new double[] { 6 + 10 * i, 90.0, 0.0 });
            //    }
            //    if (graftAmount == 50)
            //    {
            //        positions.Add(new double[] { 5 + 8 * i, 90.0, 0.0 });
            //    }
            //    if (graftAmount == 20)
            //    {
            //        positions.Add(new double[] { 4 + 6 * i, 90.0, 0.0 });
            //    }
            //    if (graftAmount == 100)
            //    {
            //        positions.Add(new double[] { 3 + 4 * i, 90.0, 0.0 });
            //    }
            //    if (graftAmount == 401)
            //    {
            //        positions.Add(new double[] { 1 + 1 * i, 90.0, 0.0 });
            //    }
            //}
            //return positions;



            // For Robin
            //if (chainLength >= 39 && chainLength <= 45)
            //{
            //    positions = new List<double[]> { new double[] { 8, 90.0, 0.0 },
            //                               new double[] { 16, 90.0, 0.0 },
            //                               new double[] { 25, 90.0, 0.0 },
            //                                new double[] { 33, 90.0, 0.0 } };

            //    if (chainLength == 39)
            //    {
            //        positions[0][0] = 8;
            //        positions[1][0] = 16;
            //        positions[2][0] = 24;
            //        positions[3][0] = 32;
            //    }

            //    if (chainLength == 41)
            //    {
            //        positions[0][0] = 9;
            //        positions[1][0] = 17;
            //        positions[2][0] = 25;
            //        positions[3][0] = 33;
            //    }
            //    if (chainLength == 42)
            //    {
            //        positions[0][0] = 10;
            //        positions[1][0] = 18;
            //        positions[2][0] = 26;
            //        positions[3][0] = 34;
            //    }

            //    if (chainLength == 44)
            //    {
            //        positions[0][0] = 9;
            //        positions[1][0] = 18;
            //        positions[2][0] = 27;
            //        positions[3][0] = 36;
            //    }

            //    return positions;
            //}

            //if (chainLength > 45 && chainLength <= 55)
            //{
            //    positions = new List<double[]> { new double[] { 8, 90.0, 0.0 },
            //                               new double[] { 16, 90.0, 0.0 },
            //                               new double[] { 24, 90.0, 0.0 },
            //                                new double[] { 31, 90.0, 0.0 },
            //                                new double[] { 39, 90.0, 0.0 }};

            //if (chainLength == 46)
            //{
            //    positions[0][0] = 8;
            //    positions[1][0] = 16;
            //    positions[2][0] = 24;
            //    positions[3][0] = 32;
            //}

            //    if (chainLength == 50)
            //    {
            //        positions[0][0] = 9;
            //        positions[1][0] = 17;
            //        positions[2][0] = 26;
            //        positions[3][0] = 34;
            //        positions[4][0] = 43;
            //    }


            //    return positions;
            //}


            //if (chainLength == 28)
            //{
            //    positions.Add(new double[] { 8, 90.0, 0.0 });
            //    positions.Add(new double[] { 15, 90.0, 0.0 });
            //    positions.Add(new double[] { 22, 90.0, 0.0 });

            //    return positions;
            //}

            //if (chainLength >= 18 || chainLength <= 21)
            //{
            //    if (graftAmount == 2)
            //    {
            //        positions = new List<double[]> { new double[] { 7, 90.0, 0.0 },
            //                               new double[] { 14, 90.0, 0.0 } };

            //        if (chainLength == 18)
            //        {
            //            positions[1][0] = 13;
            //        }

            //        if (chainLength == 21)
            //        {
            //            positions[1][0] = 15;
            //        }
            //    }
            //    if (graftAmount == 4)
            //    {
            //        positions = new List<double[]> { new double[] { 4, 90.0, 0.0 },
            //                               new double[] { 8, 90.0, 0.0 },
            //                               new double[] { 13, 90.0, 0.0 },
            //                               new double[] { 17, 90.0, 0.0 } };
            //        if (chainLength == 21)
            //        {
            //            positions[1][0] = 9;
            //            positions[3][0] = 18;
            //        }
            //    }

            //    return positions;
            //}

            //if (chainLength == 60 || chainLength == 61)
            //{

            //    if (graftAmount == 4)
            //    {
            //        positions = new List<double[]> { new double[] { 12, 90.0, 0.0 },
            //                                     new double[] { 24, 90.0, 0.0 },
            //                                     new double[] { 37, 90.0, 0.0 },
            //                                     new double[] { 49, 90.0, 0.0 }};
            //    }

            //    if (graftAmount == 6)
            //    {

            //        positions = new List<double[]> { new double[] { 8, 90.0, 0.0 },
            //                                     new double[] { 17, 90.0, 0.0 },
            //                                     new double[] { 26, 90.0, 0.0 },
            //                                     new double[] { 35, 90.0, 0.0 },
            //                                     new double[] { 44, 90.0, 0.0 },
            //                                     new double[] { 53, 90.0, 0.0 }};
            //    }

            //    return positions;
            //}

            //if (chainLength == 80)
            //{
            //    if (graftAmount == 4)
            //    {
            //        positions = new List<double[]> { new double[] { 16, 90.0, 0.0 },
            //                                     new double[] { 32, 90.0, 0.0 },
            //                                     new double[] { 49, 90.0, 0.0 },
            //                                     new double[] { 65, 90.0, 0.0 }};
            //    }

            //    if (graftAmount == 8)
            //    {
            //        positions = new List<double[]> { new double[] { 9, 90.0, 0.0 },
            //                                     new double[] { 18, 90.0, 0.0 },
            //                                     new double[] { 27, 90.0, 0.0 },
            //                                     new double[] { 36, 90.0, 0.0 },
            //                                     new double[] { 45, 90.0, 0.0 },
            //                                     new double[] { 54, 90.0, 0.0 },
            //                                     new double[] { 63, 90.0, 0.0 },
            //                                     new double[] { 72, 90.0, 0.0 }};
            //    }

            //    return positions;

            //}

            //if (chainLength == 200)
            //{
            //    positions.Add(new double[] { 10, 90.0, 0.0 });
            //    positions.Add(new double[] { 19, 90.0, 0.0 });
            //    positions.Add(new double[] { 29, 90.0, 0.0 });
            //    positions.Add(new double[] { 38, 90.0, 0.0 });
            //    positions.Add(new double[] { 48, 90.0, 0.0 });
            //    positions.Add(new double[] { 57, 90.0, 0.0 });
            //    positions.Add(new double[] { 67, 90.0, 0.0 });
            //    positions.Add(new double[] { 76, 90.0, 0.0 });
            //    positions.Add(new double[] { 86, 90.0, 0.0 });
            //    positions.Add(new double[] { 95, 90.0, 0.0 });
            //    positions.Add(new double[] { 105, 90.0, 0.0 });
            //    positions.Add(new double[] { 114, 90.0, 0.0 });
            //    positions.Add(new double[] { 124, 90.0, 0.0 });
            //    positions.Add(new double[] { 133, 90.0, 0.0 });
            //    positions.Add(new double[] { 143, 90.0, 0.0 });
            //    positions.Add(new double[] { 152, 90.0, 0.0 });
            //    positions.Add(new double[] { 162, 90.0, 0.0 });
            //    positions.Add(new double[] { 171, 90.0, 0.0 });
            //    positions.Add(new double[] { 181, 90.0, 0.0 });
            //    positions.Add(new double[] { 190, 90.0, 0.0 });

            //    return positions;
            //}

            //if (chainLength == 240)
            //{
            //    positions.Add(new double[] { 10, 90.0, 0.0 });
            //    positions.Add(new double[] { 20, 90.0, 0.0 });
            //    positions.Add(new double[] { 30, 90.0, 0.0 });
            //    positions.Add(new double[] { 39, 90.0, 0.0 });
            //    positions.Add(new double[] { 49, 90.0, 0.0 });
            //    positions.Add(new double[] { 58, 90.0, 0.0 });
            //    positions.Add(new double[] { 68, 90.0, 0.0 });
            //    positions.Add(new double[] { 78, 90.0, 0.0 });
            //    positions.Add(new double[] { 87, 90.0, 0.0 });
            //    positions.Add(new double[] { 97, 90.0, 0.0 });
            //    positions.Add(new double[] { 106, 90.0, 0.0 });
            //    positions.Add(new double[] { 116, 90.0, 0.0 });
            //    positions.Add(new double[] { 126, 90.0, 0.0 });
            //    positions.Add(new double[] { 135, 90.0, 0.0 });
            //    positions.Add(new double[] { 145, 90.0, 0.0 });
            //    positions.Add(new double[] { 154, 90.0, 0.0 });
            //    positions.Add(new double[] { 164, 90.0, 0.0 });
            //    positions.Add(new double[] { 174, 90.0, 0.0 });
            //    positions.Add(new double[] { 183, 90.0, 0.0 });
            //    positions.Add(new double[] { 193, 90.0, 0.0 });
            //    positions.Add(new double[] { 202, 90.0, 0.0 });
            //    positions.Add(new double[] { 212, 90.0, 0.0 });
            //    positions.Add(new double[] { 222, 90.0, 0.0 });
            //    positions.Add(new double[] { 231, 90.0, 0.0 });

            //    return positions;
            //}

            //if (chainLength == 96)
            //{

            //    if (graftAmount == 9)
            //    {
            //        positions.Add(new double[] { 9, 90.0, 0.0 });
            //        positions.Add(new double[] { 18, 90.0, 0.0 });
            //        positions.Add(new double[] { 28, 90.0, 0.0 });
            //        positions.Add(new double[] { 38, 90.0, 0.0 });
            //        positions.Add(new double[] { 48, 90.0, 0.0 });
            //        positions.Add(new double[] { 58, 90.0, 0.0 });
            //        positions.Add(new double[] { 68, 90.0, 0.0 });
            //        positions.Add(new double[] { 78, 90.0, 0.0 });
            //        positions.Add(new double[] { 88, 90.0, 0.0 });
            //    }
            //    else
            //    {
            //        positions.Add(new double[] { 8, 90.0, 0.0 });
            //        positions.Add(new double[] { 17, 90.0, 0.0 });
            //        positions.Add(new double[] { 26, 90.0, 0.0 });
            //        positions.Add(new double[] { 35, 90.0, 0.0 });
            //        positions.Add(new double[] { 44, 90.0, 0.0 });
            //        positions.Add(new double[] { 53, 90.0, 0.0 });
            //        positions.Add(new double[] { 62, 90.0, 0.0 });
            //        positions.Add(new double[] { 71, 90.0, 0.0 });
            //        positions.Add(new double[] { 80, 90.0, 0.0 });
            //        positions.Add(new double[] { 89, 90.0, 0.0 });
            //    }

            //    return positions;
            //}

            if (graftAmount >= chainLength)
            {
                var diff = 0;

                //if (generation <= 1)
                //{
                    for (int i = 0; i < chainLength; i++)
                    {
                        positions.Add(new double[] { i + 1, 90.0, 0.0 });
                    }

                    if (generation == 0 && (molecule.Count == chainLength + 1))
                    {
                        positions.Add(new double[] { chainLength + 1, 90.0, 0.0 });
                    }

                    diff = graftAmount - positions.Count;
                //}
                //else
                //{
                //    int firstLayer = graftAmount / 2;
                //    if (graftAmount%2 !=0)
                //    {
                //        firstLayer++;
                //    }

                //    for (int i = chainLength; i > (chainLength-firstLayer); i--)
                //    {
                //        positions.Add(new double[] { i, 90.0, 0.0 });
                //    }

                //    diff = graftAmount - firstLayer;
                //}

                if (diff > 0)
                {
                    graftAmount = diff;
                }
                else
                {
                    return positions;
                }
            }

            int initPos = positions.Count;

            if (graftAmount <= 7)
            {
                positions.Add(new double[] { chainLength / 2, -90.0, 0.0 });

                if (graftAmount == 1 && generation > 1)
                {
                    positions[initPos][0] = 1.0 / 2.0 * chainLength + 1;
                }

                if (graftAmount >= 2)
                {
                    positions.Add(new double[] { chainLength / 4, -90.0, 0.0 });
                    if (graftAmount == 2)
                    {
                        positions[initPos][0] = chainLength / 4 + chainLength / 2 ;
                        positions[initPos + 1][0] += 1;

                        if (generation > 0)
                        {
                            positions[initPos][0] += 1;
                            //positions[initPos + 1][0] = chainLength / 2 + 1;
                        }
                    }
                }
                if (graftAmount >= 3)
                {
                    positions.Add(new double[] { chainLength / 4 + chainLength / 2 + 1, -90.0, 0.0 });

                    if (graftAmount == 3 && generation > 0)
                    {
                        for (int i = initPos; i < positions.Count; i++)
                        {
                            positions[i][0] += 1.0;
                        }
                        //positions[initPos][0] = chainLength / 4 + chainLength / 2 + 1;
                        //positions[initPos + 1][0] = chainLength / 2 + 1;
                        //positions[initPos + 2][0] = chainLength;
                    }
                }
                if (graftAmount >= 4)
                {
                    positions.Add(new double[] { chainLength / 4, -90.0, 0.0 });

                    //if (generation > 0)
                    //{
                    //    positions[initPos + 0][0] += 2.0;
                    //    if (initPos > 0 && positions[initPos - 1][0] > positions[initPos + 3][0])
                    //    {
                    //        positions[initPos + 3][0] = positions[initPos - 1][0];
                    //    }
                    //}

                    if (graftAmount == 4)
                    {
                        if (generation > 0)
                        {
                            positions[initPos + 1][0] = positions[initPos][0]-1;
                            positions[initPos][0] += 2.0;
                            positions[initPos + 2][0] += 2.0;
                            positions[initPos + 3][0] -= 1.0;

                            //positions[initPos + 1][0] -= 1.0;
                            //positions[initPos][0] += 2.0;
                            //positions[initPos + 1][0] -= 1.0;
                            //positions[initPos + 2][0] += 1.0;
                            //positions[initPos + 3][0] += 2.0;
                        }
                    }
                }

                if (graftAmount >= 5)
                {
                    positions.Add(new double[] { chainLength / 4, -90.0, 0.0 });
                    //if (generation > 1)
                    //{
                    //    //positions[initPos][0] += 1.0;
                    //    positions[initPos + 4][0] = chainLength - 1;
                    //}
                    //else
                    //{
                    positions[initPos + 1][0] = positions[initPos][0] + 1;
                    positions[initPos][0] += 3.0;
                    positions[initPos + 2][0] += 2.0;
                    positions[initPos + 3][0] += 2.0;
                    //positions[initPos + 1][0] -= 1.0;
                    //positions[initPos + 2][0] += 1.0;
                    //positions[initPos + 3][0] += 3.0;
                    //positions[initPos + 4][0] += 1.0;
                    //}
                }

                if (generation == 0)
                {
                    for (int i = initPos; i < positions.Count; i++)
                    {
                        positions[i][0] += 1.0;
                    }
                }

                if (graftAmount >= 6)
                {
                    positions.Add(new double[] { chainLength - 1, -90.0, 0.0 });

                    //if (generation>1)
                    //{
                    //    positions[initPos + 5][0] = positions[initPos][0]-1;
                    //}
                }

                if (graftAmount == 7)
                {
                    positions.Add(new double[] { 2, -90.0, 0.0 });

                    //if (generation>1)
                    //{
                    //    positions[initPos + 5][0] = positions[initPos][0]-1;
                    //}
                }
            }
            else
            {
                double coef = chainLength / graftAmount;

                if (graftAmount > chainLength * 0.5)
                {
                    coef = (int)(1 / (1 - (double)(graftAmount) / chainLength));
                }

                int counter = 0;

                for (int j = chainLength; j >= 1; j--)
                {
                    counter++;
                    if (graftAmount > chainLength * 0.5)
                    {
                        if (counter != coef)
                        {
                            positions.Add(new double[] { j, -90.0, 0.0 });
                        }
                        else
                        {
                            counter = 0;
                        }
                    }
                    else
                    {
                        if (counter == coef)
                        {
                            positions.Add(new double[] { j, -90.0, 0.0 });
                        //}
                        //else
                        //{
                            counter = 0;
                        }
                    }
                }
            }

            return positions;
        }

        // Add one graft to one chain including the bonds
        private void AddOneGraft(int genNum, int beadNum, int chainNum, int counter, double horAngle, double vertAngle, double type)
        {
            //Linear substrate
            if (beadNum == 0)
            {
                for (int i = 0; i < substrateLength; i++)
                {
                    //if ((i + 1) % 2 != 0)
                    //{
                        molecule.Add(new MolData(type, i + 1, 1, 0.000 + Math.Round(step * i, 3), 0.000, 0.000));
                    //}
                    //else
                    //{
                    //    molecule.Add(new MolData(type, i + 1, 1, 0.000 + Math.Round(step * i, 3), 0.000 +
                    //                             Math.Round(step, 3), 0.000));
                    //}
                }

                // addBonds
                for (int i = 1; i < molecule.Count; i++)
                {
                    bonds.Add(new int[] { i, i + 1 });
                }
            }
            else
            {
                var graftSubstr = molecule.Where(x => x.MolIndex == chainNum).ToList();

                double beadXCoord = graftSubstr[beadNum - 1].XCoord;
                double beadYCoord = graftSubstr[beadNum - 1].YCoord;
                double beadZCoord = graftSubstr[beadNum - 1].ZCoord;


                // for bonds
                int graftInd = graftSubstr[beadNum - 1].Index;
                int startInd = molecule.Count;


                if (chainNum == 1)
                {
                    beadXCoord = molecule[beadNum - 1].XCoord;
                    beadYCoord = molecule[beadNum - 1].YCoord;
                    beadZCoord = molecule[beadNum - 1].ZCoord;

                    graftInd = molecule[beadNum - 1].Index;

                }
                // get the length of the graft in the current generation
                var graftLength = genLengths[genNum];

                for (int i = 1; i <= graftLength; i++)
                {
                        molecule.Add(new MolData(type, molecule.Count + 1, counter,
                                                 beadXCoord + Math.Round(i * step * Math.Cos(vertAngle * Math.PI / 180.0) *
                                                 Math.Cos(horAngle * Math.PI / 180.0), 3),
                                                 beadYCoord + Math.Round(i * step * Math.Cos(vertAngle * Math.PI / 180.0) *
                                                 Math.Sin(horAngle * Math.PI / 180.0), 3),
                                                 beadZCoord + Math.Round(i * step * Math.Sin(vertAngle * Math.PI / 180.0), 3)));

                    if (isDiblock)
                    {
                        if (genNum == 4 || genAmounts[genNum + 1] == 0)
                        {
                            if (i <= typeBlength)
                            {
                                molecule.Last().AtomType = 1.01;
                            }
                        }
                    }
                }

                // bonds
                for (int i = 0; i < graftLength; i++)
                {
                    if (i == 0)
                    {
                        bonds.Add(new int[] { graftInd, startInd + 1 });
                    }
                    else
                    {
                        bonds.Add(new int[] { startInd + i, startInd + 1 + i });
                    }
                }
            }
        }

        public static double CalcTypeBpercentage(int chainLengthA, int chainLengthLast,
                                                 int firstGen, int secondGen,
                                                 int thirdGen, int fourthGen)
        {
            double totalBeads = chainLengthA * (1 + firstGen + firstGen * secondGen +
                                 firstGen * secondGen * thirdGen * fourthGen) +
                                 chainLengthLast * firstGen * secondGen * thirdGen + 1;

            double typeBBeads = chainLengthA * firstGen * secondGen * thirdGen;

            if (fourthGen == 0)
            {
                typeBBeads = chainLengthA * firstGen * secondGen;
            }

            return typeBBeads / totalBeads;
        }

        public static double[] CalcOptimalComposition(int chainLengthA, int chainLengthB)
        {
            double[] generations = new double[5];

            double percentage = 0.0;

            for (int i = 3; i <= chainLengthA; i++)
            {
                for (int j = 1; j <= chainLengthA; j++)
                {
                    for (int k = 1; k <= chainLengthB; k++)
                    {
                        for (int t = 1; t <= chainLengthA; t++)
                        {
                            int totalChains = 1 + i + i * j + i * j * k + i * j * k * t;
                            int typeBChains = i * j * k;

                            double interPercentage = (double)typeBChains / (double)totalChains;

                            if (interPercentage > percentage)
                            {
                                percentage = interPercentage;
                                generations[0] = i;
                                generations[1] = j;
                                generations[2] = k;
                                generations[3] = t;
                                generations[4] = percentage;
                            }
                        }
                    }
                }
            }

            return generations;
        }
    }

    public class CombSynth : Synth
    {
        private int sideChainType;
        private double graftingDensity;
        private int backboneLength;
        private int sideChainLength;
        private int blockBlength;
        private bool isDoubleGraft;
        private bool isIndBB;
        private bool isRandom;
        private int chainNum;
        private double dbFrac;
        private double blockBfrac;
        //private bool isCompact;

        private double xSize;
        private double ySize;
        private double zSize;

        private int prevMolBeads = 0;
        private double xInit = 0.00;
        private double yInit = 0.00;
        private double zInit = 0.00;


        public CombSynth(int sideChainType, int graftingDensity,
                      int backboneLength,
                      int sideChainLength, bool isDoubleGraft, bool isIndBB, 
                      bool isRandom, int chainNum, double dbFrac,
                      int blockBlength, double blockBfrac, //bool isCompact,
                      double xSize, double ySize, double zSize)
        {
            this.sideChainType = sideChainType;
            this.graftingDensity = graftingDensity / 100.0;
            this.backboneLength = backboneLength;
            this.sideChainLength = sideChainLength;
            this.isDoubleGraft = isDoubleGraft;
            this.isIndBB = isIndBB;
            this.isRandom = isRandom;
            this.dbFrac = dbFrac;
            this.chainNum = chainNum;

            this.blockBlength = blockBlength;
            this.blockBfrac = blockBfrac;

           // this.isCompact = isCompact;

            this.step = 1.5 / Math.Pow(3.0, 1.0 / 3.0);

            this.xSize = xSize;
            this.ySize = ySize;
            this.zSize = zSize;


            graftBonds = new List<int[]>();
            bonds = new List<int[]>();
            molecule = new List<MolData>();
        }

        public void CreateMolecule()
        {
            // Matyjaszewski case
            if (sideChainType == 3)
            {
                if (!isIndBB)
                {
                    sideChainLength--;
                    blockBlength--;

                }
            }
            var rand = new Random();
            
            for (int i = 0; i < chainNum; i++)
            {
                if (i > 0)
                {
                    prevMolBeads = molecule.Count; // the number of beads within each molecule is the same. needed for convinience
                    xInit = rand.Next((int)(0.15 * xSize), (int)(0.85 * xSize))- xSize/2.0;
                    yInit = rand.Next((int)(0.15 * ySize), (int)(0.85 * ySize)) - ySize / 2.0;
                    zInit = rand.Next((int)(0.15 * zSize), (int)(0.85 * zSize)) - zSize / 2.0;
                }
              
                addBackBone();

                if (sideChainLength > 0)
                {
                    addSideChains();
                }
            }
        }

        private void addBackBone()
        {
            for (int i = 0; i < backboneLength; i++)
            {
                molecule.Add(new MolData(1.04, molecule.Count + 1, 1, xInit + i, yInit, zInit));

                if (i > 0)
                {
                    // add BackBone Bonds
                    bonds.Add(new int[] { molecule.Count-1, molecule.Count });
                }
            }

            if (!isIndBB)
            {
                if (isRandom)
                {
                    var rand = new Random();

                    int beadBCount = (int)(blockBfrac * backboneLength);

                    int counter = 0;

                    for (int i = 0; i < backboneLength; i++)
                    {
                        molecule[molecule.Count - backboneLength + i].AtomType = 1.00;
                    }

                    do
                    {
                        int num = rand.Next(1, backboneLength);

                        if (molecule[molecule.Count - 1 - backboneLength + num].AtomType == 1.00)
                        {
                            molecule[molecule.Count - 1 - backboneLength + num].AtomType = 1.01;
                            counter++;
                        }
                    } while (counter < beadBCount);

                }
                else
                {
                    for (int i = 0; i < backboneLength; i++)
                    {
                        if ((i + 1) % 2 != 0)
                        {
                            molecule[molecule.Count - backboneLength + i].AtomType = 1.00;
                        }
                        else
                        {
                            molecule[molecule.Count - backboneLength + i].AtomType = 1.00;

                            if (sideChainType == 3)
                            {

                                molecule[molecule.Count - backboneLength + i].AtomType = 1.01;
                            }
                        }
                    }
                }
            }
            //else
            //{
                //if (!isCompact)
                //{
                    
                //}
             // in case of Compact structure the whole backbone will be folded into the square spiral:
            // l is the step (eq.1) t is the spiral step (eq. 1)
            //else
            //{
                    //    double centerCoord = 50.0; //
                    //    double spirstep = 1.0;

                    //    // 1st bead
                    //    molecule.Add(new MolData(1.04, 1, 0.0, 0.0, centerCoord));
                    //    // 2nd bead
                    //    molecule.Add(new MolData(1.04, 2, 0.0, 0.0, centerCoord + spirstep));
                    //    // 3rd bead
                    //    for (int i = 0; i <= 1; i++)
                    //    {
                    //        molecule.Add(new MolData(1.04, 3, -spirstep, 0.0, centerCoord + spirstep));
                    //    }

                    //    int arm = 2;
                    //    int direction = 3; // 1 - up, 2 - left, 3 - down, 4 - right

                    //    do
                    //    {
                    //        for (int k = 0; k <= 1; k++)
                    //        {
                    //            var last = molecule.Last();

                    //            for (int i = 1; i <= arm; i++)
                    //            {
                    //                double newYcoord = (molecule.Count) * 0.00;

                    //                if (direction == 1)
                    //                {
                    //                    molecule.Add(new MolData(1.04, molecule.Count + 1, last.XCoord, newYcoord, last.ZCoord + spirstep * i));
                    //                }
                    //                else if (direction == 2)
                    //                {
                    //                    molecule.Add(new MolData(1.04, molecule.Count + 1, last.XCoord - spirstep * i, newYcoord, last.ZCoord));
                    //                }
                    //                else if (direction == 3)
                    //                {
                    //                    molecule.Add(new MolData(1.04, molecule.Count + 1, last.XCoord, newYcoord, last.ZCoord - spirstep * i));
                    //                }
                    //                else
                    //                {
                    //                    molecule.Add(new MolData(1.04, molecule.Count + 1, last.XCoord + spirstep * i, newYcoord, last.ZCoord));
                    //                }

                    //                if (molecule.Count == backboneLength)
                    //                {
                    //                    break;
                    //                }
                    //            }
                    //            if (molecule.Count == backboneLength)
                    //            {
                    //                break;
                    //            }

                    //            direction++;
                    //        }
                    //        arm++;

                    //        if (direction > 4)
                    //        { direction = 1; }
                    //    }

                    //    while (molecule.Count < backboneLength);
                //    }
                //}
 
        

        }

        private void addSideChains()
        {
            int sideChainsAmount = (int)(graftingDensity * backboneLength);

            int coef = (backboneLength - 2) / (sideChainsAmount - 2); // space between grafts

            int iter = 1;

            if (isDoubleGraft) { iter++; }

            for (int k = 1; k <= iter; k++)
            {
                int sChain = Math.Max(sideChainLength, blockBlength);

                for (int j = 0; j < sideChainsAmount; j++)
                {

                    int index = coef * j;
                    int sChainInd = molecule.Count + 1;

                    double beadXCoord = molecule[prevMolBeads + coef * j].XCoord;
                    double beadYCoord = molecule[prevMolBeads + coef * j].YCoord;
                    double beadZCoord = molecule[prevMolBeads + coef * j].ZCoord;
                    double type = 1.01;


                    if (!isIndBB)
                    {
                        type = molecule[prevMolBeads  + coef * j].AtomType;
                    }
                    else
                    {

                        if (blockBfrac < 0.5)
                        {
                            type = 1.0;
                        }

                        if (sideChainType == 3)
                        {
                            if (blockBfrac < 0.5)
                            {
                                int space = (int)(1.0 / blockBfrac);

                                if ((j + 1) % space == 0) { type = 1.01; }
                            }
                            else
                            {
                                int space = (int)(1.0 / (1.0 - blockBfrac));

                                if ((j + 1) % space == 0) { type = 1.00; }
                            }

                        }
                    }

                    for (int i = 1; i <= sChain; i++)
                    {
                        double xCoord = beadXCoord - 1.0 / Math.Sqrt(3) * Math.Round(step, 3);
                        double yCoord = beadYCoord - Math.Round(step * i, 3) * Math.Pow(-1.0 * k, j + 1);

                        if (i > 1)
                        {
                            xCoord = beadXCoord;
                            bonds.Add(new int[] { molecule.Count, molecule.Count + 1 });
                        }
                        else
                        {
                            // add SideChain Bonds
                            bonds.Add(new int[] { prevMolBeads * (chainNum - 1) + coef * j + 1, sChainInd });
                        }

                        molecule.Add(new MolData(type, molecule.Count + 1, k + 1, xCoord, yCoord, beadZCoord));

                        if (sideChainType == 3)
                        {
                                if (sChain > sideChainLength)
                                {
                                    if (type == 1.0 && i == sideChainLength)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    if (type == 1.01 && i == blockBlength)
                                    {
                                        break;
                                    }
                                }
                        }
                    }


                    // diblock
                }
            }
        }
    }

    public class FuncSynth
    {
        private bool boundExist;
        private bool funcWithChains;
        private int funcChainLength;
        private double subsType;
        private double funcType;
        private double funcFrac;
        private List<double[]> initMol;
        private List<int[]> initBonds;
        public List<int[]> finalBonds = new List<int[]>();

        public FuncSynth(bool boundExist, bool funcWithChains, int funcChainLength,
                         double subsType, double funcType, double funcFrac, 
                         List<double[]> initMol, List<int[]> initBonds)
        {
            this.boundExist = boundExist;
            this.funcWithChains = funcWithChains;
            this.funcChainLength = funcChainLength;
            this.subsType = subsType;
            this.funcType = funcType;
            this.funcFrac = funcFrac;
            this.initMol = initMol;
            this.initBonds = initBonds;
        }

        public List<MolData> FunctionalizeMolecule()
        {
            var finalMol = new List<MolData>();

           
            // We only functionalize the linear segments. 
            // If the amount of functionalized beads are > 1 then this value must be increased
            int maxBonds = 2;

            foreach (var c in initMol)
            {
                finalMol.Add(new MolData(c[3], finalMol.Count + 1, 1, c[0], c[1], c[2]));
            }

            foreach (var c in initBonds)
            {
                finalBonds.Add(new int[] { c[0], c[1] });
            }

            // Add all that in initMol
            var substrate = finalMol.Where(x => x.AtomType.Equals(subsType)).ToList();

            if (boundExist)
            {
                var bounding = finalMol.Where(x => x.AtomType.Equals(funcType)).ToList();

                foreach (var c in substrate)
                {
                    var neighbors = bounding.Where(x => Methods.GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                                       x.XCoord, x.YCoord, x.ZCoord) <= 1.0).OrderBy(x =>
                                                       Methods.GetDistance3D(0.0, 0.0, c.ZCoord,
                                                       0.0, 0.0, x.ZCoord)).ToList();
                    if (neighbors.Count > 0)
                    {
                            finalBonds.Add(new int[] { c.Index, neighbors[0].Index });
                            bounding.Remove(neighbors[0]);
                    }
                }
            }
            else
            {
                var rand = new Random();

                if (funcFrac >= 1)
                {
                    var fulFunc = (int)funcFrac;
                    funcFrac -= fulFunc;

                    maxBonds += fulFunc; // increasing the limit of grafting

                    // cycle for the integer number of functionalized beads (in case if every beads needs multiple bindings)
                    for (int k = 0; k < fulFunc; k++)
                    {
                        foreach (var c in substrate)
                        {
                            var xCoord = c.XCoord + (double)rand.Next(-50, 50) / 100.0;
                            var yCoord = c.YCoord + (double)rand.Next(-50, 50) / 100.0;
                            var zCoord = c.ZCoord + (double)rand.Next(-50, 50) / 100.0;

                            finalMol.Add(new MolData(funcType, finalMol.Count + 1, xCoord, yCoord, zCoord));
                            finalBonds.Add(new int[] { c.Index, finalMol.Count });

                            // grafting chains
                            if (funcWithChains)
                            {
                                for (int j =0; j < funcChainLength-1; j++)
                                {
                                    xCoord = finalMol[finalMol.Count - 1].XCoord + (double)rand.Next(-50, 50) / 100.0;
                                    yCoord = finalMol[finalMol.Count - 1].YCoord + (double)rand.Next(-50, 50) / 100.0;
                                    zCoord = finalMol[finalMol.Count - 1].ZCoord + (double)rand.Next(-50, 50) / 100.0;

                                    finalMol.Add(new MolData(funcType, finalMol.Count + 1, xCoord, yCoord, zCoord));
                                    finalBonds.Add(new int[] { finalMol.Count - 1, finalMol.Count });
                               }
                            }
                        }
                    }
                }

                if (funcFrac > 0)
                {
                    int subscount = (int)(substrate.Count * funcFrac);

                    var accountedList = new List<int>();

                    int counter = 0;

                    do
                    {
                        int num = rand.Next(0, substrate.Count);

                        int currBonds = finalBonds.Where(x => x[0] == substrate[num].Index ||
                                                         x[1] == substrate[num].Index).ToList().Count;

                        if (currBonds <= maxBonds)
                        {
                            var xCoord = substrate[num].XCoord + (double)rand.Next(-50, 50) / 100.0;
                            var yCoord = substrate[num].YCoord + (double)rand.Next(-50, 50) / 100.0;
                            var zCoord = substrate[num].ZCoord + (double)rand.Next(-50, 50) / 100.0;

                            finalMol.Add(new MolData(funcType, finalMol.Count + 1, xCoord, yCoord, zCoord));
                            finalBonds.Add(new int[] { substrate[num].Index, finalMol.Count });

                            // grafting chains
                            if (funcWithChains)
                            {
                                for (int j = 0; j < funcChainLength - 1; j++)
                                {
                                    xCoord = finalMol[finalMol.Count - 1].XCoord + (double)rand.Next(-50, 50) / 100.0;
                                    yCoord = finalMol[finalMol.Count - 1].YCoord + (double)rand.Next(-50, 50) / 100.0;
                                    zCoord = finalMol[finalMol.Count - 1].ZCoord + (double)rand.Next(-50, 50) / 100.0;

                                    finalMol.Add(new MolData(funcType, finalMol.Count + 1, xCoord, yCoord, zCoord));
                                    finalBonds.Add(new int[] { finalMol.Count - 1, finalMol.Count });
                                }
                            }

                            counter++;
                        }

                    } while (counter < subscount);
                }

            }

            return finalMol;
        }
    }

}
