namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class BorrowBookRequestDto
    {
        public string BookTitle { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
