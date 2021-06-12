using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        // Dependencies
        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody2d { get; private set; }

        // Player Parameters
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        public float WalkSpeed => _speedMultiplier * _walkSpeed;
        public float RunSpeed => _speedMultiplier * _runSpeed;
        private float _speedMultiplier = 1f;
        public void ApplySpeedMultiplier(float multiplier)
        {
            _speedMultiplier *= multiplier;
            _speedMultiplier = Mathf.Clamp(_speedMultiplier, 0f, 2f);
        }
        public void ResetSpeedMultiplier() => _speedMultiplier = 1f;

        // Player Conditionals
        [field: SerializeField] public Vector2 Direction { get; set; } = new Vector2(1,1);

        // Misc
        [field: SerializeField] public GameObject SelectedObject { get; private set; }

        private State _currentState;

        void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody2d = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            SetState(new Idle(this));
        }

        public void SetState(State state)
        {
            _currentState?.ExitState();
            _currentState = state;
            _currentState.EnterState();
        }

        void OnTriggerStay2D(Collider2D col)
        {
            switch (col.tag)
            {
                case Tag.Draggable:
                    SelectedObject = col.gameObject;
                    break;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            switch (col.tag)
            {
                case Tag.Draggable:
                    SelectedObject = null;
                    break;
            }
        }

        void FixedUpdate()
        {
            _currentState.DoStateBehaviourFixedUpdate();
        }

        void Update()
        {
            _currentState.DoStateBehaviour();
            _currentState.Transitions();
        }
    }
}