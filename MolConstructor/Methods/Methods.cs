using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math.Decompositions;

namespace MolConstructor
{
    public class Methods
    {

        #region Methods for geometry and other stuff
        public static bool IsFull(int xSize, int ySize, int zSize, double[] diameters)
        {
            int zAmount = (int)(zSize / diameters[2]);
            int flatAmount = GetFlatAmount(xSize, ySize, diameters);
            int maxAmount = flatAmount * zAmount;
            return flatAmount > maxAmount;
        }

        public static int GetFlatAmount(int xSize, int ySize, double[] diameters)
        {
            return (int)(xSize / diameters[0]) * (int)(ySize / diameters[1]);
        }

        public static double[] GetShapeCharacteristics(List<double[]> data)
        {
            var pol = data.Where(x => x[3] == 1.00 ||
                           x[3] == 1.01 ||
                           x[3] == 1.04 ||
                           x[3] == 1.05).ToList();

            //var pol = data.Where(x => x[3] == 1.00).ToList();

            double[] centerMass = GetCenterMass(data);
            double diagX = 0.0;
            double diagY = 0.0;
            double diagZ = 0.0;
            double elemXY = 0.0;
            double elemXZ = 0.0;
            double elemYZ = 0.0;

            foreach (var c in pol)
            {
                diagX += Math.Pow(c[0] - centerMass[0], 2.0);
                diagY += Math.Pow(c[1] - centerMass[1], 2.0);
                diagZ += Math.Pow(c[2] - centerMass[2], 2.0);
                elemXY += (c[0] - centerMass[0]) * (c[1] - centerMass[1]);
                elemXZ += (c[0] - centerMass[0]) * (c[2] - centerMass[2]);
                elemYZ += (c[1] - centerMass[1]) * (c[2] - centerMass[2]);
            }


            diagX /= pol.Count;
            diagY /= pol.Count;
            diagZ /= pol.Count;
            elemXY /= pol.Count;
            elemXZ /= pol.Count;
            elemYZ /= pol.Count;

            var gyrRad = diagX + diagY + diagZ;

            var gyrTensor = new double[3, 3]{{diagX,elemXY, elemXZ},
                                             {elemXY,diagY, elemYZ},
                                             {elemXZ,elemYZ, diagZ}};


            var solver = new EigenvalueDecomposition(gyrTensor, true, true);

            var eigens = solver.RealEigenvalues;

            var asphericity = (3.0 / 2.0) * ((Math.Pow(eigens[0], 2) + Math.Pow(eigens[1], 2) + Math.Pow(eigens[2], 2)) / Math.Pow(gyrRad, 2)) - 0.5;

            var shapeOblatness = (3.0 * eigens[0] - gyrRad) * (3.0 * eigens[1] - gyrRad) * (3.0 * eigens[2] - gyrRad) / Math.Pow(gyrRad, 3);

            return new double[] { asphericity, shapeOblatness, eigens[0], eigens[1], eigens[2] };
        }



        public static double GetGyrRadius(List<double[]> data)
        {
            return Math.Round(Math.Sqrt(GetAxInertSquareRadius(data, 0) + GetAxInertSquareRadius(data, 1) + GetAxInertSquareRadius(data, 2)), 3);
        }

        public static double GetHydroRadius2D(List<double[]> data)
        {
            return Math.Round(Math.Sqrt(GetAxInertSquareRadius(data, 0) + GetAxInertSquareRadius(data, 1)), 2);
        }

        public static double GetAxInertSquareRadius(List<double[]> data, int ax)
        {
            var pol = data.Where(x => x[3] == 1.00 ||
                              x[3] == 1.01 ||
                              x[3] == 1.04 ||
                              x[3] == 1.05).ToList();

            double[] centerMass = GetCenterMass(data);
            double sum = 0.0;
            foreach (var c in pol)
            {
                sum += Math.Pow(c[ax] - centerMass[ax], 2.0);
            }
            return sum / (double)pol.Count;
        }

        public static double[] GetDiameter(List<MolData> data)
        {
            return new double[] { GetAxDiameter(data, 0), GetAxDiameter(data, 1), GetAxDiameter(data, 2) };
        }

