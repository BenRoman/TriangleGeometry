using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MeshExplorer;
using TriangleNet.Geometry;
using TriangleNet.Meshing;

namespace GerasimTriangle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<TriangleNet.Topology.Triangle> ListOfTriangles;
        List<double> A = new List<double>();
        List<double> B = new List<double>();
        List<double> C = new List<double>();
        List<double> D = new List<double>();
        List<PointF> Center = new List<PointF>();
        List<double> R = new List<double>();
        List<Vertex> CT;
        double[][] NTG;

        public PointF[] points = new PointF[] { new PointF { X = 100, Y = 100 },
                                              new PointF { X = 200, Y = 50 } ,
                                              new PointF { X = 230, Y = 20 } ,
                                              new PointF { X = 250, Y = 100 } ,
                                              new PointF { X = 200, Y = 150 } ,
                                              new PointF { X = 250, Y = 300 } ,
                                              new PointF { X = 220, Y = 280 } ,
                                              new PointF { X = 200, Y = 350 } ,
                                              new PointF { X = 100, Y = 350 } ,
                                              new PointF { X = 50 , Y = 250 } ,
                                              new PointF { X = 150, Y = 200 } ,
                                          
        };
        public List<Vertex> vertexes = new List<Vertex>();
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics graph = pictureBox1.CreateGraphics();
            graph.DrawPolygon(Pens.Black, points);

        }
        private void triangulate_Click(object sender, EventArgs e)
        {
            ListOfTriangles = new List<TriangleNet.Topology.Triangle>();
            Triangulate();
        }
        //private void Triangulate()
        //{
        //    double minAngle = 0;
        //    double maxAngle = 0;
        //    double maxArea = 0;
        //    Polygon p = new Polygon();
        //    var options = new ConstraintOptions();
        //    foreach (var item in points)
        //        vertexes.Add(new Vertex {X = item.X , Y = item.Y });


        //        //set graphics
        //        p.AddContour(vertexes);
        //        options = new ConstraintOptions() { ConformingDelaunay = true , Convex = false };


        //    //set graphics
        //    Graphics graph = pictureBox1.CreateGraphics();
        //    Graphics gr = pictureBox1.CreateGraphics();
        //    int w = pictureBox1.ClientSize.Width / 2;
        //    int h = pictureBox1.ClientSize.Height / 2;
        //    //graph.TranslateTransform(w, h);
        //    gr.TranslateTransform(w, h);
        //    //graph.ScaleTransform(1, -1);


        //    //triangulation
        //    var quality = new QualityOptions() { MinimumAngle = minAngle, MaximumAngle = maxAngle, MaximumArea = maxArea };
        //    var mesh = p.Triangulate(options, quality);
        //    ListOfTriangles = mesh.Triangles.ToList();
        //    CT = mesh.Vertices.ToList();
        //    NTG = getBoundsVertices(mesh);
        //    for (int i = 0; i < ListOfTriangles.Count; ++i)
        //    {
        //        graph.DrawPolygon(Pens.Black, new PointF[3] { new PointF((float)ListOfTriangles[i].GetVertex(0).X, (float)ListOfTriangles[i].GetVertex(0).Y),
        //                                                         new PointF((float)ListOfTriangles[i].GetVertex(1).X, (float)ListOfTriangles[i].GetVertex(1).Y),
        //                                                         new PointF((float)ListOfTriangles[i].GetVertex(2).X, (float)ListOfTriangles[i].GetVertex(2).Y) });
        //        PointF centroid = new PointF(((float)ListOfTriangles[i].GetVertex(0).X + (float)ListOfTriangles[i].GetVertex(1).X + (float)ListOfTriangles[i].GetVertex(2).X) / 3.0f,
        //                                     ((float)ListOfTriangles[i].GetVertex(0).Y + (float)ListOfTriangles[i].GetVertex(1).Y + (float)ListOfTriangles[i].GetVertex(2).Y) / 3.0f);
        //        centroid.Y = -centroid.Y;
        //        centroid.Y -= 5;
        //        centroid.X -= 5;
        //        if (ListOfTriangles.Count < 200 && (maxArea >= 800 || maxArea == 0))
        //        {
        //            DrawText(centroid, ListOfTriangles[i].ID.ToString(), gr, true);
        //        }
        //    }
        //    if (ListOfTriangles.Count < 200 && (maxArea >= 800 || maxArea == 0))
        //    {
        //        for (int i = 0; i < CT.Count; i++)
        //        {
        //            DrawTextRed(new PointF((float)CT[i].X, (float)-CT[i].Y), CT[i].ID.ToString(), gr);
        //        }
        //    }
        //    textBox1.Text = ListOfTriangles.Count.ToString();
        //    foreach (var item in ListOfTriangles)
        //        richTextBox1.Text += item.GetVertex(0).X+" : "+ item.GetVertex(0).Y + " -  "+ item.GetVertex(1).X + " : " + item.GetVertex(1).Y +  " - "+ item.GetVertex(2).X + " : " + item.GetVertex(2).Y + "\n";

        //}
        private void Triangulate()
        {
            double minAngle = 0;
            double maxAngle = 0;
            double maxArea =  0;
            Polygon p = new Polygon();
            var options = new ConstraintOptions();
            foreach (var item in points)
                vertexes.Add(new Vertex {X = item.X , Y = item.Y });
            p.AddContour(vertexes);
            options = new ConstraintOptions() { ConformingDelaunay = true, Convex = false };
            Graphics graph = pictureBox1.CreateGraphics();
            Graphics gr = pictureBox1.CreateGraphics();
            //int w = pictureBox1.ClientSize.Width / 2;
            //int h = pictureBox1.ClientSize.Height / 2;
            //graph.TranslateTransform(w, h);
            //gr.TranslateTransform(w, h);
            //graph.ScaleTransform(1, -1);

            //triangulation
            var quality = new QualityOptions() { MinimumAngle = minAngle, MaximumAngle = maxAngle, MaximumArea = maxArea };
            var mesh = p.Triangulate(options, quality);
            ListOfTriangles = mesh.Triangles.ToList();
            CT = mesh.Vertices.ToList();
            NTG = getBoundsVertices(mesh);
            for (int i = 0; i < ListOfTriangles.Count; ++i)
            {
                graph.DrawPolygon(Pens.Black, new PointF[3] { new PointF((float)ListOfTriangles[i].GetVertex(0).X, (float)ListOfTriangles[i].GetVertex(0).Y),
                                                                 new PointF((float)ListOfTriangles[i].GetVertex(1).X, (float)ListOfTriangles[i].GetVertex(1).Y),
                                                                 new PointF((float)ListOfTriangles[i].GetVertex(2).X, (float)ListOfTriangles[i].GetVertex(2).Y) });
                PointF centroid = new PointF(((float)ListOfTriangles[i].GetVertex(0).X + (float)ListOfTriangles[i].GetVertex(1).X + (float)ListOfTriangles[i].GetVertex(2).X) / 3.0f,
                                             ((float)ListOfTriangles[i].GetVertex(0).Y + (float)ListOfTriangles[i].GetVertex(1).Y + (float)ListOfTriangles[i].GetVertex(2).Y) / 3.0f);
                centroid.Y = -centroid.Y;
                centroid.Y -= 5;
                centroid.X -= 5;
                if (ListOfTriangles.Count < 200 && (maxArea >= 800 || maxArea == 0))
                {
                    DrawText(centroid, ListOfTriangles[i].ID.ToString(), gr, true);
                }
            }
            if (ListOfTriangles.Count < 200 && (maxArea >= 800 || maxArea == 0))
            {
                for (int i = 0; i < CT.Count; i++)
                {
                    DrawTextRed(new PointF((float)CT[i].X, (float)-CT[i].Y), CT[i].ID.ToString(), gr);
                }
            }
            ABCD();
            triangulate.Visible = false;
            button1.Visible = false;
            for (int i = 0; i < ListOfTriangles.Count; ++i)
                richTextBox1.Text += (i + 1) + " ) " + Math.Round(ListOfTriangles[i].GetVertex(0).X, 3) + " : " + Math.Round(ListOfTriangles[i].GetVertex(0).Y, 3) + " -  " + Math.Round(ListOfTriangles[i].GetVertex(1).X, 3) + " : " + Math.Round(ListOfTriangles[i].GetVertex(1).Y, 3) + " - " + Math.Round(ListOfTriangles[i].GetVertex(2).X, 3) + " : " + Math.Round(ListOfTriangles[i].GetVertex(2).Y, 3) + "\nA  :  " + A[i] + "   B : " + B[i] + "   C : " + C[i] + "   D : " + D[i] + "\nCenter : " + Center[i] + "\nR : " + Math.Round(R[i], 3) + "\n\n";
            
        }
        public void ABCD()
        {
            foreach(var item in ListOfTriangles)
            {
                A.Add(item.GetVertex(0).X * item.GetVertex(1).Y + item.GetVertex(0).Y * item.GetVertex(2).X + item.GetVertex(1).X * item.GetVertex(2).Y -
                    item.GetVertex(1).Y * item.GetVertex(2).X - item.GetVertex(0).Y * item.GetVertex(1).X - item.GetVertex(2).Y * item.GetVertex(0).X);
                B.Add((Math.Pow(item.GetVertex(0).X, 2) + Math.Pow(item.GetVertex(0).Y, 2)) * item.GetVertex(1).Y + (Math.Pow(item.GetVertex(2).X, 2) + Math.Pow(item.GetVertex(2).Y, 2)) * item.GetVertex(0).Y + (Math.Pow(item.GetVertex(1).X, 2) + Math.Pow(item.GetVertex(1).Y, 2)) * item.GetVertex(2).Y -
                    (Math.Pow(item.GetVertex(2).X, 2) + Math.Pow(item.GetVertex(2).Y, 2)) * item.GetVertex(1).Y - (Math.Pow(item.GetVertex(1).X, 2) + Math.Pow(item.GetVertex(1).Y, 2)) * item.GetVertex(0).Y - (Math.Pow(item.GetVertex(0).X, 2) + Math.Pow(item.GetVertex(0).Y, 2)) * item.GetVertex(2).Y);
                C.Add((Math.Pow(item.GetVertex(0).X, 2) + Math.Pow(item.GetVertex(0).Y, 2)) * item.GetVertex(1).X + (Math.Pow(item.GetVertex(2).X, 2) + Math.Pow(item.GetVertex(2).Y, 2)) * item.GetVertex(0).X + (Math.Pow(item.GetVertex(1).X, 2) + Math.Pow(item.GetVertex(1).Y, 2)) * item.GetVertex(2).X -
                    (Math.Pow(item.GetVertex(2).X, 2) + Math.Pow(item.GetVertex(2).Y, 2)) * item.GetVertex(1).X - (Math.Pow(item.GetVertex(1).X, 2) + Math.Pow(item.GetVertex(1).Y, 2)) * item.GetVertex(0).X - (Math.Pow(item.GetVertex(0).X, 2) + Math.Pow(item.GetVertex(0).Y, 2)) * item.GetVertex(2).X);
                D.Add((Math.Pow(item.GetVertex(0).X, 2) + Math.Pow(item.GetVertex(0).Y, 2)) * item.GetVertex(1).X * item.GetVertex(2).Y + (Math.Pow(item.GetVertex(2).X, 2) + Math.Pow(item.GetVertex(2).Y, 2)) * item.GetVertex(0).X *item.GetVertex(1).Y + (Math.Pow(item.GetVertex(1).X, 2) + Math.Pow(item.GetVertex(1).Y, 2)) * item.GetVertex(2).X * item.GetVertex(0).Y-
                    (Math.Pow(item.GetVertex(2).X, 2) + Math.Pow(item.GetVertex(2).Y, 2)) * item.GetVertex(1).X * item.GetVertex(0).Y- (Math.Pow(item.GetVertex(1).X, 2) + Math.Pow(item.GetVertex(1).Y, 2)) * item.GetVertex(0).X * item.GetVertex(2).Y- (Math.Pow(item.GetVertex(0).X, 2) + Math.Pow(item.GetVertex(0).Y, 2)) * item.GetVertex(2).X * item.GetVertex(1).Y);
                Center.Add(new PointF { X =  (float)(B.Last() / (2.0 * A.Last())), Y = (float)(-C.Last() / (2.0 * A.Last())) });
                R.Add(Math.Sqrt( (Math.Pow(B.Last(),2)+ Math.Pow(C.Last(), 2) - 4 *A.Last()*D.Last() )/(4*Math.Pow(A.Last(),2))));
            }
        }
        private double[][] getBoundsVertices(IMesh mesh)
        {

            double[][] res = new double[mesh.Segments.Count()][];
            for (int i = 0; i < res.Length; ++i)
            {
                res[i] = new double[2];
                res[i][0] = mesh.Segments.ElementAt(i).P0;
                res[i][1] = mesh.Segments.ElementAt(i).P1;
            }
            return res;
        }
        private void DrawText(PointF point, string text, Graphics g, bool isYAxis = false)
        {
            var f = new Font(Font.FontFamily, 7);
            var size = g.MeasureString(text, f);
            var pt = isYAxis
                ? new PointF(point.X + 1, point.Y - size.Height / 2)
                : new PointF(point.X - size.Width / 2, point.Y + 1);
            var rect = new RectangleF(pt, size);
            g.DrawString(text, f, Brushes.Black, point);
        }
        private void DrawTextRed(PointF point, string text, Graphics g, bool isYAxis = false)
        {
            var f = new Font(Font.FontFamily, 7);
            var size = g.MeasureString(text, f);
            var pt = isYAxis
                ? new PointF(point.X + 1, point.Y - size.Height / 2)
                : new PointF(point.X - size.Width / 2, point.Y + 1);
            var rect = new RectangleF(pt, size);
            g.DrawString(text, f, Brushes.Red, point);
        }

    }
}
