namespace ProjectCafeApi.Models
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string EmployeeCollection { get; set; }
        string BillCollection { get; set; }
        string IncomeCollection { get; set; }
        string OrderCollection { get; set; }
        string TableCollection { get; set; }
        string TypeFoodCollection { get; set; }
        string PromotionCollection { get; set; }
    }
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string EmployeeCollection { get; set; }
        public string BillCollection { get; set; }
        public string IncomeCollection { get; set; }
        public string OrderCollection { get; set; }
        public string TableCollection { get; set; }
        public string FoodCollection { get; set; }
        public string TypeFoodCollection { get; set; }
        public string PromotionCollection { get; set; }
    }
}