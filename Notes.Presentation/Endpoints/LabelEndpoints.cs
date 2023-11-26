using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notes.Application.Labels;
using Notes.Application.Labels.Commands;
using Notes.Application.Labels.Queries;
using Notes.Shared;

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

        group.MapPut("{labelId:Guid}", async (Guid labelId, string name, ISender sender) =>
        {
            var command = new UpdateLabelCommand(labelId, name);
            Result<LabelResponse> result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error.Description);
            }

            return Results.Ok(result.Value);
        });

        group.MapDelete("{labelId:Guid}", async (Guid labelId, ISender sender) =>
        {
            var command = new DeleteLabelCommand(labelId);
            Result result = await sender.Send(command);

            if(result.IsFailure && result.Error == LabelErrors.LabelNotFound)
            {
                return Results.NotFound(result.Error.Description);
            }

            return Results.NoContent();
        });
    }
}
