using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector2 inputPos;
    LineDrawer lineDrawer;
    public Canvas parentCanvas;
    MeshCreator meshCreator;
    public bool mouseInput = true;
    bool touched = false;

    // Start is called before the first frame update
    void Start()
    {

        //Line drawer ve mesh creator objeleri yarat�l�r.
        lineDrawer = new LineDrawer();
        meshCreator = new MeshCreator();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseInput)
        {
            //Mouse bas�l� olduk�a imle� pozisyonu kaydedilip line drawer'a g�nderilir.
            if (Input.GetMouseButton(0))
            {
                //ilk t�kland���nda ekranda mesh varsa yok edilir.
                meshCreator.destroyMesh();
                Vector2 movePos;

                //Ekranda bulunan canvas �zerinde mouseun pozisyonu bulunur.
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    parentCanvas.transform as RectTransform,
                    Input.mousePosition, parentCanvas.worldCamera,
                    out movePos);
                Vector3 positionToReturn = parentCanvas.transform.TransformPoint(movePos);
                positionToReturn.z = parentCanvas.transform.position.z - 0.01f;

                //Pozisyonlar line drawera g�nderilir.
                lineDrawer.drawLine(positionToReturn);
            }

            //Mouse b�rak�ld���nda mesh objesi yarat�l�r ve �izgi silinir.
            else if (Input.GetMouseButtonUp(0))
            {
                meshCreator.createObject(lineDrawer);
                lineDrawer.deleteLine();
            }
        }
        else
        {
            //Parmak bas�l� olduk�a parmak pozisyonu kaydedilip line drawer'a g�nderilir.
            if (Input.touchCount > 0)
            {
                //ilk t�kland���nda ekranda mesh varsa yok edilir.
                meshCreator.destroyMesh();
                Vector2 movePos;

                //Ekranda bulunan canvas �zerinde touch inputun pozisyonu bulunur.
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    parentCanvas.transform as RectTransform,
                    Input.GetTouch(0).position, parentCanvas.worldCamera,
                    out movePos);
                Vector3 positionToReturn = parentCanvas.transform.TransformPoint(movePos);
                positionToReturn.z = parentCanvas.transform.position.z - 0.01f;

                //Pozisyonlar line drawera g�nderilir.
                lineDrawer.drawLine(positionToReturn);
                touched = true;
            }

            //Parmak b�rak�ld���nda mesh objesi yarat�l�r ve �izgi silinir.
            else if (touched)
            {
                meshCreator.createObject(lineDrawer);
                lineDrawer.deleteLine();
                touched = false;
            }
        }

        
    }
}
