using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] Transform playerCamera;

    [Header("Look Settings")]
    [SerializeField] float sensitivityX;
    [SerializeField] float sensitivityY;
    [SerializeField] float mouseLookSmooth;

    // mouse parameters
    float mouseX;
    float mouseY;
    float currentRotationX;
    float currentRotationY;
    float mouseLookDampX = 0.0f;
    float mouseLookDampY = 0.0f;


    private void LateUpdate()
    {
        Looking();
    }

    void Looking()
    {
        GetInput();
        transform.localEulerAngles = new Vector3(0f, currentRotationY, 0f);
        playerCamera.localEulerAngles = new Vector3(currentRotationX, 0f, 0f);
    }
  
    void GetInput()
    {
        // position of mouse cursor
        mouseX += Input.GetAxisRaw("Mouse X") * sensitivityX * 0.01f;
        mouseY -= Input.GetAxisRaw("Mouse Y") * sensitivityY * 0.01f;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // smooth mouse rotation (prevents jitter)
        currentRotationX = Mathf.SmoothDamp(currentRotationX, mouseY, ref mouseLookDampX, mouseLookSmooth);
        currentRotationY = Mathf.SmoothDamp(currentRotationY, mouseX, ref mouseLookDampY, mouseLookSmooth);
    }
}
