using UnityEngine;

namespace Core
{
    /// <summary>
    /// 处理Unity与Android交互
    /// </summary>
    public class AndroidManager : MonoBehaviour
    {
        public ChangeHead changeHead;

        void LogDebug(string str)
        {
            //Debug.Log(str);
        }

        void LogError(string str)
        {
            //Debug.Error(str);
        }

        public void GetImage(string imagePath)
        {
            Debug.Log("Android get image callback." + imagePath);
            changeHead.GetImage(imagePath);
        }

    }
}
