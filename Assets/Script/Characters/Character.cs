namespace Assets.Characters
{
    /// <summary>
    /// Abstract class for every characters in the game
    /// </summary>
    public abstract class Character
    {
        private int id;
        private float hp;
        private float speed;
        private bool invicible = false;
        private bool damaged = false;
        private string name;

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




    }
}