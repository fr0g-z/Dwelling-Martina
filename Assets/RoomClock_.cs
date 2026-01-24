using UnityEngine;

public class RoomClock_ : MonoBehaviour
{
    [Header("Clock Hands")]
    public Transform hourHand;
    public Transform minuteHand;

    [Header("Puzzle Settings")]
    public float snapIncrement = 5f;      // Snapping while dragging
    public float solveTolerance = 8f;     // Degrees tolerance for solving

    [Header("Door / Audio")]
    public GameObject secretDoor;         // Door to open
    public AudioClip doorAudio;           // Audio clip
    private AudioSource audioSource;

    // Dragging state
    private Transform activeHand = null;
    private float grabOffset = 0f;

    // Stored local angles
    private float hourAngle;
    private float minuteAngle;

    private bool solved = false;

    // Target angles for 12:50
    private float targetHourAngle = 360f;
    private float targetMinuteAngle = 60f;

    void Start()
    {
        // Initialize angles from hands' local rotation
        if (hourHand != null) hourAngle = Normalize(hourHand.localEulerAngles.z);
        if (minuteHand != null) minuteAngle = Normalize(minuteHand.localEulerAngles.z);

        // Ensure AudioSource exists
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (solved) return;

        // Handle dragging
        if (activeHand != null && Input.GetMouseButton(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 localMouse = activeHand.parent.InverseTransformPoint(mouseWorld);

            float mouseAngle = Mathf.Atan2(localMouse.y, localMouse.x) * Mathf.Rad2Deg;
            float newAngle = mouseAngle - grabOffset;
            newAngle = SnapAngle(newAngle, snapIncrement);

            activeHand.localRotation = Quaternion.Euler(0, 0, newAngle);

            // Update stored angle
            if (activeHand == hourHand) hourAngle = Normalize(newAngle);
            else if (activeHand == minuteHand) minuteAngle = Normalize(newAngle);
        }

        // Release check
        if (Input.GetMouseButtonUp(0) && activeHand != null)
        {
            activeHand = null;
            grabOffset = 0f;
            CheckClock();
        }
    }

    // Called by Event Trigger → PointerDown
    public void OnGrabHourHand() => BeginDrag(hourHand, hourAngle);
    public void OnGrabMinuteHand() => BeginDrag(minuteHand, minuteAngle);

    private void BeginDrag(Transform hand, float currentAngle)
    {
        if (solved) return;
        activeHand = hand;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localMouse = hand.parent.InverseTransformPoint(mouseWorld);

        float mouseAngle = Mathf.Atan2(localMouse.y, localMouse.x) * Mathf.Rad2Deg;
        grabOffset = mouseAngle - currentAngle;
    }

    private void CheckClock()
    {
        float hDiff = Mathf.Min(Mathf.Abs(hourAngle - targetHourAngle), 360 - Mathf.Abs(hourAngle - targetHourAngle));
        float mDiff = Mathf.Min(Mathf.Abs(minuteAngle - targetMinuteAngle), 360 - Mathf.Abs(minuteAngle - targetMinuteAngle));

        if (hDiff <= solveTolerance && mDiff <= solveTolerance)
        {
            solved = true;

            // Snap hands to exact position
            if (hourHand != null) hourHand.localRotation = Quaternion.Euler(0, 0, targetHourAngle);
            if (minuteHand != null) minuteHand.localRotation = Quaternion.Euler(0, 0, targetMinuteAngle);

            // Open door
            if (secretDoor != null) secretDoor.SetActive(true);

            // Play audio
            if (doorAudio != null && audioSource != null) audioSource.PlayOneShot(doorAudio);

            Debug.Log("Clock solved at 12:50!");
        }
    }

    private float SnapAngle(float a, float inc) => inc > 0 ? Mathf.Round(a / inc) * inc : a;

    private float Normalize(float a)
    {
        a %= 360f;
        if (a < 0) a += 360f;
        return a;
    }
}
