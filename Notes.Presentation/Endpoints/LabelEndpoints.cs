using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notes.Application.Labels;
using Notes.Application.Labels.Commands;
using Notes.Application.Labels.Queries;
using Shared;

namespace Notes.Presentation.Endpoints;

public static class LabelEndpoints
{
    public static void MapLabelEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/labels");

        group.MapPost("", async (CreateLabelCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });

        group.MapGet("{labelId:Guid}", async (Guid labelId, ISender sender) =>
        {
            var query = new GetLabelQuery(labelId);
            Result<LabelResponse> labelResponse = await sender.Send(query);

            if(labelResponse.IsFailure && labelResponse.Error == LabelErrors.LabelNotFound)
            {
                return Results.NotFound(labelResponse.Error.Description);
            }

            return Results.Ok(labelResponse);
        });

        group.MapGet("", async (ISender sender) =>
        {
            return await sender.Send(new GetLabelsQuery());
        });
    }
}
