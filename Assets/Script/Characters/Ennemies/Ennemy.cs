namespace Assets.Characters.Ennemies
{
    public class Ennemy : Character
    {
        public Ennemy(string ennemyName, float ennemyHP, float ennemySpeed)
        {
            this.Name = ennemyName;
            this.HP = ennemyHP;
            this.Speed = ennemySpeed;
        }
    }
}

