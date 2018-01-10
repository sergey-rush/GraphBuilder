using System;
using System.Collections.Generic;
using System.Data;
using GraphBuilder.Core;

namespace GraphBuilder.Data
{
    public abstract class DataManager : Context
    {
        private static DataManager _instance;

        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataProvider();
                }
                return _instance;
            }
        }


        public abstract List<SemanticType> GetSemanticTypes();
        public abstract int InsertSemanticType(SemanticType st);
        public abstract bool UpdateSemanticType(SemanticType st);
        public abstract bool DeleteSemanticType(int id);

        protected virtual SemanticType GetWordFromReader(IDataReader reader)
        {
            SemanticType word = new SemanticType()
            {
                Id = reader.GetInt32(0),
                ParentId = reader.GetInt32(1),
                Name = reader.GetString(2),
                Alias = reader.GetString(3)
            };

            return word;
        }


        protected virtual List<SemanticType> GetWordCollectionFromReader(IDataReader reader)
        {
            List<SemanticType> words = new List<SemanticType>();
            while (reader.Read())
                words.Add(GetWordFromReader(reader));
            return words;
        }
    }
}