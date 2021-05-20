using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public GameObject portal;
    GameObject portalRef1;
    GameObject portalRef2;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (portalRef1 != null)
                Destroy(portalRef1);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // convers floats into ints so that the portal stays in one tile space
            // mousePos.x = (int)mousePos.x;
            // mousePos.y = (int)mousePos.y;
            mousePos.z = 0;

            portalRef1 = Instantiate(portal, mousePos, Quaternion.identity);
            portalRef1.name = "Portal1";
            portalRef1.GetComponent<SpriteRenderer>().color = Color.green;

            if (portalRef2 != null)
            {
                portalRef1.GetComponent<Portal>().partnerPortal = portalRef2;
                portalRef2.GetComponent<Portal>().partnerPortal = portalRef1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (portalRef2 != null)
                Destroy(portalRef2);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // convers floats into ints so that the portal stays in one tile space
            // mousePos.x = (int)mousePos.x;
            // mousePos.y = (int)mousePos.y;
            mousePos.z = 0;

            portalRef2 = Instantiate(portal, mousePos, Quaternion.identity);
            portalRef2.name = "Portal2";
            portalRef2.GetComponent<SpriteRenderer>().color = Color.cyan;

            if (portalRef1 != null)
            {
                portalRef1.GetComponent<Portal>().partnerPortal = portalRef2;
                portalRef2.GetComponent<Portal>().partnerPortal = portalRef1;
            }
        }
    }
    public void DestroyPortals()
    {
        Destroy(portalRef1);
        Destroy(portalRef2);
    }
}