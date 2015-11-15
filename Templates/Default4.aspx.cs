using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using SSADL.Summon;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using SSADL.CMS;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Templates_Default4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //

        //fillResources();
        fillContracts();



    }

  
    public static DateTime GetRandomDateTime()
    {
        DateTime start = new DateTime(2014, 10, 1);
        return start.AddDays(new Random().Next(365));
    }
    private void fillContracts()
    {

        Random rnd = new Random();
        DataTableReader dtr = DataBase.dbDataTable("Select * From ResourcesContract where FiscalYear='2015'").CreateDataReader();
        while (dtr.Read())
        {
            long mIndex = rnd.Next(474481, 237455576);

          //  long amount = long.Parse(dtr["AnnualContractCost"].ToString()) * mIndex;
           // long.Parse(dtr["FiscalYear"].ToString()) - 1;

            DateTime start = GetRandomDateTime();
            DateTime endd = GetRandomDateTime();
            long year = start.Year;
             

            string sql = "INSERT INTO [dbo].[ResourcesContract]([ResourceID],[FiscalYear],[PeriodofPerformanceStart],[PeriodofPerformanceEnd],[RequisitionNumber],[ContractNumber],[NumberOfLicensesOwned],[LicensesCount],[AnnualContractCost],[ProcurementMethod],[ProcurementMethodOther],[ContractFileName],[CriticalNotes],[NotifyOfExpirationThisManyDaysInAdvance],[LibraryContractingOfficersRepresentative],[VendorName],[RepresentativeName],[RepresentativeEmail],[RepresentativePhone],[TechnicalContactName],[TechnicalContactEmail],[TechnicalContactPhone],[NewFeatures],[NotificationActiveStartDate],[NotificationActiveEndDate]) ";
            sql += " VALUES(@ResourceID,@FiscalYear, @PeriodofPerformanceStart, @PeriodofPerformanceEnd, @RequisitionNumber, @ContractNumber, @NumberOfLicensesOwned, @LicensesCount, @AnnualContractCost, @ProcurementMethod, @ProcurementMethodOther, @ContractFileName, @CriticalNotes, @NotifyOfExpirationThisManyDaysInAdvance, @LibraryContractingOfficersRepresentative, @VendorName, @RepresentativeName, @RepresentativeEmail, @RepresentativePhone, @TechnicalContactName, @TechnicalContactEmail, @TechnicalContactPhone, @NewFeatures, @NotificationActiveStartDate, @NotificationActiveEndDate ); ";
            SqlCommand cmd = new SqlCommand(sql);

            if (dtr["PeriodofPerformanceStart"] != null )
            {
                if (dtr["PeriodofPerformanceStart"].ToString().Trim() != "")
                {
                    start = Convert.ToDateTime(dtr["PeriodofPerformanceStart"].ToString()).AddDays(-365);
                    endd = start.AddDays(365);
                }
                else
                {
                    start = GetRandomDateTime();
                    endd = start.AddDays(365);
                }
            }
            else
            {
                start = GetRandomDateTime();
                endd = start.AddDays(365);
            }
           
            cmd.Parameters.AddWithValue("@ResourceID", dtr["ResourceID"].ToString());
            cmd.Parameters.AddWithValue("@FiscalYear", year.ToString());
            cmd.Parameters.AddWithValue("@PeriodofPerformanceStart", start.ToString("d"));
            cmd.Parameters.AddWithValue("@PeriodofPerformanceEnd", endd.ToString("d"));
            cmd.Parameters.AddWithValue("@RequisitionNumber", dtr["RequisitionNumber"].ToString());
            cmd.Parameters.AddWithValue("@ContractNumber", dtr["ContractNumber"].ToString());
            cmd.Parameters.AddWithValue("@NumberOfLicensesOwned", dtr["NumberOfLicensesOwned"].ToString());
            cmd.Parameters.AddWithValue("@LicensesCount", dtr["LicensesCount"].ToString());
            cmd.Parameters.AddWithValue("@AnnualContractCost", mIndex.ToString());
            cmd.Parameters.AddWithValue("@ProcurementMethod", dtr["ProcurementMethod"].ToString());
            cmd.Parameters.AddWithValue("@ProcurementMethodOther", dtr["ProcurementMethodOther"].ToString());
            cmd.Parameters.AddWithValue("@ContractFileName", dtr["ContractFileName"].ToString());
            cmd.Parameters.AddWithValue("@CriticalNotes", dtr["CriticalNotes"].ToString());
            cmd.Parameters.AddWithValue("@NotifyOfExpirationThisManyDaysInAdvance", dtr["NotifyOfExpirationThisManyDaysInAdvance"].ToString());
            cmd.Parameters.AddWithValue("@LibraryContractingOfficersRepresentative", dtr["LibraryContractingOfficersRepresentative"].ToString());
            cmd.Parameters.AddWithValue("@VendorName", dtr["VendorName"].ToString());
            cmd.Parameters.AddWithValue("@RepresentativeName", dtr["RepresentativeName"].ToString());
            cmd.Parameters.AddWithValue("@RepresentativeEmail", dtr["RepresentativeEmail"].ToString());
            cmd.Parameters.AddWithValue("@RepresentativePhone", dtr["RepresentativePhone"].ToString());
            cmd.Parameters.AddWithValue("@TechnicalContactName", dtr["TechnicalContactName"].ToString());
            cmd.Parameters.AddWithValue("@TechnicalContactEmail", dtr["TechnicalContactEmail"].ToString());
            cmd.Parameters.AddWithValue("@TechnicalContactPhone", dtr["TechnicalContactPhone"].ToString());
            cmd.Parameters.AddWithValue("@NewFeatures", dtr["NewFeatures"].ToString());
            cmd.Parameters.AddWithValue("@NotificationActiveStartDate", dtr["NotificationActiveStartDate"].ToString());
            cmd.Parameters.AddWithValue("@NotificationActiveEndDate", dtr["NotificationActiveEndDate"].ToString());
             executeCommandWithParameters(cmd);
        }



    }


    
    private void fillResources()
    {
        Random rnd = new Random();
        string[] PINS = { "062347", "208648", "467019", "065310", "000020", "638990", "441030", "408348", "481833", "059725", "393633", "433880", "344668", "262780", "638763", "669241", "253240", "067736", "547040", "345590", "522236", "288515", "382595", "727620", "293152", "659536", "443970", "616060", "094720", "227957", "682326", "004550", "623228", "637470", "405660", "701236", "785730", "881870", "453870", "225090", "770376", "825368", "714110", "423767", "513552", "108969", "337110", "252647", "612370", "889759", "436482", "137650", "496228", "833596", "492261", "648863", "177319", "005320", "073701", "263845", "484336", "499234", "575916", "113189", "884659", "049990", "291049", "133922", "401616", "028030", "329927", "877150", "717052", "064670", "127168", "315074", "039285", "836330", "235079", "205386", "513718", "700239", "785098", "868388", "618050", "106620", "096310", "173396", "468193", "809346", "043449", "301759", "294480", "686085", "688357", "538986", "194805", "602567", "583822", "259147", "181583", "331996", "735346", "081045", "430030", "724960", "077076", "520825", "611867", "430890", "532219", "721728", "584016", "746730", "866230", "650180", "363189", "601540", "754373", "022923", "263159", "405957", "034566", "590105", "550847", "638940", "056074", "542480", "022480", "059285", "249415", "221827", "402536", "105582", "392710", "065749", "477789", "280798", "053564", "177229", "471854", "345270", "350493", "117254", "399439", "201052", "817340", "352485", "366826", "491513", "262596", "433106", "253053", "723950", "482782", "240220", "296513", "451242", "220466", "010920", "551590", "052879", "452972", "020340", "685250", "070285", "589206", "717006", "557919", "718176", "071223", "602553", "406406", "724170", "396389", "290920", "379934", "413656", "787235", "211304", "738488", "479216", "580119", "601208", "466520", "595230", "452009", "436104", "699005", "315947", "643710", "584080", "601237", "156128", "558484", "276793", "322893", "564856", "441544", "560730", "451450", "362071", "244040", "885438", "453462", "624853", "810665", "564058", "670010", "254392", "680766", "567124", "014935", "557269", "043609", "545470", "238206", "841475", "662899", "249739", "175069", "282952", "512239", "130210", "318922", "322764", "276348", "276385", "016602", "166989", "578180", "681317", "710549", "579811", "714462", "496120", "869906", "331086", "257084", "294095", "100087", "151921", "306080", "657318", "538164", "450891", "037655", "145160", "392612", "101719", "228160", "187740", "613410", "651449", "348916", "052950", "217595", "559139", "164395", "822070", "114199", "842649", "449540", "242914", "372058", "431270", "737630", "244730", "876780", "075250", "145140", "602880", "446751", "116671", "435371", "145521", "388430", "004117", "166036", "076383", "484411", "139720", "603489", "495854", "127250", "807270", "463385", "271174", "273530", "596297", "563605", "236350", "680910", "787680", "670830", "470665", "383389", "198091", "322257", "348509", "505248", "175148", "564919", "447834", "326760", "237727", "312380", "729150", "459946", "489620", "252496", "001286", "691489", "628780", "097302", "569486", "881042", "663930", "479070", "137494", "484248", "002108", "498320", "062469", "717510", "576922", "424576", "285435", "108948", "479275", "078926", "035343", "784330", "535332", "028638", "456647", "157974", "406778", "717130", "562720", "563269", "291870", "209399", "516894", "891579", "884199", "213496", "105686", "459501", "011832", "148012", "237462", "159306", "403328", "543230", "733644", "597210", "725491", "413102", "200148", "082369", "470708", "780040", "689838", "221530", "276533", "350041", "042790", "011761", "205104", "326456", "599643", "131732", "499584", "268646", "085929", "651420", "079047", "628389", "031439", "017330", "656470", "417026", "083180", "583645", "254409", "548845", "044859", "288930", "507364", "702532", "730725", "561559", "387420", "694890", "829428", "185046", "190813", "778810", "771529", "858296", "442922", "026839", "248147", "810697", "049050", "130785", "604596", "492001", "559046", "478420", "454670", "081588", "868775", "120770", "089429", "274439", "743043", "294772", "119660", "722048", "838470", "243151", "757870", "742019", "761450", "478170", "488667", "332615", "251441", "608767", "889938", "458190", "127670", "477910", "463280", "721449", "007570", "462504", "602373", "045920", "261429", "218529", "620880", "018103", "687590", "568105", "727270", "738270", "635619", "517042", "217934", "664576", "522189", "608693", "400319", "191341", "725340", "405609", "210550", "241557", "431787", "479532", "452474", "645719", "567070", "548203", "828143", "214874", "206559", "562549", "456818", "165144", "251010", "235477", "458829", "253279", "769910", "181732", "181890", "376432", "404820", "708006", "424486", "161904", "046036", "133180", "134769", "230156", "368370", "592759", "072259", "621350", "054469", "796850", "473372", "487316", "026440", "525180", "738699", "803310", "406548", "366550", "317559", "001120", "624492", "361878", "752290", "670205", "253959", "632476", "087020", "024646", "689939", "531814", "189119", "047445", "453373", "866306", "139989", "411387", "559827", "663528", "532091", "550093", "084801", "451996", "086179", "638780", "341456", "095773", "370774", "001951", "617468", "601266", "573360", "276675", "581859", "009760", "455477", "269944", "711656", "352380", "043370", "014819", "013037", "324310", "146277", "017284", "350380", "559953", "422768", "479220", "296046", "583792", "166585", "646067", "377374", "253950", "289095", "222583", "680570", "315831", "473160", "236118", "132778", "585836", "449950", "391531", "123331", "612068", "609125", "516672", "207229", "115560", "616910", "035549", "038989", "555323", "404016", "288308", "175660", "557116", "530390", "198370", "629575", "457654", "606230", "679696", "328120", "618838", "630841", "034310", "290670", "295379", "404340", "330027", "347740", "189410", "330045", "606421", "788570", "101283", "068761", "520364", "626612", "608710", "564839", "880850", "564671", "431724", "766661", "232660", "397957", "478518", "412237", "528140", "423792", "284228", "338916", "418920", "055252", "052731", "016588", "243160", "055079", "407133", "432290", "533887", "268290", "456672", "296066", "130246", "009420", "456127", "508660", "294874", "130215", "536080", "010664", "279351", "639550", "819180", "372477", "065959", "694438", "242726", "474279", "437350", "645950", "104281", "729317", "413457", "130876", "271094", "553785", "266553", "009750", "160335", "698736", "478730", "417677", "378812", "016865", "459242", "102670", "569148", "566490", "476153", "031176", "681260", "137720", "190556", "525671", "114209", "592770", "738337", "375415", "314761", "225325", "840949", "756309", "209041", "899370", "384324", "079121", "069350", "527083", "356679", "090570", "375032", "411223", "626619", "392562", "199655", "106834", "514355", "277236", "201830", "824610", "065724", "308328", "316402", "269670", "336690", "270609", "515383", "261682", "288814", "345520", "733829", "114650", "305333", "589323", "483796", "405220", "211670", "285649", "353819", "683855", "357226", "489398", "389123", "507488", "375706", "838160", "311717", "600820", "614020", "104580", "271841", "808260", "196098", "543140", "580401", "817016", "572177", "247510", "077306", "885896", "054156", "861357", "367020", "192267", "410630", "259569", "342943", "400667", "417809", "551450", "868363", "166560", "031083", "840880", "073629", "084013", "012389", "558790", "875203", "089829", "299736", "630565", "717068", "079706", "304182", "207260", "474724", "728648", "121807", "621668", "856735", "551949", "521390", "174779", "644140", "380728", "529486", "077667", "112702", "083092", "796540", "446288", "610049", "223370", "091509", "199550", "225523", "336282", "367880", "375835", "327225", "193710" };

        string[] PINSHalf = { "637470", "405660", "701236", "785730", "881870", "453870", "225090", "770376", "825368", "714110", "423767", "513552", "108969", "337110", "252647", "612370", "889759", "436482", "356679", "090570", "375032", "411223", "626619", "392562", "199655", "106834", "514355", "277236", "201830", "824610", "065724", "308328", "316402", "269670", "336690", "270609", "515383", "261682", "288814", "345520", "733829", "114650", "305333", "589323", "483796", "405220", "211670", "285649", "353819", "683855", "357226", "489398", "389123", "507488", "375706", "838160", "311717", "600820", "614020", "104580", "271841", "808260", "196098", "543140", "580401", "817016", "572177", "247510", "077306", "885896", "054156", "861357", "367020", "192267", "410630", "259569", "342943", "400667", "417809", "551450", "868363", "166560", "031083", "840880", "073629", "084013", "012389", "558790", "875203", "089829", "299736", "630565", "717068", "079706", "304182", "207260", "474724", "728648", "121807", "621668", "856735", "551949", "521390", "174779", "644140", "380728", "529486", "077667", "112702", "083092", "796540", "446288", "610049", "223370", "091509", "199550", "225523", "336282", "367880", "375835", "327225", "193710" };


        Random r2 = new Random();


        for (int i = 0; i < 5050; i++)
        {
            // Generate random indexes for pet names. 
            int mIndex = rnd.Next(0, PINS.Length);
            int mIndexHalf = rnd.Next(0, PINSHalf.Length);
            //  int fIndex = rnd.Next(0, femalePetNames.Length);
            //2014, which began on October 1, 2013 and ended on September 30, 2014.  FY14(2013,10,1 - 2014,09,30) FY15(2014,10,1 - 2015,9,30)

            // Response.Write("<br/>PINS: ");
            // Response.Write(PINS[mIndex]);

            DateTime from = new DateTime(2014, 12, 1);
            DateTime to = new DateTime(2015, 4, 28);

            //  string usethispin = PINS[mIndex];
            string usethispin = PINSHalf[mIndexHalf];

            int resourcesid = r2.Next(102, 103); //for ints  //102 -103
            string resourceLink = "http://dev.ssadl.local/dynamicdb.aspx?rid=" + resourcesid.ToString();

            DataTableReader dtR = DataBase.dbDataTable("Select * from AccurintUsers where PIN='" + usethispin + "'").CreateDataReader();

            while (dtR.Read())
            {
                ///Alert
                ///
                string LastName = dtR["LastName"].ToString().Trim();
                string FirstName = dtR["FirstName"].ToString().Trim();
                string Email = dtR["Email"].ToString().Trim();
                string OfficeCode = dtR["OfficeCode"].ToString().Trim();
                string Server = dtR["Server"].ToString().Trim();
                string Domain = dtR["Domain"].ToString().Trim();


                string sql = "INSERT INTO [dbo].[ClickTracking]([ClickedByPIN],[ClickedByLastName],[ClickedByFirstName],[ClickedByEMail],[ClickedByOffice],[ClickedByServer],[ClickedByUserDomain],[ClickedDateTime],[DLSiteURLWhereLinkWasClicked],[DLSiteLinkTargetURL] , Resource) ";
                sql += "  VALUES(@ClickedByPIN,@ClickedByLastName,@ClickedByFirstName,@ClickedByEMail,@ClickedByOffice,@ClickedByServer,@ClickedByUserDomain,@ClickedDateTime,@DLSiteURLWhereLinkWasClicked ,@DLSiteLinkTargetURL , '" + resourcesid + "' )";
                SqlCommand cmd;
                cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@ClickedByPIN", usethispin);
                cmd.Parameters.AddWithValue("@ClickedByLastName", LastName);
                cmd.Parameters.AddWithValue("@ClickedByFirstName", FirstName);
                cmd.Parameters.AddWithValue("@ClickedByEMail", Email);
                cmd.Parameters.AddWithValue("@ClickedByOffice", OfficeCode);
                cmd.Parameters.AddWithValue("@ClickedByServer", Server);
                cmd.Parameters.AddWithValue("@ClickedByUserDomain", Domain);
                cmd.Parameters.AddWithValue("@ClickedDateTime", GetRandomDate(from, to));
                cmd.Parameters.AddWithValue("@DLSiteURLWhereLinkWasClicked", "http://dev.ssadl.local/resources/database.aspx");
                cmd.Parameters.AddWithValue("@DLSiteLinkTargetURL", resourceLink);
                executeCommandWithParameters(cmd);
            }

        }
    }

    public int executeCommandWithParameters(SqlCommand cmd, string ConnectionString = "Admin.DbConnection")
    {

        int results = 0;
        string ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
        using (SqlConnection cnn = new SqlConnection(ConnectionStr))
        {
            try
            {
                cmd.CommandType = CommandType.Text;
                cnn.Open();
                cmd.Connection = cnn;




                results = cmd.ExecuteNonQuery();
            }
            catch { results = 0; }
            finally
            {

                cnn.Close();

                cmd.Dispose();

            }


        }

        return results;


    }

    static readonly Random rnd = new Random();
    public static string GetRandomDate(DateTime from, DateTime to)
    {
        var range = to - from;

        var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

        return (from + randTimeSpan).ToString("G");
    }
    private string contentTypes = "Journal:Article:Book / eBook";

    ///////////////////////***************************





















    /////////////////////////////////////////////////////
}