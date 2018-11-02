using Managers;
using System.Collections;
using UnityEngine;

public class PDFExporter : MonoBehaviour
{
    private bool isProcessing = false;
    public string subject, ShareMessage, url;
    public string ScreenshotName;

    public void TakeScreenshotAndShare()
    {
        if (!isProcessing)
        {
            StartCoroutine(ShareScreenshot());
        }
    }

#if UNITY_ANDROID
    public IEnumerator ShareScreenshot()
    {
        isProcessing = true;
        // wait for graphics to render
        yield return new WaitForEndOfFrame();

        ScreenshotName = DataManager.Instance.CurrentPacient.ToString() + ".png";

        string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
        ScreenCapture.CaptureScreenshot(ScreenshotName);
        yield return new WaitForSeconds(1f);
        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), ShareMessage);
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Export Audiometria");
            currentActivity.Call("startActivity", jChooser);
        }
        isProcessing = false;
    }
#endif

}

