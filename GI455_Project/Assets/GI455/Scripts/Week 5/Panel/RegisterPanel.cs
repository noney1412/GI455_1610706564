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
/// todo/RegisterPanel.todo
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

    public enum InputFieldState
    {
        valid,
        error,
        normal
    }

    public enum ReportState
    {
        empty,
        typing
    }

    private void OnGUI()
    {
        if (IsReadyToRegister())
        {
            SetRegisterActive(true);
            return;
        }

        SetRegisterActive(false);
    }

    private void OnEnable()
    {
        cancel.onClick.AddListener(ClosePanel);
        SetSecretSectionActive(false);

        Report("Please, fill in your username and name");
        OnIdentificationSectionEnable();
        OnSecretSectionEnable();
        OnRegisterControlsEnable();
    }

    private void OnDisable()
    {
        ClearAllInputFields();
        report.text = string.Empty;
        cancel.onClick.RemoveListener(ClosePanel);

        OnIdentificationSectionDisable();
        OnSecretSectionDisable();
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
    /// Identification [username, inputfield]: InputField
    /// </section>
    private void OnIdentificationSectionEnable()
    {
        username.onValueChanged.AddListener(OnUsernameValueChanged);
        name.onValueChanged.AddListener(OnNameValueChanged);

        username.onEndEdit.AddListener(OnUserNameEndEdited);
        name.onEndEdit.AddListener(OnNameEndEdited);
    }

    private void OnIdentificationSectionDisable()
    {
        username.onValueChanged.RemoveListener(OnUsernameValueChanged);
        name.onValueChanged.RemoveListener(OnNameValueChanged);

        username.onEndEdit.RemoveListener(OnUserNameEndEdited);
        name.onEndEdit.RemoveListener(OnNameEndEdited);
    }

    private void OnUsernameValueChanged(string value)
    {
        ChangeInputFieldColorByState(name, InputFieldState.normal);
        Report(ReportState.typing);

        ShouldActivatePasswordSeciton();
    }

    private void OnNameValueChanged(string value)
    {
        ChangeInputFieldColorByState(name, InputFieldState.normal);
        Report(ReportState.typing);

        ShouldActivatePasswordSeciton();
    }

    private void OnUserNameEndEdited(string value)
    {
        if (string.IsNullOrWhiteSpace(value) is true)
        {
            ChangeInputFieldColorByState(username, InputFieldState.normal);
            Report("Please, fill in your username");
            return;
        }

        Report(string.Empty);
        ChangeInputFieldColorByState(username, InputFieldState.valid);
    }

    private void OnNameEndEdited(string value)
    {
        if (string.IsNullOrWhiteSpace(value) is true)
        {
            ChangeInputFieldColorByState(name, InputFieldState.normal);
            Report("Please, fill in your name");
            return;
        }

        Report(string.Empty);
        ChangeInputFieldColorByState(name, InputFieldState.valid);
    }

    private void ShouldActivatePasswordSeciton()
    {
        if (IsEachIdentificationFieldEmpty() is true)
        {
            if (IsAllIdentificationFieldsEmpty())
                ClearPasswordSectionInputField();

            SetSecretSectionActive(false);
            return;
        }

        SetSecretSectionActive(true);
    }

    public bool IsEachIdentificationFieldEmpty()
        => (string.IsNullOrWhiteSpace(username.text) || string.IsNullOrWhiteSpace(name.text)) is true;

    public bool IsAllIdentificationFieldsEmpty()
        => (string.IsNullOrWhiteSpace(username.text) == string.IsNullOrWhiteSpace(name.text)) is true;


    /// <section>
    /// Secret
    /// </section>
    private void OnSecretSectionEnable()
    {
        password.onValueChanged.AddListener(OnPasswordValueChanged);
        password.onEndEdit.AddListener(OnPasswordEndEdited);

        repeatPassword.onValueChanged.AddListener(OnRepeatPasswordValueChanged);
        repeatPassword.onEndEdit.AddListener(OnRepeatPasswordEndEdited);
    }

    private void OnSecretSectionDisable()
    {
        password.onValueChanged.RemoveListener(OnPasswordValueChanged);
        password.onEndEdit.RemoveListener(OnPasswordEndEdited);

        repeatPassword.onValueChanged.RemoveListener(OnRepeatPasswordValueChanged);
        repeatPassword.onEndEdit.RemoveListener(OnPasswordEndEdited);
    }

    private void OnPasswordValueChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value) is true)
        {
            ChangeInputFieldColorByState(password, InputFieldState.normal);
            Report("Please fill in your password");
            return;
        }

        ChangeInputFieldColorByState(password, InputFieldState.normal);
        Report(ReportState.typing);
    }

    private void OnPasswordEndEdited(string value)
    {
        if (string.IsNullOrWhiteSpace(value) is true)
        {
            ChangeInputFieldColorByState(password, InputFieldState.normal);
            Report("Please fill in your password");
            return;
        }

        ChangeInputFieldColorByState(password, InputFieldState.valid);
        Report(ReportState.empty);
    }

    private void OnRepeatPasswordValueChanged(string value)
    {
        ChangeInputFieldColorByState(repeatPassword, InputFieldState.normal);
        Report(ReportState.typing);

        if (IsPasswordMatched() is true)
        {
            ChangeInputFieldColorByState(repeatPassword, InputFieldState.valid);
            Report("Your password now match!");
            return;
        }

        Report("Please make sure your password mathch!");
    }

    private void OnRepeatPasswordEndEdited(string value)
    {
        if (IsPasswordMatched() is true)
        {
            ChangeInputFieldColorByState(repeatPassword, InputFieldState.valid);
            Report("Your are ready to register");

            return;
        }

        Report("Please make sure your password mathch!");
    }

    public void SetSecretSectionActive(bool active)
    {
        password.interactable = repeatPassword.interactable = active;
    }

    public void ClearPasswordSectionInputField()
    {
        password.text = repeatPassword.text = string.Empty;
    }

    public bool IsPasswordMatched()
        => string.IsNullOrEmpty(password.text + repeatPassword.text) is true ? false : password.text == repeatPassword.text;

    /// <section>
    /// Register Controls [register,cancel] : Button
    /// </section>
    private void OnRegisterControlsEnable()
    {
        SetRegisterActive(false);
    }

    public void SetRegisterActive(bool active)
    {
        register.interactable = active;
    }

    private bool IsReadyToRegister()
    {
        return IsAllFieldsEmpty() is false && IsPasswordMatched() is true;
    }

    /// <section>
    /// Utilities 
    /// </section>
    public void ClearAllInputFields()
    {
        username.text = name.text = password.text = repeatPassword.text = string.Empty;
    }

    public bool IsAllFieldsEmpty()
    {
        return string.IsNullOrEmpty(username.text + name.text + password.text + repeatPassword.text + string.Empty);
    }

    public void Report(ReportState state)
    {
        switch (state)
        {
            case ReportState.empty:
                Report(string.Empty);
                break;
            case ReportState.typing:
                Report("typing...");
                break;
        }
    }

    public void Report(string message)
    {
        report.text = message;
    }

    public void ChangeInputFieldColorByState(InputField field, InputFieldState state)
    {
        switch (state)
        {
            case InputFieldState.error:
                if (ColorUtility.TryParseHtmlString("#FFC4C4", out var bad))
                    field.image.color = bad;
                break;
            case InputFieldState.valid:
                if (ColorUtility.TryParseHtmlString("#B5FFC3", out var good))
                    field.image.color = good;
                break;
            case InputFieldState.normal:
                field.image.color = field.colors.normalColor;
                break;
        }
    }

    public void SetInteractiveOfAllInputFields(bool isInteractable)
    {
        username.interactable = name.interactable = password.interactable = repeatPassword.interactable = isInteractable;
    }
}