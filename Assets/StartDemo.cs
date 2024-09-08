using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeInstance.Instance.LoadDll("HotRefresh");


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            ILRuntimeInstance.Instance.appdomain.Invoke("HotRefresh.Knapsack", "CreateCanvas", null, null);
        }

    }
}
