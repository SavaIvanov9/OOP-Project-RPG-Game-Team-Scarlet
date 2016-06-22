namespace RPG_ConsoleGame.Models.Buildings
{
    using System;
    using Items;
    using Map;

    public abstract class Building : GameObject
    {
        private string name;

        protected Building(Position position, char objectSymbol, string name) : base(position, objectSymbol)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("name", "Name cannot be null, empty or whitespace.");
                }

                this.name = value;
            }
        }
    }
}
