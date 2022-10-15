namespace malone.Core.Sample.AdoNet.SqlServer.Middle.CL.Exceptions
{
    public enum ErrorCode
    {

        #region Technical Errors 1000 - 2999

                                TECH1000 = 1,

        #endregion

        #region Services Errors 3000 - 3999

                                SERVICE3000 = 3000,

        #endregion

        #region Business Errors 4000 - 4999
                                BUSINESS4000 = 4000,

        #endregion

        #region Business Validations Errors 5000 - 5999

                                BUSVAL5000 = 5000,

                                BUSVAL5001 = 5001,

        #endregion

        #region Data Access Errors 6000 - 6999

                                DATAACCESS6000 = 6000,

        #endregion

        #region Service Agent Errors 7000 - 7999

                                SERVAG7000 = 7000

        #endregion


    }
}
