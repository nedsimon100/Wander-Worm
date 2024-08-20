using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantObject : MonoBehaviour
{
    // Start is called before the first frame update
    public static ConstantObject Instance;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
