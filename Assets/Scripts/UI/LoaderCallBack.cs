using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool _isFirstUpdate = true;

    private void Update()
    {
        if (_isFirstUpdate)
        {
            _isFirstUpdate = false;

            LoadingScreen.LoaderCallback();
        }
    }
}
