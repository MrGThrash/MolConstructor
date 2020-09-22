
namespace MiccelPicker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Класс с методами по работе с тонкой пленкой
    /// </summary>
    public class FilmFormer 
    {
        private bool AutoFill;
        private bool HasWater;
        private bool HasAddSolvent;
        private bool EmptyFilm;
        private int XSize;
        private int YSize;
        private int ZSize;
        private int Density;
        private int MicAmount;
        private int LayerNum;
        private double Step;
        private double ShiftVerticalX;
        private double ShiftVerticalY;
        private double ShiftInternal;
        private double VerticalBulkShift;
        private int[] Shifts;
        private double[,] Miccel;


        public FilmFormer(bool autoFill, bool hasWater, bool hasAddSolv,
                          bool emptyFilm, int micAmount,
                          int layernum, int xSize, int ySize, int zSize,
                          int density, double shiftX, double shiftY,
                          double shiftInternal,double vertBulkShift,
                          int[] shifts, double[,] miccel)
        {
            AutoFill = autoFill;
            HasWater = hasWater;
            HasAddSolvent = hasAddSolv;
            EmptyFilm = emptyFilm;
            MicAmount = micAmount;
            LayerNum = layernum;
            XSize = xSize;
            YSize = ySize;
            ZSize = zSize;
            Density = density;
            Step = 1.0 / Math.Pow((double)Density, 1.0 / 3.0);
            ShiftVerticalX = shiftX;
            ShiftVerticalY = shiftY;
            ShiftInternal = shiftInternal;
            VerticalBulkShift = vertBulkShift;
            Shifts = shifts;
            Miccel = miccel;
        }


        public FilmFormer(int xSize, int ySize, int zSize, int density,
                          double[,] data)
        {
            EmptyFilm = true;
            XSize = xSize;
            YSize = ySize;
            ZSize = zSize;
            Density = density;
            Step = 1.0 / Math.Pow((double)Density, 1.0 / 3.0);
            Miccel = data;
        }

        # region Статические методы
        /// <summary>
        /// Проверка того, что количество слоев мицелл влезет в ящик
        /// </summary>
        public static bool IsFull(bool autofill, int xSize, int ySize, int zSize,
                                  int layerNum, int[] shifts, double[] diameters)
        {
            double shiftCoef = 1.0 - shifts[0] / 100.0;

            int zAmount = (int)(zSize / diameters[2]/shiftCoef);

            int calcAmount = GetAmountInFilm(autofill, xSize, ySize, layerNum,shifts, diameters);

            int maxAmount = (int)(calcAmount * zAmount / layerNum /shiftCoef);

            if (calcAmount > maxAmount)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Определение количества мицелл в неск. слоев в ящике
        /// </summary>
        public static int GetAmountInFilm(bool autofill,int xSize, int ySize,
                                          int layerNum, int[] shifts,
                                          double[] diameters)
        {

            double shiftCoefX = 1.0 - shifts[1] / 100.0;
            double shiftCoefY = 1.0 - shifts[2] / 100.0;
            int xAmount = (int)(xSize / diameters[0]);
            int yAmount = (int)(ySize / diameters[1]);

            if (autofill)
            {
                xAmount = (int)(xSize / diameters[0] / shiftCoefX);
                yAmount = (int)(xSize / diameters[0] / shiftCoefY);
            }

            return (int)(xAmount * yAmount * layerNum);

        }

        /// <summary>
        /// Опредедение числа цепей в пленке
        /// </summary>
        public static int GetAllChainsInFilm(int molLength, double[,] data)
        {
            int allMolecules = 0;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 3] == 1.0 || data[i, 3] == 1.01 || data[i,3]==1.04)
                    allMolecules++;
                else
                    break;
            }

            return allMolecules/molLength;
        }

        /// <summary>
        /// Определение диаметра мицеллы
        /// </summary>
        public static double[] GetDiameter(double[,] data)
        {
            double[] diameters = new double[] 
            {GetAxDiameter(data, 0),
            GetAxDiameter(data, 1),
            GetAxDiameter(data, 2)
            };

            return diameters;
        }

        public static double GetMinCoord(double[,] data, int axNum)
        {
             double[] axis = new double[data.GetLength(0)];

            for (int i = 0; i < axis.Length; i++)
            {
                axis[i] = data[i, axNum];
            }

            return axis.Min();
        }
        

        /// <summary>
        /// Определение диаметра вдоль оси
        /// </summary>
        private static double GetAxDiameter(double[,] data, int axNum)
        {
            double[] axis = new double[data.GetLength(0)];

            for (int i = 0; i < axis.Length; i++)
            {
                axis[i] = data[i, axNum];
            }

            return axis.Max() - axis.Min();
        }

        #endregion


        /// <summary>
        /// Формирование пленки
        /// </summary>
        public List<MiccelData> FormAfilm()
        {
            var film = new List<MiccelData>();

            int maxnum = XSize * YSize * ZSize * Density;

            // Формирование сухой пленки
            formDryFilm(maxnum,film);

            // Заполнение водой
            if (HasWater)
            {
                if(film.Count < maxnum)
                addWater(false,0, maxnum, 0.0, film);
            }

            // Добавление стен
            addWalls(film);

            return film;

        }
        /// <summary>
        /// Добавлене стен (внешний метод)
        /// </summary>
        public void AddWalls(List<MiccelData> film)
        {
            addWalls(film);
        }   

        /// <summary>
        /// Редактирование пленки
        /// </summary>
        public List<MiccelData> EditFilm(bool removeWaterFromFilm,
                                         bool filmOnSubstrate,
                                         bool filmOnLayer,
                                         bool isHomogeneious,
                                         bool hasTwoSolvs,
                                         int layers, int polymerAtoms,
                                         double percentage,
                                         double twoSolvPerc,
                                         double height,
                                         double[,] layer)
        {
            List<MiccelData> film = formFilmFromData();

            if (removeWaterFromFilm)
            {
                film = film.Where(x => x.atomType == 1.00 || x.atomType == 1.01 || x.atomType == 1.04 ||
                                  x.atomType == 1.05 || x.atomType == 1.06).ToList();
            }

            if (filmOnSubstrate)
            {
                double shift = calcFilmBottom(film);
                foreach (var c in film)
                {
                    c.zCoord -= shift;
                }

                checkBorders(film);
            }

            if (filmOnLayer)
            {
                film = addLayer(height, layer, film);
            }  

            int maxnum = XSize * YSize * ZSize * Density;
          
                if (!isHomogeneious)
                    addWater(true, layers, maxnum, percentage, film);
                else
                    addMixture(maxnum, layers, percentage, film);

                if (hasTwoSolvs)
                {
                    var rand = new Random();

                    int solvTwoAmount = (int)((double)(maxnum - polymerAtoms) * percentage * twoSolvPerc);
                    int counter = 0;

                    do
                    {
                        int num = rand.Next(polymerAtoms, maxnum - 1);

                        if (film.ElementAt(num).atomType == 1.02)
                        {
                            film.ElementAt(num).atomType = 1.07;
                            counter++;
                        }
                    } while (counter < solvTwoAmount);
                }

            // Добавление стен
            addWalls(film);

            return film;
        }

        # region Внутренние методы
        /// <summary>
        /// Обертка проверки
        /// </summary>
        private void checkBorders(List<MiccelData> film)
        {
            checkBorders(film, 0);
        }
        /// <summary>
        /// Проверка того, что молекул полимера нет вне пленки
        /// </summary>
        private void checkBorders(List<MiccelData> film, double coef)
        {
            checkPlainBorders(film);

            foreach (var c in film)
            {
                    if (c.zCoord <= (-ZSize / 2.0 + 0.8 * Step))
                        c.zCoord = c.zCoord + ZSize - 1.6 * Step;
                    if (c.zCoord >= (ZSize / 2.0 - 0.8 * Step - coef))
                        c.zCoord = c.zCoord - ZSize + 1.6 * Step + coef;
            }
        }
        private void checkPlainBorders(List<MiccelData> film)
        {
            foreach (var c in film)
            {
                if (c.xCoord <= -XSize / 2.0)
                    c.xCoord = c.xCoord + XSize;
                if (c.xCoord >= XSize / 2.0)
                    c.xCoord = c.xCoord - XSize;
                if (c.yCoord <= -YSize / 2.0)
                    c.yCoord = c.yCoord + YSize;
                if (c.yCoord >= YSize / 2.0)
                    c.yCoord = c.yCoord - YSize;
            }
        }
        private void formDryFilm(int maxnum, List<MiccelData> film)
        {
            // Размеры мицеллы по трем координатам
            double xDiam = GetAxDiameter(Miccel, 0);
            double yDiam = GetAxDiameter(Miccel, 1);
            double zDiam = GetAxDiameter(Miccel, 2);

            // Коэффициенты смещения (в случае уплотнения)
            double combLayerZ = 1.0 - Shifts[0] / 100.0;
            double combLayerX = 1.0 - Shifts[1] / 100.0;
            double combLayerY = 1.0 - Shifts[2] / 100.0;

            if (combLayerZ == 0.0)
            {
                combLayerZ = 1.0;
            }
            if (combLayerX == 0.0)
            {
                combLayerX = 1.0;
            }
            if (combLayerY == 0.0)
            {
                combLayerY = 1.0;
            }

            int xAmount = (int)(XSize / xDiam);
            int yAmount = (int)(YSize / yDiam);

            if (AutoFill)
            {
                xAmount = (int)(XSize / xDiam / combLayerX);
                yAmount = (int)(YSize / yDiam / combLayerY);
            }

            // Нижняя граница ящика (с учетом стенок)
            double lowerborder = 1.8 * Step ;

            double initPositionX = -XSize / 2.0 + XSize / xAmount - xDiam / 2.0;
            double initPositionY = -YSize / 2.0 + YSize / xAmount - yDiam / 2.0;

            // Добавление мицелл в пленку
            for (int f = 0; f < LayerNum; f++)
            {
                double shiftXZ = 0;
                double shiftYZ = 0;

                if ((f + 1) % 2 == 0)
                {
                    shiftXZ = ShiftVerticalX;
                    shiftYZ = ShiftVerticalY;
                }

                for (int k = 0; k < yAmount; k++)
                {

                    double shiftIntY = 0;

                    if ((k + 1) % 2 == 0)
                    {
                        shiftIntY = ShiftInternal;
                    }

                    for (int j = 0; j < xAmount; j++)
                    {
                        if ((film.Count / Miccel.GetLength(0)) == MicAmount)
                        {
                            break;
                        }

                        if (film.Count == maxnum)
                        {
                            break;
                        }
                        
                        moveMiccel(initPositionX + j * xDiam * combLayerX
                                   + shiftIntY + shiftXZ,
                                   initPositionY + k * yDiam * combLayerY
                                   + shiftYZ,
                                   -ZSize / 2.0 + lowerborder + f * zDiam * combLayerZ);

                            for (int i = 0; i < Miccel.GetLength(0); i++)
                            {
                                film.Add(new MiccelData(Miccel[i, 3], film.Count + 1,
                                                        Miccel[i, 0], Miccel[i, 1], Miccel[i, 2]));
                                if (film.Count == maxnum)
                                    break;
                            }

                            checkBorders(film);
                    }

                    if ((film.Count / Miccel.GetLength(0)) == MicAmount)
                        break;
                }
            }
        }
        /// <summary>
        /// Формирование пленки из массива
        /// </summary>
        /// <returns></returns>
        private List<MiccelData> formFilmFromData()
        {
            var film = new List<MiccelData>();

            for (int i=0; i<Miccel.GetLength(0);i++)
            {
                if (Miccel[i,3] != 1.06)
                film.Add(new MiccelData(Miccel[i, 3], film.Count + 1,
                                        Miccel[i, 0], Miccel[i, 1], Miccel[i, 2] - ZSize / 2.0));
            }

            return film;
        }
        private List<MiccelData> addLayer(double height, double[,] layer, List<MiccelData> film)
        {
            var editFilm = new List<MiccelData>();

            double shift = calcFilmBottom(film);
            foreach (var c in film)
            {
                c.zCoord -= shift;
            }

            checkBorders(film);

            for (int i = 0; i < layer.GetLength(0); i++)
            {
                editFilm.Add(new MiccelData(layer[i, 3], editFilm.Count + 1,
                                            layer[i, 0], layer[i, 1], layer[i, 2] - ZSize / 2.0));
            }

            int maxnum = XSize * YSize * ZSize * Density;

            foreach (var c in film)
            {
               
                    if (editFilm.Count < maxnum)
                        editFilm.Add(new MiccelData(c.atomType, editFilm.Count + 1,
                                                    c.xCoord, c.yCoord, c.zCoord + height));
                    else
                        break;

            }

            checkBorders(film);

            return editFilm;
           
        }
       
        /// <summary>
        /// Добавление воды в пленку
        /// </summary>
        private void addWater(bool isEditable,int gapLayers, int maxnum, double percentage, List<MiccelData> film)
        {

            int polymerCount = film.Count;

            double lowerborder =  1.8 * Step;
            double height = calcFilmHeight(film);

            int filmLayers = (int)((height - 0.8*Step) / Step);

            if (!EmptyFilm)
            {
                for (int i = 0; i < filmLayers; i++)
                {
                    double[,] waterLayer = createWallWithObstacles(i * Step + lowerborder - ZSize / 2.0, film);
                    for (int j = 0; j < waterLayer.GetLength(0); j++)
                    {

                        if (waterLayer[j, 0] == 0.0 && waterLayer[j, 1] == 0.0)
                            continue;
                        else
                        {
                            if (HasAddSolvent)
                                film.Add(new MiccelData(1.02, film.Count + 1,
                                                        waterLayer[j, 0], waterLayer[j, 1],
                                                        -ZSize / 2.0 + lowerborder + i * Step));
                            else
                                film.Add(new MiccelData(1.03, film.Count + 1,
                                                        waterLayer[j, 0], waterLayer[j, 1],
                                                        -ZSize / 2.0 + lowerborder + i * Step));
                        }
                    }
                }
            }

            if (gapLayers != 0)
            {
                for (int i = 0; i < gapLayers; i++)
                {
                    double[,] waterLayer = createWallWithObstacles(height - ZSize / 2.0 + (i+1)*Step, film);
                    for (int j = 0; j < waterLayer.GetLength(0); j++)
                    {

                        if (waterLayer[j, 0] == 0.0 && waterLayer[j, 1] == 0.0)
                            continue;
                        else
                        {
                            if (percentage != 0)
                                film.Add(new MiccelData(1.02, film.Count + 1,
                                                        waterLayer[j, 0], waterLayer[j, 1],
                                                        -ZSize / 2.0 + lowerborder + filmLayers*Step - (i + 1) * Step));
                            else
                                film.Add(new MiccelData(1.03, film.Count + 1,
                                                        waterLayer[j, 0], waterLayer[j, 1],
                                                        -ZSize / 2.0 + lowerborder + filmLayers * Step - (i + 1) * Step));
                        }
                    }
                }
            }

            // Определение оставшегося числа слоев

            int calcLayers = calcRemainedLayers(maxnum, film);
            int solvLayers = (int)(calcLayers * percentage);
            if (gapLayers != 0 && percentage !=0)
            {
                solvLayers = calcSolvLayers(polymerCount, film.Count, maxnum, percentage);
 
            }

            int remainedLayers = (int)((ZSize - 0.8*Step) / Step) - filmLayers;

            double coef = (double)remainedLayers / (double)calcLayers;

            for (int i = 0; i < calcLayers; i++)
            {
                double[,] waterLayer = createWall();
                double type = 1.03;

                if (solvLayers != 0)
                {
                    if (i < solvLayers)
                        type = 1.02;
                }

                //if (i == calcLayers - 1 && !isEditable)
                //{
                //    shiftBulk(film, coef*Step);
   
                //}


                for (int j = 0; j < waterLayer.GetLength(0); j++)
                {

                    if (i < calcLayers - 1)
                        film.Add(new MiccelData(type, film.Count + 1,
                                                waterLayer[j, 0], waterLayer[j, 1],
                                                -ZSize / 2.0 + lowerborder + filmLayers * Step
                                                + i * Step * coef));
                    else
                    {
                        if (film.Count < maxnum)
                        {
                            film.Add(new MiccelData(type, film.Count + 1,
                                               waterLayer[j, 0], waterLayer[j, 1],
                                               ZSize / 2.0 - 1.8* Step ));
                        }
                        else
                            break;
                        }     
                }

               
            }
        }
        /// <summary>
        /// Добавление смеси воды и растоврителя в пленку
        /// </summary>
        private void addMixture(int maxnum, int gapLayers, double percentage, List<MiccelData> film)
        {
            int polymerCount = film.Count;
            int waterCount = maxnum - polymerCount;
            int solvCount = (int)(waterCount * percentage);

            double lowerborder = 1.8 * Step;
            double height = calcFilmHeight(film);

            int filmLayers = (int)((height - 0.8 * Step) / Step);


            if (gapLayers != 0)
            {
                for (int i = 0; i < gapLayers; i++)
                {
                    double[,] waterLayer = createWallWithObstacles(height - ZSize / 2.0 + (i + 1) * Step, film);


                    for (int j = 0; j < waterLayer.GetLength(0); j++)
                    {

                        if (waterLayer[j, 0] == 0.0 && waterLayer[j, 1] == 0.0)
                            continue;

                        else
                                film.Add(new MiccelData(1.03, film.Count + 1,
                                                        waterLayer[j, 0], waterLayer[j, 1],
                                                        -ZSize / 2.0 + lowerborder + filmLayers * Step - (i + 1) * Step));  
                    }
                }
            }

            // Определение оставшегося числа слоев
     
            int calcLayers = calcRemainedLayers(maxnum, film);
            int remainedLayers = (int)((ZSize - 0.8 * Step) / Step) - filmLayers;

            double coef = (double)remainedLayers / (double)calcLayers;

            for (int i = 0; i < calcLayers; i++)
            {
                double[,] waterLayer = createWall();

                for (int j = 0; j < waterLayer.GetLength(0); j++)
                {

                    if (i < calcLayers - 1)
                        film.Add(new MiccelData(1.03, film.Count + 1,
                                                waterLayer[j, 0], waterLayer[j, 1],
                                                -ZSize / 2.0 + lowerborder + filmLayers * Step
                                                + i * Step * coef));
                    else
                    {
                        if (film.Count < maxnum)
                        {
                            film.Add(new MiccelData(1.03, film.Count + 1,
                                               waterLayer[j, 0], waterLayer[j, 1],
                                               ZSize / 2.0 - 1.8 * Step));
                        }
                        else
                            break;
                    }
                }
            }

            // Закрашивание воды растворителем

            if (percentage == 1.0)
            {
                foreach (var c in film)
                {
                    if (c.atomType.Equals(1.03))
                    {
                        c.atomType = 1.02;
                    }
                }
            }
            else
            {

                Random rnd = new Random();

                int randsolvCount = 0;

                do
                {
                    int num = rnd.Next(polymerCount + 1, maxnum);
                    if (film[num - 1].atomType != 1.02)
                    {
                        film[num - 1].atomType = 1.02;
                        randsolvCount++;
                    }
                } while (randsolvCount < solvCount);
            }


        }
        private void shiftBulk(List<MiccelData> film, double coef)
        {
            foreach (var bead in film)
            {
                bead.zCoord += VerticalBulkShift;
            }

            checkBorders(film,coef);
        }
        /// <summary>
        /// Добавление стен в пленку
        /// </summary>
        private void addWalls(List<MiccelData> film)
        {
            double[,] singleWall = createWall();

            // Стен по 2 слоя, расстояние между стеночными слоями 0.8/Step
            for (int j = 0; j < 2; j++)
            {
                //Нижняя стена
                for (int i = 0; i < singleWall.GetLength(0); i++)
                {
                    film.Add(new MiccelData(1.08, film.Count + 1, singleWall[i, 0] + 0.5 * j * Step, 
                                            singleWall[i, 1] + 0.5 * j * Step, -ZSize / 2.0 + j * (0.8 * Step)));

                }

                //Верхняя стена
                for (int i = 0; i < singleWall.GetLength(0); i++)
                {
                    film.Add(new MiccelData(1.09, film.Count + 1, singleWall[i, 0] + 0.5 * j * Step,
                                            singleWall[i, 1] + 0.5 * j * Step, ZSize / 2.0 - j * (0.8 * Step)));
                }
            }

            checkPlainBorders(film);
        }

        /// <summary>
        /// Расчет оставшихся слоев воды над пленкой
        /// </summary>
        private int calcRemainedLayers(int maxnum, List<MiccelData> film)
        {
            double remained = calcRemainedLayers_Float(maxnum, film);
            int calcLayers = (int)remained;
            if (calcLayers < remained)
                calcLayers++;

            return calcLayers;
        }
        private double calcRemainedLayers_Float(int maxnum, List<MiccelData> film)
        {
            int atomsPerLayer = (int)(XSize / Step) * (int)(YSize / Step);
            
            return ((double)(maxnum - film.Count) / (double)atomsPerLayer);
        }
        private int calcSolvLayers(int polymerCount, int filmCount, int maxnum, double percentage)
        {
            int atomsPerLayer = (int)(XSize / Step) * (int)(YSize / Step);

              int solvCount = (int)((maxnum - polymerCount) * percentage);

                solvCount -= (filmCount - polymerCount);

                return solvCount/(atomsPerLayer);
 

        }
        /// <summary>
        /// Расчет высоты сухой пленки
        /// </summary>
        private double calcFilmHeight(List<MiccelData> film)
        {
            double height = 0;

            // Определение высоты сухой пленки
            foreach (var c in film)
            {
                double h = c.zCoord + ZSize / 2.0;
                if (h > height)
                    height = h;
            }

            return height;
        }


        private double calcFilmBottom(List<MiccelData> film)
        {

            double minZ = film[0].zCoord + ZSize/2.0;

            foreach (var c in film)
            {
                if (c.atomType == 1.0 || c.atomType == 1.01)
                {

                double h = c.zCoord + ZSize / 2.0;

                if (h < minZ)
                    minZ = h;
               }
            }

            return (minZ - 1.8 * Step);

        }
        /// <summary>
        /// Сдвиг мицеллы вдоль осей координат
        /// </summary>
        private double[,] moveMiccel(double xCoord, double yCoord, double zCoord)
        {
            double[] xArr = new double[Miccel.GetLength(0)];
            double[] yArr = new double[Miccel.GetLength(0)];
            double[] zArr = new double[Miccel.GetLength(0)];

            for (int i = 0; i < Miccel.GetLength(0); i++)
            {
                xArr[i] = Miccel[i, 0];
                yArr[i] = Miccel[i, 1];
                zArr[i] = Miccel[i, 2];
            }

            double xDifference = xArr.Min() - xCoord;
            double yDifference = yArr.Min() - yCoord;
            double zDifference = zArr.Min() - zCoord;

            for (int i = 0; i < Miccel.GetLength(0); i++)
            {
                Miccel[i, 0] = Miccel[i, 0] - xDifference;
                Miccel[i, 1] = Miccel[i, 1] - yDifference;
                Miccel[i, 2] = Miccel[i, 2] - zDifference;
            }

            return Miccel;
        }
        /// <summary>
        /// Создание однородного слоя с препятствиями
        /// </summary>
        private double[,] createWallWithObstacles(double zCoord,List<MiccelData> film)
        {
            double[,] wall = createWall();

            var wallHoles = new List<double[]>();

            for (int i = 0; i < film.Count; i++)
            {
                if (Math.Abs(film[i].zCoord - zCoord) < Step/2.0)
                    wallHoles.Add(new double[] { film[i].xCoord, film[i].yCoord});
            }

            for (int i = 0; i < wall.GetLength(0); i++)
            {
                foreach (var c in wallHoles)
                {
                    //if (Math.Sqrt(Math.Pow(wall[i, 0] - c[0], 2) + Math.Pow(wall[i, 1] - c[1], 2)) <= Step)
                    if (Math.Abs(wall[i, 0] - c[0]) < Step
                        && Math.Abs(wall[i, 1] - c[1]) < Step)
                    {
                        wall[i, 0] = 0;
                        wall[i,1] = 0;
                    }
                }
            }
            return wall;
        }
        /// <summary>
        /// Создание однородной стены без препятствий
        /// </summary>
        private double[,] createWall()
        {
           
            int xAmount = (int)(XSize / Step);
            int yAmount = (int)(YSize / Step);


            double[,] wall = new double[xAmount * yAmount, 2];


            for (int i = 0; i < yAmount; i++)
            {
                for (int j = 0; j < xAmount; j++)
                {
                    if (j == 0 && i == 0)
                    {
                        wall[0, 0] = -XSize / 2.0;
                        wall[0, 1] = -YSize / 2.0;
                        continue;
                    }
                    else
                    {
                        wall[j + i * xAmount, 0] = Math.Round(wall[0, 0] + j * Step,3);
                        wall[j + i * xAmount, 1] = Math.Round(wall[0, 1] + i * Step,3);
                    }
                }
            }

            return wall;
        }
        #endregion
    }
}
