using System.Collections.Generic;
using Assets.Scripts.Levels;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : MonoBehaviour
    {
        #region variables

        [Header("References")]
        public GameObject[] Components;
        public ParticleContainer Explosion;
        [Header("Physic")]
        public float Force;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _startPos;
        private Dictionary<int, Vector2> _blackHoles;
        private readonly List<Vector2> _blackHolesVector = new List<Vector2>(4);
        private short _forceCount;
        private AudioSource _explosion;

        #endregion

        [UsedImplicitly]
        private void Start()
        {
            _blackHoles = new Dictionary<int, Vector2>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _startPos = transform.position;
            _explosion = GetComponent<AudioSource>();
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            EventBroker.MakeBlackHole += AddBlackHoleForce;
            EventBroker.DestroyBlackHole += RemoveBlackHoleForce;
            EventBroker.DestroySpaceShip += DestroySpaceShip;
            EventBroker.PauseGame += PauseGame;
            EventBroker.UnPauseGame += UnPauseGame;
            EventBroker.WinGame += Win;
            EventBroker.LoseGame += Lose;
        }

        [UsedImplicitly]
        private void OnDisable()
        {
            EventBroker.MakeBlackHole -= AddBlackHoleForce;
            EventBroker.DestroyBlackHole -= RemoveBlackHoleForce;
            EventBroker.DestroySpaceShip -= DestroySpaceShip;
            EventBroker.PauseGame -= PauseGame;
            EventBroker.UnPauseGame -= UnPauseGame;
            EventBroker.WinGame -= Win;
            EventBroker.LoseGame -= Lose;
        }

        private void Win(Vector2 endPos)
        {
            _rigidbody2D.velocity = new Vector2(0, 0);
            _rigidbody2D.angularVelocity = 0f;
            _rigidbody2D.simulated = false;

            for (var i = 0; i < Components.Length; i++)
            {
                LeanTween.move(Components[i], endPos, 0.5f).setEase(LeanTweenType.easeInOutSine);
                LeanTween.scale(Components[i], Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutSine);
            }
        }

        [UsedImplicitly]
        private void FixedUpdate()
        {
            switch (_forceCount)
            {
                case 0:
                    return;

                case 1:
                    var forceVector = (_blackHolesVector[0] - (Vector2)transform.position).normalized;
                    _rigidbody2D.AddForce(forceVector * Force);
                    break;

                case 2:
                    var forceVector1 = (_blackHolesVector[0] - (Vector2)transform.position).normalized;
                    var forceVector2 = (_blackHolesVector[1] - (Vector2)transform.position).normalized;
                    _rigidbody2D.AddForce((forceVector1 + forceVector2) * Force);

                    break;

                case 3:
                    var forceVector3 = (_blackHolesVector[0] - (Vector2)transform.position).normalized;
                    var forceVector4 = (_blackHolesVector[1] - (Vector2)transform.position).normalized;
                    var forceVector5 = (_blackHolesVector[2] - (Vector2)transform.position).normalized;
                    _rigidbody2D.AddForce((forceVector3 + forceVector4 + forceVector5) * Force);
                    break;

            }
        }


        private void AddBlackHoleForce(int id, Vector2 position)
        {
            _blackHoles.Add(id, position);
            UpdateForces();
        }

        private void RemoveBlackHoleForce(int id)
        {
            _blackHoles.Remove(id);
            UpdateForces();
        }

        private void DestroySpaceShip()
        {
            Explosion.transform.position = transform.position;
            Explosion.Play();
            _explosion.Play();

            for (var i = 0; i < Components.Length; i++)
                Components[i].SetActive(false);

            _rigidbody2D.velocity = new Vector2(0, 0);
            _rigidbody2D.angularVelocity = 0f;
            _rigidbody2D.simulated = false;

            Invoke("RecycleSpaceShip", 2);
        }

        [UsedImplicitly]
        private void RecycleSpaceShip()
        {
            transform.position = _startPos;
            for (var i = 0; i < Components.Length; i++)
                Components[i].SetActive(true);
            _rigidbody2D.simulated = true;
        }

        private void UpdateForces()
        {
            switch (_blackHoles.Count)
            {
                case 1:
                    _forceCount = 1;
                    _blackHolesVector.Clear();

                    if (_blackHoles.ContainsKey(0))
                        _blackHolesVector.Add(_blackHoles[0]);

                    else if (_blackHoles.ContainsKey(1))
                        _blackHolesVector.Add(_blackHoles[1]);

                    else
                        _blackHolesVector.Add(_blackHoles[2]);

                    break;
                case 2:
                    _forceCount = 2;
                    _blackHolesVector.Clear();
                    if (!_blackHoles.ContainsKey(0))
                    {
                        _blackHolesVector.Add(_blackHoles[1]);
                        _blackHolesVector.Add(_blackHoles[2]);
                    }
                    else if (!_blackHoles.ContainsKey(1))
                    {
                        _blackHolesVector.Add(_blackHoles[0]);
                        _blackHolesVector.Add(_blackHoles[2]);
                    }
                    else
                    {
                        _blackHolesVector.Add(_blackHoles[0]);
                        _blackHolesVector.Add(_blackHoles[1]);
                    }
                    break;
                case 3:
                    _forceCount = 3;
                    _blackHolesVector.Clear();
                    _blackHolesVector.Add(_blackHoles[0]);
                    _blackHolesVector.Add(_blackHoles[1]);
                    _blackHolesVector.Add(_blackHoles[2]);
                    break;
                default:
                    _forceCount = 0;
                    break;
            }
        }

        private void PauseGame()
        {
            _rigidbody2D.simulated = false;
        }

        private void UnPauseGame()
        {
            _rigidbody2D.simulated = true;
        }

        private void Lose()
        {
            Explosion.transform.position = transform.position;
            Explosion.Play();
            _explosion.Play();

            for (var i = 0; i < Components.Length; i++)
                Components[i].SetActive(false);

            _rigidbody2D.velocity = new Vector2(0, 0);
            _rigidbody2D.angularVelocity = 0f;
            _rigidbody2D.simulated = false;
        }
    }
}