        public static double[] GetDiameter(List<double[]> data)
        {
            return new double[] { GetAxDiameter(data, 0), GetAxDiameter(data, 1), GetAxDiameter(data, 2) };
        }

        public static double GetAxDiameter(List<MolData> data, int axNum)
        {

            double diam = data.Max(x => x.XCoord) - data.Min(x => x.XCoord);

            if (axNum == 1)
            {
                diam = data.Max(x => x.YCoord) - data.Min(x => x.YCoord);
            }
            if (axNum == 2)
            {
                diam = data.Max(x => x.ZCoord) - data.Min(x => x.ZCoord);
            }
            if (diam == 0) { diam = 0.5; }

            return diam;
        }

        public static double GetAxDiameter(List<double[]> data, int axNum)
        {

            double diam = data.Max(x => x[axNum]) - data.Min(x => x[axNum]);
           
            if (diam == 0) { diam = 0.5; }

            return diam;
        }

        public static double GetMolSliceRadius(List<double[]> data, int axNumOne, int axNumTwo, double[] centerMass)
        {
            var slice = data.Where(x => x[3] == 1.00 || x[3] == 1.01).ToList();

            double rad = 0.0;

            if (slice.Count != 0)
            {
                rad = slice.Max(x => Math.Sqrt(Math.Pow(x[axNumOne] - centerMass[axNumOne], 2) +
                                               Math.Pow(x[axNumTwo] - centerMass[axNumTwo], 2)));
            }
            return rad;
        }

        public static double[] GetCenterMass(List<double[]> data)
        {
            return new double[]
            {GetAxCenterMass(data, 0),
            GetAxCenterMass(data, 1),
            GetAxCenterMass(data, 2)
            };
        }

        public static double GetAxCenterMass(List<double[]> data, int axNum)
        {
            double ax = 0;

            foreach (var c in data)
            {
                ax += c[axNum];
            }

            return ax / data.Count;
        }

        public static double[] CenterStructure(double[] centerPoint, List<double[]> data)
        {
            double[] centerMass = GetCenterMass(data);
            for (int i = 0; i <= 2; i++)
                centerMass[i] = Math.Round(centerPoint[i] - centerMass[i], 2);
            return centerMass;
        }

        public static double[] CenterStructureInit(double[] sizes, double[] centerPoint, List<double[]> data)
        {

            double[] centerMass = GetCenterMassWithPBC(sizes, data);
            for (int i = 0; i < 2; ++i)
                centerMass[i] = Math.Round(sizes[i] - centerPoint[i] - centerMass[i], 2);
            centerMass[2] = 0.0;
            return centerMass;
        }

        public static double[] GetCenterMassWithPBC(double[] sizes, List<double[]> data)
        {
            double[] centerMass = new double[3];
            //for (int axInd = 0; axInd <= 2; ++axInd)
            //    numArray[axInd] = CenterAxis_Type2(true, axInd, axises[axInd], 0.0, data);
            var tetasX = new double[2];
            var tetasY = new double[2];
            var tetasZ = new double[2];

            for (int p = 0; p < data.Count; p++)
            {
                var tetaX = data[p][0] / sizes[0] * 2 * Math.PI;
                var tetaY = data[p][1] / sizes[1] * 2 * Math.PI;
                var tetaZ = data[p][2] / sizes[2] * 2 * Math.PI;

                tetasX[0] += Math.Cos(tetaX);
                tetasX[1] += Math.Sin(tetaX);

                tetasY[0] += Math.Cos(tetaY);
                tetasY[1] += Math.Sin(tetaY);

                tetasZ[0] += Math.Cos(tetaZ);
                tetasZ[1] += Math.Sin(tetaZ);
            }

            tetasX[0] /= data.Count;
            tetasX[1] /= data.Count;
            tetasY[0] /= data.Count;
            tetasY[1] /= data.Count;
            tetasZ[0] /= data.Count;
            tetasZ[1] /= data.Count;

            var meantetaX = Math.Atan2(-tetasX[1], -tetasX[0]) + Math.PI;
            var meantetaY = Math.Atan2(-tetasY[1], -tetasY[0]) + Math.PI;
            var meantetaZ = Math.Atan2(-tetasZ[1], -tetasZ[0]) + Math.PI;

            var cmX = sizes[0] * meantetaX / (2 * Math.PI);
            var cmY = sizes[1] * meantetaY / (2 * Math.PI);
            var cmZ = sizes[2] * meantetaZ / (2 * Math.PI);

            centerMass = new double[] { cmX, cmY, cmZ };
            return centerMass;
        }

