using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class DropDownParent : MonoBehaviour
{
    public GameObject[] Blocks;
    public GameObject RotationButtons;
    public GameObject PositionButtons;



    void Start()
    {

        Blocks[0].SetActive(true);

    }
    void Update()
    {


    }

    public void changeBlock(int index)
    {
        showHide(index);
    }

    private void showHide(int index)
    {
        for (int i = 0; i < Blocks.Length; i++)
        {
            if (i != index)
            {
                Blocks[i].SetActive(false);
            }
            else
                Blocks[i].SetActive(true);


        }
    }

}
