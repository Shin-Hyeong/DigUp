using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class settingControll : MonoBehaviour
{
    Text settingValue;
    Slider settingBar;
    Dropdown settingDisplay;

    GameObject mainPreference;

    int setWidth = 1920; // ����� ���� �ʺ�
    int setHeight = 1080; // ����� ���� ����

    int deviceWidth = Screen.width; // ��� �ʺ� ����
    int deviceHeight = Screen.height; // ��� ���� ����

    void Start()
    {
        mainPreference = GameObject.Find("ManagesBar");


        settingValue = GetComponentInChildren<Text>();
        settingBar = GetComponentInParent<Slider>();

        settingDisplay = GetComponentInChildren<Dropdown>();

        Screen.SetResolution(1920, 1080, false);    //�⺻ ȭ�� �ػ�
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject == FindObjectOfType<Slider>())
            settingValue.text = Mathf.Floor(settingBar.value).ToString();
    }

    public void SetDisplay()
    {
        if (settingDisplay.value == 0)
        {
            setWidth = 1920;
            setHeight = 1080;
            Debug.Log("1920*1080");
        }
        else if (settingDisplay.value == 1)
        {
            setWidth = 1280;
            setHeight = 720;
            Debug.Log("1280*720");
        }
        else
        {
            setWidth = 640;
            setHeight = 480;
            Debug.Log("640*480");
        }

        mainPreference.GetComponent<CanvasScaler>().referenceResolution = new Vector2(setWidth, setHeight);
        Screen.SetResolution(setWidth, setHeight, false);

        //Screen.SetResolution(setWidth, setHeight, false);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*public void SetDisaply()
    {

        //Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), false); // SetResolution �Լ� ����� ����ϱ�
        
        

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }*/


}

abstract class Display{
    internal abstract void ApplyDisplay();
}
interface IVolume{
    abstract void SetVolume(float v);
}