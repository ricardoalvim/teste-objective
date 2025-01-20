using JogoGourmetNet.Repository;
using JogoGourmetNet.Service.External;
using JogoGourmetNet.Service.Internal;
using JogoGourmetNet.Utils.RandomUtil;
using Moq;

namespace JogoGourmetNet.Tests.Service.External
{
    public class GameServiceTest
    {
        private Mock<ILogicGame> logicGame;
        private Mock<IDishRepository> dishRepositoryMock;
        private Mock<IRandomUtil> randomUtilMock;
        private GameService gameService;

        public GameServiceTest()
        {
            logicGame = new Mock<ILogicGame>();
            dishRepositoryMock = new Mock<IDishRepository>();
            randomUtilMock = new Mock<IRandomUtil>();

            gameService = new GameService(dishRepositoryMock.Object, randomUtilMock.Object, logicGame.Object);
        }

        [Fact]
        public void Play_DishYouThinkExist()
        {

            var dish = new DTO.Dish(new Guid(), "dish", "category", true);
            logicGame.Setup(s => s.ThereExistDish(dish)).Returns(false);

            var output = gameService.Play();

            Assert.True(output);
        }

        [Fact]
        public void Play_DishYouThinkDoesNotExist()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", true);
            logicGame.Setup(s => s.ThereExistDish(dish)).Returns(true);
            logicGame.Setup(s => s.EnableAddDish("dish")).Returns(true);

            var output = gameService.Play();

            Assert.True(output);
        }

        [Fact]
        public void Play_ChoiceIsNotAvaliable()
        {
            var dishFalse = new DTO.Dish(new Guid(), "dish", "category", false);
            var dishTrue = new DTO.Dish(new Guid(), "dish", "category", true);
            logicGame.Setup(s => s.EnableAddDish("dish")).Returns(true);
            randomUtilMock.Setup(s => s.Get()).Returns(0);

            var output = gameService.Play();

            Assert.True(output);
        }
    }
}