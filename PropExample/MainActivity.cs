using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content;

using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using PropExample.Modules;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Android.Text;
using System.Linq;

namespace PropExample
{
    [Activity(Label = "PropExample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        public static Context context;
		public static ListView lists;
         static EditText inputSearch;
		HttpClient client;
        private static List<Property> prop = new List<Property>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

           lists = FindViewById<ListView>(Resource.Id.listProperty);
			//inputSearch = FindViewById<EditText>(Resource.Id.inputSearch);
        
			GetList listss = new GetList();
            listss.Execute();

        }
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.PropMenu, menu);
			return base.OnCreateOptionsMenu(menu);
		}


		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.item1:
					var intent = new Intent(this, typeof(LoginActivity));
					StartActivity(intent);
					return true;
				case Resource.Id.item2:
					var intents = new Intent(this, typeof(RegisterActivity));
					StartActivity(intents);
					return true;
				case Resource.Id.item3:
					var inten = new Intent(this, typeof(PropertyActivity));
					StartActivity(inten);
					return true;
				default:
					return false;
			}

		}
       
		
		public class GetList : AsyncTask
		{
            Context conte;
          
			
			protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
			{
				HttpClient client = new HttpClient();

				var url = string.Format("http://10.0.2.2:8080/api/Property");
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = client.GetAsync(url).Result;
				var property = response.Content.ReadAsStringAsync().Result;
				var result = JsonConvert.DeserializeObject<List<Property>>(property);

				foreach (var g in result)
				{
					prop.Add(g);
				}
				return true;
			}
			protected override void OnPreExecute()
			{
				base.OnPreExecute();
			}
			protected override void OnPostExecute(Java.Lang.Object result)
			{
				base.OnPostExecute(result);
				lists.Adapter = new PropertyAdapter(context, prop);
			}

		}
    

        public class PropertyAdapter : BaseAdapter<Property>
        {
            private List<Property> prope = new List<Property>();
            private Context context;
            public PropertyAdapter(Context con, List<Property> lstProp)
            {
                prope.Clear();
                this.prope = lstProp;
                this.context = con;
				this.NotifyDataSetChanged();
			}

            public override Property this[int position]
            {
                get
                {
                    return prope[position];
                }
            }

            public override int Count
            {
                get
                {
                    return prope.Count;
                }
            }


            public Context MContext
            {
                get;
                private set;
            }
            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View propertie = convertView;
                propertie = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListDesign, parent, false);

               TextView txtProp_desc = propertie.FindViewById<TextView>(Resource.Id.txtDesc);
               TextView txtProp_Type = propertie.FindViewById<TextView>(Resource.Id.txtType);

                txtProp_desc.Text = prope[position].Property_Desc;
                txtProp_Type.Text = prope[position].Property_Type;

                return propertie;

            }
        }


	
    }
}

