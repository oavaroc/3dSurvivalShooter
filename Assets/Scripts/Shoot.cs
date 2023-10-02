using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private GameObject[] _bloodSplatter;
    [SerializeField]
    private int _maxAmmo = 10;
    [SerializeField]
    private int _minAmmo = 0;
    [SerializeField]
    private int _ammo = 0;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private Gun_Fire_Pistol _gun;

    private bool _canFire = true;

    [SerializeField]
    private float _fireCoolDown = 2f;
    private float _fireCoolDownTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        _playerInputActions.Player.LeftClick.performed += LeftClick_performed;
        _playerInputActions.Player.Reload.performed += Reload_performed;
        _ammo = _maxAmmo;
    }
    private void Reload_performed(InputAction.CallbackContext obj)
    {

        StartCoroutine(ReloadRoutine());

    }

    private IEnumerator ReloadRoutine()
    {
        _canFire = false;
        _gun.ReloadGun();
        yield return new WaitForSeconds(2f);
        _ammo = _maxAmmo;
        _canFire = true;
    }

    private void LeftClick_performed(InputAction.CallbackContext obj)
    {
        if(_canFire && _ammo > _minAmmo && _fireCoolDownTimer < Time.time)
        {
            _gun.FireGun();
            FireShot();
            _ammo--;
            _fireCoolDownTimer = Time.time + _fireCoolDown;
        }
    }
    private void FireShot()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Fire performed");
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, out hit, Mathf.Infinity, _layerMask))
        {
            RaycastHitSomething(hit);
        }
    }
    private void RaycastHitSomething(RaycastHit hit)
    {
        Debug.Log("Hit: " + hit.collider.name);
        if (hit.transform.TryGetComponent<Health>(out Health _entityHealth))
        {
            _entityHealth.Damage(5);
            Destroy(
                Instantiate(
                    _bloodSplatter[Random.Range(0,_bloodSplatter.Length)], 
                    hit.point + hit.normal * Random.Range(0.001f, 0.01f), 
                    Quaternion.LookRotation(hit.normal)), 
                1f);
        }
    }
}
