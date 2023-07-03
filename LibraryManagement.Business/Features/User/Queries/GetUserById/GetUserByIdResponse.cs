namespace LibraryManagement.Business.Features.User.Queries.GetUserById
{
    public class GetUserByIdResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the GSM number of the user.
        /// </summary>
        public string? Gsm { get; set; }

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        public string? Address { get; set; }
    }
}