        // Centering procedure based on the PBC
        public static double CenterAxis_Type2(bool fullNorm, int axInd, double axSize, double axPoint, List<double[]> data)
        {
            var pol = data.Where(x => x[3] == 1.00 ||
                             x[3] == 1.01 ||
                             x[3] == 1.04 ||
                             x[3] == 1.09).ToList();

            //var pol = data.Where(x => x[3] == 1.00 ||
            //        x[3] == 1.05).ToList();

            var normMol = new List<double[]>();
            // All beads will be rewtiten to the lower corner
            var minCoord = pol.Min(x => x[axInd]);

            foreach (var d in pol)
            {
                var coef = axSize / 2.0;
                if (axInd == 2) { coef = axSize; }

                if (Math.Abs(minCoord - d[axInd]) < coef)
                {
                    normMol.Add(d);
                }
                else
                {
                    if (axInd == 0)
                        normMol.Add(new double[] { d[0] - axSize, d[1], d[2], d[3] });
                    else if (axInd == 1)
                        normMol.Add(new double[] { d[0], d[1] - axSize, d[2], d[3] });
                    else
                        normMol.Add(new double[] { d[0], d[1], d[2] - axSize, d[3] });
                }
            }

            if (fullNorm)
            {
                double coef = -axSize / 2.0 + 1.0;
                if (axInd == 2)
                {
                    coef = 1.0;
                }

                if (minCoord > coef)
                {
                    foreach (var c in normMol)
                    {
                        c[axInd] -= axSize;
                    }
                }
            }

            double[] centerMass = GetCenterMass(normMol);

            if (!fullNorm)
            {
                centerMass[axInd] -= axPoint;
            }
            else
            {
                centerMass[axInd] -= axSize / 2.0;
            }

            return centerMass[axInd];
        }

        public static double GetDistance3DwithPBC(double[] sizes, double xOne, double yOne, double zOne, double xTwo, double yTwo, double zTwo)
        {
            var distX = Math.Abs(xOne - xTwo);
            var distY = Math.Abs(yOne - yTwo);
            var distZ = Math.Abs(zOne - zTwo);

            if (distX> sizes[0]/2.0)
            {
                distX -= sizes[0];
            }
            if (distY > sizes[1] / 2.0)
            {
                distY -= sizes[1];
            }
            if (distZ > sizes[2] / 2.0)
            {
                distZ -= sizes[2];
            }
            return Math.Round(Math.Sqrt(Math.Pow(distX, 2.0) + Math.Pow(distY, 2.0) + Math.Pow(distZ, 2.0)), 3);
        }

        public static double GetDistance3D(double xOne, double yOne, double zOne, double xTwo, double yTwo, double zTwo)
        {
            return Math.Round(Math.Sqrt(Math.Pow(xOne - xTwo, 2.0) + Math.Pow(yOne - yTwo, 2.0) + Math.Pow(zOne - zTwo, 2.0)), 3);
        }

        public static double GetDistance2D(double xOne, double yOne, double xTwo, double yTwo)
        {
            return Math.Round(Math.Sqrt(Math.Pow(xOne - xTwo, 2.0) + Math.Pow(yOne - yTwo, 2.0)), 3);
        }

        public static double GetDistance1D(double firstPoint, double secondPoint)
        {
            return Math.Round(firstPoint - secondPoint, 3);
        }

        public static double GetHeight(List<double[]> data)
        {
            return GetHeights(data)[1];
        }

        public static double[] GetHeights(List<double[]> data)
        {
            var pol = data.Where(x => x[3] == 1.0 || x[3] == 1.01
                                   && x[3] == 1.04 || x[3] == 1.05).ToList();

            return new double[] { pol.Min(x => x[2]), pol.Max(x => x[2]) };
        }

