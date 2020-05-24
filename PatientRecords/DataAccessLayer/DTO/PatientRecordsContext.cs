using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DTO
{
    public class PatientRecordsContext : DbContext
    {
        public PatientRecordsContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<DiseasedDto> Diseased { get; set; }

        public virtual DbSet<DoctorDto> Doctor { get; set; }

        public virtual DbSet<PatientDto> Patient { get; set; }

        public virtual DbSet<RecordDto> Record { get; set; }
    }
}
