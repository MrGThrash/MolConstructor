

using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Accord.Math.Decompositions;

namespace MolConstructor
{
    public class StructFormer
    {
        private bool notFit;
        private bool isHomoPol;
        private bool hasWalls;
        private bool autoFill;
        private bool separatedSolvs;
        private bool crossEnabled;
        private bool isFilm;
        private int location;
        private double density;
        private int wallsType;
        private double xSize;
        private double ySize;
        private double zSize;
        private int maxNum;
        private int molNumber;
        private int molInd;
        private double step;
        private double percentage;
        private double shift;
        private double[] centerPoint;
        private List<double[]> molecule;
        private List<double[]> secondMolecule;

        public StructFormer(bool _isHomoPol, bool _hasWalls, bool _autoFill, bool _separatedSolvs, bool _crossEnabled, int _location, double _density, int _wallsType, double _xSize, double _ySize, double _zSize, int _molNumber, double _percentage, double _shift, List<double[]> _molecule)
        {
            isHomoPol = _isHomoPol;
            hasWalls = _hasWalls;
            autoFill = _autoFill;
            separatedSolvs = _separatedSolvs;
            crossEnabled = _crossEnabled;
            location = _location;
            density = _density;
            wallsType = _wallsType;
            xSize = _xSize;
            ySize = _ySize;
            zSize = _zSize;
            maxNum = (int)(xSize * ySize * zSize * density);
            molNumber = _molNumber;
            molInd = 0;
            step = 1.0 / Math.Pow(density, 1.0 / 3.0);
            if (density < 1)
            {
                step = 1.0;
            }
            percentage = _percentage;
            shift = _shift;
            molecule = _molecule;
            centerPoint = new double[3];
            if (percentage != 0.0)
                return;
        }

        public StructFormer(bool _isFilm, bool _hasWalls, int _wallsType, double _xSize, double _ySize, double _zSize, double _density, double _percentage, double[] _centerPoint, List<double[]> data)
        {
            xSize = _xSize;
            ySize = _ySize;
            zSize = _zSize;
            density = _density;
            maxNum = (int)(xSize * ySize * zSize * density);
            step = 1.0 / Math.Pow(density, 1.0 / 3.0);
            if (density < 1)
            {
                step = 1.0;
            }
            molecule = data;
            isFilm = _isFilm;
            hasWalls = _hasWalls;
            wallsType = _wallsType;
            percentage = _percentage;
            if (_centerPoint != null)
                centerPoint = _centerPoint;
            else
                centerPoint = new double[3];
        }

        public StructFormer(bool _hasWalls, int _wallsType, double _xSize, double _ySize, double _zSize, double _density, double[] _centerPoint, List<double[]> data)
        {
            xSize = _xSize;
            ySize = _ySize;
            zSize = _zSize;
            density = _density;
            maxNum = (int)(xSize * ySize * zSize * density);
            step = 1.0 / Math.Pow(density, 1.0 / 3.0);
            if (density < 1)
            {
                step = 1.0;
            }
            molecule = data;
            hasWalls = _hasWalls;
            wallsType = _wallsType;
            if (_centerPoint != null)
                centerPoint = _centerPoint;
            else
                centerPoint = new double[3];
        }

        public List<MolData> ComposeStructure(bool doubleSurf)
        {
            var system = new List<MolData>();

            double interCord = this.findInterCord();
            if ((uint)molNumber > 0U)
            {
                double xDiam = Methods.GetAxDiameter(molecule, 0);
                double yDiam = Methods.GetAxDiameter(molecule, 1);
                double zDiam = Methods.GetAxDiameter(molecule, 2);

                if (molNumber > 1)
                {
                    int counter = 0;
                    double radius = Math.Max(xDiam, yDiam) * 0.8;

                    if (molNumber >= 15) { radius *= 0.5; }
                    if (molNumber >= 100) { radius *= 0.2; }
                    if (crossEnabled) { radius = 0.0; }

                    Timer timer = new Timer();
                    timer.Interval = 90000.0;
                    timer.Elapsed += new ElapsedEventHandler(reportIsFull);
                    timer.Start();
                    Random random = new Random();

                    //Placing molecules
                    do
                    {
                        bool collide = false;
                        double xCoord = random.Next(Math.Min((int)xDiam, (int)(xSize - xDiam / 2.0)), Math.Max((int)xDiam, (int)(xSize - xDiam / 2.0)));
                        double yCoord = random.Next(Math.Min((int)yDiam, (int)(ySize - yDiam / 2.0)), Math.Max((int)yDiam, (int)(ySize - yDiam / 2.0)));

                        var interMol = moveMolecule(molecule, xCoord - xSize / 2.0 - xDiam / 2.0, yCoord - ySize / 2.0 - yDiam / 2.0, interCord - zDiam / 2.0 + shift);

                        #region разделение молекул, чтобы сдвинуть в разные стороны
                        /*

                        //divide: half to +shift, half to -shift
                        List<double[]> interMol = new List<double[]>();
                        if (counter < molNumber /2)
                        {
                            interMol = moveMolecule(molecule, xCoord - xSize / 2.0 - xDiam / 2.0, yCoord - ySize / 2.0 - yDiam / 2.0, interCord - zDiam / 2.0 + shift);
                        }

                        if (counter >= molNumber/2)
                        {
                            interMol = moveMolecule(molecule, xCoord - xSize / 2.0 - xDiam / 2.0, yCoord - ySize / 2.0 - yDiam / 2.0, interCord - zDiam / 2.0 - shift);
                        }
                        */
                        #endregion

                        foreach (var c in system)
                        {
                            if (Methods.GetDistance(xCoord - xSize / 2.0, yCoord - ySize / 2.0, 0.0, c.XCoord, c.YCoord, 0.0) <= radius)
                            {
                                collide = true;
                                break;
                            }
                        }
                        if (!collide)
                        {
                            molInd++;
                            foreach (var c in interMol)
                            {
                                system.Add(new MolData(c[3], system.Count + 1, molInd, c[0], c[1], c[2]));
                            }

                            if (doubleSurf)
                            {
                                var secondInter = moveMolecule(molecule, xCoord - xSize / 2.0 - xDiam / 2.0, yCoord - ySize / 2.0 - yDiam / 2.0, zSize / 2.0 - zDiam / 2.0 + shift);
                                molInd++;

                                foreach (var c in secondInter)
                                {
                                    system.Add(new MolData(c[3], system.Count + 1, c[0], c[1], c[2]));
                                }
                            }
                            counter++;
                        }
                    }
                    while (counter < molNumber); 
                }
                else
                {
                    var interMol = moveMolecule(molecule, -xDiam / 2.0, -yDiam / 2.0, interCord - zDiam / 2.0 + shift);
                    if ((uint)molNumber > 0U)
                    {
                        foreach (var c in interMol)
                            system.Add(new MolData(c[3], system.Count + 1, 1, c[0], c[1], c[2]));
                    }
                }
                if (isHomoPol)
                {
                    foreach (var c in system)
                    {
                        if (c.AtomType == 1.01)
                            c.AtomType = 1.0;
                    }
                }
            }
            checkBorders(system);

            addSolvents(false, system);

            if (hasWalls && wallsType == 1)
            {
                AddWalls(system);
            }

            return system;
        }

        public List<MolData> ComposeRandomStructure(bool twoOils, List<int[]> bonds)
        {
            List<MolData> system = new List<MolData>();
            double collidingCoef = 0.75;
            if (crossEnabled)
            {
                collidingCoef = 0.0;
            }

            if ((uint)molNumber > 0U)
                placeMoleculesRandomly(molecule, bonds, collidingCoef, system);

            addSolvents(twoOils, system);

            if (hasWalls && wallsType == 1)
            {
                AddWalls(system);
            }

            return system;
        }

        public List<MolData> ComposeDoubleMixture(int matrixChainLength, double matrixType, double polyPerc, bool isDiblock, int blockALength)
        {
            List<MolData> system = new List<MolData>();
            double[] diameter = Methods.GetDiameter(molecule);

            double collidingCoef = 0.8;

            if (diameter[0] / xSize > 0.3 || diameter[1] / ySize > 0.3 || diameter[2] / zSize > 0.3)
            {
                collidingCoef = 0.3;
            }
            if (crossEnabled || (diameter[0] / xSize > 0.5 || diameter[1] / ySize > 0.5 || diameter[2] / zSize > 0.5))
            {
                collidingCoef = 0.0;
            }

            placeMoleculesRandomly(molecule, null, collidingCoef, system);
            addMatrixAndSolvents(matrixChainLength, matrixType, polyPerc, isDiblock, blockALength, system);

            if (hasWalls && wallsType == 1)
            {
                AddWalls(system);
            }

            return system;
        }

