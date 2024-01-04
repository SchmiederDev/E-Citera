using SQLite;

namespace E_Citera_MAUI
{
    // Declared this as a public static class to be accessible from anywhere
    // and also because there should only ever be one instance of DB-Handler
    // because it is performing operations on the database crucial for this app.
    public static class DB_Handler
    {
        const string DB_FILE_PATH_NAME = "ECitera_DataBase.sqlite3";

        private static string AppDataDirectory = FileSystem.Current.AppDataDirectory;
        public static string DB_FILE_PATH { get; private set; } = Path.Combine(AppDataDirectory, DB_FILE_PATH_NAME);

        static SQLiteConnectionString connectionString;
        public static SQLiteConnection dbConnection {  get; private set; }
        public static SQLiteAsyncConnection dbAsyncConnection { get; private set; }

        public static void EstablishConnection()
        {            
            connectionString = new SQLiteConnectionString(DB_FILE_PATH, false);
            dbConnection = new SQLiteConnection(connectionString);            
        }

        public static void EstablishAsyncConnection()
        {
            connectionString = new SQLiteConnectionString(DB_FILE_PATH, false);
            dbAsyncConnection = new SQLiteAsyncConnection(connectionString);
        }

        public static void CloseConnection()
        {
            dbConnection.Close();
        }

        // Called from 'App.xaml.cs' on start of the application.
        // 'CreateTable'-Method checks whether the respective database tables exist
        // If not it creates them.
        public static void CreateDatabase()
        {
            EstablishConnection();
            
            CreateTitleTables();            
            dbConnection.CreateTable<CitationStyle>();
            dbConnection.CreateTable<ReferenceListTableObj>();
            
            dbConnection.Close();
        }

        private static void CreateTitleTables()
        {
            dbConnection.CreateTable<AuthorTableObj>();
            dbConnection.CreateTable<TitleTableObj>();
            dbConnection.CreateTable<AuthorTitleLinkTableObj>();
            dbConnection.CreateTable<SeriesTableObj>();
        }

        #region AUTHOR TABLE REGION

        public static void InsertAuthor(AuthorTableObj authorTableObj)
        {
            EstablishConnection();
            dbConnection.Insert(authorTableObj);
            dbConnection.Close();
        }

        public static int InsertAuthorReturnID(AuthorTableObj authorTableObj)
        {
            EstablishConnection();
            dbConnection.Insert(authorTableObj);
            var authorResults = dbConnection.Table<AuthorTableObj>().Where(author => author.FirstName == authorTableObj.FirstName && author.LastName == authorTableObj.LastName);
            if(authorResults.Any())
            {
                return authorResults.First().ID;
            }
            else
                return 0;
        }

        public static AuthorTableObj GetAuthorById(int id)
        {
            EstablishConnection();
            var queryList = dbConnection.Table<AuthorTableObj>().Where(t => t.ID == id);
            
            if(queryList.Any())
                return queryList.First();
            else
                return null;

        }

        public static int AuthorIsAlreadyInDB_CompareName(string firstName, string lastName)
        {
            EstablishConnection();
            var authorResults = dbConnection.Table<AuthorTableObj>().Where(author => author.FirstName == firstName && author.LastName == lastName);
            
            if(authorResults.Any())
                return authorResults.First().ID;
            else 
                return 0;
        }

        // Currently, I removed the methods calling to delete authors when titles are deleted
        // For one thing because one entry can be linked to several titles.
        // For another because I'm planning to implement some author focused functionalities soon
        // where this circumstance will come in handy.
        // Nonetheless I kept the Delete()-Methods - ready for use.
        public static void DeleteAuthorById(int id)
        {
            EstablishConnection();
            dbConnection.Delete<AuthorTableObj>(id);
            dbConnection.Close();
        }

        public static async Task DeleteAuthorByIdAsync(int id)
        {
            var asyncDbConnection = new SQLiteAsyncConnection(connectionString);
            await asyncDbConnection.DeleteAsync<AuthorTableObj>(id);
            await asyncDbConnection.CloseAsync();
        }
        #endregion

        #region  TITLE TABLE REGION
        public static void InsertTitle(TitleTableObj titleTableObj)
        {
            EstablishConnection();
            dbConnection.Insert(titleTableObj);
            dbConnection.Close();
        }

