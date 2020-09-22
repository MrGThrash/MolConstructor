// Decompiled with JetBrains decompiler
// Type: MolConstructor.MolData
// Assembly: MolConstructor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 182210B7-9AC1-4A6B-9028-73DFC9EAD149
// Assembly location: I:\Программы\Arborescents\Arborescents\bin\Debug\MolConstructor.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace MolConstructor
{
    public class MolData
    {
        public int Index;
        public int MolIndex;
        public double AtomType;
        public double Charge;
        public double XCoord;
        public double YCoord;
        public double ZCoord;
        public List<int> Bonds;

        public MolData(double atomType, int index, double xCoord, double yCoord, double zCoord)
        {
            this.AtomType = atomType;
            this.Index = index;
            this.XCoord = xCoord;
            this.YCoord = yCoord;
            this.ZCoord = zCoord;
        }

        public MolData(double atomType, int index, double xCoord, double yCoord, double zCoord, bool withBonds)
        {
            this.AtomType = atomType;
            this.Index = index;
            this.XCoord = xCoord;
            this.YCoord = yCoord;
            this.ZCoord = zCoord;
            if (!withBonds)
                return;
            this.Bonds = new List<int>();
        }

        public MolData(double atomType, int index, int molIndex, double xCoord, double yCoord, double zCoord)
        {
            this.AtomType = atomType;
            this.MolIndex = molIndex;
            this.Index = index;
            this.XCoord = xCoord;
            this.YCoord = yCoord;
            this.ZCoord = zCoord;
        }

        public MolData(double atomType, int index, int molIndex, double charge, double xCoord, double yCoord, double zCoord)
        {
            this.AtomType = atomType;
            this.MolIndex = molIndex;
            this.Index = index;
            this.Charge = charge;
            this.XCoord = xCoord;
            this.YCoord = yCoord;
            this.ZCoord = zCoord;
        }

        // Old method for the work with xyzr files
        public static void RecolorInitially(List<double[]> data)
        {
            double recType = data.Exists((x => x[3].Equals(1.02))) &
                             data.Exists((x => x[3].Equals(1.03))) &
                             data.Exists((x => x[3].Equals(1.06))) ? 1.07 : 1.03;

            for (int i = 0; i < data.Count - 8; ++i)
            {
                if (data[i][3].Equals(1.06))
                    data[i][3] = recType;
                if (data[i][3].Equals(1.04))
                    data[i][3] = 1.05;
            }
            if (data[data.Count - 1][3] != data.Max<double[]>((Func<double[], double>)(x => x[2])))
                return;
            for (int i = data.Count - 8; i < data.Count; ++i)
            {
                if (data[i][3].Equals(1.06))
                    data[i][3] = recType;
            }
        }

        // 'Randomizer'
        public static void RecolorRandomly(int num, int startind, List<MolData> mol)
        {
            Random rand = new Random();

            foreach (var c in mol)
            {
                c.AtomType = 1.00;
            }

            int counter = 0;

            do
            {
                int ind = rand.Next(startind, mol.Count);
                if (mol[ind-1].AtomType != 1.01)
                {
                    mol[ind - 1].AtomType = 1.01;
                    counter++;
                }
            } while (counter < num) ;
        }

        // Проверка на существование атома с индексом в списке
        public static bool Exists(List<int> list, int index)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == index)
                    return true;
            }
            return false;
        }
        // Проверка на существование атома с индексом в списке
        public static bool Exists(List<MolData> list, int index)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Index == index)
                    return true;
            }
            return false;
        }

        public static int CalcTypes(List<MolData> list)
        {
            List<double> atomValues = list.Select((x => x.AtomType)).Distinct().ToList();
            list.Max((x => x.AtomType));
            if (atomValues.Contains(1.02) && atomValues.Max().Equals(1.03))
                return 4;
            return FileWorker.AtomTypes.FirstOrDefault((x => x.Value.Equals(atomValues.Max()))).Key;
        }

        public static int CalcBonds(List<int[]> bonds, List<MolData> list)
        {
            int types = 1;
            foreach (int[] bond in bonds)
            {
                int bondType = FileWorker.GetBondType(new double[2]
                {
          list[bond[0] - 1].AtomType,
          list[bond[1] - 1].AtomType
                });
                if (bondType > types)
                    types = bondType;
            }
            return types;
        }

        //restored from decompiler. Needs revision
        public static List<int[]> CreateAngles(double type, List<MolData> list, List<int[]> bonds)
        {
            List<int[]> source = new List<int[]>();
            foreach (var c in list.Where(x =>
            {
                if (x.AtomType != 1.0 && x.AtomType != 1.01 && x.AtomType != 1.04)
                    return x.AtomType == 1.05;
                return true;
            }).ToList())
            {
                if (c.AtomType.Equals(type))
                {
                    c.Bonds = new List<int>();
                    foreach (int[] bond in bonds)
                    {
                        if (bond[0].Equals(c.Index))
                            c.Bonds.Add(bond[1]);
                        if (bond[1].Equals(c.Index))
                            c.Bonds.Add(bond[0]);
                    }
                    c.Bonds = c.Bonds.Distinct().ToList();
                    if (c.Bonds.Count == 2)
                    {
                        source.Add(new int[3]
                        {
              c.Bonds[0],
              c.Index,
              c.Bonds[1]
                        });
                    }
                    if (c.Bonds.Count > 2)
                    {
                        for (int i = 0; i < c.Bonds.Count; i++)
                        {
                            for (int j = 0; j < c.Bonds.Count; j++)
                            {
                                if (source.Where(x =>
                                {
                                    if (x[0] == c.Bonds[j])
                                        return x[2] == c.Bonds[i];
                                    return false;
                                }).ToList<int[]>().Count == 0)
                                    source.Add(new int[3]
                                    {
                    c.Bonds[i],
                    c.Index,
                    c.Bonds[j]
                                    });
                            }
                        }
                    }
                }
            }

            for (int i =source.Count -1 ; i >= 0; i--)
            {
                if (source[i][0] == source[i][2])
                {
                    source.Remove(source[i]);
                }
            }

            return source;
        }

        public static int CalcAngles(List<int[]> angles, List<MolData> list)
        {
            int types = 0;
            foreach (int[] angle in angles)
            {
                int angleType = FileWorker.GetAngleType(new double[3]
                {
          list[angle[0] - 1].AtomType,
          list[angle[1] - 1].AtomType,
          list[angle[2] - 1].AtomType
                });
                if (angleType > types)
                {
                    types = angleType;
                }
            }
            return types;
        }

        public static double[] GetCenterPoint(double[] sizes, List<double[]> data)
        {
            double[] numArray = new double[3]
            {
        sizes[0] / 2.0,
        sizes[1] / 2.0,
        sizes[2] / 2.0
            };
            for (int i = 0; i <= 2; i++)
            {
                if (data.Any((x => x[i] < -1.0)))
                    numArray[i] = 0.0;
            }
            return numArray;
        }

        public static List<int[]> MultiplyBonds(int molAmount, List<int[]> initBonds)
        {
            if ((uint)molAmount > 0U)
            {
                return MultiplyBonds(0, molAmount, initBonds, null);
            }
            return new List<int[]>();
        }

        public static List<int[]> MultiplyBonds(int matrixChainLength, int molAmount, List<int[]> initBonds, List<MolData> system)
        {
            var totalBonds = new List<int[]>();
            int beadsInMol = 0;

            foreach (var bond in initBonds)
            {
                if (bond[0] > beadsInMol)
                    beadsInMol = bond[0];
                if (bond[1] > beadsInMol)
                    beadsInMol = bond[1];
            }
            for (int i = 0; i < molAmount; ++i)
            {
                foreach (var bond in initBonds)
                {
                    if (bond.Length == 2)
                        totalBonds.Add(new int[2]
                        {
              bond[0] + i * beadsInMol,
              bond[1] + i * beadsInMol
                        });
                    else
                        totalBonds.Add(new int[3]
                        {
              bond[0] + i * beadsInMol,
              bond[1] + i * beadsInMol,
              bond[2]
                        });
                }
            }
            if (system != null && matrixChainLength !=0)
            {
                int molBeads = beadsInMol * molAmount;
                int matrixBeads = 0;
                for (int i = molBeads; i < system.Count; i++)
                {
                    if (system[i].AtomType.Equals(1.03))
                    {
                        matrixBeads = i - molBeads;
                        break;
                    }
                }
                if (matrixBeads != 1)
                {
                    int matrixChainAmount = matrixBeads / matrixChainLength;
                    for (int i = 0; i < matrixChainAmount; i++)
                    {
                        for (int j = 1; j < matrixChainLength; j++)
                        {
                            if (totalBonds[0].Length == 2)
                                totalBonds.Add(new int[] { j + i*matrixChainLength + molBeads,
                                                   j + 1 + i*matrixChainLength + molBeads});
                            else
                            {
                                totalBonds.Add(new int[] { j + i*matrixChainLength + molBeads,
                                                   j + 1 + i*matrixChainLength + molBeads,2});
                            }
                        }
                    }
                }
            }
            return totalBonds;
        }

        public static List<int[]> MultiplyAngles(int molAmount, List<int[]> initAngles)
        {
            return MultiplyAngles(0, molAmount, 0.0, initAngles, null);
        }

        public static List<int[]> MultiplyAngles(int matrixChainLength, int molAmount, double molType, List<int[]> initAngles, List<MolData> system)
        {
            List<int[]> totalAngles = new List<int[]>();
            int beadsInMol = 0;
            foreach (var angle in initAngles)
            {
                if (angle[0] > beadsInMol)
                    beadsInMol = angle[0];
                if (angle[1] > beadsInMol)
                    beadsInMol = angle[1];
                if (angle[2] > beadsInMol)
                    beadsInMol = angle[2];
            }

            // Добавление связей молекул/мицелл
            for (int i = 0; i < molAmount; ++i)
            {
                foreach (int[] initAngle in initAngles)
                    totalAngles.Add(new int[4]
                    {
            initAngle[0] + i * beadsInMol,
            initAngle[1] + i * beadsInMol,
            initAngle[2] + i * beadsInMol,
            initAngle[3]
                    });
            }
            if (system != null)
            {
                int molBeads = beadsInMol * molAmount;
                int matrixBeads = 0;
                for (int index = molBeads; index < system.Count; ++index)
                {
                    if (system[index].AtomType.Equals(1.02) || system[index].AtomType.Equals(1.03))
                    {
                        matrixBeads = index - molBeads + 1;
                        break;
                    }
                }
                if (matrixBeads != 1)
                {
                    int matrixChainAmount = matrixBeads / matrixChainLength;

                    // Добавление связей цепей матрикса
                    for (int i = 0; i < matrixChainAmount; i++)
                    {
                        for (int j = 1; j < matrixChainLength - 1; j++)
                        {
                            int atomType = FileWorker.AtomTypes.FirstOrDefault(x => x.Value.Equals(molType)).Key;
                            totalAngles.Add(new int[] { j + i*matrixChainLength + molBeads,
                                                        j + 1 + i*matrixChainLength + molBeads,
                                                        j+ 2 + i*matrixChainLength + molBeads,
                                                        FileWorker.AngleTypes.FirstOrDefault(x => x.Value.Equals(
                                                        new int[]{atomType,atomType,atomType})).Key});
                        }
                    }
                }
            }
            return totalAngles;
        }

        // Прореживание списка (для больших ящиков)
        public static List<MolData> ReduceSystem(List<MolData> data, double percentage)
        {
            int polCount = data.Where(x => x.AtomType.Equals(1.00) ||
                                     x.AtomType.Equals(1.01) ||
                                     x.AtomType.Equals(1.04) ||
                                     x.AtomType.Equals(1.05)).ToList().Count;
            int solvACount = data.Where(x => x.AtomType.Equals(1.02)).ToList().Count;
            int solvBCount = data.Where(x => x.AtomType.Equals(1.03)).ToList().Count;

            data.RemoveRange(solvACount + polCount, (int)((double)solvBCount * (1.0 - percentage)));
            data.RemoveRange(polCount, (int)((double)solvACount * (1.0 - percentage)));
            return data;
        }

        public static List<MolData> ConvertToMolData(List<double[]> data, bool withBonds, List<int[]> bonds)
        {
            var system = new List<MolData>();

            for (int i = 0; i < data.Count; i++)
            {
                var newAtom = new MolData(data[i][3], i + 1, Convert.ToInt32(data[i][4]), data[i][0], data[i][1], data[i][2]);
                system.Add(newAtom);
            }

            if(withBonds)
            {
                foreach (var c in system)
                {
                    var beadBonds = new List<int>();
                    foreach (var p in bonds)
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
            }

            return system;
        }

        public static List<MolData> ShiftAll(bool hasWalls, int wallsType, bool onlyPolymer, double density, double[] sizes, double[] shifts, double[] centerPoint, List<double[]> data)
        {
            var system = new List<MolData>();
            foreach (var c in data)
            {
                for (int i = 0; i <= 2; i++)
                {
                    if (c[3] != 1.08 && c[3] != 1.09)
                    {
                        c[i] += shifts[i];
                        if (c[i] <= centerPoint[i] - sizes[i] / 2.0)
                            c[i] += sizes[i];
                        if (c[i] >= centerPoint[i] + sizes[i] / 2.0)
                            c[i] -= sizes[i];
                    }
                }
            }
            double maxNum = sizes[0] * sizes[1] * sizes[2] * density;
            if (data.Count < Math.Abs(maxNum))
            {
                maxNum = data.Count;
            }
            for (int i = 0; (double)i < maxNum; i++)
            {
                var newAtom = new MolData(data[i][3], i + 1, Convert.ToInt32(data[i][4]), data[i][0], data[i][1], data[i][2]);
                system.Add(newAtom);
            }
            if (onlyPolymer)
            {
                system = system.Where(x => x.AtomType.Equals(1.00) ||
                                           x.AtomType.Equals(1.01) ||
                                           x.AtomType.Equals(1.04) ||
                                           x.AtomType.Equals(1.09)).ToList();
            }

            if (hasWalls)
            {
                StructFormer fFormer = new StructFormer(hasWalls, wallsType, sizes[0], sizes[1], sizes[2], density, centerPoint, data);
                if (wallsType == 1 && system.Where(x => x.AtomType.Equals(1.09)).ToList().Count == 0)
                {
                    fFormer.AddWalls(system);
                }
            }
            return system;
        }

        public static void ShiftAll(bool hasWalls, bool onlyPolymer, double density, double[] sizes, double[] shifts, double[] centerPoint, List<MolData> data)
        {
            double step = 0.0;
            if (hasWalls)
                step = 0.8 * (1.0 / Math.Pow(density, 1.0 / 3.0));
            foreach (var c in data)
            {
                if (c.AtomType != 1.08 || c.AtomType != 1.09)
                {
                    c.XCoord += shifts[0];
                    c.YCoord += shifts[1];
                    c.ZCoord += shifts[2];
                    if (c.XCoord <= centerPoint[0] - sizes[0] / 2.0)
                        c.XCoord += sizes[0];
                    if (c.YCoord <= centerPoint[1] - sizes[1] / 2.0)
                        c.YCoord += sizes[1];
                    if (c.XCoord >= centerPoint[0] + sizes[0] / 2.0)
                        c.XCoord -= sizes[0];
                    if (c.YCoord >= centerPoint[1] + sizes[1] / 2.0)
                        c.XCoord -= sizes[1];
                    if (c.ZCoord <= centerPoint[2] - sizes[2] / 2.0 + step)
                        c.ZCoord += sizes[2] - 2.0 * step;
                    if (c.ZCoord >= centerPoint[2] + sizes[2] / 2.0 - step)
                        c.ZCoord -= sizes[2] - 2.0 * step;
                }
            }
        }

        public static void ShiftAllDouble(int density, double[] sizes, double[] shifts, double[] centerPoint, List<double[]> data)
        {
            int maxNum = (int)(sizes[0] * sizes[1] * sizes[2] * density);
            for (int i = 0; i < Math.Min(data.Count, maxNum); i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (data[i][3] != 1.08 || data[i][3] != 1.09)
                    {
                        data[i][j] += shifts[j];
                        if (data[i][j] <= centerPoint[j] - sizes[j] / 2.0)
                            data[i][j] += sizes[j];
                        if (data[i][j] >= centerPoint[j] + sizes[j] / 2.0)
                            data[i][j] -= sizes[j];
                    }
                }
            }
        }

        public List<double[]> ConvertToListDouble(List<MolData> data)
        {
            var doubleData = new List<double[]>();
            foreach (var c in data)
            {
                double[] line = new double[5]
                {
          c.XCoord,
          c.YCoord,
          c.ZCoord,
          c.AtomType,
          c.MolIndex
                };
                doubleData.Add(line);
            }
            return doubleData;
        }


    }
}