        public void PrepareCatalysysSystem(double substrPerc, double catalPerc, List<MolData> syst)
        {
            List<MolData> waterPhase = syst.Where((x => x.AtomType.Equals(1.03))).ToList();
            List<MolData> oilPhase = syst.Where((x => x.AtomType.Equals(1.02))).ToList();

            int catAnum = (int)(waterPhase.Count * catalPerc);
            int catBnum = (int)(oilPhase.Count * substrPerc);
            Random random = new Random();

            int catAcount = 0;
            int catBount = 0;
            do
            {
                int index = random.Next(0, waterPhase.Count - 1);
                if (waterPhase[index].AtomType.Equals(1.03))
                {
                    waterPhase[index].AtomType = 1.05;
                    catAcount++;
                }
            }
            while (catAcount < catAnum);
            do
            {
                int index = random.Next(0, oilPhase.Count - 1);
                if (oilPhase[index].AtomType.Equals(1.02))
                {
                    oilPhase[index].AtomType = 1.06;
                    catBount++;
                }
            }
            while (catBount < catBnum);
        }

        public List<MolData> ComposeFilm(out List<int[]> totalBonds, out List<int[]> totalAngles, bool areChainsUnMixed, bool twoSolvs, bool complexOne, double solvTwoPerc, int polLocation, 
                                          double polyPerc, double molTwoPerc, List<int[]> bondsOne, List<int[]> anglesOne, List<double[]> molTwo, List<int[]> bondsTwo, List<int[]> anglesTwo)
        {
            var system = new List<MolData>();

            var compComs = new List<double[]>();

            int polBeads = (int)(maxNum * polyPerc);

            var wallbeads = 0;
            if (hasWalls && wallsType == 1)
            {
                AddWalls(system);

                wallbeads = system.Count;
                system.Clear();
            }


            int mainChainAmount = (int)(polBeads * (1.0 - molTwoPerc) / molecule.Count);
            if (molTwo.Count == 0 && polyPerc.Equals(1.0))
            {
                mainChainAmount++;
            }
            int secondChainAmount = 0;
            if (molTwo.Count > 0)
            {
                secondChainAmount = (polBeads - mainChainAmount * molecule.Count) / molTwo.Count;
            }
            double height = zSize * polyPerc;
            if (!isFilm)
            {
                height = zSize;
            }

            double[] zBorders = new double[] { 2.0 * step, height };

            if (areChainsUnMixed)
            {
                double[] zBordersOne = new double[] { zBorders[0], zBorders[1] };
                double[] zBordersTwo = new double[] { zBorders[0], zBorders[1] };

                int poLocRev = Math.Abs(polLocation - 1);

                zBordersOne[poLocRev] = zSize * polyPerc * (1.0 - molTwoPerc);
                zBordersTwo[polLocation] = zBordersOne[poLocRev];

                
                addFilmMolecules(mainChainAmount, zBordersOne, molecule, bondsOne, system);
                addFilmMolecules(secondChainAmount, zBordersTwo, molTwo, bondsTwo, system);
            }
            else if (!complexOne)
            {
                addFilmMolecules(mainChainAmount, zBorders, molecule, bondsOne, system);
                addFilmMolecules(secondChainAmount, zBorders, molTwo, bondsTwo, system);
            }
            else
            {
                addFilmMolecules(mainChainAmount, zBorders, molecule, bondsOne, system);
                if ((uint)compComs.Count > 0U)
                {
                    addLinearMoleculesWithObstacles(secondChainAmount, height, zBorders, molTwo, system, compComs);
                }
                else
                {
                    addFilmMolecules(secondChainAmount, zBorders, molTwo, bondsTwo, system);
                }
            }
            system = system.OrderBy(x => x.Index).ToList();

            totalBonds = MolData.MultiplyBonds(mainChainAmount, bondsOne);
            totalAngles = MolData.MultiplyAngles(mainChainAmount, anglesOne);
            if (molTwoPerc != 0.0)
            {
                var interBonds = MolData.MultiplyBonds(secondChainAmount, bondsTwo);
                var interAngles = MolData.MultiplyAngles(secondChainAmount, anglesTwo);
                int mainBeads = mainChainAmount * molecule.Count;
                foreach (var c in interBonds)
                {
                    totalBonds.Add(new int[] { c[0] + mainBeads, c[1] + mainBeads });
                }

                foreach (var c in interAngles)
                {
                    totalAngles.Add(new int[] { c[0] + mainBeads, c[1] + mainBeads, c[2] + mainBeads });
                }
            }

            if (polyPerc.Equals(1.0))
            {
                int residPol = this.maxNum - system.Count - wallbeads;
                if (residPol > 0)
                {           
                        for (int i = 0; i < residPol; i++)
                    {
                        if (residPol >= molTwo.Count)
                        {
                            if (i < residPol - 1)
                            {
                                totalBonds.Add(new int[] { system.Count + 1, system.Count + 2 });
                            }

                            if (totalAngles.Count != 0 && i < (residPol - 1))
                            {
                                totalAngles.Add(new int[] { system.Count + 1, system.Count + 2, system.Count + 3 });
                            }
                        }
                        system.Add(new MolData(1.0, system.Count + 1, 0.0, 0.0, 0.0));
                    }
                }
                else if ((uint)residPol > 0U)
                {
                    int excess = system.Count - maxNum;

                    for (int i = maxNum + excess - 1; i > maxNum - 1-wallbeads; i--)
                    {
                        system.RemoveAt(i);
                    }

                    totalBonds.RemoveAll(x => x[0] >= maxNum -wallbeads || x[1] > maxNum);
                    totalAngles.RemoveAll(x => x[1] >=  maxNum -wallbeads || x[2] > maxNum);
                }
            }
            else
            {
                addSolvents(false, system);
                if (twoSolvs)
                {
                    int solvCount = (int)(system.Where(x => x.AtomType == 1.02).ToList().Count * solvTwoPerc);
                    int solvTwoCount = 0;
                    foreach (MolData molData in system)
                    {
                        if (molData.AtomType == 1.02)
                        {
                            molData.AtomType = 1.07;
                            solvTwoCount++;
                        }
                        if (solvTwoCount == solvCount)
                        {
                            break;
                        }
                    }
                }
            }


            checkBorders(system);
            
            if (hasWalls && wallsType == 1)
            {
                AddWalls(system);
            }
            return system;
        }
        public List<MolData> ComposePlanBrush(out List<int[]> totalBonds, out List<int[]> totalAngles, bool twoSolvs, double solvTwoPerc, double ankerType, double graftDensity, int microgelNum, double microgelPosition,
                                              List<int[]> brushBonds, List<int[]> brushAngles, List<double[]> microgel, List<int[]> microgelBonds, List<int[]> microgelAngles)
        {
            var system = new List<MolData>();

            var compComs = new List<double[]>();

            int brushNum = (int)(graftDensity * Math.Pow(density, 2.0 / 3.0) * xSize * ySize);

            double[] zBorders = new double[] { 2.0 * step, zSize - 2.0 * step };

            // AddBrushLayer
            var rnd = new Random();
            var graft = MolData.ConvertToMolData(molecule, true, brushBonds);

            int counter = 0;
            int molIndex = 1;

            do
            {
                // ankerBead
                double xCoord = rnd.Next(0, (int)(xSize * 100)) / 100.0;
                double yCoord = rnd.Next(0, (int)(ySize * 100)) / 100.0; ;
                double zCoord = 1.0 * step;

                system.Add(new MolData(ankerType, system.Count + 1, molIndex, xCoord - xSize / 2.0, yCoord - ySize / 2.0, zCoord - zSize / 2.0));

                var currBead = graft.First(x => x.AtomType == ankerType);

                var placedBeads = new List<int>();

                Methods.AddNonLinMol_Recurcion(molIndex, "Z", rnd, placedBeads, currBead, graft, system);
                counter++;
                molIndex++;

            } while (counter < brushNum);
          
            // CheckBorders after brush is added
            // checkPlainBorders(system);

            system = system.OrderBy(x => x.Index).ToList();

            totalBonds = MolData.MultiplyBonds(brushNum, brushBonds);
            totalAngles = MolData.MultiplyAngles(brushNum, brushAngles);

            // for solvents before we add the MG/NPs
            double filmHeight = system.Max((x => x.ZCoord)) + zSize / 2.0;

            if ((uint)microgelNum > 0U)
            {
   
                double[] diameter = Methods.GetDiameter(microgel);

                if (microgelNum == 1)
                {
                    var mgel = moveMolecule(microgel, centerPoint[0] - diameter[0] / 2.0, centerPoint[1] - diameter[1] / 2.0,
                                            microgelPosition - zSize / 2.0 - diameter[2] / 2.0);
                    for (int i = 0; i < mgel.Count; i++)
                    {
                        system.Add(new MolData(mgel[i][3], system.Count+1, molIndex, mgel[i][0], mgel[i][1], mgel[i][2]));
                    }
                }
                else
                {
                    double[] zborders = new double[2] { microgelPosition - diameter[2], microgelPosition + diameter[2] };
                    addNonLinearMolecules(microgelNum, zborders, 0.5, microgel, system, compComs);
                }
                List<int[]> interBonds = MolData.MultiplyBonds(microgelNum, microgelBonds);
                List<int[]> interAngles = MolData.MultiplyAngles(microgelNum, microgelAngles);

                int brushBead = brushNum * molecule.Count;
                foreach (var c in interBonds)
                {
                    totalBonds.Add(new int[] { c[0] + brushBead, c[1] + brushBead });
                }

                foreach (var c in interAngles)
                {
                    totalAngles.Add(new int[] { c[0] + brushBead, c[1] + brushBead, c[2] + brushBead });
                }
            }


            addSolvents(false, filmHeight, system);

            if (twoSolvs)
            {
                int solvCount = (int)(system.Where(x => x.AtomType == 1.02).ToList().Count * solvTwoPerc);
                int solvTwoCount = 0;
                foreach (MolData molData in system)
                {
                    if (molData.AtomType == 1.02)
                    {
                        molData.AtomType = 1.07;
                        solvTwoCount++;
                    }
                    if (solvTwoCount == solvCount)
                    {
                        break;
                    }
                }
            }

            if (wallsType == 1)
            {
                AddWalls(system);
            }

            return system;
        }