        public static int InsertTitleReturnID(TitleTableObj titleTableObj)
        {
            EstablishConnection();
            dbConnection.Insert(titleTableObj);
            var results = dbConnection.Table<TitleTableObj>().Where(title => title.ItemTitle == titleTableObj.ItemTitle);
            if(results.Any())
                return results.First().ID;
            else
                return 0;
        }

        public static TitleTableObj GetTitleById(int id)
        {
            EstablishConnection();
            var titleResult = dbConnection.Table<TitleTableObj>().Where(t => t.ID == id);
            
            if(titleResult.Any())
                return titleResult.First();
            else 
                return null;
        }

        public static List<TitleTableObj> GetTitlesAll()
        {
            EstablishConnection();
            List<TitleTableObj> titleTableObjs = dbConnection.Table<TitleTableObj>().ToList();
            
            if(titleTableObjs.Count() > 0)
                return titleTableObjs;
            else 
                return null;
        }

        public static void UpdateTitle(TitleTableObj titleTableObj)
        {
            EstablishConnection();
            dbConnection.Update(titleTableObj);
            dbConnection.Close();
        }

        public static void DeleteTitleById(int id)
        {
            EstablishConnection();
            dbConnection.Delete<TitleTableObj>(id);
            dbConnection.Close();
        }

        public static async Task DeleteTitleByIdAsync(int id)
        {
            EstablishAsyncConnection();
            await dbAsyncConnection.DeleteAsync<TitleTableObj>(id);
            await dbAsyncConnection.CloseAsync();
        }
        #endregion

        #region AUTHORS AND TITLE LINK REGION

        public static void InsertIntoAuthorsAndTitles(AuthorTitleLinkTableObj authorTitleLinkTableObj)
        {
            EstablishConnection();
            dbConnection.Insert(authorTitleLinkTableObj);
            dbConnection.Close();
        }

        public static async Task InsertIntoAuthorsAndTitlesAsync(AuthorTitleLinkTableObj authorTitleLinkTableObj)
        {
            var asyncDbConnection = new SQLiteAsyncConnection(connectionString);
            await asyncDbConnection.InsertAsync(authorTitleLinkTableObj);
            await asyncDbConnection.CloseAsync();
        }

        public static void UpdateAuthorsAndTitles(AuthorTitleLinkTableObj authorTitleLinkTableObj)
        {
            EstablishConnection();
            dbConnection.Update(authorTitleLinkTableObj);
            dbConnection.Close();
        }

        public static async Task UpdateIntoAuthorsAndTitlesAsync(AuthorTitleLinkTableObj authorTitleLinkTableObj)
        {
            var asyncDbConnection = new SQLiteAsyncConnection(connectionString);
            await asyncDbConnection.UpdateAsync(authorTitleLinkTableObj);
            await asyncDbConnection.CloseAsync();
        }

        public static int AuthorAlreadyLinkedToTitle(AuthorTitleLinkTableObj authorLink)
        {
            int entryID = 0;
            EstablishConnection();
            var results = dbConnection.Table<AuthorTitleLinkTableObj>().Where(
                linkEntry => linkEntry.Title_ID == authorLink.Title_ID 
                && linkEntry.Role == authorLink.Role
                && linkEntry.Position == authorLink.Position);

            if(results.Any())
                entryID = results.First().Entry_ID;

            dbConnection.Close();

            return entryID;
        }

        public static List<AuthorTitleLinkTableObj> GetTitleAuthorLinks_FromTitleID_and_AuthorRole(int titleID, string role)
        {
            EstablishConnection();
            List<AuthorTitleLinkTableObj> results = dbConnection.Table<AuthorTitleLinkTableObj>().Where(
                entry => entry.Title_ID == titleID &&
                entry.Role == role).ToList();
            dbConnection.Close();
            return results;
        }

        public static void DeleteAllLinkedTitles_WithTitleID(int titleID)
        { 
            EstablishConnection();
            List<AuthorTitleLinkTableObj> authorTitleLinks = dbConnection.Table<AuthorTitleLinkTableObj>().Where(linkEntry => linkEntry.Title_ID == titleID).ToList();
            foreach(AuthorTitleLinkTableObj authorTitleLink in authorTitleLinks)
            {
                dbConnection.Delete(authorTitleLink);
            }
            dbConnection.Close();
        }

