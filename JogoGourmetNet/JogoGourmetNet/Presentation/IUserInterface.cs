namespace JogoGourmetNet.Presentation
{
    public interface IUserInterface
    {
        void Show(string message);
        string Read();
        string Ask(string question);
    }
}