using UnityEngine;

public class HandsController : MonoBehaviour
{
    public AnimationClip animationClip;
    public Transform leftShoulder;
    public Transform leftArm;
    public Transform leftForearm;
    public Transform leftHand;
    public Transform rightShoulder;
    public Transform rightArm;
    public Transform rightForearm;
    public Transform rightHand;

    /*[ContextMenu("Save weapon pose")]
    void SaveWeaponPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);

        recorder.BindComponentsOfType<Transform>(leftShoulder.gameObject, false);
        recorder.BindComponentsOfType<Transform>(rightShoulder.gameObject, false);
        recorder.BindComponentsOfType<Transform>(leftArm.gameObject, false);
        recorder.BindComponentsOfType<Transform>(rightArm.gameObject, false);
        recorder.BindComponentsOfType<Transform>(leftForearm.gameObject, false);
        recorder.BindComponentsOfType<Transform>(rightForearm.gameObject, false);
        SaveHandPose(recorder, leftHand);
        SaveHandPose(recorder, rightHand);

        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(animationClip);
        AssetDatabase.SaveAssets();
    }*/

    /*void SaveHandPose(GameObjectRecorder recorder, Transform hand)
    {
        recorder.BindComponentsOfType<Transform>(hand.gameObject, false);

        // Bind fingers
        foreach (Transform finger in hand)
        {
            recorder.BindComponentsOfType<Transform>(finger.gameObject, false);

            // Bind child objects of each finger
            for (int i = 0; i < finger.childCount; i++)
            {
                Transform child = finger.GetChild(i);
                recorder.BindComponentsOfType<Transform>(child.gameObject, false);
            }
        }
    }*/
}
