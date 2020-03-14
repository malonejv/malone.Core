using malone.Core.EL.Identity;

namespace malone.Core.BL.Components.Identity.Providers
{
    public class CoreDataProtectorTokenProvider<TUserEntity> : DataProtectorTokenProvider<TUserEntity, int>
        where TUserEntity : CoreUser
    {
        public CoreDataProtectorTokenProvider(IDataProtector protector) : base(protector)
        {
        }
    }
}
