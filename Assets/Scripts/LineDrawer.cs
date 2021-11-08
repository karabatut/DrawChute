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
        //Yarat�lan Line objesine LineRenderer eklenir ve ayarlar� yap�l�r.
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

        //InputManagerden gelen pozisyonlar kullan�larak pozisyon listesi doldurulur.
        if(linePositions.Count == 0)
        {
            line.transform.position = position;
            linePositions.Add(position);
        }
        else
        {
            float distance = Vector3.Distance(position, previousPosition);
            //Ekrana bas�l� oldu�u durumda y�zlerce nokta gelmesini engellemek i�in 0.1f kadar yak�ndaki noktalar listeye al�nmaz.
            if(distance > 0.1f)
            {
                linePositions.Add(position);
            }
        }
        
        //Doldurulan liste kullan�larak ekrana �izgi �izilir. 
        Vector3[] array = linePositions.ToArray();
        lineRenderer.positionCount = array.Length;
        lineRenderer.SetPositions(array);
        previousPosition = position;
    }

    public void deleteLine()
    {
        //�izginin silinmesi gerekti�inde noktalar listesi silinir.
        linePositions.Clear();
        lineRenderer.positionCount = 0;
    }

    public List<Vector3> getLinePositions()
    {
        return linePositions;
    }
}
