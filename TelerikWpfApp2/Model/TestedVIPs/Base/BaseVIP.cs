namespace TelerikWpfApp2
{
    public class BaseVIP
    {
        private VIPConfig cfg;
        
        public int Id { get; set; }
        public string Type { get; set; } 
        public string Name { get; set; }
        public double Temperature { get; set; }
        public double VoltageInput { get; set; }
        public double VoltageOut1 { get; set; }
        public double VoltageOut2 { get; set; }
        
        public double CurrentInput { get; set; }

        public virtual bool WorkCheck()
        {
            return true;
        }
    }
}
