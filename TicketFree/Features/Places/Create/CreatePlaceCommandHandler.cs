using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketFree.Interfaces;
using TicketFree.Validations;

namespace TicketFree.Features.Places.Create
{
    public class CreatePlaceCommandHandler(
        IApplicationDbContext dbContext)
        : IRequestHandler<CreatePlaceCommand, Result<Place>>
    {
        public async Task<Result<Place>> Handle(
            CreatePlaceCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var place = new Place
                {
                    PlaceId = Guid.NewGuid(),
                    PlaceName = request.PlaceName,
                    PlaceCountMembers = request.PlaceCountMembers,
                    PlaceHolder = request.PlaceHolder
                };

                dbContext.PlacesInfo.Add(place);
                await dbContext.SaveChangesAsync(cancellationToken);

                return Result<Place>.Success(place);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                if(sqlEx.Message.Contains("FK__PlacesInf__Place__07E124C1"))
                {
                    return Result<Place>.Failure(
                    new Error("DATABASE_ERROR", $"В PlaceHolder указан несуществующий пользователь"));
                }
                else
                {
                    return Result<Place>.Failure(
                    new Error("DATABASE_ERROR", $"Ошибка базы данных: {sqlEx.Message}"));
                }
                
            }
            catch (Exception ex)
            {
                return Result<Place>.Failure(
                    new Error("UNEXPECTED_ERROR", $"Неожиданная ошибка: {ex.Message}"));
            }
        }
    }
}
