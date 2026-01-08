using TFBS.Dtos.Trips;

namespace TFBS.Services.Interfaces;

public interface ITripService
{
    Task<CreateTripResponse> CreateAsync(CreateTripRequest request);
    Task<CompleteTripResponse> CompleteAsync(int tripId, CompleteTripRequest request);


}
