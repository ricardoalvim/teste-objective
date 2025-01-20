namespace JogoGourmetNet.Utils.Message
{
    public class MessageEnglish
    {
        public static string YES => "sim";
        public static string NO => "não";
        public static string EMPTY_LIST => "A lista está vazia.";
        public static string DONT_KNOW => "Qual prato você pensou?";
        public static string THINK_DISH => "Pense em um prato que você gosta";
        public static string YES_NO => "Informe 'sim' ou 'não' para continuar!";
        public static string RIGHT => "Acertei de novo!";
        public static string EXIT => "sair";

        public static string CHOCOLATE_CAKE = "bolo de chocolate";

        public static string DishYouThinkIs(string dishIs)
        {
            return $"O prato que você pensou é {dishIs}?";
        }

        public static string YourDishIsButParentIsNot(string newDish, string dishParent)
        {
            return $"{newDish} é ________, mas {dishParent} não.";
        }
    }
}