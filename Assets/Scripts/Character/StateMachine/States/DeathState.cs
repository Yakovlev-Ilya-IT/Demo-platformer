public class DeathState : IState
{
    private readonly CharacterView _view;

    public DeathState(CharacterView characterView) => _view = characterView;

    public void Enter()
    {
         _view.StartDying();
    }

    public void Exit() { }

    public void HandleInput() { }

    public void Update() { }
}
