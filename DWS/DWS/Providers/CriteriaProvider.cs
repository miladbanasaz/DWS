using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using DWS.Models;

namespace DWS.Providers
{
    public static class CriteriaProvider
    {
        private const string CriteriaTemplate =
            "Draw a(n) <shape> with a <measurement> of <amount> [and a(n) <measurement> of <amount>] ";

        private const string CircleTemplate = "Draw a circle with a radius of 100";
        private const string SquareTemplate = "Draw a square with a side length of 200";
        private const string RectangleTemplate = "Draw a rectangle with a width of 250 and a height of 400";
        private const string OctagonTemplate = "Draw an octagon with a side length of 200";

        private const string IsoscelesTriangleTemplate =
            "Draw an isosceles triangle with a height of 200 and a width of 100";

        private const string EquilateralTriangleTemplate = "Draw a equilateral triangle with a side length of 200";

        private const string ParallelogramTemplate =
            "Draw a parallelogram with a width of 200 and a height of 100";

        private const string PentagonTemplate = "Draw a pentagon with a side length of 200";
        private const string HexagonTemplate = "Draw a hexagon with a side length of 200";
        private const string HeptagonTemplate = "Draw a heptagon with a side length of 200";

        public static ResponseModel Process(string criteria)
        {
            criteria = criteria.ToLower();

            var criteriaItems = criteria
                .Replace("draw an", "")
                .Replace("draw a", "")
                .Replace("with a", "")
                .Replace("of", "")
                .Replace("and an", "")
                .Replace("and a", "")
                .Split(' ')
                .ToList();
            criteriaItems.RemoveAll(string.IsNullOrWhiteSpace);

            if (criteriaItems[0] == "circle")
            {
                return ProcessCircle(criteriaItems.ToArray());
            }

            if (criteriaItems[0] == "square")
            {
                return ProcessSquare(criteriaItems.ToArray());
            }

            if (criteriaItems[0] == "rectangle")
            {
                return ProcessRectangle(criteriaItems.ToArray());
            }

            if (criteriaItems[0] == "octagon")
            {
                return ProcessOctagon(criteriaItems.ToArray());
            }

            if (criteriaItems[0] == "isosceles" && criteriaItems[1] == "triangle")
            {
                return ProcessIsoscelesTriangle(criteriaItems.ToArray());
            }

            if (criteriaItems[0] == "equilateral" && criteriaItems[1] == "triangle")
            {
                return ProcessEquilateralTriangle(criteriaItems.ToArray());
            }

            if (criteriaItems[0] == "parallelogram")
            {
                return ProcessParallelogram(criteriaItems.ToArray());
            }
            if (criteriaItems[0] == "pentagon")
            {
                return ProcessPentagon(criteriaItems.ToArray());
            }
            if (criteriaItems[0] == "hexagon")
            {
                return ProcessHexagon(criteriaItems.ToArray());
            }
            if (criteriaItems[0] == "heptagon")
            {
                return ProcessHeptagon(criteriaItems.ToArray());
            }

            return new ResponseModel()
            {
                Status = -1,
                Message = CriteriaTemplate
            };
        }

        private static string ConvertToBase64(Bitmap bmp)
        {
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                return Convert.ToBase64String(ms.GetBuffer());
            }
        }

