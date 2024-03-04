namespace Movieasy.Application.Abstractions.Email
{
    public interface IEmailService
    {
        public Task SendAsync(Domain.Users.Email recipient, string subject, string body);
    }
}
