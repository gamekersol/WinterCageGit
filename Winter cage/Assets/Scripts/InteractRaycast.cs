using UnityEngine;
using UnityEngine.UI;

public class InteractRaycast : MonoBehaviour
{
    [SerializeField] private float distance=5, dragForce = 3;
    [SerializeField] private RectTransform pressFlabel;
    private RaycastHit hit;
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection( Vector3.forward )* distance, Color.red);

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,distance * distance,LayerMask.GetMask("Interact"));
    }
    private float fTouchDist;
    private GameObject pastObj;
    private void Update()
    {
      //  if (hit.collider!=null&&hit.collider.gameObject.tag == "Interact")
        if (hit.collider!=null)
        {
            GameObject hitObj = hit.collider.transform.parent.gameObject;

            pressFlabel.gameObject.SetActive(true);
            pressFlabel.anchoredPosition = Camera.main.WorldToScreenPoint(hitObj.transform.position) - new Vector3(Screen.width/2,Screen.height/2);
            if (Input.GetKeyDown(KeyCode.F))
            {
                //interact
            }
            if (Input.GetMouseButtonDown(0))
            {
                fTouchDist = (hitObj.transform.position-transform.position).magnitude;
                hitObj.GetComponent<Rigidbody>().useGravity = false;
                pastObj = hitObj;
            }
            if (Input.GetMouseButton(0))
            {
                //drag object
                //hit.distance;

                Vector3 diff = transform.position + transform.TransformDirection(Vector3.forward) * fTouchDist - hitObj.transform.position;
                float diffMag = diff.magnitude;
                hitObj.transform.Translate(diff.normalized * Time.deltaTime * dragForce * diffMag);
            }
            if (Input.GetMouseButtonUp(0)) {
                hitObj.GetComponent<Rigidbody>().useGravity = true;
                pastObj = null;
            }
        }
        else
        {
            if (pastObj != null){ pastObj.GetComponent<Rigidbody>().useGravity = true; pastObj=null; }
            pressFlabel.gameObject.SetActive(false);
        }
    }
}
