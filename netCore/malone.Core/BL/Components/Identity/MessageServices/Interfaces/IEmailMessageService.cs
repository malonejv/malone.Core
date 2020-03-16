using malone.Core.EL.Identity;
using System.Threading.Tasks;

namespace malone.Core.BL.Components.Identity.MessageServices.Interfaces
{
    public interface IEmailMessageService 
    {
        Task SendAsync(IdentityMessage message);
    }
}
