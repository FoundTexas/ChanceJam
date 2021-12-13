using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitcky : MonoBehaviour
{
    public static Sitcky inst;
    // Start is called before the first frame update
    void Start()
    {
        if(inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(this.gameObject);

        }

            DontDestroyOnLoad(inst);
        
    }

}
