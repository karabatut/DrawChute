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

        //Line drawer ve mesh creator objeleri yaratýlýr.
        lineDrawer = new LineDrawer();
        meshCreator = new MeshCreator();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseInput)
        {
            //Mouse basýlý oldukça imleç pozisyonu kaydedilip line drawer'a gönderilir.
            if (Input.GetMouseButton(0))
            {
                //ilk týklandýðýnda ekranda mesh varsa yok edilir.
                meshCreator.destroyMesh();
                Vector2 movePos;

                //Ekranda bulunan canvas üzerinde mouseun pozisyonu bulunur.
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    parentCanvas.transform as RectTransform,
                    Input.mousePosition, parentCanvas.worldCamera,
                    out movePos);
                Vector3 positionToReturn = parentCanvas.transform.TransformPoint(movePos);
                positionToReturn.z = parentCanvas.transform.position.z - 0.01f;

                //Pozisyonlar line drawera gönderilir.
                lineDrawer.drawLine(positionToReturn);
            }

            //Mouse býrakýldýðýnda mesh objesi yaratýlýr ve çizgi silinir.
            else if (Input.GetMouseButtonUp(0))
            {
                meshCreator.createObject(lineDrawer);
                lineDrawer.deleteLine();
            }
        }
        else
        {
            //Parmak basýlý oldukça parmak pozisyonu kaydedilip line drawer'a gönderilir.
            if (Input.touchCount > 0)
            {
                //ilk týklandýðýnda ekranda mesh varsa yok edilir.
                meshCreator.destroyMesh();
                Vector2 movePos;

                //Ekranda bulunan canvas üzerinde touch inputun pozisyonu bulunur.
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    parentCanvas.transform as RectTransform,
                    Input.GetTouch(0).position, parentCanvas.worldCamera,
                    out movePos);
                Vector3 positionToReturn = parentCanvas.transform.TransformPoint(movePos);
                positionToReturn.z = parentCanvas.transform.position.z - 0.01f;

                //Pozisyonlar line drawera gönderilir.
                lineDrawer.drawLine(positionToReturn);
                touched = true;
            }

            //Parmak býrakýldýðýnda mesh objesi yaratýlýr ve çizgi silinir.
            else if (touched)
            {
                meshCreator.createObject(lineDrawer);
                lineDrawer.deleteLine();
                touched = false;
            }
        }

        
    }
}
