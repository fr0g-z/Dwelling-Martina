using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomClock : RoomScript<RoomClock>
{
    IProp activeHand = null;
    Transform activeTransform = null;

    float hourAngle;
    float minuteAngle;

    float grabOffset = 0f;
    bool solved = false;

    const float TARGET_HOUR = 85f;    // 2:50 hour hand
    const float TARGET_MINUTE = 300f; // 2:50 minute hand
    const float SNAP_INCREMENT = 5f;

    void Start()
    {
        // Load persistent angles
        hourAngle = GlobalScript.savedHourAngle;
        minuteAngle = GlobalScript.savedMinuteAngle;

        // Apply to hands immediately so they stay put
        ApplyHandRotation();
    }

    void Update()
    {
        if (solved || activeHand == null || activeTransform == null)
            return;

        // Dragging active hand
        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorld = E.GetMousePosition();
            Vector2 pivot = activeTransform.position;
            Vector2 dir = mouseWorld - pivot;
            float mouseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            float newAngle = mouseAngle - grabOffset;
            newAngle = SnapAngle(newAngle, SNAP_INCREMENT);

            activeTransform.rotation = Quaternion.Euler(0, 0, newAngle);

            // Update stored angle
            if (activeHand == Prop("PropHourHand"))
                hourAngle = Normalize(newAngle);
            else if (activeHand == Prop("PropMinuteHand"))
                minuteAngle = Normalize(newAngle);
        }
    }

    IEnumerator OnInteractPropHourHand(IProp prop)
    {
        if (solved || prop == null) yield break;

        activeHand = prop;
        activeTransform = prop.Instance?.transform;

        if (activeTransform != null)
        {
            Vector2 mouseWorld = E.GetMousePosition();
            Vector2 dir = mouseWorld - (Vector2)activeTransform.position;
            float mouseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            grabOffset = mouseAngle - hourAngle;
        }

        yield return E.Break;
    }

    IEnumerator OnInteractPropMinuteHand(IProp prop)
    {
        if (solved || prop == null) yield break;

        activeHand = prop;
        activeTransform = prop.Instance?.transform;

        if (activeTransform != null)
        {
            Vector2 mouseWorld = E.GetMousePosition();
            Vector2 dir = mouseWorld - (Vector2)activeTransform.position;
            float mouseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            grabOffset = mouseAngle - minuteAngle;
        }

        yield return E.Break;
    }

    IEnumerator OnRelease()
    {
        if (activeHand != null)
        {
            // Save final angles in GlobalScript
            GlobalScript.savedHourAngle = hourAngle;
            GlobalScript.savedMinuteAngle = minuteAngle;

            yield return CheckClock();
        }

        activeHand = null;
        activeTransform = null;
        grabOffset = 0f;
    }

    IEnumerator CheckClock()
    {
        if (Mathf.Abs(hourAngle - TARGET_HOUR) < SNAP_INCREMENT * 0.5f &&
            Mathf.Abs(minuteAngle - TARGET_MINUTE) < SNAP_INCREMENT * 0.5f)
        {
            solved = true;
            yield return C.player_invis.Say("The clock clicks into place.");
            var secretDoor = Prop("PropSecretDoor");
            if (secretDoor != null) secretDoor.Visible = true;
        }
    }

    void ApplyHandRotation()
    {
        var hourProp = Prop("PropHourHand");
        var minuteProp = Prop("PropMinuteHand");

        if (hourProp != null && hourProp.Instance != null)
            hourProp.Instance.transform.rotation = Quaternion.Euler(0, 0, hourAngle);

        if (minuteProp != null && minuteProp.Instance != null)
            minuteProp.Instance.transform.rotation = Quaternion.Euler(0, 0, minuteAngle);
    }

    float Normalize(float a)
    {
        a %= 360f;
        if (a < 0) a += 360f;
        return a;
    }

    float SnapAngle(float a, float inc)
    {
        return inc > 0 ? Mathf.Round(a / inc) * inc : a;
    }

	IEnumerator OnInteractHotspotLeaveroom( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.Hallway_2);
        yield return E.Break;
	}
}
