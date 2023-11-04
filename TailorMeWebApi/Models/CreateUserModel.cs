namespace TailorMeWebApi.Models
{
    public class CreateUserModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public float ChestMeasurement { get; set; }
        public float WaistMeasurement { get; set; }
        public float HipMeasurement { get; set; }

    }
}
