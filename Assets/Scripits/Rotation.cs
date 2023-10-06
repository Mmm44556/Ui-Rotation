using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rotation : MonoBehaviour
{
    private Button[] buttons;
    private Transform BlockTransform;
    private string currentObject;
    [SerializeField] GameObject[] Blocks;
    [SerializeField] Camera mainCamera;
    public float rotationSpeed;


    private void Start()
    {
        mainCamera = Camera.main;
        currentObject = gameObject.name;
        buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }
    private void Update()
    {


    }
    private void OnButtonClick(Button clickedButton)
    {

        bool isPositionBtns = currentObject == "PositionBtns";
        bool isRotationBtns = currentObject == "RotationBtns";
        for (int i = 0; i < Blocks.Length; i++)
        {
            if (Blocks[i].activeSelf)
            {

                //獲取相機當前POV跟位置
                Vector3 cameraPosition = mainCamera.transform.position;
                Vector3 cameraForward = mainCamera.transform.forward;
                Vector3 centerPoint = cameraPosition + cameraForward * 5f;
                float camFOV = mainCamera.fieldOfView;
                float maxY = cameraPosition.y + Mathf.Tan(camFOV * 0.5f * Mathf.Deg2Rad);
                float minY = cameraPosition.y - Mathf.Tan(camFOV * 0.5f * Mathf.Deg2Rad);
                float maxX = cameraPosition.x + maxY * mainCamera.aspect;
                float minX = cameraPosition.x - maxY * mainCamera.aspect;
                BlockTransform = Blocks[i].transform;
                Vector3 currentPosition = BlockTransform.position;
                Quaternion BlockRotation = BlockTransform.rotation;
                if (isRotationBtns && clickedButton.name == "Up" || clickedButton.name == "Left")
                {
                    //向上、左轉
                    rotationSpeed = 30f;
                }
                else
                {
                    //向右、下轉
                    rotationSpeed = -30f;
                }
                //建構旋轉角度
                float newRotationAngleX = BlockTransform.eulerAngles.x + rotationSpeed * Time.deltaTime;
                float newRotationAngleY = BlockTransform.eulerAngles.y + rotationSpeed * Time.deltaTime;
                // float newRotationAngleZ = BlockTransform.eulerAngles.z + rotationSpeed * Time.deltaTime;

                //當前方塊X,Y軸
                float newX = currentPosition.x;
                float newY = currentPosition.y;
                switch (clickedButton.name)
                {


                    case "Up":
                        if (newY < maxX - 1 && isPositionBtns) BlockTransform.position = NewPosition(newX, newY + .5f, 0);
                        if (isRotationBtns)
                        {
                            Quaternion newRotation = Quaternion.Euler(newRotationAngleX, BlockRotation.eulerAngles.y, BlockRotation.eulerAngles.z);
                            Blocks[i].transform.rotation = newRotation;
                        }
                        break;
                    case "Down":
                        if (newY > minX + 4 && isPositionBtns) BlockTransform.position = NewPosition(newX, newY - .5f, 0);
                        if (isRotationBtns)
                        {
                            Quaternion newRotation = Quaternion.Euler(newRotationAngleX, BlockRotation.eulerAngles.y, BlockRotation.eulerAngles.z);
                            Blocks[i].transform.rotation = newRotation;
                        }
                        break;
                    case "Left":
                        if (newX > minY - 2 && isPositionBtns) BlockTransform.position = NewPosition(newX - .5f, newY, 0);
                        if (isRotationBtns)
                        {
                            Quaternion newRotation = Quaternion.Euler(BlockRotation.eulerAngles.x, newRotationAngleY, BlockRotation.eulerAngles.z);
                            Blocks[i].transform.rotation = newRotation;
                        }
                        break;
                    case "Right":
                        if (newX < minY + 1 && isPositionBtns) BlockTransform.position = NewPosition(newX + .5f, newY, 0);
                        if (isRotationBtns)
                        {
                            Quaternion newRotation = Quaternion.Euler(BlockRotation.eulerAngles.x, newRotationAngleY, BlockRotation.eulerAngles.z);
                            Blocks[i].transform.rotation = newRotation;
                        }
                        break;
                    case "Reset":
                        BlockTransform.position = centerPoint;
                        Blocks[i].transform.rotation = Quaternion.Euler(0,0,0);
                        break;     
                }
            }else{
                Blocks[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            // BlockTransform.position = centerPoint;
            
        }
    }

    private Vector3 NewPosition(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }

}
