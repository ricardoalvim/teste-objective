using JogoGourmetNet.DTO;

namespace JogoGourmetNet.Repository
{
    public interface IDishRepository
    {
        public List<Dish> AllDishes();
        public List<Dish> AllDishesEnabled();
        void Add(string name, string category);
        void FindByGuidAndUpdateChoice(Guid guid, bool choice);
        bool ResetChoice();
        int AvaliableChoices();
        bool IsNotAvaliableChoices();
        void FindSameCategoryAndUpdateChoice(bool choice, string category);
        void FindDifferentCategoryAndUpdateChoice(bool choice, string category);
    }
}