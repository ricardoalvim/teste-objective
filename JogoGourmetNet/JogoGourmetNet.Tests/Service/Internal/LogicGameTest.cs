using JogoGourmetNet.Presentation;
using JogoGourmetNet.Repository;
using JogoGourmetNet.Service.Internal;
using JogoGourmetNet.Utils.Message;
using JogoGourmetNet.Utils.RandomUtil;
using Moq;

namespace JogoGourmetNet.Tests.Service.Internal
{
    public class LogicGameTest
    {
        private LogicGame logicGame;
        private Mock<IUserInterface> userInterfaceMock;
        private Mock<IDishRepository> dishRepositoryMock;
        private Mock<IRandomUtil> randomUtilMock;

        public LogicGameTest() 
        {
            this.userInterfaceMock = new Mock<IUserInterface>();
            this.dishRepositoryMock = new Mock<IDishRepository>();
            this.randomUtilMock = new Mock<IRandomUtil>();
            this.logicGame = new LogicGame(userInterfaceMock.Object,
                                      dishRepositoryMock.Object,
                                      randomUtilMock.Object);
        }

        [Fact]
        public void Start_JustShow()
        {
            this.logicGame.Start();
        }

        [Fact]
        public void RightAnswer_JustShow()
        {
            this.logicGame.RightAnswer();
        }

        [Fact]
        public void ThereExistDish_ShouldBeSucess()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", true);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.AllDishesEnabled()).Returns(dishes);
            var output = this.logicGame.ThereExistDish(dish);

            Assert.True(output);
        }

        [Fact]
        public void ThereExistDish_UserWantsToExit()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", true);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(MessagePortuguese.EXIT);
            var output = this.logicGame.ThereExistDish(dish);

            Assert.False(output);
        }

        [Fact]
        public void ThereExistDish_RightAnswer()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", true);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.AllDishesEnabled()).Returns(dishes);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(dish.Category);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(MessagePortuguese.YES);
            var output = this.logicGame.ThereExistDish(dish);

            Assert.True(output);
        }

        [Fact]
        public void ThereExistDish_NameRight()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", true);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.AllDishesEnabled()).Returns(dishes);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(dish.Category);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(dish.Name);
            var output = this.logicGame.ThereExistDish(dish);

            Assert.True(output);
        }

        [Fact]
        public void ThereExistDish_UserWantsToExitInName()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", true);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.AllDishesEnabled()).Returns(dishes);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(dish.Category);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(MessagePortuguese.EXIT);
            var output = this.logicGame.ThereExistDish(dish);

            Assert.True(output);
        }

        [Fact]
        public void EnableAddDish_NoAvaliable()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", false);

            dishRepositoryMock.Setup(s => s.IsNotAvaliableChoices()).Returns(false);
            
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(dish.Category);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(dish.Name);
            var output = this.logicGame.EnableAddDish("dish");

            Assert.False(output);
        }

        [Fact]
        public void EnableAddDish_EnableNew()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", false);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.IsNotAvaliableChoices()).Returns(true);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(dish.Category);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(dish.Name);
            var output = this.logicGame.EnableAddDish("dish");

            Assert.False(output);
        }

        [Fact]
        public void EnableAddDish_UserWantsExitName()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", false);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.IsNotAvaliableChoices()).Returns(true);
            userInterfaceMock.Setup(s => s.Read()).Returns(MessagePortuguese.EXIT);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(dish.Category);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(dish.Name);
            var output = this.logicGame.EnableAddDish("dish");

            Assert.True(output);
        }

        [Fact]
        public void EnableAddDish_UserWantsExitCategory()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", false);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.IsNotAvaliableChoices()).Returns(true);
            userInterfaceMock.SetupSequence(s => s.Read()).Returns(dish.Name).Returns(MessagePortuguese.EXIT);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(dish.Category);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(dish.Name);
            var output = this.logicGame.EnableAddDish("dish");

            Assert.True(output);
        }

        [Fact]
        public void EnableAddDish_UserAnswerYes()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", false);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.AllDishesEnabled()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.IsNotAvaliableChoices()).Returns(true);
            userInterfaceMock.SetupSequence(s => s.Read()).Returns(MessagePortuguese.YES).Returns(MessagePortuguese.YES);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(MessagePortuguese.YES);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(MessagePortuguese.YES);
            var output = this.logicGame.ThereExistDish(dish);

            Assert.False(output);
        }

        [Fact]
        public void EnableAddDish_UserAnswerNo()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", false);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.AllDishesEnabled()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.IsNotAvaliableChoices()).Returns(true);
            userInterfaceMock.SetupSequence(s => s.Read()).Returns(MessagePortuguese.YES).Returns(MessagePortuguese.YES);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(MessagePortuguese.YES);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(MessagePortuguese.NO);
            var output = this.logicGame.ThereExistDish(dish);

            Assert.True(output);
        }

        [Fact]
        public void EnableAddDish_UserAnswerExit()
        {
            var dish = new DTO.Dish(new Guid(), "dish", "category", false);
            var dishes = new List<DTO.Dish>();
            dishes.Add(dish);

            dishRepositoryMock.Setup(s => s.AllDishes()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.AllDishesEnabled()).Returns(dishes);
            dishRepositoryMock.Setup(s => s.IsNotAvaliableChoices()).Returns(true);
            userInterfaceMock.SetupSequence(s => s.Read()).Returns(MessagePortuguese.YES).Returns(MessagePortuguese.YES);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Category))).Returns(MessagePortuguese.YES);
            userInterfaceMock.Setup(s => s.Ask(MessagePortuguese.DishYouThinkIs(dish.Name))).Returns(MessagePortuguese.EXIT);
            var output = this.logicGame.ThereExistDish(dish);

            Assert.False(output);
        }
    }
}