
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MolConstructor
{
    public class FileWorker
    {
        public static Dictionary<int, double> AtomTypes =
            new Dictionary<int, double>()
        {
          {1, 1.00},
          {2, 1.01},
          {3, 1.03},
          {4, 1.02},
          {5, 1.04},
          {6, 1.05},
          {7, 1.06},
          {8, 1.07},
          {9, 1.08},
          {10,1.09},
          {11,1.10}
        };

        public static Dictionary<string, double> AtomTypes_MOL =
    new Dictionary<string, double>()
{
         {"C",1.00},
         {"O",1.01},
         {"S",1.02},
         {"I",1.03},
         {"N",1.04},
         {"P",1.05},
         {"H",1.06},
         {"CL",1.07},
         {"ZN",1.08},
         {"AU",1.09}
    };

        public static Dictionary<int, double[]> BondTypes = new Dictionary<int, double[]>()
            {
             {1,new double[2]{1,1}},
             {2,new double[2]{1,1.01}},
             {3,new double[2]{1.01,1.01}},
             {4,new double[2]{1.02,1.02}},
             {5,new double[2]{1.04,1.04}},
             {6,new double[2]{1.00,1.04}},
             {7,new double[2]{1.04,1.01}},
             {8,new double[2]{1.05,1.05}},
             {9,new double[2]{1.06,1.06}},
             {10,new double[2]{1.05,1.06}},
            };

        public static Dictionary<int, int[]> AngleTypes = new Dictionary<int, int[]>()
            {
             {1,new int[]{1,1,1}},
             {2,new int[]{1,1,2}},
             {3,new int[]{1,2,2}},
             {4,new int[]{2,2,2}},
             {5,new int[]{2,1,2}},
             {6,new int[]{1,2,1}},
             {7,new int[]{1,1,5}},
             {8,new int[]{1,5,5}},
             {9,new int[]{5,5,5}},
             {10,new int[]{5,1,5}},
             {11,new int[]{1,5,1}},
             };

        public static int GetBondType(double[] pair)
        {
            if (pair[0] == 1.0 && pair[1] == 1.0)
                return 1;
            if (pair[0] == 1.0 && pair[1] == 1.01 || pair[0] == 1.01 && pair[1] == 1.0)
                return 2;
            if (pair[0] == 1.01 && pair[1] == 1.01)
                return 3;
            if (pair[0] == 1.0 && pair[1] == 1.04 || pair[0] == 1.04 && pair[1] == 1.0)
                return 4;
            if (pair[0] == 1.01 && pair[1] == 1.04 || pair[0] == 1.04 && pair[1] == 1.01)
                return 5;
            if (pair[0] == 1.04 && pair[1] == 1.04)
                return 6;
            if (pair[0] == 1.0 && pair[1] == 1.05 || pair[0] == 1.05 && pair[1] == 1.0)
                return 7;
            if (pair[0] == 1.01 && pair[1] == 1.05 || pair[0] == 1.05 && pair[1] == 1.01)
                return 8;
            if (pair[0] == 1.04 && pair[1] == 1.05 || pair[0] == 1.05 && pair[1] == 1.04)
                return 9;
            if (pair[0] == 1.05 && pair[1] == 1.05)
                return 10;
            if (pair[0] == 1.0 && pair[1] == 1.06 || pair[0] == 1.06 && pair[1] == 1.0)
                return 11;
            if (pair[0] == 1.01 && pair[1] == 1.06 || pair[0] == 1.06 && pair[1] == 1.01)
                return 12;
            if (pair[0] == 1.04 && pair[1] == 1.06 || pair[0] == 1.06 && pair[1] == 1.04)
                return 13;
            if (pair[0] == 1.05 && pair[1] == 1.06 || pair[0] == 1.06 && pair[1] == 1.05)
                return 14;
            return pair[0] == 1.06 && pair[1] == 1.06 ? 15 : 0;
        }

        public static int GetAngleType(double[] triplet)
        {
            if (triplet[0] == 1.0 && triplet[1] == 1.0 && triplet[2] == 1.0 || triplet[0] == 1.0 && triplet[1] == 1.0 && triplet[2] == 1.01 || triplet[0] == 1.01 && triplet[1] == 1.0 && triplet[2] == 1.0)
                return 1;
            if (triplet[0] == 1.01 && triplet[1] == 1.01 && triplet[2] == 1.01 || triplet[0] == 1.0 && triplet[1] == 1.01 && triplet[2] == 1.01 || triplet[0] == 1.01 && triplet[1] == 1.01 && triplet[2] == 1.0)
                return 2;
            return triplet[0] == 1.01 && triplet[1] == 1.0 && triplet[2] == 1.01 || triplet[0] == 1.0 && triplet[1] == 1.01 && triplet[2] == 1.0 ? 3 : 4;
        }

        public static void Save_XYZ(string fileName, bool inBox, double[] sizes, List<MolData> atoms)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                for (int i = 0; i < atoms.Count; i++)
                {
                    if (!atoms[i].AtomType.Equals(1.08) && !atoms[i].AtomType.Equals(1.09))
                    {
                        string oneLineXyz = CreateOneLine_XYZ(atoms[i].XCoord, atoms[i].YCoord, atoms[i].ZCoord, atoms[i].AtomType);
                        sw.WriteLine(oneLineXyz);
                    }
                }
                if (inBox)
                    AddBoxCoords(sizes[0], sizes[1], sizes[2], sw);
                sw.Flush();
            }
        }

        public static void Save_MOL(string fileName, List<int[]> bonds, List<MolData> atoms)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                foreach (MolData atom in atoms)
                {
                    string type = AtomTypes_MOL.FirstOrDefault(x => x.Value == atom.AtomType).Key;
                    string oneLineMol = CreateOneLine_MOL(Condition.Coord, atom.Index, atom.Index + 1, atom.XCoord, atom.YCoord, atom.ZCoord, type);
                    streamWriter.WriteLine(oneLineMol);
                }
                if ((uint)bonds.Count > 0U)
                {
                    foreach (var bond in bonds)
                    {
                        string oneLineMol = CreateOneLine_MOL(Condition.Connection, bond[0], bond[1], 0.0, 0.0, 0.0, "");
                        streamWriter.WriteLine(oneLineMol);
                    }
                }
                streamWriter.WriteLine("END");
                streamWriter.Flush();
            }
        }

        public static void SaveBonds_DAT(string fileName, List<int[]> bonds)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                for (int i = 0; i < bonds.Count; i++)
                    streamWriter.WriteLine(string.Format("{0,8}{1,8}", bonds[i][0], bonds[i][1]));
            }
        }

        public static void SaveAngles_DAT(string fileName, List<int[]> angles)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                for (int i = 0; i < angles.Count; i++)
                    streamWriter.WriteLine(string.Format("{0,8}{1,8}{2,8}", angles[i][0], angles[i][1], angles[i][2]));
            }
        }

        public static void Save_DAT(string fileName, double density, int molLength, int allChains, double[] sizes, List<int[]> bonds, List<int[]> angles, List<MolData> film)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                int atomCount = (int)(sizes[0] * sizes[1] * sizes[2] * density);
                if (density == 6)
                {
                    atomCount /= 2;
                }
                streamWriter.WriteLine(string.Format("{0,8}{1,8}{2,8}{3,8}", atomCount, bonds.Count, allChains, density));
                streamWriter.WriteLine(string.Format("{0,20}{1,20}{2,20}", ToFortranFormat(sizes[0], "0.000000000000"), "0.232177136833D-52", "0.258027476660D-53"));
                streamWriter.WriteLine(string.Format("{0,20}{1,20}{2,20}","0.232177136833D-52", ToFortranFormat(sizes[1], "0.000000000000"), "-0.111689668373D-52"));
                streamWriter.WriteLine(string.Format("{0,20}{1,20}{2,20}", "0.387041214990D-53", "-0.167534502560D-52", ToFortranFormat(sizes[2], "0.000000000000")));
                bool xyPositive = true;
                bool onlyZpositive = true;

                foreach (var c in film)
                {
                    if (c.XCoord < 0.0)
                    {
                        xyPositive = false;
                        break;
                    }
                }
                foreach (var c in film)
                {
                    if (c.ZCoord < 0.0)
                    {
                        onlyZpositive = false;
                        break;
                    }
                }
                for (int i = 0; i < film.Count; i++)
                {
                    if (!film[i].AtomType.Equals(1.08) && !film[i].AtomType.Equals(1.09))
                    {
                        int type = 1;
                        if (film[i].AtomType.Equals(1.01))
                            type = 2;
                        if (film[i].AtomType.Equals(1.02))
                            type = 3;
                        if (film[i].AtomType.Equals(1.04))
                            type = 4;
                        if (film[i].AtomType.Equals(1.05))
                            type = 5;
                        if (film[i].AtomType.Equals(1.07))
                            type = 7;
                        if (film[i].AtomType.Equals(1.03) || film[i].AtomType.Equals(1.06))
                            type = 8;
                        double xCoord = film[i].XCoord;
                        double yCoord = film[i].YCoord;
                        double zCoord = film[i].ZCoord;
                        if (xyPositive)
                        {
                            xCoord -= sizes[0] / 2.0;
                            yCoord -= sizes[1] / 2.0;
                        }
                        if (onlyZpositive)
                            zCoord -= sizes[2] / 2.0;
                        streamWriter.WriteLine(CreateOneLine_DAT(Condition.Coord, 0, 0, type, xCoord, yCoord, zCoord));
                    }
                }
                for (int i = 0; i < film.Count; i++)
                {
                    if (!film[i].AtomType.Equals(1.08) && !film[i].AtomType.Equals(1.09))
                        streamWriter.WriteLine(CreateOneLine_DAT(Condition.Velocity, 0, 0, 0, 0.0, 0.0, 0.0));
                }
                if (bonds.Count == 0)
                {
                    if ((uint)allChains > 0U)
                    {
                        streamWriter.WriteLine(string.Format("{0,7}{1,11}", "bonds:", (allChains * (molLength - 1))));
                        for (int i = 0; i < allChains * molLength; i++)
                        {
                            if ((uint)((i + 1) % molLength) > 0U)
                            {
                                string oneLineDat = CreateOneLine_DAT(Condition.Connection, i + 1, i + 2, 0, 0.0, 0.0, 0.0);
                                streamWriter.WriteLine(oneLineDat);
                            }
                        }
                    }
                    else
                    {
                        streamWriter.WriteLine(string.Format("{0,7}{1,11}", "bonds:", "0"));
                    }
                }
                else
                {
                    streamWriter.WriteLine(string.Format("{0,7}{1,11}", "bonds:", bonds.Count));
                    foreach (var bond in bonds)
                    {
                        string oneLineDat = CreateOneLine_DAT(Condition.Connection, bond[0], bond[1], 0, 0.0, 0.0, 0.0);
                        streamWriter.WriteLine(oneLineDat);
                    }
                }
                streamWriter.WriteLine(string.Format("{0,8}{1,10}", "angles:", angles.Count));
                foreach (var angle in angles)
                {
                    string oneLineDat = CreateOneLine_DAT(Condition.Angle, angle[0], angle[1], angle[2], 0.0, 0.0, 0.0);
                    streamWriter.WriteLine(oneLineDat);
                }
                streamWriter.Flush();
            }
        }

        public static void Save_Conf(string fileName, double[] sizes, double density, int atomTypes, int bondTypes, int angleTypes, List<int[]> bonds, List<int[]> angles, List<MolData> structure)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                int atomCount = (int)(density * sizes[0] * sizes[1] * sizes[2]);
                if (structure.Count < Math.Abs(atomCount) || density == 0.0)
                    atomCount = structure.Count;
                streamWriter.WriteLine("#Model");
                streamWriter.WriteLine();
                streamWriter.WriteLine(string.Format("{0,10}{1,10}", atomCount, "atoms"));
                streamWriter.WriteLine(string.Format("{0,10}{1,10}",bonds.Count, "bonds"));
                streamWriter.WriteLine(string.Format("{0,10}{1,11}", angles.Count, "angles"));
                streamWriter.WriteLine();
                streamWriter.WriteLine(string.Format("{0,10}{1,15}", atomTypes, "atom types"));
                streamWriter.WriteLine(string.Format("{0,10}{1,15}", bondTypes, "bond types"));
                streamWriter.WriteLine(string.Format("{0,10}{1,16}", angleTypes, "angle types"));
                streamWriter.WriteLine();
                streamWriter.WriteLine(string.Format("{0,4}{1,6}{2,4}{3,4}", 0, Math.Round(sizes[0],2), "xlo", "xhi"));
                streamWriter.WriteLine(string.Format("{0,4}{1,6}{2,4}{3,4}", 0, Math.Round(sizes[1], 2), "ylo", "yhi"));
                streamWriter.WriteLine(string.Format("{0,4}{1,6}{2,4}{3,4}", 0, Math.Round(sizes[2], 2), "zlo", "zhi"));
                streamWriter.WriteLine();
                streamWriter.WriteLine("Masses");
                streamWriter.WriteLine();
                for (int i = 1; i <= atomTypes; i++)
                    streamWriter.WriteLine(string.Format("{0,10}{1,10}", i, "1.00"));
                streamWriter.WriteLine();
                streamWriter.WriteLine("Atoms");
                streamWriter.WriteLine();

                bool xyNegative = structure.Any(x => x.XCoord < 0.0);
                bool zNegative = structure.Any(x => x.ZCoord < 0.0);

                for (int i = 0; i < atomCount; i++)
                {
                    double xCoord = structure[i].XCoord;
                    double yCoord = structure[i].YCoord;
                    double zCoord = structure[i].ZCoord;
                    if (xyNegative)
                    {
                        xCoord += sizes[0] / 2.0;
                        yCoord += sizes[1] / 2.0;
                    }
                    if (zNegative)
                    {
                        zCoord += sizes[2] / 2.0;
                    }
                    int aType = AtomTypes.FirstOrDefault(x => x.Value == structure[i].AtomType).Key;
                    if (aType == 3 && atomTypes == 2 || aType == 4 && atomTypes == 3)
                    {
                        aType = atomTypes;
                    }
                    if (structure[i].AtomType > 2.0)
                    {
                        aType = (int)structure[i].AtomType;
                    }
                    if (structure[i].MolIndex == 0)
                    {
                        structure[i].MolIndex = aType;
                    }
                    string format = "{0,10}{1,10}{2,10}{3,10}{4,10}{5,10}";
                    streamWriter.WriteLine(string.Format(format, (i + 1), structure[i].MolIndex, aType, 
                                           xCoord.ToString("0.000", CultureInfo.InvariantCulture), 
                                           yCoord.ToString("0.000", CultureInfo.InvariantCulture), 
                                           zCoord.ToString("0.000", CultureInfo.InvariantCulture)));
                }
                if ((uint)bonds.Count > 0U)
                {
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("Bonds");
                    streamWriter.WriteLine();
                    for (int i = 0; i < bonds.Count; i++)
                    {
                        int bondType;
                        if (bonds[i].Length == 3)
                        {
                            bondType = bonds[i][2];
                        }
                        else
                            bondType = GetBondType(new double[2]
                            {
                             structure[bonds[i][0] - 1].AtomType,
                             structure[bonds[i][1] - 1].AtomType
                            });
                        if (bondType == 0)
                        {
                            bondType = 15;
                        }
                        streamWriter.WriteLine(string.Format("{0,10}{1,10}{2,10}{3,10}", (i + 1), bondType, bonds[i][0], bonds[i][1]));
                    }
                }
                if ((uint)angles.Count > 0U)
                {
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("Angles");
                    streamWriter.WriteLine();
                }
                for (int i = 0; i < angles.Count; i++)
                {
                    int angleType;
                    if (angles[i].Length == 4)
                        angleType = angles[i][3];
                    else
                        angleType = GetAngleType(new double[3]
                        {
                         structure[angles[i][0] - 1].AtomType,
                         structure[angles[i][1] - 1].AtomType,
                         structure[angles[i][2] - 1].AtomType
                        });
                    streamWriter.WriteLine("{0,10}{1,10}{2,10}{3,10}{4,10}", (i + 1), angleType, angles[i][0], angles[i][1], angles[i][2]);
                }
            }
        }

        public static void SaveLammpstrj(bool append, string FileName, int ItemNum, double[] sizes, double density, List<MolData> strct)
        {
            bool xyPositive = true;
            bool onlyZpositive = true;

            foreach (MolData molData in strct)
            {
                if (molData.ZCoord < 0.0)
                {
                    onlyZpositive = false;
                    break;
                }
            }
            foreach (MolData molData in strct)
            {
                if (molData.XCoord < 0.0)
                {
                    xyPositive = false;
                    break;
                }
            }
            int atomCount = (int)(density * sizes[0] * sizes[1] * sizes[2]);
            if (strct.Count < atomCount || density == 0)
                atomCount = strct.Count;
            string directoryName = Path.GetDirectoryName(FileName);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            var filemode = FileMode.Append;
            if (!append)
            {
                filemode = FileMode.Create;
            }

            using (FileStream fileStream = new FileStream(FileName, filemode, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine("ITEM: TIMESTEP");
                    streamWriter.WriteLine(ItemNum);
                    streamWriter.WriteLine("ITEM: NUMBER OF ATOMS");
                    streamWriter.WriteLine(atomCount);
                    streamWriter.WriteLine("ITEM: BOX BOUNDS pp pp pp");
                    streamWriter.WriteLine(string.Format("{0,1}{1,6}", 0, Math.Round(sizes[0],2)));
                    streamWriter.WriteLine(string.Format("{0,1}{1,6}", 0, Math.Round(sizes[1],2)));
                    streamWriter.WriteLine(string.Format("{0,1}{1,6}", 0, Math.Round(sizes[2],2)));
                    streamWriter.WriteLine("ITEM: ATOMS id type xs ys zs");

                    for (int i = 0; i < atomCount; i++)
                    {
                        double xCoord = Math.Round((strct[i].XCoord + sizes[0] / 2.0) / sizes[0], 4);
                        double yCoord = Math.Round((strct[i].YCoord + sizes[1] / 2.0) / sizes[1], 4);
                        double zCoord = Math.Round((strct[i].ZCoord + sizes[2] / 2.0) / sizes[2], 4);
                        if (onlyZpositive)
                            zCoord = Math.Round(strct[i].ZCoord / sizes[2], 4);
                        if (xyPositive)
                        {
                            xCoord = Math.Round(strct[i].XCoord / sizes[0], 4);
                            yCoord = Math.Round(strct[i].YCoord / sizes[1], 4);
                        }
                        int type = AtomTypes.FirstOrDefault((x => x.Value == strct[i].AtomType)).Key;
                        if (strct[i].AtomType > 2.0)
                        {
                            type = (int)strct[i].AtomType;
                        }
                        string oneLineLammptrj = CreateOneLine_Lammptrj(i + 1, xCoord, yCoord, zCoord, type);
                        streamWriter.WriteLine(oneLineLammptrj);
                    }
                    streamWriter.Flush();
                }
            }
        }

        private static string CreateOneLine_XYZ(double x, double y, double z, double type)
        {
            return String.Format(
                   "{0,9}{1,9}{2,9}{3,9}",
                   x.ToString("0.000", CultureInfo.InvariantCulture),
                   y.ToString("0.000", CultureInfo.InvariantCulture),
                   z.ToString("0.000", CultureInfo.InvariantCulture),
                   type.ToString("0.000", CultureInfo.InvariantCulture)
               );
        }

        private static void AddBoxCoords(double xSize, double ySize, double zSize, StreamWriter sw)
        {
            string lineXY0 = CreateOneLine_XYZ(-xSize / 2.0, -ySize / 2.0, 0, 1.080);
            string lineXYZ = CreateOneLine_XYZ(-xSize / 2.0, -ySize / 2.0, zSize, 1.080);
            string lineXy0 = CreateOneLine_XYZ(-xSize / 2.0, ySize / 2.0, 0, 1.080);
            string lineXyZ = CreateOneLine_XYZ(-xSize / 2.0, ySize / 2.0, zSize, 1.080);
            string linexY0 = CreateOneLine_XYZ(xSize / 2.0, -ySize / 2.0, 0, 1.080);
            string linexYZ = CreateOneLine_XYZ(xSize / 2.0, -ySize / 2.0, zSize, 1.080);
            string linexy0 = CreateOneLine_XYZ(xSize / 2.0, ySize / 2.0, 0, 1.080);
            string linexyZ = CreateOneLine_XYZ(xSize / 2.0, ySize / 2.0, zSize, 1.080);
            sw.WriteLine(lineXY0);
            sw.WriteLine(lineXYZ);
            sw.WriteLine(lineXy0);
            sw.WriteLine(lineXyZ);
            sw.WriteLine(linexY0);
            sw.WriteLine(linexYZ);
            sw.WriteLine(linexy0);
            sw.WriteLine(linexyZ);
        }

        private static string CreateOneLine_MOL(FileWorker.Condition cond, int number, int otherNumber, double x, double y, double z, string type)
        {
            string str1 = null;
            string str2 = number.ToString();
            switch (cond)
            {
                case Condition.Coord:
                    if (number > 99999)
                        str2 = "*****";
                    str1 = string.Format("{0,-6}{1,5}{2,3}{3,12}{4,12}{5,8}{6,8}", "HETATM", str2, type, number, 
                                x.ToString("0.000", CultureInfo.InvariantCulture), 
                                y.ToString("0.000", CultureInfo.InvariantCulture), 
                                z.ToString("0.000", CultureInfo.InvariantCulture));
                    break;
                case Condition.Connection:
                    str1 = string.Format("{0,-6}{1,6}{2,6}", "CONECT", str2, otherNumber.ToString());
                    break;
            }
            return str1;
        }

        private static string CreateOneLine_DAT(Condition cond, int number, int otherNumber, int type, double x, double y, double z)
        {
            string str = null;
            switch (cond)
            {
                case Condition.Coord:
                    str = string.Format("{0,4}{1,14}{2,14}{3,14}", type, 
                                        ToFortranFormat(x, "0.00000"), 
                                        ToFortranFormat(y, "0.00000"), 
                                        ToFortranFormat(z, "0.00000"));
                    break;
                case Condition.Velocity:
                    str = string.Format("{0,23}{0,23}{0,23}", "0.100000000000000D+00");
                    break;
                case Condition.Connection:
                    str = string.Format("{0,8}{1,8}", number, otherNumber);
                    break;
                case Condition.Angle:
                    str = string.Format("{0,8}{1,8}{2,8}", number, otherNumber, type);
                    break;
            }
            return str;
        }

        private static string CreateOneLine_Lammptrj(int number, double x, double y, double z, int type)
        {
            return string.Format("{0,6}{1,4}{2,9}{3,9}{4,9}", 
                                 number, type, 
                                 x.ToString("0.0000", CultureInfo.InvariantCulture), 
                                 y.ToString("0.0000", CultureInfo.InvariantCulture), 
                                 z.ToString("0.0000", CultureInfo.InvariantCulture));
        }

        private static string ToFortranFormat(double a, string format)
        {
            int order = 0;
            do
            {
                if ((uint)(int)a % 10U > 0U)
                {
                    a /= 10.0;
                    order++;
                }
                else if ((int)a % 10 == 0 && (uint)(int)a > 0U)
                {
                    a /= 10.0;
                    order++;
                }
                else
                    break;
            }
            while ((int)a % 10 != 0 && (uint)(int)a > 0U);
            return string.Format("{0}D+{1}", a.ToString(format,CultureInfo.InvariantCulture), order.ToString("00"));
        }

        public static List<int[]> LoadBonds(string bondspath, int format)
        {
            var bonds = new List<int[]>();
            // oldformat from
            if (format == 2)
            {
                var lines = new string[1];
                try
                {
                    lines = File.ReadAllLines(bondspath + "/bonds.dat");
                }
                catch
                {
                    lines = File.ReadAllLines(bondspath);
                }
                for (int i = 0; i < lines.Length; i++)
                {
                    var sList = readLine(lines[i]);
                    int[] row = new int[2];
                    for (int j = 0; j < 2; j++)
                        row[j] = Convert.ToInt32(sList.ElementAt(j));
                    if ((uint)row[0] > 0U && (uint)row[1] > 0U)
                        bonds.Add(row);
                }
                if (bonds[bonds.Count - 1][0] == 0 && bonds[bonds.Count - 1][1] == 0)
                {
                    int count = bonds.Count;
                    for (int i = count - 1; i >= count - 13; i--)
                        bonds.RemoveAt(i);
                }
            }
            else if (format == 1)
            {
            }
            //configlammps
            else
            {
                var files = Directory.GetFiles(bondspath, "*.txt").OrderBy(f => f).ToList();
          
                double xSize = 0.0, ySize = 0.0, zSize = 0.0;         
                foreach (var fl in files)
                {
                    try
                    {
                        LoadConfLines(out xSize, out ySize, out zSize, fl, new List<double[]>(), bonds, new List<int[]>());
                        if (bonds.Count > 1)
                            break;
                    }
                    catch
                    {
                    }
                }
            }
            return bonds;
        }

        public static List<int[]> LoadAngles(string[] lines)
        {
            var angles = new List<int[]>();
            for (int i = 0; i < lines.Length; i++)
            {
                var sList = readLine(lines[i]);
                int[] row = new int[3];
                for (int j = 0; j <= 2; j++)
                {
                    row[j] = Convert.ToInt32(sList.ElementAt(j));
                }
                if (row[0] != 0 && row[1] != 0)
                    angles.Add(row);
            }
            if (angles[angles.Count - 1][0] == 0 && angles[angles.Count - 1][1] == 0)
            {
                int amount = angles.Count;
                for (int i = amount - 1; i >= amount - 13; i--)
                {
                    angles.RemoveAt(i);
                }
            }
            return angles;
        }

        public static List<double[]> LoadXyzrLines(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var data = new List<double[]>();
            for (int i = 0; i < lines.Length; i++)
            {
                var sList = readLine(lines[i]);
                var row = new double[sList.Count + 1];
                for (int j = 0; j < sList.Count; j++)
                {
                    row[j] = replaceValue(sList.ElementAt(j));
                }
                row[row.Length - 1] = i;
                data.Add(row);
            }
            return data;
        }

        public static List<double[]> LoadMolLines(string fileName, List<int[]> bonds)
        {
            var lines = File.ReadAllLines(fileName);
            bonds.Clear();
            var data = new List<double[]>();
            for (int i = 0; i < lines.Length; i++)
            {
               var sList = readLine(lines[i]);
                if (sList.Count == 11)
                {
                    for (int index2 = 0; index2 < Convert.ToInt32(sList[0]); ++index2)
                    {
                        double[] row = new double[5];
                        var datList = readLine(lines[i + index2 + 1]);
                        for (int j = 0; j < row.Length - 2; j++)
                            row[j] = replaceValue(datList[j]);
                        if (datList[3] == "C")
                            row[3] = 1.0;
                        if (datList[3] == "O")
                            row[3] = 1.01;
                        if (datList[3] == "S")
                            row[3] = 1.02;
                        if (datList[3] == "W")
                            row[3] = 1.03;
                        if (datList[3] == "N")
                            row[3] = 1.04;
                        if (datList[3] == "P")
                            row[3] = 1.05;
                        data.Add(row);
                    }
                    for (int j = 0; j < Convert.ToInt32(sList[1]); ++j)
                    {
                        var datList = readLine(lines[i + j + data.Count + 1]);
                        if (datList.Count == 6)
                            bonds.Add(new int[2]
                            {
                             Convert.ToInt32(datList[0]),
                             Convert.ToInt32(datList[1])
                            });
                        else if (datList[0].Length == 6)
                            bonds.Add(new int[2]
                            {
                             Convert.ToInt32(datList[0].Substring(0, 3)),
                             Convert.ToInt32(datList[0].Substring(3, 3))
                            });
                        else if (datList[0].Length == 5)
                            bonds.Add(new int[2]
                            {
                             Convert.ToInt32(datList[0].Substring(0, 2)),
                             Convert.ToInt32(datList[0].Substring(2, 3))
                            });
                        else
                            bonds.Add(new int[2]
                            {
                             Convert.ToInt32(datList[0].Substring(0, 1)),
                             Convert.ToInt32(datList[0].Substring(1, 3))
                            });
                    }
                    break;
                }
            }
            return data;
        }

        public static void LoadConfLines(out double xSize, out double ySize, out double zSize, string fileName, List<double[]> data, List<int[]> bonds, List<int[]> angles)
        {
            xSize = 0;
            ySize = 0;
            zSize = 0;
            var lines = File.ReadAllLines(fileName);
            var sizes = new List<double[]>();
            bonds.Clear();
            angles.Clear();
            int molcount = 0;
            int bondscount = 0;
            int anglescount = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var sList = readLine(lines[i]);
                if (sList.Count == 2)
                {
                    if (sList[1].Equals("atoms"))
                        molcount = Convert.ToInt32(sList[0]);
                    if (sList[1].Equals(nameof(bonds)))
                        bondscount = Convert.ToInt32(sList[0]);
                    if (sList[1].Equals(nameof(angles)))
                        anglescount = Convert.ToInt32(sList[0]);
                }
                if (sList.Count == 4)
                {
                    if (sList[2] == "xlo")
                    {
                        sizes.Add(new double[2] {replaceValue(sList[0]), replaceValue(sList[1])});
                        xSize = sizes[0][1] - sizes[0][0];
                    }
                    if (sList[2] == "ylo")
                    {
                        sizes.Add(new double[2] { replaceValue(sList[0]), replaceValue(sList[1]) });
                        ySize = sizes[1][1] - sizes[1][0];
                    }
                    if (sList[2] == "zlo")
                    {
                        sizes.Add(new double[2] { replaceValue(sList[0]), replaceValue(sList[1]) });
                        zSize = sizes[2][1] - sizes[2][0];
                    }
                }
                if (sList.Count == 1)
                {
                    int counter = 0;

                    // Read atoms
                    if (sList[0].Equals("Atoms"))
                    {
                        do
                        {
                            int ind = Math.Min(i + 1 + counter, lines.Length - 1);
                            sList = readLine(lines[ind]);
                            counter++;

                            if (sList.Count >= 6)
                            {
                                var row = new double[5];
                                int startLine = 3;

                                if (sList[2].Length == 7)
                                {
                                    startLine = 2;
                                    row[3] = AtomTypes[Convert.ToInt32(sList[1])];

                                    if (sList.Count == 7)
                                    {
                                        row[4] = replaceValue(sList[6]);
                                    }
                                    else
                                    {
                                        row[4] = replaceValue(sList[5]);
                                    }
                                }
                                else
                                {
                                    row[3] = AtomTypes[Convert.ToInt32(sList[2])];
                                    row[4] = replaceValue(sList[1]);
                                    if (sList.Count == 7) { startLine = 4; }
                                }

                                for (int j = 0; j < 3; j++)
                                {
                                    row[j] = replaceValue(sList[startLine + j]) - sizes[j][0];
                                }
                                data.Add(row);
                            }
                        }
                        while (data.Count < molcount);
                    }

                    // Read bonds and angles
                    if (bondscount != 0 || (uint)anglescount > 0U)
                    {
                        if (sList[0].Equals("Bonds"))
                        {
                            counter = 0;
                            do
                            {
                                int ind = Math.Min(i + 1 + counter, lines.Length - 1);
                                sList = readLine(lines[ind]);
                                counter++;
                                if (data.Count >= 100000)
                                {
                                    if (sList.Count == 2)
                                        bonds.Add(new int[3]
                                        {
                                        Convert.ToInt32(sList[1].Substring(1, 6)),
                                        Convert.ToInt32(sList[1].Substring(7, 6)),
                                        Convert.ToInt32(sList[1].Substring(0, 1))
                                        });
                                    if (sList.Count == 3)
                                    {
                                        if (sList[1].Length == 1)
                                            bonds.Add(new int[3]
                                            {
                                             Convert.ToInt32(sList[2].Substring(0, 5)),
                                             Convert.ToInt32(sList[2].Substring(5, 6)),
                                             Convert.ToInt32(sList[1])
                                            });
                                        else
                                            bonds.Add(new int[3]
                                            {
                                             Convert.ToInt32(sList[1].Substring(1, 6)),
                                             Convert.ToInt32(sList[2]),
                                             Convert.ToInt32(sList[1].Substring(0, 1))
                                            });
                                    }
                                    if (sList.Count == 4)
                                        bonds.Add(new int[3]
                                        {
                                         Convert.ToInt32(sList[2]),
                                         Convert.ToInt32(sList[3]),
                                         Convert.ToInt32(sList[1])
                                        });
                                }
                                else if (sList.Count == 4)
                                    bonds.Add(new int[3]
                                    {
                                     Convert.ToInt32(sList[2]),
                                     Convert.ToInt32(sList[3]),
                                     Convert.ToInt32(sList[1])
                                    });
                            }
                            while (bonds.Count < bondscount);
                        }
                        if (sList[0].Equals("Angles"))
                        {
                            counter = 0;
                            do
                            {
                                var ind = Math.Min(i + 1 + counter, lines.Length - 1);
                                sList = readLine(lines[ind]);
                                counter++;
                                if (sList.Count == 5 && angles != null)
                                    angles.Add(new int[4]
                                    {
                                     Convert.ToInt32(sList[2]),
                                     Convert.ToInt32(sList[3]),
                                     Convert.ToInt32(sList[4]),
                                     Convert.ToInt32(sList[1])
                                    });
                                if (ind == lines.Length - 1)
                                {
                                    break;
                                }
                            }
                            while (angles.Count < anglescount);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        //public static List<double[]> LoadLammpstrjLines(string fileName, int snapNum,out double[] sizes)
        //{
        //    var snapnum = 0;
        //    return LoadLammpstrjLines(fileName,)
        //}


        public static List<double[]> LoadLammpstrjLines(string fileName, out int snapNum, out double[] sizes)
        {
            var lines = readLammpstrjFile(fileName);
            sizes = new double[3];
            var data = new List<double[]>();
            int molcount = 0;

            snapNum = 0;
            
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "ITEM: TIMESTEP")
                {
                    snapNum = Convert.ToInt32(lines[i + 1]);
                }
                    if (lines[i] == "ITEM: NUMBER OF ATOMS")
                    molcount = Convert.ToInt32(lines[i + 1]);
                if (lines[i] == "ITEM: BOX BOUNDS pp pp pp" || lines[i] == "ITEM: BOX BOUNDS ff pp pp" || lines[i] == "ITEM: BOX BOUNDS pp ff pp" || lines[i] == "ITEM: BOX BOUNDS pp pp ff")
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var sList = readLine(lines[i + j + 1]);
                        if (sList[0].Length == 22 || sList[0].Length == 23)
                        {
                            double maxSize = replaceValue(sList[1].Substring(0, 4)) * Math.Pow(10.0, (double)Convert.ToInt32(sList[1].Substring(20)));
                            double minSize = replaceValue(sList[0].Substring(0, 4)) * Math.Pow(10.0, (double)Convert.ToInt32(sList[0].Substring(20)));
                            sizes[j] = maxSize - minSize;
                        }
                        else
                            sizes[j] = replaceValue(sList[1]) - replaceValue(sList[0]);
                    }
                }
                if (lines[i] == "ITEM: ATOMS id type xs ys zs")
                {
                    List<double[]> atomList = new List<double[]>();
                    for (int j = 0; j < molcount; j++)
                    {
                        var sList = readLine(lines[i + j + 1]);
                        double[] row = new double[sList.Count];
                        for (int k = 0; k < sList.Count; k++)
                        {
                            switch (k)
                            {
                                case 0:
                                    row[k] = (double)Convert.ToInt32(sList[0]);
                                    break;
                                case 1:
                                    row[k] = AtomTypes[Convert.ToInt32(sList[1])];
                                    break;
                                default:
                                    row[k] = Math.Round(replaceValue(sList[k]) * sizes[k - 2], 3);
                                    if (row[k] < 0.0)
                                        row[k] = 0.0;
                                    break;
                            }
                        }
                        atomList.Add(row);
                    }
                    atomList = atomList.OrderBy(x => x[0]).ToList();
                    foreach (var c in atomList)
                    {
                        try
                        {
                            data.Add(new double[] { c[2], c[3], c[4], c[1], c[0] - 1 });
                        }
                        catch
                        {
                            var error = c[0];

                            throw new Exception("ошибка в элементе " + error.ToString());
                        }
                    }
                }
            }
            return data;
        }

        public static List<double[]> LoadMol2Lines(string fileName, List<int[]> bonds)
        {
            var lines = File.ReadAllLines(fileName);
            var data = new List<double[]>();
            int molcount = 0;
            int bondscount = 0;
            //int anglescount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "structure1")
                {
                    var sList = readLine(lines[i + 1]);
                    molcount = Convert.ToInt32(sList[0]); 
                    bondscount = Convert.ToInt32(sList[1]);
                }
                if (lines[i] == "@<TRIPOS>ATOM" )
                {
                    List<double[]> atomList = new List<double[]>();
                    for (int j = 0; j < molcount; j++)
                    {
                        var sList = readLine(lines[i + j + 1]);
                        double[] row = new double[sList.Count];
                        for (int k = 0; k < 5; k++)
                        {
                            switch (k)
                            {
                                case 0:
                                    {
                                        row[k] = (double)Convert.ToInt32(sList[0]);
                                        break;
                                    }
                                case 1:
                                    {
                                        row[k] = AtomTypes_MOL[sList[1].ToString()];
                                        break;
                                    }
                                default:
                                    {
                                        row[k] = Math.Round(replaceValue(sList[k]), 3);
                                        if (row[k] < 0.0)
                                            row[k] = 0.0;
                                        break;
                                    }
                            }
                        }
                        atomList.Add(row);
                    }

                    atomList = atomList.OrderBy(x => x[0]).ToList();
                    foreach (var c in atomList)
                    {
                        try
                        {
                            data.Add(new double[] { c[2], c[3], c[4], c[1], c[0] - 1 });
                        }
                        catch (Exception ex)
                        {
                            var error = c[0];

                            throw new Exception("ошибка в элементе " + error.ToString()+". Причина ошибки:\n"+
                                                ex.ToString());
                        }
                    }

                }

                if (lines[i] == "@<TRIPOS>BOND")
                {
                    int counter = 0;
                    do
                    {
                        int ind = Math.Min(i + 1 + counter, lines.Length - 1);
                        var sList = readLine(lines[ind]);
                        counter++;
                        if (sList.Count == 4)
                        {
                            bonds.Add(new int[3]
                            {
                                     Convert.ToInt32(sList[1]),
                                     Convert.ToInt32(sList[2]),
                                     Convert.ToInt32(sList[3])
                            });
                        }
                    } while (bonds.Count < bondscount);
                }
                //    if (sList[0].Equals("Angles"))
                //    {
                //        counter = 0;
                //        do
                //        {
                //            var ind = Math.Min(i + 1 + counter, lines.Length - 1);
                //            sList = readLine(lines[ind]);
                //            counter++;
                //            if (sList.Count == 5 && angles != null)
                //                angles.Add(new int[4]
                //                {
                //                     Convert.ToInt32(sList[2]),
                //                     Convert.ToInt32(sList[3]),
                //                     Convert.ToInt32(sList[4]),
                //                     Convert.ToInt32(sList[1])
                //                });
                //            if (ind == lines.Length - 1)
                //            {
                //                break;
                //            }
                //        }
                //        while (angles.Count < anglescount);
                //    }

                //}
            }        
            return data;
        }

        private static string[] readLammpstrjFile(string fileName)
        {
            using (StreamReader file = new StreamReader(fileName))
            {
                var readLines = new List<string>();
                int snapshotcounts = 0;
                do
                {
                    var tempLine = file.ReadLine();
                    if (tempLine == "ITEM: TIMESTEP")
                    { snapshotcounts++; }
                    if (tempLine == null) break;
                    readLines.Add(tempLine);
                } while (snapshotcounts < 2);
                if (readLines[readLines.Count - 1] == " ")
                {
                    readLines.RemoveAt(readLines.Count - 1);
                }
                var lines = new string[readLines.Count];
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = readLines[i];
                }
                return lines;
            }
        }

        private static List<string> readLine(string line)
        {
            string[] strs = line.Split(new char[] { ' ', '\t'});
            var sList = new List<string>();
            foreach (var ss in strs)
            {
                if (ss.Trim() != "")
                    sList.Add(ss);
            }
            return sList;
        }

        private static List<string> molLine(string line)
        {
            string[] strs = line.Split(new char[] { ' ' });
            var sList = new List<string>();
            foreach (var ss in strs)
            {
                if (ss.Trim() != "")
                    sList.Add(ss);
            }
            return sList;
        }

        private static double replaceValue(string str)
        {
            str = !(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ".") ? str.Replace(".", ",") : str.Replace(",", ".");
            if (str == "")
                throw new ApplicationException("Имеются незаполненные поля! Убедитесь,что заданы все параметры расчета!");
            return Convert.ToDouble(str);
        }

        private enum Condition : byte
        {
            Coord,
            Velocity,
            Connection,
            Angle,
        }
    }
}
