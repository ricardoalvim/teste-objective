using JogoGourmetNet.Exceptions;
using JogoGourmetNet.Repository;

namespace JogoGourmetNet.Utils.RandomUtil
{
    public class RandomUtil : IRandomUtil
    {
        private IDishRepository DishRepository;
        public RandomUtil(IDishRepository dishRepository)
        {
            DishRepository = dishRepository;
        }

        public int Get()
        {
            var allDishes = DishRepository.AllDishes();
            if (allDishes.Count == 0)
            {
                throw new EmptyDishListException();
            }

            Random random = new Random();
            int indiceAleatorio = random.Next(0, allDishes.Count);

            return indiceAleatorio;
        }

        public int GetEnabled()
        {
            var allDishesEnabled = DishRepository.AllDishesEnabled();
            if (allDishesEnabled.Count == 0)
            {
                throw new EmptyDishListException();
            }

            Random random = new Random();
            int indiceAleatorio = random.Next(0, allDishesEnabled.Count);

            return indiceAleatorio;
        }
    }
}