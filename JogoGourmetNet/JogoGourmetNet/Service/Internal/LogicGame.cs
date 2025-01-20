using JogoGourmetNet.DTO;
using JogoGourmetNet.Presentation;
using JogoGourmetNet.Repository;
using JogoGourmetNet.Utils.Message;
using JogoGourmetNet.Utils.RandomUtil;

namespace JogoGourmetNet.Service.Internal
{
    public class LogicGame : ILogicGame
    {
        private IUserInterface userInterface;
        private IDishRepository dishRepository;
        private IRandomUtil randomUtil;

        public LogicGame(IUserInterface interacaoUsuario, IDishRepository dishRepository, IRandomUtil randomUtil)
        {
            this.userInterface = interacaoUsuario;
            this.dishRepository = dishRepository;
            this.randomUtil = randomUtil;
        }

        private bool UserAskToExit(string message)
        {
            return message == MessagePortuguese.EXIT ? true : false;
        }

        private bool AddDish(string parentDishName)
        {
            userInterface.Show(MessagePortuguese.DONT_KNOW);
            string newDish = userInterface.Read();

            if (UserAskToExit(newDish))
            {
                return true;
            }

            userInterface.Show(MessagePortuguese.YourDishIsButParentIsNot(newDish, parentDishName));
            string category = userInterface.Read();

            if (UserAskToExit(category))
            {
                return true;
            }

            dishRepository.Add(newDish, category);

            return false;
        }

        public bool EnableAddDish(string parentDishName)
        {
            if (dishRepository.IsNotAvaliableChoices())
            {
                return AddDish(parentDishName);
            }

            return false;
        }

        public bool RightAnswer()
        {
            userInterface.Show(MessagePortuguese.RIGHT);
            return true;
        }

        public void Start()
        {
            userInterface.Show(MessagePortuguese.THINK_DISH);
            userInterface.Show(MessagePortuguese.YES_NO);
        }

        private Dish FindDishEnabled()
        {
            return dishRepository.AllDishesEnabled().FirstOrDefault()!;
        }

        public bool UpdateDishStatus(Dish dish, bool choice)
        {
            dishRepository.FindByGuidAndUpdateChoice(dish.Id, choice);
            return true;
        }

        public void UpdateAllDishesWithSameCategory(bool choice, string category)
        {
            dishRepository.FindSameCategoryAndUpdateChoice(false, category);
        }

        public void UpdateAllDishesWithDifferentCategory(bool choice, string category)
        {
            dishRepository.FindDifferentCategoryAndUpdateChoice(false, category);
        }

        public Dish GenerateNewDishEnabled(string parentDishName)
        {
            EnableAddDish(parentDishName);
            return FindDishEnabled();
        }

        public bool ThereExistDish(Dish dish)
        {
            var dishCategory = userInterface.Ask(MessagePortuguese.DishYouThinkIs(dish.Category));

            if (dishCategory != MessagePortuguese.EXIT)
            {
                if (dishCategory != MessagePortuguese.YES)
                {
                    UpdateAllDishesWithSameCategory(false, dish.Category);
                    return true;
                }
                else
                {
                    UpdateAllDishesWithDifferentCategory(false, dish.Category);                    
                }
                dish = GenerateNewDishEnabled(dish.Name);

                var isThisDish = userInterface.Ask(MessagePortuguese.DishYouThinkIs(dish.Name));

                if (isThisDish != MessagePortuguese.EXIT)
                {                
                    if (isThisDish == MessagePortuguese.YES)
                    {
                        RightAnswer();
                        return dishRepository.ResetChoice();
                    }
                    return UpdateDishStatus(dish, false);
                }
                return false;
            }

            return false;
        }
    }
}