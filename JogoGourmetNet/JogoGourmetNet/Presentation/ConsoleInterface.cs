using System.Diagnostics.CodeAnalysis;

namespace JogoGourmetNet.Presentation
{
    [ExcludeFromCodeCoverage]
    public class ConsoleInterface : IUserInterface
    {
        public void Show(string message)
        {
            Console.WriteLine(message);
        }

        public string Read()
        {
            ShowInputGuideToUser();
            return Console.ReadLine()!.ToLower();
        }

        private void ShowInputGuideToUser()
        {
            Console.Write(">: ");
        }

        public string Ask(string question)
        {
            this.Show(question);
            return this.Read();
        }
    }
}