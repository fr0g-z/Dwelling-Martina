using UnityEngine;
using PowerScript;
using PowerTools.Quest;
using static GlobalScript;

public class RoomClock_ : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;

    public GameObject secretDoor;
    public AudioClip doorAudio;

    public float snapIncrement = 5f;
    public float solveTolerance = 8f;

    private AudioSource audioSource;
    private Transform activeHand = null;
    private float grabOffset = 0f;
    private float hourAngle;
    private float minuteAngle;
    private bool solved = false;

    private const float targetHourAngle = 360f;
    private const float targetMinuteAngle = 60f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (ClockSolved.ClockSolved_)
        {
            solved = true;
            if (hourHand != null) hourHand.localRotation = Quaternion.Euler(0, 0, targetHourAngle);
            if (minuteHand != null) minuteHand.localRotation = Quaternion.Euler(0, 0, targetMinuteAngle);
            if (secretDoor != null) secretDoor.SetActive(true);
            return;
        }

        if (hourHand != null) hourAngle = Normalize(hourHand.localEulerAngles.z);
        if (minuteHand != null) minuteAngle = Normalize(minuteHand.localEulerAngles.z);
    }

    void Update()
    {
        if (solved) return;

        if (activeHand != null && Input.GetMouseButton(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 localMouse = activeHand.parent.InverseTransformPoint(mouseWorld);
            float mouseAngle = Mathf.Atan2(localMouse.y, localMouse.x) * Mathf.Rad2Deg;
            float newAngle = SnapAngle(mouseAngle - grabOffset, snapIncrement);
            activeHand.localRotation = Quaternion.Euler(0, 0, newAngle);

            if (activeHand == hourHand) hourAngle = Normalize(newAngle);
            else if (activeHand == minuteHand) minuteAngle = Normalize(newAngle);
        }

        if (Input.GetMouseButtonUp(0) && activeHand != null)
        {
            activeHand = null;
            grabOffset = 0f;
            CheckClock();
        }
    }

    public void OnGrabHourHand()
    {
        if (ClockSolved.ClockSolved_) return;
        BeginDrag(hourHand, hourAngle);
    }

    public void OnGrabMinuteHand()
    {
        if (ClockSolved.ClockSolved_) return;
        BeginDrag(minuteHand, minuteAngle);
    }

    private void BeginDrag(Transform hand, float currentAngle)
    {
        activeHand = hand;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localMouse = hand.parent.InverseTransformPoint(mouseWorld);
        grabOffset = Mathf.Atan2(localMouse.y, localMouse.x) * Mathf.Rad2Deg - currentAngle;
    }

    private void CheckClock()
    {
        float hDiff = Mathf.Min(Mathf.Abs(hourAngle - targetHourAngle), 360f - Mathf.Abs(hourAngle - targetHourAngle));
        float mDiff = Mathf.Min(Mathf.Abs(minuteAngle - targetMinuteAngle), 360f - Mathf.Abs(minuteAngle - targetMinuteAngle));

        if (hDiff <= solveTolerance && mDiff <= solveTolerance)
        {
            solved = true;
            ClockSolved.ClockSolved_ = true;

            if (hourHand != null) hourHand.localRotation = Quaternion.Euler(0, 0, targetHourAngle);
            if (minuteHand != null) minuteHand.localRotation = Quaternion.Euler(0, 0, targetMinuteAngle);
            if (secretDoor != null) secretDoor.SetActive(true);
            if (doorAudio != null && audioSource != null) audioSource.PlayOneShot(doorAudio);
            I.MumsPin.AddAsActive();

        }
    }

    private float SnapAngle(float angle, float increment) => increment > 0 ? Mathf.Round(angle / increment) * increment : angle;
    private float Normalize(float angle) { angle %= 360f; if (angle < 0) angle += 360f; return angle; }
}
