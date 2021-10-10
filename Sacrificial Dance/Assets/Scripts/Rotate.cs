using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,Speed * Time.deltaTime);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.rotation = Quaternion.identity;
        }
    }
}
