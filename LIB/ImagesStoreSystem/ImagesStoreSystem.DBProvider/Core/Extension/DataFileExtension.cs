using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using NHibernate;

namespace ImagesStoreSystem.DBProvider.Core.Extension
{
    public static class DataFileExtension
    {
        public static void UploadContent(this DataFile file, ISession session, Stream src)
        {
			if (src != null)
			{
				CreateEmptyContent(session, file);
				using (Stream dst = GetContentStream(session, file, FileAccess.Write))
				{
					src.CopyTo(dst);
				}
			}
			else
			{
				RemoveContent(file, session);
			}
        }

        public static void DownloadContent(this DataFile file, ISession session, Stream dst)
        {
			using (Stream src = GetContentStream(session, file, FileAccess.Read))
			{
				if (src != null)
				{
					src.CopyTo(dst);
				}
			}
        }

        public static void RemoveContent(this DataFile file, ISession session)
        {
            session.CreateSQLQuery(string.Format("UPDATE [Files] SET [FileContent]=null WHERE [FileID]={0}", file.Id)).ExecuteUpdate();
        }

        private static void CreateEmptyContent(ISession session, DataFile file)
        {
            session.CreateSQLQuery(string.Format(@"UPDATE [Files] SET [FileContent]=(CAST ('' as varbinary(max))) WHERE [FileID]={0}", file.Id)).ExecuteUpdate();
        }

        private static Stream GetContentStream(ISession session, DataFile file, FileAccess access)
        {
            var res = session.CreateSQLQuery(string.Format(@"SELECT FileContent.PathName() as path, GET_FILESTREAM_TRANSACTION_CONTEXT() as context FROM Files WHERE FileID='{0}'", file.Id))
                .AddScalar("path", NHibernateUtil.String)
                .AddScalar("context", NHibernateUtil.Binary)
                .List()[0] as IList;

            string path = res[0] as string;
			if (path != null)
			{
				var ress = new SqlFileStream(path, res[1] as byte[], access, FileOptions.SequentialScan, 0);
				return ress;
			}
			return null;
            
        }


        public static BackupFile CreateBuckup(this DataFile file, ISession session, BackupsStorage storage)
        {
            var obj = new BackupFile(session.Get<DataFile>(file.Id), session.Get<BackupsStorage>(storage.Id));
            return session.Get<BackupFile>(session.Save(obj));      
        }
    }
}