        public List<MolData> ComposeDrop(out List<int[]> totalBonds, out List<int[]> totalAngles, bool inBox, bool oneMol, double radius, double polyPerc, double solvType, bool inPercents, bool inUnits, bool outsideDrop, bool nonLinMol, double distFromSubstrate, List<int[]> bonds, List<int[]> angles)
        {
            var system = new List<MolData>();

            int dropNum = (int)(4.0 * Math.PI / 3.0 * Math.Pow(radius, 3.0) * density);
            int polNum = (int)((double)dropNum * polyPerc);

            if (inBox && dropNum >= maxNum)
            {
                throw new Exception("Капля больше размеров ящика!\n" +
                                     "Уменьшите размер капли!");
            }

            int molAmount = polNum / this.molecule.Count;

            if (oneMol)
            {
                molAmount = 1;
            }

            if (inUnits)
            {
                molAmount = (int)(polyPerc * 100.0);
                polNum = molecule.Count * molAmount;
            }
            if (inPercents)
            {
                dropNum = (int)(radius * (maxNum - molAmount * molecule.Count) / 100.0);
                radius = !outsideDrop ? Math.Round(Math.Pow((dropNum + polNum) / (4.0 * Math.PI / 3.0 * density), 1.0 / 3.0), 2) :
                         Math.Round(Math.Pow(dropNum / (4.0 * Math.PI / 3.0 * density), 1.0 / 3.0), 2);
            }
            Math.Max(polNum, molAmount * molecule.Count);

            int[,] borders = new int[3, 2]
            {
            {-(int)radius, (int)radius},
            {-(int)radius, (int)radius},
            {-(int)radius, (int)radius}
            };

            addDropMolecules(dropNum, molAmount, oneMol, solvType, radius, borders, outsideDrop, nonLinMol, this.molecule, system);
            totalBonds = MolData.MultiplyBonds(molAmount, bonds);
            totalAngles = MolData.MultiplyAngles(molAmount, angles);
            if (inBox)
            {
                addDropSurround(radius, solvType, system);
                if (this.hasWalls)
                {
                    if (distFromSubstrate != double.NaN)
                    {
                        MolData.ShiftAll(false, false, density, new double[] { xSize, ySize, zSize },
                                         new double[] { 0.0, 0.0, +radius + 0.8 * step + distFromSubstrate }, centerPoint, system);
                    }

                    if (wallsType == 1)
                    {
                        AddWalls(system);
                    }
                }
            }
            return system;
        }

        public List<MolData> ComposeLipidSystem(out List<int[]> totalBonds, out List<int[]> totalAngles, bool isRandom, int lipidNum, double ligandPerc,
                                                int microgelNum, double layerPosition, double microgelPosition, bool isTwolLiqs,
                                                List<int[]> lipidBonds, List<int[]> lipidAngles,
                                                List<double[]> microgel, List<int[]> microgelBonds, List<int[]> microgelAngles)
        {
            List<double[]> Coms = new List<double[]>();
            var system = new List<MolData>();

            if (isRandom)
            {
                if ((uint)microgelNum > 0U)
                {
                    secondMolecule = microgel;
                    double[] diameter = Methods.GetDiameter(secondMolecule);

                    double collidingCoef = 0.8;

                    if (diameter[0] / xSize > 0.3 || diameter[1] / ySize > 0.3 || diameter[2] / zSize > 0.3)
                    {
                        collidingCoef = 0.3;
                    }
                    molNumber = microgelNum;
                    placeMoleculesRandomly(secondMolecule, null, collidingCoef, system);

                    totalBonds = MolData.MultiplyBonds(microgelNum, microgelBonds);
                    totalAngles = MolData.MultiplyAngles(microgelNum, microgelAngles);

                    var interBonds = MolData.MultiplyBonds(lipidNum, lipidBonds);
                    var interAngles = MolData.MultiplyAngles(lipidNum, lipidAngles);

                    int microgelBeads = microgelNum * microgel.Count;
                    foreach (var c in interBonds)
                    {
                        totalBonds.Add(new int[] { c[0] + microgelBeads, c[1] + microgelBeads });
                    }

                    foreach (var c in interAngles)
                    {
                        totalAngles.Add(new int[] { c[0] + microgelBeads, c[1] + microgelBeads, c[2] + microgelBeads });
                    }
                }
                else
                {
                    totalBonds = MolData.MultiplyBonds(lipidNum, lipidBonds);
                    totalAngles = MolData.MultiplyAngles(lipidNum, lipidAngles);
                }
                molNumber = lipidNum;
                placeMoleculesRandomly(molecule, null, 1.0, system);

            }
            else
            {
                double[] lipDiameter = Methods.GetDiameter(molecule);
                if (lipidNum > 1)
                {
                    double[] zborders = new double[] { layerPosition - lipDiameter[2], layerPosition + lipDiameter[2] };
                    addFilmMolecules(lipidNum, zborders, molecule, lipidBonds, system);
                    //addNonLinearMolecules(lipidNum, zborders, 0.01, molecule, system, Coms);
                }
                else
                {
                    var layer = moveMolecule(molecule, centerPoint[0] - lipDiameter[0] / 2.0, centerPoint[1] - lipDiameter[1] / 2.0, layerPosition - zSize / 2.0 - lipDiameter[2] / 2.0);
                    for (int i = 0; i < layer.Count; i++)
                    {
                        system.Add(new MolData(layer[i][3], system.Count + 1, 1, layer[i][0], layer[i][1], layer[i][2]));
                    }
                }

                totalBonds = MolData.MultiplyBonds(lipidNum, lipidBonds);
                totalAngles = MolData.MultiplyAngles(lipidNum, lipidAngles);

                if ((uint)microgelNum > 0U)
                {
                    double[] diameter = Methods.GetDiameter(microgel);

                    var molInd = system.Max(x => x.MolIndex);

                    if (microgelNum == 1)
                    {
                        var mgel = moveMolecule(microgel, centerPoint[0] - diameter[0] / 2.0, centerPoint[1] - diameter[1] / 2.0,
                                                microgelPosition - zSize / 2.0 - diameter[2] / 2.0);
                        molInd++;

                        for (int i = 0; i < mgel.Count; i++)
                        {
                            system.Add(new MolData(mgel[i][3], system.Count + 1, molInd, mgel[i][0], mgel[i][1], mgel[i][2]));
                        }
                    }
                    else
                    {
                        double[] zborders = new double[2] { microgelPosition - diameter[2], microgelPosition + diameter[2] };
                        addNonLinearMolecules(microgelNum, zborders, 0.5, microgel, system, Coms);
                    }
                    List<int[]> interBonds = MolData.MultiplyBonds(microgelNum, microgelBonds);
                    List<int[]> interAngles = MolData.MultiplyAngles(microgelNum, microgelAngles);

                    int lipidBeads = lipidNum * molecule.Count;
                    foreach (var c in interBonds)
                    {
                        totalBonds.Add(new int[] { c[0] + lipidBeads, c[1] + lipidBeads });
                    }

                    foreach (var c in interAngles)
                    {
                        totalAngles.Add(new int[] { c[0] + lipidBeads, c[1] + lipidBeads, c[2] + lipidBeads });
                    }
                }
            }
            percentage = 0.0;

            if (ligandPerc > 0.0)
            {
                int ligandnum = (int)(lipidNum * ligandPerc);
                double headsType = 1.01;

                if (molecule.Where(x => x[3].Equals(headsType)).ToList().Count == 0)
                {
                    headsType = 1.05;
                }

                int counter = 0;

                Random rand = new Random();
                do
                {
                    var localInd = rand.Next();

                    var locMol = system.Where(x => x.MolIndex == localInd).ToList();

                    if (locMol.Count < 15)
                    {
                        if (locMol.Where(x => x.AtomType == headsType).ToList().Count != 0)
                        {
                            foreach (var c in locMol)
                            {
                                if (c.AtomType == headsType)
                                {
                                    c.AtomType = 1.06;
                                }
                            }
                            counter++;
                        }
                    }
                } while (counter < ligandnum);
            }

            // solvent
            if (lipidNum == 1 && !isRandom)
            {
                addSolventsToLayer(isTwolLiqs, layerPosition, system);
            }
            else
            {
                addSolvents(false, system);
            }
            if (hasWalls && wallsType == 1)
            {
                AddWalls(system);
            }

            return system;
        }

