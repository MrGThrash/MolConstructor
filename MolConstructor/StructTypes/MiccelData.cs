using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MiccelPicker
{
    /// <summary>
    /// Класс с полями для атомов, входящих в состав мицеллы
    /// </summary>
    public class MiccelData
    {
        public int index;
        public double atomType;
        public double xCoord;
        public double yCoord;
        public double zCoord;
        public List<int> Bonds;

        public MiccelData(double _atomType, int _index,
                          double _xCoord, double _yCoord,
                          double _zCoord)
        {
            atomType = _atomType;
            index = _index;
            xCoord = _xCoord;
            yCoord = _yCoord;
            zCoord = _zCoord;
        }

        // Проверка на существование атома с индексом в списке
        public static bool Exists(List<int> list ,int index)
        {
            foreach (int i in list)
            {
                if (i == index)
                    return true;
            }

            return false;

        }
        // Проверка на существование атома с индексом в списке
        public static bool Exists(List<MiccelData> list, int index)
        {
            foreach (var c in list)
            {
                if (c.index == index)
                    return true;
            }

            return false;

        }
        // Определение длины молекулы блок-сополимера
        public static int[] GetChainLength(double[,] data)
        {
            int lengthA = 0;
            int lengthB = 0;

            double type = data[0, 3];

            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 3].Equals(type))
                    lengthA++;
                else break;
            }

            for (int i = lengthA; i < data.GetLength(0); i++)
            {
                    if (!data[i, 3].Equals(type))
                        lengthB++;

                    else break;
            }

            return new int[] { lengthA, lengthB };

        }

        // Реплицирование связей 
        public static List<int[]> MultiplyBonds(int molAmount, int maxnum, List<int[]> initBonds)
        {
            var totalBonds = new List<int[]>();
            int beadsInMol = 0;
            foreach (var c in initBonds)
            {
                if (c[0] > beadsInMol)
                    beadsInMol = c[0];
                if (c[1] > beadsInMol)
                    beadsInMol = c[1];
            }

            for (int i = 0; i < molAmount; i++)
            {

                foreach (var c in initBonds)
                {
                    if (((c[0] + i * beadsInMol) <= maxnum) && ((c[1] + i * beadsInMol) <= maxnum))
                        totalBonds.Add(new int[] { c[0] + i * beadsInMol,
                                               c[1] + i * beadsInMol});
                    else
                        break;
                }
            }


            return totalBonds;
        }

        public static List<int[]> ProcessBonds(double[,] data,List<int[]> initbonds, List<MiccelData> atoms)
        {
            List<int[]> finalBonds = new List<int[]>();

            // Adding nanoparticle bonds
            int maxNum= 0;
            int listIndex =0;
            int nanoCount = 0;

            foreach (var c in atoms)
            {
                if (c.index > maxNum && c.atomType == 1.02)
                {
                    maxNum = c.index;
                    listIndex = atoms.FindIndex(x => x.index == c.index);
                    nanoCount++;
                }
            }    
            
            int minNum = atoms.Min(x => x.index);

            foreach (var c in initbonds)
            {
                if ((c[0] <= maxNum && c[1] <= maxNum) &&
                    (c[0] >= minNum && c[1] >= minNum))
                {
                    finalBonds.Add(c);
                }
            }

            foreach (var c in finalBonds)
            {
                c[0] -= (minNum - 1);
                c[1] -= (minNum - 1);
            }


            int nPartAmount = 0;

            for (int i=0; i< data.GetLongLength(0); i++)
            {
                if (data[i, 3].Equals(1.00) || data[i, 3].Equals(1.01))
                {
                    nPartAmount = i+1;
                    break;
                }
            }

            int chainLength = 2;

            // Adding polymer crown
            foreach (var c in initbonds)
            {
                if (c[0] == nPartAmount)
                {
                    int[] curBond = c;
                    int bondInd = initbonds.FindIndex(x => x == c);
                    for (int i = bondInd + 1; i < initbonds.Count; i++)
                    {
                        if (initbonds[i][0] == initbonds[i - 1][1])
                            chainLength++;
                        else
                            break;
                    }
                    break;
                }
            }

            int chainAmount = (atoms.Count - nanoCount) / chainLength;

            for (int i=0; i<chainAmount; i++)
            {
                for(int j=1; j<chainLength;j++)
                {
                    finalBonds.Add(new int[] { maxNum + j + i * chainLength, maxNum + j + 1 + i * chainLength });
                }
            }

            return finalBonds;
        }

        public static List<MiccelData> ShiftAll(bool hasWalls, bool onlyPolymer, int density, 
                                                int xSize, int ySize, int zSize,
                                                double xShift, double yShift, double zShift, 
                                                double[,] data)
        {

            var atoms = new List<MiccelData>();

            // 0 Этап - смещение (если есть) всех частиц по координате
            for (int i = 0; i < data.GetLength(0); i++)
            {
                data[i, 0] += xShift;
                if (data[i, 0] <= -xSize / 2.0)
                    data[i, 0] = data[i,0] + xSize;
                if (data[i, 0] >= xSize / 2.0)
                    data[i, 0] = data[i,0] - xSize;
            }

            for (int i = 0; i < data.GetLength(0); i++)
            {
                data[i, 1] += yShift;
                if (data[i, 1] <= -ySize / 2.0)
                    data[i, 1] = data[i, 1]  + ySize;
                if (data[i, 1] >= ySize / 2.0)
                    data[i, 1] = data[i, 1] - ySize;
            }

            // 1 Этап - смещение по оси z с учетом наличия/отсутствия стенок
            double coef = 0;

            if (hasWalls)
            {
                double step = 1.0 / Math.Pow((double)density, 1.0 / 3.0);
                coef = 0.8 * step;
            }

            //1.5 Этап - определение типа координат
            bool onlyZPositive = true;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 2] < 0.0)
                {
                    onlyZPositive = false;
                    break;
                }
            }

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    if (onlyZPositive)
                    {
                        data[i, 2] += zShift;
                        if (data[i, 2] <= coef)
                            data[i, 2] = data[i, 2] + zSize - 2 * coef;
                        if (data[i, 2] >= zSize - coef)
                            data[i, 2] = data[i, 2] - zSize + 2 * coef;
                    }
                    else
                    {
                        data[i, 2] += (zShift - zSize / 2.0);
                        if (data[i, 2] <= -zSize / 2.0 + coef)
                            data[i, 2] = data[i, 2] + zSize - 2 * coef;
                        if (data[i, 2] >= zSize / 2.0 - coef)
                            data[i, 2] = data[i, 2] - zSize + 2 * coef;
                    }
                }

                int maxcount = xSize * ySize * ySize * density;

                for (int i = 0; i < maxcount; i++)
            {
                if (onlyPolymer)
                {
                    if (data[i, 3].Equals(1.00) || data[i, 3].Equals(1.01) || data[i, 3].Equals(1.04))
                        atoms.Add(new MiccelData(data[i, 3], i + 1, data[i, 0], data[i, 1], data[i, 2]));
                }
                else
                {
                    atoms.Add(new MiccelData(data[i, 3], i + 1, data[i, 0], data[i, 1], data[i, 2]));
                }
            }

            if (hasWalls)
            {
                FilmFormer fFormer = new FilmFormer(xSize, ySize, zSize, density, data);

                fFormer.AddWalls(atoms);
            }

            return atoms;

        }

        // Задача ленивого Димы
        public static List<MiccelData> CutGovno(double xZero, double yZero, double zZero,
                                                double radius, double[,] data, List<int[]> bonds)
        {
            var totalIndexes = new List<int>();
            var atoms = new List<MiccelData>();

            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 3].Equals(1.00) || data[i, 3].Equals(1.02))
                {
                    if (Math.Sqrt(Math.Pow(xZero - data[i, 0], 2) +
                                 Math.Pow(yZero - data[i, 1], 2) +
                                 Math.Pow(zZero - data[i, 2], 2)) <= radius)
                        totalIndexes.Add(i + 1);
                }
            }
            // Достраивапние до полноценных цепочек

            int totalAtoms = totalIndexes.Count;
            bool areEq = false;

            do
            {
            for (int i=0; i< totalAtoms;i++)
            {
            foreach (var c in bonds)
            {
                if (c[0] == totalIndexes[i])
                {
                    if (!totalIndexes.Exists(x => x == c[1]))
                        totalIndexes.Add(c[1]);
                }
                if (c[1] == totalIndexes[i])
                {
                    if (!totalIndexes.Exists(x => x == c[0]))
                        totalIndexes.Add(c[0]);
                }
            }
                }
            if (totalAtoms < totalIndexes.Count)
            {
                totalAtoms = totalIndexes.Count;
            }
            else
                areEq = true;
            } while(areEq == false);

            totalIndexes.Sort();

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < totalIndexes.Count; j++)
                {
                    if ((i + 1) == totalIndexes[j])
                    {
                        atoms.Add(new MiccelData(data[i, 3], i + 1, data[i, 0], data[i, 1], data[i, 2]));
                    }
                }
            }
            return atoms;
        }
       

        // Основной метод по вырезанию мицеллы
        public static List<MiccelData> CutMiccel(double xZero, double yZero, double zZero, double xSize, double ySize, 
                                                 double zSize, double radius, bool onlyCore, double coreType,
                                                 double[,] data, List<int[]> bonds)
        {
            var addIndexes = new List<int>();
            var totalIndexes = new List<int>();
            var atoms = new List<MiccelData>();

            // 0 Этап - центрирование точки выбора и присвоение всем связей
            var LocData = ShiftAll(false, true, 3, (int)xSize, (int)ySize, (int)zSize, -xZero, -yZero, zSize / 2.0 - zZero, data);

            if (bonds.Count != 0)
            {
                foreach (var c in LocData)
                {
                    c.Bonds = new List<int>();
                    var locBonds = bonds.Where(x => x[0] == c.index || x[1] == c.index).ToList();
                    
                    foreach (var v in locBonds)
                    {
                        if (v[0] != c.index)
                        {
                            c.Bonds.Add(v[0]);
                        }
                        else
                        {
                            c.Bonds.Add(v[1]);
                        }
                    }
                }
            }

            // 1 Этап - выделение атомов ядра в пределах радиуса 
            for (int i = 0; i < LocData.Count; i++)
            {
                    if (Math.Sqrt(Math.Pow(xZero - data[i, 0], 2) +
                                  Math.Pow(yZero - data[i, 1], 2) +
                                  Math.Pow(zZero - data[i, 2], 2)) <= radius)
                    {
                        atoms.Add(LocData[i]);
                    }
              }


            // 2 Этап - добавление атомов до полноценных цепочек

            int counter;

            do
            {
                counter = atoms.Count;

                for (int i = 0; i < counter; i++)
                {
                    // Достройка по связям 

                    foreach (var c in atoms[i].Bonds)
                    {
                        if (atoms.FindIndex(x => x.index == c) == -1)
                        {
                            var addAtoms = LocData.Where(x => x.index == c).ToList();

                            if (onlyCore)
                            {
                                addAtoms = addAtoms.Where(x => x.atomType.Equals(coreType)).ToList();
                            }

                            atoms.AddRange(addAtoms);
                        }
                    }
                    // После того как бид выбран, его связи уничтожаются
                    atoms[i].Bonds.Clear();
                }

            } while (atoms.Count > counter);

            return atoms;
 
        }

        // Основной метод по вырезанию мицеллы
        public static List<MiccelData> CutMiccelaLAMMPS(double xZero, double yZero, double zZero,
                                                        double radius, double[,] data)
        {

            var atoms = new List<MiccelData>();

             for (int i = 0; i < data.GetLength(0); i++)
                {
                 if( data[i,1].Equals(1.00) || data[i,1].Equals(1.01))
                 {
                     if (Math.Sqrt(Math.Pow(xZero - data[i, 2], 2) +
                                  Math.Pow(yZero - data[i, 3], 2) +
                                  Math.Pow(zZero - data[i, 4], 2)) <= radius)
                     {
                         atoms.Add(new MiccelData(data[i, 1], (int)data[i, 0], data[i, 2], data[i, 3], data[i, 4]));
                    }
                 }
                }
            

            return atoms;

        }

        /// <summary>
        /// Связывание мицелл в ядре
        /// </summary>
        public static List<int[]> BoundMiccelCore(int aLength, int bLength, int amount,
                                                   double radius, double[,] miccel)
        {
            var bonds = new List<int[]>();
            var bondsInMiccel = new List<int[]>(); 
            var intermedBonds = new List<int[]>();

            int maxBonds = 4;

            // Запись первоначальных связей
            for (int i = 0; i < miccel.GetLength(0); i++)
            {
                int first = i + 1;
                int second = i + 2;

                if (((i + 1) % (aLength + bLength)) != 0)
                {
                    intermedBonds.Add(new int[] { first, second });
                }
            }


            int allChains = FilmFormer.GetAllChainsInFilm(aLength + bLength, miccel);


            // Идем по каждой цепи
            for (int k = 0; k < allChains; k++)
            {
                // Идем по каждому биду внутри цепи k, принадлежащей ядру
                for (int i = bLength - 1; i >= 0; i--)
                {

                    int beadbonds = 0;

                    int index = k * (aLength + bLength) + aLength + i;

                    int first = index + 1;


                    for (int j = 0; j < intermedBonds.Count; j++)
                    {
                        if (intermedBonds[j][0] == first || intermedBonds[j][1] == first)
                            beadbonds++;
                    }

                    if (beadbonds > 3)
                    {
                        continue;
                    }

                    // Смотрим остальные биды внутри ядра , не принадлежащие цепи k
                    for (int j = 0; j < miccel.GetLength(0); j++)
                    {
                        if (beadbonds > 3)
                            break;

                        if (j < k * (aLength + bLength) || j >= (k + 1) * (aLength + bLength))
                        {
                            if (miccel[j, 3] == 1.01)
                            {
                              
                                double distance = Math.Sqrt(Math.Pow(miccel[index, 0] - miccel[j, 0], 2) + 
                                                            Math.Pow(miccel[index, 1] - miccel[j, 1], 2) +
                                                            Math.Pow(miccel[index, 2] - miccel[j, 2], 2));

                                if (distance <= radius)
                                {
                                    int second = j + 1;

                                    int secondBonds = 0;

                                    bool hasElement = false;

                                    for (int l = 0; l < intermedBonds.Count; l++)
                                    {

                                        if (intermedBonds[l][0] == second ||
                                            intermedBonds[l][1] == second)
                                        {
                                            secondBonds++;
                                        }
                                    }

                                    //for (int l = 0; l < interBondsPerAtom.Count; l++)
                                    //{
                                    //    if (interBondsPerAtom[l][0] == j + 1)
                                    //    {
                                    //        hasElement = true;
                                    //        break;
                                    //    }
                                    //}

                                    if (!hasElement  && secondBonds < maxBonds)
                                    {
                                        intermedBonds.Add(new int[] { first, second });
                                        beadbonds++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Сортировка
            for (int i = 0; i < miccel.GetLength(0); i++)
            {
                foreach (var c in intermedBonds)
                {
                    if (c[0] == i + 1)
                        bondsInMiccel.Add(c);
                }
            }

            // Запись
            for (int a = 0; a < amount; a++)
            {
                for (int i = 0; i < bondsInMiccel.Count; i++)
                {
                    bonds.Add(new int[] { bondsInMiccel[i][0] + a * miccel.GetLength(0),
                                          bondsInMiccel[i][1] + a * miccel.GetLength(0)});
                }
            }

            return bonds;
        }
        public static List<int[]> CombineBonds(bool onLayer, double[,] layer, List<int[]> filmbonds,
                                                List<int[]> layerBonds)
        {
            var bonds = new List<int[]>();

            int length = 0;

            if (onLayer)
            {
                length = layer.GetLength(0);

                foreach (var c in layerBonds)
                {
                    bonds.Add(c);
                }

            }

            foreach (var c in filmbonds)
            {
                bonds.Add(new int[] { c[0] + length, c[1] + length });
            }


           // int filmAtoms = bonds[bonds.Count - 1][1];
           
            return bonds;
        }



        public static void ConvertDataToLAMMPS(double xSize, double ySize, double zSize, List<MiccelData> system)
        {
        
            foreach (var c in system)
            {
                c.xCoord = Math.Round(c.xCoord/ xSize, 4);
                c.yCoord = Math.Round(c.yCoord/ ySize, 4);
                c.zCoord = Math.Round(c.zCoord / zSize, 4);

                if (c.atomType.Equals(1.01))
                    c.atomType = 2;
                if (c.atomType.Equals(1.02))
                    c.atomType = 3;
                if (c.atomType.Equals(1.03))
                    c.atomType = 4;
                if (c.atomType.Equals(1.07))
                    c.atomType = 8;
                if (c.atomType.Equals(1.08))
                    c.atomType = 9;
            }
        }


        #region outdated Code

           //// Запись доп. связей
           // for (int i = 0; i < miccel.GetLength(0); i++)
           // {
           //     if (miccel[i, 3] == 1.01)
           //     {
           //         int first = i + 1;

           //         double radA = Math.Sqrt(Math.Pow(miccel[i, 0], 2) +
           //                         Math.Pow(miccel[i, 1], 2) +
           //                         Math.Pow(miccel[i, 2], 2));

           //         for (int j = 0; j < miccel.GetLength(0); j++)
           //         {
           //             if (miccel[j, 3] == 1.01)
           //             {
           //                 int bondsPerAtom = 0;

           //                 double radB = Math.Sqrt(Math.Pow(miccel[j, 0], 2) +
           //                         Math.Pow(miccel[j, 1], 2) +
           //                         Math.Pow(miccel[j, 2], 2));

           //                 double distance = Math.Sqrt(Math.Abs(Math.Pow(radA, 2) - Math.Pow(radB, 2)));

           //                 if (j != i && distance <= radius)
           //             {
           //                 int second = j + 1;

           //                 int secondBonds = 0;
           //                 int count = intermedBonds.Count;
           //                 bool hasElement = false;

           //                 var interBondsPerAtom = new List<int[]>();

           //                 for (int k = 0; k < count; k++)
           //                 {
           //                     if (intermedBonds[k][1] == i + 1)
           //                     {
           //                         bondsPerAtom++;
           //                         interBondsPerAtom.Add(new int[] { intermedBonds[k][0], intermedBonds[k][1] });
           //                     }

           //                     if (intermedBonds[k][0] == i + 1)
           //                     {
           //                         bondsPerAtom++;
           //                     }

           //                     if (intermedBonds[k][0] == j + 1 ||
           //                         intermedBonds[k][1] == j + 1)
           //                     {
           //                         secondBonds++;
           //                     }
           //                 }

           //                 for (int k = 0; k < interBondsPerAtom.Count; k++)
           //                 {
           //                     if (interBondsPerAtom[k][0] == j + 1)
           //                     {
           //                         hasElement = true;
           //                         break;
           //                     }
           //                 }

           //                 if (!hasElement && bondsPerAtom < maxBonds && secondBonds < maxBonds)
           //                 {
           //                     intermedBonds.Add(new int[] { first, second });
           //                 }
           //             }
           //         }
           //         }
           //     }
           // }


        #endregion
    }
}
