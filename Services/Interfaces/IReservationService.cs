using TFBS.Dtos.Reservations;

namespace TFBS.Services.Interfaces;

public interface IReservationService
{
    Task<CreateReservationResponse> CreateAsync(CreateReservationRequest request);
}
