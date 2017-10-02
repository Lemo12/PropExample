
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PropExample.Modules;

namespace PropExample
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
		EditText textE, textP;
		HttpClient client;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);
			textE = FindViewById<EditText>(Resource.Id.editEmail);
			textP = FindViewById<EditText>(Resource.Id.editPass);
            TextView text = FindViewById<TextView>(Resource.Id.txtCreate);

			Button but = FindViewById<Button>(Resource.Id.button1);
			but.Click += But_Click;

			text.Click += delegate
			{
				Intent ti = new Intent(this, typeof(RegisterActivity));
				StartActivity(ti);
			};
        }

		public void But_Click(Object sender, EventArgs e)
		{
			string email = "";
			string password = "";
			GetData(email, password);

		}

		public async void GetData(string email, string Pass)
		{
			Customer cust = null;

			var uri = new System.Uri(string.Format("http://10.0.2.2:8080/api/CustomersLogin?Email='" + email + "'&Password='" + Pass + "'"));

			HttpResponseMessage response = null;

			response = await client.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{

			};
			Toast welcome = Toast.MakeText(this, "Welcome" + cust.Email + ".", ToastLength.Short);
			welcome.Show();

		}
    }
}
