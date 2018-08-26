using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MusicDataService.Model;
using Dapper;
using System.Data.SQLite;

namespace MusicDataService
{
    /// <summary>
    /// Summary description for MusicService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MusicService : System.Web.Services.WebService
    {
        const string QUERY_SELECT_ALL = "SELECT * FROM music";
        const string QUERY_SELECT_ONE = "SELECT * FROM music WHERE id = @id";
        const string QUERY_INSERT = "INSERT INTO music (track,artist,album,genre,duration) VALUES (@track,@artist,@album,@genre,@duration)";
        const string QUERY_UPDATE = "UPDATE music SET track=@track,artist=@artist,album=@album,genre=@genre,duration=@duration WHERE id=@id";

        [WebMethod]
        public List<MusicDataService.Model.Music> SelectAll()
        {
            var db = MusicDataService.Model.Database.GetSQLiteConnection();
            return db.Query<Music>(QUERY_SELECT_ALL).ToList();
        }

        [WebMethod]
        public Music SelectOne(string id)
        {
            var db = Database.GetSQLiteConnection();
            var param = new { id };
            return db.QuerySingle<Music>(QUERY_SELECT_ONE, param);
        }

        [WebMethod]
        public Music Save(Music record)
        {
            var db = Database.GetSQLiteConnection().OpenAndReturn();
            var trans = db.BeginTransaction();
            try
            {
                if (record.Id == 0)
                {
                    db.Execute(QUERY_INSERT, record, trans);
                    record.Id = db.LastInsertRowId;
                }
                else
                {
                    db.Execute(QUERY_UPDATE, record, trans);
                }

                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }

            db.Close();
            return record;
        }



    }
}
