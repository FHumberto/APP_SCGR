﻿using SCGR.Domain.Abstractions.Errors;
using SCGR.Domain.Abstractions.Types;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SCGR.Application.Common.Wrappers;

[DebuggerNonUserCode]
[ExcludeFromCodeCoverage]
public class Result<T>
{
    public bool IsSuccess { get; }
    public Error? Error { get; }
    private readonly T? _value;

    protected Result(T value)
    {
        IsSuccess = true;
        _value = value;
        Error = null;
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
        _value = default;
    }

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException();

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
    public static implicit operator Result<T>(Error error) => new(error);
    public static implicit operator Result<T>(T value) => new(value);
}

public sealed class Result : Result<Unit>
{
    private Result(Unit value) : base(value) { }
    private Result(Error error) : base(error) { }
    public static Result Success() => new(Unit.Value);
    public static new Result Failure(Error error) => new(error);
    public static implicit operator Result(Error error) => new(error);
}
