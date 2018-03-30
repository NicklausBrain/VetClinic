namespace VetClinic.Domain.Contracts
{
	public interface IMedicalRecordRepository : IRepository<MedicalRecord>
	{
		void RecordTreatment(Treatment treatment);
	}
}
