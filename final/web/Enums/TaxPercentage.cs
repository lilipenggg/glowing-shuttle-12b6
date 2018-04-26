namespace web.Enums
{
    public class TaxPercentage
    {
        public TaxPercentage()
        {
            Value = 0.071; 
        }
        
        public double Value { get; set; }
        
        public static TaxPercentage Tax { get { return new TaxPercentage(); }}
    }
}