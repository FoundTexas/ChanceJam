using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClone : MonoBehaviour
{
    public int speed = 3;
    public List<MatrizTM> Moves;
    public Vector3 tar;

    public void GetMoves(List<MatrizTM> m)
    {
        foreach (MatrizTM i in m)
        {
            MatrizTM tmp = i;
            Moves.Add(tmp);
        }
    }

    public void Go()
    {
        StopAllCoroutines();
        StartCoroutine(ReadMoves());
    }

    public IEnumerator ReadMoves()
    {
        foreach (MatrizTM m in Moves)
        {
            yield return new WaitForSeconds(m.time);
            tar = m.pos;
        }
    }

    void Update()
    {

        if (Vector3.Distance(transform.position, tar) > 0.01f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, tar, step);
        }
    }
}
