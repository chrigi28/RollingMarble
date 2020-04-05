using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        [SerializeField] bool randomizeSizes = false;
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
                this.CreateNewInstance(false, false);
            }

            var finishpos = new Vector3(0, this.Ground.transform.localScale.y / 2 + Finish.transform.localScale.y / 2, this.Ground.transform.localScale.z - 5);
            Instantiate(Finish, finishpos, Quaternion.identity);
        }

        private void CreateNewInstance(bool isBorderStop, bool left)
        {
            GameObject ob = Instantiate(Obstacles[1]);

            if (randomizeSizes)
            {
                ob.transform.localScale = this.GetRandomSize(5);
            }

            if (randomizeRotation)
            {
                ob.transform.rotation = Random.rotation;
            }

            if (isBorderStop)
            {
                ob.transform.position = this.GetNextBorderPos(ob, left);
            }
            else
            {
                ob.transform.position = this.GetRandomPosition(ob);
            }
        }

        private Vector3 GetRandomSize(int maxSize)
        {
            var size = Vector3.one;
            size.x *= Random.Range(0.5f, maxSize);
            size.y *= Random.Range(0.5f, 5);
            size.z *= Random.Range(0.5f, maxSize);
            return size;
        }

        private void LoadBorderStopps()
        {
            GameObject ob;
            var groundend = this.Ground.transform.localScale.z;
            while (this.lastZPosition < groundend - 50)
            {
                this.CreateNewInstance(true, true);
                this.CreateNewInstance(true, false);
            }
        }

        private Vector3 GetNextBorderPos(GameObject ob, bool left)
        {
            var newZpos = this.lastZPosition += 50;
            var xPos = this.Ground.transform.localScale.x - ob.transform.localScale.x;
            float newXpos = xPos / 2;
            if (left)
            {
                newXpos *= -1;
            }

            return new Vector3(newXpos, this.Ground.transform.localScale.y / 2 + ob.transform.localScale.y / 2, newZpos);
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
        
        private Vector3 GetRandomPosition(GameObject ob)
        {
            var groundScale = Ground.transform.localScale;

            var minXpos = ob.transform.localScale.x / 2;
            var groundStart = groundScale.x / 2;
            var xPos = Random.Range(-groundStart, groundStart) % (groundScale.x / 2 - ob.transform.localScale.x / 2);
            var zPos = Random.Range(this.firstObstecleDistance, groundScale.z - firstObstecleDistance);
            
            return new Vector3(xPos,groundScale.y / 2 + ob.transform.localScale.y/2, zPos);
        }
    }
}
