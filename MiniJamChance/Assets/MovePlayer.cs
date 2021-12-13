using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Animator anim;
    public Slider CurTimeUI;
    public int CloneAmount = 3, KeysAmount = 1;
    int curKeys;
    public List<MoveClone> mclones;
    public float moveSpeed = 5f, TimeStamp, StartCloneTime = 10;
    float CurCloneTime, CurrentTime;
    public Vector3 curPos, Dir, lastpos;
    public List <MatrizTM> Moves;
    public GameObject clone, Instuct;
    bool canInput, Won;

    public LayerMask WSM;
    // Start is called before the first frame update
    void Start()
    {
        curKeys = 0;
        curPos = new Vector3(0.5f, 0.5f);
        CurCloneTime = 0;
        CurrentTime = 0;
        CurTimeUI.maxValue = StartCloneTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CurTimeUI.value = CurCloneTime;
        Instuct.SetActive(!canInput);

        if (!canInput)
        {
            if (Input.anyKey)
            {
                if (!Won)
                {
                    if (mclones.Count > 0)
                    {
                        if (mclones.Count < CloneAmount)
                        {
                            ReadMoves();
                            canInput = true;
                        }
                        else
                        {
                            FindObjectOfType<SceneLoader>().Reload();
                        }
                    }
                    else
                    {
                        canInput = true;
                    }
                }
                else if (Won)
                {
                    Debug.Log("Won");
                }
            }
        }
        if (canInput)
        {
            CurCloneTime += Time.deltaTime;
            CurrentTime += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, curPos, moveSpeed * Time.deltaTime);
            Dir.x = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            Dir.y = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

            if (Vector3.Distance(transform.position, curPos) <= .05f)
            {
                if (Mathf.Abs(Dir.x) == 1)
                {
                    if (!Physics2D.OverlapCircle(new Vector2(curPos.x + Dir.x, curPos.y + Dir.y), 0.2f, WSM))
                    {
                        curPos.x += Dir.x;
                    }
                    TimeStamp = CurrentTime;
                    CurrentTime = 0;
                    MatrizTM tmp = new MatrizTM(TimeStamp, curPos);
                    Moves.Add(tmp);
                }
                if (Mathf.Abs(Dir.y) == 1)
                {
                    if (!Physics2D.OverlapCircle(new Vector2(curPos.x + Dir.x, curPos.y + Dir.y), 0.2f, WSM))
                    {
                        curPos.y += Dir.y;
                    }
                    TimeStamp = CurrentTime;
                    CurrentTime = 0;
                    MatrizTM tmp = new MatrizTM(TimeStamp, curPos);
                    Moves.Add(tmp);
                }
            }
            if (Input.GetKeyDown("space") || CurCloneTime >= StartCloneTime)
            {
                CurrentTime = 0;
                CurCloneTime = 0;
                canInput = false;
                Vector3 resetv = new Vector3(0.5f, 0.5f);
                transform.position = resetv;
                curPos = resetv;
                Dir = Vector3.zero;
                MoveClone g = Instantiate(clone, new Vector3(0.5f, 0.5f, 0), Quaternion.identity, null).GetComponent<MoveClone>();
                g.GetMoves(this.Moves);
                mclones.Add(g);

                foreach (MoveClone c in mclones)
                {
                    c.gameObject.transform.position = resetv;
                }
            }
        }
        anim.SetFloat("speed", Dir.magnitude);
    }

    public void ReadMoves()
    {
        foreach (MoveClone c in mclones)
        {
            c.Go();
        }
        Moves.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "End")
        {
            if (curKeys >= KeysAmount)
            {
                Won = true;
                FindObjectOfType<SceneLoader>().Load(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
        else if (collision.tag == "Key")
        {
            Destroy(collision.gameObject);
            curKeys++;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COL");
        Dir = lastpos;
    }
}
