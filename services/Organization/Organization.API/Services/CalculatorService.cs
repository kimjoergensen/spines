namespace Organization.API.Services;

using System.Threading.Tasks;

using Organization.API.Services.Interfaces;

public class CalculatorService : ICalculatorService
{
    public ValueTask<MultiplyResult> MultiplyAsync(MultiplyRequest request)
    {
        var result = new MultiplyResult { Result = request.X * request.Y };
        return new ValueTask<MultiplyResult>(result);
    }
}
