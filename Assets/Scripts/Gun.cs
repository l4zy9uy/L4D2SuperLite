
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace MyInputManager
{
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
        [SerializeField]
        private int _bulletsLeft, _bulletsShot;

        //bools 
        [SerializeField]
        private bool _readyToShoot, _reloading;
        [SerializeField]
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
        //public CamShake camShake;
        private float _camShakeMagnitude, _camShakeDuration;
        [SerializeField]
        private TextMeshProUGUI _text;

        public Transform recoilPosition;
        public Transform rotationPoint;

        public float positionalRecoilSpeed = 8f;
        public float rotationalRecoilSpeed = 8f;

        public float positionalReturnSpeed = 18f;
        public float rotationalReturnSpeed = 38f;

        public Vector3 recoilRotation = new Vector3(10f, 5f, 7f);
        public Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);
        public Vector3 originalPos;

        Vector3 rotationalRecoil;
        Vector3 positionalRecoil;
        Vector3 rot;

        [SerializeField]
        RecoilShake recoilShake;

        public PlayerInput playerInput;

        private float timeSinceLastShot = 0f;
        private void Awake()
        {
            _bulletsLeft = _magazineSize;
            _readyToShoot = true;
            originalPos = recoilPosition.localPosition;
            Debug.Log(recoilPosition.localPosition);

        }

        private void FixedUpdate()
        {
            rotationalRecoil = Vector3.Lerp(rotationalRecoil, new Vector3(0, 180f, 0), rotationalReturnSpeed * Time.deltaTime);
            positionalRecoil = Vector3.Lerp(positionalRecoil, originalPos, positionalReturnSpeed * Time.deltaTime);

            recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
            rot = Vector3.Slerp(rot, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
            rotationPoint.localRotation = Quaternion.Euler(rot);
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
            
            else _shooting = MyInputManager.Instance.shoot;

            if (MyInputManager.Instance.reload && _bulletsLeft < _magazineSize && !_reloading)
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
            recoilShake.ScreenShake(direction);

            rotationalRecoil += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
            positionalRecoil += new Vector3(Random.Range(-recoilKickBack.x, recoilKickBack.x), Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);

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

            Invoke("ResetShot", _timeBetweenShooting);

            /*if (_bulletsShot > 0 && _bulletsLeft > 0)
                Invoke("Shoot", _timeBetweenShots);*/

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
}
