using System;
using System.Data.OleDb;

namespace miAlert.Models.AccessDatabase
{
    public partial class Onlineinjurysurveillance
    {
        public int Id { get; set; }
        public DateTime TodaysDate { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime ArticleDate { get; set; }
        public string City { get; set; }
        public int STATES { get; set; }
        public bool Death { get; set; }
        public string County { get; set; }
        public string Location { get; set; }
        public int NonfatalNum { get; set; }
        public int FatalNum { get; set; }
        public int Event { get; set; }
        public bool Child { get; set; }
        public bool Farmworker { get; set; }
        public bool FarmOwner { get; set; }
        public bool Occupational { get; set; }
        public bool Tractor { get; set; }
        public bool Atv { get; set; }
        public bool Livestock { get; set; }
        public string LivestockSpecify { get; set; }
        public bool CropCommodity { get; set; }
        public string CropCommSpecify { get; set; }
        public string DescriptionOther { get; set; }
        public string Link { get; set; }
        public string Link2 { get; set; }
        public string Link3 { get; set; }
        public byte[] File { get; set; }
        public int AgCategory { get; set; }
        public int StaffId { get; set; }

        public void InsertIntoDatabase()
        {
            var connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Database11.accdb");
            var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO OnlineInjurySurveillance ( ID, Todays_Date, Event_Date, Article_Date, City, STATES, Death, County, Location, Nonfatal_num, Fatal_num, Event, Child, Farmworker, Farm_owner, Occupational, Tractor, ATV, Livestock, Livestock_Specify, Crop_Commodity, Crop_Comm_Specify, Description_Other, Link, Link2, Link3, File, File.FileData, File.FileName, File.FileType, AgCategory, StaffID )" +
                "VALUES (@ID, @Todays_Date, @Event_Date, @Article_Date, @City, @STATES, @Death, @County, @Location, @Nonfatal_num, @Fatal_num, @Event, @Child, @Farmworker, @Farm_owner, @Occupational, @Tractor, @ATV, @Livestock, @Livestock_Specify, @Crop_Commodity, @Crop_Comm_Specify, @Description_Other, @Link, @Link2, @Link3, @File, @File.FileData, @File.FileName, @File.FileType, @AgCategory, @StaffID )";

            command.Parameters.AddWithValue("@ID", Id);
            command.Parameters.AddWithValue("@Todays_Date", TodaysDate);
            command.Parameters.AddWithValue("@Event_Date", EventDate);
            command.Parameters.AddWithValue("@Article_Date", ArticleDate);
            command.Parameters.AddWithValue("@City", City);
            command.Parameters.AddWithValue("@STATES", STATES);
            command.Parameters.AddWithValue("@Death", Death);
            command.Parameters.AddWithValue("@County", County);
            command.Parameters.AddWithValue("@Location", Location);
            command.Parameters.AddWithValue("@Nonfatal_num", NonfatalNum);

            connection.Open();
            var resp = command.ExecuteNonQuery();
            connection.Close();
            int i = 0;
        }
    }  
}