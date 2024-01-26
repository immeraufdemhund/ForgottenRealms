namespace ForgottenRealms.Engine.CommandsFeature;

public interface IGameCommand
{
    void Execute();
}
public class NullGameCommand : IGameCommand
{
    public void Execute() { }
}
