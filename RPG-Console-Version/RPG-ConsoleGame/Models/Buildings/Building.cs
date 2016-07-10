using RPG_ConsoleGame.Exceptions;

namespace RPG_ConsoleGame.Models.Buildings
{
    using System;
    using Map;
    using RPG_ConsoleGame.Items;
    using Interfaces;
    
    [Serializable()]
    public abstract class Building : GameObject, IBuilding
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
                    throw new IncorrectNameException("Name cannot be null, empty or whitespace.");
                }

                this.name = value;
            }
        }
    }
}
