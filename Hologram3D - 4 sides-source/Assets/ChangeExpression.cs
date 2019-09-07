using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeExpression : MonoBehaviour
{
    private float value;
    private int expressionID; 
    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        expressionID = -1; 
    }

    // Update is called once per frame
    void Update()
    {
        if (expressionID != -1 && value < 100  && expressionID != 6)
        {
            SkinnedMeshRenderer renderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
            value += 5;
            renderer.SetBlendShapeWeight(expressionID, value);
        }
    }
    void changeExpression(float[] input)
    {
      
        SkinnedMeshRenderer renderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        int id = (int)input[0];
        if (id >= 0 && id <= 6)
        {
            value = 0;
            expressionID = id; 
            Debug.Log("Change expression to : " + expressionID);
            for (int i = 0; i < 6; ++i)
            {
                renderer.SetBlendShapeWeight(i, 0);
            }
        }
       
    }
}