        public static List<MolData> MakeSlice(bool onlyPolymer, bool bySphere, double density, double xSize, double ySize, double zSize, int index, double coord, List<double[]> data)
        {
            var system = new List<MolData>();
            double step = 1.0 / Math.Pow(density, 1.0 / 3.0);
            var pol = data.Where(x => x[3] == 1.0 && x[3] == 1.01
                                 && x[3] == 1.04 && x[3] == 1.05).ToList();
            if (!onlyPolymer)
            {
                if (bySphere)
                {
                    double[] cMass = Methods.GetCenterMass(pol);
                    pol = data.Where(x => (Math.Sqrt(Math.Pow(x[0] - cMass[0], 2) + Math.Pow(x[1] - cMass[1], 2) + Math.Pow(x[2] - cMass[2], 2)) <= coord)
                                            || (x[3] == 1.00 || x[3] == 1.01 || x[3] == 1.04 || x[3] == 1.05)).ToList();
                }
                else
                {
                    int dataind = 0;
                    if (index == 0) dataind = 2;
                    if (index == 1) dataind = 1;

                    pol = data.Where(x => (x[dataind] > coord - step / 2.0 && x[dataind] < coord + step / 2.0)
                                           || (x[3] == 1.00 || x[3] == 1.01 || x[3] == 1.04 || x[3] == 1.05)).ToList();
                }
            }

            for (int i = 0; i < pol.Count; i++)
            {
                system.Add(new MolData(pol[i][3], i + 1, pol[i][0], pol[i][1], pol[i][2]));
            }

            return system;
        }

        public static List<MolData> Recolor(bool onlyPolymer, bool nonLinear, bool allDiffCol, bool isDb, int order, int molOneLength, int molOneCount, int molTwoLength, int molTwoCount, List<double[]> data)
        {
            var system = new List<MolData>();

            var pol = data.Where(x => x[3] == 1.00 ||
                                         x[3] == 1.01 ||
                                         x[3] == 1.04 ||
                                         x[3] == 1.05).ToList();

            if (!onlyPolymer)
            {
                pol = data;
            }
            int molcount = pol.Count / molOneLength;
            for (int i = 0; i < molcount; i++)
            {
                int finalType = i;
                for (int j = 0; j < molOneLength; j++)
                {
                    data[j + molOneLength * i][4] = (double)(i + 1);
                    if (isDb)
                    {
                        if (data[j + molOneLength * i][3].Equals(1.0))
                        {
                            data[j + molOneLength * i][3] = 1.0 + (double)finalType / 100.0;
                            if (finalType > 9)
                                data[j + molOneLength * i][3] = (double)(finalType + 1);
                        }
                        else if (data[j + molOneLength * i][3].Equals(1.01))
                        {
                                data[j + molOneLength * i][3] = (double)(finalType + 1 + molcount);
                        }
                        //int aType = data[j + molOneLength * i][3].Equals(1.0) ? 1 : (data[j + molOneLength * i][3].Equals(1.04) ? 1 : 0);
                        //data[j + molOneLength * i][3] = aType == 0 ? 1.0 + (double)finalType / 100.0 : -1.0;
                    }
                    else
                    {
                        data[j + molOneLength * i][3] = 1.0 + (double)finalType / 100.0;
                        if (finalType > 9)
                            data[j + molOneLength * i][3] = (double)(finalType + 1);
                    }
                }
            }
            for (int i = 0; i < pol.Count; i++)
            {
                if (pol[i][3] > 0.0)
                    system.Add(new MolData(pol[i][3], i + 1, (int)pol[i][4], pol[i][0], pol[i][1], pol[i][2]));
            }
            return system;
        }

       

        #region Internal methods
        private void checkBorders(List<MolData> system)
        {
            checkBorders(system, 0.0);
        }

        private void checkBorders(List<MolData> system, double coef)
        {
            checkPlainBorders(system);
            bool zPositive = true;
            foreach (var c in system)
            {
                if (c.ZCoord < 0.0)
                {
                    zPositive = false;
                    break;
                }
            }
            foreach (MolData molData in system)
            {
                if (zPositive)
                {
                    if (molData.ZCoord <= 0.8 * step)
                        molData.ZCoord = molData.ZCoord + zSize - 1.6 * step;
                    if (molData.ZCoord >= zSize - 0.8 * step - coef)
                        molData.ZCoord = molData.ZCoord - zSize + 1.6 * step + coef;
                }
                else
                {
                    if (molData.ZCoord <= -zSize / 2.0 + 0.8 * step)
                        molData.ZCoord = molData.ZCoord + zSize - 1.6 * step;
                    if (molData.ZCoord >= zSize / 2.0 - 0.8 * step - coef)
                        molData.ZCoord = molData.ZCoord - zSize + 1.6 * step + coef;
                }
            }
        }

        private void checkPlainBorders(List<MolData> system)
        {
            foreach (var c in system)
            {
                if (c.XCoord <= -xSize / 2.0 + 0.5)
                    c.XCoord += xSize;
                if (c.XCoord >= xSize / 2.0 - 0.5)
                    c.XCoord -= xSize;
                if (c.YCoord <= -ySize / 2.0 + 0.5)
                    c.YCoord += ySize;
                if (c.YCoord >= ySize / 2.0 - 0.5)
                    c.YCoord -= ySize;
            }
        }

        private void placeMoleculesRandomly(List<double[]> mol, List<int[]> bonds, double collidingCoef, List<MolData> system)
        {
            double xDiam = Methods.GetAxDiameter(mol, 0);
            double yDiam = Methods.GetAxDiameter(mol, 1);
            double zDiam = Methods.GetAxDiameter(mol, 2);

            if (zDiam < 2) { zDiam = 2; }

            int counter = 0;

            if (system.Count != 0)
            {
                molInd = system.Max(x => x.MolIndex);
            }

            double radius = Math.Max(zDiam, Math.Max(xDiam, yDiam)) * collidingCoef;
            Random rnd = new Random();

            Timer timer = new Timer();
            timer.Interval = 90000.0;
            timer.Elapsed += new ElapsedEventHandler(this.reportIsFull);
            timer.Start();

            if (molNumber > 1)
            {
                do
                {
                    bool collide = false;
                    double xCoord = (double)rnd.Next(Math.Min((int)xDiam, (int)(xSize - xDiam / 2.0)), Math.Max((int)xDiam, (int)(xSize - xDiam / 2.0)));
                    double yCoord = (double)rnd.Next(Math.Min((int)yDiam, (int)(ySize - yDiam / 2.0)), Math.Max((int)yDiam, (int)(ySize - yDiam / 2.0)));
                    double zCoord = (double)rnd.Next(Math.Min((int)zDiam, (int)(zSize - zDiam / 2.0)), Math.Max((int)zDiam, (int)(zSize - zDiam / 2.0)));
                    if (separatedSolvs)
                    {
                        if (location != 1)
                            zCoord = rnd.Next(Math.Min((int)(zDiam), (int)(zSize * (1 - percentage) - zDiam / 2.0)),
                                              Math.Max((int)(zDiam), (int)(zSize * (1 - percentage) - zDiam / 2.0)));
                        else
                            zCoord = rnd.Next(Math.Min((int)(zSize * percentage + zDiam / 4.0), (int)(zSize - zDiam / 2.0)),
                                              Math.Max((int)(zSize * percentage + zDiam / 4.0), (int)(zSize - zDiam / 2.0)));
                    }
                    var interMol = moveMolecule(molecule, xCoord - xSize / 2.0 - xDiam / 2.0,
                                                     yCoord - ySize / 2.0 - yDiam / 2.0,
                                                     zCoord - zSize / 2.0 - zDiam / 2.0);

                    var slice = new List<MolData>();
                    var distances = new List<double>();

                    if (collidingCoef != 0.0)
                    {
                        foreach (var c in system)
                        {
                            if (Methods.GetDistance(xCoord - xSize / 2.0, yCoord - ySize / 2.0, zCoord - zSize / 2.0,
                                                         c.XCoord, c.YCoord, c.ZCoord) <= radius)
                            {
                                collide = true;
                                break;
                            }
                        }
                    }
                    if (!collide)
                    {
                        molInd++;
                        counter++;

                        for (int i = 0; i < interMol.Count; i++)
                        {
                            system.Add(new MolData(interMol[i][3], system.Count + 1, molInd,
                                                    interMol[i][0], interMol[i][1], interMol[i][2]));
                        }
                    }

                    if (notFit)
                    {
                        throw new Exception("Размер ящика слишком мал! Измените число молекул!");
                    }
                }
                while (counter < molNumber);
            }
            else
            {
                var interMol = moveMolecule(mol, -xDiam / 2.0, -yDiam / 2.0, -zSize * percentage / 2.0);
                molInd++;

                foreach (var c in interMol)
                {
                    system.Add(new MolData(c[3], system.Count + 1, molInd,
                                           c[0], c[1], c[2]));
                }

                //var randMol = MolData.ConvertToMolData(mol, true, bonds);

                //var borders = new List<double[]> { new double[] { 0.0, 0.0 },
                //                                   new double[] { 0.0, 0.0 },
                //                                   new double[] { 0.0, 0.0 } };

                //// starterbead
                //double xCoord = rnd.Next(0, (int)(xSize * 100)) / 100.0;
                //double yCoord = rnd.Next(0, (int)(ySize * 100)) / 100.0;
                //double zCoord = rnd.Next(0, (int)(zSize * 100)) / 100.0;

                //system.Add(new MolData(randMol[0].AtomType, system.Count + 1, xCoord - xSize / 2.0, yCoord - ySize / 2.0, zCoord - zSize / 2.0));

                //var currBead = randMol[0];

                //var placedBeads = new List<int>();

                //addNonLinMol_Recurcion("0", borders, rnd, placedBeads, currBead, randMol, system);
            }

            if (isHomoPol)
            {
                foreach (var c in system)
                {
                    if (c.AtomType == 1.01)
                        c.AtomType = 1.0;
                }
            }

            checkBorders(system);
        }