        public static double GetIntegralHeight(List<double[]> data)
        {
            double stepH = data[1][0] - data[0][0];
            double[] func1 = new double[data.Count];
            double[] func2 = new double[data.Count];
            for (int i = 0; i < data.Count; ++i)
            {
                func1[i] = data[i][0] * (data[i][1] + data[i][2] + data[i][3]);
                func2[i] = data[i][1] + data[i][2] + data[i][3];
            }
            return Math.Round(2.0 * CalcIntegral(stepH, func1) / CalcIntegral(stepH, func2), 1);
        }

        public static double GetDifferentialHeight(List<double[]> data)
        {
            double stepH = data[1][0] - data[0][0];
            double[] func = new double[data.Count];

            for (int i = 0; i < data.Count; i++)
                func[i] = data[i][1] + data[i][2] + data[i][3];

            double[] diff = CalcDifferential(stepH, func);

            for (int i = 0; i < func.Length - 1 && (func[i] != 0.0 || func[i + 1] != 0.0); ++i)
            {
                if (Math.Abs(diff[i]) == 0.0)
                    return i * stepH;
            }
            return 0.0;
        }

        public static double GetPsiXParam(int orderType, int particles, double radius, List<MolData> points)
        {
            var psi6 = 0.0;
            var psiReal = 0.0;
            var psiImp = 0.0;

            var neighbors = new List<MolData>();

            for (int i = 0; i < particles; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    if (points[j].Index != points[i].Index)
                    {
                        var dist = GetDistance3D(points[i].XCoord, points[i].YCoord, 0.0,
                                                points[j].XCoord, points[j].YCoord, 0.0);

                        if (dist < radius)
                        {
                            neighbors.Add(points[j]);
                        }
                    }
                }
                // sort by distance
                neighbors = neighbors.OrderBy(x => GetDistance3D(points[i].XCoord, points[i].YCoord, 0.0,
                                                x.XCoord, x.YCoord, 0.0)).ToList();

                // leave only X neighbors depending on the OrderType
                if (neighbors.Count > orderType)
                {
                    for (int j = neighbors.Count - 1; j > orderType-1; j--)
                    {
                        neighbors.RemoveAt(j);
                    }
                }

                var psiTempReal = 0.0;
                var psiTempImp = 0.0;

                foreach (var c in neighbors)
                {
                    var tan = (points[i].YCoord - c.YCoord) / (points[i].XCoord - c.XCoord);
                    var atan = Math.Atan(tan);

                    psiTempReal += Math.Cos(orderType * atan);
                    psiTempImp += Math.Sin(orderType * atan);
                }

                psiReal += psiTempReal / neighbors.Count;
                psiImp += psiTempImp / neighbors.Count;

                neighbors.Clear(); // empty neighbor list for the next particle 
            }

            psi6 = Math.Sqrt(Math.Pow(psiReal / particles, 2) + Math.Pow(psiImp / particles, 2));

            return psi6;
        }

        public static List<double[]> GetPolymers(List<double[]> data)
        {
            var polymerTypes = FileWorker.GetTableTypes("Polymer");

            var pol = new List<double[]>();

            foreach (var c in data)
            {
                if (polymerTypes.Contains(c[3]))
                {
                    pol.Add(c);
                }
            }

            return pol;
        }

        public static List<MolData> GetPolymers(List<MolData> data)
        {
            var polymerTypes = FileWorker.GetTableTypes("Polymer");

            var pol = new List<MolData>();

            foreach (var c in data)
            {
                if (polymerTypes.Contains(c.AtomType))
                {
                    pol.Add(c);
                }
            }

            return pol;
        }

