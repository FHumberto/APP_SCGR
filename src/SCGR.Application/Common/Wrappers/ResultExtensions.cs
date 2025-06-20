using SCGR.Domain.Abstractions.Errors;
using System.Diagnostics;

namespace SCGR.Application.Common.Wrappers;

[DebuggerNonUserCode]
public static class ResultExtensions
{
    public static TResult Match<TResult>
        (this Result result, Func<TResult> onSuccess, Func<Error, TResult> onFailure)
            => result.IsSuccess ? onSuccess() : onFailure(result.Error!);

    public static TResult Match<TValue, TResult>
        (this Result<TValue> result, Func<TValue, TResult> onSuccess, Func<Error, TResult> onFailure)
            => result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error!);
}
