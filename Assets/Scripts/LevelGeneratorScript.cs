using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelGeneratorScript : MonoBehaviour
    {
        [SerializeField] public GameObject[] Obstacles;
        [SerializeField] GameObject Player;
        [SerializeField] GameObject Ground;
        [SerializeField] GameObject Finish;
        [SerializeField] int NumbersOfObstacles = 100;
        [SerializeField] bool addBorderStopps = true;
        [SerializeField] bool randomizeRotation = false;
        [SerializeField] int RandomSeed = 282828;
        [SerializeField] float firstObstecleDistance = 12.5f;

        private float lastZPosition;
        ////private List<GameObject> Obstacls;
    
        // Start is called before the first frame update
        void Awake()
        {
            Random.InitState(RandomSeed);
            
            if (this.addBorderStopps)
            {
                this.LoadBorderStopps();
            }

            for (int i = 0; i < NumbersOfObstacles; i++)
            {
                GameObject ob;
                if (i > NumbersOfObstacles / 2) 
                { 
                    ob = Instantiate(Obstacles[0], this.GetRandomPosition(Obstacles[0]), Quaternion.identity);
                }
                else
                {
                    ob = Instantiate(Obstacles[1], this.GetRandomPosition(Obstacles[1]), Quaternion.identity);
                }

                ////this.Obstacls.Add(ob);
            }

            var finishpos = new Vector3(0, this.Ground.transform.localScale.y / 2 + Finish.transform.localScale.y / 2, this.Ground.transform.localScale.z - 5);
            Instantiate(Finish, finishpos, Quaternion.identity);
        }

        private void LoadBorderStopps()
        {
            GameObject ob;
            var groundend = this.Ground.transform.localScale.z;
            while (this.lastZPosition < groundend - 50)
            {
                var x = this.GetNextBorderPos(Obstacles[1]);
                var width = Obstacles[1].transform.localScale.x / 2;
                x.x = this.Ground.transform.localScale.x / 2 - width;
                ob = Instantiate(Obstacles[1], x, Quaternion.identity);
                x.x *= -1;
                ob = Instantiate(Obstacles[1], x, Quaternion.identity);
            }
        }

        private Vector3 GetNextBorderPos(GameObject obstacle)
        {
            var newZpos = this.lastZPosition += 50;
            return new Vector3(0, this.Ground.transform.localScale.y / 2 + obstacle.transform.localScale.y / 2, newZpos);
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

            var minXpos = ob.transform.localScale.x / 2;
            var groundStart = groundScale.x / 2;
            var xPos = Random.Range(-groundStart, groundStart) % (groundScale.x / 2 - ob.transform.localScale.x / 2);
            var zPos = Random.Range(this.firstObstecleDistance, groundScale.z - firstObstecleDistance);
            //todo check if no intersection apply rules
            return new Vector3(xPos,groundScale.y / 2 + ob.transform.localScale.y/2, zPos);
        }
    }
}
