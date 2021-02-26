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
        SetPasswordFieldInteractive(false);

        Report("Please, fill in your username and name");

        username.onValueChanged.AddListener(ValidateUsernameSection);
        name.onValueChanged.AddListener(ValidateUsernameSection);

        username.onEndEdit.AddListener(ValidateUsername);
        name.onEndEdit.AddListener(ValidateName);

        password.onEndEdit.AddListener(ValidatePassword);
        repeatPassword.onValueChanged.AddListener(ValidatePassword);
        repeatPassword.onEndEdit.AddListener(ValidatePassword);
    }

    private void OnDisable()
    {
        ClearInput();
        report.text = string.Empty;
        cancel.onClick.RemoveListener(ClosePanel);

        username.onValueChanged.RemoveListener(ValidateUsernameSection);
        name.onValueChanged.RemoveListener(ValidateUsernameSection);

        username.onEndEdit.RemoveListener(ValidateUsername);
        name.onEndEdit.RemoveListener(ValidateName);

        password.onEndEdit.RemoveListener(ValidatePassword);
        repeatPassword.onValueChanged.RemoveListener(ValidatePassword);
        repeatPassword.onEndEdit.RemoveListener(ValidatePassword);
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

    public bool IsNameSectionEmpty()
    {
        return (string.IsNullOrWhiteSpace(name.text) == string.IsNullOrWhiteSpace(username.text));
    }

    public void ActivatePasswordSeciton(string value)
    {
        if (IsNameSectionEmpty() is true)
            return;

        SetPasswordSectionActive(true);
    }

    public void ValidateUsernameSection(string value)
    {
        if (!IsNameSectionEmpty())
        {
            Report("Please, fill in your username and name");
            SetPasswordFieldInteractive(false);
        }
        else
        {
            SetPasswordFieldInteractive(true);
        }
    }

    public void ValidateUsername(string value)
    {
        if (string.IsNullOrWhiteSpace(username.text))
        {
            SetInputFieldNotValid(username);
            return;
        }

        SetInputFieldValid(username);
    }

    public void ValidateName(string value)
    {
        if (string.IsNullOrWhiteSpace(name.text))
        {
            SetInputFieldNotValid(name);
            return;
        }

        SetInputFieldValid(name);
    }


    /// <section>
    /// Password
    /// </section>
    public void SetPasswordSectionActive(bool active)
    {
        password.interactable = repeatPassword.interactable = active;
    }

    public bool IsPasswordMatched()
    {
        return string.IsNullOrEmpty(password.text + repeatPassword.text) ? false : password.text == repeatPassword.text;
    }

    public void ValidatePassword(string value)
    {
        if (!IsNameSectionEmpty())
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