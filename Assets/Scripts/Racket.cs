using UnityEngine;

public class Racket : MonoBehaviour {

    [SerializeField] [Range(0, 1)] private float swingPower = 0f;
    [SerializeField] private float swingPowerIncrease = 0.1f;
    [SerializeField] private float swingSpeed = 1f;
    [SerializeField] private bool canSwing = true;
    [SerializeField] private bool isSwinging = false;
    [SerializeField] private float swingReset = 1f;

    [SerializeField] private float clipPlaneAdjust = 1f;
    [SerializeField] private Transform racketPivot;

    [SerializeField] float XHitAngleDampener = 0.15f;
    [SerializeField] float YHitAngleDampener = 0.05f;

    private Quaternion originalRotation = Quaternion.Euler(0, 0, 0);
    private Quaternion endSwingRotation = Quaternion.Euler(22f, -45f, -30f);

    private Vector3 lastMousePos;
    private Vector3 mouseDelta;
    private Vector3 spin = Vector3.zero;

    private void Start() {
        Cursor.visible = false;
        print(endSwingRotation);
    }

    private void Update() {
        Vector3 mousePosition = Input.mousePosition;        
        this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane + clipPlaneAdjust));

        if (this.canSwing && Input.GetMouseButtonDown(0)) {
            print("swing");
            this.canSwing = false;
            this.isSwinging = true;
        }

        if (this.isSwinging) {
            this.RotateForward();
        }

        if (!this.isSwinging && !this.canSwing) {
            this.RotateBack();
        }
        this.mouseDelta = Input.mousePosition - lastMousePos;

        this.spin.x = this.mouseDelta.x > 0 ? -1 * this.mouseDelta.magnitude * XHitAngleDampener : 1 * this.mouseDelta.magnitude * XHitAngleDampener;
        this.spin.y = this.mouseDelta.y > 0 ? -1 * this.mouseDelta.magnitude * YHitAngleDampener : 1 * this.mouseDelta.magnitude * YHitAngleDampener;

        this.lastMousePos = mousePosition;

    }

    private void Swing() {

    }

    private void RotateForward() {
        print("rotating forward");
        transform.rotation = Quaternion.Slerp(this.transform.rotation, endSwingRotation, Time.deltaTime * this.swingSpeed);
        if (this.transform.rotation.Equals(this.endSwingRotation) || this.transform.rotation == this.endSwingRotation) {
            this.isSwinging = false;
            print("finished swing");
        }
    }

    private void RotateBack() {
        print("rotating back");
        transform.rotation = Quaternion.Slerp(this.transform.rotation, this.originalRotation, Time.deltaTime * this.swingReset);
        if (this.transform.rotation.Equals(this.originalRotation) || this.transform.rotation == this.originalRotation) {
            this.ResetSwing();
        }
    }

    private void ResetSwing() {
        this.canSwing = true;
        this.isSwinging = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ball") {
            print("collided with ball");
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            print(rb.velocity);
            rb.velocity = new Vector3(this.spin.x, this.spin.y, 50f);
            print(rb.velocity);

            Ball ball = collision.gameObject.GetComponent<Ball>();
            ball.Active = true;
        }
    }

}
