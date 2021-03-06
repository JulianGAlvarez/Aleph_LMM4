using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using OpenCvSharp.Demo;

public class CountorFinder : WebCamera
{
    [SerializeField] private float Threshold = 96.4f;
    [SerializeField] private bool ShowProcessedImage = true;
    [SerializeField] private float CurveAccuracy = 10f;
    [SerializeField] private float MinArea = 5000f;

    [SerializeField] private PolygonCollider2D PolygonCollider;
    private Vector2[] vectorList;

    private Mat image;
    private Mat processImage = new Mat();
    private Point[][] contours;
    private HierarchyIndex[] hierarchy;


    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
       image = OpenCvSharp.Unity.TextureToMat(input);

       Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
       Cv2.Threshold(processImage, processImage, Threshold, 255, ThresholdTypes.BinaryInv);
       Cv2.FindContours(processImage, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);


       PolygonCollider.pathCount = 0;

       foreach(Point[] contour in contours)
        {
            Point[] points = Cv2.ApproxPolyDP(contour, CurveAccuracy, true);
            var area = Cv2.ContourArea(contour);

            if(area > MinArea)
            {
                drawContour(processImage, new Scalar(125, 125, 125), 2, points);

                PolygonCollider.pathCount++;
                PolygonCollider.SetPath(PolygonCollider.pathCount - 1, toVector2(points));
            }
        }

       if(output == null) 
        {
            output = OpenCvSharp.Unity.MatToTexture(ShowProcessedImage ? processImage : image);
        } else
        {
            OpenCvSharp.Unity.MatToTexture(ShowProcessedImage ? processImage : image, output);
        }
       return true;
    }

    private Vector2[] toVector2(Point[] points)
    {
        vectorList = new Vector2[points.Length];
        for(int i = 0; i < points.Length; i++)
        {
            vectorList[i] = new Vector2(points[i].X, points[i].Y);
        }
        return vectorList;
    }
    private void drawContour(Mat Image, Scalar Color, int Thickness, Point[] Points)
    {
        for(int i = 1; i < Points.Length; i++)
        {
            Cv2.Line(Image,Points[i-1], Points[i], Color, Thickness);
        }
        Cv2.Line(Image, Points[Points.Length - 1], Points[0], Color, Thickness);
    }
}
