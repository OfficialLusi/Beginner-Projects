using NumberGuessingGame.Application;

namespace NumberGuessingName;

class Program
{
    public static void Main(string[] args)
    {
        CliService cli = new CliService();
        cli.CliMain();
    }
}