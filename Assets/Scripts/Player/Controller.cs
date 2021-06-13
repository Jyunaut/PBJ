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

        public Vector2 Direction { get; set; }

        // Misc
        [field: SerializeField] public GameObject SelectedObject { get; private set; }
        [field: SerializeField] public GameObject HeldItem { get; private set; }

        private State _currentState;

        void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody2d = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            SetState(new Idle(this));
            Direction = Vector3.one;
        }

        public void SetState(State state)
        {
            _currentState?.ExitState();
            _currentState = state;
            _currentState.EnterState();
        }

        private void AdjustDirection()
        {
            if ((Direction.x < 0 && transform.localScale.x > 0)
                || Direction.x > 0 && transform.localScale.x < 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if (_currentState.GetType() == typeof(Player.Dragging)) return;
            switch (col.tag)
            {
                case Tag.Draggable:
                case Tag.HidingSpot:
                case Tag.Stool:
                    SelectedObject = col.gameObject;
                    break;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject != SelectedObject) return;
            switch (col.tag)
            {
                case Tag.Draggable:
                case Tag.HidingSpot:
                case Tag.Stool:
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

            if (Input.GetButtonDown(PlayerInput.Interact) && SelectedObject != null)
                SelectedObject.GetComponent<IInteractable>()?.Interact();

            AdjustDirection();
        }
    }
}