        public static async Task DeleteAllLinkedTitlesAsync_WithTitleID(int titleID)
        {
            EstablishAsyncConnection();
            List<AuthorTitleLinkTableObj> authorTitleLinks = await dbAsyncConnection.Table<AuthorTitleLinkTableObj>().Where(linkEntry => linkEntry.Title_ID == titleID).ToListAsync();
            
            foreach(AuthorTitleLinkTableObj authorTitleLink in authorTitleLinks)
            {
                await dbAsyncConnection.DeleteAsync(authorTitleLink);
            }

            await dbAsyncConnection.CloseAsync();
        }

        #endregion

        #region SERIES TABLE REGION

        public static void InsertSeries(SeriesTableObj seriesTableObj)
        {
            EstablishConnection();
            dbConnection.Insert(seriesTableObj);
            dbConnection.Close();
        }

        public static int InsertSeriesReturnID(SeriesTableObj seriesTableObj)
        {
            EstablishConnection();
            dbConnection.Insert(seriesTableObj);
            var results = dbConnection.Table<SeriesTableObj>().Where(series => series.SeriesTitle == seriesTableObj.SeriesTitle);
            if (results.Any())
                return results.First().SeriesID;
            else
                return 0;
        }

        public static int SeriesIsAlreadyInDB_CompareStrings(string seriesTitle)
        {
            EstablishConnection();
            var results = dbConnection.Table<SeriesTableObj>().Where(series => series.SeriesTitle == seriesTitle);
            if(results.Count() > 0)
                return results.First().SeriesID;
            else 
                return 0;
        }

        public static SeriesTableObj GetSeriesById(int id)
        {
            EstablishConnection();
            var requestedSeries = dbConnection.Table<SeriesTableObj>().Where(t => t.SeriesID == id);
            
            if(requestedSeries.Count() > 0)
                return requestedSeries.First();
            else
                return null;
        }

        public static void UpdateSeries(SeriesTableObj seriesTableObj)
        {
            EstablishConnection();
            dbConnection.Update(seriesTableObj);
            dbConnection.Close();
        }

        // Currently, Series are not deleted if the title is deleted.
        // because I'm planning to implement some further functionalities for Series soon
        // where this circumstance will come in handy.
        // (like searching them and then link several titles to one series, e. g.)
        // Nonetheless I kept the Delete()-Methods - ready for use if needed.
        public static void DeleteSeriesById(int id)
        {
            EstablishConnection();
            dbConnection.Delete<SeriesTableObj>(id);
            dbConnection.Close();
        }

        public static async Task DeleteSeriesByIdAsync(int id)
        {
            var asyncDbConnection = new SQLiteAsyncConnection(connectionString);
            await asyncDbConnection.DeleteAsync<SeriesTableObj>(id);
            await asyncDbConnection.CloseAsync();
        }

        #endregion

        #region CITATION STYLE TABLE
        public static void InsertCitationStyle(CitationStyle citationStyle)
        {
            EstablishConnection();
            dbConnection.Insert(citationStyle);
            dbConnection.Close();
        }

        public static int InsertCitationStyleReturnID(CitationStyle citationStyle)
        {
            EstablishConnection();
            dbConnection.Insert(citationStyle);
            var results = dbConnection.Table<CitationStyle>().Where(styleObject => styleObject.StyleName == citationStyle.StyleName);
            if(results.Any())
            {
                int newStyleID = results.First().StyleID;
                dbConnection.Close();
                return newStyleID;
            }

            else
            {
                dbConnection.Close();
                return 0;
            }
            
        }

        public static async void InsertCitationStyleAsync(CitationStyle citationStyle)
        {
            EstablishAsyncConnection();
            await dbAsyncConnection.InsertAsync(citationStyle);
            await dbAsyncConnection.CloseAsync();
        }

        public static CitationStyle Get_CitationStyle_by_ID(int styleId)
        {
            EstablishConnection();
            CitationStyle citationStyle = dbConnection.Table<CitationStyle>().Where(styleObj => styleObj.StyleID == styleId).First();
            dbConnection.Close();
            return citationStyle;
        }

