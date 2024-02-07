using MonthlyCosts.Domain.Core.Commands;
using MonthlyCosts.Domain.Core.Exceptions;

namespace MonthlyCosts.Domain.Core.CommandHandlers;

public abstract class CommandHandler
{
    public virtual void ValidateAndThrow(Command command)
    {
        if (command.IsValid()) return;

        throw InvalidCommandException.GetCommandInvalidException(command.ValidationResult);
    }
}
