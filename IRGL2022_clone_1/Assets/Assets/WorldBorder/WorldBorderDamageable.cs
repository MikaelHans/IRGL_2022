using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class WorldBorderDamageable : MonoBehaviour
    {
        public GameObject worldBorder;
        public float DPS = 5;
        public float tickTime = 1;
        [SerializeField]
        private float totalTime = 0;
        private Player player;
        // Start is called before the first frame update
        void Start()
        {
            player = gameObject.GetComponent<Player>();
            worldBorder = GameObject.FindGameObjectWithTag("WorldBorder");
        }

        // Update is called once per frame

        bool IsInside(Collider c, Vector3 point)
        {
            Vector3 closest = c.ClosestPoint(point);
            // Because closest=point if inside - not clear from docs I feel
            return closest == point;
        }
        void Update()
        {
            totalTime += Time.deltaTime;
            if (totalTime > tickTime)
            {
                if(worldBorder != null)
                {
                    if (!IsInside(worldBorder.GetComponent<MeshCollider>(), transform.position))
                    {
                        player.TakeDamage(DPS, -999);
                        totalTime = 0;
                    }
                }                
            }
        }
    }
}
