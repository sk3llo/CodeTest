using System;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Android.Content;
using Testproject.Droid.Helpers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Testproject.ViewModels;

namespace Testproject.Droid

{
    [Activity(Label = "Login", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        EditText login;
        EditText password;
        TextView loginErrorText;
        TextView passwordErrorText;
        Button submitButton;

        int minLength = 5;
        int maxLength = 12;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);
            submitButton = FindViewById<Button>(Resource.Id.submitButton);
            login = FindViewById<EditText>(Resource.Id.loginField);
            password = FindViewById<EditText>(Resource.Id.passwordField);
            loginErrorText = FindViewById<TextView>(Resource.Id.loginErrorText);
            passwordErrorText = FindViewById<TextView>(Resource.Id.passwordErrorText);

            ValidateLogin();
            ValidatePassword();

            submitButton.Click += Submit;
        }

        // LOGIN
        void ValidateLogin()
        {
            login.AfterTextChanged += (sender, args) =>
            {
                if (login.Length() != 0)
                {
                    // Check if contains digits or letters
                    var rxNoWordsChars = new Regex(@"\W");
                    var rxDigitsChars = new Regex(@"[0-9]");
                    var rxLettersChars = new Regex(@"[A-Za-z]");
                    var rxCheckSameSequence = new Regex(@"^(.+)(?:\1)+$");
                    bool isEvenLength = login.Text.Length % 2 == 0;

                    if (rxNoWordsChars.Match(login.Text).Length == 0)
                    {


                        // Length error
                        if (login.Text.Length < minLength)
                        {
                            loginErrorText.Text = "At least 5 characters required";
                        }
                        else if (login.Text.Length > maxLength)
                        {
                            loginErrorText.Text = "Exceeded max length";
                        }

                        // No letters error
                        if (rxLettersChars.Match(login.Text).Length == 0)
                        {
                            loginErrorText.Text = "At least one letter is required";
                        }

                        // No numbers error
                        if (rxDigitsChars.Match(login.Text).Length == 0)
                        {
                            loginErrorText.Text = "At least one number is required";
                        }



                        // Similar pattern error
                        if (rxCheckSameSequence.Match(login.Text).Length > 0)
                        {
                            loginErrorText.Text = "Repeated pattern detected";
                        }

                        if (login.Text.Length >= minLength &&
                        login.Text.Length <= maxLength &&
                        rxLettersChars.Match(login.Text).Length > 0 &&
                        rxDigitsChars.Match(login.Text).Length > 0 &&
                        rxCheckSameSequence.Match(login.Text).Length == 0
                        )
                        {
                            loginErrorText.Text = "";
                        }

                    }
                    else
                    {
                        loginErrorText.Text = "Only digits and letters allowed.";
                    }


                }
                else
                {
                    loginErrorText.Text = "";
                }
            };
        }

        // PASSWORD
        void ValidatePassword()
        {
            password.AfterTextChanged += (sender, args) =>
            {
                if (password.Length() != 0)
                {
                    // Check if contains digits or letters
                    var rxNoWordsChars = new Regex(@"\W");
                    var rxDigitsChars = new Regex(@"[0-9]");
                    var rxLettersChars = new Regex(@"[A-Za-z]");
                    var rxCheckSameSequence = new Regex(@"^(.+)(?:\1)+$");
                    bool isEvenLength = password.Text.Length % 2 == 0;

                    if (rxNoWordsChars.Match(password.Text).Length == 0)
                    {


                        // Length error
                        if (password.Text.Length < minLength)
                        {
                            passwordErrorText.Text = "At least 5 characters required";
                        }
                        else if (password.Text.Length > maxLength)
                        {
                            passwordErrorText.Text = "Exceeded max length";
                        }

                        // No letters error
                        if (rxLettersChars.Match(password.Text).Length == 0)
                        {
                            passwordErrorText.Text = "At least one letter is required";
                        }

                        // No numbers error
                        if (rxDigitsChars.Match(password.Text).Length == 0)
                        {
                            passwordErrorText.Text = "At least one number is required";
                        }



                        // Similar pattern error
                        if (rxCheckSameSequence.Match(password.Text).Length > 0)
                        {
                            passwordErrorText.Text = "Repeated pattern detected";
                        }

                        if (password.Text.Length >= minLength &&
                        password.Text.Length <= maxLength &&
                        rxLettersChars.Match(password.Text).Length > 0 &&
                        rxDigitsChars.Match(password.Text).Length > 0 &&
                        rxCheckSameSequence.Match(password.Text).Length == 0
                        )
                        {
                            passwordErrorText.Text = "";
                        }

                    }
                    else
                    {
                        passwordErrorText.Text = "Only digits and letters allowed.";
                    }


                }
                else
                {
                    passwordErrorText.Text = "";
                }
            };
        }


        async void Submit(object sender, EventArgs args)
        {
            if (loginErrorText.Text.Length == 0 && passwordErrorText.Text.Length == 0)
            {

                // Check if in DB
                LoginModel savedRecord = await App.Database.LoginCheckExists(login.Text);

                if (savedRecord != null)
                {
                    // Match passwords
                    if (savedRecord.password == password.Text)
                    {
                        // OK
                        var newIntent = new Intent(this, typeof(SecondPageActivity));
                        newIntent.PutExtra("mainList", JsonConvert.SerializeObject(await App.Database.queryAll()));

                        StartActivity(newIntent);
                    } else
                    {
                        // Wrong password
                        passwordErrorText.Text = "Wrong Password.";
                    }
                } else
                {
                    // New login
                    bool newCreated = await App.Database.SaveLogin(login.Text, password.Text);

                    if (newCreated)
                    {
                        // OK
                        var newIntent = new Intent(this, typeof(SecondPageActivity));
                        newIntent.PutExtra("mainList", JsonConvert.SerializeObject(await App.Database.queryAll()));

                        StartActivity(newIntent);
                    }
                }

            }
        }
    }
}