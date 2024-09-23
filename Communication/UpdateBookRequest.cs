namespace LibraryManager.Communication
{
    public class UpdateBookRequest
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Author { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AvailableUnits { get; set; }
    }
}
