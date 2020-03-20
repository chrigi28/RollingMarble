using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneScript : BaseScript
{
    public GameObject Block1x5y;
    public GameObject Block2x5y;
    public GameObject Ground;
    public GameObject Finish;
    public int RandomSeed = 282828;
    public float firstObstecleDistance = 12.5f;

    private List<GameObject> Obstacls;
    

    // Start is called before the first frame update
    void Awake()
    {
        Random.InitState(RandomSeed);
        this.Obstacls = new List<GameObject>();
        int noOfObst = 100;
        int count1 = Random.Range(25, noOfObst);
        int count2 = noOfObst - count1;
        for (int i = 0; i < noOfObst; i++)
        {
            GameObject ob;
            if (count1 > 0 && count1 % 2 == 0)
            {
                ob = Instantiate(Block1x5y, this.GetRandomPosition(Block1x5y), Quaternion.identity);
            }
            else
            {
                ob = Instantiate(Block2x5y, this.GetRandomPosition(Block2x5y), Quaternion.identity);
            }

            this.Obstacls.Add(ob);
        }

        var finishpos = new Vector3(0, this.Ground.transform.localScale.y / 2 + Finish.transform.localScale.y / 2,
            this.Ground.transform.localScale.z - 5);
        Instantiate(Finish, finishpos
            , Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 GetRandomPosition(GameObject ob)
    {
        var groundScale = Ground.transform.localScale;
        var minXpos= ob.transform.localScale.x /2;
        var groundStart = groundScale.x / 2;
        var xPos = Random.Range(-groundStart + minXpos, groundStart - minXpos);
        var zPos = Random.Range(this.firstObstecleDistance, groundScale.z - firstObstecleDistance);

        //todo check if no intersection apply rules
        return new Vector3(xPos,groundScale.y / 2 + ob.transform.localScale.y/2, zPos);
    }
}