        public static async Task<CitationStyle> Get_CitationStyle_by_ID_Async(int styleId)
        {
            EstablishAsyncConnection();
            CitationStyle citationStyle = await dbAsyncConnection.Table<CitationStyle>().Where(styleObj => styleObj.StyleID == styleId).FirstAsync();
            await dbAsyncConnection.CloseAsync();
            return citationStyle;
        }

        public static CitationStyle Get_CitationStyle_by_Name(string styleName)
        {
            EstablishConnection();
            var styleResults = dbConnection.Table<CitationStyle>().Where(styleObj => styleObj.StyleName == styleName);
            CitationStyle citationStyle = null;
            if(styleResults.Any())
                citationStyle = styleResults.First();
            dbConnection.Close();
            return citationStyle;
        }

        public static async Task<CitationStyle> Get_CitationStyle_by_Name_Async(string styleName)
        {
            EstablishAsyncConnection();
            CitationStyle citationStyle = await dbAsyncConnection.Table<CitationStyle>().Where(styleObj => styleObj.StyleName == styleName).FirstAsync();
            await dbAsyncConnection.CloseAsync();
            return citationStyle;
        }

        public static List<string> GetStyleNames()
        {
            EstablishConnection();
            List<string> styleNames = new List<string>();
            List<CitationStyle> citationStyles = dbConnection.Table<CitationStyle>().ToList();
            foreach (CitationStyle style in citationStyles)
            {
                styleNames.Add(style.StyleName);
            }
            dbConnection.Close();
            return styleNames;
        }

        public static void UpdateCitationStyle(CitationStyle citationStyle)
        {
            EstablishConnection();
            dbConnection.Update(citationStyle);
            dbConnection.Close();
        }

        public static async void UpdateCitationStyleAsync(CitationStyle citationStyle)
        {
            EstablishAsyncConnection();
            await dbAsyncConnection.UpdateAsync(citationStyle);
            await dbAsyncConnection.CloseAsync();
        }

        public static void DeleteCitationStyle(int styleID)
        {
            EstablishConnection();
            dbConnection.Delete<CitationStyle>(styleID);
            dbConnection.Close();
        }

        public static async void DeleteCitationStyleAsync(int styleID)
        {
            EstablishAsyncConnection();
            await dbAsyncConnection.DeleteAsync<CitationStyle>(styleID);
            await dbAsyncConnection.CloseAsync();
        }

        #endregion

        #region REFERENCE LIST TABLE

        public static void InsertReferenceListObject(ReferenceListTableObj referenceListTableObj)
        {
            EstablishConnection();
            dbConnection.Insert(referenceListTableObj);
            dbConnection.Close();
        }

        public static async void InsertReferenceListObjectAsync(ReferenceListTableObj referenceListTableObj)
        {
            EstablishAsyncConnection();
            await dbAsyncConnection.InsertAsync(referenceListTableObj);
            await dbAsyncConnection.CloseAsync();
        }

        public static void InsertReferenceListBulk(List<ReferenceListTableObj> referenceListTableObjs)
        {
            EstablishConnection();
            dbConnection.InsertAll(referenceListTableObjs);
            dbConnection.Close();
        }

        public static void InsertReferenceListBulkAsync(List<ReferenceListTableObj> referenceListTableObjs)
        {
            EstablishAsyncConnection();
            dbAsyncConnection.InsertAllAsync(referenceListTableObjs);
            dbAsyncConnection.CloseAsync();
        }

        public static List<ReferenceListTableObj> Get_ReferenceList_by_ID(string listID)
        {
            EstablishConnection();
            List<ReferenceListTableObj> results = dbConnection.Table<ReferenceListTableObj>().Where(
                referenceObj => referenceObj.ListID == listID).ToList();
            if(results.Any())
            {
                dbConnection.Close();
                return results;
            }

            else 
            {
                dbConnection.Close();
                return null; 
            }
        }

