using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace Assets.Entities
{
    /// <summary>
    /// Abstract class for every characters in the game
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        private int id;
        private float hp;
        private float speed;
        private bool invicible = false;
        private bool damaged = false;
        private string name;
        private Rigidbody2D rigidBody;

        private List<Location> path;

        public bool usePathFinder = false;

        /***
            GETTERS AND SETTERS
        ***/

        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public float HP
        {
            get
            {
                return hp;
            }

            set
            {
                hp = value;
            }
        }

        public float Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed= value;
            }
        }

        public bool Invicible
        {
            get
            {
                return invicible;
            }

            set
            {
                invicible = value;
            }
        }

        public bool Damaged
        {
            get
            {
                return damaged;
            }

            set
            {
                damaged= value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Rigidbody2D RigidBody { get => rigidBody; set => rigidBody= value; }

        /// <summary>
        /// Checks if the character is dead
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsDead()
        {
            if ( hp <= 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Reduces health of a character by one
        /// </summary>
        public void ApplyDamage(float amount)
        {
            if ( !invicible )
            {
                damaged = true;
                hp  -= amount;

                if (hp < 0)
                {
                    Kill();
                } 
            }
        }

        /// <summary>
        /// Instantly add the amount to the character hp
        /// </summary>
        public void Heal(float amount)
        {
            hp  += amount;
        }

        /// <summary>
        /// Adds the amount of hp to the character over time
        /// </summary>
        public void RegenerateHealth(float dot, float amount)
        {
            hp  += amount;
        }

        /// <summary>
        /// Reduces the amount of hp to the character over time
        /// </summary>
        public void Bleed(float dot, float amount)
        {
            hp  -= amount;
        }

        /// <summary>
        /// Attack method
        /// </summary>
        public void Attack(float damage, Character target)
        {
            target.HP  -= damage;
        }

        public void Kill()
        {
            this.hp = 0;
        }

        public Vector3 GetDirection(Vector3 playerVec)
        {
            if (usePathFinder)
            {
                Location pathTarget = new Location(Mathf.FloorToInt(playerVec.x)
                    , Mathf.FloorToInt(playerVec.y));

                Location start = new Location(Mathf.FloorToInt(transform.position.x)
                    , Mathf.FloorToInt(transform.position.y));

                Tilemap groundTilemap = GameObject.FindGameObjectsWithTag("Walkable")[0].GetComponent<Tilemap>();
                Tilemap corridorTilemap = GameObject.FindGameObjectsWithTag("Walkable")[1].GetComponent<Tilemap>();

                AStarPathfinder aStar = new AStarPathfinder(start, pathTarget, groundTilemap, corridorTilemap);

                aStar.ComputePath();

                path = aStar.GetPath();

                if (path != null && path.Count > 0)
                {
                    return (new Vector3(path[0].X + 0.5f, path[0].Y + 0.5f, 0) - transform.position).normalized;
                }
            }

            return (playerVec - transform.position).normalized;
        }

        public Vector3 GetDirection(Transform playerTransform)
        {
            return GetDirection(playerTransform.position);
        }

        /// <summary>
        /// Moves the character towards the target
        /// Also orienting the character towards the targer
        /// </summary>
        /// <param name="target">The target Vector3 that the entity should move towards</param>
        public void MoveTo(Vector3 target, float movingSpeed)
        {
            Vector3 direction = GetDirection(target);
            this.rigidBody.velocity = Vector2.zero;
            this.rigidBody.AddForce(direction * movingSpeed);
        }

        /// <summary>
        /// Stops the character movements
        /// </summary>
        public void StopMoving()
        {
            this.rigidBody.velocity = Vector2.zero;
        }

    }
}