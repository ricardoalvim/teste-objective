using JogoGourmetNet.DTO;
using JogoGourmetNet.Exceptions;
using JogoGourmetNet.Repository;
using JogoGourmetNet.Utils.RandomUtil;
using Moq;

namespace JogoGourmetNet.Tests.Util.RandomUtil
{
    public class RandomUtilTest
    {
        private Mock<IDishRepository> DishRepository;
        private IRandomUtil RandomUtil;

        public RandomUtilTest()
        {
            DishRepository = new Mock<IDishRepository>();
            RandomUtil = new JogoGourmetNet.Utils.RandomUtil.RandomUtil(DishRepository.Object);
        }

        [Fact]
        public void Get_ShouldThrowException()
        {
            var emptyList = new List<Dish>();
            DishRepository.Setup(s => s.AllDishes()).Returns(emptyList);

            Assert.Throws<EmptyDishListException>(() => RandomUtil.Get());
        }

        [Fact]
        public void Get_ShouldReturnIndex()
        {
            var dishes = new List<Dish>();
                dishes.Add(new Dish(new Guid(), "dish", "category", true));

            DishRepository.Setup(s => s.AllDishes()).Returns(dishes);

            var output = RandomUtil.Get();
            Assert.True(output == 0);
        }

        [Fact]
        public void GetEnabled_ShouldReturnIndex()
        {
            var dishes = new List<Dish>();
            dishes.Add(new Dish(new Guid(), "dish", "category", true));

            DishRepository.Setup(s => s.AllDishesEnabled()).Returns(dishes);

            var output = RandomUtil.GetEnabled();
            Assert.True(output == 0);
        }

        [Fact]
        public void GetEnabled_ShouldThrowException()
        {
            var emptyList = new List<Dish>();
            DishRepository.Setup(s => s.AllDishesEnabled()).Returns(emptyList);

            Assert.Throws<EmptyDishListException>(() => RandomUtil.GetEnabled());
        }
    }
}