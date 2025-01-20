namespace JogoGourmetNet.DTO
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool Choice { get; set; }

        public Dish(Guid id, string name, string category, bool choice)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Choice = choice;
        }

        public bool UpdateChoice(bool choice)
        {
            Choice = choice;
            return true;
        }
    }
}