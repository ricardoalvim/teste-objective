using JogoGourmetNet.DTO;

namespace JogoGourmetNet.Service.Internal
{
    public interface ILogicGame
    {
        bool EnableAddDish(string parentDishName);
        bool RightAnswer();
        void Start();
        void UpdateAllDishesWithSameCategory(bool choice, string category);
        Dish GenerateNewDishEnabled(string parentDishName);
        bool ThereExistDish(Dish dish);
    }
}