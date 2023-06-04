using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private void Start()
    {
        SetUpKeys();
        SetUpCamera();
        
    }

    private void LateUpdate()
    {
        CameraMoveInput();
        CameraRotateInput();
        CameraZoomInput();
    }

    #region KEYCODES
    KeyCode keyMoveLeft;
    KeyCode keyMoveRight;
    KeyCode keyMoveUp;
    KeyCode keyMoveDown;
    KeyCode keyMoveLeftAlt;
    KeyCode keyMoveRightAlt;
    KeyCode keyMoveUpAlt;
    KeyCode keyMoveDownAlt;
    KeyCode keyRotateRight;
    KeyCode keyRotateLeft;
    KeyCode keyCameraRun;

    public KeyCode GetKey(string id)
    {
        switch (id)
        {
            #region BASE MOVE
            case "MoveLeft":
                return keyMoveLeft;
            case "MoveRight":
                return keyMoveRight;
            case "MoveUp":
                return keyMoveUp;
            case "MoveDown":
                return keyMoveDown;
            #endregion
            #region ALT MOVE
            case "MoveLeftAlt":
                return keyMoveLeftAlt;
            case "MoveRightAlt":
                return keyMoveRightAlt;
            case "MoveUpAlt":
                return keyMoveUpAlt;
            case "MoveDownAlt":
                return keyMoveDownAlt;

            #endregion

            case "CameraRun":
                return keyCameraRun;

            case "CameraRotateLeft":
                return keyRotateLeft;
            case "CameraRotateRight":
                return keyRotateRight;
        }

        return KeyCode.None;
    }

    void ChangeKey(string id, KeyCode key)
    {

    }

    void SetUpKeys()
    {
        keyMoveLeft = KeyCode.A;
        keyMoveRight = KeyCode.D;
        keyMoveUp = KeyCode.W;
        keyMoveDown = KeyCode.S;

        keyMoveLeftAlt = KeyCode.LeftArrow;
        keyMoveRightAlt = KeyCode.RightArrow;
        keyMoveUpAlt = KeyCode.UpArrow;
        keyMoveDownAlt = KeyCode.DownArrow;

        keyRotateRight = KeyCode.E;
        keyRotateLeft = KeyCode.Q;

        keyCameraRun = KeyCode.LeftShift;

    }
    #endregion

    #region CAMERA CONTROL
    [Separator("CAMERA BASE")]
    [SerializeField] Transform cameraRig;
    public float movementTime;
    public float cameraRange;
    Vector3 originalPos;

    [Separator("CAMERA MOVE")]
    public float movementSpeed;
    public float fastMovementSpeed;
    Vector3 newPos;

    [Separator("CAMERA ROTATION")]
    public float rotationAmount;
    Quaternion newRotation;

    [Separator("CAMERA ZOOM")]
    [SerializeField] Transform cam;
    public Vector3 zoomAmount;
    public Vector3 newZoom;
    [SerializeField] float minZoom = -10;
    [SerializeField] float maxZoom = 10;
    void SetUpCamera()
    {
        newPos = cameraRig.position;
        originalPos = cameraRig.position;
        newRotation = cameraRig.rotation;
        newZoom = cam.localPosition;
    }

    void CameraMoveInput()
    {
        float currentSpeed = 0;
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(GetKey("CameraRun")))
        {
            currentSpeed = fastMovementSpeed;
        }
        else
        {
            currentSpeed = movementSpeed;
        }

        if(Input.GetKey(GetKey("MoveUp")) || Input.GetKey(GetKey("MoveUpAlt")))
        {
            dir += cameraRig.forward * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(GetKey("MoveDown")) || Input.GetKey(GetKey("MoveDownAlt")))
        {
            dir += cameraRig.forward * -currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(GetKey("MoveRight")) || Input.GetKey(GetKey("MoveRightAlt")))
        {
             dir += cameraRig.right * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(GetKey("MoveLeft")) || Input.GetKey(GetKey("MoveLeftAlt")))
        {
            dir += cameraRig.right * -currentSpeed * Time.deltaTime;
        }

        newPos += dir;
        cameraRig.position = Vector3.Lerp(cameraRig.position, newPos, Time.deltaTime * movementTime);      
        
    }

    void CameraRotateInput()
    {
        if (Input.GetKey(GetKey("CameraRotateRight")))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(GetKey("CameraRotateLeft")))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        cameraRig.rotation = Quaternion.Lerp(cameraRig.rotation, newRotation, Time.deltaTime * movementTime);
    }

    void CameraZoomInput()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
          if(newZoom.y != minZoom) newZoom += zoomAmount;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
          if(newZoom.y != maxZoom) newZoom -= zoomAmount;
        }

        cam.localPosition = Vector3.Lerp(cam.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    bool CameraTooFar(Vector3 dir)
    {
        return Vector3.Distance(newPos + dir, originalPos) > cameraRange;
    }


    #endregion
}
