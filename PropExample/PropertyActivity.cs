
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
using Newtonsoft.Json;
using PropExample.Modules;

namespace PropExample
{
    [Activity(Label = "PropertyActivity")]
    public class PropertyActivity : Activity
    {

        HttpClient client;
        EditText txtDesc, txtType, txtStat, txtAdress, txtCity, txtBed, txtBath, txtGarge, txtPrice;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddProperty);

            txtDesc = FindViewById<EditText>(Resource.Id.editDesc);
            txtType = FindViewById<EditText>(Resource.Id.editType);
            txtStat = FindViewById<EditText>(Resource.Id.editStat);
            txtAdress = FindViewById<EditText>(Resource.Id.editAddress);
            txtCity = FindViewById<EditText>(Resource.Id.editCit);
            txtBed = FindViewById<EditText>(Resource.Id.editBed);
            txtBath = FindViewById<EditText>(Resource.Id.editBath);
            txtGarge = FindViewById<EditText>(Resource.Id.editgar);
            txtPrice = FindViewById<EditText>(Resource.Id.editPrice);

            Button button = FindViewById<Button>(Resource.Id.btnAddP);
            button.Click += Button_Clicked;

            client = new HttpClient();

        }
        public async void Button_Clicked(Object sender, EventArgs e)
        {
            Intent t1 = new Intent(this, typeof(ImageActivity));
            StartActivity(t1);

            var prop = new Property
            {
                Property_Desc = txtDesc.Text,
                Property_Type = txtType.Text,
                Property_Stat = txtStat.Text,
                Address = txtAdress.Text,
                City = txtCity.Text,
                BedRoom = (int)txtBed,
                BathRoom = (int)txtBath,
                Garage = (int)txtGarge,
                Price = (double)txtPrice

            };

			var uri = new System.Uri(string.Format(@"http://10.0.2.2:8080/api/AddProperty"));
			var json = JsonConvert.SerializeObject(prop);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = null;

			response = await client.PostAsync(uri, content);

			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				Customer custs = JsonConvert.DeserializeObject<Customer>(data);

				Toast.MakeText(this, "Thank you for registering with Property24", ToastLength.Long).Show();
			}

        }
    }
}
