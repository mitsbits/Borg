namespace Borg.System.Licencing.Contracts
{
    public interface IBorgLicenceService
    {
        IBorgLicence Retrieve();

        int ActiveApplicationServerCount();

        int ActiveApplicationUserCount();
    }
}