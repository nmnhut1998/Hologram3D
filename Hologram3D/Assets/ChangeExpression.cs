using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeExpression : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void changeExpression(float[] input)
    {
        SkinnedMeshRenderer renderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        var expressionID = (int)input[0];
        if (expressionID != 6)
        {
            renderer.SetBlendShapeWeight(expressionID, input[1]);
        }
        else
        {
            //neutral expression
            for (int i =0; i < 6; ++i)
            {
                renderer.SetBlendShapeWeight(expressionID, 0);

            }
        }
    }
}
