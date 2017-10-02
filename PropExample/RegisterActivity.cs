
using System;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using PropExample.Modules;

namespace PropExample
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText textF, textL, textE, textP;
        HttpClient client;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register);
            textF = FindViewById<EditText>(Resource.Id.editFirst);
			textL = FindViewById<EditText>(Resource.Id.editLast);
			textE = FindViewById<EditText>(Resource.Id.editEmail);
			textP = FindViewById<EditText>(Resource.Id.editPass);

            Button but = FindViewById<Button>(Resource.Id.btnRegister);
            but.Click += But_Clicked;

            TextView text = FindViewById<TextView>(Resource.Id.textView1);
            text.Click += delegate
            {
                Intent ti = new Intent(this, typeof(LoginActivity));
                StartActivity(ti);
            };

        }

		public async void But_Clicked(Object sender, EventArgs e)
		{
            client = new HttpClient();
			var myClient = new Customer
			{
                Firstname = textF.Text,
				Lastname = textL.Text,
				Email = textE.Text,
				Password = textP.Text
			};

			textF.Text = "";
			textL.Text = "";
			textE.Text = "";
			textP.Text = "";

			var uri = new System.Uri(string.Format(@"http://10.0.2.2:8080/api/Registers"));
			var json = JsonConvert.SerializeObject(myClient);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = null;

			response = await client.PostAsync(uri, content);

			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				Customer custs = JsonConvert.DeserializeObject<Customer>(data);
			}
			Toast.MakeText(this, "Thank you " + myClient.Email + "for registering with Property24", ToastLength.Long).Show();

        }
    }
}
