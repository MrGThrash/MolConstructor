using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;


namespace MiccelPicker
{
    public class FileWorkerW
    {

        /// <summary>
        /// перечислимый тип видов записи в строку
        /// </summary>
        private enum Condition : byte
        {
            Coord,
            Velocity,
            Connection,
            Angle
        }


        /// <summary>
        /// Сохранить мицеллу в файл XYZR
        /// </summary>
        public static void Save_XYZ(string fileName, int xLength, int yLength, int zLength, List<MiccelData> atoms)
        {
            var dirname = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dirname)) Directory.CreateDirectory(dirname);
            using (var sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                // Таблица
                for (int i = 0; i < atoms.Count; i++)
                {

                    if (atoms[i].atomType != 1.08 && atoms[i].atomType != 1.09)
                    {
                        string line = CreateOneLine_XYZ(atoms[i].xCoord + xLength / 2,
                                                        atoms[i].yCoord + yLength / 2,
                                                        atoms[i].zCoord + zLength / 2,
                                                        atoms[i].atomType);

                        sw.WriteLine(line);
                    }
                }

                sw.Flush();
            }

        }

        /// <summary>
        /// Сохранить мицеллу в файл XYZR
        /// </summary>
        public static void SaveShift(string fileName, 
                                     double xSize, double ySize, double zSize, 
                                     List<MiccelData> atoms)
        {
            var dirname = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dirname)) Directory.CreateDirectory(dirname);
            using (var sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                // Таблица
                for (int i = 0; i < atoms.Count; i++)
                {
                    string line = CreateOneLine_XYZ(atoms[i].xCoord,
                                                    atoms[i].yCoord,
                                                    atoms[i].zCoord, 
                                                    atoms[i].atomType);

                    sw.WriteLine(line);
                }

                // Запись ящика
                string lineXY0 = CreateOneLine_XYZ(-xSize / 2.0, -ySize / 2.0, 0, 1.060);
                string lineXYZ = CreateOneLine_XYZ(-xSize / 2.0, -ySize / 2.0, zSize, 1.060);
                string lineXy0 = CreateOneLine_XYZ(-xSize / 2.0, ySize / 2.0, 0, 1.060);
                string lineXyZ = CreateOneLine_XYZ(-xSize / 2.0, ySize / 2.0, zSize, 1.060);
                string linexY0 = CreateOneLine_XYZ(xSize / 2.0, -ySize / 2.0, 0, 1.060);
                string linexYZ = CreateOneLine_XYZ(xSize / 2.0, -ySize / 2.0, zSize, 1.060);
                string linexy0 = CreateOneLine_XYZ(xSize / 2.0, ySize / 2.0, 0, 1.060);
                string linexyZ = CreateOneLine_XYZ(xSize / 2.0, ySize / 2.0, zSize, 1.060);
                sw.WriteLine(lineXY0);
                sw.WriteLine(lineXYZ);
                sw.WriteLine(lineXy0);
                sw.WriteLine(lineXyZ);
                sw.WriteLine(linexY0);
                sw.WriteLine(linexYZ);
                sw.WriteLine(linexy0);
                sw.WriteLine(linexyZ);

                sw.Flush();
            }
        }


         /// <summary>
        /// Сохранить мицеллу в файл MOL
        /// </summary>
        public static void Save_MOL(string fileName, bool connention, int molLength,
                                    List<MiccelData> atoms)
        {
            Save_MOL(fileName, connention, molLength, new List<int[]>(), atoms);
        }

        /// <summary>
        /// Сохранить мицеллу в файл MOL
        /// </summary>
        public static void Save_MOL(string fileName,bool connection, int molLength,
                                   List<int[]> bonds,
                                   List<MiccelData> atoms)
        {
            var dirname = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dirname)) Directory.CreateDirectory(dirname);
            using (var sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                // Таблица
                for (int i = 0; i < atoms.Count; i++)
                {
                    string type = "C";
                    if (atoms[i].atomType == 1.01)
                        type = "O";
                    if (atoms[i].atomType == 1.02)
                        type = "S";
                    if (atoms[i].atomType == 1.03 || atoms[i].atomType == 1.06)
                        type = "I";
                    if (atoms[i].atomType == 1.04)
                        type = "N";
                    if (atoms[i].atomType == 1.05)
                        type = "H";
                    if (atoms[i].atomType == 1.07)
                        type = "Z";
                    if (atoms[i].atomType == 1.08)
                        type = "ZN";
                    if (atoms[i].atomType == 1.09)
                        type = "AU";

                    string line = CreateOneLine_MOL(Condition.Coord, i + 1,i+2,
                                                    atoms[i].xCoord, atoms[i].yCoord,
                                                    atoms[i].zCoord, type);   
                    sw.WriteLine(line);
                }


                // Связи
                if (connection)
                {
                    if (bonds.Count != 0)
                    {

                        for (int i = 0; i < atoms.Count; i++)
                        {
                            if (((i + 1) % molLength) != 0)
                            {
                                string line = CreateOneLine_MOL(Condition.Connection, i, i + 1,
                                                                atoms[i].xCoord, atoms[i].yCoord,
                                                                atoms[i].zCoord, null);
                                sw.WriteLine(line);
                            }

                        }
                    }
                    else
                    {
                        foreach (var c in bonds)
                        {
                            string line = CreateOneLine_MOL(Condition.Connection, c[0]-1, c[1]-1, 0, 0, 0, null);
                            sw.WriteLine(line);
                        }
                    }
                }

                sw.WriteLine("END");
                sw.Flush();
            }

        }


        public static void SaveBonds_DAT(string fileName, List<int[]> bonds)
        {
                 var dirname = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dirname)) Directory.CreateDirectory(dirname);
            using (var sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                for (int i = 0; i < bonds.Count; i++)
                {
                    sw.WriteLine(String.Format(
                    "{0,8}{1,8}",
                    bonds[i][0],
                    bonds[i][1]));
                }
            }
        }

        /// <summary>
        /// Сохранить мицеллу в рестарт-файл 
        /// </summary>
        public static void Save_DAT(string fileName,int density,
                                    int molLength,double[] scales,
                                    double[,] data, List<MiccelData> atoms)
        {
            int amount = data.GetLength(0) - 8;

             var dirname = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dirname)) Directory.CreateDirectory(dirname);
            using (var sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                // Запись параметров
                sw.WriteLine(String.Format(
                    "{0,8}{1,8}{2,8}{3,8}",
                    amount,
                    molLength,
                    atoms.Count/molLength, 
                    density));

                // Запись размеров ящика
                sw.WriteLine(String.Format(
                    "{0,20}{1,20}{2,20}",
                    ToFortranFormat(scales[0],"0.000000000000"),
                    "0.232177136833D-52",
                    "0.258027476660D-53"
                    ));
                sw.WriteLine(String.Format(
                    "{0,20}{1,20}{2,20}",
                    "0.232177136833D-52",
                    ToFortranFormat(scales[1], "0.000000000000"),
                    "-0.111689668373D-52"
                    ));
                sw.WriteLine(String.Format(
                    "{0,20}{1,20}{2,20}",
                    "0.387041214990D-53",
                    "-0.167534502560D-52",
                    ToFortranFormat(scales[2], "0.000000000000")
                    ));

                // Запись атомов мицеллы
                for(int i =0; i< atoms.Count; i++)
                {
                    int type = 1;
                    if (atoms[i].atomType != 1.0)
                        type = 2;

                    sw.WriteLine(CreateOneLine_DAT(Condition.Coord, 0,0, type,
                                                   atoms[i].xCoord, atoms[i].yCoord, 
                                                   atoms[i].zCoord - scales[2]/2.0));
                }

                // Запись остальных атомов
                for (int i = 0 ;i< amount; i++)
                {
                    if(!MiccelData.Exists(atoms,i+1))
                    sw.WriteLine(CreateOneLine_DAT(Condition.Coord, 0,0, 8,
                                                   data[i, 0], data[i, 1],
                                                   data[i, 2] - scales[2] / 2.0));
                   
                }

                // Запись скоростей
                for (int i = 0; i < amount; i++)
                {
                    sw.WriteLine(CreateOneLine_DAT(Condition.Velocity,0, 0, 0, 0, 0, 0));
                }

                // Запись связей
                sw.WriteLine(String.Format(
                    "{0,7}{1,11}",
                    "bonds:",
                    atoms.Count*(molLength-1)/molLength
                    ));

                for (int i = 0; i < atoms.Count; i++)
                {
                    if (((i + 1) % molLength) != 0)
                    {
                        string line = CreateOneLine_DAT(Condition.Connection, i + 1, i+2,
                                                        0,0,0,0);
                        sw.WriteLine(line);
                    }

                }

                // Запись углов
                sw.WriteLine(String.Format(
                    "{0,8}{1,10}",
                    "angles:",0
                    ));
    
                sw.Flush();
            }
        }


        public static void SaveFilm_DAT(string fileName, int density,
                                        int molLength, int allChains, double[] scales, 
                                        List<int[]> bonds, List <int[]> angles,
                                        List<MiccelData> film)
        {
            var dirname = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dirname)) Directory.CreateDirectory(dirname);
            using (var sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {

                int allatoms = (int)(scales[0] * scales[1] * scales[2] * density);

                if (density == 6)
                    allatoms = allatoms / 2;

                // Запись параметров
                sw.WriteLine(String.Format(
                    "{0,8}{1,8}{2,8}{3,8}",
                    allatoms,
                    bonds.Count,
                    allChains,
                    density));

                // Запись размеров ящика
                sw.WriteLine(String.Format(
                    "{0,20}{1,20}{2,20}",
                    ToFortranFormat(scales[0], "0.000000000000"),
                    "0.232177136833D-52",
                    "0.258027476660D-53"
                    ));
                sw.WriteLine(String.Format(
                    "{0,20}{1,20}{2,20}",
                    "0.232177136833D-52",
                    ToFortranFormat(scales[1], "0.000000000000"),
                    "-0.111689668373D-52"
                    ));
                sw.WriteLine(String.Format(
                    "{0,20}{1,20}{2,20}",
                    "0.387041214990D-53",
                    "-0.167534502560D-52",
                    ToFortranFormat(scales[2], "0.000000000000")
                    ));


                // Запись атомов пленки
                for (int i = 0; i < film.Count; i++)
                {
                    if (film[i].atomType != 1.08 && film[i].atomType != 1.09)
                    {
                        int type = 1;
                        if (film[i].atomType == 1.01)
                        {
                            type = 2;
                        }
                        if (film[i].atomType == 1.02)
                        {
                            type = 3;
                        }
                        if (film[i].atomType == 1.04)
                        {
                            type = 4;
                        }
                        if (film[i].atomType == 1.05)
                        {
                            type = 5;
                        }
                        if (film[i].atomType == 1.07)
                        {
                            type = 7;
                        }
                        if (film[i].atomType == 1.03 || film[i].atomType == 1.06)
                            type = 8;

                        sw.WriteLine(CreateOneLine_DAT(Condition.Coord, 0,0, type,
                                                       film[i].xCoord, film[i].yCoord,
                                                       film[i].zCoord));
                    }
                }

                // Запись скоростей
                for (int i = 0; i < film.Count; i++)
                {
                    if (film[i].atomType != 1.08 && film[i].atomType != 1.09)
                        sw.WriteLine(CreateOneLine_DAT(Condition.Velocity,0, 0, 0, 0, 0, 0));
                }

                // Запись связей
                if (bonds.Count == 0)
                {
                    //sw.WriteLine(String.Format(
                    //    "{0,7}{1,11}",
                    //    "bonds:",
                    //    allChains * (molLength - 1)
                    //    ));

                    //for (int i = 0; i < allChains * molLength; i++)
                    //{
                    //    if (((i + 1) % molLength) != 0)
                    //    {
                    //        string line = CreateOneLine_DAT(Condition.Connection, i + 1, i + 2,
                    //                                        0, 0, 0, 0);
                    //        sw.WriteLine(line);
                    //    }

                    //}
                }
                else
                {
                    sw.WriteLine(String.Format("{0,7}{1,11}",
                                               "bonds:",
                                               bonds.Count
                                                 ));

                    for (int i = 0; i < bonds.Count; i++)
                    {
                        string line = CreateOneLine_DAT(Condition.Connection, bonds[i][0], bonds[i][1],
                                                         0, 0, 0, 0);
                        sw.WriteLine(line);
                    }
            }

            // Запись углов
            sw.WriteLine(String.Format(
                    "{0,8}{1,10}",
                    "angles:", angles.Count
                    ));

                for (int i = 0; i < angles.Count; i++)
                {
                    string line = CreateOneLine_DAT(Condition.Angle, angles[i][0], angles[i][1],
                                                    angles[i][2], 0, 0, 0);
                    sw.WriteLine(line);
                }

                sw.Flush();
            }
        }


        public static void SaveFilm_LAMMPS(string fileName,
                                           int Xsize, int Ysize, int Zsize,
                                           List<MiccelData> System)
        {

            var dirname = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dirname)) Directory.CreateDirectory(dirname);
            using (var sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251), 65536))
            {
                sw.WriteLine("ITEM: TIMESTEP");
                sw.WriteLine("0");
                sw.WriteLine("ITEM: NUMBER OF ATOMS");
                sw.WriteLine(System.Count);
                sw.WriteLine("ITEM: BOX BOUNDS pp pp pp");
                sw.WriteLine(String.Format("{0,1}{1,6}", 0, Xsize));
                sw.WriteLine(String.Format("{0,1}{1,6}", 0, Ysize));
                sw.WriteLine(String.Format("{0,1}{1,6}", 0, Zsize));
                sw.WriteLine("ITEM: ATOMS id type xs ys zs");
                for (int i = 0; i < System.Count; i++)
                {
                    string line = CreateOneLine_Lammptrj(System[i].index, System[i].xCoord, System[i].yCoord, System[i].zCoord, (int)System[i].atomType);
                    sw.WriteLine(line);
                }
                sw.Flush();
            }
        }

        private static string CreateOneLine_XYZ(double x, double y, double z, double type)
        {
            return String.Format(
                "{0,9}{1,9}{2,9}{3,9}",
                x.ToString("0.000",CultureInfo.InvariantCulture),
                y.ToString("0.000",CultureInfo.InvariantCulture),
                z.ToString("0.000",CultureInfo.InvariantCulture),
                type.ToString("0.000",CultureInfo.InvariantCulture)
            );
        }

        private static string CreateOneLine_MOL(Condition cond,int number, int otherNumber, double x, double y, 
                                                double z, string type)
        {
            string result = null;

            string numberString = number.ToString();

            if (number > 99999) numberString = "*****";
            
            switch (cond)
            {
                case Condition.Coord:
                    result = String.Format(
                        "{0,-6}{1,5}{2,3}{3,12}{4,12}{5,8}{6,8}",
                        "HETATM",
                        numberString,
                        type,
                        number,
                        x.ToString("0.000", CultureInfo.InvariantCulture),
                        y.ToString("0.000", CultureInfo.InvariantCulture),
                        z.ToString("0.000", CultureInfo.InvariantCulture)
                    );
                    break;

                case Condition.Connection:
                    result = String.Format(
                    "{0,-6}{1,5}{2,5}",
                    "CONECT",
                    number.ToString(),
                    otherNumber.ToString()
                    );
                    break;
            }

            return result;
        }

        private static string CreateOneLine_DAT(Condition cond,int number,int otherNumber,
                                                int type,
                                                double x,double y,double z)
        {
            string result = null;
            switch (cond)
            {
                case Condition.Coord:
                    result = String.Format(
                        "{0,4}{1,14}{2,14}{3,14}",
                        type,
                        ToFortranFormat(x,"0.00000"),
                        ToFortranFormat(y,"0.00000"),
                       ToFortranFormat(z,"0.00000")
                    );
                    break;

                case Condition.Velocity:
                    result = String.Format(
                        "{0,23}{0,23}{0,23}",
                        "0.100000000000000D+00"
                        );
                    break;
                case Condition.Connection:
                    result = String.Format(
                    "{0,8}{1,8}",
                    number,
                    otherNumber
                    );
                    break;
               case Condition.Angle:
                    result = String.Format(
                              "{0,8}{1,8}{2,8}",
                              number,
                              otherNumber,
                              type);
                    break;
            }

            return result; 
        }

        private static string CreateOneLine_Lammptrj(int number, double x, double y, double z, int type)
        {
            return String.Format(
                "{0,6}{1,2}{2,9}{3,9}{4,9}",
                number,
                type,
                x.ToString("0.0000", CultureInfo.InvariantCulture),
                y.ToString("0.0000", CultureInfo.InvariantCulture),
                z.ToString("0.0000", CultureInfo.InvariantCulture)
            );
        }



        private static string ToFortranFormat(double a, string format)
        {
            int order = 0;

            do
            {
                if (((int)a % 10) != 0)
                {
                    a = a / 10.0;
                    order++;
                }

                else if (((int)a % 10) == 0 
                         && (int)a != 0)
                {
                    a = a / 10.0;
                    order++;
                }

                else
                    break;

            } while (((int)a % 10) != 0 && (int)a != 0);

            return String.Format("{0}D+{1}",
                                 a.ToString(format, CultureInfo.InvariantCulture),
                                 order.ToString("00")
            );
 
        }

        public static List<int[]> LoadBonds(string[] lines)
        {
            var bonds = new List<int[]>();

            for (int i = 0; i < lines.Length; i++)
            {
                var sList = readLine(lines[i]);

                int[] row = new int[2];

                for (int j = 0; j < 2; j++)
                {
                    row[j] = Convert.ToInt32(sList.ElementAt(j));
                }

                if (row[0] != 0 && row[1] != 0)
                    bonds.Add(row);
            }

            if (bonds[bonds.Count - 1][0] == 0 && bonds[bonds.Count - 1][1] == 0)
            {
                int amount = bonds.Count;
                for (int i = amount - 1; i >= amount - 13; i--)
                {
                    bonds.RemoveAt(i);
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

        public static double[,] LoadFromLinesXYZR(string[] lines)
        {
            int rows = 4;

         double[,] data = new double[lines.Length,rows];

         for (int i = 0; i < lines.Length; i++)
         {
             string[] strs = lines[i].Split(new Char[] { ' ' });

             var sList = new List<string>();

             foreach (var ss in strs)
             {
                 if (ss.Trim() != "")
                     sList.Add(ss);
             }

                        for (int j = 0; j < rows; j++)
                 {
                     data[i, j] = replaceValue(sList.ElementAt(j));
                 }
            
         }
            return data;
        }


         public static double[,] LoadFromLinesLAMMPS (string[] lines)
         {
             int rows = 5;
             int atomsNum = Convert.ToInt32(lines[3]);

             int[] sizes = LoadSizesLAMMPS(lines);

             double[,] data = new double[atomsNum, rows];

             for (int i = atomsNum-1; i >= 0; i--)
             {

                 string[] strs = lines[lines.Length-i-1].Split(new Char[] { ' ' });

                 var sList = new List<string>();

                 foreach (var ss in strs)
                 {
                     if (ss.Trim() != "")
                         sList.Add(ss);
                 }


                 data[i, 0] = Convert.ToInt32(sList.ElementAt(0));

                 int type = Convert.ToInt32(sList.ElementAt(1));

                 if( type == 1)
                     data[i, 1] = 1.000;
                 if( type ==2)
                     data[i, 1] = 1.010;
                 if (type == 3)
                     data[i, 1] = 1.020;
                 if (type == 4)
                     data[i, 1] = 1.030;
                 
                 for (int j = 2; j < rows; j++)
                 {
                     data[i, j] = replaceValue(sList.ElementAt(j)) * sizes[j-2];
                 }

             }

             return data;
         }

         public static int[] LoadSizesLAMMPS(string[] lines)
         {
             string[] xSize = lines[5].Split(new Char[] { ' ' });
             string[] ySize = lines[6].Split(new Char[] { ' ' });
             string[] zSize = lines[7].Split(new Char[] { ' ' });

             int[] sizes = new int[] { Convert.ToInt32(xSize[1]) - Convert.ToInt32(xSize[0]),
                                       Convert.ToInt32(ySize[1]) - Convert.ToInt32(ySize[0]),
                                       Convert.ToInt32(zSize[1]) - Convert.ToInt32(zSize[0])};

             return sizes;
         }

        private static List<string> readLine(string line)
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

    }
}
