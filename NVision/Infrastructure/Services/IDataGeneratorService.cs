using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IDataGeneratorService
    {
        Task GenerateData();
    }
}