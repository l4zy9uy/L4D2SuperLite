
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    //Gun stats
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _timeBetweenShooting, _spread, _range, _reloadTime, _timeBetweenShots;
    [SerializeField]
    private int _magazineSize, _bulletsPerTap;
    [SerializeField]
    private bool _allowButtonHold;

    private int _bulletsLeft, _bulletsShot;

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
    private TextMeshProUGUI _text;

    [SerializeField]
    private ProceduralRecoil recoil;

    private void Awake()
    {
        _bulletsLeft = _magazineSize;
        _readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        //SetText
        _text.SetText(_bulletsLeft + " / " + _magazineSize);
    }
    private void MyInput()
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
        _readyToShoot = false;

        //Spread
        float x = Random.Range(-_spread, _spread);
        float y = Random.Range(-_spread, _spread);

        //Calculate Direction with Spread
        Vector3 direction = _fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(_fpsCam.transform.position, direction, out _rayHit, _range, _whatIsEnemy))
        {
            Debug.Log(_rayHit.collider.name);

            /*if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);*/
        }

        //Graphics
        Instantiate(_bulletHoleGraphic, _rayHit.point, Quaternion.Euler(0, 180, 0));
        _bulletsLeft--;
        _bulletsShot--;

        recoil.recoil();
        Invoke("ResetShot", _timeBetweenShooting);

        if (_bulletsShot > 0 && _bulletsLeft > 0)
            Invoke("Shoot", _timeBetweenShots);

    }
    private void ResetShot()
    {
        _readyToShoot = true;
    }
    public void Reload()
    {
        _reloading = true;
        Invoke("ReloadFinished", _reloadTime);
    }
    private void ReloadFinished()
    {
        _bulletsLeft = _magazineSize;
        _reloading = false;
    }

}
