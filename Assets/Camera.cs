using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraMove : MonoBehaviour
{
    public float Sens;

    public Transform orientation;
    public Transform CameraPos;

    float xRotation;
    float yRotation;

    public InputActionAsset PlayerControls;

    private InputAction lookAction;

    Vector2 lookInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lookAction = PlayerControls.FindActionMap("Gameplay").FindAction("CameraMovement");

        lookAction.Enable();


    }

    // Update is called once per frame
    void Update()
    {
        lookInput = lookAction.ReadValue<Vector2>();
        Debug.Log("Camera" + lookInput);
        xRotation += lookInput.x * Sens * Time.deltaTime;


        yRotation -= lookInput.y * Sens * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        CameraPos.transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, xRotation, 0);
    }
}