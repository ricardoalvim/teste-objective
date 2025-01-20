using JogoGourmetNet.Utils.Message;

namespace JogoGourmetNet.Exceptions
{
    [Serializable]
    public class EmptyDishListException : Exception
    {
        public EmptyDishListException() : base(String.Format(MessagePortuguese.EMPTY_LIST)) { }
    }
}