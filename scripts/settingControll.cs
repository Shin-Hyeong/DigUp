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

    int setWidth = 1920; // 사용자 설정 너비
    int setHeight = 1080; // 사용자 설정 높이

    int deviceWidth = Screen.width; // 기기 너비 저장
    int deviceHeight = Screen.height; // 기기 높이 저장

    void Start()
    {
        mainPreference = GameObject.Find("ManagesBar");


        settingValue = GetComponentInChildren<Text>();
        settingBar = GetComponentInParent<Slider>();

        settingDisplay = GetComponentInChildren<Dropdown>();

        Screen.SetResolution(1920, 1080, false);    //기본 화면 해상도
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

        //Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), false); // SetResolution 함수 제대로 사용하기
        
        

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }*/


}

abstract class Display{
    internal abstract void ApplyDisplay();
}
interface IVolume{
    abstract void SetVolume(float v);
}