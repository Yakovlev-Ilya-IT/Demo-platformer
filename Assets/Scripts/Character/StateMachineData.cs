using System;

public class StateMachineData
{
    public float YVeloicty;
    public float XVelocity;

    private float _speed;
    private float _xInput;

    public float XInput
    {
        get => _xInput;
        set
        {
            if (_xInput < -1 || _xInput > 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _xInput = value;
        }
    }

    public float Speed
    {
        get => _speed;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _speed = value;
        }
    }
}
