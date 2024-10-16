// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Aspire.Dashboard.Model;

public class ExecutableViewModel : ResourceViewModel
{
    public override string ResourceType => "Executable";
    public int? ProcessId { get; init; }
    public string? ExecutablePath { get; set; }
    public string? WorkingDirectory { get; set; }
    public List<string>? Arguments { get; set; }
}

