using JogoGourmetNet.Repository;

namespace JogoGourmetNet.Tests.Repository
{
    public class DishRepositoryTest
    {
        [Fact]
        public void AllDishes_NotEmpty()
        {
            var DishRepository = new DishRepository();
            var output = DishRepository.AllDishes();
            Assert.NotEmpty(output);
        }

        [Fact]
        public void AllDishes_ReturnOne()
        {
            var DishRepository = new DishRepository();
            DishRepository.Add("dish", "category");
            var output = DishRepository.AllDishes();
            Assert.NotEmpty(output);
        }

        [Fact]
        public void IsAvaliableChoices_HaveAvaliableOptions()
        {
            var DishRepository = new DishRepository();
            DishRepository.Add("dish", "category");
            var output = DishRepository.IsNotAvaliableChoices();
            Assert.False(output);
        }

        [Fact]
        public void IsAvaliableChoices_DontHaveAvaliableOptions()
        {
            var DishRepository = new DishRepository();
            DishRepository.Add("dish", "category");
            DishRepository.FindSameCategoryAndUpdateChoice(false, "category");
            var output = DishRepository.IsNotAvaliableChoices();

            Assert.True(output);
        }

        [Fact]
        public void AllDishesEnabled_HaveAvaliableOptions()
        {
            var DishRepository = new DishRepository();
            DishRepository.Add("dish", "category");
            var output = DishRepository.AllDishesEnabled();

            Assert.True(output.Count > 0);
        }

        [Fact]
        public void AllDishesEnabled_HaveNoAvaliableOptions()
        {
            var DishRepository = new DishRepository();
            DishRepository.Add("dish", "category");
            DishRepository.FindDifferentCategoryAndUpdateChoice(false, "category1");
            var output = DishRepository.AllDishesEnabled();

            Assert.True(output.Count == 0);
        }

        [Fact]
        public void FindByGuidAndUpdateChoice_HaveNoAvaliableOptions()
        {
            var DishRepository = new DishRepository();
            DishRepository.Add("dish", "category");
            var guid = DishRepository.AllDishes().FirstOrDefault()!.Id;
            DishRepository.FindByGuidAndUpdateChoice(guid, false);
            var output = DishRepository.AllDishesEnabled();

            Assert.NotNull(output);
        }
    }
}