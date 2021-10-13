
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public RawImage compassScrollTexture;
    public Transform playersPositionInWorld;

    // Update is called once per frame
    void Update()
    {
        //uvrect allows you to scroll through the x and y of the image
        compassScrollTexture.uvRect = new Rect(playersPositionInWorld.localEulerAngles.y / 360, 0, 1, 1);
    }
}