        private void reportIsFull(object source, ElapsedEventArgs e)
        {
            notFit = true;
        }


        private void addSolvents(bool twoOils, List<MolData> system)
        {
            addSolvents(twoOils, 0.0, system);
        }

        private void addSolvents(bool twoOils, double heightF, List<MolData> system)
        {
            // in case of implicit solvent (MD)
            if (density == 0.0)
            {
                return;
            }

            int polCount = system.Where(x => x.AtomType.Equals(1.0)
                                        || x.AtomType.Equals(1.01)
                                        || x.AtomType.Equals(1.04)
                                        || x.AtomType.Equals(1.05)
                                        || x.AtomType.Equals(1.06)).ToList().Count;

            // starting molIndex
            molInd = system.Max(x => x.MolIndex);

            // walls stuff
            int wallCount = 0;
            if (hasWalls && wallsType == 1)
            {
                var wallSystem = new List<MolData>();
                AddWalls(wallSystem);
                wallCount = wallSystem.Count;
            }
            int waterCount = maxNum - polCount - wallCount;
            int oilCount = (int)((double)(this.maxNum - polCount - wallCount) * this.percentage);
            double waterType = 1.03;
            double solvType = 1.02;

            if (location == 1)
            {
                waterType = 1.02;
                solvType = 1.03;
            }
            double zBorder = 0.0;

            if (separatedSolvs)
            {
                if (percentage < 1.0 && percentage > 0.0)
                {
                    waterCount -= oilCount;
                    zBorder = findInterCord() + zSize / 2.0;
                }
            }
            double[] zBorders = new double[2] { 0.0, zSize };

            if (hasWalls)
            {
                zBorders = new double[2] { 2.0 * step, zSize - 2.0 * step };
            }
            if (isFilm)
            {
                if (heightF != 0)
                {
                    zBorders[0] = heightF;
                }
                else
                {
                    double filmHeight = system.Max((x => x.ZCoord)) + zSize / 2.0;
                    zBorders[0] = filmHeight;

                }
            }
            Random rnd = new Random();
            Random rndDec = new Random();
            int counter = 0;
            do
            {
                double xCoord = (double)rnd.Next(1, (int)xSize-1) + (double)rndDec.Next(-100, 100) / 100.0;
                double yCoord = (double)rnd.Next(1, (int)ySize-1) + (double)rndDec.Next(-100, 100) / 100.0;
                double zCoord = (double)rnd.Next((int)zBorders[0], (int)zBorders[1]) + (double)rndDec.Next(-100, 100) / 100.0;
                if (separatedSolvs && percentage < 1.0 && percentage > 0.0)
                {
                    zCoord = this.location != 1 ? (double)rnd.Next((int)zBorders[0], (int)zBorder) + (double)rndDec.Next(-100, 100) / 100.0 :
                                                  (double)rnd.Next((int)zBorder, (int)zBorders[1]) + (double)rndDec.Next(-100, 100) / 100.0;
                }

                if (true)
                {
                    molInd++;
                    counter++;
                    system.Add(new MolData(waterType, system.Count + 1, molInd, xCoord - xSize / 2.0, yCoord - ySize / 2.0, zCoord - zSize / 2.0));
                }
            }
            while (counter < waterCount);

            // add oil
            if (percentage == 0.0)
            {
                return;
            }
            else if (percentage < 1.0)
            {
                if ((uint)oilCount > 0U)
                {
                    if (separatedSolvs)
                    {
                        counter = 0;
                        do
                        {
                            double xCoord = (double)rnd.Next(0, (int)xSize) + (double)rndDec.Next(-100, 100) / 100.0;
                            double yCoord = (double)rnd.Next(0, (int)ySize) + (double)rndDec.Next(-100, 100) / 100.0;
                            double zCoord = (double)rnd.Next((int)zBorder, (int)zBorders[1]) + (double)rndDec.Next(-100, 100) / 100.0;
                            if (location == 1)
                            {
                                zCoord = (double)rnd.Next((int)zBorders[0], (int)zBorder) + (double)rndDec.Next(-100, 100) / 100.0;
                            }
                            if (true)
                            {
                                molInd++;
                                counter++;
                                system.Add(new MolData(solvType, system.Count + 1, this.molInd, xCoord - xSize / 2.0, yCoord - ySize / 2.0, zCoord - zSize / 2.0));
                            }
                        }
                        while (counter < oilCount);
                    }
                    else
                    {
                        int randsolvCount = 0;
                        do
                        {
                            int number = rnd.Next(polCount + 1, maxNum - wallCount);
                            if (!system[number - 1].AtomType.Equals(solvType))
                            {
                                system[number - 1].AtomType = solvType;
                                randsolvCount++;
                            }
                        }
                        while (randsolvCount < oilCount);
                    }
                }
                if (!twoOils)
                {
                    return;
                }
                else
                {
                    int randSecondOil = 0;

                    // Two oils
                    do
                    {
                        int number = rnd.Next(polCount + 1, maxNum - wallCount);
                        if (system[number - 1].AtomType.Equals(solvType))
                        {
                            system[number - 1].AtomType = 1.07;
                            randSecondOil++;
                        }
                    }
                    while (randSecondOil < oilCount / 2);
                }
            }
            else
            {
                foreach (MolData molData in system)
                {
                    if (molData.AtomType.Equals(waterType))
                        molData.AtomType = solvType;
                }
            }
        }

        private void addSolventsToLayer(bool isTwoLiqs, double layerPos, List<MolData> system)
        {
            if (density == 0.0)
            {
                return;
            }

            int polCount = system.Where(x => x.AtomType.Equals(1.0)
                                        || x.AtomType.Equals(1.01)
                                        || x.AtomType.Equals(1.04)
                                        || x.AtomType.Equals(1.05)
                                        || x.AtomType.Equals(1.06)).ToList().Count;

            // starting molIndex
            molInd = system.Max(x => x.MolIndex);

            // Walls stuff
            int wallCount = 0;
            if (hasWalls && wallsType == 1)
            {
                var wallSystem = new List<MolData>();
                AddWalls(wallSystem);
                wallCount = wallSystem.Count;
            }
            int waterCount = maxNum - polCount - wallCount;

            int lowWaterCount = (int)(waterCount * (layerPos / zSize));
            int upperWaterCount = waterCount - lowWaterCount;

            double lowerType = 1.03;
            double upperType = 1.03;

            if (isTwoLiqs)
            {
                upperType = 1.02;
            }

            double[] zBordersLow = new double[2] { 0.0, layerPos - 3.5 };
            double[] zBordersUp = new double[2] { layerPos + 3.5, zSize };

            if (hasWalls)
            {
                zBordersLow[0] = 2.0 * step;
                zBordersUp[1] = zSize - 2.0 * step;
            }

            Random rnd = new Random();
            Random rndDec = new Random();

            for (int i = 0; i <= 1; i++)
            {
                var typeW = lowerType;
                int localCount = lowWaterCount;
                var localZBorders = zBordersLow;

                if (i == 1)
                {
                    typeW = upperType;
                    localCount = upperWaterCount;
                    localZBorders = zBordersUp;
                }


                int counter = 0;
                do
                {
                    double xCoord = (double)rnd.Next(0, (int)xSize) + (double)rndDec.Next(-100, 100) / 100.0;
                    double yCoord = (double)rnd.Next(0, (int)ySize) + (double)rndDec.Next(-100, 100) / 100.0;
                    double zCoord = (double)rnd.Next((int)localZBorders[0], (int)localZBorders[1]) + (double)rndDec.Next(-100, 100) / 100.0;
                    molInd++;
                    counter++;

                    system.Add(new MolData(typeW, system.Count + 1, molInd, xCoord - xSize / 2.0, yCoord - ySize / 2.0, zCoord - zSize / 2.0));

                } while (counter < localCount);
            }
        }

