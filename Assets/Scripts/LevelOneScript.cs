using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelOneScript : BaseScript
    {
        [SerializeField]
        public GameObject[] Obstacles;
        public GameObject Player;
        public GameObject Ground;
        public GameObject Finish;
        [SerializeField] int NumbersOfObstacles = 100;
        public int RandomSeed = 282828;
        public float firstObstecleDistance = 12.5f;

        ////private List<GameObject> Obstacls;
    

        // Start is called before the first frame update
        void Awake()
        {
            Random.InitState(RandomSeed);
            ////this.Obstacls = new List<GameObject>();
        
            int count1 = Random.Range(25, NumbersOfObstacles);
            int count2 = NumbersOfObstacles - count1;
            for (int i = 0; i < NumbersOfObstacles; i++)
            {
                GameObject ob;
                if (i % 2 == 0)
                {
                    ob = Instantiate(Obstacles[0], this.GetRandomPosition(Obstacles[0]), Quaternion.identity);
                }
                else
                {
                    ob = Instantiate(Obstacles[1], this.GetRandomPosition(Obstacles[1]), Quaternion.identity);
                }

                ////this.Obstacls.Add(ob);
            }

            var finishpos = new Vector3(0, this.Ground.transform.localScale.y / 2 + Finish.transform.localScale.y / 2,
                this.Ground.transform.localScale.z - 5);
            Instantiate(Finish, finishpos
                , Quaternion.identity);
        }

        void Start()
        {
            SetPlayerPosition();
        }

        private void SetPlayerPosition()
        {
            var pos = Ground.transform.position;
            pos.y = 1;
            pos.z -= Ground.transform.localScale.z / 2;
            this.Player.transform.position = pos;
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
}
