namespace Assets.Entities
{
    public class Character : Entity
    {
        public Character(string entityName, float entityHP, float entitySpeed)
        {
            this.Name = entityName;
            this.HP = entityHP;
            this.Speed = entitySpeed;
        }
    }
}

