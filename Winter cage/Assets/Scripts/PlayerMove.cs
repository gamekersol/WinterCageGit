using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f,rotSpeed = 1f;
    private Rigidbody rb2d; 
    private Transform cameraTransform;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        rb2d = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    float xAngle,yAngle;
    void FixedUpdate()
    {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2d.linearVelocity =transform.TransformDirection( new Vector3(inputs.x * moveSpeed,rb2d.linearVelocity.y, inputs.y * moveSpeed));
        
        Vector3 mouseInputs = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //cameraTransform.Rotate(-mouseInputs.y * Vector3.right, rotSpeed* Mathf.Abs(mouseInputs.y));
        //transform.Rotate(mouseInputs.x * Vector3.up, rotSpeed* Mathf.Abs(mouseInputs.x));
        xAngle -= mouseInputs.y * rotSpeed;
        xAngle = Mathf.Clamp(xAngle, -80f, 60f); // Обмеження кута кам
        yAngle += mouseInputs.x * rotSpeed;

        cameraTransform.localRotation = Quaternion.Euler(new Vector3(xAngle, 0, 0));
        transform.rotation = Quaternion.Euler(new Vector3(0, yAngle, 0));
    }
}
