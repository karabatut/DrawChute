using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer
{
    List<Vector3> linePositions = new List<Vector3>();
    GameObject line = new GameObject("Line");
    LineRenderer lineRenderer;
    Vector3 previousPosition = new Vector3();

    public LineDrawer()
    {
        //Yaratýlan Line objesine LineRenderer eklenir ve ayarlarý yapýlýr.
        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 2.5f;
        lineRenderer.endWidth = 2.5f;
    }

    public void drawLine(Vector3 position)
    {

        //InputManagerden gelen pozisyonlar kullanýlarak pozisyon listesi doldurulur.
        if(linePositions.Count == 0)
        {
            line.transform.position = position;
            linePositions.Add(position);
        }
        else
        {
            float distance = Vector3.Distance(position, previousPosition);
            //Ekrana basýlý olduðu durumda yüzlerce nokta gelmesini engellemek için 0.1f kadar yakýndaki noktalar listeye alýnmaz.
            if(distance > 0.1f)
            {
                linePositions.Add(position);
            }
        }
        
        //Doldurulan liste kullanýlarak ekrana çizgi çizilir. 
        Vector3[] array = linePositions.ToArray();
        lineRenderer.positionCount = array.Length;
        lineRenderer.SetPositions(array);
        previousPosition = position;
    }

    public void deleteLine()
    {
        //Çizginin silinmesi gerektiðinde noktalar listesi silinir.
        linePositions.Clear();
        lineRenderer.positionCount = 0;
    }

    public List<Vector3> getLinePositions()
    {
        return linePositions;
    }
}