        public static List<ReferenceListTableObj> Get_ReferenceList_by_Name(string listName)
        {
            EstablishConnection();
            List<ReferenceListTableObj> results = dbConnection.Table<ReferenceListTableObj>().Where(
                referenceObj => referenceObj.ReferenceListName == listName).ToList();
            if (results.Any())
            {
                dbConnection.Close();
                return results;
            }

            else
            {
                dbConnection.Close();
                return null;
            }
        }

        public static async Task<List<ReferenceListTableObj>> Get_ReferenceList_by_Name_Async(string listName)
        {
            EstablishAsyncConnection();
            List<ReferenceListTableObj> results = await dbAsyncConnection.Table<ReferenceListTableObj>().Where(
                referenceObj => referenceObj.ReferenceListName == listName).ToListAsync();
            if (results.Any())
            {
                await dbAsyncConnection.CloseAsync();
                return results;
            }

            else 
            {
                await dbAsyncConnection.CloseAsync();
                return null; 
            }
        }

        public static bool ReferenceListIDExists(string listID)
        {
            EstablishConnection();
            var results = dbConnection.Table<ReferenceListTableObj>().Where
                (
                    referencListObject => 
                    referencListObject.ListID == listID
                );
            if (results.Any())
            {
                dbConnection.Close();
                return true;
            }

            else
            {
                dbConnection.Close();
                return false;
            }
        }

        public static bool ReferenceListNameExists(string listName)
        {
            EstablishConnection();
            var results = dbConnection.Table<ReferenceListTableObj>().Where
                (
                    referencListObject =>
                    referencListObject.ReferenceListName == listName
                );
            if (results.Any())
            {
                dbConnection.Close();
                return true;
            }

            else
            {
                dbConnection.Close();
                return false;
            }
        }

        public static List<string> GetReferenceListNames()
        {
            EstablishConnection();
            List<string> listNames = new List<string>();
            List<ReferenceListTableObj> referenceListTableObjs = 
                dbConnection.Table<ReferenceListTableObj>().ToList();
            foreach (ReferenceListTableObj reference in referenceListTableObjs)
            {
                if(!listNames.Contains(reference.ListID))
                    listNames.Add(reference.ListID);
            }
            dbConnection.Close();
            return listNames;
        }

        public static void UpdateReferenceListObject(ReferenceListTableObj referenceListTableObj)
        {
            EstablishConnection();
            dbConnection.Update(referenceListTableObj);
            dbConnection.Close();
        }

        public static async void UpdateReferencListObjectAsync(ReferenceListTableObj referenceListTableObj)
        {
            EstablishAsyncConnection();
            await dbAsyncConnection.UpdateAsync(referenceListTableObj);
            await dbAsyncConnection.CloseAsync();
        }

        /* Reference List Objects are not deleted by their primary key (= 'Entry-ID')
         * because a List-ID can be linked to many Title-ID's therefore also many 'Entry-ID's'
         * but which are hardly relevant to the purpose this table fullfills
         * (creating/expressing a relationship between two types of objects).
         * Therefore in this case it much easier/ practical to use the distinct and unique key-combination
         * (List-ID/ Title-ID) rather than to scrabble for the primary key (again, in this case).
        */
        public static void Delete_Single_ReferenceListObject(string listID, int titleID)
        {
            EstablishConnection();
            dbConnection.Table<ReferenceListTableObj>().Delete(
                referenceEntry => 
                referenceEntry.ListID == listID
                && referenceEntry.TitleID == titleID);
            dbConnection.Close();
        }

        public static void DeleteReferenceListByName(string listName)
        {
            EstablishConnection();
            dbConnection.Table<ReferenceListTableObj>().Delete(
                    referenceListEntry =>
                    referenceListEntry.ReferenceListName == listName);
            dbConnection.Close();
        }

        public static async void DeleteReferenceListByNameAsync(string listName)
        {
            EstablishAsyncConnection();
            List<ReferenceListTableObj> referenceListTableObjs = await dbAsyncConnection.Table<ReferenceListTableObj>().Where
                (referenceListEntry => referenceListEntry.ReferenceListName == listName).ToListAsync();

            foreach (ReferenceListTableObj referenceListEntry in referenceListTableObjs)
            {
                await dbAsyncConnection.DeleteAsync(referenceListEntry.Entry_ID);
            }

            await dbAsyncConnection.CloseAsync();
        }

        #endregion
    }
}
