namespace Assets.Characters.Ennemies
{
    public class Ennemy : Character
    {
        // Start is called before the first frame update
        public Ennemy(string ennemyName, float ennemyHP)
        {
            this.Name = ennemyName;
            this.HP = ennemyHP;
        }
    }
}

