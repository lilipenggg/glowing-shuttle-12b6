using Microsoft.EntityFrameworkCore.Storage;

namespace web.Enums
{
    public class RoleType
    {
        private RoleType(string value)
        {
            Value = value; 
        }
        
        public string Value { get; set; }
        
        public static RoleType Customer { get { return new RoleType("Customer"); }}
        public static RoleType Vendor { get { return new RoleType("Vendor"); }}
        public static RoleType Employee { get { return new RoleType("Employee"); }}
    }
}