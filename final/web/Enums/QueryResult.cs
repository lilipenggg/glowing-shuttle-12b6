namespace web.Enums
{
    public class QueryResult
    {
        private QueryResult(string value)
        {
            Value = value; 
        }
        
        public string Value { get; set; }
        
        public static QueryResult Succeed { get { return new QueryResult("Succeed"); }}
        public static QueryResult Failed { get { return new QueryResult("Failed"); }}
    }
}