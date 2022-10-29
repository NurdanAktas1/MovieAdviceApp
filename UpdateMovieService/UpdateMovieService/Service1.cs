using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using Dapper;

namespace UpdateMovieService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        public Service1()
        {
            InitializeComponent();
        }
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=MovieAdviceDb; Trusted_Connection=True;";
        string Authtoken = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4OGI2MDE2Yzg4MDI4ZWMyMTFkNDJmNGNjZGQwZjY5YiIsInN1YiI6IjYzNTA2MWIzZDM2M2U1MDA3YTY2NTMwYSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.x-qGIQqS8UJsW6RGMrSb8fpahUMsT352ddPjZfs-7_w";
        string ApiKey = "88b6016c88028ec211d42f4ccdd0f69b";
        protected override async void OnStart(string[] args)
        {
            if (timer == null)
            {
                timer=new Timer();
            }

            List<Movie> movieList = await List();
            foreach (Movie movie in movieList)
            {
                await CheckIsExistForCreate(movie);
            } 
            InsertRecord(movieList);
            //timer.Interval = 5000; 
            timer.Interval = 3600000; //1saat
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
           
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
        }

        protected override void OnStop()
        {
        }
        public async Task<List<Movie>> List() // film listesi geliyor 
        {
            List<Movie> movieList = new List<Movie>();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var baseAddress = new Uri("https://api.themoviedb.org/3/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Authtoken);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                using (var response = await httpClient.GetAsync("list/4? api_key=" + ApiKey + "&language=en-US"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<TheMovieDbListDto>(responseData);
                    foreach (var result in model.items)
                    {
                        var movie = new Movie();

                        movie.Adult = result.adult;
                        movie.Poster_path = result.poster_path;
                        movie.Backdrop_path = result.backdrop_path;
                        movie.Media_type = result.media_type;
                        movie.Overview = result.overview;
                        movie.Original_language = result.original_language;
                        movie.Original_title = result.original_title;
                        movie.Popularity = result.popularity;
                        movie.Poster_path = result.poster_path;
                        movie.Release_date = result.release_date;
                        movie.Title = result.title;
                        movie.Video = result.video;
                        movie.Vote_average = result.vote_average;
                        movie.Vote_count = result.vote_count;
                        movieList.Add(movie);
                    }

                    return movieList;

                }

            }

        }
        static void InsertRecord(IEnumerable<Movie> movie)
        {
            using (SqlConnection oConnection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=MovieAdviceDb; Trusted_Connection=True;"))
            {
                oConnection.Open();
                using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                {
                    using (SqlCommand oCommand = oConnection.CreateCommand())
                    {
                        oCommand.Transaction = oTransaction;
                        oCommand.CommandType = CommandType.Text;
                        oCommand.CommandText = "INSERT INTO [Movies] ([Adult], [Backdrop_path],[Media_type], [Original_language],[Original_title], [Overview],[Popularity], [Poster_path],[Release_date], [Title],[Video], [Vote_average], [Vote_count]) VALUES (@Adult, @Backdrop_path,@Media_type,@Original_language,@Original_title,@Overview,@Popularity,@Poster_path,@Release_date,@Title,@Video,@Vote_average,@Vote_count);";
                        oCommand.Parameters.Add(new SqlParameter("@Adult", SqlDbType.Bit));
                        oCommand.Parameters.Add(new SqlParameter("@Backdrop_path", SqlDbType.NVarChar));
                        oCommand.Parameters.Add(new SqlParameter("@Media_type", SqlDbType.NVarChar));
                        oCommand.Parameters.Add(new SqlParameter("@Original_language", SqlDbType.NVarChar));
                        oCommand.Parameters.Add(new SqlParameter("@Original_title", SqlDbType.NVarChar));
                        oCommand.Parameters.Add(new SqlParameter("@Overview", SqlDbType.NVarChar));
                        oCommand.Parameters.Add(new SqlParameter("@Popularity", SqlDbType.Float));
                        oCommand.Parameters.Add(new SqlParameter("@Poster_path", SqlDbType.NVarChar));
                        oCommand.Parameters.Add(new SqlParameter("@Release_date", SqlDbType.NVarChar));
                        oCommand.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar));
                        oCommand.Parameters.Add(new SqlParameter("@Video", SqlDbType.Bit));
                        oCommand.Parameters.Add(new SqlParameter("@Vote_average", SqlDbType.Float));
                        oCommand.Parameters.Add(new SqlParameter("@Vote_count", SqlDbType.Int));
                        try
                        {
                            foreach (var oSetting in movie)
                            {
                                oCommand.Parameters[0].Value = oSetting.Adult;
                                oCommand.Parameters[1].Value = oSetting.Backdrop_path;
                                oCommand.Parameters[2].Value = oSetting.Media_type;
                                oCommand.Parameters[3].Value = oSetting.Original_language;
                                oCommand.Parameters[4].Value = oSetting.Original_title;
                                oCommand.Parameters[5].Value = oSetting.Overview;
                                oCommand.Parameters[6].Value = oSetting.Popularity;
                                oCommand.Parameters[7].Value = oSetting.Poster_path;
                                oCommand.Parameters[8].Value = oSetting.Release_date;
                                oCommand.Parameters[9].Value = oSetting.Title;
                                oCommand.Parameters[10].Value = oSetting.Video;
                                oCommand.Parameters[11].Value = oSetting.Vote_average;
                                oCommand.Parameters[12].Value = oSetting.Vote_count;
                                if (oCommand.ExecuteNonQuery() != 1)
                                {
                                    //'handled as needed, 
                                    //' but this snippet will throw an exception to force a rollback
                                    throw new InvalidProgramException();
                                }
                            }
                            oTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            oTransaction.Rollback();
                            throw;
                        }
                    }
                }
            }
        }
        private async Task<bool> CheckIsExistForCreate(Movie t)
        {

            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    int count = 0;

                    count = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM [Movies] x WITH(NOLOCK) WHERE x.Original_title=@Original_title AND x.Release_date=@Release_date", new { Original_title = t.Original_title, Release_date = t.Release_date });
                    if (count > 0) throw new ApplicationException("Bu kayıt mevcut.");
                    return true;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
