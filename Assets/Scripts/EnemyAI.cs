using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private CharacterController _controller;

    private Player _target;

    private bool _groundedAI;
    private Vector3 _aiVelocity = Vector3.zero;
    [SerializeField]
    private float _aiSpeed = 2.0f;
    [SerializeField]
    private float _gravityValue = -9.81f;
    [SerializeField]
    private float _attackDelay = 1f;

    private State _currentState;

    private Coroutine _coroutine;
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _currentState = new ChaseState(this);
        _currentState.Enter();

    }

    private void Update()
    {
        _currentState.Update();
    }

    public void ChangeState(State newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void CalculateAIMovement()
    {

        _groundedAI = _controller.isGrounded;
        if (_groundedAI)
        {
            Vector3 _direction = _target.transform.position - transform.position;
            _direction.y = 0;
            transform.rotation = Quaternion.LookRotation(_direction);
            _aiVelocity = _direction.normalized * _aiSpeed;
        }
        _aiVelocity.y += _gravityValue;
        _controller.Move((_aiVelocity) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeState(new AttackState(this));
            _coroutine = StartCoroutine(AttackPlayerRoutine(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeState(new ChaseState(this));
            StopCoroutine(_coroutine);
        }
    }

    IEnumerator AttackPlayerRoutine(Collider other)
    {
        while (other.CompareTag("Player"))
        {
            yield return new WaitForSeconds(_attackDelay); 
            if(other.TryGetComponent(out Health health))
            {
                health.Damage(5);
            }
        }
    }
}
