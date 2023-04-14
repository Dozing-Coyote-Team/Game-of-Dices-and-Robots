using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("Camera References")]
    [SerializeField]
    private CinemachineVirtualCamera _playerCamera;
    [SerializeField]
    private CinemachineVirtualCamera _combatCamera;

    public void ActiveCombatCamera(GameObject enemy)
    {
        _combatCamera.LookAt = enemy.transform;
        _combatCamera.Priority = 10;
        _playerCamera.Priority = 1;
    }

    public void ActivePlayerCamera()
    {
        if (_playerCamera.Priority > _combatCamera.Priority)
            return;

        _combatCamera.Priority = 1;
        _playerCamera.Priority = 10;
    }
}
