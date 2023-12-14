
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

enum GunType
{
    AutomaticGun, Shotgun, PistolGun
}
public class Gun : MonoBehaviour
{
    //Gun stats
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _timeBetweenShooting, _spread, _range, _reloadTime, _timeBetweenShots;
    [SerializeField]
    private int _bulletsPerTap;
    [SerializeField]
    private bool _allowButtonHold;
    [SerializeField]
    private GunType _gunType;

    [SerializeField]
    public int _bulletsLeft;
    public int _bulletsShot;
    public int _magazineSize;
    public int _currentBullets;

    //bools 
    private bool _readyToShoot, _reloading;
    private bool _shooting;

    //Reference
    [SerializeField]
    private Camera _fpsCam;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private RaycastHit _rayHit;
    [SerializeField]
    private LayerMask _whatIsEnemy;

    //Graphics
    [SerializeField]
    private GameObject _bulletHoleGraphic;
    [SerializeField]
    private ParticleSystem _muzzleFlash;

    [SerializeField]
    private ProceduralRecoil recoil;

    [SerializeField]
    public AnimationClip _animationClip;

    private void Awake()
    {
        _bulletsLeft = _magazineSize;
        _readyToShoot = true;
    }

    private void Update()
    {
        if (_allowButtonHold) _shooting = Input.GetKey(KeyCode.Mouse0);

        else _shooting = InputManager.Instance.shoot;

        if (InputManager.Instance.reload && _bulletsLeft < _magazineSize && !_reloading)
        {
            Reload();
        }
        //Shoot
        if (_readyToShoot && (_shooting == true) && !_reloading && _bulletsLeft > 0) //_shooting == GunState.Spray ||
        {
            _bulletsShot = _bulletsPerTap;
            Shoot();
        }
    }
    public void Shoot()
    {
        _muzzleFlash.Play();
        recoil.recoil();
        _readyToShoot = false;

        //Spread
        float x = Random.Range(-_spread, _spread);
        float y = Random.Range(-_spread, _spread);

        //Calculate Direction with Spread
        Vector3 direction = _fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(_fpsCam.transform.position, direction, out _rayHit, _range, _whatIsEnemy))
        {
            //Graphics
            Instantiate(_bulletHoleGraphic, _rayHit.point, transform.rotation);
        }
        if (_gunType != GunType.Shotgun || _bulletsShot <= 1) _bulletsLeft--;
        _bulletsShot--;
        Invoke("ResetShot", _timeBetweenShooting);

        Burst();
    }

    //this invoking supports multi bullets per-shot
    private void Burst()
    {
        if (_bulletsShot > 0 && _bulletsLeft > 0)
        {
            Invoke("Shoot", _timeBetweenShots);
        }
    }

    //Reser shot for bursting (like shotgun, etc, ...)
    private void ResetShot()
    {
        _readyToShoot = true;
    }
    private void Reload()
    {
        _reloading = true;
        Invoke("ReloadFinished", _reloadTime);
    }
    private void ReloadFinished()
    {
        int numReloadBullets = _magazineSize - _bulletsLeft;
        if( numReloadBullets <= _currentBullets )
        {
            _currentBullets -= numReloadBullets;
            _bulletsLeft = _magazineSize;
        }
        else
        {
            _bulletsLeft = _bulletsLeft + _currentBullets;
            _currentBullets = 0;
        }
        _reloading = false;
    }

}