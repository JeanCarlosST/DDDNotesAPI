using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notes.Application.Notes;
using Notes.Application.Notes.Commands;
using Notes.Application.Notes.Queries;
using Shared;

namespace Notes.Presentation.Endpoints;

public static class NotesEndpoints
{
    public static void MapNotesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/notes");

        group.MapPost("", async (CreateNoteCommand command, ISender sender) =>
        {
            Result result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error.Description);
            }

            return Results.Ok();
        });

        group.MapGet("/{noteId:Guid}", async (Guid noteId, ISender sender) =>
        {
            var query = new GetNoteQuery(noteId);
            Result<NoteResponse> noteResponse = await sender.Send(query);

            if (noteResponse.IsFailure && noteResponse.Error == NoteErrors.NoteNotFound)
            {
                return Results.NotFound(noteResponse.Error.Description);
            }

            return Results.Ok(noteResponse.Value);
        });
    }
}
