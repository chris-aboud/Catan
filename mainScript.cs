using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainScript : MonoBehaviour
{
    public GameObject[] hexGameObject;
    public float size; /* should be equal to 1.120 */ 
    public float width;
    public float height;
    public float horizShift;
    public int rowNum;
    public int totalRows;
    public int totalHexes;
    public int curMax;
    public int middle;
    public int middleHexes;
    public bool organized;
    public bool isRegular;    
    // Start is called before the first frame update
    void Start()
    {
        rowNum = 0;
    }

    /* Hex Terrain Count
     * Brick - 3
     * Ore - 3
     * Forest - 4
     * Grain - 4
     * Wool - 4
     * Desert - 1
     * */
     
    void organizeHexes()
    {
        /* setup values */
    width = Mathf.Sqrt(3) * size;
        height = 2 * size;
        if (isRegular)
        {
            totalRows = 5;
            middle = 3;
            middleHexes = 5;
            totalHexes = 19;
            horizShift = 4f;
            Camera.main.orthographicSize = 5.85f;
        }
        else
        {
            totalRows = 7;
            middle = 4;
            middleHexes = 6;
            totalHexes = 30;
            horizShift = 5.5f;
            Camera.main.orthographicSize = 7.6f;
        }
        rowNum = 0;

        /* rotates the hexagon sprite to pointed top */
        int index = 0;
        foreach (GameObject hex in hexGameObject)
        {
            if(hex.transform.rotation.eulerAngles.z != 90 && hex.transform.rotation.eulerAngles.z != 270)
            { 
                hex.transform.Rotate(0, 0, 90);
            }
            if (isRegular && index >= totalHexes)
            {
                hex.SetActive(false);
            }
            else
            {
                hex.SetActive(true);
            }
            index++;
        }

        // places each hex in proper positions according to row and column number
        int k = 0;
        for (int row = middle; row > 0; row++)
        {
            for (int col = 1; col <= middleHexes - Mathf.Abs(row - middle); col++)
            {
                hexGameObject[k].transform.position = new Vector3(-horizShift * width + ((width * (col - 1)) + width*((middle-1) + .5f*Mathf.Abs(row - middle))), (row - middle)*.75f*height, 0);
                //hexGameObject[k].GetComponent<Renderer>().material
                k++;
            }
            
            if (row < middle)
            {
                row = row - 2;
            }
            if (row == totalRows)
            {
                row = middle - 2;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (organized)
        {
            organizeHexes();
            organized = false;
        }
    }
}
