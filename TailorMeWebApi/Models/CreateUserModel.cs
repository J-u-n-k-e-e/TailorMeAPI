namespace TailorMeWebApi.Models
{
    public class CreateUserModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int ChestMeasurement { get; set; }
        public int WaistMeasurement { get; set; }
        public int HipMeasurement { get; set; }

    }
}
