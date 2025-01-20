using JogoGourmetNet.Repository;
using JogoGourmetNet.Service.Internal;
using JogoGourmetNet.Utils.Message;
using JogoGourmetNet.Utils.RandomUtil;

namespace JogoGourmetNet.Service.External
{
    public class GameService : IGameService
    {
        private IDishRepository dishRepository { get; set; }
        private ILogicGame logicGame { get; set; }
        private IRandomUtil randomUtil { get; set; }
        private bool answer = true;

        public GameService (IDishRepository dishRepository, IRandomUtil randomUtil, ILogicGame logicGame)
        {
            this.dishRepository = dishRepository;
            this.randomUtil = randomUtil;
            this.logicGame = logicGame;

            this.dishRepository.Add("lasanha", "massa");
            this.dishRepository.Add("bolo de chocolate", "bolo");
        }

        public bool Play()
        {           
            logicGame.Start();

            while (answer)
            {
                var dishEnabled = logicGame.GenerateNewDishEnabled(MessagePortuguese.CHOCOLATE_CAKE);

                if (dishEnabled == null)
                {
                    answer = !logicGame.EnableAddDish(MessagePortuguese.CHOCOLATE_CAKE);
                }

                answer = logicGame.ThereExistDish(dishEnabled!);        
            }
            return true;
        }
    }
}