        private void addDropSurround(double radius, double solvType, List<MolData> system)
        {
            double atomType = 1.03;
            if (atomType.Equals(solvType))
                atomType = 1.02;
            int surrCount = maxNum - system.Count;
            Random rnd = new Random();
            Random rndDec = new Random();
            int counter = 0;
            do
            {
                double xCoord = (double)rnd.Next(-(int)xSize / 2, (int)xSize / 2) + (double)rndDec.Next(-100, 100) / 100.0;
                double yCoord = (double)rnd.Next(-(int)ySize / 2, (int)ySize / 2) + (double)rndDec.Next(-100, 100) / 100.0;
                double zCoord = (double)rnd.Next(-(int)zSize / 2, (int)zSize / 2) + (double)rndDec.Next(-100, 100) / 100.0;
                if (Math.Sqrt(Math.Pow(xCoord, 2.0) + Math.Pow(yCoord, 2.0) + Math.Pow(zCoord, 2.0)) > radius)
                {
                    molInd++;
                    system.Add(new MolData(atomType, system.Count + 1, xCoord, yCoord, zCoord));
                    counter++;
                }
            }
            while (counter < surrCount);
            checkBorders(system);
        }

        private void addMatrixAndSolvents(int chainLength, double matrixAtomType, double polyPerc, bool isDiblock, int blockALength, List<MolData> system)
        {
            int maxNumMatrix = (int)((double)this.maxNum * (1.0 - this.percentage));
            int remainedPol = (int)((double)maxNumMatrix * polyPerc) - system.Count;

            if (remainedPol < 0)
            {
                addSolvents(false, system);
            }
            else
            {
                int chainAmount = remainedPol / chainLength;
                if (chainAmount == 0) { chainAmount++; }

                double height = zSize * (1.0 - percentage);
                double[] zborders = new double[] { 2.0 * step, zSize * (1.0 - percentage) };

                if (location == 1)
                {
                    zborders = new double[] { zSize * percentage, zSize - step };
                }

                var mol = new List<double[]>();
                for (int i = 0; i < chainLength; i++)
                {
                    if (isDiblock)
                    {
                        if (i < blockALength)
                        {
                            mol.Add(new double[] { 1, 1, 1 + i * step, matrixAtomType });
                        }
                        else
                        {
                            mol.Add(new double[] { 1, 1, 1 + i * step, 1.01 });
                        }
                    }
                    else
                    {
                        mol.Add(new double[] { 1, 1, 1 + i * step, matrixAtomType });
                    }
                }
                addLinearFilmMolecules(chainAmount, height, zborders, mol, system);

                int waterMolecules = maxNumMatrix - system.Count;
                int solvMolecules = this.maxNum - maxNumMatrix;
                Random rnd = new Random();

                for (int i = 0; i < waterMolecules; i++)
                {
                    double xCoord = (double)rnd.Next(0, (int)xSize);
                    double yCoord = (double)rnd.Next(0, (int)ySize);
                    double zCoord = location == 1 ? (double)rnd.Next((int)(zSize * percentage), (int)(zSize - step)) : (double)rnd.Next((int)(2.0 * step), (int)(zSize * (1.0 - percentage)));

                    molInd++;
                    system.Add(new MolData(1.03, system.Count + 1, molInd,
                       xCoord - xSize / 2.0,
                       yCoord - ySize / 2.0,
                       zCoord - zSize / 2.0));
                }
                if ((uint)solvMolecules <= 0U)
                    return;
                for (int i = 0; i < solvMolecules; i++)
                {
                    double xCoord = rnd.Next(0, (int)xSize);
                    double yCoord = rnd.Next(0, (int)ySize);
                    double zCoord = location == 1 ? (double)rnd.Next((int)(2.0 * this.step), (int)(zSize * percentage)) : (double)rnd.Next((int)(zSize * (1.0 - percentage)), (int)(zSize - step));

                    molInd++;
                    system.Add(new MolData(1.03, system.Count + 1, molInd,
                                           xCoord - xSize / 2.0,
                                           yCoord - ySize / 2.0,
                                           zCoord - zSize / 2.0));
                }
            }
        }
        /// <summary>
        /// Add linear molecules in film
        /// </summary>
        private void addLinearFilmMolecules(int chainAmount, double height, double[] zborders, List<double[]> molecule, List<MolData> system)
        {
            if (chainAmount == 0)
            {
                return;
            }

            int length = molecule.Count;
            double minZ = 2.0 * step;

            if (zborders[0] > minZ)
            {
                minZ = zborders[0];
            }

            Random rnd = new Random();
            Random rndDec = new Random();
            for (int i = 0; i < chainAmount; i++)
            {
                double xCoord = (double)rnd.Next(0, (int)xSize);
                double yCoord = (double)rnd.Next(0, (int)ySize);
                double zCoord = (double)rnd.Next((int)zborders[0], (int)zborders[1]);

                int counter = 1;
                int initCount = system.Count;

                molInd++;
                if (i % 2 == 0)
                    system.Add(new MolData(molecule[0][3], system.Count + 1, this.molInd, xCoord - (double)this.xSize / 2.0, yCoord - (double)this.ySize / 2.0, zCoord - (double)this.zSize / 2.0));
                else
                    system.Add(new MolData(molecule[length - counter][3], initCount + length, this.molInd, xCoord - (double)this.xSize / 2.0, yCoord - (double)this.ySize / 2.0, zCoord - (double)this.zSize / 2.0));
                do
                {
                    double xCoordNext = (double)rnd.Next((int)(xCoord - 2.0 * this.step), Math.Min((int)(xCoord + 2.0 * step), (int)xSize)) + (double)rndDec.Next(-100, 100) / 100.0;
                    double yCoordNext = (double)rnd.Next((int)(yCoord - 2.0 * this.step), Math.Min((int)(yCoord + 2.0 * step), (int)ySize)) + (double)rndDec.Next(-100, 100) / 100.0;
                    double zCoordNext = (double)rnd.Next((int)minZ, Math.Min((int)(zCoord + 2.0 * this.step), (int)height));
                    double distance = Math.Sqrt(Math.Pow(xCoordNext - xCoord, 2.0) + Math.Pow(yCoordNext - yCoord, 2.0) + Math.Pow(zCoordNext - zCoord, 2.0));

                    if (distance >= this.step && distance <= 2.5 * this.step)
                    {
                        xCoord = xCoordNext;
                        yCoord = yCoordNext;
                        zCoord = zCoordNext;

                        if (i % 2 == 0)
                        {
                            system.Add(new MolData(molecule[counter][3], system.Count + 1, molInd,
                                           xCoord - xSize / 2.0, yCoord - ySize / 2.0,
                                           zCoord - zSize / 2.0));
                        }
                        else
                        {
                            system.Add(new MolData(molecule[length - counter - 1][3], initCount + length - counter, molInd,
                                          xCoord - xSize / 2.0, yCoord - ySize / 2.0,
                                          zCoord - zSize / 2.0));
                        }
                        counter++;
                    }
                }
                while (counter < length);
            }

            checkBorders(system);
        }

