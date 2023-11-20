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
            _text.SetText(_bulletsLeft + " / " + _magazineSize); // hiển thị số đạn còn lại trên súng
        }
        private void MyInput()
        {
            if (_allowButtonHold) _shooting = Input.GetKey(KeyCode.Mouse0); // kiểm tra xem có đang giữ nút bắn hay không
            
            else _shooting = MyInputManager.Instance.shoot;

            if (MyInputManager.Instance.reload && _bulletsLeft < _magazineSize && !_reloading) // kiểm tra xem có đang giữ nút reload hay không
            {
                Reload(); // Nạp đạn
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
            if (!_readyToShoot) return; // == false
            
            _muzzleFlash.Play(); // hiệu ứng súng bắn
            _readyToShoot = false;

            //Spread
            float x = Random.Range(-_spread, _spread);
            float y = Random.Range(-_spread, _spread);

            //Calculate Direction with Spread
            Vector3 direction = _fpsCam.transform.forward + new Vector3(x, y, 0);

            //Raycast để xác định đạn bắn trúng ai
            if (Physics.Raycast(_fpsCam.transform.position, direction, out _rayHit, _range, _whatIsEnemy))
            {
                Debug.Log(_rayHit.collider.name);
                // Nếu bắn trúng zombie thì gọi hàm takeDamage() của zombie
                Zombie zombie = _rayHit.collider.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.takeDamage(20); // Replace damageAmount with the actual damage you want to deal
                }
            }

            //Hiển thị đồ hoạ đạn bắn trúng tường
            Instantiate(_bulletHoleGraphic, _rayHit.point, Quaternion.Euler(0, 180, 0));
            //Cập nhật số đạn còn lại
            _bulletsLeft--;
            _bulletsShot--;

            recoil.recoil();

            // Gọi resetShot sau khoảng thời gian _timeBetweenShooting
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
}
