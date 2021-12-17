using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitcky : MonoBehaviour
{
    public bool Replace;
    public static Sitcky inst;
    // Start is called before the first frame update
    void Start()
    {
        if (!Replace)
        {
            Debug.Log("D");
            if (inst == null)
            {
                inst = this;
            }
            else if (inst != this)
            {
                Destroy(this.gameObject);

            }
        }
        else if (Replace)
        {
            if (inst == null)
            {
                inst = this;
            }
            else if (inst != this)
            {
                Debug.Log("R");
                Destroy(inst.gameObject);
                inst = this;

            }
        }
        DontDestroyOnLoad(inst);
    }

}
