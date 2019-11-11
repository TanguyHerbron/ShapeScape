namespace Assets.Characters
{
    public class Player : Character
    {
        public Player(string playerName, float playerHP, float playerSpeed)
        {
            this.Name = playerName;
            this.HP = playerHP;
            this.Speed = playerSpeed;
        }
    }
}

