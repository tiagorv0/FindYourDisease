using FindYourDisease.Users.Domain.Interfaces;
using FindYourDisease.Users.Domain.Model;
using Microsoft.Extensions.Configuration;

namespace FindYourDisease.Users.Infra.Repository;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    public PatientRepository(IConfiguration config) : base(config)
    {
    }
}
