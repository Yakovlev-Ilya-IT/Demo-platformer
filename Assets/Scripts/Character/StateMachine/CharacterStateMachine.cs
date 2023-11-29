using System.Collections.Generic;
using System.Linq;

public class CharacterStateMachine : IStationStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;

    public CharacterStateMachine(Character character)
    {
        StateMachineData stateMachineData = new StateMachineData();

        _states = new List<IState>()
        {
            new IdlingState(this, stateMachineData, character),
            new RunningState(this, stateMachineData, character),
            new JumpingState(this, stateMachineData, character),
            new FallingState(this, stateMachineData, character),
            new HurtState(this, stateMachineData, character),
            new DeathState(character.View),
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : IState
    {
        IState state = _states.FirstOrDefault(state => state is T);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void HandleInput()
    {
        _currentState.HandleInput();
    }

    public void Update()
    {
        _currentState.Update();
    }
}
