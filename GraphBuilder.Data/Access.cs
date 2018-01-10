using System.Collections.Generic;
using GraphBuilder.Core;

namespace GraphBuilder.Data
{
    public class Access
    {
        public static List<SemanticType> GetSemanticTypes()
        {
            List<SemanticType> words = new List<SemanticType>();
            words = Context.Data.GetSemanticTypes();
            return words;
        }

        public static int InsertSemanticType(SemanticType st)
        {
            int returnValue = Context.Data.InsertSemanticType(st);
            return returnValue;
        }

        public static bool UpdateSemanticType(SemanticType st)
        {
            bool returnValue = Context.Data.UpdateSemanticType(st);
            return returnValue;
        }

        public static bool DeleteSemanticType(int id)
        {
            bool returnValue = Context.Data.DeleteSemanticType(id);
            return returnValue;
        }
    }
}