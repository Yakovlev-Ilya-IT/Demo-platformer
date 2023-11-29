using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private RunningStateConfig _runningStateConfig;
    [SerializeField] private AirborneStateConfig _airborneStateConfig;
    [SerializeField] private HurtStateConfig _hurtStateConfig;

    public RunningStateConfig RunningStateConfig => _runningStateConfig;
    public AirborneStateConfig AirborneStateConfig => _airborneStateConfig;
    public HurtStateConfig HurtStateConfig => _hurtStateConfig;
}
