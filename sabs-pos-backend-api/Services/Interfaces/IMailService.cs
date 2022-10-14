using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public interface IMailService
    {
        Task Send(string subject, string body, params string[] receivers);
    }
}
