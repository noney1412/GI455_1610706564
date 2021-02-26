#region metadata
/**
GI455_Project 
by CHANON_PANPILA_1610706564
author: https://github.com/noney1412

NOTE: 
    - Unity Version (2020.2.3f1), .NET 4.6, C# 8.0
**/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <todo>
/// 1. Clear InputField on disable AND when Register failed
/// 2. Open Panel AND  Close Panel
/// 3. Check Field Emptiness
/// 4. Password Matching
/// </todo>
public class RegisterPanel : MonoBehaviour
{
    [Header("Input Field")]
    public InputField username;
    public new InputField name;
    public InputField password;
    public InputField repeatPassword;

    [Header("Button")]
    public Button register;
    public Button cancel;

    [Header("Report")]
    public Text report;

    private void OnEnable()
    {
        cancel.onClick.AddListener(ClosePanel);
        SetPasswordSectionActive(false);

        Report("Please, fill in your username and name");
        UsernameSectionEnable();
    }

    private void OnDisable()
    {
        ClearInput();
        report.text = string.Empty;
        cancel.onClick.RemoveListener(ClosePanel);
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    /// <section>
    /// Username
    /// </section>
    private void UsernameSectionEnable()
    {
        username.onValueChanged.AddListener(WhenUsernameIsTyping);
        name.onValueChanged.AddListener(WhenNameIsTyping);

        username.onEndEdit.AddListener(OnUserNameEndEdited);
        name.onEndEdit.AddListener(OnNameEndEdited);
    }

    public bool IsEachUserNameSectionEmpty()
        => (string.IsNullOrWhiteSpace(username.text) || string.IsNullOrWhiteSpace(name.text));

    public bool IsAllUserNameSectionEmpty()
        => (string.IsNullOrWhiteSpace(username.text) == string.IsNullOrWhiteSpace(name.text));

    public void ShouldActivatePasswordSeciton()
    {
        if (IsEachUserNameSectionEmpty() is true)
        {
            if (IsAllUserNameSectionEmpty())
                ClearPasswordSectionInputField();

            SetPasswordSectionActive(false);
            return;
        }

        SetPasswordSectionActive(true);
    }

    public void WhenUsernameIsTyping(string value)
    {
        username.image.color = username.colors.normalColor;
        Report("Typing...");

        ShouldActivatePasswordSeciton();
    }

    public void WhenNameIsTyping(string value)
    {
        name.image.color = name.colors.normalColor;
        Report("Typing...");

        ShouldActivatePasswordSeciton();
    }

    public void OnUserNameEndEdited(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            SetInputFieldNotValid(username);
            Report("Please, fill in your username");
            return;
        }

        Report(string.Empty);
        SetInputFieldValid(username);
    }

    public void OnNameEndEdited(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            SetInputFieldNotValid(name);
            Report("Please, fill in your name");
            return;
        }

        Report(string.Empty);
        SetInputFieldValid(name);
    }

    /// <section>
    /// Password
    /// </section>
    public void SetPasswordSectionActive(bool active)
    {
        password.interactable = repeatPassword.interactable = active;
    }

    public void ClearPasswordSectionInputField()
    {
        password.text = repeatPassword.text = string.Empty;
    }

    public bool IsPasswordMatched()
    {
        return string.IsNullOrEmpty(password.text + repeatPassword.text) ? false : password.text == repeatPassword.text;
    }

    public void ValidatePassword(string value)
    {
        if (!IsEachUserNameSectionEmpty())
        {
            password.text = repeatPassword.text = string.Empty;
            return;
        }

        if (!string.IsNullOrWhiteSpace(password.text))
        {
            SetInputFieldValid(password);
        }

        if (IsPasswordMatched())
        {
            Report("string.Empty");
            SetInputFieldValid(password);
            SetInputFieldValid(repeatPassword);
        }
        else
        {
            // highlight #FFC4C4
            Report("<color='#C02E3E'>Password is not matched</color>");
            SetInputFieldNotValid(repeatPassword);
        }
    }

    /// <section>
    /// Utilities 
    /// </section>
    public void ClearInput()
    {
        username.text = name.text = password.text = repeatPassword.text = string.Empty;
    }

    public bool IsAllFieldsEmpty()
    {
        return string.IsNullOrEmpty(username.text + name.text + password.text + repeatPassword.text + string.Empty);
    }

    public void Report(string message)
    {
        report.text = message;
    }

    public void SetInputFieldValid(InputField field)
    {
        if (ColorUtility.TryParseHtmlString("#B5FFC3", out var good))
            field.image.color = good;
    }

    public void SetInputFieldNotValid(InputField field)
    {
        if (ColorUtility.TryParseHtmlString("#FFC4C4", out var bad))
            field.image.color = bad;
    }

    public void SetInteractiveOfAllInputFields(bool isInteractable)
    {
        username.interactable = name.interactable = password.interactable = repeatPassword.interactable = isInteractable;
    }

    public void SetPasswordFieldInteractive(bool isIntaractable)
    {
        password.interactable = isIntaractable;
        repeatPassword.interactable = isIntaractable;
    }

}