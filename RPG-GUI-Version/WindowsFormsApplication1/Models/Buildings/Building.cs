namespace WindowsFormsApplication1.Models.Buildings
{
    using System;
    using Map;

    [Serializable()]
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
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("name", "Name cannot be null, empty or whitespace.");
                }

                this.name = value;
            }
        }
    }
}
