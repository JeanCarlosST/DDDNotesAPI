using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notes.Application.Notes;
using Notes.Application.Notes.Commands;
using Notes.Application.Notes.Queries;
using Notes.Shared;

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

        group.MapGet("{noteId:Guid}", async (Guid noteId, ISender sender) =>
        {
            var query = new GetNoteQuery(noteId);
            Result<NoteResponse> noteResponse = await sender.Send(query);

            if (noteResponse.IsFailure && noteResponse.Error == NoteErrors.NoteNotFound)
            {
                return Results.NotFound(noteResponse.Error.Description);
            }

            return Results.Ok(noteResponse.Value);
        });

        group.MapGet("", async (ISender sender) =>
        {
            var query = new GetNotesQuery();
            Result<List<NoteResponse>> noteResponseList = await sender.Send(query);

            return Results.Ok(noteResponseList.Value);
        });

        group.MapPut("{noteId:Guid}", async (Guid noteId, UpdateNoteRequest request, ISender sender) =>
        {
            var command = new UpdateNoteCommand(
                noteId,
                request.Title,
                request.LabelId,
                request.Elements);

            Result<NoteResponse> result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error.Description);
            }

            return Results.Ok(result.Value);
        });

        group.MapDelete("{noteId:Guid}", async (Guid noteId, ISender sender) =>
        {
            var command = new DeleteNoteCommand(noteId);
            Result result = await sender.Send(command);

            if (result.IsFailure && result.Error == NoteErrors.NoteNotFound)
            {
                return Results.NotFound(result.Error.Description);
            }

            return Results.NoContent();
        });
    }
}