        private static ResponseModel ProcessCircle(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {CircleTemplate}"
            };
            try
            {
                if (criteria.Length < 3 || criteria[1] != "radius")
                    return res;

                var radius = Convert.ToInt32(criteria[2]);

                var bmp = new Bitmap(radius*2 + 1, radius*2 + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
                g.DrawEllipse(new Pen(Color.Black), 0, 0, radius*2, radius*2);

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch
            {
            }

            return res;
        }

        private static ResponseModel ProcessSquare(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {SquareTemplate}"
            };
            try
            {
                if (criteria.Length < 4 || criteria[2] != "length")
                    return res;

                var length = Convert.ToInt32(criteria[3]);

                var bmp = new Bitmap(length + 1, length + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
                g.DrawRectangle(new Pen(Color.Black), 0, 0, length, length);

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch
            {
            }

            return res;
        }

        private static ResponseModel ProcessRectangle(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {RectangleTemplate}"
            };
            try
            {
                if (criteria.Length < 5 || criteria[1] != "width" || criteria[3] != "height")
                    return res;

                var width = Convert.ToInt32(criteria[2]);
                var height = Convert.ToInt32(criteria[4]);

                var bmp = new Bitmap(width + 1, height + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
                g.DrawRectangle(new Pen(Color.Black), 0, 0, width, height);

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch
            {
            }

            return res;
        }

        private static ResponseModel ProcessOctagon(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {OctagonTemplate}"
            };
            try
            {
                if (criteria.Length < 4 || criteria[2] != "length")
                    return res;

                var length = Convert.ToInt32(criteria[3]);
                var r2 = Convert.ToSingle(length / Math.Sqrt(2));

                var bmp = new Bitmap(length * 2 + 1, length * 2 + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);

                var x = bmp.Width / 2;
                var y = bmp.Height / 2;

                var points = new List<PointF>()
                {
                    new PointF(x, y - length),
                    new PointF(x + r2, y - r2),
                    new PointF(x + length, y),
                    new PointF(x + r2, y + r2),
                    new PointF(x, y + length),
                    new PointF(x - r2, y + r2),
                    new PointF(x - length, y),
                    new PointF(x - r2, y - r2),
                };

                g.DrawPolygon(new Pen(Color.Black), points.ToArray());

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch 
            {
            }

            return res;
        }

        private static ResponseModel ProcessIsoscelesTriangle(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {IsoscelesTriangleTemplate}"
            };
            try
            {
                if (criteria.Length < 6 || criteria[4] != "width" || criteria[2] != "height")
                    return res;

                var width = Convert.ToInt32(criteria[5]);
                var height = Convert.ToInt32(criteria[3]);

                var bmp = new Bitmap(width + 1, height + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);

                var points = new List<PointF>()
                {
                    new PointF(width / 2, 0),
                    new PointF(0, height),
                    new PointF(width, height),
                };

                g.DrawPolygon(new Pen(Color.Black), points.ToArray());

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch
            {
            }

            return res;
        }

        private static ResponseModel ProcessEquilateralTriangle(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {EquilateralTriangleTemplate}"
            };
            try
            {
                if (criteria.Length < 5 || criteria[3] != "length")
                    return res;

                var length = Convert.ToInt32(criteria[4]);

                var bmp = new Bitmap(length * 2 + 1, length * 2 + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);

                var points = new List<PointF>();

                var center = new PointF(bmp.Width / 2, bmp.Height / 2);
                var Angle = 0.525;

                for (int i = 0; i < 3; i++)
                {
                    var p = new PointF
                    {
                        X = center.X + length * (float) Math.Cos(Angle + i * 2 * Math.PI / 3),
                        Y = center.Y + length * (float) Math.Sin(Angle + i * 2 * Math.PI / 3)
                    };
                    points.Add(p);
                }

                g.DrawPolygon(new Pen(Color.Black), points.ToArray());

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch
            {
            }

            return res;
        }

        private static ResponseModel ProcessParallelogram(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {ParallelogramTemplate}"
            };
            try
            {
                if (criteria.Length < 5 || criteria[1] != "width" || criteria[3] != "height")
                    return res;

                var width = Convert.ToInt32(criteria[2]);
                var height = Convert.ToInt32(criteria[4]);

                var bmp = new Bitmap(width + 1, height + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);

                var points = new List<PointF>()
                {
                    new PointF(width/3, 0),
                    new PointF(0, height),
                    new PointF(width, height),
                    new PointF(width/3*2, 0)
                };

                g.DrawPolygon(new Pen(Color.Black), points.ToArray());

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch
            {
            }

            return res;
        }

        private static ResponseModel ProcessPentagon(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {PentagonTemplate}"
            };
            try
            {
                if (criteria.Length < 4 || criteria[2] != "length")
                    return res;

                var length = Convert.ToInt32(criteria[3]);

                var bmp = new Bitmap(length*2 + 1, length*2 + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);

                var points = CalculateVertices(5, length, new PointF(length+(length/2), length*2- (length / 5)));

                g.DrawPolygon(new Pen(Color.Black), points);

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch
            {
            }

            return res;
        }

        private static ResponseModel ProcessHexagon(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {HexagonTemplate}"
            };
            try
            {
                if (criteria.Length < 4 || criteria[2] != "length")
                    return res;

                var length = Convert.ToInt32(criteria[3]);

                var bmp = new Bitmap(length*2 + 1, length*2 + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);

                var points = CalculateVertices(6, length, new PointF(length+(length/2), length*2- (length / 5)));

                g.DrawPolygon(new Pen(Color.Black), points);

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch
            {
            }

            return res;
        }

        private static ResponseModel ProcessHeptagon(string[] criteria)
        {
            var res = new ResponseModel()
            {
                Status = -1,
                Message = $"Acceptance Criteria is: {HeptagonTemplate}"
            };
            try
            {
                if (criteria.Length < 4 || criteria[2] != "length")
                    return res;

                var length = Convert.ToInt32(criteria[3]);

                var bmp = new Bitmap(length*3 + 1, length*3 + 1);
                var g = Graphics.FromImage(bmp);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);

                var points = CalculateVertices(7, length, new PointF(length*2, length*3- (length / 5)));

                g.DrawPolygon(new Pen(Color.Black), points);

                res.Image = ConvertToBase64(bmp);

                bmp.Dispose();

                res.Status = 0;
            }
            catch
            {
            }

            return res;
        }


        private static PointF[] CalculateVertices(int nSides, int nSideLength, PointF ptFirstVertex)
        {
            if (nSides < 3)
                throw new ArgumentException("Polygons can't have less than 3 sides...");

            var aptsVertices = new PointF[nSides];
            var deg = (180.0 * (nSides - 2)) / nSides;
            var step = 360.0 / nSides;
            var rad = deg * (Math.PI / 180);

            double nSinDeg = Math.Sin(rad);
            double nCosDeg = Math.Cos(rad);

            aptsVertices[0] = ptFirstVertex;

            for (int i = 1; i < aptsVertices.Length; i++)
            {
                double x = aptsVertices[i - 1].X - nCosDeg * nSideLength;
                double y = aptsVertices[i - 1].Y - nSinDeg * nSideLength;
                aptsVertices[i] = new Point((int)x, (int)y);


                //recalculate the degree for the next vertex
                deg -= step;
                rad = deg * (Math.PI / 180);

                nSinDeg = Math.Sin(rad);
                nCosDeg = Math.Cos(rad);

            }
            return aptsVertices;
        }
    }
}