        private void addLinearMoleculesWithObstacles(int chainAmount, double height, double[] zborders, List<double[]> molecule, List<MolData> system, List<double[]> coms)
        {
            if (chainAmount == 0)
            {
                return;
            }

            int length = molecule.Count;

            double minZ = 2.0 * this.step;

            if (zborders[0] > minZ)
            {
                minZ = zborders[0];
            }

            Random rnd = new Random();
            Random rndDec = new Random();

            for (int i = 0; i < chainAmount; i++)
            {
                int counter = 0;
                int initCount = system.Count;
                double xCoord;
                double yCoord;
                double zCoord;
                do
                {
                    // Первое звено цепи
                    xCoord = (double)rnd.Next(0, (int)xSize);
                    yCoord = (double)rnd.Next(0, (int)ySize);
                    zCoord = (double)rnd.Next((int)zborders[0], (int)zborders[1]);

                    // Провека не попадает ли в частицу
                    if (coms.Where(x => Methods.GetDistance(x[0], x[1], x[2], xCoord, yCoord, zCoord) < 1.2).ToList().Count == 0)
                    {
                        counter++;
                        molInd++;

                        if (i % 2 == 0)
                            system.Add(new MolData(molecule[0][3], system.Count + 1, molInd,
                                                  xCoord - xSize / 2.0, yCoord - ySize / 2.0,
                                                  zCoord - zSize / 2.0));
                        else
                            system.Add(new MolData(molecule[length - counter][3], initCount + length, molInd,
                                                 xCoord - xSize / 2.0, yCoord - ySize / 2.0,
                                                 zCoord - zSize / 2.0));
                    }
                }
                while (counter == 0);
                do
                {
                    double xCoordNext = (double)rnd.Next((int)(xCoord - 2.0 * step), Math.Min((int)(xCoord + 2.0 * step), (int)xSize)) + (double)rndDec.Next(-100, 100) / 100.0;
                    double yCoordNext = (double)rnd.Next((int)(yCoord - 2.0 * step), Math.Min((int)(yCoord + 2.0 * step), (int)ySize)) + (double)rndDec.Next(-100, 100) / 100.0;
                    double zCoordNext = (double)rnd.Next((int)minZ, Math.Min((int)(zCoord + 2.0 * step), (int)height));
                    double distance = Methods.GetDistance(xCoordNext, yCoordNext, zCoordNext, xCoord, yCoord, zCoord);

                    if (distance >= step && distance <= 2.5 * step && coms.Where((x => Methods.GetDistance(x[0], x[1], x[2], xCoordNext, yCoordNext, zCoordNext) <= 1.2)).ToList().Count == 0)
                    {
                        xCoord = xCoordNext;
                        yCoord = yCoordNext;
                        zCoord = zCoordNext;

                        if (i % 2 == 0)
                        {
                            system.Add(new MolData(molecule[counter][3], system.Count + 1, molInd,
                                           xCoord - xSize / 2.0, yCoord - ySize / 2.0,
                                           zCoord - zSize / 2.0));
                        }
                        else
                        {
                            system.Add(new MolData(molecule[length - counter - 1][3], initCount + length - counter, molInd,
                                          xCoord - xSize / 2.0, yCoord - ySize / 2.0,
                                          zCoord - zSize / 2.0));
                        }
                        counter++;
                    }
                }
                while (counter < length);
            }
            checkBorders(system);
        }

        private void addFilmMolecules(int chainAmount, double[] zborders, List<double[]> mol, List<int[]> bonds, List<MolData> system)
        {
            if (chainAmount == 0)
            {
                return;
            }
            else
            {
                Random rnd = new Random();

                int counter = 0;
                int molIndex = 1;
                var filmMol = MolData.ConvertToMolData(mol, true, bonds);

                //MolData.RecolorRandomly(6, 2, filmMol);

                // Order of molecule number
                if (system.Count != 0)
                {
                    molIndex = system.Max(x => x.MolIndex) + 1;
                }

                var borders = new List<double[]> { new double[] { -xSize / 2.0 + 1.8, xSize / 2.0 - 1.8 },
                                                   new double[] { -ySize / 2.0 + 1.8, ySize / 2.0 - 1.8 },
                                                   new double[] {zborders[0] - zSize / 2.0, zborders[1] - zSize / 2.0 } };

                do
                {
                    // starterbead
                    double xCoord = rnd.Next(1, (int)xSize - 1);
                    double yCoord = rnd.Next(1, (int)ySize - 1);
                    double zCoord = rnd.Next((int)(zborders[0] * 100), (int)(zborders[1] * 100)) / 100.0;

                    system.Add(new MolData(filmMol[0].AtomType, system.Count+1, molIndex, xCoord - xSize / 2.0, yCoord - ySize / 2.0, zCoord - zSize / 2.0));

                    var currBead = filmMol[0];

                    var placedBeads = new List<int>();

                    Methods.AddNonLinMol_Recurcion(molIndex, "0", borders, rnd, placedBeads, currBead, filmMol, system);
                    //MolData.RecolorRandomly(6, 19, filmMol);

                    counter++;
                    molIndex++;
                    
                } while (counter < chainAmount);
            }
        }

        private void addNonLinearMolecules(int chainAmount, double[] zborders, double collidingCoef, List<double[]> mol, List<MolData> system, List<double[]> coms)
        {
            if (chainAmount == 0)
            {
                return;
            }
            else
            {

                Random rnd = new Random();
                Random rndDec = new Random();

                // in case if we don't use the constructor
                if (system.Count != 0)
                {
                    molInd = system.Max(x => x.MolIndex);
                }

                double xDiam = Methods.GetAxDiameter(mol, 0);
                double yDiam = Methods.GetAxDiameter(mol, 1);
                double zDiam = Methods.GetAxDiameter(mol, 2);

                if (collidingCoef == 0.0)
                {
                    if ((chainAmount * mol.Count) >= 0.1 * maxNum)
                        collidingCoef = 0.0;
                    if ((chainAmount * mol.Count) <= 0.05 * maxNum)
                    {
                        collidingCoef = Methods.GetDiameter(mol).Max() >= (zborders[1] - zborders[0]) / 3.0 ? 0.5 : 1.0;
                    }
                }

                double radius = Math.Max(zDiam, Math.Max(xDiam, yDiam)) * collidingCoef;


                Timer timer = new Timer();
                timer.Interval = (30000 * chainAmount);
                timer.Elapsed += new ElapsedEventHandler(reportIsFull);
                timer.Start();

                checkBorders(system);

                int placedMolecules = 0;
                do
                {
                    bool collide = false;

                    double xCoord = (double)rnd.Next(Math.Min((int)xDiam, (int)(xSize - xDiam / 2.0)), Math.Max((int)xDiam, (int)(xSize - xDiam / 2.0)));
                    double yCoord = (double)rnd.Next(Math.Min((int)yDiam, (int)(ySize - yDiam / 2.0)), Math.Max((int)yDiam, (int)(ySize - yDiam / 2.0)));
                    double zCoord = (double)rnd.Next((int)(zborders[0] + zDiam), (int)(zborders[1] - zDiam));

                    var interMol = moveMolecule(mol, xCoord - xSize / 2.0 - xDiam / 2.0, yCoord - ySize / 2.0 - yDiam / 2.0, zCoord - zSize / 2.0 - zDiam / 2.0);

                    var slice = new List<MolData>();
                    var distances = new List<double>();

                    foreach (var c in system)
                    {
                        if (Methods.GetDistance(xCoord - xSize / 2.0, yCoord - ySize / 2.0, zCoord - zSize / 2.0, c.XCoord, c.YCoord, c.ZCoord) <= radius)
                        {
                            collide = true;
                            break;
                        }
                    }
                    if (!collide)
                    {
                        double[] com = Methods.GetCenterMass(interMol);
                        double diam = Methods.GetDiameter(interMol).Max();
                        coms.Add(new double[] { com[0], com[1], com[2], diam });

                        molInd++;

                        for (int i = 0; i < interMol.Count; i++)
                        {
                            system.Add(new MolData(interMol[i][3], system.Count + 1, molInd, interMol[i][0], interMol[i][1], interMol[i][2]));
                        }
                        placedMolecules++;
                    }
                    if (notFit)
                    {
                        throw new Exception("Размер ящика слишком мал! Измените число молекул!");
                    }
                }
                while (placedMolecules < chainAmount);
            }
        }

