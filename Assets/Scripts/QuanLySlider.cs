using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuanLySlider : MonoBehaviour
{
    public Slider slider;
    public RectTransform fillRect;

    void Update()
    {
        // Lấy giá trị hiện tại của Slider
        float sliderValue = slider.value;

        // Lấy kích thước của RectTransform của Fill Rect
        Vector2 sizeDelta = fillRect.sizeDelta;

        // Lấy kích thước của Handle Slide Area
        RectTransform handleSlideArea = slider.handleRect;

        // Tính toán vị trí của Fill Rect
        float fillWidth = Mathf.Lerp(0, handleSlideArea.rect.width, slider.normalizedValue);
        float fillPosition = (fillWidth - sizeDelta.x) / 2;

        // Đặt lại vị trí của fillRect để nó di chuyển theo handle
        fillRect.localPosition = new Vector3(fillPosition, 0, 0);
    }
}
