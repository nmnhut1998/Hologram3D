using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    private int frameCount; 
    void Start()
    {
        frameCount = 0;         
    }

    // Update is called once per frame
    void Update()
    {
        frameCount += 1; 
        if (frameCount == 180)
        {
            this.gameObject.SetActive(false); 
        }
    }
}
