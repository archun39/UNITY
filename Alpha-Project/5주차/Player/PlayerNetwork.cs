using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private readonly NetworkVariable<Vector3> _networkPosition = new(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> _networkRotation = new(writePerm: NetworkVariableWritePermission.Owner);

    private NetworkVariable<bool> _networkIsRunning = new(writePerm: NetworkVariableWritePermission.Owner);

    private Animator _animator;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        var trans = transform;
        if (IsOwner)
        {
            _networkPosition.Value = trans.position;
            _networkRotation.Value = trans.rotation;

            _networkIsRunning.Value = _animator.GetBool(IsRunning);
        }
        else
        {
            trans.position = _networkPosition.Value;
            trans.rotation = _networkRotation.Value;
            
            _animator.SetBool(IsRunning, _networkIsRunning.Value);
        }
    }
}
