using UnityEngine;

public abstract class InputReaderSO : ScriptableObject
{
    protected MainInputControls _controls;

    protected virtual void OnEnable() => _controls ??= new MainInputControls();
}