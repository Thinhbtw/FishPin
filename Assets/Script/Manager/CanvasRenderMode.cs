using UnityEngine;

public class CanvasRenderMode : MonoBehaviour
{
    private Canvas mThisCanvas;

    public RenderMode mode;

    #region property

    private Canvas ThisCanvas
    {
        get
        {
            if (mThisCanvas == null)
                mThisCanvas = gameObject.GetComponent<Canvas>();
            return mThisCanvas;
        }
    }

    #endregion

    private void OnDrawGizmos()
    {
        RenderCamera();
    }

    private void OnEnable()
    {
        RenderCamera();
    }

    private void RenderCamera()
    {
        if (ThisCanvas == null)
            return;
        ThisCanvas.renderMode = mode;
        if (mode.Equals(RenderMode.ScreenSpaceOverlay))
            return;
        Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (mainCamera != null)
            ThisCanvas.worldCamera = mainCamera;
    }
}