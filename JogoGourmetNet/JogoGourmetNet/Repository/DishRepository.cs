using JogoGourmetNet.DTO;

namespace JogoGourmetNet.Repository
{
    public class DishRepository : IDishRepository
    {
        private static List<Dish> dishes = new List<Dish>();

        public void Add(string name, string category)
        {
            var id = Guid.NewGuid();
            AllDishes().Add(new Dish(id, name, category, true));
            ResetChoice();
        }

        public bool ResetChoice()
        {
            dishes.ForEach(prato =>
            {
                prato.UpdateChoice(true);
            });
            return true;
        }

        public int AvaliableChoices()
        {
            return AllDishes().Where(w => w.Choice).Count();
        }

        public bool IsNotAvaliableChoices()
        {
            if (AvaliableChoices() == 0)
            {
                return true;
            }
            return false;
        }

        public void FindSameCategoryAndUpdateChoice(bool choice, string category)
        {
            AllDishes().ForEach(prato =>
            {
                if (prato.Category == category)
                {
                    prato.UpdateChoice(choice);
                }
            });
        }

        public void FindDifferentCategoryAndUpdateChoice(bool choice, string category)
        {
            AllDishes().ForEach(prato =>
            {
                if (prato.Category != category)
                {
                    prato.UpdateChoice(choice);
                }
            });
        }

        public void FindByGuidAndUpdateChoice(Guid guid, bool choice)
        {
            AllDishes().Single(x => x.Id == guid).UpdateChoice(choice);
        }

        public List<Dish> AllDishes()
        {
            return dishes;
        }

        public List<Dish> AllDishesEnabled()
        {
            return dishes.Where(dish => dish.Choice).ToList();
        }
    }
}