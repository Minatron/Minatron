using System;
using System.Data;
using System.IO;
using Microsoft.SqlServer.Types;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace ImagesStoreSystem.DBProvider
{
    class SqlGeographyUserType : IUserType
    {
        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x == null || y == null)
                return false;

            if (x is SqlGeography && y is SqlGeography)
            {
                var geo1 = x as SqlGeography;
                var geo2 = y as SqlGeography;

                string s1 = new string(geo1.AsTextZM().Value);
                string s2 = new string(geo2.AsTextZM().Value);

                return s1.Equals(s2) && (bool)(geo1.STSrid == geo2.STSrid);
            }

            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            object prop = NHibernateUtil.Binary.NullSafeGet(rs, names[0]);
            if (prop == null)
                return null;

            var geo = new SqlGeography();
            geo.Read(new BinaryReader(new MemoryStream((byte[])prop)));
            return geo;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
                return;
            }

            var geo = value as SqlGeography;
            var stream = new MemoryStream();
            geo.Write(new BinaryWriter(stream));
            stream.Position = 0;
            var geoByteArray = new byte[stream.Length];
            stream.Read(geoByteArray, 0, (int)stream.Length);
            ((IDataParameter) cmd.Parameters[index]).Value = geoByteArray;
        }

        public object DeepCopy(object value)
        {
            if (value == null)
                return null;

            var source = (SqlGeography)value;
            SqlGeography target = SqlGeography.STGeomFromText(source.AsTextZM(), (int)source.STSrid);
            return target;
        }

        public object Replace(object original, object target, object owner)
        {
            return DeepCopy(original);
        }

        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public SqlType[] SqlTypes
        {
            get
            {
                return new[] {NHibernateUtil.Binary.SqlType};
            }
        }

        public Type ReturnedType
        {
            get { return typeof(SqlGeography); }
        }

        public bool IsMutable
        {
            get { return true; }
        }
    }
}