        // calculated Radial Distribution function. 
        // calcType = 0 then 2d, = 1 then 3d
        public static List<double[]> CalcRadialDistFunc(int calcType, int particles, double step, double maxDist, double density, List<MolData> points)
        {
            var rdf = new List<double[]>();

            int stepsNum = (int)(maxDist / (step));

            rdf.Add(new double[2] { 0, 0.0 });

            for (int i = 0; i < particles; i++)
            {
                for (int j = 1; j <= stepsNum; j++)
                {
                    var layer = new List<MolData>();

                    foreach (var c in points)
                    {
                        if (c.Index != points[i].Index)
                        {
                            var dist = GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                                   points[i].XCoord, points[i].YCoord, points[i].ZCoord);
                            if (dist > (step * j) && dist <= (step * (j + 1)))
                            {
                                layer.Add(c);
                            }
                        }
                    }

                    var gr = layer.Count / (Math.PI * (Math.Pow(j * step, 2) - Math.Pow((j - 1) * step, 2)) * density);
                    if (calcType == 1)
                    {
                        gr = layer.Count / (4.0 / 3.0 * Math.PI * (Math.Pow(j * step, 3) - Math.Pow((j - 1) * step, 3)) * density);
                    }

                    if (i == 0)
                    {
                        rdf.Add(new double[2] { j * step, gr });
                    }
                    else
                    {
                        rdf[j][1] += gr;
                    }
                }
            }

            foreach (var c in rdf)
            {
                c[1] /= particles;
            }