        private void addDropMolecules(int dropNum, int molAmount, bool oneMol, double solvType, double radius, int[,] borders, bool outsideDrop, bool nonLinMol, List<double[]> molecule, List<MolData> system)
        {
            double xCoord, yCoord, zCoord;
            Random rnd = new Random();
            Random rndDec = new Random();
            double distanceFromCenter;

            if (system.Count != 0)
            {
                molInd = system.Max(x => x.MolIndex);
            }

            if (oneMol)
            {
                double rad = -radius;
                if (!outsideDrop)
                    rad = 0.0;
                molecule = moveMolecule(molecule, rad - Methods.GetAxDiameter(molecule, 0), 
                                        rad - Methods.GetAxDiameter(molecule, 1), 
                                        rad - Methods.GetAxDiameter(molecule, 2));
                molInd++;

                foreach (var c in molecule)
                {
                    system.Add(new MolData(c[3], system.Count + 1, molInd, c[0], c[1], c[2]));
                }
            }
            else
            {
                // Addition of chains/molecules
                for (int i = 0; i < molAmount; i++)
                {
                    // First bead
                    xCoord = rnd.Next(borders[0, 0] / 2, borders[0, 1] / 2);
                    yCoord = rnd.Next(borders[1, 0] / 2, borders[1, 1] / 2);
                    zCoord = rnd.Next(borders[2, 0] / 2, borders[2, 1] / 2);

                    if (outsideDrop)
                    {
                        do
                        {
                            xCoord = rnd.Next(-(int)(xSize / 2.0), (int)(xSize / 2.0));
                        }
                        while (xCoord < (borders[0, 0] - 2) && xCoord > (borders[0, 1] + 2));

                        do
                        {
                            yCoord = (double)rnd.Next(-(int)(ySize / 2.0), (int)(ySize / 2.0));
                        }
                        while (yCoord < (borders[1, 0] - 2) && yCoord > (borders[1, 1] + 2));

                        do
                        {
                            zCoord = (double)rnd.Next(-(int)(zSize / 2.0), (int)(zSize / 2.0));
                        }
                        while (zCoord < (borders[2, 0] - 2) && zCoord > (borders[2, 1] + 2));
                    }
                    if (!nonLinMol)
                    {
                        molInd++;

                        system.Add(new MolData(molecule[0][3], system.Count + 1, molInd, xCoord, yCoord, zCoord));

                        int counter = 1;
                        do
                        {
                            double xCoordNext = (double)rnd.Next(Math.Min((int)(xCoord - 2.0 * step), borders[0, 0]), Math.Min((int)(xCoord + 2.0 * step), borders[0, 1])) + (double)rndDec.Next(-100, 100) / 100.0;
                            double yCoordNext = (double)rnd.Next(Math.Min((int)(yCoord - 2.0 * step), borders[1, 0]), Math.Min((int)(yCoord + 2.0 * step), borders[1, 1])) + (double)rndDec.Next(-100, 100) / 100.0;
                            double zCoordNext = (double)rnd.Next(Math.Min((int)(zCoord - 2.0 * step), borders[2, 0]), Math.Min((int)(zCoord + 2.0 * step), borders[2, 1])) + (double)rndDec.Next(-100, 100) / 100.0;
                            double distance = Math.Sqrt(Math.Pow(xCoordNext - xCoord, 2.0) + Math.Pow(yCoordNext - yCoord, 2.0) + Math.Pow(zCoordNext - zCoord, 2.0));
                            distanceFromCenter = Math.Sqrt(Math.Pow(xCoordNext, 2.0) + Math.Pow(yCoordNext, 2.0) + Math.Pow(zCoordNext, 2.0));

                            bool collide = false;
                            if (outsideDrop)
                            {
                                if (distanceFromCenter > radius)
                                    collide = true;
                            }
                            else if (distanceFromCenter <= radius)
                            {
                                collide = true;
                            }

                            if (((distance < step ? 0 : (distance <= 2.0 * step ? 1 : 0)) & (collide ? 1 : 0)) != 0)
                            {
                                xCoord = xCoordNext;
                                yCoord = yCoordNext;
                                zCoord = zCoordNext;
                                system.Add(new MolData(molecule[counter][3], system.Count + 1, this.molInd, xCoord, yCoord, zCoord));
                                ++counter;
                            }
                        }
                        while (counter < molecule.Count);
                    }
                    else
                    {
                        molecule = moveMolecule(molecule, -xCoord, -yCoord, -zCoord);
                        molInd++;
                        foreach (var c in molecule)
                            system.Add(new MolData(c[3], system.Count + 1, this.molInd, c[0], c[1], c[2]));
                    }
                }
            }

            int solvCount = dropNum - system.Count;
            if (outsideDrop)
            {
                solvCount = dropNum;
            }
            int solvcounter = 0;

            do
            {
                xCoord = rnd.Next(borders[0, 0], borders[0, 1]) + rndDec.Next(-100, 100) / 100.0;
                yCoord = rnd.Next(borders[1, 0], borders[1, 1]) + rndDec.Next(-100, 100) / 100.0;
                zCoord = rnd.Next(borders[2, 0], borders[2, 1]) + rndDec.Next(-100, 100) / 100.0;

                distanceFromCenter = Math.Sqrt(Math.Pow(xCoord, 2) + Math.Pow(yCoord, 2)
                                    + Math.Pow(zCoord, 2));

                if (distanceFromCenter <= radius)
                {
                    molInd++;
                    system.Add(new MolData(solvType, system.Count + 1, this.molInd, xCoord, yCoord, zCoord));
                    solvcounter++;
                }
            } while (solvcounter < solvCount);
        }

        public void AddWalls(List<MolData> system)
        {
            double[,] wall = this.createWall();

            double zCoord = 0.0;

            if (centerPoint[2] == 0.0)
                zCoord = zSize / 2.0;

            // Each wall has 2 layers, the distance between the layers is 0.8/Step
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < wall.GetLength(0); j++)
                {
                    system.Add(new MolData(1.08, system.Count + 1, 0, wall[j, 0] + 0.5 * i * step, wall[j, 1] + 0.5 * i * step, -zCoord + (1 - i) * (0.8 * step) + 0.001));
                }
                for (int j = 0; j < wall.GetLength(0); j++)
                {
                    system.Add(new MolData(1.09, system.Count + 1, 0, wall[j, 0] + 0.5 * i * step, wall[j, 1] + 0.5 * i * step, zSize - zCoord - (1 - i) * (0.8 * step) - 0.001));
                }
            }
            checkPlainBorders(system);
        }


      
        private int calcRemainedLayers(int maxnum, List<MolData> system)
        {
            double remained = this.calcRemainedLayers_Float(maxnum, system);
            int calcLayers = (int)remained;
            if (calcLayers < remained)
                calcLayers++;
            return calcLayers;
        }

        private double calcRemainedLayers_Float(int maxnum, List<MolData> system)
        {
            int atomsPerLayer = (int)((xSize / step + 1) * (ySize / step + 1));
            return (maxnum - system.Count) / (double)atomsPerLayer;
        }

        /// <summary>
        /// Calculation of height of a dry polymer
        /// </summary>
        private double calcPolyHeight(List<MolData> system)
        {
            double height = 0.0;
            foreach (var c in system)
            {
                double h = c.ZCoord + zSize / 2.0;
                if (h > height)
                    height = h;
            }
            return height;
        }

        private double findInterCord()
        {
            if (percentage == 0.0 || percentage == 1.0)
            {
                return 0.0;
            }

            if (location == 1)
            {
                return (percentage - 0.5) * zSize;
            }

            return (0.5 - percentage) * zSize;
        }


        private List<double[]> moveMolecule(List<double[]> moveMol, double xCoord, double yCoord, double zCoord)
        {
           return moveMolecule(new double[] { 0.0, 0.0, 0.0 }, moveMol, xCoord, yCoord, zCoord);
        }

        /// <summary>
        /// Molecule shift along the Cartesian axes
        /// </summary>
        private List<double[]> moveMolecule(double[] rotattions, List<double[]> moveMol, double xCoord, double yCoord, double zCoord)
        {
        
            double xDiff = moveMol.Min(x => x[0]) - xCoord;
            double yDiff = moveMol.Min(x => x[1]) - yCoord;
            double zDiff = moveMol.Min(x => x[2]) - zCoord;

       

            foreach (var c in moveMol)
            {
                c[0] -= xDiff;
                c[1] -= yDiff;
                c[2] -= zDiff;
            }

            return moveMol;
        }

        /// <summary>
        /// Create one wall layer with obstacles
        /// </summary>
        private double[,] createWallWithObstacles(double zCoord, List<MolData> system)
        {
            double[,] wall = createWall();

            var wallHoles = new List<double[]>();

            for (int i = 0; i < system.Count; i++)
            {
                if (Math.Abs(system[i].ZCoord - zCoord) < step / 2.0)
                    wallHoles.Add(new double[] { system[i].XCoord, system[i].YCoord });
            }
            for (int i = 0; i < wall.GetLength(0); i++)
            {
                foreach (var c in wallHoles)
                {
                    if (Math.Abs(wall[i, 0] - c[0]) < step && Math.Abs(wall[i, 1] - c[1]) < step)
                    {
                        wall[i, 0] = 0.0;
                        wall[i, 1] = 0.0;
                    }
                }
            }
            return wall;
        }

        /// <summary>
        /// Create one wall layer
        /// </summary>
        private double[,] createWall()
        {
            int xAmount = (int)(xSize / step) + 1;
            int yAmount = (int)(ySize / step) + 1;
            double[,] wall = new double[xAmount * yAmount, 2];
            for (int i = 0; i < xAmount; i++)
            {
                for (int j = 0; j < yAmount; ++j)
                {
                    if (j == 0 && i == 0)
                    {
                        wall[0, 0] = -xSize / 2.0;
                        wall[0, 1] = -ySize / 2.0;
                        continue;
                    }
                    else
                    {
                        wall[j + i * xAmount, 0] = Math.Round(wall[0, 0] + j * step, 3);
                        wall[j + i * xAmount, 1] = Math.Round(wall[0, 1] + i * step, 3);
                    }
                }
            }
            return wall;
        }
        #endregion
    }
}
