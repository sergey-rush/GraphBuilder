using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using GraphBuilder.Core;

namespace GraphBuilder.Data
{
    public class DataProvider : DataManager
    {
        private SQLiteConnection cn = new SQLiteConnection(ConnectionString);

        public DataProvider()
        {
            cn.Open();
        }

        public override List<SemanticType> GetSemanticTypes()
        {
            SQLiteCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT Id, ParentId, Name, Alias FROM SemanticTypes";
            return GetWordCollectionFromReader(cmd.ExecuteReader());
        }

        public override int InsertSemanticType(SemanticType st)
        {
            SQLiteCommand cmd = cn.CreateCommand();
            cmd.CommandText = "INSERT INTO SemanticTypes (ParentId, Name, Alias) VALUES (@ParentId, @Name, @Alias)";
            cmd.Parameters.AddWithValue("@ParentId", st.ParentId);
            cmd.Parameters.AddWithValue("@Name", st.Name);
            cmd.Parameters.AddWithValue("@Alias", st.Alias);
            return cmd.ExecuteNonQuery();
        }

        public override bool UpdateSemanticType(SemanticType st)
        {
            SQLiteCommand cmd = cn.CreateCommand();
            cmd.CommandText = "UPDATE SemanticTypes SET Name = @Name, Alias = @Alias WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", st.Id);
            cmd.Parameters.AddWithValue("@Name", st.Name);
            cmd.Parameters.AddWithValue("@Alias", st.Alias);
            int ret = cmd.ExecuteNonQuery();
            return (ret == 1);
        }

        public override bool DeleteSemanticType(int id)
        {
            SQLiteCommand cmd = cn.CreateCommand();
            cmd.CommandText = "DELETE FROM SemanticTypes WHERE (Id = @Id) OR (ParentId = @Id)";
            cmd.Parameters.AddWithValue("@Id", id);
            int ret = cmd.ExecuteNonQuery();
            return (ret == 1);
        }
    }
}
