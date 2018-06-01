using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHead : MonoBehaviour
{
    public const string FILE_PREFIX = "file://";

    public UITexture headTexture;
    public GameObject btnAlbum;
    public GameObject btnPhoto;

    private void Awake()
    {
        UIEventListener.Get(btnAlbum).onClick += OpenAlbum;
        UIEventListener.Get(btnPhoto).onClick += OpenPhoto;
    }

    void OpenAlbum(GameObject go)
    {
#if UNITY_EDITOR
        //nothing
#else
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("openAlbum");
#endif
    }

    void OpenPhoto(GameObject go)
    {
#if UNITY_EDITOR
        //nothing
#else
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("openCamera");
#endif
    }

    /// <summary>
    /// 选择完图片回调
    /// </summary>
    /// <param name="data">Data.</param>
    public void GetImage(object data)
    {
        const string TEMP_IMAGE = "/images/temp.png";
        StartCoroutine(LoadTextureFromLocal(TEMP_IMAGE, delegate (Texture texture)
        {
            //设置图片
            headTexture.mainTexture = texture;
        }));
    }

    /// <summary>
    /// 读取本地图片
    /// </summary>
    /// <returns>The texture.</returns>
    /// <param name="relativePath">Path.</param>
    /// <param name="callback">Callback.</param>
    public IEnumerator LoadTextureFromLocal(string relativePath, Action<Texture> callback)
    {
        if (!string.IsNullOrEmpty(relativePath) && callback != null)
        {
            //从本地获取，file://xxx
            string localPath = FILE_PREFIX + Application.persistentDataPath + relativePath;
            Debug.Log("LoadTexture at local " + localPath);

            WWW www = new WWW(localPath);
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("加载本地图片失败;" + www.error);
                callback(null);
            }
            else
            {
                callback(www.texture);
            }
        }
    }

}