            return rdf;
        }

        private static double CalcIntegral(double stepH, double[] func)
        {
            double intSumm = (func[0] + func[func.Length - 1]) / 2.0;
            for (int i = 0; i < func.Length - 2; i++)
            {
                intSumm += func[i + 1];
            }
            return intSumm * stepH;
        }

        private static double[] CalcDifferential(double stepH, double[] func)
        {
            double[] diff = new double[func.Length - 1];
            for (int i = 0; i < func.Length - 1; i++)
            {
                diff[i] = (func[i + 1] - func[i]) / stepH;
            }
            return diff;
        }
        #endregion
        
        #region Methods with recursion
        // Finds the cluster using the radius search
        public static void GetOneAggregate(List<MolData> core, bool hasBonds, double beadType, double radius, double[] sizes,
                                           double[] centerPoint, List<MolData> initCoreBeads)
        {

            var newBeads = new List<MolData>();

            if (core.Count == 0)
            {            
                core.Add(initCoreBeads[0]);
                newBeads.Add(initCoreBeads[0]);
                initCoreBeads.Remove(initCoreBeads[0]);
            }

            do
            {
                    var currBead = newBeads[0];

                    foreach (var c in initCoreBeads)
                    {
                        if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord, currBead.XCoord, currBead.YCoord, currBead.ZCoord) <= radius)
                        {
                            core.Add(c);
                            newBeads.Add(c);
                        }

                        if (currBead.XCoord > (sizes[0] / 2.0 + centerPoint[0]) - radius)
                        {
                            if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                            currBead.XCoord - (sizes[0] / 2.0 + centerPoint[0]), currBead.YCoord, currBead.ZCoord) <= radius)
                            {
                                core.Add(c);
                                newBeads.Add(c);
                            }
                        }

                        if (currBead.XCoord < (sizes[0] / 2.0 - centerPoint[0]) + radius)
                        {
                            if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                            currBead.XCoord + (sizes[0] / 2.0 + centerPoint[0]), currBead.YCoord, currBead.ZCoord) <= radius)
                            {
                                core.Add(c);
                                newBeads.Add(c);
                            }
                        }

                        if (currBead.YCoord > (sizes[1] / 2.0 + centerPoint[1]) - radius)
                        {
                            if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                            currBead.XCoord, currBead.YCoord - (sizes[1] / 2.0 + centerPoint[1]), currBead.ZCoord) <= radius)
                            {
                                core.Add(c);
                                newBeads.Add(c);
                            }
                        }

                        if (currBead.YCoord < (sizes[1] / 2.0 - centerPoint[1]) + radius)
                        {
                            if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                            currBead.XCoord, currBead.YCoord + (sizes[1] / 2.0 + centerPoint[1]), currBead.ZCoord) <= radius)
                            {
                                core.Add(c);
                                newBeads.Add(c);
                            }
                        }

                        if (currBead.ZCoord > (sizes[2] / 2.0 + centerPoint[2]) - radius)
                        {
                            if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                            currBead.XCoord, currBead.YCoord, currBead.ZCoord - (sizes[2] / 2.0 + centerPoint[2])) <= radius)
                            {
                                core.Add(c);
                                newBeads.Add(c);
                            }
                        }

                        if (currBead.ZCoord < (sizes[2] / 2.0 - centerPoint[2]) + radius)
                        {
                            if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                            currBead.XCoord, currBead.YCoord, currBead.ZCoord + (sizes[2] / 2.0 + centerPoint[2])) <= radius)
                            {
                                core.Add(c);
                                newBeads.Add(c);
                            }
                        }
                    }

                // Accounting bonds
                if (hasBonds)
                    {
                         foreach (var c in currBead.Bonds)
                            {
                                var bondBead = initCoreBeads.Find(x => x.Index == c);

                                if (bondBead != null && !newBeads.Contains(bondBead))
                                {
                                    core.Add(bondBead);
                                    newBeads.Add(bondBead);
                                }
                            }
                    }

                    newBeads.Remove(currBead);


                // Remove the beads from initial
                foreach (var c in newBeads)
                {
                    initCoreBeads.Remove(c);
                }


            } while (newBeads.Count > 0);



            //// Accounting №2 for residual bonds
            //if (hasBonds)
            //{
            //    for (int i = 0; i < core.Count; i++)
            //    {
            //        foreach (var c in core[i].Bonds)
            //        {
            //            int bondInd = initCoreBeads.FindIndex(x => x.Index == c);

            //            if (bondInd != -1)
            //            {
            //                core.Add(initCoreBeads[bondInd]);
            //                initCoreBeads.RemoveAt(bondInd);
            //            }
            //        }
            //    }
            //}
        }

        public static void GetOneAggregate_Recursion(bool hasBonds, double beadType, double radius, double[] sizes, double[] centerPoint, 
                                               MolData currBead, List<MolData> core, List<MolData> initCoreBeads)
        {

            if (initCoreBeads.Contains(currBead))
            {
            core.Add(currBead);
            initCoreBeads.Remove(currBead);
            }

            var newBeads = new List<MolData>();

            foreach (var c in initCoreBeads)
                {
                 
                if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord, currBead.XCoord, currBead.YCoord, currBead.ZCoord) <= radius)
                {
                    newBeads.Add(c);
                }

                if (currBead.XCoord > (sizes[0]/2.0 + centerPoint[0])- radius)
                {
                    if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord, 
                                    currBead.XCoord - (sizes[0] / 2.0 + centerPoint[0]), currBead.YCoord, currBead.ZCoord) <= radius)
                    {
                        newBeads.Add(c);
                    }
                }

                if (currBead.XCoord < (sizes[0] / 2.0 - centerPoint[0]) + radius)
                {
                    if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                    currBead.XCoord + (sizes[0] / 2.0 + centerPoint[0]), currBead.YCoord, currBead.ZCoord) <= radius)
                    {
                        newBeads.Add(c);
                    }
                }

                if (currBead.YCoord > (sizes[1] / 2.0 + centerPoint[1]) - radius)
                {
                    if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                    currBead.XCoord, currBead.YCoord - (sizes[1] / 2.0 + centerPoint[1]), currBead.ZCoord) <= radius)
                    {
                        newBeads.Add(c);
                    }
                }

                if (currBead.YCoord < (sizes[1] / 2.0 - centerPoint[1]) + radius)
                {
                    if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                    currBead.XCoord, currBead.YCoord + (sizes[1] / 2.0 + centerPoint[1]), currBead.ZCoord) <= radius)
                    {
                        newBeads.Add(c);
                    }
                }

                if (currBead.ZCoord > (sizes[2] / 2.0 + centerPoint[2]) - radius)
                {
                    if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                    currBead.XCoord, currBead.YCoord, currBead.ZCoord - (sizes[2] / 2.0 + centerPoint[2])) <= radius)
                    {
                        newBeads.Add(c);
                    }
                }

                if (currBead.ZCoord < (sizes[2] / 2.0 - centerPoint[2]) + radius)
                {
                    if (GetDistance3D(c.XCoord, c.YCoord, c.ZCoord,
                                    currBead.XCoord, currBead.YCoord, currBead.ZCoord + (sizes[2] / 2.0 + centerPoint[2])) <= radius)
                    {
                        newBeads.Add(c);
                    }
                }
                
            }
            foreach (var c in newBeads)
            {
                core.Add(c);
                initCoreBeads.Remove(c);
            }

            // Accounting bonds
            if (hasBonds)
            {
                var nbCount = newBeads.Count;
                for (int p = 0; p < nbCount; p++)
                {
                    foreach (var c in newBeads[p].Bonds)
                    {
                        var bondBead = initCoreBeads.Find(x => x.Index == c);

                        if (bondBead !=null && !newBeads.Contains(bondBead))
                        {
                            newBeads.Add(bondBead);
                        }
                    }
                }
            }

            //newBeads = newBeads.Distinct().ToList();

            if (newBeads.Count != 0)
            {
                foreach (var c in newBeads)
                {
                    //if (!core.Contains(c))
                    //{
                        currBead = c;
                        GetOneAggregate_Recursion(hasBonds, beadType, radius, sizes, centerPoint, currBead, core, initCoreBeads);
                    //}
                }     
            }
            else
            {
                return;
            }
        }


        // Add nonlinear molecule into the simulation box bead by bead (polymers) by recurcion 
        public static void AddNonLinMol_Recurcion(int molIndex, string direction, Random rnd, List<int> placedBeads, MolData currBead, List<MolData> mol, List<MolData> system)
        {
            AddNonLinMol_Recurcion(molIndex, direction, new List<double[]>(), rnd, placedBeads, currBead, mol, system);
        }
        public static void AddNonLinMol_Recurcion(int molIndex, string direction, List<double[]> borders, Random rnd, List<int> placedBeads, MolData currBead, List<MolData> mol, List<MolData> system)
        {
            currBead.XCoord = system[system.Count - 1].XCoord;
            currBead.YCoord = system[system.Count - 1].YCoord;
            currBead.ZCoord = system[system.Count - 1].ZCoord;

            placedBeads.Add(currBead.Index);

            if (currBead.Bonds.Count == 1 && placedBeads.Contains(currBead.Bonds[0]))
            {
                return;
            }
            else
            {
                foreach (var c in currBead.Bonds)
                {
                    if (!placedBeads.Contains(c))
                    {
                        double xCoord = rnd.Next(-600, 600) / 1000.0 + currBead.XCoord;
                        double yCoord = rnd.Next(-600, 600) / 1000.0 + currBead.YCoord;
                        double zCoord = rnd.Next(-600, 600) / 1000.0 + currBead.ZCoord;

                        if (direction == "X")
                        {
                            xCoord = rnd.Next(0, 500) / 1000.0 + currBead.XCoord;
                        }
                        if (direction == "Y")
                        {
                            yCoord = rnd.Next(0, 500) / 1000.0 + currBead.YCoord;
                        }
                        if (direction == "Z")
                        {
                            zCoord = rnd.Next(0, 500) / 1000.0 + currBead.ZCoord;
                        }

                        if (borders.Count != 0)
                        {
                            if (borders[0][0] != borders[0][1])
                            {
                                if (xCoord < borders[0][0])
                                {
                                    xCoord = borders[0][0];
                                }

                                if (xCoord > borders[0][1])
                                {
                                    xCoord = borders[0][1];
                                }
                            }

                            if (borders[1][0] != borders[1][1])
                            {
                                if (yCoord < borders[1][0])
                                {
                                    yCoord = borders[1][0];
                                }

                                if (yCoord > borders[1][1])
                                {
                                    yCoord = borders[1][1];
                                }
                            }

                            if (borders[2][0] != borders[2][1])
                            {
                                if (zCoord < borders[2][0])
                                {
                                    zCoord = borders[2][0];
                                }

                                if (zCoord > borders[2][1])
                                {
                                    zCoord = borders[2][1];
                                }
                            }
                        }

                        system.Add(new MolData(mol.First(x => x.Index == c).AtomType, system.Count + 1, molIndex, xCoord, yCoord, zCoord));


                        placedBeads.Add(c);

                        currBead = mol.First(x => x.Index == placedBeads[placedBeads.Count - 1]);

                        AddNonLinMol_Recurcion(molIndex, direction, borders, rnd, placedBeads, currBead, mol, system);
                    }
                }
            }
        }
        #endregion
    }
}
