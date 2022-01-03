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
        private float totalTime = 0;
        // Start is called before the first frame update
        void Start()
        {
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
                if (!IsInside(worldBorder.GetComponentInChildren<CapsuleCollider>(), transform.position))
                {
                    this.GetComponent<Health>().TakeDamage(DPS, worldBorder);
                    totalTime = 0;
                }
            }
        }
    }
}
