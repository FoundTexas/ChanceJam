using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MatrizTM
{
    public Vector3 pos;
    public float time;

    public MatrizTM(float t, Vector3 p)
    {
        time = t;
        pos = p;
    }
}


public class MovePlayer : MonoBehaviour
{
    public Slider CurTimeUI;
    public int ConeAmount;
    public List<MoveClone> mclones;
    public float moveSpeed = 5f, TimeStamp, StartCloneTime;
    float CurCloneTime, CurrentTime;
    public Vector3 curPos, Dir;
    public List <MatrizTM> Moves;
    public GameObject clone;
    bool canInput;
    // Start is called before the first frame update
    void Start()
    {
        curPos = new Vector3(0.5f, 0.5f);
        CurCloneTime = 0;
        CurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            CurrentTime += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, curPos, moveSpeed * Time.deltaTime);
            Dir.x = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            Dir.y = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

            if (Vector3.Distance(transform.position, curPos) <= .05f)
            {
                if (Mathf.Abs(Dir.x) == 1)
                {
                    curPos.x += Dir.x;
                    TimeStamp = CurrentTime;
                    CurrentTime = 0;
                    MatrizTM tmp = new MatrizTM(TimeStamp, curPos);
                    Moves.Add(tmp);
                }
                else if (Mathf.Abs(Dir.y) == 1)
                {
                    curPos.y += Dir.y;
                    TimeStamp = CurrentTime;
                    CurrentTime = 0;
                    MatrizTM tmp = new MatrizTM(TimeStamp, curPos);
                    Moves.Add(tmp);
                }
            }
        }
        else if (!canInput && Input.GetKeyDown("space") || CurCloneTime >= StartCloneTime)
        {
            canInput = true;
            Vector3 resetv = new Vector3(0.5f, 0.5f);
            transform.position = resetv;
            curPos = resetv;
            Dir = Vector3.zero;
            foreach (MoveClone c in mclones)
            {
                c.gameObject.transform.position = resetv;
            }
            ReadMoves();
        }
    }

    public void ReadMoves()
    {
        MoveClone g = Instantiate(clone,new Vector3(0.5f,0.5f,0),Quaternion.identity,null).GetComponent<MoveClone>();
        g.GetMoves(this.Moves);
        mclones.Add(g);
        CurrentTime = 0;

        foreach(MoveClone c in mclones)
        {
            c.Go();
        }
        Moves.Clear();
    }
}
