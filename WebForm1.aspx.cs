using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiPrize
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
			string file1 = System.IO.File.ReadAllText("C:\\test\\2019年高雄得獎里長.csv").Trim(); //取得檔案位置將資料存成字串。
			string[] f_array = file1.Split('\n');//將換行做字串切割存到陣列內

			string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["prizeLiConnectionString"].ConnectionString;//從config找到資料庫位置[]內放的是Web.config的connectionStrings的name。
			SqlConnection connection = new SqlConnection(s_data);//建立與資料庫建立起連接的通道，以s_data內的連接字串連接所對應的資料庫。

			string sql = ""; //先創一個等下用來存SQL語法的空字串

			connection.Open();//開啟通道   

			foreach (var a in f_array)
			{//使用foreach將f_array陣列的每一個陣列都跑一遍。

				string[] f_array2 = a.Split(',');//將每一次跑的陣列都再以逗號進行一次分割。

				sql = $"INSERT INTO [Li](年度, 得獎類別, 區別, 里別, 姓名)VALUES  (@year, @prize, @Chitype, @Litype, @name)";//存入插入多筆資料的SQL語法;將f_array2陣列的資料存進Li資料表欄位中。VALUES先使用@參數來取代直接給值，以防SQL Injection 程式碼。

				SqlCommand command = new SqlCommand(sql, connection);//要對SQL Server下什麼SQL指令。

				command.Parameters.Add("@year", SqlDbType.NVarChar);//給@參數加入資料型態
				command.Parameters["@year"].Value = f_array2[0];//@參數的值是f_array2[0]
				command.Parameters.Add("@prize", SqlDbType.NVarChar);
				command.Parameters["@prize"].Value = f_array2[1];
				command.Parameters.Add("@Chitype", SqlDbType.NVarChar);
				command.Parameters["@Chitype"].Value = f_array2[2];
				command.Parameters.Add("@Litype", SqlDbType.NVarChar);
				command.Parameters["@Litype"].Value = f_array2[3];
				command.Parameters.Add("@name", SqlDbType.NVarChar);
				command.Parameters["@name"].Value = f_array2[4];

				command.ExecuteNonQuery();//執行command的SQL語法，回傳受影響的資料數目。

			}
			connection.Close();//關閉與資料庫連線

			
			veiw();//執行副程式
		}

		private void veiw()
		{//副程式名稱為:veiw()
			string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["prizeLiConnectionString"].ConnectionString;//從config找到資料庫位置[]內放的是Web.config的connectionStrings的name。

			SqlConnection connection = new SqlConnection(s_data);//建立與資料庫建立起連接的通道
			SqlCommand command = new SqlCommand(@"SELECT id,  年度,得獎類別,區別,里別,姓名 FROM [Li]", connection);//要對SQL Server下什麼SQL指令。
			connection.Open();
			SqlDataReader goDataReader = command.ExecuteReader();//new一個DataReader接取ExecuteReader所執行SQL語法回傳的資料。
			GridView1.DataSource = goDataReader;//告訴GridView1的資料來源是從goDataReader
			GridView1.DataBind();//將資料來源與GridView1做繫結    
			connection.Close();
		}
	}
}